using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_00
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager mGraphics;
        private SpriteBatch mSpriteBatch;
        private List<Sprite> mSprites;

        public Game1()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            mSpriteBatch = new SpriteBatch(GraphicsDevice);
            var texture = Content.Load<Texture2D>("Ye");
            var texture2 = Content.Load<Texture2D>("240px-Unilogo");

            mSprites = new List<Sprite>()
            {
                new Sprite(texture)
                {
                    mPosition = new Vector2(100, 100),
                    mInput = new input()
                    {
                        UpKeys = Keys.W,
                        DownKeys = Keys.S,
                        LeftKeys = Keys.A,
                        RightKeys = Keys.D,
                    }
                }
            };
        }
        // TODO: use this.Content to load your game content here


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {    Exit();}


            // TODO: Add your update logic here
            foreach (var sprite in mSprites)
            {
                sprite.Update();
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            mSpriteBatch.Begin();
            foreach (var sprite in mSprites )
            {
                sprite.Draw(mSpriteBatch);
            }
            mSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}