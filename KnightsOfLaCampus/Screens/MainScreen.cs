using KnightsOfLaCampus.Buttons;
using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Saves;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Screens
{
    /// <summary>
    /// The Main Screen of the game. This is what the player will see firstly
    /// </summary>
    internal sealed class MainScreen : IScreen
    {
        public bool DrawLower { get; set; }
        public bool UpdateLower { get; set; }

        // Graphics
        private Texture2D mBackground;
        private Texture2D mMainMenu;

        private Button mNewGameButton;
        private Button mLoadGameButton;
        private Button mStatisticButton;
        private Button mTechDemoButton;
        private Button mQuitToDesktopButton;
        
        // Size of the graphics in relation to the screen size -> Positions when viewed from the centre of the screen
        // Since Jenkins otherwise complains, each button gets its own x coordinate.
        private const int ButtonMenuX = (Globals.ScreenWidth / 2) - 177;

        private const int ButtonMenuY0 = (Globals.ScreenHeight / 2) - 129;
        private const int ButtonMenuY1 = (Globals.ScreenHeight / 2) - 45;
        private const int ButtonMenuY2 = (Globals.ScreenHeight / 2) + 39;
        private const int ButtonMenuY3 = (Globals.ScreenHeight / 2) + 123;
        private const int ButtonMenuY4 = (Globals.ScreenHeight / 2) + 207;

        private const int MainMenuX = (Globals.ScreenWidth / 2) - 292;
        private const int MainMenuY = (Globals.ScreenHeight / 2) - 382;

        // Sound
        private SoundManager mSoundManager;

        /// <summary>
        /// Constructor of the class MainScreen.
        /// </summary>
        public MainScreen()
        {
            DrawLower = false;
            UpdateLower = false;

            // Loads the menu music
            mSoundManager = new SoundManager();
        }

        /// <summary>
        /// Loads the content
        /// </summary>
        public void LoadContent()
        {
            // Loads the background
            mBackground = Globals.Content.Load<Texture2D>("UI\\Background\\MainMenuBackground");
            mMainMenu = Globals.Content.Load<Texture2D>("UI\\Background\\MainMenu");

            // Loads the Neues Spiel button
            mNewGameButton = new ButtonClick(new Vector2(ButtonMenuX, ButtonMenuY0),
                "MenuButton",
                Color.Goldenrod, 
                Color.Wheat);

            // Loads the Spiel laden button
            mLoadGameButton = new ButtonClick(new Vector2(ButtonMenuX, ButtonMenuY1),
                "MenuButton",
                Color.Goldenrod,
                Color.Wheat);

            // Loads the Statistic button.
            mStatisticButton = new ButtonClick(new Vector2(ButtonMenuX, ButtonMenuY2)
                , "MenuButton", Color.Goldenrod, Color.Wheat);

            // Loads the TEch Demo button.
            mTechDemoButton = new ButtonClick(new Vector2(ButtonMenuX, ButtonMenuY3)
                , "MenuButton", Color.Goldenrod, Color.Wheat);

            // Loads the Quit button.
            mQuitToDesktopButton = new ButtonClick(new Vector2(ButtonMenuX, ButtonMenuY4)
                , "MenuButton", Color.Goldenrod, Color.Wheat);

            // Loads the button contents
            mNewGameButton.LoadContent();
            mLoadGameButton.LoadContent();
            mStatisticButton.LoadContent();
            mTechDemoButton.LoadContent();
            mQuitToDesktopButton.LoadContent();

            mSoundManager.ChangeMusic(1);   // Menu Music
        }

        public void Update(GameTime gameTime)
        {
            if (mQuitToDesktopButton.IsPressed())
            {
                Globals.mQuitGame = true;
            }

        }

        /// <summary>
        /// Adds a new screen to the stack depending on the button that was clicked.
        /// </summary>
        /// <returns>Returns the new screen</returns>
        public IScreen NextScreen()
        {
            // When there are no save states yet
            if (mNewGameButton.IsPressed())
            {
                mSoundManager.ChangeMusic(0);
                Globals.mLoadGame = false;
                GameGlobals.mGold = 0;
                return new GameScreen();
            }

            // If there are saved variables, its possible to load a savegame
            if (mLoadGameButton.IsPressed())
            {
                mSoundManager.ChangeMusic(0);
                Globals.mLoadGame = true;
                return new GameScreen();
            }

            return mStatisticButton.IsPressed() ? new StatisticScreen() : null;
        }

        /// <summary>
        /// Since there can be no previous screen, null is returned.
        /// </summary>
        /// <returns>Null</returns>
        public IScreen PrevScreen()
        {
            // since there is no previous screen, this always returns null 
            return null;
        }
        public bool BackToMenu()
        {
            return false;
        }

        /// <summary>
        /// Draws the content of the GameScreen
        /// </summary>
        public void Draw()
        {
            // Draws the background
            Globals.SpriteBatch.Draw(mBackground,
                new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight),
                Color.White);

            // Draws the buttons
            mNewGameButton.Draw(Globals.SpriteBatch);
            mLoadGameButton.Draw(Globals.SpriteBatch);
            mStatisticButton.Draw(Globals.SpriteBatch);
            mTechDemoButton.Draw(Globals.SpriteBatch);
            mQuitToDesktopButton.Draw(Globals.SpriteBatch);

            Globals.SpriteBatch.Draw(mMainMenu,
                new Rectangle(MainMenuX, MainMenuY, mMainMenu.Width, mMainMenu.Height),
                Color.White);
        }
    }
}
