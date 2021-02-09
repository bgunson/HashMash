using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HashMash
{
    public class Hasher
    {
        
        public string HashPassword(string password, int prfIndex)
        {
            if (password == "") return "";

            // The following is from https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-5.0

            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Use int var to denote which algorithm to use
            // 0 = SHA1
            // 1 = SHA256
            // 2 = SHA512

            int numBits;
            if (prfIndex == 0)
            {
                numBits = 160;
            } else if (prfIndex == 1)
            {
                numBits = 256;
            } else
            {
                numBits = 512;
            }

            return "";
            //KeyDerivationPrf prf = (KeyDerivationPrf) prfIndex;

            //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //password: password,
            //salt: salt,
            //prf: prf,
            //iterationCount: 10000,
            //numBytesRequested: numBits / 8));
            //return hashed;
        }

        

    }

}
