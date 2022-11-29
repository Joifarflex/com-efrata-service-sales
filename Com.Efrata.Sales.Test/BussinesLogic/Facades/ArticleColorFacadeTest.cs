﻿using Com.Efrata.Sales.Test.BussinesLogic.DataUtils;
using Com.Efrata.Sales.Test.BussinesLogic.Utils;
using Com.Efrata.Service.Sales.Lib;
using Com.Efrata.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Efrata.Service.Sales.Lib.BusinessLogic.Logic;
using Com.Efrata.Service.Sales.Lib.Models;
using Com.Efrata.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Efrata.Sales.Test.BussinesLogic.Facades
{
    public class ArticleColorFacadeTest : BaseFacadeTest<SalesDbContext, ArticleColorFacade, ArticleColorLogic, ArticleColor, ArticleColorDataUtil>
    {
        private const string ENTITY = "ArticleColor";
        public ArticleColorFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ArticleColorLogic)))
                .Returns(Activator.CreateInstance(typeof(ArticleColorLogic), serviceProviderMock.Object, identityService, dbContext) as ArticleColorLogic);

            return serviceProviderMock;
        }
    }
}
