using System;
using FluentAssertions;
using LittleLambs.CRM.Core.Customers;
using LittleLambs.CRM.Core.Entities;
using Xunit;

namespace LittleLambs.CRM.Tests
{
	public class CustomerTests
	{
		[Fact]
		public void ShouldThrowArgumentNullExceptionOnNullCustomerName()
		{
			Action create = () => new Customer(null);
			create.ShouldThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void CanCreateInstanceWithCustomerName()
		{
			var customer = new Customer("Name");
			customer.Should().BeOfType<Customer>();
		}

		[Fact]
		public void CanSetAndRetrievePhysicalAddress()
		{
			var customer = new Customer("Test Customer");
			var address = new Address(
				"Address 1",
				string.Empty,
				"City 1",
				new State("AL", "Alabama"),
				"36453",
				AddressType.PhysicalAddress
				);
			customer.PhysicalAddress = address;
			customer.PhysicalAddress.Should().Be(address);
		}

		[Fact]
		public void ShouldThrowExceptionWithIncorectAddressType()
		{
			var customer = new Customer("Test Customer");
			var address = new Address(
				"Address 1",
				string.Empty,
				"City 1",
				new State("AL", "Alabama"),
				"36453",
				AddressType.MailingAddress
				);

			Action setAddress = () => customer.PhysicalAddress = address;
			setAddress.ShouldThrowExactly<ArgumentOutOfRangeException>();
		}

		[Fact]
		public void CanSetAndRetrieveMailingAddress()
		{
			var customer = new Customer("Test Customer");
			var address = new Address(
				"Address 1",
				string.Empty,
				"City 1",
				new State("AL", "Alabama"),
				"36453",
				AddressType.MailingAddress
				);
			customer.MailingAddress = address;
			customer.MailingAddress.Should().Be(address);
		}

		[Fact]
		public void CanSetAndRetrieveShippingAddress()
		{
			var customer = new Customer("Test Customer");
			var address = new Address(
				"Address 1",
				string.Empty,
				"City 1",
				new State("AL", "Alabama"),
				"36453",
				AddressType.ShippingAddress
				);
			customer.ShippingAddress = address;
			customer.ShippingAddress.Should().Be(address);
		}
	}
}