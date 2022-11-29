﻿using AutoMapper;
using Com.Efrata.Sales.Test.BussinesLogic.DataUtils.GarmentOmzetTargetDataUtils;
using Com.Efrata.Sales.Test.BussinesLogic.Utils;
using Com.Efrata.Service.Sales.Lib;
using Com.Efrata.Service.Sales.Lib.AutoMapperProfiles.GarmentOmzetTargetProfiles;
using Com.Efrata.Service.Sales.Lib.BusinessLogic.Facades.GarmentOmzetTargetFacades;
using Com.Efrata.Service.Sales.Lib.BusinessLogic.Logic.GarmentOmzetTargetLogics;
using Com.Efrata.Service.Sales.Lib.Models.GarmentOmzetTargetModel;
using Com.Efrata.Service.Sales.Lib.Services;
using Com.Efrata.Service.Sales.Lib.ViewModels.GarmentOmzetTargetViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Efrata.Sales.Test.BussinesLogic.Facades.GarmentOmzetTargetTest
{
    public class GarmentOmzetTargetTest : BaseFacadeTest<SalesDbContext, GarmentOmzetTargetFacade, GarmentOmzetTargetLogic, GarmentOmzetTarget, GarmentOmzetTargetDataUtil>
    {
        private const string ENTITY = "GarmentOmzetTargets";

        public GarmentOmzetTargetTest() : base(ENTITY)
        {
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GarmentOmzetTargetMapper>();
            });
            var mapper = configuration.CreateMapper();

            GarmentOmzetTargetViewModel vm = new GarmentOmzetTargetViewModel { Id = 1 };
            GarmentOmzetTarget model = mapper.Map<GarmentOmzetTarget>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}