using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using LittleLambs.CRM.Core.Base;
using LittleLambs.CRM.Core.Entities;

namespace LittleLambs.CRM.Core.DataAccess
{
	[ContractClass(typeof (RepositoryContract<>))]
	public interface IRepository<T> where T : IAggregateRoot<Guid>
	{
		T Get(Guid id);
		Task<T> GetAsync(Guid id);
		IEnumerable<T> GetAll();
		IPagedList<T> GetPagedList(int page, int pageSize);
		Task<IPagedList<T>> GetPagedListAsync(int page, int pageSize);
		void Delete(Guid id);
		T Upsert(T entity);
	}

	[ContractClassFor(typeof (IRepository<>))]
	internal abstract class RepositoryContract<T> : IRepository<T> where T : IAggregateRoot<Guid>
	{
		public T Get(Guid id)
		{
			Contract.Requires<ArgumentNullException>(id != null, "Id cannot be null on Get calls from repositories.");
			Contract.Ensures(Contract.Result<T>() != null);
			return default(T);
		}

		public IEnumerable<T> GetAll()
		{
			Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
			return default(IEnumerable<T>);
		}

		public IPagedList<T> GetPagedList(int page, int pageSize)
		{
			Contract.Requires<ArgumentOutOfRangeException>(page >= 1, "page >= 1");
			Contract.Requires<ArgumentOutOfRangeException>(pageSize >= 1, "pageSize >= 1");
			Contract.Ensures(Contract.Result<IPagedList<T>>() != null);
			return default(IPagedList<T>);
		}

		public Task<IPagedList<T>> GetPagedListAsync(int page, int pageSize)
		{
			Contract.Requires<ArgumentOutOfRangeException>(page >= 1, "page >= 1");
			Contract.Requires<ArgumentOutOfRangeException>(pageSize >= 1, "pageSize >= 1");
			Contract.Ensures(Contract.Result<IPagedList<T>>() != null);
			return default(Task<IPagedList<T>>);
		}

		public void Delete(Guid id)
		{
			Contract.Requires<ArgumentNullException>(id != null, "Id cannot be null on Delete calls from repositories.");
		}

		public T Upsert(T entity)
		{
			Contract.Requires<ArgumentNullException>(entity != null, "Entity cannot be null on Upsert calls from repositories.");
			Contract.Ensures(Contract.Result<T>() != null);
			return default(T);
		}

		public Task<T> GetAsync(Guid id)
		{
			Contract.Requires<ArgumentNullException>(id != null, "Id cannot be null on Get calls from repositories.");
			Contract.Ensures(Contract.Result<T>() != null);
			return default(Task<T>);
		}
	}
}