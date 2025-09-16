Release Notes
=============

5.3.0
------------------

* Added `Securepay` to the `PaymentProcessor` enum.
* Added `CreditApplication` and `FundTransfer` to the `EventType` enum.

5.2.0 (2025-05-23)
------------------

* Added support for the `/billing_phone/matches_postal` and
  `/shipping_phone/matches_postal` outputs. These are available as the
  `MatchesPostal` property on `MaxMind.MinFraud.Response.Phone`.
* Added `Cryptomus` to the `PaymentProcessor` enum.

5.1.0 (2025-02-10)
------------------

* .NET 6.0 and .NET 7.0 have been removed as targets as they have both
  reach their end of support from Microsoft. If you are using these versions,
  the .NET Standard 2.1 target should continue working for you.
* .NET 9.0 has been added as a target.
* The minFraud Factors subscores have been deprecated. They will be removed
  in March 2025. Please see [our release notes](https://dev.maxmind.com/minfraud/release-notes/2024/#deprecation-of-risk-factor-scoressubscores)
  for more information.
* Added `Epayco` to the `PaymentProcessor` enum.

5.1.0-beta.1 (2024-09-06)
-------------------------

* Added support for the new risk reasons outputs in minFraud Factors. The risk
  reasons output codes and reasons are currently in beta and are subject to
  change. We recommend that you use these beta outputs with caution and avoid
  relying on them for critical applications.

5.0.0 (2024-07-08)
------------------

* Updated `TransactionReport` to make the `ipAddress` parameter optional. Now
  the `tag` and at least one of the following parameters must be supplied:
  `ipAddress`, `maxmindId`, `minfraudID`, `transactionID`.
* Added `BillingPhone` and `ShippingPhone` properties to the minFraud Insights
  and Factors response models. These contain objects with information about
  the respective phone numbers. Please see [our developer
  site](https://dev.maxmind.com/minfraud/api-documentation/responses/) for
  more information.
* Added `Payconex` to the `PaymentProcessor` enum.
* Removed several obsolete constructors as well as the `Last4Digits`
  property on `MaxMind.MinFraud.Request.CreditCard`.

4.3.0 (2024-04-16)
------------------

* Added `PxpFinancial` and `Trustpay` to the `PaymentProcessor` enum.
* Equivalent domain names are now normalized when `hashAddress` is used.
  For example, `googlemail.com` will become `gmail.com`.
* Periods are now removed from `gmail.com` email address local parts when
  `hashAddress` is used. For example, `f.o.o@gmail.com` will become
  `foo@gmail.com`.
* Fastmail alias subdomain email addresses are now normalized when
  `hashAddress` is used. For example, `alias@user.fastmail.com` will
  become `user@fastmail.com`.
* Additional `yahoo.com` email addresses now have aliases removed from
  their local part when `hashAddress` is used. For example,
  `foo-bar@yahoo.com` will become `foo@yahoo.com` for additional
  `yahoo.com` domains.
* Duplicate `.com`s are now removed from email domain names when
  `hashAddress` is used. For example, `example.com.com` will become
  `example.com`.
* Certain TLD typos are now normalized when `hashAddress` is used. For
  example, `example.comcom` will become `example.com`.
* Additional `gmail.com` domain names with leading digits are now
  normalized when `hashAddress` is used. For example, `100gmail.com` will
  become `gmail.com`.
* Additional `gmail.com` typos are now normalized when `hashAddress` is
  used. For example, `gmali.com` will become `gmail.com`.
* When `hashAddress` is used, all trailing periods are now removed from an
  email address domain. Previously only a single period was removed.
* When `hashAddress` is used, the local part of an email address is now
  normalized to NFC.

4.2.0 (2023-12-05)
------------------

* .NET 5.0 has been removed as a target as it has reach its end of life.
  However, if you are using .NET 5.0, the .NET Standard 2.1 target should
  continue working for you.
* .NET 7.0 and .NET 8.0 have been added as a target.
* Added  `GooglePay`, `Placetopay`, and `ShopifyPayments` to the
  `PaymentProcessor` enum.
* Added `IWebServiceClient` to facilitate mocking of `WebServiceClient`.
  Pull request by Ian Göbl. GitHub #152.
* Updated `MaxMind.GeoIP2` dependency to version that includes the `IsAnycast`
  property on `MaxMind.GeoIP2.Model.Traits`. This is `true` if the IP address
  belongs to an [anycast network](https://en.wikipedia.org/wiki/Anycast).
  This is available in minFraud Insights and Factors.

4.1.0 (2022-03-28)
------------------

* Added the `Country` property to `MaxMind.MinFraud.Request.CreditCard`.
  This is the country where the issuer of the card is located. This may be
  passed instead of `IssuerIdNumber` if you do not wish to pass partial
  account numbers or if your payment processor does not provide them.

4.0.0 (2022-02-07)
------------------

* This library no longer targets .NET 4.6.1.
* .NET 6.0 was added as a target.
* Removed deprecated code, include:
  * The class `MaxMind.MinFraud.Response.GeoIP2Country` was removed. This was
    a subclass of `MaxMind.GeoIP2.Model.Country` that added an `IsHighRisk`
    property. This property was marked obsolete in 2019. Now
    `MaxMind.GeoIP2.Model.Country` is used directly.
  * In `MaxMind.MinFraud.Response.Subscores`, the properties `EmailTenure`
    and `IPTenure` were removed. These were deprecated in the web service
    and always return a fixed value.

3.3.0 (2022-01-27)
------------------

* Upgraded `MaxMind.GeoIP2` to 4.1.0. This adds mobile country code (MCC)
  and mobile network code (MNC) to minFraud Insights and Factors responses.
  These are available at `response.IPAddress.Traits.MobileCountryCode` and
  `response.IPAddress.Traits.MobileNetworkCode`. We expect this data to be
  available by late January 2022.
* Added the following new values to the `PaymentProcessor` enum:
  * `Boacompra`
  * `Boku`
  * `Coregateway`
  * `Fiserv`
  * `Neopay`
  * `Neosurf`
  * `Openbucks`
  * `Paysera`
  * `Payvision`
  * `Trustly`
  * `Windcave`
* The `last4Digits` constructor parameter and and `Last4Digits` property of
  `MaxMind.MinFraud.Request.CreditCard` have been deprecated in favor of
  `lastDigits` / `LastDigits` respectively and will be removed in a future
  release. `lastDigits` / `LastDigits` also now supports two digit values in
  addition to the previous four digit values.
* Eight digit `MaxMind.MinFraud.Request.CreditCard.issuerIdNumber` inputs are
  now supported in addition to the previously accepted six digit `issuerIdNumber`.
  In most cases, you should send the last four digits for
  `MaxMind.MinFraud.Request.CreditCard.lastDigits`. If you send a `issuerIdNumber`
  that contains an eight digit IIN, and if the credit card brand is not one of the
  following, you should send the last two digits for `lastDigits`:
  * `Discover`
  * `JCB`
  * `Mastercard`
  * `UnionPay`
  * `Visa`

3.2.0 (2021-08-27)
------------------

* Added the following new values to the `PaymentProcessor` enum:
  * `Cardknox`
  * `Creditguard`
  * `Credorax`
  * `Datacap`
  * `Dlocal`
  * `Onpay`
  * `Safecharge`
* Documented the new "test" disposition.
* Added support for the `/disposition/risk_label` output in Score, Insights and
  Factors. This is the available as the `RiskLabel` property of
  `MaxMind.MinFraud.Response.Disposition`, and is the label of the custom rule
  that was triggered by the transaction.
* Added support for the `/credit_card/was_3d_secure_successful` input in Score,
  Insights and Factors. This input should indicate whether or not the outcome
  of 3-D Secure verification (e.g. Safekey, SecureCode, Verified by Visa) was
  successful. `true` if customer verification was successful, or `false` if
  the customer failed verification. If 3-D Secure verification was not used, was
  unavailable, or resulted in another outcome other than success or failure, do
  not include this field.

3.1.0 (2021-02-02)
------------------

* Added `ApplePay` and `ApsPayments` to the `PaymentProcessor` enum.
* Added additional normalizing of the request email address in
  `MaxMind.MinFraud.Request.Email` when `hashAddress` is set to `true`.
* Added support for the IP address risk reasons in the minFraud Insights and
  Factors responses. This is available at `response.IPAddress.RiskReasons`.
  It is a list of `MaxMind.MinFraud.Response.IPRiskReason` objects.

3.0.0 (2020-11-24)
------------------

* This library now requires .NET Framework 4.6.1 or greater or .NET Standard
  2.0 or greater.
* .NET 5.0 was added as a target framework.
* `System.Text.Json` is now used for serializing and deserializing JSON.
  If you are serializing the objects yourself, the `Newtonsoft.Json`
  attributes have been removed and you will need to switch to
   `System.Text.Json`.
* When creating a `Transaction` object, you may now use init-only properties
  rather than constructor parameters.
* You may now create `WebServiceClient` as a typed client with
  `IHttpClientFactory` in .NET Core 2.1+.
* Exception objects now correctly implement `ISerializable`.
* The `Warnings` properties on the response models are now an
  `IReadOnlyList<Warning>` rather than an `IList<Warning>`.
* The `Names` properties on `NamedEntity` models are now
  `IReadOnlyDictionary<string, string>`.
* The `Subdivisions` property on `MaxMind.MinFraud.Response.IPAddress` is now
  an `IReadOnlyList<Subdivision>`.
* `GeoNameId` properties on `NamedEntity` models are now `long?` rather than
  `int?` to match the underlying database.

2.9.0 (2020-10-14)
------------------

* This library now requires .NET Framework 4.5 or greater or .NET Standard
  2.0 or greater.
* The device IP address is no longer a required input.
* Added `Tsys` to the `PaymentProcessor` enum.

2.8.0 (2020-09-25)
------------------

* Added `response.IPAddress.Traits.IsResidentialProxy` to the minFraud
  Insights and Factors models. This indicates whether the IP address is on a
  suspected anonymizing network and belongs to a residential ISP.

2.7.0 (2020-07-31)
------------------

* Added the following new values to the `PaymentProcessor` enum:
  * `Cashfree`
  * `FirstAtlanticCommerce`
  * `Komoju`
  * `Paytm`
  * `Razorpay`
  * `Systempay`
* Added support for new Subscores outputs. These are
  available as the `Device`, `EmailLocalPart` and `ShippingAddress` properties
  on `MaxMind.MinFraud.Response.Subscores` on minFraud Factors response
  objects.

2.6.0 (2020-06-19)
------------------

* Added support for the minFraud Report Transaction API. Reporting
  transactions to MaxMind helps us detect about 10-50% more fraud and
  reduce false positives for you.

2.5.0 (2020-04-06)
------------------

* Added support for the new credit card output `/credit_card/is_business`.
  This indicates whether the card is a business card. It may be accessed via
  `response.CreditCard.IsBusiness` on the minFraud Insights and Factors
  response objects.

2.4.0 (2020-03-26)
------------------

* Added support for the `/email/domain/first_seen` output in minFraud Insights
  and Factors. This is available as the `FirstSeen` property on
  `MaxMind.MinFraud.Response.EmailDomain`.
* Added the following new values to the `PaymentProcessor` enum:
  * `Cardpay`
  * `Epx`

2.3.0 (2020-02-21)
------------------

* Added support for the `/email/is_disposable` output in minFraud Insights
  and Factors. This is available as the `IsDisposable` property on
  `MaxMind.MinFraud.Response.Email`.
* Added the following new values to the `PaymentProcessor` enum:
  * `Cetelem`
  * `Ecommpay`
  * `G2aPay`
  * `Mercanet`

2.2.0 (2019-12-10)
------------------

* This library has been updated to support the nullable reference types
  introduced in C# 8.0.
* The client-side validation for numeric custom inputs has been updated to
  match the server-side validation. The valid range is -9,999,999,999,999
  to 9,999,999,999,999. Previously, larger numbers were allowed.
* Added the following new values to the `PaymentProcessor` enum:
  * `Affirm`
  * `Interac`
* Deprecated the `EmailTenure` and `IPTenure` properties of
  `MaxMind.MinFraud.Response.Subscores`.
* Deprecated the `IsHighRisk` property of `MaxMind.MinFraud.Response.GeoIP2Country`.
* `netstandard2.1` was added as a target framework.

2.1.0 (2019-08-12)
------------------

* Previously, `FactorsAsync` would incorrectly return an `Insights`
  object, hiding Factors-specific fields. Bug reported by Bogdan
  Polovyk. GitHub #49.
* Added the following new values to the `PaymentProcessor` enum:
  * `Afterpay`
  * `DataCash`
  * `Dotpay`
  * `GoCardless`
  * `Klarna`
  * `Payeezy`
  * `Paylike`
  * `PaymentExpress`
  * `Paysafecard`
  * `SmartDebit`
  * `SynapseFI`
* Dependencies were updated.

2.0.0 (2018-04-11)
------------------

* The `userId` constructor parameter for `WebServiceClient` was renamed to
  `accountId` and support was added for the error code `ACCOUNT_ID_REQUIRED`.
* Added the following new values to the `PaymentProcessor` enum:
  * `Ccavenue`
  * `CtPayments`
  * `Dalenys`
  * `Oney`
  * `Posconnect`
* Added support for the `/device/local_time` output. This is exposed as
  the `LocalTime` property on `MaxMind.MinFraud.Response.Device`.
* Added support for the `/credit_card/is_virtual` output. This is exposed as
  the `IsVirtual` property on `MaxMind.MinFraud.Response.CreditCard`.
* Added `PayoutChange` to the `EventType` enum.

1.6.0 (2018-01-22)
------------------

* Updated `MaxMind.GeoIP2` dependency. With this version, the
  `IsInEuropeanUnion` property is now available on
  `MaxMind.MinFraud.Response.GeoIP2Country` and
  `MaxMind.GeoIP2.Model.RepresentedCountry`. `IsInEuropeanUnion` is `true` if
  the country is a member state of the European Union.
* Added the following new values to the `PaymentProcessor` enum:
  * `Cybersource`
  * `TransactPro`
  * `Wirecard`

1.5.0 (2017-11-01)
------------------

* Behavior change: When sending an email address to MaxMind, this library now
  defaults to sending the plain text email address rather than its MD5 hash.
  Previously only the MD5 hash of the email address would be sent and sending
  the plain text email address was not possible. If you wish to send only the
  MD5 hash of the email address, you must now set the `hashAddress` constructor
  parameter to `true` on `MaxMind.MinFraud.Request.Email`.
* Previously, it was possible to get an `IndexOutOfRangeException` when calling
  the `MaxMind.MinFraud.Request.Email` constructor with an invalid email
  address. Now an `ArgumentException` will be thrown.
* When sending a hashed email address, the address is now lower-cased before
  the MD5 is calculated.
* Added the following new values to the `PaymentProcessor` enum:
  * `Bpoint`
  * `CheckoutCom`
  * `Emerchantpay`
  * `Heartland`
  * `Payway`
* A `netstandard2.0` target was added to eliminate additional dependencies
  required by the `netstandard1.4` target.
* As part of the above work, the separate Mono build files were dropped. As
  of Mono 5.0.0, `msbuild` is supported.
* Updated `MaxMind.GeoIP2` dependency to add support for GeoIP2 Precision
  Insights anonymizer fields.

1.4.1 (2017-07-21)
------------------

* Fixed an issue where the client would throw an exception if the
  `Content-Length` header was missing in the response. This could happen with
  chunked responses.

1.4.0 (2017-07-07)
------------------

* Added support for custom inputs. These can be set up from your account portal.
* Added support for the `/device/session_age` and `/device/session_id` inputs.
  Use the `sessionAge` and `sessionId` constructor parameters on
  `MaxMind.MinFraud.Request.Device` to set them.
* Added support for the `/email/first_seen` output. Use `FirstSeen` on
  `MaxMind.MinFraud.Response.FirstSeen` to access it.
* Added the following new values to the `PaymentProcessor` enum:
  * `AmericanExpressPaymentGateway`
  * `Bluesnap`
  * `Commdoo`
  * `Curopayments`
  * `Ebs`
  * `Exact`
  * `Hipay`
  * `LemonWay`.
  * `Oceanpayment`
  * `Paymentwall`
  * `Payza`
  * `Securetrading`
  * `SolidtrustPay`
  * `Vantiv`
  * `Vericheck`
  * `Vpos`
* Updated the docs for `MaxMind.MinFraud.Response.Address` now that
 `IsPostalInCity` may be returned for addresses world-wide.
* Switched to the updated MSBuild .NET Core build system.
* Moved tests from NUnit to xUnit.net.

1.3.0 (2016-11-22)
------------------

* The disposition was added to the minFraud response models. This is used to
  return the disposition of the transaction as set by the custom rules for the
  account.
* Updated to .NET Core 1.1.

1.2.1 (2016-11-21)
------------------

* Fixed a bug where a null `username` passed to the request `Account` model
  would cause an exception when attempting to encode the username as an MD5.

1.2.0 (2016-11-14)
------------------

* Added `/credit_card/token` input. Use the `token` constructor parameter on
  `MaxMind.MinFraud.Request.CreditCard` to set it.
* All validation regular expressions are now pre-compiled.
* Use framework assembly for `System.Net.Http` on .NET 4.5 rather than NuGet
  package.

1.1.0 (2016-10-11)
------------------

* Added the following new values to the `EventType` enum: `EmailChange` and
  `PasswordReset`.

1.0.0 (2016-09-16)
------------------

* First production release. No code changes.

0.7.1 (2016-08-08)
------------------

* Re-release of 0.7.0 to fix strong-name issue. No code changes.

0.7.0 (2016-08-01)
------------------

* .NET Core 1.0 support.
* Clarification of unit price in documentation.

0.7.0-beta1 (2016-06-15)
------------------------

* .NET Core support.
* `Email` request class constructor now takes a `string` email address rather
  than a `MailAddress`. This was does as CoreFX does not currently have
  `System.Net.Mail`.
* Exceptions are no longer serializable.
* Added the following new values to the `PaymentProcessor` enum:
  `ConceptPayments`, `Ecomm365`, `Orangepay`, and `PacnetServices`.

0.6.0 (2016-06-08)
------------------

* BREAKING CHANGE: `CreditsRemaining` has been removed from the web service
  models and has been replaced by `QueriesRemaining`.
* Added `QueriesRemaining` and `FundsRemaining`. Note that `FundsRemaining`
  will not be returned by the web service until our new credit system is in
  place.
* `Confidence` and `LastSeen` were added to the `Device` response model.
* `LocalTime` in the `GeoIP2Location` model is now nullable.

0.5.0 (2016-05-23)
------------------

* Added support for the minFraud Factors.
* Added IP address risk to the minFraud Score model.
* Handle `PERMISSION_REQUIRED` errors by throwing a
  `PermissionRequiredException`.
* Updated dependencies.
* Added the following new values to the `PaymentProcessor` enum:
  `Ccnow`, `Dalpay`, `Epay` (replaces `Epayeu`), `Payplus`, `Pinpayments`,
  `Quickpay`, and `Verepay`.

0.4.0-beta2 (2016-01-29)
------------------------

* The target framework is now .NET 4.5 rather than 4.5.2.
* Updated GeoIP2 to 2.6.0-beta2.

0.4.0-beta1 (2016-01-28)
------------------------

* Added support for new minFraud Insights outputs. These are:
    * `/credit_card/brand`
    * `/credit_card/type`
    * `/device/id`
    * `/email/is_free`
    * `/email/is_high_risk`
* `Input` on the `Warning` response model has been replaced with
  `InputPointer`. The latter is a JSON pointer to the input that caused the
  warning.
* Updated GeoIP2 to 2.6.0-beta1.

0.3.1 (2015-12-04)
------------------

* Actually remove the BCL dependency.

0.3.0 (2015-12-04)
------------------

* Update `MaxMind.GeoIP2` to 2.5.0. This removes the BCL dependency.
* Upgrade to NUnit 3.

0.2.0 (2015-09-23)
------------------

* Update `MaxMind.GeoIP2` to 2.4.0.

0.2.0-beta1 (2015-09-10)
------------------------

* Add `is_gift` and `has_gift_message` order inputs.
* Upgrade to the latest GeoIP2 release.
* `Score.CreditsRemaining` was change from a `ulong?` to `long?` in order to
  be more CLS compliant.
* GeoIP2 dependency was updated to a version that does not depend on
  RestSharp.
* Some parameters and properties were changed from using concrete classes to
  interfaces.
* The library now has a strong name.

0.1.1-beta1 (2015-06-30)
------------------------

* Upgrade to Json.NET 7.0.1 and GeoIP2 2.3.1-beta1.

0.1.0 (2015-06-29)
------------------

* Release as beta.

0.0.1 (2015-06-18)
------------------

* Initial release.
