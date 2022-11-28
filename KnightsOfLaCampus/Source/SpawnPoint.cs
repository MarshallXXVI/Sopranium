using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.Astar;
using KnightsOfLaCampus.UnitsGameObject;
using KnightsOfLaCampus.UnitsGameObject.Enemies;
using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Units
{
    internal class SpawnPoint
    {
        private readonly Player mPlayer;
        private readonly SoMuchOfSpots mField;
        private readonly Vector2 mPosition;
        public readonly McTimer mSpawnTimer = new McTimer(1800);
        public SpawnPoint(Player target, SoMuchOfSpots field,
            Vector2 position)
        {
            mPlayer = target;
            mField = field;
            mPosition = position;
        }

        public void Update(GameTime gameTime)
        {
            mSpawnTimer.UpdateTimer(gameTime);
            if (!mSpawnTimer.Test())
            {
                return;
            }

            SpawnMob();
            mSpawnTimer.ResetToZero();
        }

        private void SpawnMob()
        {
            GameGlobals.mPassMobs(new BadGuy(mPlayer, mField){Position = mPosition});
        }
    }
    
}
