using MaxMind.MinFraud.Request;
using System;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class CreditCardTest
    {
        [Fact]
        public void TestIssuerIdNumber()
        {
            var cc = new CreditCard(issuerIdNumber: "123456");
            Assert.Equal("123456", cc.IssuerIdNumber);
        }

        [Fact]
        public void TestIssuerIdNumberThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(issuerIdNumber: "1234567"));
        }

        [Fact]
        public void TestIssuerIdNumberThatIsTooShort()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(issuerIdNumber: "12345"));
        }

        [Fact]
        public void TestIssuerIdNumberThatHasLetters()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(issuerIdNumber: "12345a"));
        }

        [Fact]
        public void TestLast4Digits()
        {
            var cc = new CreditCard(last4Digits: "1234");
            Assert.Equal("1234", cc.Last4Digits);
        }

        [Fact]
        public void TestLast4DigitsThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(last4Digits: "12345"));
        }

        [Fact]
        public void TestLast4DigitsThatIsTooShort()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(last4Digits: "123"));
        }

        [Fact]
        public void TestLast4DigitsThatHasLetters()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(last4Digits: "123a"));
        }

        [Fact]
        public void TestBankName()
        {
            var cc = new CreditCard(bankName: "Bank");
            Assert.Equal("Bank", cc.BankName);
        }

        [Fact]
        public void TestBankPhoneCountryCode()
        {
            var cc = new CreditCard(bankPhoneCountryCode: "1");
            Assert.Equal("1", cc.BankPhoneCountryCode);
        }

        [Fact]
        public void TestBankPhoneNumber()
        {
            var phone = "231-323-3123";
            var cc = new CreditCard(bankPhoneNumber: phone);
            Assert.Equal(phone, cc.BankPhoneNumber);
        }

        [Fact]
        public void TestAvsResult()
        {
            var cc = new CreditCard(avsResult: 'Y');
            Assert.Equal('Y', cc.AvsResult);
        }

        [Fact]
        public void TestCvvResult()
        {
            var cc = new CreditCard(cvvResult: 'N');
            Assert.Equal('N', cc.CvvResult);
        }

        [Theory]
        [InlineData("4485921507912924")]
        [InlineData("432312")]
        [InlineData("this is invalid")]
        [InlineData("")]
        [InlineData(
            "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx")]
        public void TestInvalidToken(string token)
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(last4Digits: token));
        }

        [Theory]
        [InlineData("t4485921507912924")]
        [InlineData("a7f6%gf83fhAu")]
        [InlineData("valid_token")]
        public void TestValidToken(string token)
        {
            var cc = new CreditCard(token: token);
            Assert.Equal(token, cc.Token);
        }

        [Fact]
        public void TestWas3DSecureSuccessful()
        {
            var cc = new CreditCard(was3DSecureSuccessful: true);
            Assert.True(cc.Was3DSecureSuccessful);
        }
    }
}