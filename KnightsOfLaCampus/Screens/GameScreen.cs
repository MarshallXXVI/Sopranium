using System;
using KnightsOfLaCampus.Buttons;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Screens
{
    /// <summary>
    /// The Game screen of the game where the player plays on. The player can either switch to the main menu or can open the
    /// recruitment menu, repair menu or pause menu from here. 
    /// </summary>
    internal sealed class GameScreen : IScreen
    {
        public bool DrawLower { get; set; }
        public bool UpdateLower { get; set; }

        // what will hold in this class Player.cs World.cs and LevelManager.cs


        // TODO Temporary buttons -> later with left click 
        private Button mBuy;
        private Button mRepair;
        private Button mPause;
        private Button mSkipDay;

        //Declaration Timer
        //Move to Globals.
        private SpriteFont mFont;

        // Position of Timer
        private const int TimerPositionX = 32;
        private const int TimerPositionY = 16;

        //shows how many Days and Nights, /2 and you get how many Nights for example
        //private int mCounter;
        private float mTime;

        /// <summary>
        /// Constructor of the class GameScreen.
        /// Sets the MainMenu to stop loading and stop continuing while the game is open.
        /// </summary>
        public GameScreen()
        {
            DrawLower = false;
            UpdateLower = false;

        }

        /// <summary>
        /// Loads the UI of the game
        /// </summary>
        public void LoadContent()
        {
            // Loading the Temporary buttons
            mRepair = new ButtonClick(new Vector2(Globals.ScreenWidth - 100, 20), "BuyButton", Color.Green, Color.Gray);
            mRepair.LoadContent();

            mBuy = new ButtonClick(new Vector2(Globals.ScreenWidth - 100, 50), "RepairButton", Color.Green, Color.Gray);
            mBuy.LoadContent();

            mPause = new ButtonClick(new Vector2(Globals.ScreenWidth - 100, 80), "PauseButton", Color.Green, Color.Gray);
            mPause.LoadContent();

            mSkipDay = new ButtonClick(new Vector2(TimerPositionX + 300, TimerPositionY), "SkipDay", Color.Red, Color.Green);
            mSkipDay.LoadContent();

            //Loading the Timer 600f means 600s = 10min for 1 day or night
            mFont = Globals.Content.Load<SpriteFont>("font");
            GameGlobals.mTimer = new GameTimer(Globals.Content.Load<Texture2D>("UI\\Background\\TimeBackgroundDay"),
                mFont,
                new(TimerPositionX, TimerPositionY),
                60f
            );
            GameGlobals.mTimer.StartStop();
            GameGlobals.mTimer.Repeat = true;
            Globals.mWorld = new World();
        }

        public void Update(GameTime gameTime)
        {
            Globals.Mouse.Update();
            Globals.mWorld.Update(gameTime);
            Globals.Mouse.UpdateOld();
            //Update the timer with the mTime Variable in Source/GameTimer
            mTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameGlobals.mTimer.Update(mTime);
            if (mSkipDay.IsPressed())
            {
                GameGlobals.mTimer.mTimeLeft = 5f;
            }
        }

        /// <summary>
        /// Opens different menus depending on the button
        /// Button Repair = repair menu
        /// Button Buy = purchase menu
        /// Button Pause = pause menu
        /// </summary>
        /// <returns>New instance of IScreen or null</returns>
        public IScreen NextScreen()
        {
            // To implement button for access to Buy Menu
            if (mRepair.IsPressed())
            {
                // Opens up the inventory
                return new RecruitmentMenu();
            }

            // To implement button for access to Repair Menu
            if (mBuy.IsPressed())
            {
                // Opens up the inventory
                return new RepairMenu();
            }

            // Open Pause Menu
            if (mPause.IsPressed())
            {
                // Opens up the inventory
                return new PauseMenu();
            }

            return null;
        }


        public IScreen PrevScreen()
        {
            return null;
        }

        /// <summary>
        /// Draws the UI of the game
        /// </summary>
        public void Draw()
        {
            Globals.mWorld.Draw(Globals.SpriteBatch);

            // Drawing the Temporary buttons
            mRepair.Draw(Globals.SpriteBatch);
            mBuy.Draw(Globals.SpriteBatch);
            mPause.Draw(Globals.SpriteBatch);
            mSkipDay.Draw(Globals.SpriteBatch);
            //Draw Timer
            GameGlobals.mTimer.Draw();
            Globals.SpriteBatch.DrawString(mFont, "GOLDS: " + GameGlobals.mGold.ToString(), new Vector2(32, 80), Color.Red);
        }

        public bool BackToMenu()
        {
            return false;
        }
    }
}
