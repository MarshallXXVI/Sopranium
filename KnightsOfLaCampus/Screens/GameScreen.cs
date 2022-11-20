using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace KnightsOfLaCampus.Screens;

internal sealed class GameScreen : IScreen
{
    public bool DrawLower { get; set; }
    public bool UpdateLower { get; set; }

    // what will hold in this class Player.cs World.cs and LevelManager.cs

    private World mWorld;

    public GameScreen()
    {
        DrawLower = false;
        UpdateLower = false;

    }

    public void LoadContent()
    {
        mWorld = new World();
    }

    public void Update(GameTime gameTime)
    {
        Globals.Mouse.Update();
        mWorld.Update(gameTime);
        Globals.Mouse.UpdateOld();
    }

    public IScreen NextScreen()
    {
        // To implement button for access to Inventory
        //if (Mouse.GetState().RightButton == ButtonState.Pressed)
        //{
        //    // Opens up the inventory
        //    return new InventoryScreen();
        //}
        return null;
    }

    public IScreen PrevScreen()
    {
        // To implement button for Exit or to MenuOptions.
        //if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
        //{
        //    return this;
        //}
        return null;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        mWorld.Draw(Globals.SpriteBatch);
    }
}