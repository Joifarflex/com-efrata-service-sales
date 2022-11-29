﻿using Com.Efrata.Service.Sales.Lib.Models.GarmentSalesContractModel;
using Com.Efrata.Service.Sales.Lib.Services;
using Com.Efrata.Service.Sales.Lib.Utilities;
using Com.Efrata.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Efrata.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics
{
    public class GarmentSalesContractROLogic : BaseLogic<GarmentSalesContractRO>
    {
        public GarmentSalesContractROLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<GarmentSalesContractRO> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<long> GetGSalesContractIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.GarmentSalesContract.Id == id).Select(d => d.Id));
        }

        public override void UpdateAsync(long id, GarmentSalesContractRO model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            DbSet.Update(model);
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }
    }
}
