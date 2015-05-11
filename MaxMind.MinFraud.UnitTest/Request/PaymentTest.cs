using MaxMind.MinFraud.Request;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class PaymentTest
    {
        [Test]
        public void TestProcessor()
        {
            var payment = new Payment(processor: PaymentProcessor.Adyen);
            Assert.AreEqual(PaymentProcessor.Adyen, payment.Processor);
        }

        [Test]
        public void TestWasAuthorized()
        {
            var payment = new Payment(wasAuthorized: true);
            Assert.AreEqual(payment.WasAuthorized, true);
        }

        [Test]
        public void TestDeclineCode()
        {
            var payment = new Payment(declineCode: "declined");
            Assert.AreEqual("declined", payment.DeclineCode);
        }
    }
}