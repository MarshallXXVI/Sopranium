using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame00.Models;

namespace Monogame00
{
    public class Grid
    {
        protected List<Vector2> mPath;

        public bool mShowGrid;

        public Vector2 mSpotDims, mGridDims, mPhysicalStartPos, mTotalPhysicalDims, mCurrentHoverSlot;

        public Basic2d mGridImage;

        public List<List<Spots>> mGrid = new List<List<Spots>>();

        public AStarPathFinder mPathFinder;
        public Grid(Vector2 spotdims, Vector2 startpos, Vector2 totaldims)
        {
            mShowGrid = false;
            mSpotDims = spotdims;
            mPhysicalStartPos = new Vector2((int)startpos.X, (int)startpos.Y);
            mTotalPhysicalDims = new Vector2((int)totaldims.X, (int)totaldims.Y);
            mCurrentHoverSlot = new Vector2(0, 0);

            mGridImage = new Basic2d("2D/shade",
                mSpotDims / 2,
                new Vector2(mSpotDims.X - 2, mSpotDims.Y - 2));
            SetGrid();
        }

        public virtual void Update(Vector2 offset)
        {
            mCurrentHoverSlot =
                GetSpotsFromPixel(new Vector2(Globals.mMouse.mNewMousePos.X, Globals.mMouse.mNewMousePos.Y), -offset);
        }

        public virtual Vector2 GetPosFromLocation(Vector2 loc)
        {
            return mPhysicalStartPos + new Vector2((int)loc.X * mSpotDims.X, (int)loc.Y * mSpotDims.Y);
        }

        public virtual Spots GetSpotsFromGrid(Vector2 loc)
        {
            if (loc.X >= 0 && loc.Y >= 0 && loc.X < mGrid.Count && loc.Y < mGrid[(int)loc.X].Count)
            {
                return mGrid[(int)loc.X][(int)loc.Y];
            }

            return null;
        }

        public virtual Vector2 GetSpotsFromPixel(Vector2 pixel, Vector2 offset)
        {
            Vector2 adjustedPos = pixel - mPhysicalStartPos + offset;
            Vector2 tempVector2 = new Vector2(Math.Min(Math.Max(0, (int)(adjustedPos.X / mSpotDims.X)), mGrid.Count - 1),
                                              Math.Min(Math.Max(0, (int)(adjustedPos.Y / mSpotDims.Y)), mGrid[0].Count - 1));
            return tempVector2;
        }

        public virtual void SetGrid()
        {
            mGridDims = new Vector2((int)(mTotalPhysicalDims.X / mSpotDims.X),
                (int)(mTotalPhysicalDims.Y / mSpotDims.Y));
            mGrid.Clear();

            for (int i = 0; i <= mGridDims.X; i++)
            {
                mGrid.Add(new List<Spots>());

                for (int j = 0; j <= mGridDims.Y; j++)
                {
                    mGrid[i].Add(new Spots(i, j));
                }
            }
        }

        public List<Vector2> GetPathFromGrid(Vector2 start, Vector2 target, bool ifDiagonal)
        {
            mPathFinder = new AStarPathFinder(this, start, target, ifDiagonal);
            mPath = mPathFinder.GetPath();
            //System.Diagnostics.Debug.WriteLine("StarPathFinderCreate:" + mPath.Count);
            return mPath;
        }

        public virtual void DrawGrid(Vector2 offset)
        {
            if (mShowGrid)
            {
                Vector2 topLeft = GetSpotsFromPixel(new Vector2(0, 0), Vector2.Zero);
                Vector2 botRight =
                    GetSpotsFromPixel(new Vector2(Globals.mScreenWidth, Globals.mScreenHeight), Vector2.Zero);

                //Globals.mEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                //Globals.mEffect.CurrentTechnique.Passes[0].Apply();

                for (int j = (int)topLeft.X; j < botRight.X && j <= mGrid.Count; j++)
                {
                    for (int k = (int)topLeft.Y; k < botRight.Y && k <= mGrid[0].Count; k++)
                    {
                        if (mCurrentHoverSlot.X == j && mCurrentHoverSlot.Y == k)
                        {
                            //mGridImage.Draw(offset + mPhysicalStartPos + new Vector2(j * mSpotDims.X, k * mSpotDims.Y));
                            //Globals.mEffect.Parameters["filterColor"].SetValue(Color.Red.ToVector4());
                            //Globals.mEffect.CurrentTechnique.Passes[0].Apply();
                            continue;
                        }
                        else
                        {
                            mGridImage.Draw(offset + mPhysicalStartPos + new Vector2(j * mSpotDims.X, k * mSpotDims.Y));
                            //Globals.mEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                            //Globals.mEffect.CurrentTechnique.Passes[0].Apply();
                        }

                        //mGridImage.Draw(offset + mPhysicalStartPos + new Vector2(j * mSpotDims.X, k * mSpotDims.Y));
                    }
                }

            }
        }
    }
}
