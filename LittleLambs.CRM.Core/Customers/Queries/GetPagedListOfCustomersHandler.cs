using LittleLambs.CRM.Core.Base;

namespace LittleLambs.CRM.Core.Customers.Queries
{
	internal class GetPagedListOfCustomersHandler : IRequestHandler<GetPagedListOfCustomersRequest, IPagedList<Customer>>
	{
		private readonly ICustomerRepository _repository;

		public GetPagedListOfCustomersHandler(ICustomerRepository repository)
		{
			_repository = repository;
		}

		public IPagedList<Customer> Handle(GetPagedListOfCustomersRequest request)
		{
			return _repository.GetPagedList(request.PageSize, request.PageNumber);
		}
	}
}