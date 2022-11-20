using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Managers;

// this will be construct in Unit (which has Animation).
// to fix:: must be consider when some Sprite has different Dimension.
internal sealed class AnimationManager
{
    private Animation mAnimation;

    private float mTimer;

    public Vector2 Position { get; set; }

    public AnimationManager(Animation animation)
    {
        mAnimation = animation;
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 offset)
    {
        // correct position to draw should be original position - offset of that Sprite width/2 and height/2.

        spriteBatch.Draw(mAnimation.Texture,
            Position - offset,
            new Rectangle(mAnimation.CurrentFrame * mAnimation.FrameWidth,
                0,
                mAnimation.FrameWidth,
                mAnimation.FrameHeight),
            Color.White);
    }
    public void Play(Animation animation)
    {
        if (mAnimation == animation)
        {
            return;
        }
        mAnimation = animation;
        mAnimation.CurrentFrame = 0;
        mTimer = 0;
    }

    public void Stop()
    {
        mTimer = 0f;

        mAnimation.CurrentFrame = 0;
    }

    public void Update(GameTime gameTime)
    {
        mTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (mTimer > mAnimation.FrameSpeed)
        {
            mTimer = 0f;

            mAnimation.CurrentFrame++;

            if (mAnimation.CurrentFrame >= mAnimation.FrameCount)
            {
                mAnimation.CurrentFrame = 0;
            }
        }
    }
}