using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Source.Interfaces
{
    internal interface IFriendlyUnit

    {
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public List<Vector2> Path { get; set; }
        public bool GetIfSelected();
        public void AudioUpdate();
        public void GraphicsUpdate();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}