using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Monogame_00.Models
{
    public class Spots
    {
        // f, g, h, value for AStar Path finding.
        public int mF = 0;
        public int mG = 0;
        public int mH = 0;

        public Vector2 mPositionOfThisSpot;
        public Spots(int i, int j)
        {
            mPositionOfThisSpot.X = i;
            mPositionOfThisSpot.Y = j;
        }
    }
}
