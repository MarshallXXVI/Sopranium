using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using KnightsOfLaCampus.Source.Astar;
using KnightsOfLaCampus.Source.Interfaces;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework.Audio;

namespace KnightsOfLaCampus.Source;

internal sealed class Player
{
    // will be later implement.
    // first goal is allocate new Sprite which is King and trying to Manipulate him by MouseClick. (done)
    // check which character is being control now King himself or his army.
    // (Only king is done) his army can inherit from him;

    // after that trying to control multiple target according to GDD.

    // Coordinate that King will be Spawn.

    private const int X = (Globals.ScreenWidth / 2) + 15;
    private const int Y = (Globals.ScreenHeight / 2) + 15;

    public readonly King mKing;

    private readonly Knight mKnight;

    private readonly SoMuchOfSpots mPlayerField;
    private readonly List<IFriendlyUnit> mMyFriendlyUnits = new List<IFriendlyUnit>();
    private readonly List<IFriendlyUnit> mNowControlUnit = new List<IFriendlyUnit>();
    private readonly SoundEffect mHitEffect;
    private readonly Basic2D mArrow;

    internal Player(SoMuchOfSpots grid)
    {
        mPlayerField = grid;
        mHitEffect = Globals.Content.Load<SoundEffect>("Stuffs\\Logo_hit");
        mArrow = new Basic2D("Stuffs\\goThereArrow",
            new Vector2(16, 16),
            new Vector2(32, 32));
        mKing = new King(){mVelocity = Vector2.Zero, Position = new Vector2(X + 32, Y + 32)};

        // now Knight share same texture as King don't be confuse.
        mKnight = new Knight() { mVelocity = Vector2.Zero, Position = new Vector2(X + 64, Y + 32) };
        // test add some Friend to MyFriendlyUnits List. King is always ours Friend.
        mMyFriendlyUnits.Add(mKing);
        mMyFriendlyUnits.Add(mKnight);
    }

    internal void Update(GameTime gameTime)
    {
        FriendUpdate();
        mKnight.Update(gameTime);
        mKing.Update(gameTime);
    }

    // Update always friend if our friend has flag true. we push him to our stack.
    // if none of them has flag true.
    // we clear our stack. this stack hold maximum 1 element.
    private void FriendUpdate()
    {
        foreach (var friend in mMyFriendlyUnits)
        {
            if (friend.GetIfSelected() && mNowControlUnit.Count == 0)
            {
                mNowControlUnit.Add(friend);
            }
            else if(friend.GetIfSelected() && mNowControlUnit.Contains(friend))
            {
                GivePathToUnit(mNowControlUnit[0], mPlayerField);
            }
            else if (!friend.GetIfSelected() && mNowControlUnit.Contains(friend))
            {
                mNowControlUnit.Remove(friend);
            }
        }
    }

    // This Function always check currently Unit selected. If Player have no Unit in Control play HitEffect.
    private void GivePathToUnit(IFriendlyUnit unit, SoMuchOfSpots grid)
    {
        if (Globals.Mouse.RightClick())
        {
            //unit.Path.Clear();
            var moveTo = new Vector2(Globals.Mouse.mNewMousePos.X, Globals.Mouse.mNewMousePos.Y);
            // product from PathFinding should come after this.
            unit.Path = FindPath(unit.Position, grid.GetSpotsFromPixel(moveTo, Vector2.Zero));
        }
        else if (Globals.Mouse.RightClick() && mNowControlUnit.Count == 0)
        {
            mHitEffect.Play();
        }
    }

    //FindPath function.
    private List<Vector2> FindPath(Vector2 currentPos, Vector2 target)
    {
        var tempStartSpot = mPlayerField.GetSpotsFromPixel(currentPos, Vector2.Zero);

        var tempPath = mPlayerField.GetPathFromGrid(tempStartSpot, target);

        if (tempPath == null || tempPath.Count == 0)
        {
            System.Diagnostics.Debug.WriteLine("noPathIsCreate");
        }

        return tempPath;
    }

    // just draw an Arrow if there is some character in Control not thing fancy.
    private void DrawArrow()
    {
        var topLeft = mPlayerField.GetSpotsFromPixel(new Vector2(0, 0), Vector2.Zero);
        var botRight =
            mPlayerField.GetSpotsFromPixel(new Vector2(Globals.ScreenWidth, Globals.ScreenHeight), Vector2.Zero);

        for (var j = (int)topLeft.X; j < botRight.X && j <= mPlayerField.mGrid.Count; j++)
        {
            for (var k = (int)topLeft.Y; k < botRight.Y && k <= mPlayerField.mGrid[0].Count; k++)
            {
                if (((int)mPlayerField.mCurrentHoverSlot.X == j && (int)mPlayerField.mCurrentHoverSlot.Y == k) && !mPlayerField.mGrid[j][k].mIfFilled && mNowControlUnit.Count == 1)
                {
                    mArrow.Draw(new Vector2(j * 32, k * 32));
                }
            }
        }

    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        DrawArrow();
        mKnight.Draw(spriteBatch);
        mKing.Draw(spriteBatch);
    }
}