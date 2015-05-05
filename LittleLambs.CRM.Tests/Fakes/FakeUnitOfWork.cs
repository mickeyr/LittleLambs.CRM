using LittleLambs.CRM.Core.DataAccess;

namespace LittleLambs.CRM.Tests.Fakes
{
	internal class FakeUnitOfWork : IUnitOfWork
	{
		public void Dispose()
		{
		}

		public void SaveChanges()
		{
		}

		public void RollBack()
		{
		}
	}
}