using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Units
{
    internal sealed class Gold
    {
            // This class represents the amount of gold on a GridBox which can be collected by the King
            private readonly Texture2D mTexture;
            // Represents the Position on the used GridBox
            // Represents the actual Positions on the map which is inside the used GridBox but can be scaled
            // to fit the Texture better
            private readonly Vector2 mPosition;
            private readonly King mKing;
            public bool mIfDead;
            public readonly int mAmount;

            public Gold(Vector2 position, King king)
            {
                mAmount = 5;
                mIfDead = false;
                mTexture = Globals.Content.Load<Texture2D>("Gold");
                mPosition = position;
                mKing = king;
            }

            // Checks if the King enters the GridBox of the Gold and if so sets Alive to false
            // * I tried to do it with the Grid Methods but failed, so I had to do it with a Rectangle on
            // the map, which is essentially the GridBox *
            public void Update()
            {
                if (!(Vector2.Distance(mPosition, mKing.Position) < 10))
                {
                    return;
                }

                GameGlobals.mGold += mAmount;
                mIfDead = true;

            }

            // Draws the Texture inside the Grid Box but a bit scaled
            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(mTexture,
                    new Rectangle((int)mPosition.X - 16, (int)mPosition.Y - 32 / 4, 32, 32),
                    Color.White);

            }
        }
    }
