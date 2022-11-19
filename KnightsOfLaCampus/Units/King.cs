using System;
using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Saves;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;


namespace KnightsOfLaCampus.Units
{
    internal sealed class King : Unit
    {
        public bool mIfSelected;

        internal Vector2 mVelocity;

        private const int KingXOffset = 16;

        private const int KingYOffset = 50;

        private readonly AnimationManager mAnimationManager;

        //use to test the actual position with Animation.
        //private readonly Texture2D mTexture;

        private readonly SoundManager mSoundManager;

        private readonly SaveManager mSaveManager;

        private readonly Dictionary<string, Animation> mAnimations;

        internal King()
        {
            var animations = new Dictionary<string, Animation>()
            {
                { "up", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkTop"), 4) },
                { "down", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkDown"), 4) },
                { "left", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkLeft"), 8) },
                { "right", new Animation(Globals.Content.Load<Texture2D>("King/KingWalkRight"), 8) }
            };
            mAnimations = animations;
            mAnimationManager = new AnimationManager(mAnimations.First().Value);
            //mTexture = Globals.Content.Load<Texture2D>("2D/Red");
            mSaveManager = new SaveManager();
            mSoundManager = new SoundManager();
            mSoundManager.AddSoundEffect("Walk", "Audio\\SoundEffects\\WalkDirt");
        }

        public override void Update(GameTime gameTime)
        {
            CheckIfSelected();
            GraphicsUpdate();
            AudioUpdate();
            mAnimationManager.Update(gameTime);
            mVelocity = Vector2.Zero;

            // #TODO call SaveManager here and save position of King

            if (SavedVariables.LoadSavedVariables)
            {
                mSaveManager.LoadFromXml();
                Position = SavedVariables.KingPositon;
                SavedVariables.LoadSavedVariables = false;
            }

            SavedVariables.KingPositon = Position;
            mSaveManager.SaveToXml();
        }


        // this function always check if this Unit is selected or not.
        // mIfSelected is ours flag,
        private void CheckIfSelected()
        {
            // we check if Left Mouse is Clicked.
            if (Globals.Mouse.LeftClick())
            {
                // mouseKingDist is distance between mouse last pick position and Unit position.
                float mouseKingDist = Vector2.Distance(this.Position, Globals.Mouse.mNewMousePos);
                // we set our flag to true if unit was never been selected.
                if (mouseKingDist < 15 && !mIfSelected)
                {
                    mIfSelected = true;
                }
                // else mouse has been click but not on Unit we set our flag false.
                else if (mouseKingDist > 15)
                {
                    mIfSelected = false;
                }
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            mAnimationManager?.Draw(spriteBatch, new Vector2(KingXOffset, KingYOffset));
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

        internal Vector2 Position
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
