using System;
using System.Diagnostics.Contracts;
using LittleLambs.CRM.Core.Base;

namespace LittleLambs.CRM.Core.Customers.Queries
{
	internal class GetCustomerHandler : IRequestHandler<GetCustomerRequest, Customer>
	{
		private readonly ICustomerRepository _customerRepository;

		public GetCustomerHandler(ICustomerRepository customerRepository)
		{
			Contract.Requires<ArgumentNullException>(customerRepository != null,
				"Customer repository is required for the show customer handler.");
			_customerRepository = customerRepository;
		}

		public Customer Handle(GetCustomerRequest query)
		{
			Contract.Ensures(Contract.Result<Customer>() != null);
			return _customerRepository.Get(query.Id);
		}

		[ContractInvariantMethod]
		private void InvariantObjects()
		{
			Contract.Invariant(_customerRepository != null);
		}
	}
}