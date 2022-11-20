using System;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using KnightsOfLaCampus.Source.Astar;
using KnightsOfLaCampus.Source.Interfaces;
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
    private List<Vector2> mPathSpots = new List<Vector2>();
    private readonly List<IFriendlyUnit> mMyFriendlyUnits = new List<IFriendlyUnit>();
    private readonly List<IFriendlyUnit> mNowControlUnit = new List<IFriendlyUnit>();
    private readonly SoundEffect mHitEffect;

    internal Player(SoMuchOfSpots grid)
    {
        mPlayerField = grid;
        mHitEffect = Globals.Content.Load<SoundEffect>("Logo_hit");
        mKing = new King(){mVelocity = Vector2.Zero, Position = new Vector2(X, Y)};

        mKnight = new Knight() { mVelocity = Vector2.Zero, Position = new Vector2(X + 32, Y) };
        mMyFriendlyUnits.Add(mKing);

        mMyFriendlyUnits.Add(mKnight);
    }

    internal void Update(GameTime gameTime)
    {
        foreach (var friend in mMyFriendlyUnits)
        {
            CheckWhichUnitIsSelected(friend, mPlayerField);

            if (friend.GetIfSelected())
            {
                mNowControlUnit.Clear();
                mNowControlUnit.Add(friend);
                CheckIfClickToMove(mNowControlUnit[0], gameTime);
            }
        }

        mKnight.Update(gameTime);
        mKing.Update(gameTime);
    }

    private void CheckWhichUnitIsSelected(IFriendlyUnit unit, SoMuchOfSpots grid)
    {
        if (Globals.Mouse.RightClick() && unit.GetIfSelected())
        {
            var moveTo = new Vector2(Globals.Mouse.mNewMousePos.X, Globals.Mouse.mNewMousePos.Y);
            // product from PathFinding should come after this.
            mPathSpots = FindPath(unit.Position, grid.GetSpotsFromPixel(moveTo, Vector2.Zero));
            unit.Path = mPathSpots;
        }
        else if (Globals.Mouse.RightClick() && mNowControlUnit.Count == 0)
        {
            mHitEffect.Play();
        }
    }

    //This Function always update if player ClickToMove a character somewhere.
    //If Click we create new Path and delete old Path.
    private void CheckIfClickToMove(IFriendlyUnit unit, GameTime gameTime)
    {
        //these are steps how we moving ours Sprite.
        if (mPathSpots == null)
        {
            System.Diagnostics.Debug.WriteLine("noPathIsCreate");
        }

        //Check if ours path is created and ours Sprite is not finished moving to Destination.
        if (unit.Path == null || 0 > unit.Path.Count - 1)
        {
            return;
        }

        if (MoveTowardsSpot(unit, unit.Path[0], gameTime))
        {
            unit.Path.RemoveAt(0);
        }
    }

    // This Function return true if Sprite move to next Node and hit the target.
    // KeyWord PathFollowing.
    private static bool MoveTowardsSpot(IFriendlyUnit unit, Vector2 target, GameTime gameTime)
    {
        // if we hit ours next destination return true;
        if (unit.Position == target)
        {
            return true;
        }

        // if not we get next direction and add to ours Sprite Position.
        var tempVector2 = Vector2.Normalize(target - unit.Position);
        unit.Velocity = tempVector2;
        unit.Position += unit.Velocity * 0.15f * gameTime.ElapsedGameTime.Milliseconds;

        // if ours Sprite move pass target.
        // we set ours Sprite position to that targetPosition.
        if (Math.Abs(Vector2.Dot(unit.Velocity,
                Vector2.Normalize(target - unit.Position)) + 1) < 0.1f)
        {
            unit.Position = target;
        }
        // return true or false;
        return unit.Position == target;
    }

    //old_FindPath function.
    private List<Vector2> FindPath(Vector2 currentPos, Vector2 target)
    {
        mPathSpots.Clear();

        var tempStartSpot = mPlayerField.GetSpotsFromPixel(currentPos, Vector2.Zero);

        var tempPath = mPlayerField.GetPathFromGrid(tempStartSpot, target);

        if (tempPath == null || tempPath.Count == 0)
        {
            System.Diagnostics.Debug.WriteLine("noPathIsCreate");
        }

        return tempPath;
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        mKnight.Draw(spriteBatch);
        mKing.Draw(spriteBatch);
    }
}