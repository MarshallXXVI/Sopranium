using System.Collections.Generic;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.GridNew;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace KnightsOfLaCampus.Managers.MovementManagement
{
    /// <summary>
    /// Manages the movement of a list of Units 
    /// </summary>
    internal sealed class MovementManager
    {
        // The lists of Units the manager has to deal with
        private readonly List<Unit> mAllyUnits;
        private readonly List<Unit> mEnemyUnits;

        // The Grid, that the units walk on
        private readonly Grid mGrid;

        // The unit movers to perform the movement
        private readonly List<UnitMover> mAllyMovers;
        private readonly List<UnitMover> mEnemyMovers;

        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="allyUnits"></param>
        /// <param name="enemyUnits"></param>
        public MovementManager(Grid grid, List<Unit> allyUnits, List<Unit> enemyUnits)
        {
            // Inits the List of UnitMovers
            mAllyMovers = new List<UnitMover>();
            mEnemyMovers = new List<UnitMover>();

            mGrid = grid;
            mAllyUnits = allyUnits;
            mEnemyUnits = enemyUnits;

            // Adds for each unit one AllyMover
            foreach (var unit in mAllyUnits)
            {
                mAllyMovers.Add(new AllyMover(mGrid, unit));
            }

            // Adds for each unit one EnemyMover
            foreach (var unit in mEnemyUnits)
            {
                mEnemyMovers.Add(new EnemyMover(mGrid, unit));
            }
        }

        /// <summary>
        /// Updates each UnitMover of the list
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Updates the AllyMovers
            foreach (var mover in mAllyMovers)
            {
                ChangeSelectedUnit();
                mover.Update(gameTime);
            }

            // Updates the EnemyMovers
            foreach (var mover in mEnemyMovers)
            {
                // TODO: Implement Enemy selection behaviour here
                mover.Update(gameTime);
            }

        }
        /// <summary>
        /// Manages which Unit is currently moveable by the player and make also
        /// sure there is only one Unit moveable. This is only for Ally Units for now !
        /// </summary>
        private void ChangeSelectedUnit()
        {
            if (Globals.Mouse.LeftClick())
            {
                // If the mouse us clicked reads the position and the corresponding grid position
                var mousePosition = Mouse.GetState().Position;
                var mouseGridPosition = mGrid.PixelToGridPosition(new (mousePosition.X, mousePosition.Y));

                // Now marks the Units as playable if there is a Unit at the gridPosition
                foreach (var unit in mAllyUnits)
                {
                    var unitGridPosition = mGrid.PixelToGridPosition(unit.mPosition);
                    if (mouseGridPosition == unitGridPosition)
                    {
                        // Make sure each Unit is unmoveable but the selected one
                        // #TODO Runtime is O(n²) here -> BAD
                        DisableAllUnitMovement();

                        // Enables the Movement for the selected Unit
                        unit.mMoveableByPlayer = true;
                    }
                }
            }
        }

        /// <summary>
        /// Makes each Unit unmoveable by the player
        /// </summary>
        private void DisableAllUnitMovement()
        {
            foreach (var unit in mAllyUnits)
            {
                unit.mMoveableByPlayer = false;
            }
        }

        /// <summary>
        /// Draws each UnitMover of the list
        /// </summary>
        public void Draw()
        {
            // Draws the ally units
            foreach (var mover in mAllyMovers)
            {
                mover.Draw();
            }

            // Draws the enemy units
            foreach (var mover in mEnemyMovers)
            {
                mover.Draw();
            }
        }
    }
}
