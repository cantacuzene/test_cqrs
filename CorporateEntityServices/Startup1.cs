using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.Variance;
using MediatR;
using Microsoft.Owin;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using Owin;

[assembly: OwinStartup(typeof(CorporateEntityServices.Startup))]

namespace CorporateEntityServices
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(options => options.Bootstrapper = new Bootstrapper());
        }
    }

    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            container.Update(builder => builder.RegisterSource(new ContravariantRegistrationSource()));
            container.Update(builder => builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces());
            container.Update(builder => builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            }));
            container.Update(builder => builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>) c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            }));
            container.Update(builder => builder.RegisterAssemblyTypes(typeof(Newtonsoft.Json.IJsonLineInfo).Assembly).AsImplementedInterfaces());
        }
        protected override void ConfigureRequestContainer(ILifetimeScope container, NancyContext context)
        {
            container.Update(builder => builder.RegisterAssemblyTypes(typeof(Startup).Assembly).AsImplementedInterfaces());
        }
    }
}
