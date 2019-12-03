using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// Account related data information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed class Account
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userId">A unique user ID associated with the end-user
        /// in your system. If your system allows the login name for the
        /// account to be changed, this should not be the login name for the
        /// account, but rather should be an internal ID that does not
        /// change. This is not your MaxMind account ID.</param>
        /// <param name="username">The username associated with the account.
        /// This is not the MD5 of username. Rather, the MD is automatically
        /// generated from this string.</param>
        public Account(
            string? userId = null,
            string? username = null
        )
        {
            UserId = userId;
            Username = username;

            if (username == null)
            {
                return;
            }
            using (var md5Generator = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(Username);
                var md5 = md5Generator.ComputeHash(bytes);
                UsernameMD5 = BitConverter.ToString(md5)
                    .Replace("-", string.Empty)
                    .ToLower();
            }
        }

        /// <summary>
        /// A unique user ID associated with the end-user in your
        /// system. If your system allows the login name for the
        /// account to be changed, this should not be the login
        /// name for the account, but rather should be an internal
        /// ID that does not change. This is not your MaxMind user
        /// ID.
        /// </summary>
        [JsonProperty("user_id")]
        public string? UserId { get; }

        /// <summary>
        /// The username associated with the account. This is
        /// not the MD5 of username. Rather, the MD5 is automatically
        /// generated from this string.
        /// </summary>
        [JsonIgnore]
        public string? Username { get; }

        /// <summary>
        /// The MD5 generated from the <c>Username</c>
        /// </summary>
        [JsonProperty("username_md5")]
        public string? UsernameMD5 { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"UserId: {UserId}, Username: {Username}, UsernameMD5: {UsernameMD5}";
        }
    }
}
