﻿using Com.Efrata.Service.Sales.Lib.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Efrata.Service.Sales.Lib.ViewModels.SalesInvoice
{
    public class SalesInvoiceDetailViewModel : BaseViewModel
    {
        public int? ShippingOutId { get; set; }
        public string BonNo { get; set; }

        public ICollection<SalesInvoiceItemViewModel> SalesInvoiceItems { get; set; }
    }
}
