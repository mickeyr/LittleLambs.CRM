using System.Collections.Generic;

namespace LittleLambs.CRM.Core.Base
{
	public interface IPagedList<out T>
	{
		int PageSize { get; }
		int CurrentPage { get; }
		int TotalItems { get; }
		int TotalPages { get; }
		bool HasNextPage { get; }
		bool HasPreviousPage { get; }
		IEnumerable<T> Items { get; }
	}
}