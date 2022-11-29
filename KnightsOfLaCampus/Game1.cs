using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Screens;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KnightsOfLaCampus
{
    internal sealed class Game1 : Game
    {
        private readonly GraphicsDeviceManager mGraphics;

        private readonly ScreenManager mScreenManager;

        // Big Thanks to By Stephen Armstrong // March 10th, 2020
        // https://www.industrian.net/tutorials/changing-brightness/
        // Now we can test dimming ours screen by pressing page up and page down.
        // A single-pixel texture that will be stretched to cover the screen.
        private Texture2D mPixel;
        // A value for the player to control brightness.
        public byte mBrightnessValue;


        internal Game1()
        {
            mGraphics = new GraphicsDeviceManager(this);

            // creates the ScreenManager
            mScreenManager = new ScreenManager();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // sets the screen to a fix size 
            mGraphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            mGraphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            mGraphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.GetInput();
            Globals.GetSpriteBatch(GraphicsDevice);
            Globals.GetContent(Content);
            Globals.GetGraphicsDevice(GraphicsDevice);

            mScreenManager.AddScreen(new MainScreen());


            // Create a 1x1 Texture.
            mPixel = new Texture2D(GraphicsDevice, 1, 1);
            // Set the color data to the 1x1 Texture.
            mPixel.SetData<Color>(new Color[] { Color.White });
            // Set a default brightnessValue of 100 (full brightness).
            mBrightnessValue = 100;
            Globals.mBrightness = mBrightnessValue;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Globals.mQuitGame)
            {
                Exit();
            }

            mScreenManager.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
            {
                IncreaseBrightness();
            }
            // Press/Hold the Page Down key to increase the brightness.
            if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
            {
                DecreaseBrightness();
            }

            base.Update(gameTime);
        }

        void IncreaseBrightness()
        {
            // Brightness can only be increased if it is under 100.
            if (Globals.mBrightness < 100)
            {
                Globals.mBrightness++;
                System.Console.WriteLine("Brightness: {0} %", Globals.mBrightness);
            }
        }
        void DecreaseBrightness()
        {
            // Brightness can only be decreased if it is above 0.
            if (Globals.mBrightness > 0)
            {
                Globals.mBrightness--;
                System.Console.WriteLine("Brightness: {0} %", Globals.mBrightness);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Globals.SpriteBatch.Begin();

            mScreenManager.Draw(Globals.SpriteBatch);

            // brightnessMultiplier will contain a percentage value of transparency.
            float brightnessMultiplier = // Remove the current brightnessValue from 100 and divide it by 100 to get a value between 0 and 1.00.
                100 - Globals.mBrightness;
            brightnessMultiplier /= 100;
            // Stretch the single-pixel texture to cover the screen. The color black is rendered using brightnessMultiplier as its transparency value.
            Globals.SpriteBatch.Draw(mPixel, new Rectangle(0, 0, mGraphics.PreferredBackBufferWidth, mGraphics.PreferredBackBufferHeight), Color.Black * brightnessMultiplier);

            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}