using LittleLambs.CRM.Core.Base;

namespace LittleLambs.CRM.Core.Customers.Queries
{
	public class GetPagedListOfCustomersRequest : IRequest<IPagedList<Customer>>
	{
		public GetPagedListOfCustomersRequest(int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
		}

		public int PageNumber { get; private set; }
		public int PageSize { get; private set; }
	}
}