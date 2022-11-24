using KnightsOfLaCampus.Buttons;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Screens
{
    internal abstract class GameMenu : IScreen
    {
        public bool DrawLower { get; }
        public bool UpdateLower { get; }

        // Graphics
        protected Texture2D Background { get; set; }
        protected ButtonClick CloseButton { get; set; }

        // Size of the graphics in relation to the screen size -> Positions when viewed from the centre of the screen
        protected const int CloseButtonX = (Globals.ScreenWidth / 2) + 205;
        protected const int CloseButtonY = (Globals.ScreenHeight / 2) - 208;

        protected const int BackgroundX = (Globals.ScreenWidth / 2) - 292;
        protected const int BackgroundY = (Globals.ScreenHeight / 2) - 284;

        /// <summary>
        /// Constructor of the class GameMenu.
        /// Sets the game to keep loading but stop continuing while the menu is open.
        /// </summary>
        protected GameMenu()
        {
            DrawLower = true;
            UpdateLower = false;
        }

        public abstract void LoadContent();


        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Determines where you want to go next
        /// </summary>
        public abstract IScreen NextScreen();

        /// <summary>
        /// Close the menu and go to the previous screen when the Exit button is clicked
        /// </summary>
        public IScreen PrevScreen()
        {
            return CloseButton.IsPressed() ? this : null;
        }

        public abstract bool BackToMenu();

        /// <summary>
        /// Draws all the buttons we need + background
        /// </summary>
        public abstract void Draw();
    }
}
