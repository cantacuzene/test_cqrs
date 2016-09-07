using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediatR;

namespace CorporateEntityServices.Modules.CorporateEntityModule.Commands
{
    public class AddCorporateEntityCommand : IAsyncRequest<bool>
    {
        public int Id;
        public Guid InternalReference;
        public string Name;

    }

    public class ModifyCorporateEntityCommand : IAsyncRequest<bool>
    {
        public int Id;
        public Guid InternalReference;
        public string Name;

    }
}