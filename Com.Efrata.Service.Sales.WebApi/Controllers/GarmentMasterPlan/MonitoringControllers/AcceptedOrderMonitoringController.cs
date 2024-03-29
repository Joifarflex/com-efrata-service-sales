﻿using Com.Efrata.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Efrata.Service.Sales.Lib.Services;
using Com.Efrata.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Com.Efrata.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Efrata.Service.Sales.WebApi.Controllers.GarmentMasterPlan.MonitoringControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/garment-master-plan/sewing-blocking-plans-accepted-order-monitoring")]
    [Authorize]
    public class AcceptedOrderMonitoringController : BaseMonitoringController<AcceptedOrderMonitoringViewModel, IAcceptedOrderMonitoringFacade>
    {
        private readonly static string apiVersion = "1.0";

        public AcceptedOrderMonitoringController(IIdentityService identityService, IAcceptedOrderMonitoringFacade facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}
