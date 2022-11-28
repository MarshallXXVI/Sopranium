using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfLaCampus.Source.Interfaces
{
    internal interface IEnemyUnit
    {
        public bool IsDead { get; set; }
        public float HitDist { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public List<Vector2> Path { get; set; }
        public void TakeDamage(float damage);
        public void AudioUpdate();
        public void GraphicsUpdate();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
