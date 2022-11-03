using Monogame00.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame00.Models;
using Monogame00.Source.Engine;
using System.Reflection;
using Microsoft.VisualBasic;

namespace Monogame00.Source
{
    public class Player
    {
        public Sprite mPlayer;

        protected int mIndex;

        protected Vector2 mMoveTo;

        protected List<Vector2> mPathSpots = new List<Vector2>();

        protected Grid mPlayerGrid;

        public Player(Grid grid)
        {
            mPlayerGrid = grid;
            var animations = new Dictionary<string, Animation>()
            {
                { "up", new Animation(Globals.mContent.Load<Texture2D>("player/up"), 3) },
                { "down", new Animation(Globals.mContent.Load<Texture2D>("player/down"), 3) },
                { "left", new Animation(Globals.mContent.Load<Texture2D>("player/left"), 3) },
                { "right", new Animation(Globals.mContent.Load<Texture2D>("player/right"), 3) }
            };
            mPlayer = new Sprite(animations)
            {
                Position = new Vector2(100, 100),
                mInput = new Input()
                {
                    LeftKeys = Keys.A,
                    RightKeys = Keys.D,
                    UpKeys = Keys.W,
                    DownKeys = Keys.S

                }
            };
        }

        public void Update(GameTime gameTime)
        {
            if (Globals.mMouse.RightClick())
            {
                mMoveTo = new Vector2(Globals.mMouse.mNewMousePos.X, Globals.mMouse.mNewMousePos.Y);
                mIndex = 1;
                System.Diagnostics.Debug.WriteLine(mPlayerGrid.GetSpotsFromPixel(mMoveTo, Vector2.Zero));
                mPathSpots = FindPath(mPlayerGrid, mPlayerGrid.GetSpotsFromPixel(mMoveTo, Vector2.Zero));
                System.Diagnostics.Debug.WriteLine(mPathSpots.Count);
            }

            if (mPathSpots == null)
            {
                System.Diagnostics.Debug.WriteLine("noPathIsCreate");
            }
            
            if (mPathSpots != null && mIndex < mPathSpots.Count)
            {
                Vector2 tempVelocity = mPlayerGrid.GetSpotsFromPixel(mPathSpots[mIndex], Vector2.Zero) -
                                    mPlayerGrid.GetSpotsFromPixel(mPlayer.Position, Vector2.Zero);

                if (tempVelocity == Vector2.Zero && mIndex < mPathSpots.Count - 1)
                {
                    mPlayer.mVelocity = mPlayerGrid.GetSpotsFromPixel(mPathSpots[mIndex + 1], Vector2.Zero) -
                                        mPlayerGrid.GetSpotsFromPixel(mPlayer.Position, Vector2.Zero);
                }
                else
                {
                    mPlayer.mVelocity = tempVelocity;
                }

                System.Diagnostics.Debug.WriteLine("Player Position: " + mPlayer.Position);

                if (mPlayerGrid.GetSpotsFromPixel(mPlayer.Position, Vector2.Zero) != mPlayerGrid.GetSpotsFromPixel(mPathSpots[mIndex], Vector2.Zero))
                {
                    mPlayer.Position += mPlayer.mVelocity;
                }
                else
                {
                    mIndex++;
                }
            }
            mPlayer.Update(gameTime);
        }

        public virtual List<Vector2> FindPath(Grid grid, Vector2 target)
        {
            mPathSpots.Clear();

            Vector2 tempStartSpot = grid.GetSpotsFromPixel(mPlayer.Position, Vector2.Zero);

            List<Vector2> tempPath = grid.GetPathFromGrid(tempStartSpot, target, false);

            if (tempPath == null || tempPath.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("noPathIsCreate");
            }

            return tempPath;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            mPlayer.Draw(spriteBatch);
        }
    }
}
