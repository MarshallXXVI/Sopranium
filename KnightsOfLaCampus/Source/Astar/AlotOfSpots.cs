using System;
using System.Collections.Generic;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KnightsOfLaCampus.Source.Astar

{
    // this class contain AStar Algorithm, CollisionBackground, Object and etc.
    internal sealed class AlotOfSpots
    {
        private const int Int2 = 2;

        // Allocate memory for ours Grid which inside Grid contain Spots.
        // each Spots contain in formation of that Gird[i][k].
        // call Spots.cs
        internal readonly List<List<Spots>> mGrid = new List<List<Spots>>();

        private bool mShowGrid;

        private Vector2 mGridDims, mCurrentHoverSlot, mSpotDims;

        private readonly Vector2 mPhysicalStartPos, mTotalPhysicalDims;

        private readonly Basic2D mGridImage;

        internal Vector2 GridDims => mGridDims;

        internal Vector2 SpotDims => mSpotDims;

        /// <summary>
        /// Constructor of Grid
        /// </summary>
        internal AlotOfSpots(Vector2 spotDims, Vector2 startPos, Vector2 totalDims)
        {
            mShowGrid = false;
            mSpotDims = spotDims;
            mPhysicalStartPos = new Vector2((int)startPos.X, (int)startPos.Y);
            mTotalPhysicalDims = new Vector2((int)totalDims.X, (int)totalDims.Y);
            mCurrentHoverSlot = new Vector2(0, 0);

            mGridImage = new Basic2D("2D/Black",
                mSpotDims / Int2,
                new Vector2(mSpotDims.X -Int2, mSpotDims.Y - Int2));
            SetGrid();
        }

        // if update will calculate where mouse is Hovering on screen.
        internal void Update(Vector2 offset)
        {
            mCurrentHoverSlot =
                GetSpotsFromPixel(new Vector2(Globals.Mouse.mNewMousePos.X, Globals.Mouse.mNewMousePos.Y), -offset);

            // Toggle grid is on and LeftMouseClick can mark that tile as Filled aka unwalkable.
            if (!mShowGrid)
            {
                return;
            }

            if (!Globals.Mouse.LeftClick() && !Globals.Mouse.LeftClickHold())
            {
                return;
            }

            var markThisSpot = GetSpotsFromPixel(new Vector2(Globals.Mouse.mNewMousePos.X, Globals.Mouse.mNewMousePos.Y), -offset);
            //System.Diagnostics.Debug.WriteLine(markThisSpot);
            mGrid[(int)markThisSpot.X][(int)markThisSpot.Y].mIfFilled = true;
        }

        internal Vector2 GetPosFromLocation(Vector2 loc)
        {
            return mPhysicalStartPos + new Vector2((int)loc.X * mSpotDims.X, (int)loc.Y * mSpotDims.Y);
        }


        // this function will return which i or j in Gird[i][j] from Screen.
        internal Vector2 GetSpotsFromPixel(Vector2 pixel, Vector2 offset)
        {
            var adjustedPos = pixel - mPhysicalStartPos + offset;
            var tempVector2 = new Vector2(Math.Min(Math.Max(0, (int)(adjustedPos.X / mSpotDims.X)), mGrid.Count - 1),
                                              Math.Min(Math.Max(0, (int)(adjustedPos.Y / mSpotDims.Y)), mGrid[0].Count - 1));
            return tempVector2;
        }

        // Generate Grid that contain Spots with 0 information.
        private void SetGrid()
        {
            mGridDims = new Vector2((int)(mTotalPhysicalDims.X / mSpotDims.X),
                (int)(mTotalPhysicalDims.Y / mSpotDims.Y));
            mGrid.Clear();

            for (var i = 0; i <= mGridDims.X; i++)
            {
                mGrid.Add(new List<Spots>());

                for (var j = 0; j <= mGridDims.Y; j++)
                {
                    mGrid[i].Add(new Spots(new Vector2(i, j), 0, 0, false));
                }
            }
        }

        //Generate a shortest path from Position (Vector2)start to (Vector2)target.
        //Call AStarPathFinder.cs
        //And return List<Vector2>.
        internal List<Vector2> GetPathFromGrid(Vector2 start, Vector2 target)
        {
            var pathFinder = new AStarPathFinder(this, start, target);
            var path = pathFinder.GetPath;
            return path;
        }

        // DrawGrid of Screen can be disable on enable for debugging.
        internal void DrawGrid(Vector2 offset)
        {
            if (!mShowGrid) {return;}
            var topLeft = GetSpotsFromPixel(new Vector2(0, 0), Vector2.Zero);
            var botRight =
                GetSpotsFromPixel(new Vector2(Globals.ScreenWidth, Globals.ScreenHeight), Vector2.Zero);

            for (var j = (int)topLeft.X; j < botRight.X && j <= mGrid.Count; j++)
            {
                for (var k = (int)topLeft.Y; k < botRight.Y && k <= mGrid[0].Count; k++)
                {
                    if (!((int)mCurrentHoverSlot.X == j && (int)mCurrentHoverSlot.Y == k) && !mGrid[j][k].mIfFilled)
                    {
                        mGridImage.Draw(offset + mPhysicalStartPos + new Vector2(j * mSpotDims.X, k * mSpotDims.Y));
                    }
                }
            }
        }
    }
}

