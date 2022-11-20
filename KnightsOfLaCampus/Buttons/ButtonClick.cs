using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KnightsOfLaCampus.Buttons;

internal sealed class ButtonClick : Button
{
    // Mouse state to make the button "clickable"
    private MouseState mCurrentMouseState;

    public ButtonClick(Vector2 position, string assetName, Color colorSelected, Color colorUnselected) : 
        base(position, assetName, colorSelected, colorUnselected) {}
 
    public override void LoadContent()
    {
        mTexture = Globals.Content.Load<Texture2D>(mAssetName);

        // Must be assigned after loading the texture to avoid null exceptions
        mButtonFrame = new Rectangle((int)mPosition.X, (int)mPosition.Y, mTexture.Width, mTexture.Height);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var mousePosition = Mouse.GetState().Position;

        // Draws the button
        var color = mColorUnselected;
        if (mButtonFrame.Contains(mousePosition))
        {
            color = mColorSelected;
        }

        Globals.SpriteBatch.Draw(mTexture,
            new Vector2((int)mPosition.X, (int)mPosition.Y),
            color);
    }

    public override bool IsPressed()
    {
        // The active state from the last frame is now old
        var lastMouseState = mCurrentMouseState;

        // Get the mouse state relevant for this frame
        mCurrentMouseState = Mouse.GetState();

        var mousePosition = Mouse.GetState().Position;
        // checks if the mouse is clicked and at the position of the button
        if (lastMouseState.LeftButton == ButtonState.Pressed && mCurrentMouseState.LeftButton == ButtonState.Released
                                                             && mButtonFrame.Contains(mousePosition))
        {
            return true;
        }

        return false;
    }
}