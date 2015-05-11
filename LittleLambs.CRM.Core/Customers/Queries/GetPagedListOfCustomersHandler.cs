using System.Threading.Tasks;
using LittleLambs.CRM.Core.Base;
using MediatR;

namespace LittleLambs.CRM.Core.Customers.Queries
{
	internal class GetPagedListOfCustomersHandler : IAsyncRequestHandler<GetPagedListOfCustomersRequest, IPagedList<Customer>>
	{
		private readonly ICustomerRepository _repository;

		public GetPagedListOfCustomersHandler(ICustomerRepository repository)
		{
			_repository = repository;
		}

		public Task<IPagedList<Customer>> Handle(GetPagedListOfCustomersRequest request)
		{
			return _repository.GetPagedListAsync(request.PageNumber, request.PageSize);
		}
	}
}