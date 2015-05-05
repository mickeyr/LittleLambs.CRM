using System;
using System.Diagnostics.Contracts;

namespace LittleLambs.CRM.Core.Base
{
	[ContractClass(typeof (RequestHandlerContract<,>))]
	internal interface IRequestHandler<in TRequest, out TResponse>
		where TRequest : IRequest<TResponse>
	{
		TResponse Handle(TRequest request);
	}

	[ContractClassFor(typeof (IRequestHandler<,>))]
	internal abstract class RequestHandlerContract<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		public TResponse Handle(TRequest request)
		{
			Contract.Requires<ArgumentNullException>(request != null, "Request cannot be null for RequestHandler.Handle");
			return default(TResponse);
		}
	}
}