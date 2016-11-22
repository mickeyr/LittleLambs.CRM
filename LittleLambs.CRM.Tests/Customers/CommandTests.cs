using System;
using System.Linq;
using Autofac;
using FluentAssertions;
using LittleLambs.CRM.Core.Customers;
using LittleLambs.CRM.Core.Customers.Commands;
using LittleLambs.CRM.Core.Customers.Queries;
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
		public async void CanCreateCustomerWithNameAsync()
		{
			var command = new CreateCustomerRequest("name");
			var c = await _mediator.SendAsync(command);
			var found = _repository.Get(c.Id);
			found.Should().Be(c);
		}

		[Fact]
		public async void CanRetrievePagedListOfCustomersAsync()
		{
			for (int i = 0; i < 20; i++)
			{
				_repository.Upsert(new Customer(String.Format("Name {0}", i)));
			}

			var command = new GetPagedListOfCustomersRequest(1, 10);
			var result = await _mediator.SendAsync( command );
			result.TotalItems.Should().Be(20);
			result.TotalPages.Should().Be(2);
			result.Items.First().Name.Should().Be("Name 0");
			result.Items.Last().Name.Should().Be("Name 9");
			result.HasNextPage.Should().Be(true);
			result.HasPreviousPage.Should().Be(false);
		}

		[Fact]
		public async void CanRetrieveCustomerByIdAsync()
		{
			_repository.Upsert(new Customer("Customer"));
			var expected = _repository.GetAll().First();

			var command = new GetCustomerRequest( expected.Id );
			var result = await _mediator.SendAsync(command);
			result.Should().Be(expected);
		}
	}
}