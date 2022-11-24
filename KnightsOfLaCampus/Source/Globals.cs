using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

    }
}
