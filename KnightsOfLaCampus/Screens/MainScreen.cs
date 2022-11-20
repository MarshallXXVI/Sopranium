using KnightsOfLaCampus.Buttons;
using KnightsOfLaCampus.Saves;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Screens;

internal sealed class MainScreen : IScreen
{
    public bool DrawLower { get; set; }
    public bool UpdateLower { get; set; }

    private Texture2D mBackground;

    private Button mNewGameButton;
    private Button mLoadGameButton;

    public MainScreen()
    {
        DrawLower = false;
        UpdateLower = false;
    }
    public void LoadContent()
    {
        // Loads the background
        mBackground = Globals.Content.Load<Texture2D>("Hauptmenü");

        // Loads the Neues Spiel button
        mNewGameButton = new ButtonClick(new Vector2(100, 100),
            "NeuesSpielButton",
            Color.Green, 
            Color.White);

        // Loads the Spiel laden button
        mLoadGameButton = new ButtonClick(new Vector2(100, 300),
            "SpielLadenButton",
            Color.Green,
            Color.White);

        // Loads the button contents
        mNewGameButton.LoadContent();
        mLoadGameButton.LoadContent();
    }

    public void Update(GameTime gameTime)
    {

    }

    public IScreen NextScreen()
    {
        if (mNewGameButton.IsPressed())
        {
            return new GameScreen();
        }

        if (mLoadGameButton.IsPressed())
        {
            SavedVariables.LoadSavedVariables = true;
            return new GameScreen();
        }

        return null;
    }

    public IScreen PrevScreen()
    {
        return null;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Draws the background
        Globals.SpriteBatch.Draw(mBackground, 
            new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight), 
            Color.White);

        // Draws the buttons
        mNewGameButton.Draw(spriteBatch);
        mLoadGameButton.Draw(spriteBatch);
    }
}