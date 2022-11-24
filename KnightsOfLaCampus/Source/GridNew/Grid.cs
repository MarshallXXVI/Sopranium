using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KnightsOfLaCampus.Source.GridNew
{
    internal sealed class Grid
    {
        #region Declarations

        // The Grid itself is an 2D array of GridBoxes
        private readonly GridBox[,] mGrid;

        // An 2D array of textures to fill in the boxes
        private readonly Texture2D[,] mTextures;

        // The pathfinder to calculate the shortest path
        private AstarGrid mAstarGrid;

        // The Dimensions of the grid
        public Vector2 GridDimensions { get; set; }

        private const int CollisionFieldId = 16;

        #endregion

        /// <summary>
        /// CONSTRUCTOR 1: In case textures need to be loaded
        /// </summary>
        /// <param name="textures"></param>
        public Grid(Texture2D[,] textures = null)
        {
            #region Init

            if (textures != null)
            {
                mTextures = textures;
                // Reads the dimensions of the textures array
                int sizeX = textures.GetLength(0);
                int sizeY = textures.GetLength(1);

                GridDimensions = new Vector2(sizeX, sizeY);

                mGrid = new GridBox[sizeX, sizeY];

                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        mGrid[i, j] = new GridBox(new Vector2(GridBox.sBoxSize * i, GridBox.sBoxSize * j), textures[i, j]);
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// CONSTRUCTOR 2: In case a raw grid without textures is needed
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public Grid(int sizeX, int sizeY)
        {
            #region Init
            mGrid = new GridBox[sizeX, sizeY];
            GridDimensions = new Vector2(sizeX, sizeY);

            // Fills the Grid with empty GridBoxes
            for (var i = 0; i < sizeX; i++)
            {
                for (var j = 0; j < sizeY; j++)
                {
                    mGrid[i, j] = new GridBox(new Vector2(GridBox.sBoxSize * i, GridBox.sBoxSize * j));
                }
            }
            
            #endregion
        }

        /// <summary>
        /// Reads out other information from the map, such as blockades, in order to be able to use it later in the grid.
        /// </summary>
        /// <param name="tmxMap"></param>
        public void SetTileMap(TmxMap tmxMap)
        {
            #region Implementation

            // Go through the collision layer and mark the blocked fields
            for (var j = 0; j < tmxMap.Layers[1].Tiles.Count; j++)
            {
                var tileId = tmxMap.Layers[1].Tiles[j].Gid;

                // Extract the position of the blockade
                var tilePositionX = (j % tmxMap.Width);
                var tilePositionY = (int)Math.Floor(j / (double)tmxMap.Width);

                // TileId = 16 are collision fields
                CollisionStateAt(new Vector2(tilePositionX, tilePositionY), tileId == CollisionFieldId);
            }

            #endregion
        }

        /// <summary>
        /// Changes the texture of one Grid box at given grid coordinates and
        /// doesn't create a new GridBox
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="texture"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void SetBoxTextureAt(Vector2 pos, Texture2D texture = null)
        {
            #region Implementation

            // To avoid index error
            try
            {
                mGrid[(int)pos.X, (int)pos.Y].BoxTexture = texture;
            }
            catch (IndexOutOfRangeException e)
            {
                throw new IndexOutOfRangeException("Please insert x and y that are in range of the grid", e);
            }

            #endregion
        }

        /// <summary>
        /// Load a 2D array of textures in the map
        /// </summary>
        /// <param name="textures"></param>
        private void LoadTextures(Texture2D[,] textures)
        {
            #region Implementation

            if (textures != null)
            {
                // Reads the dimensions of the textures array
                var sizeX = textures.GetLength(0);
                var sizeY = textures.GetLength(1);

                for (var i = 0; i < sizeX; i++)
                {
                    for (var j = 0; j < sizeY; j++)
                    {
                        // new GridBox(sBoxSize*i, sBoxSize*j, sBoxSize, sBoxSize, boxTexture);
                        SetBoxTextureAt(new Vector2(i, j), textures[i, j]);
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// Sets the collision state at given grid coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="state"></param>
        public void CollisionStateAt(Vector2 pos, bool state)
        {
            #region Implementation
            mGrid[(int)pos.X, (int)pos.Y].CollisionOn = state;

            #endregion
        }

        public Stack<GridNode> GetShortestPath(Vector2 start, Vector2 end)
        {
            #region Implementation

            mAstarGrid = new AstarGrid(this);
            var path = mAstarGrid.FindPath(start, end);

            // For some reason all the textures are going to be removed so 
            // we need to load them again
            LoadTextures(mTextures);
            if (path.Count != 0)
            {
                return path;
            }

            // If there is no path it returns an empty stack
            return new Stack<GridNode>();

            #endregion
        }

        /// <summary>
        /// Gets the GridBox at the given grid position position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public GridBox GetBoxAt(Vector2 pos)
        {
            #region Implementation

            // to avoid index error
            try
            {
                return mGrid[(int)pos.X, (int)pos.Y];
            }
            catch (Exception e)
            {
                throw new IndexOutOfRangeException("Please insert x and y that are in range of the grid", e);
            }

            #endregion
        }

        /// <summary>
        /// Creates a new GridBox at the grid position (replaces the old one)
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="gridBox"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void SetBoxAt(Vector2 pos, GridBox gridBox)
        {
            #region Implementation

            // to avoid index error
            try
            {
                // Changes the position such that it fits in the grid
                gridBox.BoxFrame = new Rectangle(
                    GridBox.sBoxSize * (int)pos.X, 
                    GridBox.sBoxSize * (int)pos.Y,
                    GridBox.sBoxSize, 
                    GridBox.sBoxSize
                );
                mGrid[(int)pos.X, (int)pos.Y] = gridBox;
            }
            catch (Exception e)
            {
                throw new IndexOutOfRangeException("Please insert x and y that are in range of the grid", e);
            }

            #endregion
        }

        /// <summary>
        /// Returns the grid position for given pixel coordinates
        /// </summary>
        /// <param name="pixPos"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Vector2 PixelToGridPosition(Vector2 pixPos)
        {
            #region Implementation

            for (var i = 0; i < GridDimensions.X; i++)
            {
                for (var j = 0; j < GridDimensions.Y; j++)
                {
                    if (mGrid[i, j].ContainsPixel((int)pixPos.X, (int)pixPos.Y))
                    {
                        return new Vector2(i, j);
                    }
                }
            }

            throw new Exception("Position must be in range of the grid");

            #endregion
        }

        /// <summary>
        /// Returns the pixel position for given grid coordinates
        /// Important: The pixel position is the upper left corner of the grid box
        /// </summary>
        /// <param name="gridPos"></param>
        /// <returns></returns>
        public Vector2 GridToPixelPosition(Vector2 gridPos)
        {
            return GetBoxAt(gridPos).GetPosition();
        }

        public void Draw()
        {
            #region Implementation

            // Reads the dimensions of the textures array
            var sizeX = (int)GridDimensions.X;
            var sizeY = (int)GridDimensions.Y;

            // Draws each GridBox if its not null
            for (var i = 0; i < sizeX; i++)
            {
                for (var j = 0; j < sizeY; j++)
                {
                    mGrid[i, j].Draw();
                }
            }

            #endregion
        }
    }
}
