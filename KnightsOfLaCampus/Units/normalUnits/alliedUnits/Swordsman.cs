using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Saves;


namespace KnightsOfLaCampus.Units
{
    internal sealed class Swordsman : AllyUnit
    {

        private const int SwordsmanXOffset = 16;

        private const int SwordsmanYOffset = 50;

        private readonly AnimationManager mAnimationManager;

        private readonly Texture2D mTexture;

        private readonly SoundManager mSoundManager;

        private readonly SaveManager mSaveManager;

        private readonly Dictionary<string, Animation> mAnimations;

        internal Swordsman()
        {
            mSelected = false;
            // mTexture = Globals.Content.Load<Texture2D>("Konig");
            var animations = new Dictionary<string, Animation>()
            {
                { "up", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkTop"), 4) },
                { "down", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkDown"), 4) },
                { "left", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkLeft"), 4) },
                { "right", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkRight"), 4) }
            };
            mAnimations = animations;
            mAnimationManager = new AnimationManager(mAnimations.First().Value);
        }


        public override void Update(GameTime gameTime)
        {
            mPosition += mVelocity;
            GraphicsUpdate();
            AudioUpdate();
            mAnimationManager.Update(gameTime);
            // mVelocity = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            mAnimationManager?.Draw(spriteBatch, new Vector2(SwordsmanXOffset, SwordsmanYOffset));
            // Globals.SpriteBatch.Draw(mTexture, new Rectangle ((int)Position.X - 10, (int)Position.Y - 10, 60, 60), Color.White);
        }

        public override void AudioUpdate()
        {
        }

        public override void GraphicsUpdate()
        {
            switch (mVelocity.X)
            {
                case > 0:
                    mAnimationManager.Play(mAnimations["right"]);
                    break;
                case < 0:
                    mAnimationManager.Play(mAnimations["left"]);
                    break;
                default:
                    {
                        switch (mVelocity.Y)
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

        public override bool IsSelected()
        {
            return mSelected;
        }

        public override Vector2 Position
        {
            get => mPosition;
            set
            {
                mPosition = value;

                if (mAnimationManager != null)
                {
                    mAnimationManager.Position = mPosition;
                }
            }
        }
    }
}

// Will be implement later.

//public override void SpawnPoint(Vector2 position)
//{
//    // Currently empty
//}

//public override void TakeDamage(int damage)
//{
//    mHpBar -= 1;
//}

//public static void AttackOther(Vector2 position)
//{
//    // Currently empty
//}

//public static void CollectGold()
//{
//    // Currently empty
//}

//public static void RepairWall()
//{
//    // can call value of Gold here directly no use of Parameter.
//    // Currently empty
//}
