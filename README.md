# .NET API for MaxMind minFraud Services #

## Description ##

This package provides an API for the [MaxMind minFraud web services](http://dev.maxmind.com/minfraud/).
This includes minFraud Score, Insights, and Factors. It also includes our
[minFraud Report Transaction API](https://dev.maxmind.com/minfraud/report-transaction/).

The legacy minFraud Standard and Premium services are not supported by this
API.

## Requirements ##

This library works with .NET Framework version 4.6.1 and above and .NET
Standard 2.0 or above.

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
takes your MaxMind account ID and license key:

```csharp
var client = new WebServiceClient(10, "LICENSEKEY");
```

You may also specify the fall-back locales, the host, or the timeout as
optional parameters. See the API docs for more information.

This object is safe to share across threads. If you are making multiple
requests, the object should be reused to so that new connections are not
created for each request. Once you have finished making requests, you
should dispose of the object to ensure the connections are closed and any
resources are promptly returned to the system.

### Making a minFraud Score, Insights, or Factors Request ###

Create a new `Transaction` object. This represents the transaction that you
are sending to minFraud:

```csharp
var transaction = new Transaction(
    device: new Device(
        ipAddress: System.Net.IPAddress.Parse("152.216.7.110"),
        userAgent: "Mozilla/5.0 (X11; Linux x86_64)",
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

A minFraud Insights request by calling the `InsightsAsync` method:

```csharp
var insights = await client.InsightsAsync(transaction);
```

Or a minFraud Factors request by calling the `FactorsAsync` method:

```csharp
var factors = await client.FactorsAsync(transaction);
```

If the request succeeds, a model object will be returned for the endpoint. If
the request fails, an exception will be thrown.

See the API documentation for more details.

### ASP.NET Core Usage ###

To use the web service API with HttpClient factory pattern as a 
[Typed client](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.1#typed-clients)
you need to do the following:

1. Add the following lines to `Startup.cs` `ConfigureServices` method:

```csharp
// Configure to read configuration options from MinFraud section
services.Configure<WebServiceClientOptions>(Configuration.GetSection("MinFraud"));

// Configure dependency injection for WebServiceClient
services.AddHttpClient<WebServiceClient>();
```

2. Add configuration in your `appsettings.json` with your account ID and license key.

```jsonc
...
  "MinFraud": {
    "AccountId": 10,
    "LicenseKey": "LICENSEKEY",
    "Timeout": TimeSpan.FromSeconds(5), // optional
    "Host": "minfraud.maxmind.com" // optional
  },
...
```

3. Inject the `WebServiceClient` where you need to make the call and use it.

```csharp
[ApiController]
[Route("[controller]")]
public class MinFraudController : ControllerBase
{
    private readonly WebServiceClient _minfraudClient;

    public MaxMindController(WebServiceClient minfraudClient)
    {
        _minfraudClient = minfraudClient;
    }

    [HttpGet]
    public async Task<double?> RiskScore(Transaction transaction)
    {
        var score = await _minfraudClient.ScoreAsync(transaction);

        return score.RiskScore;
    }
}
```


### Reporting a Transaction to MaxMind ###

If a transaction was scored incorrectly or you received a chargeback, you may
report it to MaxMind. To do this, create a new `TransactionReport` object:

```csharp
var report = new TransactionReport(
    ipAddress: IPAddress.Parse("104.16.148.244"), 
    tag: TransactionReportTag.Chargeback,
    chargebackCode: "BL",
    maxmindId: "abcd1234",
    minfraudId: new Guid("01c25cb0-f067-4e02-8ed0-a094c580f5e4"),
    notes: "Suspicious account behavior",
    transactionId: "txn123");
```

Only the `ipAddress` and `tag` parameters are required.

Send the report by calling the `ReportAsync` method using the `await` keyword:

```csharp
await client.ReportAsync(report);
```

The endpoint does not return any data and the method does not return a value.
If there is an error, an exception will be thrown.

### Exceptions ###

Thrown by the request models:

* `ArgumentException` - Thrown when invalid data is passed to the model
  constructor.

Thrown by `WebServiceClient` method calls:

* `AuthenticationException` - Thrown when the server is unable to authenticate
  the request, e.g., if the license key or account ID is invalid.
* `InsufficientFundsException` - Thrown when your account is out of funds.
* `PermissionRequireException` - Thrown when your account does not have
  permission to access the service.
* `InvalidRequestException` - Thrown when the server rejects the request for
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
            device: new Device(
                ipAddress: System.Net.IPAddress.Parse("152.216.7.110"),
                userAgent: "Mozilla/5.0 (X11; Linux x86_64)",
                acceptLanguage: "en-US,en;q=0.8",
                sessionAge: 3600,
                sessionId: "a333a4e127f880d8820e56a66f40717c"
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
                phoneNumber: "123-456-7890",
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
                phoneNumber: "123-456-0000",
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
                issuerIdNumber: "411111",
                bankName: "Bank of No Hope",
                bankPhoneCountryCode: "1",
                bankPhoneNumber: "123-456-1234",
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
                referrerUri: new Uri("http://www.amazon.com/")
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
            },
            customInputs: new CustomInputs.Builder
            {
                { "float_input", 12.1d},
                { "integer_input", 3123},
                { "string_input", "This is a string input."},
                { "boolean_input", true},
            }.Build()
        );

        // If you are making multiple requests, a single WebServiceClient
        // should be shared across requests to allow connection reuse. The
        // class is thread safe.
        //
        // Replace "6" with your account ID and "ABCD567890" with your license
        // key.
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

This software is Copyright (c) 2015-2020 by MaxMind, Inc.

This is free software, licensed under the Apache License, Version 2.0.
