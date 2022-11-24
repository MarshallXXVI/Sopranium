using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KnightsOfLaCampus.Source
{
    internal abstract class NormalUnit : Unit
    {
        protected float mSpeed;
        protected float mAttackRange;

        public void AttackOther(Unit unit)
        {
            if (GetDistance(mPosition, unit.mPosition) > mAttackRange)
            {
                mPosition += RadialMovement(unit.mPosition, mPosition, mSpeed);
            }
            else
            {
                unit.TakeDmg(1);
            }
        }

        public float GetDistance(Vector2 pos, Vector2 target)
        {
            return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
        }

        // Returns the Movement from pos towards focus dependant on the speed
        public Vector2 RadialMovement(Vector2 focus, Vector2 pos, float speed)
        {
            float dist = GetDistance(pos, focus);

            if (dist <= speed)
            {
                return focus - pos;
            }
            else
            {
                return (focus - pos) * speed / dist;
            }
        }

    }
}
