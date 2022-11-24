using KnightsOfLaCampus.Buttons;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfLaCampus.Screens
{
    internal sealed class StatisticScreen : IScreen
    {
        private int mGold, mAlive, mHeart, mTime, mDamage;
        private Texture2D mBackground;
        private ButtonClick mCloseButton;
        private SpriteFont mFont;
        private const int CloseButtonX = 1172;
        private const int CloseButtonY = 235;
        public bool DrawLower { get; }
        public bool UpdateLower { get; }

        public StatisticScreen()
        {
            DrawLower = false;
            UpdateLower = false;
        }
        public void LoadContent()
        {
            // these field can pass some value from other class maybe all the data will be collected or stored in some class that has public properties.
            mGold = 0;
            mAlive = 0;
            mHeart = 0;
            mTime = 0;
            mDamage = 0;

            mCloseButton = new ButtonClick(new Vector2(CloseButtonX, CloseButtonY), "CloseButton", Color.Black, Color.Firebrick);
            mCloseButton.LoadContent();
            mBackground = Globals.Content.Load<Texture2D>("UI\\Background\\menuStatistics");
            mFont = Globals.Content.Load<SpriteFont>("Font");
        }

        public void Update(GameTime gameTime)
        {

        }

        // there is no more next screen
        public IScreen NextScreen()
        {
            return null;
        }

        // if button close is pressed then return back to PauseMenu.
        public IScreen PrevScreen()
        {
            return mCloseButton.IsPressed() ? this : null;
        }

        public bool BackToMenu()
        {
            return false;
        }

        public void Draw()
        {
            // draw all the Value here.
            // can make it more nicer by adding icon before the name. (just a suggestion)
            Globals.SpriteBatch.Draw(mBackground, new Rectangle(704, 220, mBackground.Width, mBackground.Height), Color.White);
            Globals.SpriteBatch.DrawString(mFont, "Gold   : " + mGold, new Vector2((Globals.ScreenWidth / 2) - 75, 300), Color.Black);
            Globals.SpriteBatch.DrawString(mFont, "Alive  : " + mAlive, new Vector2((Globals.ScreenWidth / 2) - 75, 400), Color.Black);
            Globals.SpriteBatch.DrawString(mFont, "Heart  : " + mHeart, new Vector2((Globals.ScreenWidth / 2) - 75, 500), Color.Black);
            Globals.SpriteBatch.DrawString(mFont, "Time   : " + mTime, new Vector2((Globals.ScreenWidth / 2) - 75, 600), Color.Black);
            Globals.SpriteBatch.DrawString(mFont, "Damage : " + mDamage, new Vector2((Globals.ScreenWidth / 2) - 75, 700), Color.Black);
            // draw close button.
            mCloseButton.Draw(Globals.SpriteBatch);
        }
    }
}
