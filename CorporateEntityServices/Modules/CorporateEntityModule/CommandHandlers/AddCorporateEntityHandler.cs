using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CorporateEntityServices.Modules.CorporateEntityModule.Commands;
using CorporateEntityServices.Modules.CorporateEntityModule.Entities;
using CorporateEntityServices.Modules.CorporateEntityModule.Store;
using MediatR;

namespace CorporateEntityServices.Modules.CorporateEntityModule.CommandHandlers
{
    public class AddCorporateEntityHandler : IAsyncRequestHandler<AddCorporateEntityCommand, bool>
    {
        public async Task<bool> Handle(AddCorporateEntityCommand message)
        {

            return await Task.Factory.StartNew(() =>
            {
                CorporateEntityDataStore.Add(new CorporateEntity()
                {
                    Id = message.Id,
                    InternalReference = Guid.NewGuid(),
                    Name = message.Name
                });
                return true;
            });
        }


    }
    public class ModifyCorporateEntityHandler : IAsyncRequestHandler<ModifyCorporateEntityCommand, bool>
    {
        public async Task<bool> Handle(ModifyCorporateEntityCommand message)
        {

            return await Task.Factory.StartNew(() =>
            {
                CorporateEntityDataStore.Modify(new CorporateEntity()
                {
                    Id = message.Id,
                    InternalReference = message.InternalReference,
                    Name = message.Name
                });
                return true;
            });
        }


    }
}