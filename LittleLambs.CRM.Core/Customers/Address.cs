using System;
using System.Diagnostics.Contracts;
using LittleLambs.CRM.Core.Entities;
using Value;

namespace LittleLambs.CRM.Core.Customers
{
	public sealed class Address : ValueObject
	{
		private readonly string _addressLine1;
		private readonly string _addressLine2;
		private readonly AddressType _addressType;
		private readonly string _city;
		private readonly string _postalCode;
		private readonly State _state;

		public Address(
			string addressLine1,
			string addressLine2,
			string city,
			State state,
			string postalCode,
			AddressType addressType)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(addressLine1), "Address line 1 is required");
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(city), "City is required");
			Contract.Requires<ArgumentNullException>(state != null, "State cannot be null");
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(postalCode), "Postal code is required");

			_addressLine1 = addressLine1;
			_addressLine2 = addressLine2;
			_city = city;
			_state = state;
			_postalCode = postalCode;
			_addressType = addressType;
		}

		public string AddressLine1
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return _addressLine1;
			}
		}

		public string AddressLine2
		{
			get { return _addressLine2; }
		}

		public string City
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return _city;
			}
		}

		public State State
		{
			get
			{
				Contract.Ensures(Contract.Result<State>() != null);
				return _state;
			}
		}

		public string PostalCode
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return _postalCode;
			}
		}

		public AddressType AddressType
		{
			get { return _addressType; }
		}

		[ContractInvariantMethod]
		private void InvariantObjects()
		{
			Contract.Invariant(_addressLine1 != null);
			Contract.Invariant(_city != null);
			Contract.Invariant(_state != null);
			Contract.Invariant(_postalCode != null);
		}
	}
}