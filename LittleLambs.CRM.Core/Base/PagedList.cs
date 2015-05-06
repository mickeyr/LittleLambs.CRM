using System;
using System.Collections.Generic;

namespace LittleLambs.CRM.Core.Base
{
	public class PagedList<T> : IPagedList<T>
	{
		public PagedList(IEnumerable<T> items, int currentPage, int pageSize, int totalItems)
		{
			CurrentPage = currentPage;
			PageSize = pageSize;
			TotalItems = totalItems;
			Items = items;
		}

		public int PageSize { get; private set; }
		public int CurrentPage { get; private set; }
		public int TotalItems { get; private set; }

		public int TotalPages
		{
			get { return (int) Math.Ceiling(TotalItems/(double) PageSize); }
		}

		public bool HasNextPage
		{
			get { return CurrentPage < TotalPages; }
		}

		public bool HasPreviousPage
		{
			get { return CurrentPage > 1; }
		}

		public IEnumerable<T> Items { get; private set; }
	}
}