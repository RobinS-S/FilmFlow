﻿using System.Security.Cryptography;
using System.Text;

namespace FilmFlow.Domain
{
    public class Crypto
    {
        public static string GenerateRandomBaseEncodedString()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(randomBytes);
            }
            var inputText = Convert.ToBase64String(randomBytes);
            return inputText;
        }

        public static string GenerateHash(string input)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
