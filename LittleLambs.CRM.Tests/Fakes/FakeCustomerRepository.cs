using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleLambs.CRM.Core.Base;
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

		public Task<Customer> GetAsync(Guid id)
		{
			return Task.Factory.StartNew(() => Get(id));
		}

		public IEnumerable<Customer> GetAll()
		{
			return _customers;
		}

		public IPagedList<Customer> GetPagedList(int page, int pageSize)
		{
			var items = _customers
				.Skip((page - 1)*pageSize)
				.Take(pageSize)
				.AsEnumerable();
			return new PagedList<Customer>(items, page, pageSize, _customers.Count);
		}

		public Task<IPagedList<Customer>> GetPagedListAsync(int page, int pageSize)
		{
			return Task.Factory.StartNew(() => GetPagedList(page, pageSize));
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