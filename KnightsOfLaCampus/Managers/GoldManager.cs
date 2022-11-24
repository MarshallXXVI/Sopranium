using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace KnightsOfLaCampus.Managers
{
    // This class takes a list of Grid Coordinates and sets Gold Objects
    // inside the given GridBox.
    internal sealed class GoldManager
    {
        private readonly List<Gold> mGoldSpawnPoints;
        private readonly King mKing;

        public GoldManager(King king, List<Vector2> spawnPoints)
        {
            mGoldSpawnPoints = new List<Gold>();
            mKing = king;

            foreach (var spawn in spawnPoints)
            {
                mGoldSpawnPoints.Add(new Gold(spawn, mKing));
            }
        }

        // If a Gold Object is not alive it gets removed and the King gets his amount of Gold
        public void Update()
        {
            for (var i = 0; i < mGoldSpawnPoints.Count; i++)
            {
                mGoldSpawnPoints[i].Update();

                if (!mGoldSpawnPoints[i].mAlive)
                {
                    mKing.mGold += mGoldSpawnPoints[i].mAmount;
                    mGoldSpawnPoints.RemoveAt(i);
                    i--;
                }
            }
        }

        // Draws the Gold Objects from the List
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var spawn in mGoldSpawnPoints)
            {
                spawn.Draw(spriteBatch);
            }
        }
    }
}
