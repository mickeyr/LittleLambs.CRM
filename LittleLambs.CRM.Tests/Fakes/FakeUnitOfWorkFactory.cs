using LittleLambs.CRM.Core.DataAccess;

namespace LittleLambs.CRM.Tests.Fakes
{
	internal class FakeUnitOfWorkFactory : IUnitOfWorkFactory
	{
		public IUnitOfWork CreateUnitOfWork()
		{
			return new FakeUnitOfWork();
		}
	}
}