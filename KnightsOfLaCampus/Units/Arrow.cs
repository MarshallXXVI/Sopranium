using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.Interfaces;
using KnightsOfLaCampus.UnitsGameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Units
{
    internal sealed class Arrow
    {
        public bool mIfDead;
        private readonly McTimer mTimer;
        private readonly float mSpeed;
        private readonly float mRotation;
        private Vector2 mPosition;
        private readonly Vector2 mDirection;
        private readonly Texture2D mTexture;
        public Arrow(IFriendlyUnit owner, Vector2 target)
        {
            mTexture = Globals.Content.Load<Texture2D>("Arrow");
            mIfDead = false;
            mPosition = owner.Position;
            mSpeed = 5f;
            mRotation = RotateTowards(mPosition, target);
            mDirection = target - owner.Position;
            mDirection.Normalize();
            mTimer = new McTimer(6000);
        }

        public void Update(List<IEnemyUnit> enemy, GameTime gameTime)
        {
            mPosition += mDirection * mSpeed;
            mTimer.UpdateTimer(gameTime);
            if (mTimer.Test())
            {
                mIfDead = true;
            }

            if (HitSomeEnemy(enemy))
            {
                mIfDead = true;
            }
        }

        private bool HitSomeEnemy(IReadOnlyList<IEnemyUnit> enemy)
        {
            foreach (var t in enemy)
            {
                if (Vector2.Distance(mPosition, t.Position) < t.HitDist)
                {
                    t.TakeDamage(1);
                    return true;
                }
            }

            return false;
        }


        private static float RotateTowards(Vector2 pos, Vector2 focus)
        {

            float h, sineTheta, angle;
            if (pos.Y - focus.Y != 0)
            {
                h = (float)Math.Sqrt(Math.Pow(pos.X - focus.X, 2) + Math.Pow(pos.Y - focus.Y, 2));
                sineTheta = (float)(Math.Abs(pos.Y - focus.Y) / h);
            }
            else
            {
                h = pos.X - focus.X;
                sineTheta = 0;
            }

            angle = (float)Math.Asin(sineTheta);

            // Drawing diagonial lines here.
            //Quadrant 2
            if (pos.X - focus.X > 0 && pos.Y - focus.Y > 0)
            {
                angle = (float)(Math.PI * 3 / 2 + angle);
            }
            //Quadrant 3
            else if (pos.X - focus.X > 0 && pos.Y - focus.Y < 0)
            {
                angle = (float)(Math.PI * 3 / 2 - angle);
            }
            //Quadrant 1
            else if (pos.X - focus.X < 0 && pos.Y - focus.Y > 0)
            {
                angle = (float)(Math.PI / 2 - angle);
            }
            else if (pos.X - focus.X < 0 && pos.Y - focus.Y < 0)
            {
                angle = (float)(Math.PI / 2 + angle);
            }
            else if (pos.X - focus.X > 0 && pos.Y - focus.Y == 0)
            {
                angle = (float)Math.PI * 3 / 2;
            }
            else if (pos.X - focus.X < 0 && pos.Y - focus.Y == 0)
            {
                angle = (float)Math.PI / 2;
            }
            else if (pos.X - focus.X == 0 && pos.Y - focus.Y > 0)
            {
                angle = (float)0;
            }
            else if (pos.X - focus.X == 0 && pos.Y - focus.Y < 0)
            {
                angle = (float)Math.PI;
            }

            return angle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, new Rectangle((int)mPosition.X, (int)mPosition.Y, 32, 8),null , Color.White, mRotation, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
