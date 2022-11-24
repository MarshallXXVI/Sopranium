using KnightsOfLaCampus.Managers;
using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Saves;
using KnightsOfLaCampus.Source.GridNew;


namespace KnightsOfLaCampus.Units
{
    internal sealed class King : Unit
    {
        internal Vector2 mVelocity;

        private readonly SoundManager mSoundManager;

        private readonly SaveManager mSaveManager;

        // # TODO replace with animations later
        private Texture2D mKingTexture;

        public int mGold;

        internal King()
        {
            mSaveManager = new SaveManager();
            mSoundManager = new SoundManager();
            mSoundManager.AddSoundEffect("Walk", "Audio\\SoundEffects\\WalkDirt");

            mGold = 10;

            mKingTexture = Globals.Content.Load<Texture2D>(@"Konig");

            // King is always moveable by the player at the begining of the game
            mMoveableByPlayer = true;

            if (SavedVariables.LoadSavedVariables)
            {
                mSaveManager.LoadFromXml();
                mPosition = SavedVariables.KingPositon;
                SavedVariables.LoadSavedVariables = false;
            }
            else
            {
                // Sets the spawn position points to the left upper corner
                mPosition = new Vector2(GridBox.sBoxSize * 30, GridBox.sBoxSize * 16);
            }
        }

        public override void Update(GameTime gameTime)
        {
            GraphicsUpdate();
            AudioUpdate();
            mVelocity = Vector2.Zero;

            // #TODO call SaveManager here and save position of King

            

            SavedVariables.KingPositon = mPosition;
            mSaveManager.SaveToXml();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mKingTexture,
                new Rectangle((int)mPosition.X, (int)mPosition.Y - GridBox.sBoxSize / 4, GridBox.sBoxSize, GridBox.sBoxSize),
                Color.White
                );
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

        }
    }
}

