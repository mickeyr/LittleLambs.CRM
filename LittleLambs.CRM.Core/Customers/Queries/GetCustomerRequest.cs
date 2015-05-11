using System;
using MediatR;

namespace LittleLambs.CRM.Core.Customers.Queries
{
	public class GetCustomerRequest : IAsyncRequest<Customer>
	{
		private readonly Guid _id;

		public GetCustomerRequest(Guid id)
		{
			_id = id;
		}

		public Guid Id
		{
			get { return _id; }
		}
	}
}