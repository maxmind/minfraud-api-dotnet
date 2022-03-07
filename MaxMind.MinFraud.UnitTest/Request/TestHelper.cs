using MaxMind.MinFraud.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Xunit.Abstractions;

namespace MaxMind.MinFraud.UnitTest.Request
{
    internal static class TestHelper
    {
        public static string TestDirectory { get; } = GetTestDirectory();

        private static string GetTestDirectory()
        {
            // check if environment variable MAXMIND_TEST_BASE_DIR is set
            var dbPath = Environment.GetEnvironmentVariable("MAXMIND_TEST_BASE_DIR");

            if (!string.IsNullOrEmpty(dbPath))
            {
                if (!Directory.Exists(dbPath))
                {
                    throw new System.Exception(
                        "Path set as environment variable MAXMIND_TEST_BASE_DIR does not exist!");
                }

                return dbPath;
            }

            // In Microsoft.NET.Test.Sdk v15.0.0, the current working directory
            // is not set to project's root but instead the output directory.
            // see: https://github.com/Microsoft/vstest/issues/435.
            //
            // Let's change the strategry of finding the parent directory of
            // TestData directory by walking from cwd backwards upto the
            // volume's root.
            var currentDirectory = Directory.GetCurrentDirectory();
            var currentDirectoryInfo = new DirectoryInfo(currentDirectory);

            do
            {
                if (currentDirectoryInfo.EnumerateDirectories("TestData*", SearchOption.AllDirectories).Any())
                {
                    return currentDirectoryInfo.FullName;
                }
                currentDirectoryInfo = currentDirectoryInfo.Parent;
            } while (currentDirectoryInfo?.Parent != null);

            return currentDirectory;
        }

        public static Transaction CreateFullRequestUsingConstructors()
        {
            return new Transaction(
                device: new Device(IPAddress.Parse("152.216.7.110"),
                    userAgent:
                    "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.89 Safari/537.36",
                    acceptLanguage: "en-US,en;q=0.8",
                    sessionAge: 3600.5,
                    sessionId: "foobar"
                ),
                account:
                new Account(
                    userId: "3132",
                    username: "fred"
                ),
                billing:
                new Billing(
                    firstName: "First",
                    lastName: "Last",
                    company: "Company",
                    address: "101 Address Rd.",
                    address2: "Unit 5",
                    city: "City of Thorns",
                    region: "CT",
                    country: "US",
                    postal: "06510",
                    phoneNumber: "123-456-7890",
                    phoneCountryCode: "1"
                ),
                creditCard:
                new CreditCard(
                    country: "US",
                    issuerIdNumber: "411111",
                    bankName: "Bank of No Hope",
                    bankPhoneCountryCode: "1",
                    bankPhoneNumber: "123-456-1234",
                    avsResult: 'Y',
                    cvvResult: 'N',
                    lastDigits: "7643",
                    token: "123456abc1234",
                    was3DSecureSuccessful: false
                ),
                customInputs: new CustomInputs.Builder
                {
                    { "float_input", 12.1d},
                    { "integer_input", 3123},
                    { "string_input", "This is a string input."},
                    { "boolean_input", true},
                }.Build(),
                email:
                new Email(
                    address: "test@maxmind.com",
                    domain: "maxmind.com",
                    hashAddress: false
                ),
                userEvent:
                new Event
                (
                    transactionId: "txn3134133",
                    shopId: "s2123",
                    time: new DateTimeOffset(2014, 4, 12, 23, 20, 50, 52, new TimeSpan(0)),
                    type: EventType.Purchase
                ),
                order:
                new Order(
                    amount: 323.21m,
                    currency: "USD",
                    discountCode: "FIRST",
                    affiliateId: "af12",
                    subaffiliateId: "saf42",
                    referrerUri: new Uri("http://www.amazon.com/"),
                    isGift: true,
                    hasGiftMessage: false
                ),
                payment:
                new Payment(
                    processor: PaymentProcessor.Stripe,
                    wasAuthorized: false,
                    declineCode: "invalid number"
                ),
                shipping:
                new Shipping(
                    firstName: "ShipFirst",
                    lastName: "ShipLast",
                    company: "ShipCo",
                    address: "322 Ship Addr. Ln.",
                    address2: "St. 43",
                    city: "Nowhere",
                    region: "OK",
                    country: "US",
                    postal: "73003",
                    phoneNumber: "123-456-0000",
                    phoneCountryCode: "1",
                    deliverySpeed: ShippingDeliverySpeed.SameDay
                ),
                shoppingCart: new List<ShoppingCartItem>
                {
                    new ShoppingCartItem(
                        category: "pets",
                        itemId: "ad23232",
                        quantity: 2,
                        price: 20.43m
                    ),
                    new ShoppingCartItem(
                        category: "beauty",
                        itemId: "bst112",
                        quantity: 1,
                        price: 100.00m
                    )
                }
            );
        }

        public static Transaction CreateFullRequest()
        {
            return new Transaction
            {
                Device = new Device
                {
                    IPAddress = IPAddress.Parse("152.216.7.110"),
                    UserAgent =
                        "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.89 Safari/537.36",
                    AcceptLanguage = "en-US,en;q=0.8",
                    SessionAge = 3600.5,
                    SessionId = "foobar"
                },
                Event = new Event
                {
                    TransactionId = "txn3134133",
                    ShopId = "s2123",
                    Time = new DateTimeOffset(2014, 4, 12, 23, 20, 50, 52, new TimeSpan(0)),
                    Type = EventType.Purchase
                },
                Account = new Account
                {
                    UserId = "3132",
                    Username = "fred"
                },
                Email = new Email
                {
                    Address = "test@maxmind.com",
                    Domain = "maxmind.com",
                    HashAddress = false
                },
                Billing = new Billing
                {
                    FirstName = "First",
                    LastName = "Last",
                    Company = "Company",
                    Address = "101 Address Rd.",
                    Address2 = "Unit 5",
                    City = "City of Thorns",
                    Region = "CT",
                    Country = "US",
                    Postal = "06510",
                    PhoneNumber = "123-456-7890",
                    PhoneCountryCode = "1"
                },
                Shipping = new Shipping
                {
                    FirstName = "ShipFirst",
                    LastName = "ShipLast",
                    Company = "ShipCo",
                    Address = "322 Ship Addr. Ln.",
                    Address2 = "St. 43",
                    City = "Nowhere",
                    Region = "OK",
                    Country = "US",
                    Postal = "73003",
                    PhoneNumber = "123-456-0000",
                    PhoneCountryCode = "1",
                    DeliverySpeed = ShippingDeliverySpeed.SameDay
                },
                Payment = new Payment
                {
                    Processor = PaymentProcessor.Stripe,
                    WasAuthorized = false,
                    DeclineCode = "invalid number"
                },
                CreditCard = new CreditCard
                {
                    Country = "US",
                    IssuerIdNumber = "411111",
                    BankName = "Bank of No Hope",
                    BankPhoneCountryCode = "1",
                    BankPhoneNumber = "123-456-1234",
                    AvsResult = 'Y',
                    CvvResult = 'N',
                    LastDigits = "7643",
                    Token = "123456abc1234",
                    Was3DSecureSuccessful = false,
                },
                CustomInputs = new CustomInputs.Builder
                {
                    { "float_input", 12.1d},
                    { "integer_input", 3123},
                    { "string_input", "This is a string input."},
                    { "boolean_input", true},
                }.Build(),
                Order = new Order
                {
                    Amount = 323.21m,
                    Currency = "USD",
                    DiscountCode = "FIRST",
                    AffiliateId = "af12",
                    SubaffiliateId = "saf42",
                    ReferrerUri = new Uri("http://www.amazon.com/"),
                    IsGift = true,
                    HasGiftMessage = false
                },
                ShoppingCart = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem
                    {
                        Category = "pets",
                        ItemId = "ad23232",
                        Quantity = 2,
                        Price = 20.43m
                    },
                    new ShoppingCartItem
                    {
                        Category = "beauty",
                        ItemId = "bst112",
                        Quantity = 1,
                        Price = 100.00m
                    }
                }
            };
        }

        public static string ReadJsonFile(string name)
        {
            return
                File.ReadAllText(Path.Combine(TestDirectory, "TestData",
                    $"{name}.json"));
        }

        public static bool VerifyRequestFor(string service, HttpRequestMessage message, ITestOutputHelper output)
        {
            var requestFile = service == "transactions/report" ? "report-request" : "full-request";
            var requestBody = ReadJsonFile(requestFile);

            if (message.Content?.Headers.ContentType?.ToString() != "application/json; charset=utf-8")
            {
                output.WriteLine("Unexpected content type: " + message.Content?.Headers.ContentType);
                return false;
            }
            var contentTask = message.Content.ReadAsStringAsync();
            contentTask.Wait();
            var content = contentTask.Result;
            var expectedRequest = JsonDocument.Parse(requestBody);
            var request = JsonDocument.Parse(content);

            var comparer = new JsonElementComparer();

            var areEqual = comparer.JsonEquals(expectedRequest, request);
            if (!areEqual)
            {
                output.WriteLine($"Expected: {requestBody}");
                output.WriteLine($"Actual: {content}");
            }
            return areEqual;
        }
    }
}
