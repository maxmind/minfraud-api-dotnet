using MaxMind.MinFraud.Request;
using System;
using System.Net;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class TransactionReportTest
    {
        private IPAddress IP { get; } = IPAddress.Parse("1.1.1.1");

        [Fact]
        public void TestAll()
        {
            var chargebackCode = "4853";
            var maxmindId = "abcd1234";
            var minfraudId = Guid.NewGuid();
            var notes = "This was an account takeover.";
            var transactionId = "txn123";

            var report = new TransactionReport
            {
                IPAddress = IP,
                Tag = TransactionReportTag.Chargeback,
                ChargebackCode = chargebackCode,
                MaxMindId = maxmindId,
                MinFraudId = minfraudId,
                Notes = notes,
                TransactionId = transactionId
            };

            Assert.Equal(IP, report.IPAddress);
            Assert.Equal(TransactionReportTag.Chargeback, report.Tag);
            Assert.Equal(maxmindId, report.MaxMindId);
            Assert.Equal(minfraudId, report.MinFraudId);
            Assert.Equal(notes, report.Notes);
            Assert.Equal(transactionId, report.TransactionId);
        }

        [Fact]
        public void TestGuidEmptyIsNormalized()
        {
            var report = new TransactionReport
            {
                Tag = TransactionReportTag.NotFraud,
                MinFraudId = Guid.Empty
            };
            Assert.Null(report.MinFraudId);
        }

        [Theory]
        [InlineData("abcd123")]
        [InlineData("")]
        [InlineData("abcd12345")]
        public void TestMaxMindIdIsInvalid(string? maxmindId)
        {
            Assert.Throws<ArgumentException>(() => new TransactionReport
            {
                Tag = TransactionReportTag.SpamOrAbuse,
                MaxMindId = maxmindId
            });
        }
    }
}
