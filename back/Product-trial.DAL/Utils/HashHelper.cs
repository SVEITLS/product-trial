using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Product_trial.DAL.Utils
{
    public static class HashHelper
    {
        private const int SaltSize = 16; // 128 bits
        private const int KeySize = 32; // 256 bits private
        const int Iterations = 50000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
        private const char SegmentDelimiter = ':';

        public static string Hash(string input) 
        { 
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2( input, salt, Iterations, Algorithm, KeySize );
            return string.Join( SegmentDelimiter, Convert.ToBase64String(hash), Convert.ToBase64String(salt), Iterations, Algorithm ); 
        } 
        
        public static bool Verify(string input, string hashString) 
        {
            string[] segments = hashString.Split(SegmentDelimiter);
            byte[] hash = Convert.FromBase64String(segments[0]);
            byte[] salt = Convert.FromBase64String(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2( input, salt, iterations, algorithm, hash.Length );
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        } 
    }
}
 