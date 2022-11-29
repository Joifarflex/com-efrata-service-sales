﻿using Com.Efrata.Sales.Test.BussinesLogic.Utils;
using Com.Efrata.Service.Sales.Lib.BusinessLogic.Facades.GarmentBookingOrderFacade;
using Com.Efrata.Service.Sales.Lib.Models.GarmentBookingOrderModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Efrata.Sales.Test.BussinesLogic.DataUtils.GarmentBookingOrderDataUtils
{
    public class ExpiredGarmentBookingOrderDataUtil : BaseDataUtil<ExpiredGarmentBookingOrderFacade, GarmentBookingOrder>
    {
        public ExpiredGarmentBookingOrderDataUtil(ExpiredGarmentBookingOrderFacade facade) : base(facade)
        {
        }

        public override Task<GarmentBookingOrder> GetNewData()
        {
            var bookingOrder = new GarmentBookingOrder
            {
                BuyerId = 1,
                BookingOrderNo = "BA1234",
                BookingOrderDate = new DateTime(2019, 01, 01),
                BuyerCode = "UnitCode",
                BuyerName = "UnitName",
                SectionId = 1,
                SectionCode = "SectionCode",
                SectionName = "SectionName",
                ConfirmedQuantity = 1,
                CanceledQuantity = 55,
                IsBlockingPlan = true,
                OrderQuantity = 9,
                ExpiredBookingQuantity = 9,
                HadConfirmed = true,
                Remark = "Remark",
                DeliveryDate = new DateTimeOffset().AddDays(30),
                Items = new List<GarmentBookingOrderItem>()
            };

            bookingOrder.Items.Add(new GarmentBookingOrderItem
            {
                ComodityId = 1,
                ComodityCode = "ComodityCode",
                ComodityName = "ComodityName",
                ConfirmQuantity = 55,
                Remark = "Remark",
            });
            return Task.FromResult(bookingOrder);
        }
    }
}