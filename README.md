# .NET API for MaxMind minFraud Score and minFraud Insights

## Beta Note ##

This is a beta release. The API may change before the first production
release.

You may find information on the changes in minFraud Score and minFraud
Insights in our [What's New
documentation](https://dev.maxmind.com/minfraud/whats-new-in-minfraud-score-and-minfraud-insights/).

## Description ##

This package provides an API for the [MaxMind minFraud Score and
minFraud Insights web services](http://dev.maxmind.com/minfraud/minfraud-score-and-insights-api-documentation/).

## Requirements ##

This library works with .NET Framework version 4.5.2 and above. If you are
using Mono, version 4 or greater is required.

This library depends on [GeoIP2](http://www.nuget.org/packages/MaxMind.GeoIP2/)
and its dependencies.

## Installation ##

### NuGet ###

We recommend installing this library with NuGet. To do this, type the
following into the Visual Studio Package Manager Console:

```
install-package MaxMind.MinFraud
```

## Usage ##

This API uses the [async/await
feature](https://msdn.microsoft.com/en-us/library/hh191443.aspx) introduced in
.NET 4.5 to provide non-blocking calls to the minFraud web services.

To use this API, first create a new `WebServiceClient` object. The constructor
takes your MaxMind user ID, license key, and an optional options array as
arguments:

```csharp
var client = new WebServiceClient(10, 'LICENSEKEY');
```

This object should be disposed of when you done using it.

Then create a new `Transaction` object. This represents the transaction that
you are sending to minFraud:

```csharp
var transaction = new Transaction(
    device: new Device(System.Net.IPAddress.Parse("81.2.69.160"),
        userAgent:
            "Mozilla/5.0 (X11; Linux x86_64)",
        acceptLanguage: "en-US,en;q=0.8"
        ),
    account:
        new Account(
            userId: "3132",
            username: "fred"
            )
    );
```


After creating the request object, send a non-blocking minFraud Score request
by calling the `ScoreAsync` method using the `await` keyword from a method
marked as async:

```csharp
var score = await client.ScoreAsync(transaction);
```

Or a minFraud Insights request by calling `InsightsAsynce` method:

```csharp
var score = await client.ScoreAsync(transaction);
```

If the request succeeds, a model object will be returned for the endpoint. If
the request fails, an exception will be thrown.

See the API documentation for more details.

### Exceptions ###

Thrown by the request models:

* `ArgumentException` - Thrown when invalid data is passed to the model
  constructor.

Thrown by `ScoreAsync(transaction)` or `InsightsAsync(transaction)` on
`WebServiceClient`:

* `AuthenticationException` - Thrown when the server is unable to authenticate
  the request, e.g., if the license key or user ID is invalid.
* `InsufficientFundsException` - Thrown when your account is out of funds.
* `InvalidRequestException` -  Thrown when the server rejects the request for
  another reason such as invalid JSON in the POST.
* `MinFraudException` - Thrown when the server returns an unexpected response.
  This also serves as the base class for the above exceptions.
* `HttpException` - Thrown when an unexpected HTTP error occurs such as an
  internal server error or other unexpected status code.

## Example

```csharp
using MaxMind.MinFraud;
using MaxMind.MinFraud.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MinFraudExample
{
    static void Main()
    {
        MinFraudAsync().Wait();
    }

    static public async Task MinFraudAsync()
    {
        var transaction = new Transaction(
            device: new Device(System.Net.IPAddress.Parse("81.2.69.160"),
                userAgent:
                "Mozilla/5.0 (X11; Linux x86_64)",
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
                address: new System.Net.Mail.MailAddress("test@maxmind.com"),
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
                amount: (decimal) 323.21,
                currency: "USD",
                discountCode: "FIRST",
                affiliateId: "af12",
                subaffiliateId: "saf42",
                referrerUri: new Uri("http://www.amazon.com/")
            ),
            shoppingCart: new List<ShoppingCartItem>
            {
                new ShoppingCartItem(
                    category: "pets",
                    itemId: "ad23232",
                    quantity: 2,
                    price: (decimal) 20.43
                ),
                new ShoppingCartItem(
                    category: "beauty",
                    itemId: "bst112",
                    quantity: 1,
                    price: (decimal) 100.00
                )
            }
        );

        using (var client = new WebServiceClient(6, "ABCD567890"))
        {
            // Use `InsightsAsync` if querying Insights
            var score = await client.ScoreAsync(transaction);
            Console.WriteLine(score);
        }
    }
}
```

## Support ##

Please report all issues with this code using the
[GitHub issue tracker](https://github.com/maxmind/minfraud-api-dotnet/issues).

If you are having an issue with the minFraud service that is not specific
to the client API, please see
[our support page](http://www.maxmind.com/en/support).

## Contributing ##

Patches and pull requests are encouraged. Please include unit tests whenever
possible.

## Versioning ##

This API uses [Semantic Versioning](http://semver.org/).

## Copyright and License ##

This software is Copyright (c) 2016 by MaxMind, Inc.

This is free software, licensed under the Apache License, Version 2.0.
