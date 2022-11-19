using KnightsOfLaCampus.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using KnightsOfLaCampus.Source.Astar;
using KnightsOfLaCampus.Source.GridNew;
using Microsoft.Xna.Framework.Audio;
using TiledSharp;

namespace KnightsOfLaCampus.Source
{
    internal sealed class World
    {
        private readonly Vector2 mOffSet;
        private readonly Grid mGrid;
        private readonly Player mPlayer;
        private readonly TileMapManager mMapManager;


        internal World()
        {
            var soundManager = new SoundManager();
            mOffSet = new Vector2(0, 0);
            // Grid is created can be bigger than Screen.
            // spotDims is how width and height of a spot.
            // startPos is top left of grid it can be -100, -100 if we want to generate bigger map.
            // totalDims is bottom right of grid it can be bigger than Globals.ScreenWidth and Globals.ScreenHeight.
            // call Source/Astar/Grid.cs

            // Demonstration how to Manipulate a grid which is already construct.
            // here a invisible wall was create in the middle of the screen.
            //for (var i = 0; i <= mGrid.GridDims.Y; i++)
            //{
            //    mGrid.mGrid[(int)((Globals.ScreenWidth / 2) / 40)][i].mIfFilled = true;
            //}
            // if LeftMouseClickAndDrag you can draw a maze.

            // Import Map
            var mapLevel1 = new TmxMap("Content\\Maps\\Level1.tmx");
            var tileSet = Globals.Content.Load<Texture2D>(mapLevel1.Tilesets[0].Name.ToString());
            
            mMapManager = new TileMapManager(mapLevel1);

            // Initislaizes the Grid with the TileMap.png
            mGrid = new Grid(mMapManager.GetTextureArray());

            // Import Player call Player.cs
            mPlayer = new Player(mGrid);

            // Test set MusicBackground
            soundManager.ChangeMusic(0);


        }
        internal void Update(GameTime gameTime)
        {

            mPlayer.Update(gameTime);
            mGrid.Update(mOffSet);

        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            //enable this to draw map.
            //mMapManager.Draw();
            mGrid.Draw();
            mPlayer.Draw(spriteBatch);
        }
    }
}
