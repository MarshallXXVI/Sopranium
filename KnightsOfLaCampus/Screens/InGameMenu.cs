using KnightsOfLaCampus.Buttons;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using KnightsOfLaCampus.Source;

namespace KnightsOfLaCampus.Screens
{
    /// <summary>
    /// Abstract class to better manage the in game menus
    /// </summary>
    internal abstract class InGameMenu : IScreen
    {
        public bool DrawLower { get; }
        public bool UpdateLower { get; }

        // Graphics
        protected Texture2D Background { get; set; }
        protected ButtonClick CloseButton { get; set; }

        // Size of the graphics in relation to the screen size -> Positions when viewed from the bottom of the screen
        protected const int CloseButtonX = 1764;    // left edge
        protected const int CloseButtonY = Globals.ScreenHeight - 98;

        // Font-Object to write the prices
        protected SpriteFont WritingPrice { get; set; }

        /// <summary>
        /// Constructor of the class RepairMenu.
        /// Sets the game to keep loading and continuing while the menu is open.
        /// </summary>
        protected InGameMenu()
        {
            DrawLower = true;
            UpdateLower = true;
        }

        /// <summary>
        /// Loads all used resources such as background, buttons and font
        /// </summary>
        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// No further screen can be loaded after this one and therefore null is returned.
        /// </summary>
        public IScreen NextScreen()
        {
            return null;
        }

        /// <summary>
        /// Closes the menu when the Close button is pressed. If a wall is pressed, it should be checked whether it can be bought and then act accordingly.
        /// </summary>
        public IScreen PrevScreen()
        {
            if (CloseButton.IsPressed())
            {
                return this;
            }
            return null;
        }

        public bool BackToMenu()
        {
            return false;
        }

        /// <summary>
        /// Draws all buttons, the background and the cost of the wall
        /// </summary>
        public abstract void Draw();
    }
}
