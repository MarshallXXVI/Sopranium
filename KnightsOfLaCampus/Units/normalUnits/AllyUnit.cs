using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Source
{
    internal abstract class AllyUnit : NormalUnit
    {
        protected int mCost;
        public bool mSelected;
        public abstract bool IsSelected();
        public abstract Vector2 Position { get; set; }
        internal Vector2 mVelocity;

        public void Defend(Unit unit)
        {
            if (GetDistance(unit.mPosition, mPosition) < mAttackRange)
            {
                unit.TakeDmg(1);
            }
        }
        public void Movable(bool isDay, Vector2 coordinate)
        {
            if (isDay && mSelected)
            {
                mPosition += RadialMovement(coordinate, mPosition, mSpeed);
            }
        }

    }
}
