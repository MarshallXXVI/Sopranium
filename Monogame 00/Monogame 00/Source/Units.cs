using Microsoft.Xna.Framework.Graphics;
using Monogame00.Source.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Monogame00.Models;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Monogame00.Sprites;
using Microsoft.Xna.Framework.Input;

namespace Monogame00.Source
{
    public class Units
    {
        public Vector2 mPosition;

        public Sprite mUnit;

        protected Player mTarget;

        protected int mIndex;

        protected List<Vector2> mPathSpots = new List<Vector2>();

        protected Grid mUnitGrid;

        public Units(Grid grid, Player target, Vector2 spawnPosition, bool ifAlive)
        {
            mUnitGrid = grid;
            mTarget = target;
            mIndex = 0;
            var animations = new Dictionary<string, Animation>()
            {
                { "up", new Animation(Globals.mContent.Load<Texture2D>("player/up"), 3) },
                { "down", new Animation(Globals.mContent.Load<Texture2D>("player/down"), 3) },
                { "left", new Animation(Globals.mContent.Load<Texture2D>("player/left"), 3) },
                { "right", new Animation(Globals.mContent.Load<Texture2D>("player/right"), 3) }
            };
            mUnit = new Sprite(animations)
            {
                Position = spawnPosition,
                mInput = new Input()
            };
            mPosition = mUnit.Position;
        }

        public void Update(GameTime gameTime)
        {
            // Learn How To build an AI.
            if (mUnit != null && mTarget != null)
            {
                Vector2 tempTargetPosition = new Vector2(mTarget.mPlayer.Position.X, mTarget.mPlayer.Position.Y);
                //System.Diagnostics.Debug.WriteLine("TargetPosition: " + mUnitGrid.GetSpotsFromPixel(tempTargetPosition, Vector2.Zero));
                mPathSpots = FindPath(mUnitGrid ,mUnitGrid.GetSpotsFromPixel(tempTargetPosition, Vector2.Zero));
                //if (tempTargetPosition != mUnitGrid.GetSpotsFromPixel(mTarget.mPlayer.Position, Vector2.Zero))
                //{
                //    mPathSpots = FindPath(mUnitGrid, mUnitGrid.GetSpotsFromPixel(tempTargetPosition, Vector2.Zero));
                //    mIndex = 0;
                //}
                //System.Diagnostics.Debug.WriteLine("Unit to Play Paths Count: " + mPathSpots.Count);
                //System.Diagnostics.Debug.WriteLine("TargetPosition: " + mUnitGrid.GetSpotsFromPixel(new Vector2(800 ,800), Vector2.Zero));
                if (mPathSpots != null && mIndex < mPathSpots.Count - 1)
                {
                    //System.Diagnostics.Debug.WriteLine("Path to Player" + mUnitGrid.GetSpotsFromPixel(mPathSpots[mIndex], Vector2.Zero));
                    //System.Diagnostics.Debug.WriteLine(mUnitGrid.GetSpotsFromPixel(mPathSpots[mIndex], Vector2.Zero));
                    //System.Diagnostics.Debug.WriteLine("Index: " + mIndex + " and " + mUnitGrid.GetSpotsFromPixel(mPathSpots[mIndex], Vector2.Zero));
                    Vector2 tempVelocity = new Vector2((int)((mPathSpots[mIndex].X - mPathSpots[mIndex +1 ].X) / 20),
                            (int)((mPathSpots[mIndex].Y - mPathSpots[mIndex + 1].Y) / 20));
                    
                    //System.Diagnostics.Debug.WriteLine(tempVelocity);
                    if (tempVelocity == Vector2.Zero && mIndex < mPathSpots.Count - 1)
                    {
                        mUnit.mVelocity = -0.7f *
                                          (mUnitGrid.GetSpotsFromPixel(mPathSpots[mIndex + 1], Vector2.Zero) -
                                           (mUnitGrid.GetSpotsFromPixel(mUnit.Position, Vector2.Zero)));
                    }
                    else
                    {
                        mUnit.mVelocity = -0.7f * tempVelocity;
                    }
                    //System.Diagnostics.Debug.WriteLine("Unit Position: " + mUnitGrid.GetSpotsFromPixel(mUnit.Position, Vector2.Zero));

                    if (mUnitGrid.GetSpotsFromPixel(mUnit.Position, Vector2.Zero) != mUnitGrid.GetSpotsFromPixel(mPathSpots[mIndex], Vector2.Zero))
                    {
                        mUnit.Position += mUnit.mVelocity;
                    }
                    else
                    {
                        mIndex++;
                    }
                }

                mUnit.Update(gameTime);
            }
        }

        public virtual List<Vector2> FindPath(Grid grid, Vector2 target)
        {
            mPathSpots.Clear();

            Vector2 tempStartSpot = grid.GetSpotsFromPixel(mUnit.Position, Vector2.Zero);

            List<Vector2> tempPath = grid.GetPathFromGrid(tempStartSpot, target, false);

            if (tempPath == null || tempPath.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("noPathIsCreate");
            }

            return tempPath;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mUnit.Draw(spriteBatch);
        }
    }
}
