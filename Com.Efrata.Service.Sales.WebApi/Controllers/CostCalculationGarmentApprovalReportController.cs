﻿
using Com.Efrata.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Efrata.Service.Sales.Lib.Services;
using Com.Efrata.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Efrata.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Efrata.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/cost-calculation-garment-approval-report")]
    [Authorize]
    public class CostCalculationGarmentApprovalReportController : BaseMonitoringController<CostCalculationGarmentApprovalReportViewModel, ICostCalculationGarmentApprovalReport>
    {
        private readonly static string apiVersion = "1.0";

        public CostCalculationGarmentApprovalReportController(IIdentityService identityService, ICostCalculationGarmentApprovalReport facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}