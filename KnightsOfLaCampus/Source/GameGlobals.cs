using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsOfLaCampus.Source;

namespace KnightsOfLaCampus
{
    internal static class GameGlobals
    {
        public static int mGold = 0;
        public static int mLevel = 0;
        public static GameTimer mTimer;
        public static PassObject mPassMobs, mPassFriends, mPassGolds, mPassArrow;
    }
}
