using System;
using System.Diagnostics.Contracts;
using LittleLambs.CRM.Core.Entities;
using Value;

namespace LittleLambs.CRM.Core.Customers
{
	public sealed class Contact : ValueObject
	{
		private readonly string _emailAddress;
		private readonly string _firstName;
		private readonly string _lastName;
		private readonly PhoneNumber _phoneNumber;
		private readonly string _position;

		public Contact(string firstName, string lastName, string position, PhoneNumber phoneNumber, string emailAddress)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(firstName), "First name cannot be null");
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(lastName), "Last name cannot be null");
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(position), "Position cannot be null");
			Contract.Requires<ArgumentNullException>(
				phoneNumber != null || !string.IsNullOrWhiteSpace(emailAddress),
				"A phone number or email address is required.");

			_firstName = firstName;
			_lastName = lastName;
			_position = position;
			_phoneNumber = phoneNumber;
			_emailAddress = emailAddress;
		}

		public string FirstName
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return _firstName;
			}
		}

		public string LastName
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return _lastName;
			}
		}

		public string FullName
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return string.Format("{0} {1}", _firstName, _lastName);
			}
		}

		public string Position
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return _position;
			}
		}

		public PhoneNumber PhoneNumber
		{
			get { return _phoneNumber; }
		}

		public string EmailAddress
		{
			get { return _emailAddress; }
		}

		[ContractInvariantMethod]
		private void InvariantObjects()
		{
			Contract.Invariant(_firstName != null);
			Contract.Invariant(_lastName != null);
			Contract.Invariant(_position != null);
			Contract.Invariant(_firstName != null);
		}
	}
}