using System;
using System.Collections.Generic;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KnightsOfLaCampus.Source.Astar;

// Let it be Legacy Code.
internal sealed class AStarPathFinder
{
    private const int Inf = 99999999;
    private const int Int2 = 2;
    private readonly List<Spots> mViewable;

    private readonly List<List<Spots>> mMasterGrid;

    private readonly SoMuchOfSpots mOriginalGrid;

    private readonly Vector2 mStart, mTarget;

    internal AStarPathFinder(SoMuchOfSpots originalGrid, Vector2 start, Vector2 target)
    {


        mViewable = new List<Spots>();

        mMasterGrid = new List<List<Spots>>();

        mOriginalGrid = originalGrid;
        mStart = start;
        mTarget = target;


        for (var i = 0; i < originalGrid.mGrid.Count; i++)
        {

            mMasterGrid.Add(new List<Spots>());
            for (var j = 0; j < originalGrid.mGrid[0].Count; j++)
            {

                mMasterGrid[i].Add(new Spots(new Vector2(i, j), 
                    originalGrid.mGrid[i][j].mCost, Inf, originalGrid.mGrid[i][j].mIfFilled));

            }
        }
    }

    internal List<Vector2> GetPath => this.GenPath();

    private List<Vector2> GenPath()
    {

        mViewable.Add(mMasterGrid[(int)mStart.X][(int)mStart.Y]);

        while (mViewable.Count > 0 && !((int)mViewable[0].mPositionOfThisSpot.X == (int)mTarget.X && (int)mViewable[0].mPositionOfThisSpot.Y == (int)mTarget.Y))
        {
            CheckAroundSpot(mMasterGrid, mViewable);
        }


        var path = new List<Vector2>();

        if (mViewable.Count == 0)
        {
            return path;
        }
        var currentViewableStart = 0;
        var currentSpot = mViewable[currentViewableStart];

             
        while (currentSpot.mPositionOfThisSpot != mStart)
        { 
            var tempPos = mOriginalGrid.GetPosFromLocation(currentSpot.mPositionOfThisSpot) +
                          (mOriginalGrid.GridDims / Int2);
            var thisPos = mOriginalGrid.GetSpotsFromPixel(tempPos, Vector2.Zero);
            path.Add(new Vector2((thisPos.X * (int)mOriginalGrid.SpotDims.X) + (int)(mOriginalGrid.SpotDims.X / Int2), (thisPos.Y * (int)mOriginalGrid.SpotDims.Y) + (int)(mOriginalGrid.SpotDims.Y / Int2)));

            if ((int)currentSpot.mParentOfThisSpot.X != -1 && (int)currentSpot.mParentOfThisSpot.Y != -1)
            { 
                if ((int)currentSpot.mPositionOfThisSpot.X ==
                    (int)mMasterGrid[(int)currentSpot.mParentOfThisSpot.X][(int)currentSpot.mParentOfThisSpot.Y]
                        .mPositionOfThisSpot.X &&
                    (int)currentSpot.mPositionOfThisSpot.Y ==
                    (int)mMasterGrid[(int)currentSpot.mParentOfThisSpot.X][(int)currentSpot.mParentOfThisSpot.Y]
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

        path.Reverse();
        return path;
    }

    private static void CheckAroundSpot(System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<Spots>> masterGrid,
        System.Collections.Generic.IList<Spots> viewable)
    {
        Spots currentSpots;

        //Above
        if (Above(masterGrid, viewable))
        {
            currentSpots =
                masterGrid[(int)viewable[0].mPositionOfThisSpot.X][(int)viewable[0].mPositionOfThisSpot.Y - 1];
            SetAStarSpot(viewable, currentSpots, new Vector2(viewable[0].mPositionOfThisSpot.X, viewable[0].mPositionOfThisSpot.Y), viewable[0].mCurrentDistance, 1);
        }

        //Below
        if (Below(masterGrid, viewable))
        {
            currentSpots =
                masterGrid[(int)viewable[0].mPositionOfThisSpot.X][(int)viewable[0].mPositionOfThisSpot.Y + 1];
            SetAStarSpot(viewable, currentSpots, new Vector2(viewable[0].mPositionOfThisSpot.X, viewable[0].mPositionOfThisSpot.Y), viewable[0].mCurrentDistance, 1);
        }

        //Left
        if (Left(masterGrid, viewable))
        {
            currentSpots =
                masterGrid[(int)viewable[0].mPositionOfThisSpot.X - 1][(int)viewable[0].mPositionOfThisSpot.Y];
            SetAStarSpot(viewable, currentSpots, new Vector2(viewable[0].mPositionOfThisSpot.X, viewable[0].mPositionOfThisSpot.Y), viewable[0].mCurrentDistance, 1);
        }

        //Right
        if (Right(masterGrid, viewable))
        {
            currentSpots =
                masterGrid[(int)viewable[0].mPositionOfThisSpot.X + 1][(int)viewable[0].mPositionOfThisSpot.Y];
            SetAStarSpot(viewable, currentSpots, new Vector2(viewable[0].mPositionOfThisSpot.X, viewable[0].mPositionOfThisSpot.Y), viewable[0].mCurrentDistance, 1);
        }

        viewable[0].mHasBeenUsed = true;
        viewable.RemoveAt(0);
    }

    private static bool Above(
        System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<Spots>> masterGrid,
        System.Collections.Generic.IList<Spots> viewable)
    {
        return (viewable[0].mPositionOfThisSpot.Y > 0 && viewable[0].mPositionOfThisSpot.Y < masterGrid[0].Count &&
                !masterGrid[(int)viewable[0].mPositionOfThisSpot.X][(int)viewable[0].mPositionOfThisSpot.Y - 1]
                    .mIfFilled);
    }

    private static bool Below(
        System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<Spots>> masterGrid,
        System.Collections.Generic.IList<Spots> viewable)
    {
        return (viewable[0].mPositionOfThisSpot.Y >= 0 &&
                viewable[0].mPositionOfThisSpot.Y + 1 < masterGrid[0].Count &&
                !masterGrid[(int)viewable[0].mPositionOfThisSpot.X][(int)viewable[0].mPositionOfThisSpot.Y + 1]
                    .mIfFilled);
    }

    private static bool Left(
        System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<Spots>> masterGrid,
        System.Collections.Generic.IList<Spots> viewable)
    {
        return (viewable[0].mPositionOfThisSpot.X > 0 && viewable[0].mPositionOfThisSpot.X < masterGrid.Count &&
                !masterGrid[(int)viewable[0].mPositionOfThisSpot.X - 1][(int)viewable[0].mPositionOfThisSpot.Y]
                    .mIfFilled);
    }

    private static bool Right(
        System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<Spots>> masterGrid,
        System.Collections.Generic.IList<Spots> viewable)

    {
        return (viewable[0].mPositionOfThisSpot.X >= 0 &&
                viewable[0].mPositionOfThisSpot.X + 1 < masterGrid.Count &&
                !masterGrid[(int)viewable[0].mPositionOfThisSpot.X + 1][(int)viewable[0].mPositionOfThisSpot.Y]
                    .mIfFilled);
    }
        
            

    private static void SetAStarSpot(System.Collections.Generic.IList<Spots> viewable,
        Spots nextSpot,
        Vector2 nextParent,
        float d,
        float dist)
    {
        var addedDist = (nextSpot.mCost * dist);

        switch (nextSpot.mIsViewable)
        {
            case false when !nextSpot.mHasBeenUsed:
                nextSpot.SetSpot(nextParent, d, d + addedDist);
                nextSpot.mIsViewable = true;

                SetAStarSpotInsert(viewable, nextSpot);
                break;
            case true when d < nextSpot.mFscore:
                nextSpot.SetSpot(nextParent, d, d + addedDist);
                break;
        }
    }

    private static void SetAStarSpotInsert(System.Collections.Generic.IList<Spots> list, Spots newSpot)
    {
        var added = false;

        for (var i = 0; i < list.Count; i++)
        {
            if (list[i].mFscore <= newSpot.mFscore)
            {
                continue;
            }

            list.Insert(Math.Max(1, i), newSpot);
            added = true;
            break;
        }

        if (!added)
        {
            list.Add(newSpot);
        }
    }
}