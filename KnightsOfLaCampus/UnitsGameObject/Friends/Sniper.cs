﻿using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Saves;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Units;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsOfLaCampus.Source.Interfaces;

namespace KnightsOfLaCampus.UnitsGameObject.Friends
{
    internal sealed class Sniper : Friend
    {
        private const int UnitId = 22;
        private const int SniperXOffset = 9;

        private const int SniperYOffset = 24;

        private readonly McTimer mArrowTimer = new McTimer(1200);

        public Sniper()
        {
            mIsDead = false;
            var animations = new Dictionary<string, Animation>()
            {
                { "up", new Animation(Globals.Content.Load<Texture2D>("Sniper\\SniperUp"), 3) },
                { "down", new Animation(Globals.Content.Load<Texture2D>("Sniper\\SniperDown"), 3) },
                { "left", new Animation(Globals.Content.Load<Texture2D>("Sniper\\SniperLeft"), 3) },
                { "right", new Animation(Globals.Content.Load<Texture2D>("Sniper\\SniperRight"), 3) }
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
            CheckIfSelected();
            Shoot(enemies, gameTime);
            Move(gameTime);
            GraphicsUpdate();
            AudioUpdate();
            mAnimationManager.Update(gameTime);
            Velocity = Vector2.Zero;
        }

        public override int Id
        {
            get => UnitId;
            set => throw new NotImplementedException();
        }

        private void Shoot(IEnumerable<IEnemyUnit> enemy, GameTime gameTime)
        {
            mArrowTimer.UpdateTimer(gameTime);
            foreach (var t in enemy.Where(t => Vector2.Distance(Position, t.Position) < 300 && mArrowTimer.Test()))
            {
                GameGlobals.mPassArrow(new Arrow(this, t.Position));
                mArrowTimer.ResetToZero();
            }
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
            mAnimationManager?.Draw(spriteBatch, new Vector2(SniperXOffset, SniperYOffset));
        }
    }
}
