using System;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using KnightsOfLaCampus.Source.Astar;
using KnightsOfLaCampus.Source.GridNew;
using Microsoft.Xna.Framework.Audio;

namespace KnightsOfLaCampus.Source
{
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

        private readonly King mKing;

        private readonly List<AllyUnit> mAlliedUnits = new List<AllyUnit>();

        private readonly AlotOfSpots mPlayerField;
        private List<Vector2> mPathSpots = new List<Vector2>();
        private readonly SoundEffect mHitEffect;

        internal Player(Grid grid)
        {
            mPlayerField = grid.mAlotOfSpots;
            mHitEffect = Globals.Content.Load<SoundEffect>("Logo_hit");
            mKing = new King(){mVelocity = Vector2.Zero, Position = new Vector2(X, Y)};

            // Adding one allied Unit for testing purposes
            mAlliedUnits.Add(new Swordsman() { mVelocity = Vector2.Zero, Position = new Vector2(30, 30) });
        }

        internal void Update(GameTime gameTime)
        {
            // CheckIfPlayerClick on Character now we only have King.
            CheckIfClickToMove(mPlayerField, mKing, gameTime);
            mKing.Update(gameTime);

            for (var i = 0; i < mAlliedUnits.Count; i++)
            {
                mAlliedUnits[i].Update(gameTime);
            }
        }


        //This Function always update if player ClickToMove a character somewhere.
        //If Click we create new Path and delete old Path.
        private void CheckIfClickToMove(AlotOfSpots grid, King king, GameTime gameTime)
        {
            if (Globals.Mouse.RightClick() && king.mIfSelected)
            {
                var moveTo = new Vector2(Globals.Mouse.mNewMousePos.X, Globals.Mouse.mNewMousePos.Y);
                // product from PathFinding should come after this.
                // Old_mPathSpots = FindPath(king.Position, grid.GetSpotsFromPixel(moveTo, Vector2.Zero));
            }
            else if(Globals.Mouse.RightClick() && !king.mIfSelected)
            {
                mHitEffect.Play();
            }

            //these are steps how we moving ours Sprite.
            if (mPathSpots == null)
            {
                System.Diagnostics.Debug.WriteLine("noPathIsCreate");
            }

            //Check if ours path is created and ours Sprite is not finished moving to Destination.
            if (mPathSpots == null || 0 > mPathSpots.Count - 1)
            {
                return;
            }

            if (MoveTowardsSpot(king, mPathSpots[0], gameTime))
            {
                mPathSpots.RemoveAt(0);
            }
        }

        // This Function return true if Sprite move to next Node and hit the target.
        // KeyWord PathFollowing.
        private static bool MoveTowardsSpot(King king, Vector2 target, GameTime gameTime)
        {
            // if we hit ours next destination return true;
            if (king.Position == target)
            {
                return true;
            }

            // if not we get next direction and add to ours Sprite Position.
            var tempVector2 = Vector2.Normalize(target - king.Position);
            king.mVelocity = tempVector2;
            king.Position += king.mVelocity * 0.15f * gameTime.ElapsedGameTime.Milliseconds;

            // if ours Sprite move pass target.
            // we set ours Sprite position to that targetPosition.
            if (Math.Abs(Vector2.Dot(king.mVelocity,
                    Vector2.Normalize(target - king.Position)) + 1) < 0.1f)
            {
                king.Position = target;
            }
            // return true or false;
            return king.Position == target;
        }

        //old_FindPath function.
        //private List<Vector2> FindPath( Vector2 currentPos, Vector2 target)
        //{
        //    mPathSpots.Clear();

        //    var tempStartSpot = mPlayerField.GetSpotsFromPixel(currentPos, Vector2.Zero);

        //    var tempPath = mPlayerField.GetPathFromGrid(tempStartSpot, target);

        //    if (tempPath == null || tempPath.Count == 0)
        //    {
        //        System.Diagnostics.Debug.WriteLine("noPathIsCreate");
        //    }

        //    return tempPath;
        //}

        internal void Draw(SpriteBatch spriteBatch)
        {
            mKing.Draw(spriteBatch);

            for (int i = 0; i < mAlliedUnits.Count; i++)
            {
                mAlliedUnits[i].Draw(spriteBatch);
            }
        }
    }
}
