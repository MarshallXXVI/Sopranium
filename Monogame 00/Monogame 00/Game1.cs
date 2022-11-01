using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame_00.Models;
using Monogame_00.Source;
using Monogame_00.Source.Engine;
using Monogame_00.Sprites;

namespace Monogame_00
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager mGraphics;
        //private SpriteBatch mSpriteBatch;
        private World mWorld;
        private int mColums;
        private int mRows;

        public Game1()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mColums = mGraphics.PreferredBackBufferWidth;
            mRows = mGraphics.PreferredBackBufferHeight;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.mScreenWidth = mColums;
            Globals.mScreenHeight = mRows;
            Globals.mContent = this.Content;
            Globals.mSpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.mEffect = Globals.mContent.Load<Effect>("Effects/NormalCopy");
            Globals.mMouse = new McMouseControl();
            mWorld = new World();
        }
        // TODO: use this.Content to load your game content here


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {    Exit();}

            mWorld.Update(gameTime);
            Globals.mMouse.Update();
            // TODO: Add your update logic here


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Globals.mSpriteBatch.Begin();
            mWorld.Draw(Globals.mSpriteBatch);
            Globals.mSpriteBatch.End();
            base.Draw(gameTime);
        }

        public virtual void ChangeGameState(object info)
        {
            Globals.mGameState = Convert.ToInt32(info, Globals.mCulture);
        }
    }
}