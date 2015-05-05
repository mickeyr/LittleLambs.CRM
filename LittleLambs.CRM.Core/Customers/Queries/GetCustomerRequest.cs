using System;
using LittleLambs.CRM.Core.Base;

namespace LittleLambs.CRM.Core.Customers.Queries
{
	public class GetCustomerRequest : IRequest<Customer>
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