using KnightsOfLaCampus.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KnightsOfLaCampus.Source.GridNew;
using KnightsOfLaCampus.Units;
using TiledSharp;
using System.Collections.Generic;
using KnightsOfLaCampus.Managers.MovementManagement;

namespace KnightsOfLaCampus.Source
{
    internal sealed class World
    {
        private readonly Vector2 mOffSet;
        private readonly Grid mGrid;
        private readonly TileMapManager mMapManager;
        private readonly MovementManager mMovementManager;
        private readonly GoldManager mGoldManager;

        // Positions of the Gold Spawns on the map via Grid Positions
        List<Vector2> mGoldSpawns = new List<Vector2>();

        // List of Units in the world
        private List<Unit> mAllyUnits;
        private List<Unit> mEnemyUnits;

        // Units on this world
        private King mKing;
        private Unit mSwordsman;

        internal World()
        {
            mOffSet = new Vector2(0, 0);

            // Import Map
            var mapLevel1 = new TmxMap("Content\\Level1.tmx");
            mMapManager = new TileMapManager(mapLevel1);

            // Initialize the Grid with the TileMap.png
            mGrid = new Grid(mMapManager.GetTextureArray());
            mGrid.SetTileMap(mapLevel1);

            // Initialize the Units
            mKing = new King();
            mSwordsman = new Swordsman();

            // Inits the lists of Units
            mAllyUnits = new List<Unit>() {mKing, mSwordsman};
            mEnemyUnits = new List<Unit>();


            // Initialize the AnimationManager and
            mMovementManager = new MovementManager(mGrid, mAllyUnits, mEnemyUnits);

            // Initialize the spawned Gold
            mGoldSpawns.Add(new Vector2(10, 10));
            mGoldSpawns.Add(new Vector2(20, 20));
            mGoldManager = new GoldManager(mKing, mGoldSpawns);


        }
        internal void Update(GameTime gameTime)
        {
            mMovementManager.Update(gameTime);
            mGoldManager.Update();
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            mGrid.Draw();
            mMovementManager.Draw();
            mGoldManager.Draw(spriteBatch);
        }
            
    }
}
