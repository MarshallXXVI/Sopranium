using KnightsOfLaCampus.Buttons;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Screens
{
    internal sealed class PauseMenu : GameMenu
    {
        // Graphics
        private ButtonClick mButtonBackToMenu;
        private Texture2D mBackgroundGrayEffect;

        // Placement of Button
        private const int ButtonBackToMenuX = (Globals.ScreenWidth / 2) - 177;
        private const int ButtonBackToMenuY = (Globals.ScreenHeight / 2) + 60;

        /// <summary>
        /// Loads all used resources such as background and buttons 
        /// </summary>
        public override void LoadContent()
        {
            // Background graphics
            mBackgroundGrayEffect = Source.Globals.Content.Load<Texture2D>("UI\\Background\\pauseMenuBackground");
            Background = Source.Globals.Content.Load<Texture2D>("UI\\Background\\menuPause");

            // Close Button
            CloseButton = new ButtonClick(new Vector2(CloseButtonX, CloseButtonY), "CloseButton", Color.Black, Color.Firebrick);
            CloseButton.LoadContent();

            // Back-to-Menu-Button
            mButtonBackToMenu = new ButtonClick(new Vector2(ButtonBackToMenuX, ButtonBackToMenuY), "MenuButtonBig", Color.Goldenrod, Color.Wheat);
            mButtonBackToMenu.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Return to main menu + save game
        /// </summary>
        public override IScreen NextScreen()
        {
            return null;
        }

        /// <summary>
        /// Return to main menu + save game
        /// </summary>
        public override bool BackToMenu()
        {
            // TODO Speichern
            return mButtonBackToMenu.IsPressed();
        }

        /// <summary>
        /// Draws all buttons and the background
        /// </summary>
        public override void Draw()
        {
            // Background-Graphic
            Globals.SpriteBatch.Draw(mBackgroundGrayEffect, new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight), Color.White);

            mButtonBackToMenu.Draw(Globals.SpriteBatch);

            Globals.SpriteBatch.Draw(Background, new Rectangle(BackgroundX, BackgroundY, Background.Width, Background.Height), Color.White);

            // Close-Button
            CloseButton.Draw(Globals.SpriteBatch);

        }
    }
}
