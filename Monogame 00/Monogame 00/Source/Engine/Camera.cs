using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame00.Source.Engine
{
    public class Camera
    {
        public Matrix Transfrom { get; private set; }

        public void Follow(Player target)
        {
            var positon = Matrix.CreateTranslation(
                -target.mPlayer.Position.X - (target.mPlayer.Rectangle.Width / 2),
                -target.mPlayer.Position.Y - (target.mPlayer.Rectangle.Height / 2), 0);

            var offset = Matrix.CreateTranslation(
                Globals.mScreenWidth / 2,
                Globals.mScreenHeight / 2,
                0);

            Transfrom = positon * offset;
        }
    }
}
