using System;
using System.Diagnostics.Contracts;

namespace LittleLambs.CRM.Core.Base
{
	[ContractClass(typeof (MediatorContract))]
	public interface IMediator
	{
		TResponse Request<TResponse>(IRequest<TResponse> request);
	}

	[ContractClassFor(typeof (IMediator))]
	internal abstract class MediatorContract : IMediator
	{
		public TResponse Request<TResponse>(IRequest<TResponse> request)
		{
			Contract.Requires<ArgumentNullException>(request != null, "Request is required to be not null by the mediator class.");
			return default(TResponse);
		}
	}
}