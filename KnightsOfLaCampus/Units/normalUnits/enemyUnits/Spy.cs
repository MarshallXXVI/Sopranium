using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace KnightsOfLaCampus.Units
{
    internal sealed class Spy : EnemyUnit
    {


        private readonly Texture2D mTexture;

        // private readonly SoundManager mSoundManager;

        // private readonly SaveManager mSaveManager;

        // private readonly Dictionary<string, Animation> mAnimations;

        internal Spy()
        {
            mAlive = true;
            mHpBar = 5;
            mSelected = false;
            mTexture = Globals.Content.Load<Texture2D>("Schwertkampfer");
            mPosition = new Vector2(Globals.ScreenWidth - 100, 100);
            mVelocity = new Vector2(-1, 0);
        }


        public override void Update(GameTime gameTime)
        {
            mPosition += mVelocity;
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

    }
}


