using System;
using System.Collections.Generic;
using System.Linq;
using LittleLambs.CRM.Core.Customers;

namespace LittleLambs.CRM.Tests.Fakes
{
	internal class FakeCustomerRepository : ICustomerRepository
	{
		private readonly List<Customer> _customers;

		public FakeCustomerRepository()
		{
			_customers = new List<Customer>();
		}

		public Customer Get(Guid id)
		{
			return _customers.Single(c => c.Id == id);
		}

		public IEnumerable<Customer> GetAll()
		{
			return _customers;
		}

		public void Delete(Guid id)
		{
			_customers.Remove(_customers.Single(c => c.Id == id));
		}

		public Customer Upsert(Customer entity)
		{
			if (_customers.Any(c => c.Id == entity.Id))
			{
				Delete(entity.Id);
			}

			_customers.Add(entity);
			return entity;
		}
	}
}