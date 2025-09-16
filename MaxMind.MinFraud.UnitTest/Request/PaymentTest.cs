using MaxMind.MinFraud.Request;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class PaymentTest
    {
        [Fact]
        public void TestProcessor()
        {
            var payment = new Payment(processor: PaymentProcessor.Adyen);
            Assert.Equal(PaymentProcessor.Adyen, payment.Processor);
        }

        [Fact]
        public void TestWasAuthorized()
        {
            var payment = new Payment(wasAuthorized: true);
            Assert.True(payment.WasAuthorized);
        }

        [Fact]
        public void TestDeclineCode()
        {
            var payment = new Payment(declineCode: "declined");
            Assert.Equal("declined", payment.DeclineCode);
        }

        [Fact]
        public void TestBankDebitMethod()
        {
            var payment = new Payment(method: PaymentMethod.BankDebit);
            Assert.Equal(PaymentMethod.BankDebit, payment.Method);
        }

        [Fact]
        public void TestCardMethod()
        {
            var payment = new Payment(method: PaymentMethod.Card);
            Assert.Equal(PaymentMethod.Card, payment.Method);
        }

        [Fact]
        public void TestCryptoMethod()
        {
            var payment = new Payment(method: PaymentMethod.Crypto);
            Assert.Equal(PaymentMethod.Crypto, payment.Method);
        }

        [Fact]
        public void TestDigitalWalletMethod()
        {
            var payment = new Payment(method: PaymentMethod.DigitalWallet);
            Assert.Equal(PaymentMethod.DigitalWallet, payment.Method);
        }

        [Fact]
        public void TestRewardsMethod()
        {
            var payment = new Payment(method: PaymentMethod.Rewards);
            Assert.Equal(PaymentMethod.Rewards, payment.Method);
        }
    }
}
