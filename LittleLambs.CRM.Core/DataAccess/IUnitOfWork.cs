using System;

namespace LittleLambs.CRM.Core.DataAccess
{
	public interface IUnitOfWork : IDisposable
	{
		void SaveChanges();
		void RollBack();
	}
}