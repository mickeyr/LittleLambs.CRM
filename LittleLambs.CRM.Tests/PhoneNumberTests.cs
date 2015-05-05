using System;
using FluentAssertions;
using LittleLambs.CRM.Core.Entities;
using Xunit;

namespace LittleLambs.CRM.Tests
{
	public class PhoneNumberTests
	{
		[Fact]
		public void CanParseProperlyFormattedPhoneNumber()
		{
			var phoneNumberString = "(555) 555-2424";
			var phoneNumber = PhoneNumber.Parse(phoneNumberString);
			phoneNumber.Should().BeOfType<PhoneNumber>();
			phoneNumber.ToString().Should().Be(phoneNumberString);
		}
		
		[Fact]
		public void CanParseProperlyFormattedPhoneNumberWithCountryCode()
		{
			var phoneNumberString = "1 (555) 555-2424";
			var formattedPhoneNumberString = "(555) 555-2424";
			var phoneNumber = PhoneNumber.Parse( phoneNumberString );
			phoneNumber.Should().BeOfType<PhoneNumber>();
			phoneNumber.ToString().Should().Be( formattedPhoneNumberString );
		}

		[Fact]
		public void CanParsePhoneNumberWithDashFormatting()
		{
			var phoneNumberString = "1-555-555-2424";
			var formattedPhoneNumberString = "(555) 555-2424";
			var phoneNumber = PhoneNumber.Parse( phoneNumberString );
			phoneNumber.Should().BeOfType<PhoneNumber>();
			phoneNumber.ToString().Should().Be( formattedPhoneNumberString );
		}

		[Fact]
		public void CanParseUnformattedPhoneNumber()
		{
			var phoneNumberString = "15555552424";
			var formattedPhoneNumberString = "(555) 555-2424";
			var phoneNumber = PhoneNumber.Parse( phoneNumberString );
			phoneNumber.Should().BeOfType<PhoneNumber>();
			phoneNumber.ToString().Should().Be( formattedPhoneNumberString );
		}

		[Fact]
		public void ShouldThrowExceptionWhenParsingInvalidPhoneNumber()
		{
			var phoneNumberString = "(555) 555-242";

			Action parse = () => PhoneNumber.Parse(phoneNumberString);

			parse.ShouldThrow<ArgumentOutOfRangeException>();
		}
	}
}
