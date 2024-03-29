﻿using Com.Efrata.Sales.Test.BussinesLogic.Utils;
using Com.Efrata.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MaxWHConfirmFacades;
using Com.Efrata.Service.Sales.Lib.Models.GarmentMasterPlan.MaxWHConfirmModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Efrata.Sales.Test.BussinesLogic.DataUtils.GarmentMasterPlan.MaxWHConfirmDataUtils
{
    public class MaxWHConfirmDataUtil : BaseDataUtil<MaxWHConfirmFacade, MaxWHConfirm>
    {
        public MaxWHConfirmDataUtil(MaxWHConfirmFacade facade) : base(facade)
        {
        }

        public override Task<MaxWHConfirm> GetNewData()
        {
            var wh = new MaxWHConfirm
            {
                UnitMaxValue=70,
                SKMaxValue=2
            };
            
            return Task.FromResult(wh);
        }
    }
}
