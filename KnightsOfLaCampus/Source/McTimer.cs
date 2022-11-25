using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using KnightsOfLaCampus.Source;

namespace KnightsOfLaCampus
{
    internal class McTimer
    {
        public bool mGoodToGo;
        protected int mSec;
        protected TimeSpan mTimer = new TimeSpan();


        public McTimer(int m)
        {
            mGoodToGo = false;
            mSec = m;
        }
        public McTimer(int m, bool startloaded)
        {
            mGoodToGo = startloaded;
            mSec = m;
        }

        public int MSec
        {
            get => mSec;
            set => mSec = value;
        }
        public int Timer => (int)mTimer.TotalMilliseconds;


        public void UpdateTimer(GameTime gameTime)
        {
            mTimer += gameTime.ElapsedGameTime;
        }


        public bool Test()
        {
            if (mTimer.TotalMilliseconds >= mSec || mGoodToGo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ResetToZero()
        {
            mTimer = TimeSpan.Zero;
            mGoodToGo = false;
        }
    }
}
