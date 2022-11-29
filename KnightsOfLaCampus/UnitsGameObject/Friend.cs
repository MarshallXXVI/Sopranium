using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.UnitsGameObject
{
    /// <summary>
    /// Implementing same ideas with Units but using abstract and inheritance.
    /// abstract Friend class which has IFriendlyUnit interface.
    /// </summary>
    internal abstract class Friend : IFriendlyUnit
    {
        public abstract int Id { get; set; }
        protected bool mIsDead;
        private Vector2 mPosition;

        protected AnimationManager mAnimationManager;

        protected SoundManager mSoundManager;

        protected SaveManager mSaveManager;

        protected Dictionary<string, Animation> mAnimations;

        public List<Vector2> Path { get; set; } = new List<Vector2>();

        /// <summary>
        /// public field can be read or write by external class.
        /// </summary>
        public bool IsSelected { get; set; }
        public bool IsDead { get; set; }
        public Vector2 Velocity { get; set; }

        public Vector2 Position
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

        public abstract void Update(GameTime gameTime, List<IEnemyUnit> enemies);

        // call by interface return status of selected.
        public bool GetIfSelected()
        {
            return IsSelected;
        }

        // always move to Target Position.
        // if there is a path given and have not arrived at destination yet.
        // then let's move.
        protected void Move(GameTime gameTime)
        {
            if (Path == null || 0 > Path.Count - 1)
            {
                return;
            }

            if (MoveTowardsSpot(Path[0], gameTime))
            {
                Path.RemoveAt(0);
            }
        }

        // a helper function to move towards to next position in path.
        private bool MoveTowardsSpot(Vector2 target, GameTime gameTime)
        {
            // if we hit ours next destination return true;
            if (Position == target)
            {
                return true;
            }

            // if not we get next direction and add to ours Sprite Position.
            var tempVector2 = Vector2.Normalize(target - Position);
            Velocity = tempVector2;
            Position += Velocity * 0.1f * gameTime.ElapsedGameTime.Milliseconds;

            // if ours Sprite move pass target.
            // we set ours Sprite position to that targetPosition.
            if (Math.Abs(Vector2.Dot(Velocity,
                    Vector2.Normalize(target - Position)) + 1) < 0.1f)
            {
                Position = target;
            }

            // return true or false;
            return Position == target;
        }

        // this function always check if this Unit is selected or not.
        // mIfSelected is ours flag,
        protected void CheckIfSelected()
        {
            // we check if Left Mouse is Clicked.
            if (!Globals.Mouse.LeftClick())
            {
                return;
            }

            // mouseKingDist is distance between mouse last pick position and Unit position.
            var mouseKingDist = Vector2.Distance(Position, Globals.Mouse.mNewMousePos);
            IsSelected = mouseKingDist switch
            {
                // we set our flag to true if unit was never been selected.
                < 17 when !IsSelected => true,
                // else mouse has been click but not on Unit we set our flag false.
                > 17 => false,
                _ => IsSelected
            };

        }

        public abstract void AudioUpdate();

        public abstract void GraphicsUpdate();

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
