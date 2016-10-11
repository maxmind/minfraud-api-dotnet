Release Notes
=============

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
