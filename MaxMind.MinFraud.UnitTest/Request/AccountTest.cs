using MaxMind.MinFraud.Request;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class AccountTest
    {
        [Test]
        public void TestUserId()
        {
            var account = new Account(
                userId: "usr"
                );

            Assert.AreEqual("usr", account.UserId);
        }

        [Test]
        public void TestUsername()
        {
            var account = new Account(
                username: "username"
                );
            Assert.AreEqual("14c4b06b824ec593239362517f538b29", account.UsernameMD5);
        }

        // This test exist as there was a situation where a null
        // username was causing an exception due to not properly
        // checking for null before converting to an MD5.
        [Test]
        public void TestNullUsername()
        {
            var account = new Account(username: null);
            Assert.IsNull(account.UsernameMD5);
        }
    }
}