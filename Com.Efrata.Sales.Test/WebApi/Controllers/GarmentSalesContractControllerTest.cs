﻿using AutoMapper;
using Com.Efrata.Sales.Test.WebApi.Utils;
using Com.Efrata.Service.Sales.Lib.BusinessLogic.Interface.GarmentSalesContractInterface;
using Com.Efrata.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Efrata.Service.Sales.Lib.Services;
using Com.Efrata.Service.Sales.Lib.ViewModels.GarmentSalesContractViewModels;
using Com.Efrata.Service.Sales.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Efrata.Sales.Test.WebApi.Controllers
{
    public class GarmentSalesContractControllerTest : BaseControllerTest<GarmentSalesContractController, GarmentSalesContract, GarmentSalesContractViewModel, IGarmentSalesContract>
    {
        protected override GarmentSalesContractController GetController((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IGarmentSalesContract> Facade, Mock<IMapper> Mapper, Mock<IServiceProvider> ServiceProvider) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            mocks.ServiceProvider.Setup(s => s.GetService(typeof(IHttpClientService))).Returns(new HttpClientTestService());
            GarmentSalesContractController controller = (GarmentSalesContractController)Activator.CreateInstance(typeof(GarmentSalesContractController), mocks.IdentityService.Object, mocks.ValidateService.Object, mocks.Facade.Object, mocks.Mapper.Object, mocks.ServiceProvider.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(GarmentSalesContract));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void GetPDF_When_Model_State_Invalid()
        {
            var mocks = GetMocks();
            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "test");

            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, statusCode);

        }

        [Fact]
        public void Get_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Get_PDF_OK_Ekspor()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            GarmentSalesContractViewModel vm = new GarmentSalesContractViewModel()
            {
                SCType = "Ekspor",
                AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel(),
                SalesContractROs = new List<GarmentSalesContractROViewModel>()
                {
                    new GarmentSalesContractROViewModel
                    {
                        Uom = new Service.Sales.Lib.ViewModels.IntegrationViewModel.UomViewModel(),
                        Items = new List<GarmentSalesContractItemViewModel>()
                        {
                            new GarmentSalesContractItemViewModel()
                        }
                    }
                }
                
            };
            mocks.Mapper.Setup(s => s.Map<GarmentSalesContractViewModel>(It.IsAny<GarmentSalesContract>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_PDF_Lokal_OK()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            GarmentSalesContractViewModel vm = new GarmentSalesContractViewModel()
            {
                SCType = "Lokal",
                AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel(),
                SalesContractROs = new List<GarmentSalesContractROViewModel>()
                {
                    new GarmentSalesContractROViewModel
                    {
                        Uom = new Service.Sales.Lib.ViewModels.IntegrationViewModel.UomViewModel(),
                        Items = new List<GarmentSalesContractItemViewModel>()
                        {
                            new GarmentSalesContractItemViewModel()
                        }
                    }
                }

            };
            mocks.Mapper.Setup(s => s.Map<GarmentSalesContractViewModel>(It.IsAny<GarmentSalesContract>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);


        }

        [Fact]
        public void Get_PDF_Lokal_OK_NoItems()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            GarmentSalesContractViewModel vm1 = new GarmentSalesContractViewModel()
            {
                SCType = "Lokal",
                AccountBank = new Service.Sales.Lib.ViewModels.IntegrationViewModel.AccountBankViewModel()
                {
                    AccountName="aa",
                    BankName="bb"
                },
                SalesContractROs = new List<GarmentSalesContractROViewModel>()
                {
                    new GarmentSalesContractROViewModel
                    {
                        Uom = new Service.Sales.Lib.ViewModels.IntegrationViewModel.UomViewModel(),

                    }
                }

            };
            mocks.Mapper.Setup(s => s.Map<GarmentSalesContractViewModel>(It.IsAny<GarmentSalesContract>()))
                .Returns(vm1);
            var controller1 = GetController(mocks);
            var response1 = controller1.GetPDF(1).Result;

            Assert.NotNull(response1);

        }

        [Fact]
        public async Task GetByRO_NotNullModel_ReturnOK()
        {
            var ViewModel = this.ViewModel;
            ViewModel.SalesContractROs = new List<GarmentSalesContractROViewModel>();

            var mocks = GetMocks();
            mocks.Facade
                .Setup(f => f.ReadByRO(It.IsAny<string>()))
                .Returns(Model);
            mocks.Mapper
                .Setup(m => m.Map<GarmentSalesContractViewModel>(It.IsAny<GarmentSalesContract>()))
                .Returns(ViewModel);

            var controller = GetController(mocks);
            var response = controller.GetByRO(It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetByRO_NullModel_ReturnNotFound()
        {
            var mocks = GetMocks();
            mocks.Mapper.Setup(f => f.Map<GarmentSalesContractViewModel>(It.IsAny<GarmentSalesContract>())).Returns(ViewModel);
            mocks.Facade.Setup(f => f.ReadByRO(It.IsAny<string>())).Returns((GarmentSalesContract)null);

            var controller = GetController(mocks);
            var response = controller.GetByRO(It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task GetByRO_ThrowException_ReturnInternalServerError()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.ReadByRO(It.IsAny<string>())).Throws(new Exception());

            var controller = GetController(mocks);
            var response = controller.GetByRO(It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task GetByRO_BadRequest()
        {
            var mocks = this.GetMocks();

            var controller = GetController(mocks);
            controller.ModelState.AddModelError("key", "value");
            var response = controller.GetByRO(It.IsAny<string>());

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
    }
}

