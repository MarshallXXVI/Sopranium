using System.Globalization;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KnightsOfLaCampus.Source.Astar
{
    internal sealed class Spots
    {
        #region AStarPathFinder
        public float mFscore;
        public float mCurrentDistance;
        public readonly float mCost;
        public bool mHasBeenUsed, mIsViewable;
        public Vector2 mPositionOfThisSpot, mParentOfThisSpot;
        #endregion

        public bool mIfFilled;

        public Spots(Vector2 pos, float cost, float fscore, bool filled)
        {
            mHasBeenUsed = false;
            mIsViewable = false;
            mPositionOfThisSpot = pos;
            mCost = cost;
            mFscore = fscore;
            mIfFilled = filled;
        }

        public void SetSpot(Vector2 parent, float fscore, float currentDis)
        {
            mParentOfThisSpot = parent;
            mCurrentDistance = currentDis;
            mFscore = fscore;
        }
    }
}
