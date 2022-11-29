﻿using Com.Efrata.Service.Sales.Lib.Utilities;
using Com.Efrata.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System.ComponentModel.DataAnnotations;

namespace Com.Efrata.Service.Sales.Lib
{
    public class SalesContract
    {
        public Buyer Buyer { get; set; }
        public Comodity Comodity { get; set; }
        public string ComodityDescription { get; set; }
        public decimal? OrderQuantity { get; set; }
        public string SalesContractNo { get; set; }
        public string UomUnit { get; set; }
        public string DeliveredTo { get; set; }
    }
}