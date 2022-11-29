﻿using Com.Efrata.Service.Sales.Lib.Helpers;
using Com.Efrata.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Efrata.Service.Sales.Lib.Services;
using Com.Efrata.Service.Sales.Lib.Utilities.BaseClass;
using Com.Efrata.Service.Sales.Lib.ViewModels.Garment;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Efrata.Service.Sales.Lib.BusinessLogic.Logic.Garment
{
    public class GarmentProductionOrderReportLogic : BaseMonitoringLogic<GarmentProductionOrderReportViewModel>
    {
        private SalesDbContext dbContext;
        private IIdentityService identityService;
        private IHttpClientService httpClientService;

        public GarmentProductionOrderReportLogic(SalesDbContext dbContext, IIdentityService identityService, IHttpClientService httpClientService)
        {
            this.dbContext = dbContext;
            this.identityService = identityService;
            this.httpClientService = httpClientService;
        }

        public override IQueryable<GarmentProductionOrderReportViewModel> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<CostCalculationGarment> Query = dbContext.CostCalculationGarments;

            if (filter.year > 0 && filter.month > 0)
            {
                DateTimeOffset min = new DateTimeOffset(filter.year, filter.month, 1, 0, 0, 0, TimeSpan.FromHours(identityService.TimezoneOffset));
                DateTimeOffset max = new DateTimeOffset(filter.month < 12 ? filter.year : filter.year + 1, filter.month % 12 + 1, 1, 0, 0, 0, TimeSpan.FromHours(identityService.TimezoneOffset));
                Query = Query.Where(w => w.DeliveryDate >= min && w.DeliveryDate < max);
            }
            else
            {
                throw new Exception("Invalid Year or Month");
            }
            if (!string.IsNullOrWhiteSpace(filter.unit))
            {
                Query = Query.Where(w => w.UnitCode == filter.unit);
            }
            if (!string.IsNullOrWhiteSpace(filter.section))
            {
                Query = Query.Where(w => w.Section == filter.section);
            }
            if (!string.IsNullOrWhiteSpace(filter.buyer))
            {
                Query = Query.Where(w => w.BuyerBrandCode == filter.buyer);
            }

            Query = Query.Where(w => w.IsApprovedKadivMD == true);

            var costCalculations = Query.Select(s => new CostCalculationGarment
            {
                Id = s.Id,
                DeliveryDate = s.DeliveryDate,
                BuyerCode = s.BuyerCode,
                BuyerBrandName = s.BuyerBrandName,
                Section = s.Section,
                Commodity = s.Commodity,
                Article = s.Article,
                RO_Number = s.RO_Number,
                CreatedUtc = s.CreatedUtc,
                Quantity = s.Quantity,
                UOMUnit = s.UOMUnit,
                ConfirmPrice = s.ConfirmPrice,
                ConfirmDate = s.ConfirmDate,
                IsApprovedKadivMD = s.IsApprovedKadivMD,
                CostCalculationGarment_Materials = s.CostCalculationGarment_Materials
            }).OrderBy(o => o.DeliveryDate);

            IQueryable<ViewModels.IntegrationViewModel.BuyerViewModel> buyerQ = GetGarmentBuyer().AsQueryable();

            var diffFirstDayInYearWithMonday = new DateTime(filter.year, 1, 1).DayOfWeek - DayOfWeek.Monday;
            diffFirstDayInYearWithMonday = diffFirstDayInYearWithMonday + (diffFirstDayInYearWithMonday > -1 ? 0 : 7);
            var garmentProductionOrders = costCalculations.ToList()
                .GroupBy(cc => {
                    var week = Math.Ceiling((decimal)(cc.DeliveryDate.ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).DayOfYear + diffFirstDayInYearWithMonday) / 7);
                    week = week > 52 ? 1 : week;
                    return $"W - {week}";
                })
                .Select(groupWeek => new GarmentProductionOrderReportViewModel
                {
                    Week = groupWeek.Key,
                    Buyers = groupWeek
                        .GroupBy(week => week.BuyerBrandName)
                        .Select(groupBuyer => new GarmentProductionOrderReportBuyerViewModel
                        {
                            Buyer = groupBuyer.Key,
                            Type = buyerQ.Where(x => x.Code == groupBuyer.First().BuyerCode).Select(x => x.Type).FirstOrDefault(),
                            Quantities = groupBuyer.Sum(s => s.Quantity),
                            Amounts = groupBuyer.Sum(s => s.Quantity * s.ConfirmPrice),
                            Details = groupBuyer
                                .Select(detail => GetGarmentProductionOrder(detail))
                                .OrderBy(o => o.Date)
                                .ToList()
                        })
                        .OrderBy(o => o.Buyer)
                        .ToList()
                })
                .ToList();

            return garmentProductionOrders.AsQueryable();
        }

        GarmentProductionOrderReportDetailViewModel GetGarmentProductionOrder(CostCalculationGarment cc)
        {
            return new GarmentProductionOrderReportDetailViewModel
            {
                Section = cc.Section,
                Commodity = cc.Commodity,
                Article = cc.Article,
                RONo = cc.RO_Number,
                Date = cc.CreatedUtc.AddHours(identityService.TimezoneOffset),
                Quantity = cc.Quantity,
                Uom = cc.UOMUnit,
                ConfirmPrice = cc.ConfirmPrice,
                Amount = cc.Quantity * cc.ConfirmPrice,
                ConfirmDate = cc.ConfirmDate.ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).DateTime,
                ShipmentDate = cc.DeliveryDate.ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).DateTime,
                ValidationPPIC = cc.IsApprovedKadivMD ? "SUDAH" : "BELUM",
                TermPayment = cc.CostCalculationGarment_Materials.Any(x => x.isFabricCM == true) ? "CMT" : "FOB"
            };
        }

        private class Filter
        {
            public int year { get; set; }
            public int month { get; set; }
            public string unit { get; set; }
            public string section { get; set; }
            public string buyer { get; set; }
        }

        public List<ViewModels.IntegrationViewModel.BuyerViewModel> GetGarmentBuyer()
        {
            string buyerUri = "master/garment-buyers/all";
            var response = httpClientService.GetAsync($@"{APIEndpoint.Core}{buyerUri}").Result.Content.ReadAsStringAsync();

            if (response != null)
            {
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Result);
                var json = result.Single(p => p.Key.Equals("data")).Value;
                List<ViewModels.IntegrationViewModel.BuyerViewModel> buyerList = JsonConvert.DeserializeObject<List<ViewModels.IntegrationViewModel.BuyerViewModel>>(json.ToString());

                return buyerList;
            }
            else
            {
                return null;
            }
        }
    }
}
