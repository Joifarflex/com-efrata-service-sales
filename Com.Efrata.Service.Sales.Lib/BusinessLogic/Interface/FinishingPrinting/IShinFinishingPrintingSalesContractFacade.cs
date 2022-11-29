﻿using Com.Efrata.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Efrata.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Efrata.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting
{
    public interface IShinFinishingPrintingSalesContractFacade : IBaseFacade<FinishingPrintingSalesContractModel>
    {
        Task<FinishingPrintingSalesContractModel> ReadParent(long id);
    }
}
