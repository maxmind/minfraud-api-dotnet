Release Notes
=============

2.2.0
------------------

* This library has been updated to support the nullable reference types
  introduced in C# 8.0.
* Added the following new values to the `PaymentProcessor` enum:
  * `Affirm`
  * `Interac`
* Deprecated the `EmailTenure` and `IPTenure` properties of 
  `MaxMind.MinFraud.Response.Subscores`. 
* Deprecated the `IsHighRisk` property of `MaxMind.MinFraud.Response.GeoIP2Country`.

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
