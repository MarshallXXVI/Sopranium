using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_00
{
    internal class Sprite
    {
        private Texture2D mTexture;
        public Vector2 mPosition;
        public float mSpeed = 2f;
        public input mInput;

        public Sprite(Texture2D texture)
        {
            mTexture = texture;
        }

        public void Update()
        {
            Move();
        }

        private void Move()
        {
            if (mInput == null)
            {
                return;
            }

            if (Keyboard.GetState().IsKeyDown(mInput.LeftKeys))
            {
                mPosition.X -= mSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(mInput.RightKeys))
            {
                mPosition.X += mSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(mInput.UpKeys))
            {
                mPosition.Y -= mSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(mInput.DownKeys))
            {
                mPosition.Y += mSpeed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, mPosition, Color.White);
        }
    }
}   
