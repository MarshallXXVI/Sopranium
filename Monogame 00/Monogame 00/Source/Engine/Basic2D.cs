#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Monogame_00;
#endregion


namespace Monogame_00
{
    public class Basic2d
    {
        public float mRot;

        public Vector2 mPos, mDims, mFrameSize;

        public Texture2D mMyModel;

        public Basic2d(string path, Vector2 pos, Vector2 dims)
        {
            mPos = new Vector2(pos.X, pos.Y);
            mDims = new Vector2(dims.X, dims.Y);
            mRot = 0.0f;

            mMyModel = Globals.mContent.Load<Texture2D>(path);
        }

        public virtual void Update(Vector2 offset)
        {

        }

        public virtual bool Hover(Vector2 offset)
        {
            return HoverImg(offset);
        }

        public virtual bool HoverImg(Vector2 offset)
        {
            Vector2 mousePos = new Vector2(Globals.mMouse.mNewMousePos.X, Globals.mMouse.mNewMousePos.Y);

            if (mousePos.X >= (mPos.X + offset.X) - mDims.X / 2 && mousePos.X <= (mPos.X + offset.X) + mDims.X / 2 && mousePos.Y >= (mPos.Y + offset.Y) - mDims.Y / 2 && mousePos.Y <= (mPos.Y + offset.Y) + mDims.Y / 2)
            {
                return true;
            }

            return false;
        }

        public virtual void Draw(Vector2 offset)
        {
            if (mMyModel != null)
            {
                Globals.mSpriteBatch.Draw(mMyModel, new Rectangle((int)(mPos.X + offset.X), (int)(mPos.Y + offset.Y), (int)mDims.X, (int)mDims.Y), null, Color.White, mRot, new Vector2(mMyModel.Bounds.Width / 2, mMyModel.Bounds.Height / 2), new SpriteEffects(), 0);
            }
        }

        public virtual void Draw(Vector2 offset, Vector2 origin, Color color)
        {
            if (mMyModel != null)
            {
                Globals.mSpriteBatch.Draw(mMyModel, new Rectangle((int)(mPos.X + offset.X), (int)(mPos.Y + offset.Y), (int)mDims.X, (int)mDims.Y), null, color, mRot, new Vector2(origin.X, origin.Y), new SpriteEffects(), 0);
            }
        }
    }
}
