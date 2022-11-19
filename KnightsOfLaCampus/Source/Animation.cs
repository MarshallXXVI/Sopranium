using Microsoft.Xna.Framework.Graphics;


namespace KnightsOfLaCampus.Source
{
    internal sealed class Animation
    {
        private const float Frame = 0.2f;

        public int CurrentFrame { get; set; }

        public int FrameCount { get; }

        public int FrameHeight => Texture.Height;

        public float FrameSpeed { get; }

        public int FrameWidth => Texture.Width / FrameCount;

        public Texture2D Texture { get; }

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            FrameSpeed = Frame;
        }
    }
}
