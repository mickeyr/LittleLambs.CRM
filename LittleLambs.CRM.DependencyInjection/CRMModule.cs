using Autofac;
using Autofac.Features.Variance;
using LittleLambs.CRM.Core.DataAccess;
using MediatR;

namespace LittleLambs.CRM.DependencyInjection
{
	public class CrmModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterSource(new ContravariantRegistrationSource());
			builder.RegisterAssemblyTypes(typeof (IMediator).Assembly).AsImplementedInterfaces();
			builder.RegisterAssemblyTypes(typeof (IRepository<>).Assembly).AsImplementedInterfaces();
		}
	}
}