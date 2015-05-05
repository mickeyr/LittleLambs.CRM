using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace LittleLambs.CRM.Core.ExtensionMethods
{
	public static class StringExtensions
	{
		[Pure]
		public static string ExtractDigits(this string lhs)
		{
			Contract.Requires<ArgumentNullException>(lhs != null, "string cannot be null in ExtractDigits extension method.");
			Contract.Ensures(Contract.Result<string>() != null);
			return new string(lhs.Where(char.IsDigit).ToArray());
		}
	}
}