﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using LittleLambs.CRM.Core.Entities;

namespace LittleLambs.CRM.Core.DataAccess
{
	[ContractClass(typeof (RepositoryContract<>))]
	public interface IRepository<T> where T : IAggregateRoot<Guid>
	{
		T Get(Guid id);
		IEnumerable<T> GetAll();
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
	}
}