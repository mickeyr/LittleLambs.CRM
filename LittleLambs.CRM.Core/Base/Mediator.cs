using System;
using System.Diagnostics.Contracts;

namespace LittleLambs.CRM.Core.Base
{
	public delegate object HandlerFactory(Type handlerType);

	public class Mediator : IMediator
	{
		private readonly HandlerFactory _handlerFactory;

		public Mediator(HandlerFactory handlerFactory)
		{
			Contract.Requires<ArgumentNullException>(handlerFactory != null, "Handler factory is required by the Mediator class");
			_handlerFactory = handlerFactory;
		}

		public TResponse Request<TResponse>(IRequest<TResponse> request)
		{
			var handler = GetQueryHandler(request);
			var response = handler.Handle(request);
			return response;
		}

		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(_handlerFactory != null);
		}

		private RequestHandler<TResponse> GetQueryHandler<TResponse>(IRequest<TResponse> request)
		{
			Contract.Requires<ArgumentNullException>(request != null, "Request cannot be null in GetQueryHandler");
			Contract.Ensures(Contract.Result<RequestHandler<TResponse>>() != null);
			var handlerType = typeof (IRequestHandler<,>).MakeGenericType(request.GetType(), typeof (TResponse));
			var wrapperType = typeof (RequestHandler<,>).MakeGenericType(request.GetType(), typeof (TResponse));

			object handler;
			try
			{
				handler = _handlerFactory(handlerType);

				if (handler == null)
					throw BuildException(request);
			}
				// ReSharper disable once CatchAllClause
			catch (Exception e)
			{
				throw BuildException(request, e);
			}
			var wrapperHandler = Activator.CreateInstance(wrapperType, handler);
			return (RequestHandler<TResponse>) wrapperHandler;
		}

		private static InvalidOperationException BuildException(object query, Exception innerException = null)
		{
			Contract.Requires<ArgumentNullException>(query != null, "Must provide a query to the BuildException method.");
			Contract.Ensures(Contract.Result<InvalidOperationException>() != null);

			return
				new InvalidOperationException(
					string.Format(
						"Handler was not found for request type {0}.\r\n Container or service locator not configured properly or handlers not registered with your container.",
						query.GetType()), innerException);
		}

		[ContractClass(typeof (RequestHandlerContract<>))]
		private abstract class RequestHandler<TResult>
		{
			public abstract TResult Handle(IRequest<TResult> message);
		}

		[ContractClassFor(typeof (RequestHandler<>))]
		private abstract class RequestHandlerContract<TResult> : RequestHandler<TResult>
		{
			public override TResult Handle(IRequest<TResult> message)
			{
				Contract.Requires<ArgumentNullException>(message != null, "message != null");
				return default(TResult);
			}
		}

		private class RequestHandler<TCommand, TResult> : RequestHandler<TResult> where TCommand : IRequest<TResult>
		{
			private readonly IRequestHandler<TCommand, TResult> _inner;

			public RequestHandler(IRequestHandler<TCommand, TResult> inner)
			{
				Contract.Requires<ArgumentNullException>(inner != null, "The inner request handler cannot be null.");
				_inner = inner;
			}

			public override TResult Handle(IRequest<TResult> message)
			{
				return _inner.Handle((TCommand) message);
			}

			[ContractInvariantMethod]
			private void ObjectInvariant()
			{
				Contract.Invariant(_inner != null);
			}
		}
	}
}