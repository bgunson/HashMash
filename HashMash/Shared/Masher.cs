using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace HashMash.Shared
{
    /*
     *  The main class that will handle the "mashing" (pathetic hashing) of some input text from the user in the Masher.razor componment.
     *  The class will store a total count for every click the user has made to calculate their erspective level, which is also stored.
     *  As the user progresses through levels, the hashing will get more complex.
     */
    public partial class Masher
    {

        private int _totalCount;     // Total count across all levels
        private int _currentLevel;   // Current level
        private int _levelCount;     // Number of clicks on current level
        private float _progress;     // Progress as a percentage of levelCOunt / 64
        private List<int> _listOfPrimes;    // Our list of currently tracked primes for modulus
        private int _currentMod;     // The current modulus
        //private MD5 _md5Hash;        // An MD5 object for MD5 hash calculation, unused as its broken
        private SHA256 _sha256Hash;  // A SHA256 object for SHA256 hash calculation

        /*
         * No-arg constructor, init everything to 0 
         */
        public Masher() 
        {
            _totalCount = 0;
            _currentLevel = 0;
            _levelCount = 0;
            _progress = 0;
            int[] tempPrimeList = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
                                    73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 
                                    157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 
                                    239, 241, 251, 257}; // All primes < 256
            _listOfPrimes = new List<int>(tempPrimeList); // Easiest way to initialize a list with a bunch of elements
            _currentMod = 257;
            //_md5Hash = MD5.Create();
            _sha256Hash = SHA256.Create();
        }

        /*
         * Used to check if a number is prime, and add it to the list of tracked primes if so
         */
        public bool IsPrime(int n)
        {
            if (n < 2) return false;
            foreach(int p in _listOfPrimes)
            {
                if (p > Math.Sqrt(n)) break;
                if (n % p == 0) return false;
            }
            if (!_listOfPrimes.Contains(n)) _listOfPrimes.Add(n);
            return true;
        }

        /*
         * 
         */
        public string getHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /*
         * Main incrementer, can be used to handle logic for level progression
         */
        public void increment()
        {
            _totalCount++;
            _levelCount++;
            int oldLevel = _currentLevel;
            _currentLevel = _totalCount / 64;
            if (oldLevel != _currentLevel)
            {
                // If level changed, then reset current count
                _levelCount = 0;
            }
            if(IsPrime(_currentLevel + 256))
            {
                _currentMod = _currentLevel + 256;
            }
            _progress = (((float) _levelCount) / 64) * 100;

        }

        /*
         * Given some input string, return "mashed" string with particular operations. Result should be the digest,
         * the hexadecimal representation of the string.
         */
        public string mashInput()
        {
            string mashed = "";
            string step1 = "";
            string potentialHash = inputValue;
            foreach (char c in potentialHash)
            {
                string a = Convert.ToString(((c + _levelCount) << (_totalCount % 16)) % _currentMod, 16);
                if (a.Length > 1) {
                    step1 += a[0];
                    step1 += a[1];
                } else
                {
                    step1 += "0" + a;
                }
            }
            while(step1.Length < 64)
            {
                step1 += "6c";
            }
            if(step1.Length > 64)
            {
                step1 = step1.Substring(0, 64);
            }
            mashed = step1;
            mashed = getHash(_sha256Hash, mashed);
            return mashed;
        }

        /*
         * Mash an individual character with appropriate operations.
         * 
         * c is the char (as an int) being mashed
         * b is the base representation which we are converting to e.g. 10 (decimal), 2 (binary), 16 (hexa) etc.
         */
        public string mashCh(int c, int b)
        {
            c += _levelCount;
            return Convert.ToString(c, b);
        }

        public string shiftCh(int c, int b)
        {
            string returnVal = Convert.ToString((c + _levelCount) << (_totalCount % 16), b);
            return returnVal;
        }

        public string modCh(int c, int b)
        {
            string returnVal = Convert.ToString(((c + _levelCount) << (_totalCount % 16)) % _currentMod, b);
            if (returnVal.Length == 1) returnVal = "0" + returnVal;
            return returnVal;
        }

        /*
         * To reset the classes, and handle any other operations when invoked.
         */
        public void reset()
        {
            _totalCount = 0;
            _levelCount = 0;
            _currentLevel = 0;
            _progress = 0;
        }
    }
}
