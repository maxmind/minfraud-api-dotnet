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
        public void TestRequired()
        {
            var maxmindId = "12345678";
            var minfraudId = Guid.NewGuid();
            var tag = TransactionReportTag.NotFraud;
            var transactionId = "txn123";

            TransactionReport report;

            // ipAddress supplied as identifier
            report = new TransactionReport(tag: tag, ipAddress: IP);
            Assert.Equal(TransactionReportTag.NotFraud, report.Tag);
            Assert.Equal(IP, report.IPAddress);

            // maxmindId supplied as identifier
            report = new TransactionReport(tag: tag, ipAddress: null, maxmindId: maxmindId);
            Assert.Equal(TransactionReportTag.NotFraud, report.Tag);
            Assert.Null(report.IPAddress);
            Assert.Equal(maxmindId, report.MaxMindId);

            // minfraudId supplied as identifier
            report = new TransactionReport(tag: tag, ipAddress: null, minfraudId: minfraudId);
            Assert.Equal(TransactionReportTag.NotFraud, report.Tag);
            Assert.Null(report.IPAddress);
            Assert.Equal(minfraudId, report.MinFraudId);

            // tranactionId supplied as identifier
            report = new TransactionReport(tag: tag, ipAddress: null, transactionId: transactionId);
            Assert.Equal(TransactionReportTag.NotFraud, report.Tag);
            Assert.Null(report.IPAddress);
            Assert.Equal(transactionId, report.TransactionId);

            // no identifier supplied
            Assert.Throws<ArgumentException>(() => new TransactionReport(ipAddress: null, tag: tag));
        }

        [Fact]
        public void TestAll()
        {
            var chargebackCode = "4853";
            var maxmindId = "abcd1234";
            var minfraudId = Guid.NewGuid();
            var notes = "This was an account takeover.";
            var transactionId = "txn123";

            var report = new TransactionReport(
                ipAddress: IP,
                tag: TransactionReportTag.Chargeback,
                chargebackCode: chargebackCode,
                maxmindId: maxmindId,
                minfraudId: minfraudId,
                notes: notes,
                transactionId: transactionId);

            Assert.Equal(IP, report.IPAddress);
            Assert.Equal(TransactionReportTag.Chargeback, report.Tag);
            Assert.Equal(maxmindId, report.MaxMindId);
            Assert.Equal(minfraudId, report.MinFraudId);
            Assert.Equal(notes, report.Notes);
            Assert.Equal(transactionId, report.TransactionId);
        }

        [Theory]
        [InlineData("abcd123")]
        [InlineData("")]
        [InlineData("abcd12345")]
        public void TestMaxMindIdIsInvalid(string? maxmindId)
        {
            Assert.Throws<ArgumentException>(() => new TransactionReport(
                IP, TransactionReportTag.SpamOrAbuse, maxmindId: maxmindId));
        }
    }
}