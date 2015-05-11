using System;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using LittleLambs.CRM.Tests.Fakes;
using MediatR;
using Microsoft.Practices.ServiceLocation;

namespace LittleLambs.CRM.Tests
{
	public class BaseTest
	{
		protected readonly IContainer _container;
		protected readonly IMediator _mediator;
		public BaseTest()
		{
			var builder = IoC.Bootstrap();
			builder.RegisterType<FakeUnitOfWorkFactory>().AsImplementedInterfaces().InstancePerLifetimeScope();
			builder.RegisterType<FakeUnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
			builder.RegisterType<FakeCustomerRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
			var lazyContainer = new Lazy<IContainer>( () => builder.Build() );
			var serviceLocatorProvider = new ServiceLocatorProvider( () => new AutofacServiceLocator( lazyContainer.Value ) );
			builder.RegisterInstance( serviceLocatorProvider );
			_container = lazyContainer.Value;

			_mediator = _container.Resolve<IMediator>();

		}
	}
}
