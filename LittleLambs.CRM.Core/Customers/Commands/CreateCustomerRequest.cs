using System;
using System.Diagnostics.Contracts;
using LittleLambs.CRM.Core.Base;

namespace LittleLambs.CRM.Core.Customers.Commands
{
	public class CreateCustomerRequest : IRequest<Customer>
	{
		private readonly string _name;

		public CreateCustomerRequest(string name)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), "Customer name cannot be null.");
			_name = name;
		}

		public string Name
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return _name;
			}
		}

		[ContractInvariantMethod]
		private void InvariantObjects()
		{
			Contract.Invariant(_name != null);
		}
	}
}