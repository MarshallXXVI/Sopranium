using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Saves;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.UnitsGameObject.Friends
{
    internal sealed class Kaiser : Friend
    {
        private const int KingXOffset = 9;

        private const int KingYOffset = 24;


        public Kaiser()
        {
            mIsDead = false;
            var animations = new Dictionary<string, Animation>()
            {
                { "up", new Animation(Globals.Content.Load<Texture2D>("Kaiser\\KaiserUp"), 3) },
                { "down", new Animation(Globals.Content.Load<Texture2D>("Kaiser\\KaiserDown"), 3) },
                { "left", new Animation(Globals.Content.Load<Texture2D>("Kaiser\\KaiserLeft"), 3) },
                { "right", new Animation(Globals.Content.Load<Texture2D>("Kaiser\\KaiserRight"), 3) }
            };
            mAnimations = animations;
            mAnimationManager = new AnimationManager(mAnimations.First().Value);
            IsSelected = false;
            mSaveManager = new SaveManager();
            mSoundManager = new SoundManager();
            mSoundManager.AddSoundEffect("Walk", "Audio\\SoundEffects\\WalkDirt");

        }

        public override void Update(GameTime gameTime, List<IEnemyUnit> enemies)
        {
            //TODO ADD CheckIfAttack();
            CheckIfSelected();
            Move(gameTime);
            GraphicsUpdate();
            AudioUpdate();
            mAnimationManager.Update(gameTime);
            Velocity = Vector2.Zero;
        }

        public override void AudioUpdate()
        {
            if (Velocity != Vector2.Zero)
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
            switch (Velocity.X)
            {
                case > 0:
                    mAnimationManager.Play(mAnimations["right"]);
                    break;
                case < 0:
                    mAnimationManager.Play(mAnimations["left"]);
                    break;
                default:
                    {
                        switch (Velocity.Y)
                        {
                            case > 0:
                                mAnimationManager.Play(mAnimations["down"]);
                                break;
                            case < 0:
                                mAnimationManager.Play(mAnimations["up"]);
                                break;
                            default:
                                mAnimationManager.Stop();
                                break;
                        }
                        break;
                    }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            mAnimationManager?.Draw(spriteBatch, new Vector2(KingXOffset, KingYOffset));
        }
    }
}
