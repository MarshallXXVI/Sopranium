using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame00.Managers;
using Monogame00.Source.Engine;

namespace Monogame00.Sprites
{
    public class Sprite
    {
        protected Texture2D mTexture;

        protected AnimationManager mAnimationManager;

        protected Dictionary<string, Animation> mAnimations;

        protected Vector2 mProtectedPosition;

        protected Vector2 mProtectedTarget;

        protected MouseState mMouseState;

        public Vector2 Position
        {
            get { return mProtectedPosition; }
            set
            {
                mProtectedPosition = value;

                if (mAnimationManager != null)
                {
                    mAnimationManager.Position = mProtectedPosition;
                }
            }
        }

        public Vector2 mVelocity;

        public float mSpeed = 1f;

        public Input mInput;

        public Sprite(Texture2D texture)
        {
            mTexture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            Move();

            SetAnimation();

            mAnimationManager.Update(gameTime);
            Position += mVelocity;
            mVelocity = Vector2.Zero;

        }

        protected virtual void SetAnimation()
        {
            if (mVelocity.X > 0)
            {
                mAnimationManager.Play(mAnimations["right"]);
            }
            else if (mVelocity.X < 0)
            {
                mAnimationManager.Play(mAnimations["left"]);
            }
            else if (mVelocity.Y > 0)
            {
                mAnimationManager.Play(mAnimations["down"]);
            }
            else if (mVelocity.Y < 0)
            {
                mAnimationManager.Play(mAnimations["up"]);
            }
            else
            {
                mAnimationManager.Stop();
            }
        }

        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(mInput.LeftKeys))
            {
                mVelocity.X = -mSpeed;
            }
            else if (Keyboard.GetState().IsKeyDown(mInput.RightKeys))
            {
                mVelocity.X = mSpeed;
            }
            else if (Keyboard.GetState().IsKeyDown(mInput.UpKeys))
            {
                mVelocity.Y = -mSpeed;
            }
            else if (Keyboard.GetState().IsKeyDown(mInput.DownKeys))
            {
                mVelocity.Y = mSpeed;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (mTexture != null)
            {
                spriteBatch.Draw(mTexture, Position, Color.White);
            } else if (mAnimationManager != null)
            {
                mAnimationManager.Draw(spriteBatch);
            }
            else
            {
                throw new Exception("This is Fucked");
            }
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            mAnimations = animations;
            mAnimationManager = new AnimationManager(mAnimations.First().Value);
        }

        protected virtual void ShortestPath()
        {
            mMouseState = Mouse.GetState();
            mProtectedTarget = mMouseState.Position.ToVector2();
            
            float mouseToSpritesDis = Vector2.Distance(mProtectedPosition, mProtectedTarget);

        }
    }
}
