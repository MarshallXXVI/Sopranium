using System;
using System.Collections.Generic;
using KnightsOfLaCampus.Source.GridNew;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Managers.MovementManagement
{
    /// <summary>
    /// Moves one unit a given or user generated path
    /// </summary>
    internal abstract class UnitMover
    {
        #region Declaration

        // The Grid that the calcualtions are made on
        protected readonly Grid mGrid;

        // The next node of the path
        protected Vector2 mNext;

        // The Unit, that the animation manager deals with
        protected readonly Unit mUnit;

        #endregion

        /// <summary>
        /// Constructor 1:
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="unit"></param>
        protected UnitMover(Grid grid, Unit unit)
        {
            #region Init

            // Inits the unit and the grid
            mGrid = grid;
            mUnit = unit;

            #endregion
        }

        /// <summary>
        /// Moves the Unit along a given Path
        /// </summary>
        protected void MoveUnitAlongPath()
        {
            #region Implementation

            // Calculate the direction vector based on the position of the unit 
            // and the next nodes position

            Vector2 current = mUnit.mPosition;
            Vector2 next = mGrid.GridToPixelPosition(mNext);

            // If the unit reaches the next Node, its time to pop a new one
            if (next - current == new Vector2(0, 0))
            {
                if (mUnit.mPath.Count != 0)
                {
                    mNext = mUnit.mPath.Pop().Position;
                }
            }
            // In case there is no path set yet we need to make sure not to normalize 
            // a (0, 0) vector, which is going to cause a NaN and later an exception
            else
            {
                // In this case its a fixed speed value, we can change that later
                mUnit.mDirection = next - current;
                mUnit.mDirection.Normalize();
                if ((next - current).Length() < Math.Ceiling(2f))
                {
                    mUnit.mPosition.X = (float)Math.Floor(current.X);
                    mUnit.mPosition.Y = (float)Math.Floor(current.Y);

                    mUnit.mPosition += mUnit.mDirection;
                }
                else
                {
                    mUnit.mPosition += mUnit.mDirection * 2f;
                }

                #endregion
            }
        }

        

        /// <summary>
        /// Calculates the interpolated pixel path for a given grid path, such that
        /// each pixel of the path is now in the output path
        /// </summary>
        /// <param name="gridPath"></param>
        /// <returns></returns>
        private Stack<GridNode> GetInterpolationPath(Stack<GridNode> gridPath)
        {
            // #TODO add you implementation here
            return gridPath;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();

    }
}
