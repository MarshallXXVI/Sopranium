using KnightsOfLaCampus.Source;
using KnightsOfLaCampus.Source.GridNew;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace KnightsOfLaCampus.Units
{
    // This class represents the amount of gold on a GridBox which can be collected by the King
    internal sealed class Gold
    {
        public Texture2D mTexture;
        // Represents the Position on the used GridBox
        private Vector2 mGridPos;
        // Represents the actual Positions on the map which is inside the used GridBox but can be scaled
        // to fit the Texture better
        private Vector2 mPosition;
        private King mKing;
        public bool mAlive;
        public int mAmount;

        public Gold(Vector2 gridPos, King king)
        {
            mAmount = 5;
            mAlive = true;
            mTexture = Globals.Content.Load<Texture2D>("Gold");
            mGridPos = gridPos;

            mPosition.X = GridBox.sBoxSize * mGridPos.X;
            mPosition.Y = GridBox.sBoxSize * mGridPos.Y + 8;

            mKing = king;


        }

        // Checks if the King enters the GridBox of the Gold and if so sets Alive to false
        // * I tried to do it with the Grid Methods but failed, so I had to do it with a Rectangle on
        // the map, which is essentially the GridBox *
        public void Update()
        {
            Rectangle tmpRectangle = new Rectangle((int)(GridBox.sBoxSize * mGridPos.X), (int)(GridBox.sBoxSize * mGridPos.Y),
                GridBox.sBoxSize, GridBox.sBoxSize);
            if (tmpRectangle.Contains((int)mKing.mPosition.X, (int)mKing.mPosition.Y))
            {
                mAlive = false;
            }

        }

        // Draws the Texture inside the Grid Box but a bit scaled
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture,
                new Rectangle((int)mPosition.X, (int)mPosition.Y - GridBox.sBoxSize / 4, GridBox.sBoxSize, GridBox.sBoxSize),
                Color.White);

        }
    }
}
