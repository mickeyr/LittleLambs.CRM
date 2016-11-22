using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using LittleLambs.CRM.Core.Entities;

namespace LittleLambs.CRM.Core.Customers
{
	public sealed class Customer : IAggregateRoot<Guid>
	{
		private readonly List<Address> _addressList;
		private readonly List<Contact> _contactList;
		private readonly Guid _id;
		private readonly string _name;
		private PhoneNumber _phoneNumber;

		public Customer(string name)
			: this(Guid.NewGuid(), name, new List<Address>(), new List<Contact>())
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), "Customer name is required.");
		}

		public Customer(string name, List<Address> addressList)
			: this(Guid.NewGuid(), name, addressList, new List<Contact>())
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), "Customer name is required.");
			Contract.Requires<ArgumentNullException>(addressList != null, "Address list cannot be null.");
		}

		public Customer(string name, List<Contact> contactList)
			: this(Guid.NewGuid(), name, new List<Address>(), contactList)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), "Customer name is required.");
			Contract.Requires<ArgumentNullException>(contactList != null, "Contact list cannot be null.");
		}

		public Customer(Guid id, string name, List<Address> addressList, List<Contact> contactList)
		{
			Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(name), "Customer name is required.");
			Contract.Requires<ArgumentNullException>(addressList != null, "Address list cannot be null.");
			Contract.Requires<ArgumentNullException>(contactList != null, "Contact list cannot be null.");

			_id = id;
			_name = name;
			_addressList = addressList;
			_contactList = contactList;
		}

		public string Name
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return _name;
			}
		}

		public string PhoneNumber
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return _phoneNumber.ToString();
			}
			set { _phoneNumber = Customers.PhoneNumber.Parse(value); }
		}

		public IEnumerable<Address> AddressList
		{
			get
			{
				Contract.Ensures(Contract.Result<IEnumerable<Address>>() != null);
				lock (_addressList)
				{
					return _addressList.AsEnumerable();
				}
			}
		}

		public Address PhysicalAddress
		{
			get { return AddressList.SingleOrDefault(a => a.AddressType == AddressType.PhysicalAddress); }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null,
					"Physical address cannot be set to null.  To delete, try calling RemoveAddress(customer.PhysicalAddress)");
				Contract.Requires<ArgumentOutOfRangeException>(
					value.AddressType == AddressType.PhysicalAddress,
					"Cannot set the physical address to an address type other than PhysicalAddress");
				AddAddress(value);
			}
		}

		public Address MailingAddress
		{
			get { return AddressList.SingleOrDefault(a => a.AddressType == AddressType.MailingAddress); }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null,
					"Mailing address cannot be set to null.  To delete, try calling RemoveAddress(customer.MailingAddress)");
				Contract.Requires<ArgumentOutOfRangeException>(
					value.AddressType == AddressType.MailingAddress,
					"Cannot set the mailing address to an address type other than MailingAddress");
				AddAddress(value);
			}
		}

		public Address ShippingAddress
		{
			get { return AddressList.SingleOrDefault(a => a.AddressType == AddressType.ShippingAddress); }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null,
					"Shipping address cannot be set to null.  To delete, try calling RemoveAddress(customer.ShippingAddress)");
				Contract.Requires<ArgumentOutOfRangeException>(
					value.AddressType == AddressType.ShippingAddress,
					"Cannot set the shipping address to an address type other than ShippingAddress");
				AddAddress(value);
			}
		}

		public IEnumerable<Contact> ContactList
		{
			get
			{
				Contract.Ensures(Contract.Result<IEnumerable<Contact>>() != null);
				return _contactList.AsEnumerable();
			}
		}

		public Guid Id
		{
			get { return _id; }
		}

		public bool CanBeSaved
		{
			get { return true; }
		}

		public bool CanBeDeleted
		{
			get { return true; }
		}

		public void AddContact(Contact contact)
		{
			Contract.Requires<ArgumentNullException>(contact != null, "Cannot add a null contact to the contact list.");
			_contactList.Add(contact);
		}

		public void RemoveContact(Contact contact)
		{
			Contract.Requires<ArgumentNullException>(contact != null, "Cannot remove a null contact from the contact list.");
			_contactList.Remove(contact);
		}

		public void RemoveAddress(Address address)
		{
			lock (_addressList)
			{
				_addressList.Remove(address);
			}
		}

		private void AddAddress(Address address)
		{
			lock (_addressList)
			{
				var currentAddressOfType = _addressList.FirstOrDefault(a => a.AddressType == address.AddressType);
				if (currentAddressOfType != null)
				{
					_addressList.Remove(currentAddressOfType);
				}

				_addressList.Add(address);
			}
		}

		[ContractInvariantMethod]
		private void InvariantObjects()
		{
			Contract.Invariant(_contactList != null);
			Contract.Invariant(_addressList != null);
		}
	}
}