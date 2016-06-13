using MaxMind.MinFraud.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace MaxMind.MinFraud.UnitTest.Request
{
    internal class TestHelper
    {
        public static Transaction CreateFullRequest()
        {
            return new Transaction(
                device: new Device(IPAddress.Parse("81.2.69.160"),
                    userAgent:
                        "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.89 Safari/537.36",
                    acceptLanguage: "en-US,en;q=0.8"
                    ),
                userEvent:
                    new Event
                        (
                        transactionId: "txn3134133",
                        shopId: "s2123",
                        time: new DateTimeOffset(2014, 4, 12, 23, 20, 50, 52, new TimeSpan(0)),
                        type: EventType.Purchase
                        ),
                account:
                    new Account(
                        userId: "3132",
                        username: "fred"
                        ),
                email:
                    new Email(
                        address: "test@maxmind.com",
                        domain: "maxmind.com"
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
                        phoneNumber: "323-123-4321",
                        phoneCountryCode: "1"
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
                        phoneNumber: "403-321-2323",
                        phoneCountryCode: "1",
                        deliverySpeed: ShippingDeliverySpeed.SameDay
                        ),
                payment:
                    new Payment(
                        processor: PaymentProcessor.Stripe,
                        wasAuthorized: false,
                        declineCode: "invalid number"
                        ),
                creditCard:
                    new CreditCard(
                        issuerIdNumber: "323132",
                        bankName: "Bank of No Hope",
                        bankPhoneCountryCode: "1",
                        bankPhoneNumber: "800-342-1232",
                        avsResult: 'Y',
                        cvvResult: 'N',
                        last4Digits: "7643"
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

        public static string CurrentDirectory =>
            Directory.GetCurrentDirectory();

        public static string ReadJsonFile(string name)
        {
            return
                File.ReadAllText(Path.Combine(CurrentDirectory, "TestData",
                    $"{name}.json"));
        }

        public static bool VerifyRequestFor(string service, HttpRequestMessage message)
        {
            var requestBody = ReadJsonFile("full-request");

            if (message.Content.Headers.ContentType.ToString() != "application/json; charset=utf-8")
            {
                return false;
            }
            var contentTask = message.Content.ReadAsStringAsync();
            contentTask.Wait();
            var content = contentTask.Result;
            var expectedRequest = JsonConvert.DeserializeObject<JObject>(requestBody);
            var request = JsonConvert.DeserializeObject<JObject>(content);
            return JToken.DeepEquals(expectedRequest, request);
        }
    }
}