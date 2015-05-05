using Autofac;
using FluentAssertions;
using LittleLambs.CRM.Core.Base;
using LittleLambs.CRM.Core.Customers;
using LittleLambs.CRM.Core.Customers.Commands;
using LittleLambs.CRM.Tests.Fakes;
using Xunit;

namespace LittleLambs.CRM.Tests.Customers
{
	public class CommandTests : BaseTest
	{
		private readonly ICustomerRepository _repository;

		public CommandTests()
		{
			_repository = _container.Resolve<ICustomerRepository>();
		}

		[Fact]
		public void CanCreateCustomerWithName()
		{
			var command = new CreateCustomerRequest("name");
			var mediator = _container.Resolve<IMediator>();
			var c = mediator.Request(command);
			var found = _repository.Get(c.Id);
			found.Should().Be(c);
		}
	}
}