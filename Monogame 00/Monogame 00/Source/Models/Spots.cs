using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame00.Models
{
    public class Spots
    {
        public float mFscore = 0;
        public float mCost, mCurrentDistance;
        public bool mIfFilled, mHasBeenUsed, mIsViewable;
        public Vector2 mPositionOfThisSpot, mParentOfThisSpot;

        public Spots(int i, int j)
        {
            mPositionOfThisSpot.X = i;
            mPositionOfThisSpot.Y = j;
        }

        public Spots(Vector2 pos, float cost, float fscore, bool filled)
        {
            mHasBeenUsed = false;
            mIsViewable = false;
            mPositionOfThisSpot = pos;
            mCost = cost;
            mFscore = fscore;
            mIfFilled = filled;
        }

        public void SetGrid(Vector2 parent, float fscore, float currentDis)
        {
            mParentOfThisSpot = parent;
            mCurrentDistance = currentDis;
            mFscore = fscore;
        }
    }
}
