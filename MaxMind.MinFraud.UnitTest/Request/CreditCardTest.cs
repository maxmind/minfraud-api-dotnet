using MaxMind.MinFraud.Request;
using System;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class CreditCardTest
    {
        [Fact]
        public void TestCountry()
        {
            var cc = new CreditCard(country: "US");
            Assert.Equal("US", cc.Country);
        }

        [Fact]
        public void TestCountryInit()
        {
            var cc = new CreditCard
            {
                Country = "US"
            };
            Assert.Equal("US", cc.Country);
        }

        [Fact]
        public void TestCountryThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(country: "USA"));
        }

        [Fact]
        public void TestCountryWithNumbers()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(country: "U1"));
        }

        [Fact]
        public void TestCountryInWrongCase()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(country: "us"));
        }

        [Fact]
        public void TestIssuerIdNumber()
        {
            var cc6 = new CreditCard(issuerIdNumber: "123456");
            Assert.Equal("123456", cc6.IssuerIdNumber);

            var cc8 = new CreditCard(issuerIdNumber: "12345678");
            Assert.Equal("12345678", cc8.IssuerIdNumber);
        }

        [Fact]
        public void TestIssuerIdNumberThatIsInvalidLength()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(issuerIdNumber: "1234567"));
        }

        [Fact]
        public void TestIssuerIdNumberThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(issuerIdNumber: "123456789"));
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
#pragma warning disable 618
            var cc2 = new CreditCard(last4Digits: "12");
            Assert.Equal("12", cc2.Last4Digits);

            var cc4 = new CreditCard(last4Digits: "1234");
            Assert.Equal("1234", cc4.Last4Digits);
#pragma warning restore 618
        }

        [Fact]
        public void TestLastDigits()
        {
            var cc2 = new CreditCard(lastDigits: "12");
            Assert.Equal("12", cc2.LastDigits);

            var cc4 = new CreditCard(lastDigits: "1234");
            Assert.Equal("1234", cc4.LastDigits);
        }

        [Fact]
        public void TestLastDigitsThatIsTooLong()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(lastDigits: "12345"));
        }

        [Fact]
        public void TestLastDigitsThatIsTooShort()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(lastDigits: "1"));
        }

        [Fact]
        public void TestLastDigitsThatHasLetters()
        {
            Assert.Throws<ArgumentException>(() => new CreditCard(lastDigits: "123a"));
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
            Assert.Throws<ArgumentException>(() => new CreditCard(token: token));
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