using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using System;

namespace KnightsOfLaCampus.Source
{
    internal static class Globals
    {
        // Some globals that we might use 

        public const int ScreenWidth = 1920;
        public const int ScreenHeight = 1080;

        public static GraphicsDevice Device { get; private set; }

        public static void GetSpriteBatch(GraphicsDevice graphicsDevice)
        {
            Globals.SpriteBatch = new SpriteBatch(graphicsDevice);
        }

        public static void GetGraphicsDevice(GraphicsDevice device)
        {
            Globals.Device = device;
        }

        public static void GetInput()
        {
            Globals.Mouse = new Input();
        }

        public static void GetContent(ContentManager content)
        {
            Globals.Content = content;
        }

        public static Input Mouse { get; private set; }

        public static SpriteBatch SpriteBatch { get; private set; }

        public static ContentManager Content { get; private set; }

        public static float GetDistance(Vector2 pos, Vector2 target)
        {
            return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
        }
    }
}
