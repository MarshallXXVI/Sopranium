using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.Astar;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.UnitsGameObject.Enemies
{
    internal sealed class BadGuy : Enemy
    {
        internal BadGuy(Player target, SoMuchOfSpots field)
        {
            mIfDead = false;
            mHitDist = 16;
            mMaxHp = 1;
            mHp = mMaxHp;
            var animations = new Dictionary<string, Animation>()
            {
                { "up", new Animation(Globals.Content.Load<Texture2D>("UNIT1/up"), 3) },
                { "down", new Animation(Globals.Content.Load<Texture2D>("UNIT1/down"), 3) },
                { "left", new Animation(Globals.Content.Load<Texture2D>("UNIT1/left"), 3) },
                { "right", new Animation(Globals.Content.Load<Texture2D>("UNIT1/right"), 3) }
            };
            mAnimations = animations;
            mAnimationManager = new AnimationManager(animations.First().Value);
            mSaveManager = new SaveManager();
            mSoundManager = new SoundManager();
            mSoundManager.AddSoundEffect("Walk", "Audio\\SoundEffects\\WalkDirt");
            mTarget = target.mKaiser;
            mEnemyField = field;
        }

        public override void Update(GameTime gameTime)
        {
            if (mHp == 0)
            {
                mIfDead = true;
            }
            ApproachTarget(gameTime);
            GraphicsUpdate();
            Velocity = Vector2.Zero;
            AudioUpdate();
            mAnimationManager.Update(gameTime);
        }

        public override void AudioUpdate()
        {
            if (mVelocity != Vector2.Zero)
            {
                mSoundManager.PlaySound("Walk");
            }
            else
            {
                mSoundManager.StopSound("Walk");
            }
        }

        public override void GraphicsUpdate()
        {
            if (Velocity.X > 0 && Math.Abs(Velocity.X) > Math.Abs(Velocity.Y))
            {
                mAnimationManager.Play(mAnimations["right"]);
            }

            if (Velocity.X < 0 && Math.Abs(Velocity.X) > Math.Abs(Velocity.Y))
            {
                mAnimationManager.Play(mAnimations["left"]);
            }

            if (Velocity.Y > 0 && Math.Abs(Velocity.Y) > Math.Abs(Velocity.X))
            {
                mAnimationManager.Play(mAnimations["down"]);
            }

            if (Velocity.Y < 0 && Math.Abs(Velocity.Y) > Math.Abs(Velocity.X))
            {
                mAnimationManager.Play(mAnimations["up"]);
            }
            if (Velocity.Y == 0 && Velocity.X == 0)
            {
                mAnimationManager.Stop();
            }

        }

        public override void TakeDamage(float damage)
        {
            mHp -= damage;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            mAnimationManager?.Draw(spriteBatch, new Vector2(9, 24));
        }
    }
}
