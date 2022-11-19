using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Source
{
    internal abstract class Unit
    {
        protected Vector2 mPosition;
        public abstract void AudioUpdate();
        public abstract void GraphicsUpdate();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
