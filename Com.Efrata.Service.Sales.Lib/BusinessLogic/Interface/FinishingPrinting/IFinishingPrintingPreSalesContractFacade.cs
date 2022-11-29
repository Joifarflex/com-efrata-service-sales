﻿using Com.Efrata.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Efrata.Service.Sales.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Efrata.Service.Sales.Lib.BusinessLogic.Interface.FinishingPrinting
{
    public interface IFinishingPrintingPreSalesContractFacade : IBaseFacade<FinishingPrintingPreSalesContractModel>
    {
        Task<int> PreSalesPost(List<long> listId);
        Task<int> PreSalesUnpost(long id);
    }
}
