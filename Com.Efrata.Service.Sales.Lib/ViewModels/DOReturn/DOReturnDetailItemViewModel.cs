﻿using Com.Efrata.Service.Sales.Lib.Utilities;
using Com.Efrata.Service.Sales.Lib.ViewModels.DOSales;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Efrata.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnDetailItemViewModel : BaseViewModel
    {
        //public DOSalesViewModel DOSales { get; set; }
        public int? DOSalesId { get; set; }
        public string DOSalesNo { get; set; }
    }
}
