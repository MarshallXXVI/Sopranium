using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using KnightsOfLaCampus.Source;
using TiledSharp;

namespace KnightsOfLaCampus.Managers
{
    internal sealed class TileMapManager
    {
        private readonly TmxMap mTmxMap;    // Imported Map created with Tiled
        private readonly Texture2D mTileSet;    // Imported tile-set used in Tiled to create the map
        private readonly int mNumberOfColumnsInTileSet; // Number of columns in the tile-set

        /// <summary>
        /// Constructor Constructor of the class TileMapManager which loads a TmxMap with the corresponding tileset.
        /// </summary>
        /// <param name="pMap"></param>
        public TileMapManager(TmxMap pMap)
        {
            mTmxMap = pMap;
            mTileSet = Globals.Content.Load<Texture2D>(pMap.Tilesets[0].Name);
            mNumberOfColumnsInTileSet = mTileSet.Width / mTmxMap.TileWidth;
        }

        /// <summary>
        /// Creates an 2D Array based on a TileMap and TmXMap  
        /// </summary>
        public Texture2D[,] GetTextureArray()
        {
            // The final texture Array map
            var resTextureMap = new Texture2D[mTmxMap.Width, mTmxMap.Height];

            // Go through all the tiles of the first layer and draw them in the right position.
            for (var j = 0; j < mTmxMap.Layers[0].Tiles.Count; j++)
            {
                var tileId = mTmxMap.Layers[0].Tiles[j].Gid;
                if (tileId == 0)
                {
                    continue;
                }

                // Save the position of the tile in the tile-set (image) temporarily.
                var columnFromTileInTileSet = (tileId - 1) % mNumberOfColumnsInTileSet;
                var rowFromTileInTileSet = (int)Math.Floor((tileId - 1) / (double)mNumberOfColumnsInTileSet);   // Math.Floor => rounds off to the next int

                // Temporarily save the position of the tile in the array, which will be returned later.
                var tilePositionX = (j % mTmxMap.Width);
                var tilePositionY = Math.Floor(j / (double)mTmxMap.Width);

                // Create the tile source Rectangle for the output of the map.
                var sourceRectangle = new Rectangle((mTmxMap.TileWidth) * columnFromTileInTileSet, (mTmxMap.TileHeight) * rowFromTileInTileSet, mTmxMap.TileWidth, mTmxMap.TileHeight);

                // Extracts the texture from the mTileSet based on the sourceRectangle 
                var cropTexture = new Texture2D(Globals.Device, sourceRectangle.Width, sourceRectangle.Height);
                var data = new Color[sourceRectangle.Width * sourceRectangle.Height];
                mTileSet.GetData(0, sourceRectangle, data, 0, data.Length);
                cropTexture.SetData(data);

                // Try to get the texture from this one
                resTextureMap[tilePositionX, (int)tilePositionY] = cropTexture;
            }
            
            return resTextureMap;
        }
    }
}