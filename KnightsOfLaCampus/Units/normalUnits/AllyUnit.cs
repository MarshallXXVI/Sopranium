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

    }
}
