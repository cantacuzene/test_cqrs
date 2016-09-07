using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CorporateEntityServices.Modules.CorporateEntityModule.Commands;
using CorporateEntityServices.Modules.CorporateEntityModule.Queries;
using MediatR;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;

namespace CorporateEntityServices.Modules.CorporateEntityModule
{
    public class CorporateEntityModule : NancyModule
    {
        private readonly IMediator _mediator;
        public CorporateEntityModule(IMediator mediator):base("/CorporateEntities")
        {
            _mediator = mediator;
            Get["/{startIndex}/{length}", true]= GetCorporateEntitiesService;
            Get["/{id}",true]= GetCorporateEntityService;
            Post["/", true] = AddCorporateEntityService;
            Patch["/", true] = ModifyCorporateEntityService;
        }
        private async Task<dynamic> HelloWorld(dynamic _, CancellationToken ctx)
        {
            return "Hello from nancy asynx";
        }
        private async Task<dynamic> GetCorporateEntityService(dynamic _, CancellationToken ctx)
        {
            var response= await _mediator.SendAsync(new GetCorporateEntityQuery()
            {
                Id = _.id
            });
            return Response.AsJson(response);
        }

        private async Task<dynamic> GetCorporateEntitiesService(dynamic _, CancellationToken ctx)
        {
            var response = await _mediator.SendAsync(new GetCorporateEntitiesQuery()
            {
                startIndex = _.startIndex ??0,
                length = _.length ?? 100
            });
            return Response.AsJson(response);
        }

        private async Task<dynamic> AddCorporateEntityService(dynamic _, CancellationToken ctx)
        {
            dynamic request = JsonConvert.DeserializeObject(Request.Body.AsString(),typeof(AddCorporateEntityCommand));
            var response = await _mediator.SendAsync(new AddCorporateEntityCommand()
            {
                Id = request.Id,
                
                Name = request.Name
            });
            return Response.AsJson(response);
        }
        private async Task<dynamic> ModifyCorporateEntityService(dynamic _, CancellationToken ctx)
        {
            dynamic request = JsonConvert.DeserializeObject(Request.Body.AsString(), typeof(ModifyCorporateEntityCommand));
            var response = await _mediator.SendAsync(new ModifyCorporateEntityCommand()
            {
                Id = request.Id,
                InternalReference = request.InternalReference ?? Guid.Empty,
                Name = request.Name
            });
            return Response.AsJson(response);
        }
    }
}