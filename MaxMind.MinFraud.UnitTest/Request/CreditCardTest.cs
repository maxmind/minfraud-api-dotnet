using System;
using MaxMind.MinFraud.Exception;
using NUnit.Framework;
using MaxMind.MinFraud.Request;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class CreditCardTest
    {
        [Test]
        public void TestIssuerIdNumber()
        {
            var cc = new CreditCard(issuerIdNumber: "123456");
            Assert.AreEqual("123456", cc.IssuerIdNumber);
        }

        [Test]
        public void TestIssuerIdNumberThatIsTooLong()
        {
            Assert.That(() => new CreditCard(issuerIdNumber: "1234567"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestIssuerIdNumberThatIsTooShort()
        {
            Assert.That(() => new CreditCard(issuerIdNumber: "12345"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestIssuerIdNumberThatHasLetters()
        {
            Assert.That(() => new CreditCard(issuerIdNumber: "12345a"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestLast4Digits()
        {
            var cc = new CreditCard(last4Digits: "1234");
            Assert.AreEqual("1234", cc.Last4Digits);
        }

        [Test]
        public void TestLast4DigitsThatIsTooLong()
        {
            Assert.That(() => new CreditCard(last4Digits: "12345"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestLast4DigitsThatIsTooShort()
        {
            Assert.That(() => new CreditCard(last4Digits: "123"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestLast4DigitsThatHasLetters()
        {
            Assert.That(() => new CreditCard(last4Digits: "123a"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TestBankName()
        {
            var cc = new CreditCard(bankName: "Bank");
            Assert.AreEqual("Bank", cc.BankName);
        }

        [Test]
        public void TestBankPhoneCountryCode()
        {
            var cc = new CreditCard(bankPhoneCountryCode: "1");
            Assert.AreEqual("1", cc.BankPhoneCountryCode);
        }

        [Test]
        public void TestBankPhoneNumber()
        {
            string phone = "231-323-3123";
            var cc = new CreditCard(bankPhoneNumber: phone);
            Assert.AreEqual(phone, cc.BankPhoneNumber);
        }

        [Test]
        public void TestAvsResult()
        {
            var cc = new CreditCard(avsResult: 'Y');
            Assert.AreEqual('Y', cc.AvsResult);
        }

        [Test]
        public void TestCvvResult()
        {
            var cc = new CreditCard(cvvResult: 'N');
            Assert.AreEqual('N', cc.CvvResult);
        }
    }
}