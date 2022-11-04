#region Includes
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Monogame00
{
    public delegate void PassObject(object i);

    public class Globals
    {
        public static int mScreenHeight, mScreenWidth, mGameState = 0;

        public static System.Globalization.CultureInfo mCulture = new System.Globalization.CultureInfo("en-US");

        public static ContentManager mContent;

        public static SpriteBatch mSpriteBatch;

        public static Effect mEffect;

        public static McMouseControl mMouse;

        public static float GetDistance(Vector2 pos, Vector2 target)
        {
            return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
        }

    }
}
