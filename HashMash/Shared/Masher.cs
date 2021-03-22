using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HashMash.Shared
{
    public partial class Masher
    {

        private int totalCount;
        private int currentLevel;
        private int levelCount;

        public Masher()
        {

            this.totalCount = 0;
            this.currentLevel = 0;
            this.levelCount = 0;

        }

        public void increment()
        {
            totalCount++;
            if (levelCount < 128) levelCount++; // For now just to limit click on level 0
        }

      
        public void reset()
        {
            totalCount = 0;
            levelCount = 0;
            currentLevel = 0;
        }
    }
}
