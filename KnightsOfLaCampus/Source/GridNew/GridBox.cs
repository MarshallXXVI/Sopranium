using System.Runtime.InteropServices.ComTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KnightsOfLaCampus.Source.GridNew
{
    internal sealed class GridBox
    {
        #region Declaration
        // The size of each box
        public static int mBoxSize = 32;


        // The Frame of the GridBox which contains the position and the dimensions
        public Rectangle BoxFrame { get; set; }


        // To toggle collisions for this Box
        public bool CollisionOn { get; set; }

        // The background texture of the box
        public Texture2D BoxTexture { get; set; }

        #endregion

        // CONSTRUCTOR 1: Creates a GridBox with an optional texture at position x, y
        public GridBox(Vector2 position, Texture2D boxTexture = null)
        {
            #region Implementation

            // We might change the Box dimension later 
            BoxFrame = new Rectangle((int)position.X, (int)position.Y, mBoxSize, mBoxSize);

            // To keep it optional to draw the GridBox or just use the box for game logic
            if (boxTexture != null)
            {
                BoxTexture = boxTexture;
            }

            #endregion
        }

        // Constructor 2: Creates a GridBox with an optional texture
        // This is for passing a new Box to the SetBoxAt(..) in the 
        // Grid, because x and y are going to recalculated anyways.
        public GridBox(Texture2D boxTexture = null)
        {
            #region Implementation

            // We might change the Box dimension later 
            BoxFrame = new Rectangle(0, 0, mBoxSize, mBoxSize);

            // To keep it optional to draw the GridBox or just use the box for game logic
            if (boxTexture != null)
            {
                BoxTexture = boxTexture;
            }

            #endregion
        }

        // returns the center of the GridBox
        public Vector2 GetCenter()
        {
            #region Implementation

            // Assuming the origin of the Frame is the upper left corner
            return new Vector2(BoxFrame.X + BoxFrame.Width / 2, BoxFrame.Y + BoxFrame.Height);

            #endregion
        }

        // To implement unit movement later
        public bool IsClicked()
        {
            // #TODO implement event logic here
            return false;
        }


        // Draws the background texture
        public void Draw()

        #region Implementation

        {
            if (BoxTexture != null) 
            {
                var mousePosition = Mouse.GetState().Position;

                // Draws the box with hover effect
                var color = Color.White;
                if (BoxFrame.Contains(mousePosition))
                {
                    color = Color.DimGray;
                }

                Globals.SpriteBatch.Draw(BoxTexture,
                    BoxFrame, 
                    color
                    );
            }
        }

        #endregion
    }
}
