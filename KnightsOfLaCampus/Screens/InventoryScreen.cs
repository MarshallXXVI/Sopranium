using KnightsOfLaCampus.Buttons;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Screens
{
    internal sealed class InventoryScreen : IScreen
    {
        public bool DrawLower { get; set; }
        public bool UpdateLower { get; set; }

        private Texture2D mInventoryBackground;
        private ButtonClick mCloseButton;

        public InventoryScreen()
        {
            DrawLower = true;
            UpdateLower = false;
        }
        public void LoadContent()
        {
            mInventoryBackground = Source.Globals.Content.Load<Texture2D>("Inventar");
            mCloseButton = new ButtonClick( new Vector2(15, 15), "CloseButton", Color.OrangeRed, Color.White);
            mCloseButton.LoadContent();
        }

        public void Update(GameTime gameTime)
        {

        }

        public IScreen NextScreen()
        {
            return null;
        }

        public IScreen PrevScreen()
        {
            if (mCloseButton.IsPressed())
            {
                return this;
            }
            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Globals.SpriteBatch.Draw(
                mInventoryBackground,
                new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight * 1/4),
                Color.LightGray
            );

            mCloseButton.Draw(spriteBatch);
        }
    }
}
