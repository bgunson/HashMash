using System;

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

        /*
         * No-arg constructor, init everything to 0 
         */
        public Masher() // We shouldn't need 'this' keyword here should we?
        {
            this._totalCount = 0;
            this._currentLevel = 0;
            this._levelCount = 0;
            this._progress = 0;
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
            _progress = (((float) _levelCount) / 64) * 100;

        }

        /*
         * Given some input string, return "mashed" string with particular operations. Result should be the digest,
         * the hexadecimal representation of the string.
         */
        public string mashInput()
        {
            string mashed = "";
            foreach (char c in inputValue)
            {
                mashed += Convert.ToString(c + _levelCount, 16);
            }
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
