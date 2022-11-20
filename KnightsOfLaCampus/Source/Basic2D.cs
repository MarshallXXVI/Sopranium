using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;

namespace KnightsOfLaCampus.Source;

internal sealed class Basic2D
{
    private const int Int2 = 2;

    private readonly float mRot;

    private readonly Vector2 mPos, mDims;

    private readonly Texture2D mMyModel;

    internal Basic2D(string path, Vector2 pos, Vector2 dims)
    {
        mRot = 0f;
        mPos = new Vector2(pos.X, pos.Y);
        mDims = new Vector2(dims.X, dims.Y);
        mMyModel = Globals.Content.Load<Texture2D>(path);
    }

    internal void Draw(Vector2 offset)
    {
        if (mMyModel == null)
        {
            return;
        }

        var x = (int) (mPos.X + offset.X);
        var y = (int)(mPos.Y + offset.Y);
        Globals.SpriteBatch.Draw(mMyModel, new Rectangle(x, y,
            (int) mDims.X, (int) mDims.Y), null, Color.White, mRot, new Vector2((float)mMyModel.Bounds.Width / Int2, (float) mMyModel.Bounds.Height / Int2), new SpriteEffects(), 0);
    }

}