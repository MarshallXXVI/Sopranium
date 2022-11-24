using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Source
{
    internal abstract class EnemyUnit : NormalUnit
    {
        protected int mCost;
        public bool mSelected;
        internal Vector2 mVelocity;
        public abstract bool IsSelected();
        // Moves the enemy towards the ECTS
        public void Approach()
        {
            mPosition += RadialMovement(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2), mPosition, mSpeed);
        }

    }
}
