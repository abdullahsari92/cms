using System;
using System.Security.Cryptography;
using System.Text;


namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out string passwordHash)
        {
            using var hmac = new SHA256CryptoServiceProvider();

            passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public static string VerifyPasswordHash(string password)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            var shaPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return shaPassword;
        }
    }
}