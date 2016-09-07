using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorporateEntityServices.Modules.CorporateEntityModule.Entities;
using MediatR;

namespace CorporateEntityServices.Modules.CorporateEntityModule.Queries
{
    public class GetCorporateEntityQuery : IAsyncRequest<IEnumerable<CorporateEntity>>
    {
        public int Id;
    }
    public class GetCorporateEntitiesQuery : IAsyncRequest<IEnumerable<CorporateEntity>>
    {
        public int startIndex;
        public int length;
    }
}