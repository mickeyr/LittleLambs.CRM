using System.Collections.Generic;
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
			builder.Register<SingleInstanceFactory>( ctx =>
			{
				var c = ctx.Resolve<IComponentContext>();
				return t => c.Resolve( t );
			} );
			builder.Register<MultiInstanceFactory>( ctx =>
			{
				var c = ctx.Resolve<IComponentContext>();
				return t => (IEnumerable<object>)c.Resolve( typeof( IEnumerable<> ).MakeGenericType( t ) );
			} );
		}
	}
}