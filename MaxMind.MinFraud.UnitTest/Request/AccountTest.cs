using MaxMind.MinFraud.Request;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class AccountTest
    {
        [Fact]
        public void TestUserId()
        {
            var account = new Account(
                userId: "usr"
            );

            Assert.Equal("usr", account.UserId);
        }

        [Fact]
        public void TestUsername()
        {
            var account = new Account(
                username: "username"
            );
            Assert.Equal("14c4b06b824ec593239362517f538b29", account.UsernameMD5);
        }

        // This test exist as there was a situation where a null
        // username was causing an exception due to not properly
        // checking for null before converting to an MD5.
        [Fact]
        public void TestNullUsername()
        {
            var account = new Account(username: null);
            Assert.Null(account.UsernameMD5);
        }
    }
}