using Autofac;
using Autofac.Features.Variance;
using LittleLambs.CRM.Core.Base;

namespace LittleLambs.CRM.DependencyInjection
{
	public class CrmModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterSource(new ContravariantRegistrationSource());
			builder.RegisterAssemblyTypes(typeof (IMediator).Assembly).AsImplementedInterfaces();
			builder.Register<HandlerFactory>(ctx =>
			{
				var c = ctx.Resolve<IComponentContext>();
				return t => c.Resolve(t);
			});
		}
	}
}
