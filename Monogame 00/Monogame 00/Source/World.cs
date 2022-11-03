using Microsoft.Xna.Framework.Graphics;
using Monogame00.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Monogame00.Sprites;
using Microsoft.Xna.Framework.Input;
using Monogame00.Source.Engine;
using Microsoft.Xna.Framework.Content;
using Monogame00;
using Monogame00.Source;
using Monogame00.Sprites;

namespace Monogame00
{
    public class World
    {
        public Microsoft.Xna.Framework.Vector2 mOffSet;
        public Grid mGrid;
        public Player mPlayer;
        public World()
        {
            mOffSet = new Vector2(0, 0);
            mGrid = new Grid(new System.Numerics.Vector2(20, 20),
                new System.Numerics.Vector2(0, 0),
                new System.Numerics.Vector2(Globals.mScreenWidth, Globals.mScreenHeight));
            mPlayer = new Player(mGrid);
        }

        public virtual void Update(GameTime gameTime)
        {
            mGrid.Update(mOffSet);
            mPlayer.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            mGrid.DrawGrid(mOffSet);
            mPlayer.Draw(spriteBatch);
        }
    }
}
