using System.Diagnostics.Contracts;

namespace LittleLambs.CRM.Core.DataAccess
{
	[ContractClass(typeof (UnitOfWorkFactoryContract))]
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork CreateUnitOfWork();
	}

	[ContractClassFor(typeof (IUnitOfWorkFactory))]
	internal abstract class UnitOfWorkFactoryContract : IUnitOfWorkFactory
	{
		public IUnitOfWork CreateUnitOfWork()
		{
			Contract.Ensures(Contract.Result<IUnitOfWork>() != null);
			return default(IUnitOfWork);
		}
	}
}