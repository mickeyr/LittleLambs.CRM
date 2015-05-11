using System;
using System.Web.Http;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Integration.WebApi;
using LittleLambs.CRM.DependencyInjection;
using Microsoft.Practices.ServiceLocation;
using Owin;

namespace LittleLambs.CRM.API
{
	public static class AutofacConfig
	{
		public static void UseAutofac(this IAppBuilder app, HttpConfiguration config)
		{
			var builder = new ContainerBuilder();
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			builder.RegisterAssemblyModules(assemblies);
			builder.RegisterAssemblyModules(typeof (CrmModule).Assembly);

			var lazyContainer = new Lazy<IContainer>(() => builder.Build());
			var serviceLocatorProvider = new ServiceLocatorProvider(() => new AutofacServiceLocator(lazyContainer.Value));
			builder.RegisterInstance(serviceLocatorProvider);
			
			config.DependencyResolver = new AutofacWebApiDependencyResolver(lazyContainer.Value);
			app.UseWebApi(config);
			app.UseAutofacMiddleware(lazyContainer.Value);
			

		}
	}
}