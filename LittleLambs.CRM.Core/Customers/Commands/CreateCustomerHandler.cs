using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using LittleLambs.CRM.Core.Base;
using LittleLambs.CRM.Core.DataAccess;
using MediatR;

namespace LittleLambs.CRM.Core.Customers.Commands
{
	internal class CreateCustomerHandler : IAsyncRequestHandler<CreateCustomerRequest, Customer>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;

		public CreateCustomerHandler(IUnitOfWorkFactory unitOfWorkFactory, ICustomerRepository customerRepository)
		{
			Contract.Requires<ArgumentNullException>(unitOfWorkFactory != null,
				"Unit of work factory is required for CreateCustomerHandler");
			Contract.Requires<ArgumentNullException>(customerRepository != null,
				"Customer repository is required for CreateCustomerHandler");
			_unitOfWorkFactory = unitOfWorkFactory;
			_customerRepository = customerRepository;
		}

		public Task<Customer> Handle(CreateCustomerRequest query)
		{
			Contract.Ensures(Contract.Result<Customer>() != null);
			return Task.Factory.StartNew(() =>
			{
				using ( var uow = _unitOfWorkFactory.CreateUnitOfWork() )
				{
					Contract.Assume(
						!string.IsNullOrWhiteSpace( query.Name ),
						"The query paramater name is required to be nut null and not empty." );
					var customer = new Customer( query.Name );
					customer = _customerRepository.Upsert( customer );
					uow.SaveChanges();
					return customer;
				}
			});
			
		}

		[ContractInvariantMethod]
		private void InvariantObjects()
		{
			Contract.Invariant(_unitOfWorkFactory != null);
			Contract.Invariant(_customerRepository != null);
		}
	}
}