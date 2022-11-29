﻿using Com.Efrata.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Efrata.Service.Sales.Lib.ViewModels.IntegrationViewModel
{
    public class StandardTestsViewModel : BaseViewModel
    {
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }

    }
}
