using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Monogame00.Source.Engine;
using Monogame00.Source;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using System;

namespace Monogame00
{
    public class World
    {
        public Vector2 mOffSet;
        public Grid mGrid;
        public Player mPlayer;
        public Units mUnits, mUnit2S;
        public World()
        {
            GameGlobals.mCheckScroll = CheckScroll;

            mOffSet = new Vector2(0, 0);
            mGrid = new Grid(new Vector2(20, 20),
                new Vector2(0, 0),
                new Vector2(Globals.mScreenWidth + 200, Globals.mScreenHeight + 200));
            mPlayer = new Player(mGrid);
            mUnits = new Units(mGrid, mPlayer, new Vector2(100, 100), true);
            mUnit2S = new Units(mGrid, mPlayer, new Vector2(400, 600), true);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (mGrid != null) {
                mGrid.Update(mOffSet);
            }
            mUnits.Update(gameTime);
            mUnit2S.Update(gameTime);
            mPlayer.Update(gameTime);
        }

        public virtual void CheckScroll(object info)
        {
            Vector2 tempPos = (Vector2)info;

            if (tempPos.X < -mOffSet.X + (Globals.mScreenWidth* .4f))
            {
                mOffSet = new Vector2(mOffSet.X + mPlayer.mPlayer.mSpeed * 0.5f, mOffSet.Y);
            }

            if (tempPos.X > -mOffSet.X + (Globals.mScreenWidth * .6f))
            {
                mOffSet = new Vector2(mOffSet.X - mPlayer.mPlayer.mSpeed * 0.5f, mOffSet.Y);
            }

            if (tempPos.Y < -mOffSet.Y + (Globals.mScreenHeight * .4f))
            {
                mOffSet = new Vector2(mOffSet.X, mOffSet.Y + mPlayer.mPlayer.mSpeed * 0.5f);
            }

            if (tempPos.Y > -mOffSet.Y + (Globals.mScreenHeight * .6f))
            {
                mOffSet = new Vector2(mOffSet.X, mOffSet.Y - mPlayer.mPlayer.mSpeed * 0.5f);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            mGrid.DrawGrid(mOffSet);
            mUnits.Draw(spriteBatch);
            mUnit2S.Draw(spriteBatch);
            mPlayer.Draw(spriteBatch);
        }
    }
}
