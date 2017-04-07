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
            Assert.Equal(payment.WasAuthorized, true);
        }

        [Fact]
        public void TestDeclineCode()
        {
            var payment = new Payment(declineCode: "declined");
            Assert.Equal("declined", payment.DeclineCode);
        }
    }
}