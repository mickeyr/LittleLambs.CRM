using Autofac;
using LittleLambs.CRM.Tests.Fakes;

namespace LittleLambs.CRM.Tests
{
	public class BaseTest
	{
		protected readonly IContainer _container;
		public BaseTest()
		{
			var builder = IoC.Bootstrap();
			builder.RegisterType<FakeUnitOfWorkFactory>().AsImplementedInterfaces().InstancePerLifetimeScope();
			builder.RegisterType<FakeUnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
			builder.RegisterType<FakeCustomerRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
			_container = builder.Build();
		}
	}
}
