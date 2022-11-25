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
    public class McTimer
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
            get { return mSec; }
            set { mSec = value; }
        }
        public int Timer
        {
            get { return (int)mTimer.TotalMilliseconds; }
        }



        public void UpdateTimer(GameTime gameTime)
        {
            mTimer += gameTime.ElapsedGameTime;
        }

        public void UpdateTimer(float speed, GameTime gameTime)
        {
            mTimer += TimeSpan.FromTicks((long)(gameTime.ElapsedGameTime.Ticks * speed));
        }

        public virtual void AddToTimer(int msec)
        {
            mTimer += TimeSpan.FromMilliseconds((long)(msec));
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

        public void Reset()
        {
            mTimer = mTimer.Subtract(new TimeSpan(0, 0, mSec / 60000, mSec / 1000, mSec % 1000));
            if (mTimer.TotalMilliseconds < 0)
            {
                mTimer = TimeSpan.Zero;
            }
            mGoodToGo = false;
        }

        public void Reset(int newtimer)
        {
            mTimer = TimeSpan.Zero;
            MSec = newtimer;
            mGoodToGo = false;
        }

        public void ResetToZero()
        {
            mTimer = TimeSpan.Zero;
            mGoodToGo = false;
        }

        public virtual XElement ReturnXml()
        {
            XElement xml = new XElement("Timer",
                                    new XElement("mSec", mSec),
                                    new XElement("timer", Timer));



            return xml;
        }

        public void SetTimer(TimeSpan time)
        {
            mTimer = time;
        }

        public virtual void SetTimer(int msec)
        {
            mTimer = TimeSpan.FromMilliseconds((long)(msec));
        }
    }
}
