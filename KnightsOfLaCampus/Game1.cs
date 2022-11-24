using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Screens;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace KnightsOfLaCampus
{
    internal sealed class Game1 : Game
    {
        private readonly GraphicsDeviceManager mGraphics;

        private readonly ScreenManager mScreenManager;

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            mScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Globals.SpriteBatch.Begin();

            mScreenManager.Draw(Globals.SpriteBatch);

            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}