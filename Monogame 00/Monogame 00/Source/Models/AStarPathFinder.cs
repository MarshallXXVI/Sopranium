using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Monogame00.Models
{
    public class AStarPathFinder
    {
        private List<Spots> mViewable;

        private List<Spots> mUsed;

        private List<List<Spots>> mMasterGrid;

        private Grid mOriginalGrid;

        private Vector2 mStart, mTarget;

        private bool mIfDiagonal;
        public AStarPathFinder(Grid originalGrid, Vector2 start, Vector2 target, bool ifDiagonal)
        {
            mViewable = new List<Spots>();

            mMasterGrid = new List<List<Spots>>();

            mUsed = new List<Spots>();

            mIfDiagonal = ifDiagonal;
            mOriginalGrid = originalGrid;

            mStart = start;
            mTarget = target;

            bool filled = false;

            float cost = 1;

            for (int i = 0; i < originalGrid.mGrid.Count; i++)
            {
                mMasterGrid.Add(new List<Spots>());
                for (int j = 0; j < originalGrid.mGrid[0].Count; j++)
                {
                    filled = originalGrid.mGrid[i][j].mIfFilled;

                    if (originalGrid.mGrid[i][j].mIfFilled)
                    {
                        filled = true;
                    }

                    cost = originalGrid.mGrid[i][j].mCost;

                    mMasterGrid[i].Add(new Spots(new Vector2(i, j), cost, 99999999, filled));

                }
            }
        }

        public List<Vector2> GetPath()
        {
            System.Diagnostics.Debug.WriteLine(mMasterGrid.Count * mMasterGrid[0].Count);
            mViewable.Add(mMasterGrid[(int)mStart.X][(int)mStart.Y]);

            while (mViewable.Count > 0 && !(mViewable[0].mPositionOfThisSpot.X == mTarget.X && mViewable[0].mPositionOfThisSpot.Y == mTarget.Y))
            {
                CheckAroundSpot(mMasterGrid, mViewable, mUsed, mTarget, mIfDiagonal);
            }

            

            List<Vector2> path = new List<Vector2>();
            System.Diagnostics.Debug.WriteLine("mViewable: " + mViewable.Count);

            if (mViewable.Count > 0)
            {
                int currentViewableStart = 0;
                Spots currentSpot = mViewable[currentViewableStart];

                path.Clear();
                Vector2 tempPos;

                while (true)
                {
                    tempPos = mOriginalGrid.GetPosFromLocation(currentSpot.mPositionOfThisSpot) +
                              (mOriginalGrid.mGridDims / 2);
                    path.Add(new Vector2(tempPos.X, tempPos.Y));

                    if (currentSpot.mPositionOfThisSpot == mStart)
                    {
                        break;
                    }
                    else
                    {
                        if ((int)currentSpot.mParentOfThisSpot.X != -1 && (int)currentSpot.mParentOfThisSpot.Y != -1)
                        {
                            if (currentSpot.mPositionOfThisSpot.X ==
                                mMasterGrid[(int)currentSpot.mParentOfThisSpot.X][(int)currentSpot.mParentOfThisSpot.Y]
                                    .mPositionOfThisSpot.X &&
                                currentSpot.mPositionOfThisSpot.Y ==
                                mMasterGrid[(int)currentSpot.mParentOfThisSpot.X][(int)currentSpot.mParentOfThisSpot.Y]
                                    .mPositionOfThisSpot.Y)
                            {
                                currentSpot = mViewable[currentViewableStart];
                                currentViewableStart++;
                            }

                            currentSpot =
                                mMasterGrid[(int)currentSpot.mParentOfThisSpot.X][(int)currentSpot.mParentOfThisSpot.Y];
                        }
                        else
                        {
                            currentSpot = mViewable[currentViewableStart];
                            currentViewableStart++;
                        }

                    }
                }

                path.Reverse();
            }
            System.Diagnostics.Debug.WriteLine("FinalPathCount: " + path.Count);
            return path;
        }

        private void CheckAroundSpot(List<List<Spots>> mastergrid, List<Spots> viewable, List<Spots> use, Vector2 target, bool ifDiagonal)
        {
            Spots currentSpots;
            bool up = true, down = true, left = true, right = true;

            //Above
            if (viewable[0].mPositionOfThisSpot.Y > 0 && viewable[0].mPositionOfThisSpot.Y < mastergrid[0].Count &&
                !mastergrid[(int)viewable[0].mPositionOfThisSpot.X][(int)viewable[0].mPositionOfThisSpot.Y - 1].mIfFilled)
            {
                currentSpots =
                    mastergrid[(int)viewable[0].mPositionOfThisSpot.X][(int)viewable[0].mPositionOfThisSpot.Y - 1];
                up = currentSpots.mIfFilled;
                SetAStarSpot(viewable, use, currentSpots, new Vector2(viewable[0].mPositionOfThisSpot.X, viewable[0].mPositionOfThisSpot.Y), viewable[0].mCurrentDistance, target, 1);
            }

            //Below
            if (viewable[0].mPositionOfThisSpot.Y >= 0 && viewable[0].mPositionOfThisSpot.Y + 1 < mastergrid[0].Count &&
                !mastergrid[(int)viewable[0].mPositionOfThisSpot.X][(int)viewable[0].mPositionOfThisSpot.Y + 1].mIfFilled)
            {
                currentSpots =
                    mastergrid[(int)viewable[0].mPositionOfThisSpot.X][(int)viewable[0].mPositionOfThisSpot.Y + 1];
                down = currentSpots.mIfFilled;
                SetAStarSpot(viewable, use, currentSpots, new Vector2(viewable[0].mPositionOfThisSpot.X, viewable[0].mPositionOfThisSpot.Y), viewable[0].mCurrentDistance, target, 1);
            }

            //Left
            if (viewable[0].mPositionOfThisSpot.X > 0 && viewable[0].mPositionOfThisSpot.X < mastergrid.Count &&
                !mastergrid[(int)viewable[0].mPositionOfThisSpot.X - 1][(int)viewable[0].mPositionOfThisSpot.Y].mIfFilled)
            {
                currentSpots =
                    mastergrid[(int)viewable[0].mPositionOfThisSpot.X - 1][(int)viewable[0].mPositionOfThisSpot.Y];
                left = currentSpots.mIfFilled;
                SetAStarSpot(viewable, use, currentSpots, new Vector2(viewable[0].mPositionOfThisSpot.X, viewable[0].mPositionOfThisSpot.Y), viewable[0].mCurrentDistance, target, 1);
            }

            //Right
            if (viewable[0].mPositionOfThisSpot.X >= 0 && viewable[0].mPositionOfThisSpot.X + 1 < mastergrid.Count &&
                !mastergrid[(int)viewable[0].mPositionOfThisSpot.X + 1][(int)viewable[0].mPositionOfThisSpot.Y].mIfFilled)
            {
                currentSpots =
                    mastergrid[(int)viewable[0].mPositionOfThisSpot.X + 1][(int)viewable[0].mPositionOfThisSpot.Y];
                right = currentSpots.mIfFilled;
                SetAStarSpot(viewable, use, currentSpots, new Vector2(viewable[0].mPositionOfThisSpot.X, viewable[0].mPositionOfThisSpot.Y), viewable[0].mCurrentDistance, target, 1);
            }

            viewable[0].mHasBeenUsed = true;
            mUsed.Add(viewable[0]);
            viewable.RemoveAt(0);
        }

        private void SetAStarSpot(List<Spots> viewable,
            List<Spots> used,
            Spots nextSpot,
            Vector2 nextParent,
            float d,
            Vector2 target,
            float dist)
        {
            float f = d;
            float addedDist = (nextSpot.mCost * dist);

            if (!nextSpot.mIsViewable && !nextSpot.mHasBeenUsed)
            {
                nextSpot.SetGrid(nextParent, f, d + addedDist);
                nextSpot.mIsViewable = true;

                SetAStarSpotInsert(viewable, nextSpot);
            }

            else if(nextSpot.mIsViewable)
            {
                if (f < nextSpot.mFscore)
                {
                    nextSpot.SetGrid(nextParent, f, d + addedDist);
                }
            }
        }

        private void SetAStarSpotInsert(List<Spots> list, Spots newspot)
        {
            bool added = false;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].mFscore > newspot.mFscore)
                {
                    list.Insert(Math.Max(1, i), newspot);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                list.Add(newspot);
            }
        }
    }
}
