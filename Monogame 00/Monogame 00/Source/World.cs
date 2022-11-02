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
using Monogame00.Sprites;

namespace Monogame00
{
    public class World
    {
        public System.Numerics.Vector2 mOffSet;
        public Grid mGrid;
        public Sprite mPlayer;
        public World()
        {
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
            mOffSet = new System.Numerics.Vector2(0, 0);

            mGrid = new Grid(new System.Numerics.Vector2(20, 20),
                new System.Numerics.Vector2(0, 0),
                new System.Numerics.Vector2(Globals.mScreenWidth, Globals.mScreenHeight));
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
