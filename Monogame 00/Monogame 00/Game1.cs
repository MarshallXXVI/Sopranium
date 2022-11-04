using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame00.Source.Engine;
using Monogame00.Sprites;

namespace Monogame00
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager mGraphics;
        //private SpriteBatch mSpriteBatch;
        private World mWorld;
        private int mColums;
        private int mRows;
        private Camera mCamera;

        public Game1()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mGraphics.PreferredBackBufferHeight = 600;
            mGraphics.PreferredBackBufferWidth = 900;
            mGraphics.ApplyChanges();
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
            Globals.mMouse = new McMouseControl();
            mWorld = new World();
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {    Exit();}
            Globals.mMouse.Update();

            mWorld.Update(gameTime);
            Globals.mMouse.UpdateOld();
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
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