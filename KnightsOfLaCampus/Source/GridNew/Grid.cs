using System;
using System.Collections.Generic;
using System.Threading;
using KnightsOfLaCampus.Source.Astar;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KnightsOfLaCampus.Source.GridNew
{
    internal class Grid
    {
        #region Declarations

        // The Grid itself is an 2D array of GridBoxes
        public readonly GridBox[,] mGrid;

        // An 2D array of textures to fill in the boxes
        private Texture2D[,] mTextures;

        // The Dimensions of the grid
        public Vector2 GridDimensions { get; set; }

        // SameGridButCalculateAStar
        public readonly AlotOfSpots mAlotOfSpots;

        public List<Vector2> mPath;

        #endregion

        // CONSTRUCTOR 1: In case textures need to be loaded
        public Grid(Texture2D[,] textures = null)
        {
            #region Init

            if (textures != null)
            {
                mTextures = textures;
                mAlotOfSpots = new AlotOfSpots(new Vector2(32, 32),
                    new Vector2(0, 0),
                    new Vector2(Globals.ScreenWidth, Globals.ScreenHeight));
                // Reads the dimensions of the textures array
                int sizeX = textures.GetLength(0);
                int sizeY = textures.GetLength(1);

                GridDimensions = new Vector2(sizeX, sizeY);

                mGrid = new GridBox[sizeX, sizeY];

                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        // new GridBox(mBoxSize*i, mBoxSize*j, mBoxSize, mBoxSize, boxTexture);
                        mGrid[i, j] = new GridBox(new Vector2(GridBox.mBoxSize * i, GridBox.mBoxSize * j), textures[i, j]);
                    }
                }
            }

            #endregion
        }

        // CONSTRUCTOR 2: In case a raw grid without textures is needed
        public Grid(int sizeX, int sizeY)
        {
            #region Init
            mAlotOfSpots = new AlotOfSpots(new Vector2(32, 32),
                new Vector2(0, 0),
                new Vector2(Globals.ScreenWidth, Globals.ScreenHeight));
            mGrid = new GridBox[sizeX, sizeY];
            GridDimensions = new Vector2(sizeX, sizeY);

            // Fills the Grid with empty GridBoxes
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    // new GridBox(mBoxSize*i, mBoxSize*j, mBoxSize, mBoxSize, boxTexture);
                    mGrid[i, j] = new GridBox(new Vector2(GridBox.mBoxSize * i, GridBox.mBoxSize * j));
                }
            }
            
            #endregion
        }

        // To pass the tile map later and clone the informations to the grid
        public void SetTileMap(TmxMap tmxMap)
        {
            // #TODO Jana: 
            // Hier kannst du gerne das Einlesen der TmxMap in das Grid machen, sodass im Grid
            // die Kollissionen der GridBoxen (CollisionOn) auf True sind, wo sie auch im Kollissionslayer sind.

            // Go through the collision layer and mark the blocked fields
            for (var j = 0; j < tmxMap.Layers[1].Tiles.Count; j++)
            {
                var tileId = tmxMap.Layers[1].Tiles[j].Gid;

                // Maybe right maybe wrong
                var tilePositionX = (j % tmxMap.Width);
                var tilePositionY = (int)Math.Floor(j / (double)tmxMap.Width);

                // TileId = 16 are collision fields
                if (tileId == 16)
                {
                    CollisonStateAt(tilePositionX, tilePositionY, true);
                }
                else
                {
                    CollisonStateAt(tilePositionX, tilePositionY, false);
                }
            }
        }


        // Sets the texture of a GridBox at grid position x, y
        // this doesn't create a new box
        public void SetBoxTextureAt(int x, int y, Texture2D texture = null)
        {
            #region Implementation

            // To avoid index error
            try
            {
                mGrid[x, y].BoxTexture = texture;
            }
            catch (IndexOutOfRangeException e)
            {
                throw new IndexOutOfRangeException("Please insert x and y that are in range of the grid", e);
            }

            #endregion
        }

        // Fills the entire Grid with only one texture
        public void FillGridWith(Texture2D texture = null)
        {
            #region Implementation

            for (int i = 0; i < GridDimensions.X; i++)
            {
                for (int j = 0; j < GridDimensions.Y; j++)
                {
                    SetBoxTextureAt(i, j, texture);
                }
            }

            #endregion
        }

        // Sets the collison state of the GridBox at grid position x, y
        public void CollisonStateAt(int x, int y, bool state)
        {
            #region Implementation
            mAlotOfSpots.mGrid[x][y].mIfFilled = state;
            mGrid[x, y].CollisionOn = state;

            #endregion
        }

        // Gets the GridBox at the given position
        public GridBox GetBoxAt(int x, int y)
        {
            #region Implementation

            // to avoid index error
            try
            {
                return mGrid[x, y];
            }
            catch (Exception e)
            {
                throw new IndexOutOfRangeException("Please insert x and y that are in range of the grid", e);
            }

            #endregion
        }

        // Creates a new GridBox at grid position x, y
        public void SetBoxAt(int x, int y, GridBox gridBox)
        {
            #region Implementation

            // to avoid index error
            try
            {
                // Changes the position such that it fits in the grid
                gridBox.BoxFrame = new Rectangle(
                    GridBox.mBoxSize * x, 
                    GridBox.mBoxSize * y,
                    GridBox.mBoxSize, 
                    GridBox.mBoxSize
                );
                mGrid[x, y] = gridBox;
            }
            catch (Exception e)
            {
                throw new IndexOutOfRangeException("Please insert x and y that are in range of the grid", e);
            }

            #endregion
        }

        // have not tested yet.
        public List<Vector2> GenPath(Vector2 start, Vector2 target)
        {
            return mAlotOfSpots.GetPathFromGrid(start, target);
        }

        public void Update(Vector2 offset)
        {
            mAlotOfSpots?.Update(offset);
        }

        public void Draw()
        {
            #region Implementation
            // mAlotOfSpots.DrawGrid(Vector2.Zero);
            // Reads the dimensions of the textures array
            int sizeX = (int)GridDimensions.X;
            int sizeY = (int)GridDimensions.Y;

            // Draws each GridBox if its not null
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    mGrid[i, j].Draw();
                }
            }

            #endregion
        }
    }
}
