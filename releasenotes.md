Release Notes
=============

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
