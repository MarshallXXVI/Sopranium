using KnightsOfLaCampus.Buttons;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Screens
{
    /// <summary>
    /// The repair menu class is used to display this menu at the bottom of the game.
    /// It also needs an object from each repairable item to read its price
    /// and the current gold level of the king to find out if the unit can be bought.
    /// </summary>
    internal sealed class RepairMenu : InGameMenu
    {
        // Repair-Button of the units
        private ButtonClick mButtonRepairWall;

        // Size of the graphics in relation to the screen size -> Positions when viewed from the centre of the screen
        private const int ButtonBuyUnitX = (Globals.ScreenWidth / 2) - 696;
        private const int ButtonBuyUnitY = (Globals.ScreenHeight / 2) + 488;

        private const int PriceX = (Globals.ScreenWidth / 2) - 645;
        private const int PriceY = (Globals.ScreenHeight / 2) + 460;

        /// <summary>
        /// Loads all used resources such as background, buttons and font
        /// </summary>
        public override void LoadContent()
        {
            #region Init

            // Background graphics
            Background = Source.Globals.Content.Load<Texture2D>("UI\\Background\\BuyMenu");

            // Close Button
            CloseButton = new ButtonClick(new Vector2(CloseButtonX, CloseButtonY), "CloseButton", Color.Black, Color.Firebrick);
            CloseButton.LoadContent();

            // Repair-Buttons of each unit
            mButtonRepairWall = new ButtonClick(new Vector2(ButtonBuyUnitX, ButtonBuyUnitY), "BuyMenuButton", Color.Firebrick, Color.Firebrick);
            mButtonRepairWall.LoadContent();

            // Font-Object
            WritingPrice = Globals.Content.Load<SpriteFont>("Font");

            #endregion
        }

        /// <summary>
        /// Draws all buttons, the background and the cost of the wall
        /// </summary>
        public override void Draw()
        {
            // Buy-Buttons of each unit
            mButtonRepairWall.Draw(Globals.SpriteBatch);

            // Background-Graphic
            Globals.SpriteBatch.Draw(Background, new Rectangle(0, Globals.ScreenHeight - Background.Height, Background.Width, Background.Height), Color.White);

            // Close-Button
            CloseButton.Draw(Globals.SpriteBatch);

            // Prices of the wall
            Globals.SpriteBatch.DrawString(WritingPrice, "10", new Vector2(PriceX, PriceY), Color.Black);
        }
    }
}
