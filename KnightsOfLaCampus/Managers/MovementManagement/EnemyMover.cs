using System.Collections.Generic;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.GridNew;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Managers.MovementManagement
{
    /// <summary>
    /// The Logic how the Enemy Units move. Enemies follow a path, that is already assigned 
    /// </summary>
    internal sealed class EnemyMover : UnitMover
    {
        public EnemyMover(Grid grid, Unit unit) : base(grid, unit)
        {
            #region Declaration

            // Inits the path for the unit
            mUnit.mPath = new Stack<GridNode>();

            // #TODO: Generated fix path here

            // Sets the first node
            if (mUnit.mPath.Count != 0)
            {
                mNext = mUnit.mPath.Pop().Position;
            }

            // Sets the unit's position to the start of the path
            mUnit.mPosition = mGrid.GridToPixelPosition(mNext);

            #endregion
        }

        public override void Update(GameTime gameTime)
        {
            // Movement behaviour of the Unit, it just moves along a fix path
            MoveUnitAlongPath();

            // Updates the Unit to save the position 
            mUnit.Update(gameTime);
        }

        public override void Draw()
        {
            mUnit.Draw(Globals.SpriteBatch);
        }
    }
}
