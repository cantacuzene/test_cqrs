using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CorporateEntityServices.Modules.CorporateEntityModule.Entities;
using CorporateEntityServices.Modules.CorporateEntityModule.Queries;
using CorporateEntityServices.Modules.CorporateEntityModule.Store;
using MediatR;

namespace CorporateEntityServices.Modules.CorporateEntityModule.QueryHandler
{
    public class GetCorporateEntityQueryHandler : IAsyncRequestHandler<GetCorporateEntityQuery, IEnumerable<CorporateEntity>>
    {
        public async Task<IEnumerable<CorporateEntity>> Handle(GetCorporateEntityQuery message)
        {
            return await Task.Factory.StartNew(() =>
            {
                return CorporateEntityDataStore.get((x) => x.Id == message.Id);
            });
        }

    }

    public class GetCorporateEntitiesQueryHandler : IAsyncRequestHandler<GetCorporateEntitiesQuery, IEnumerable<CorporateEntity>>
    {
        public async Task<IEnumerable<CorporateEntity>> Handle(GetCorporateEntitiesQuery message)
        {
            return await Task.Factory.StartNew(() =>
            {
                return CorporateEntityDataStore.get(message.startIndex,message.length);
            });
        }

    }
}