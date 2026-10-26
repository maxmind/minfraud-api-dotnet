#region

using MaxMind.MinFraud.Request;
using System;
using System.Text.Json;
using Xunit;

#endregion

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class ShippingTest
    {
        // Same as code in BillingTest
        [Fact]
        public void TestFirstName()
        {
            var loc = new Shipping
            {
                FirstName = "frst"
            };
            Assert.Equal("frst", loc.FirstName);
        }

        [Fact]
        public void TestLastName()
        {
            var loc = new Shipping { LastName = "last" };
            Assert.Equal("last", loc.LastName);
        }

        [Fact]
        public void TestCompany()
        {
            var loc = new Shipping { Company = "company" };
            Assert.Equal("company", loc.Company);
        }

        [Fact]
        public void TestAddress()
        {
            var loc = new Shipping { Address = "addr" };
            Assert.Equal("addr", loc.Address);
        }

        [Fact]
        public void TestAddress2()
        {
            var loc = new Shipping { Address2 = "addr2" };
            Assert.Equal("addr2", loc.Address2);
        }

        [Fact]
        public void TestCity()
        {
            var loc = new Shipping { City = "Pdx" };
            Assert.Equal("Pdx", loc.City);
        }

        [Fact]
        public void TestRegion()
        {
            var loc = new Shipping { Region = "MN" };
            Assert.Equal("MN", loc.Region);
        }

        [Fact]
        public void TestCountry()
        {
            var loc = new Shipping { Country = "US" };
            Assert.Equal("US", loc.Country);
        }

        [Fact]
        public void TestCountryThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new Shipping { Country = "USA" });
        }

        [Fact]
        public void TestCountryWithNumbers()
        {
            Assert.Throws<ArgumentException>(() => new Shipping { Country = "U1" });
        }

        [Fact]
        public void TestCountryInWrongCase()
        {
            Assert.Throws<ArgumentException>(() => new Shipping { Country = "us" });
        }

        [Fact]
        public void TestPostal()
        {
            var loc = new Shipping { Postal = "03231" };
            Assert.Equal("03231", loc.Postal);
        }

        [Fact]
        public void TestPhoneNumber()
        {
            var phone = "321-321-3213";
            var loc = new Shipping { PhoneNumber = phone };
            Assert.Equal(phone, loc.PhoneNumber);
        }

        [Fact]
        public void TestPhoneCountryCode()
        {
            var loc = new Shipping { PhoneCountryCode = "1" };
            Assert.Equal("1", loc.PhoneCountryCode);
        }

        [Fact]
        public void TestPhoneVerificationMethod()
        {
            var loc = new Shipping { PhoneVerificationMethod = LocationPhoneVerificationMethod.Network };
            Assert.Equal(LocationPhoneVerificationMethod.Network, loc.PhoneVerificationMethod);
        }

        [Fact]
        public void TestPhoneWasVerificationSuccessful()
        {
            var loc = new Shipping { PhoneWasVerificationSuccessful = false };
            Assert.False(loc.PhoneWasVerificationSuccessful);
        }

        [Fact]
        public void TestPhoneVerificationTime()
        {
            var time = new DateTimeOffset(2026, 10, 1, 14, 30, 0, new TimeSpan(0));
            var loc = new Shipping { PhoneVerificationTime = time };
            Assert.Equal(time, loc.PhoneVerificationTime);
        }

        [Theory]
        [InlineData(LocationPhoneVerificationMethod.DeliveredCode, "delivered_code")]
        [InlineData(LocationPhoneVerificationMethod.Network, "network")]
        [InlineData(LocationPhoneVerificationMethod.Other, "other")]
        public void TestPhoneVerificationSerialization(LocationPhoneVerificationMethod method, string expected)
        {
            var loc = new Shipping
            {
                PhoneVerificationMethod = method,
                PhoneWasVerificationSuccessful = false,
                PhoneVerificationTime = new DateTimeOffset(2026, 10, 1, 16, 30, 0, new TimeSpan(2, 0, 0))
            };

            var json = JsonSerializer.Serialize(loc, TestHelper.SerializerOptions);
            var comparer = new JsonElementComparer();
            Assert.True(
                comparer.JsonEquals(
                    JsonDocument.Parse(
                        $$"""
                        {
                            "phone_verification_method": "{{expected}}",
                            "phone_was_verification_successful": false,
                            "phone_verification_time": "2026-10-01T16:30:00+02:00"
                        }
                        """),
                    JsonDocument.Parse(json)
                ),
                json
            );
        }

        [Fact]
        public void TestPhoneVerificationUnsetIsNotSerialized()
        {
            var json = JsonSerializer.Serialize(new Shipping { FirstName = "f" }, TestHelper.SerializerOptions);
            Assert.DoesNotContain("phone_verification_method", json);
            Assert.DoesNotContain("phone_was_verification_successful", json);
            Assert.DoesNotContain("phone_verification_time", json);
        }

        // End shared code

        [Fact]
        public void TestDeliverySpeed()
        {
            var loc = new Shipping { DeliverySpeed = ShippingDeliverySpeed.Expedited };
            Assert.Equal(ShippingDeliverySpeed.Expedited, loc.DeliverySpeed);
        }
    }
}
