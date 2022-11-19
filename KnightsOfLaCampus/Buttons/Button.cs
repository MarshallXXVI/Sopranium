using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Buttons
{
    internal abstract class Button
    {
        // mTexture of the button
        protected Texture2D mTexture;

        // The Frame of the button. For now it gets scaled by 
        // the loaded textures
        protected Rectangle mButtonFrame;

        // Path to the target texture
        protected readonly string mAssetName;

        // The mouse position is equal to the button's position
        protected Color mColorSelected;

        // The default color of the button
        protected  Color mColorUnselected;

        // Position of the Button
        protected  Vector2 mPosition;

        public abstract void LoadContent();
        public abstract void Draw(SpriteBatch spriteBatch);

        // Event if the Button is pressed
        public abstract bool IsPressed();

        protected Button(Vector2 position, string assetName, Color colorSelected, Color colorUnselected)
        {
            mColorSelected = colorSelected;
            mColorUnselected = colorUnselected;

            mAssetName = assetName;
            mPosition = position;

        }
    }
}
