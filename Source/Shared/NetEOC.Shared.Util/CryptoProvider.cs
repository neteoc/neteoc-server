using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NetEOC.Shared.Util
{
    public class CryptoProvider
    {
        private static RandomNumberGenerator random = RandomNumberGenerator.Create();

        public string HashPassword(string salt, string password)
        {
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000);

            byte[] hash = hasher.GetBytes(64);

            return Convert.ToBase64String(hash);
        }

        public bool VerifyHash(string salt, string password, string hash)
        {
            return HashPassword(salt, password) == hash;
        }

        public string GenerateSalt()
        {
            int max_length = 32;

            byte[] salt = new byte[max_length];

            random.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            StringBuilder res = new StringBuilder();

            Random rnd = new Random();

            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            return res.ToString();
        }

        public string CreateUrlKey()
        {
            return CreatePassword(64);
        }
    }
}
