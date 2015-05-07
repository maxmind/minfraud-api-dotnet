using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    public class Account
    {
        public Account(
            string userId = null,
            string username = null
            )
        {
            UserId = userId;
            Username = username;
        }

        [JsonProperty("user_id")]
        public string UserId { get; }

        [JsonIgnore]
        public string Username { get; }

        [JsonProperty("username_md5")]
        public string UsernameMD5
        {
            get
            {
                using (var md5Generator = MD5.Create())
                {
                    var bytes = Encoding.UTF8.GetBytes(Username);
                    var md5 = md5Generator.ComputeHash(bytes);
                    return BitConverter.ToString(md5)
                        .Replace("-", string.Empty)
                        .ToLower();
                }
            }
        }

        public override string ToString()
        {
            return $"UserId: {UserId}, Username: {Username}, UsernameMD5: {UsernameMD5}";
        }
    }
}