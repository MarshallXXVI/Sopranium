using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Saves;
using System;
using KnightsOfLaCampus.Source.GridNew;


namespace KnightsOfLaCampus.Units
{
    internal sealed class Swordsman : AllyUnit
    {

        private readonly Texture2D mTexture;

        // private readonly SoundManager mSoundManager;

        // private readonly SaveManager mSaveManager;

        internal Swordsman()
        {
            mAlive = true;
            mHpBar = 5;
            mSpeed = 2.0f;
            mSelected = false;
            mTexture = Globals.Content.Load<Texture2D>("Schwertkampfer");
            mPosition = new Vector2(GridBox.sBoxSize * 15, GridBox.sBoxSize * 15);
            mVelocity = new Vector2(1, 0);

            mMoveableByPlayer = false;
        }


        public override void Update(GameTime gameTime)
        {
            // mPosition += mVelocity;
            GraphicsUpdate();
            AudioUpdate();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture,
                new Rectangle((int)mPosition.X, (int)mPosition.Y, 32, 32),
                Color.White
                );
        }

        public override void AudioUpdate()
        {
        }

        public override void GraphicsUpdate()
        {

            
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
            }
        }
    }
}
