using System.Threading.Tasks;
using System.Web.Http;
using LittleLambs.CRM.Core.Customers.Queries;
using MediatR;

namespace LittleLambs.CRM.API.Controllers
{
	public class CustomerController : ApiController
	{
		private readonly IMediator _mediator;

		public CustomerController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IHttpActionResult> GetContacts(GetPagedListOfCustomersRequest request)
		{
			var result = await _mediator.SendAsync(request);
			return Ok(result);
		}
	}
}