using System;
using System.Diagnostics.Contracts;
using LittleLambs.CRM.Core.ExtensionMethods;
using Value;

namespace LittleLambs.CRM.Core.Entities
{
	public sealed class PhoneNumber : ValueObject
	{
		private readonly string _areaCode;
		private readonly string _lineNumber;
		private readonly PhoneNumberType _phoneNumberType;
		private readonly string _prefix;

		public PhoneNumber(string areaCode, string prefix, string lineNumber, PhoneNumberType phoneNumberType)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(areaCode), "The area code cannot be null.");
			Contract.Requires<ArgumentOutOfRangeException>(areaCode.Length == 3, "The area code must be 3 digits");
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(prefix), "The prefix cannot be null.");
			Contract.Requires<ArgumentOutOfRangeException>(prefix.Length == 3, "The prefix must be 3 digits");
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(lineNumber), "The line number cannot be null.");
			Contract.Requires<ArgumentOutOfRangeException>(lineNumber.Length == 4, "The line number must be 4 digits");

			_areaCode = areaCode;
			_prefix = prefix;
			_lineNumber = lineNumber;
			_phoneNumberType = phoneNumberType;
		}

		public PhoneNumberType PhoneNumberType
		{
			get { return _phoneNumberType; }
		}

		public override string ToString()
		{
			return string.Format("({0}) {1}-{2}", _areaCode, _prefix, _lineNumber);
		}

		public static PhoneNumber Parse(string phoneNumber)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(phoneNumber), "The phone number cannot be null");
			Contract.Requires<ArgumentOutOfRangeException>(
				phoneNumber.ExtractDigits().Length == 10 || phoneNumber.ExtractDigits().Length == 11,
				"The phone number must be 10 or 11 digits long.");

			Contract.Ensures(Contract.Result<PhoneNumber>() != null);

			return Parse(phoneNumber, PhoneNumberType.Unknown);
		}

		public static PhoneNumber Parse(string phoneNumber, PhoneNumberType type)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(phoneNumber), "The phone number cannot be null");
			Contract.Requires<ArgumentOutOfRangeException>(
				phoneNumber.ExtractDigits().Length == 10 || phoneNumber.ExtractDigits().Length == 11,
				"The phone number must be 10 or 11 digits long.");

			Contract.Ensures(Contract.Result<PhoneNumber>() != null);

			var strippedPhoneNumber = phoneNumber.ExtractDigits();

			if (strippedPhoneNumber.Length == 11)
			{
				strippedPhoneNumber = strippedPhoneNumber.Substring(1);
			}

			return new PhoneNumber(
				strippedPhoneNumber.Substring(0, 3), // area code
				strippedPhoneNumber.Substring(3, 3), // prefix
				strippedPhoneNumber.Substring(6, 4), // line number
				type);
		}
	}
}