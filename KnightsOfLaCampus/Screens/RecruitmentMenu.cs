using KnightsOfLaCampus.Buttons;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Screens
{
    /// <summary>
    /// The recruitment menu class is used to display this menu at the bottom of the game.
    /// It also needs an object from each purchasable unit to read its price
    /// and the current gold level of the king to find out if the unit can be bought.
    /// </summary>
    internal sealed class RecruitmentMenu : InGameMenu
    {
        // Buy-Button of the units
        private ButtonClick mButtonBuyUnitSwordsman;
        private ButtonClick mButtonBuyUnitKavallerie;
        private ButtonClick mButtonBuyUnitHeilerin;
        private ButtonClick mButtonBuyUnitBogenschuetze;

        // Size of the graphics in relation to the screen size -> Positions when viewed from the centre of the screen
        // Since Jenkins otherwise complains, each button gets its own x coordinate.
        private const int Distance = 256;

        private const int ButtonBuyUnitX0 = (Globals.ScreenWidth / 2) - 728;
        private const int ButtonBuyUnitX1 = (Globals.ScreenWidth / 2) - 728 + Distance;
        private const int ButtonBuyUnitX2 = (Globals.ScreenWidth / 2) - 728 + (Distance * 2);
        private const int ButtonBuyUnitX3 = (Globals.ScreenWidth / 2) - 728 + (Distance * 3);

        private const int ButtonBuyUnitY = (Globals.ScreenHeight / 2) + 488;

        private const int PriceX0 = (Globals.ScreenWidth / 2) - 678;
        private const int PriceX1 = (Globals.ScreenWidth / 2) - 678 + Distance;
        private const int PriceX2 = (Globals.ScreenWidth / 2) - 678 + (Distance * 2);
        private const int PriceX3 = (Globals.ScreenWidth / 2) - 678 + (Distance * 3);

        private const int PriceY = (Globals.ScreenHeight / 2) + 460;

        /// <summary>
        /// Loads all used resources such as background, buttons and font
        /// </summary>
        public override void LoadContent()
        {
            // The units
            // "BuyableUnits = new List<AllyUnit>();"
            // "BuyableUnits.Add(new Swordsman());"

            #region Init

            // Background graphics
            Background = Source.Globals.Content.Load<Texture2D>("UI\\Background\\RecruitmentMenu");

            // Close Button
            CloseButton = new ButtonClick(new Vector2(CloseButtonX, CloseButtonY), "CloseButton", Color.Black, Color.Firebrick);
            CloseButton.LoadContent();

            // Buy-Buttons of each unit
            mButtonBuyUnitSwordsman = new ButtonClick(new Vector2(ButtonBuyUnitX0, ButtonBuyUnitY), "BuyMenuButton", Color.Green, Color.Firebrick);
            mButtonBuyUnitSwordsman.LoadContent();

            mButtonBuyUnitKavallerie = new ButtonClick(new Vector2(ButtonBuyUnitX1, ButtonBuyUnitY), "BuyMenuButton", Color.Green, Color.Firebrick);
            mButtonBuyUnitKavallerie.LoadContent();

            mButtonBuyUnitHeilerin = new ButtonClick(new Vector2(ButtonBuyUnitX2, ButtonBuyUnitY), "BuyMenuButton", Color.Green, Color.Firebrick);
            mButtonBuyUnitHeilerin.LoadContent();

            mButtonBuyUnitBogenschuetze = new ButtonClick(new Vector2(ButtonBuyUnitX3, ButtonBuyUnitY), "BuyMenuButton", Color.Green, Color.Firebrick);
            mButtonBuyUnitBogenschuetze.LoadContent();

            // Font-Object
            WritingPrice = Globals.Content.Load<SpriteFont>("Font");

            #endregion
        }

        public override void Update(GameTime gameTime)
        {
            if (mButtonBuyUnitSwordsman.IsPressed() && GameGlobals.mGold >= 10)
            {
                GameGlobals.mGold -= 10;
                GameGlobals.mPassFriends(new Knight(){Position = new Vector2(900, 700)});
            }
            if (mButtonBuyUnitKavallerie.IsPressed() && GameGlobals.mGold >= 200)
            {
                GameGlobals.mGold -= 200;
                GameGlobals.mPassFriends(new Knight() { Position = new Vector2(1100, 700) });
            }
            if (mButtonBuyUnitHeilerin.IsPressed() && GameGlobals.mGold >= 10)
            {
                GameGlobals.mGold -= 10;
                GameGlobals.mPassFriends(new Knight() { Position = new Vector2(1200, 700) });
            }
            if (mButtonBuyUnitBogenschuetze.IsPressed() && GameGlobals.mGold >= 10)
            {
                GameGlobals.mGold -= 10;
                GameGlobals.mPassFriends(new Knight() { Position = new Vector2(1000, 700) });
            }
        }

        /// <summary>
        /// Draws all buttons, the background and the cost of each unit
        /// </summary>
        public override void Draw()
        {
            // Buy-Buttons of each unit
            mButtonBuyUnitSwordsman.Draw(Globals.SpriteBatch);
            mButtonBuyUnitKavallerie.Draw(Globals.SpriteBatch);
            mButtonBuyUnitHeilerin.Draw(Globals.SpriteBatch);
            mButtonBuyUnitBogenschuetze.Draw(Globals.SpriteBatch);

            // Background-Graphic
            Globals.SpriteBatch.Draw(Background, new Rectangle(0, Globals.ScreenHeight - Background.Height, Background.Width, Background.Height), Color.White);

            // Close-Button
            CloseButton.Draw(Globals.SpriteBatch);

            // Prices of each unit
            // Globals.SpriteBatch.DrawString(WritingPrice, BuyableUnits[0].mCost.ToString(), new Vector2(PriceX - Distance * 3, PriceY), Color.Black);
            Globals.SpriteBatch.DrawString(WritingPrice, "10", new Vector2(PriceX3, PriceY), Color.Black);
            Globals.SpriteBatch.DrawString(WritingPrice, "10", new Vector2(PriceX2, PriceY), Color.Black);
            Globals.SpriteBatch.DrawString(WritingPrice, "200", new Vector2(PriceX1, PriceY), Color.Black);
            Globals.SpriteBatch.DrawString(WritingPrice, "10", new Vector2(PriceX0, PriceY), Color.Black);
        }
    }
}
