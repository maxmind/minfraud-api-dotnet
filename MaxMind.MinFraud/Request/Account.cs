using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

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
        }

        /// <summary>
        /// A unique user ID associated with the end-user in your
        /// system. If your system allows the login name for the
        /// account to be changed, this should not be the login
        /// name for the account, but rather should be an internal
        /// ID that does not change. This is not your MaxMind user
        /// ID.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; init; }

        /// <summary>
        /// The username associated with the account. This is
        /// not the MD5 of username. Rather, the MD5 is automatically
        /// generated from this string.
        /// </summary>
        [JsonIgnore]
        public string? Username { get; init; }

        /// <summary>
        /// The MD5 generated from the <c>Username</c>
        /// </summary>
        [JsonPropertyName("username_md5")]
        public string? UsernameMD5 {
            get
            {
                if (Username == null)
                {
                    return null;
                }
                using var md5Generator = MD5.Create();

                var bytes = Encoding.UTF8.GetBytes(Username);
                var md5 = md5Generator.ComputeHash(bytes);
                return BitConverter.ToString(md5)
                    .Replace("-", string.Empty)
                    .ToLower();
            }
        }

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
