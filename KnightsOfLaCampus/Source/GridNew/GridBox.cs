using System.Runtime.InteropServices.ComTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KnightsOfLaCampus.Source.GridNew
{
    internal class GridBox
    {
        #region Declaration
        // The size of each box
        public static readonly int sBoxSize = 32;


        // The Frame of the GridBox which contains the position and the dimensions
        public Rectangle BoxFrame { get; set; }


        // To toggle collisions for this Box
        public bool CollisionOn { get; set; }

        // The background texture of the box
        public Texture2D BoxTexture { get; set; }

        // Texture for marking the field
        private readonly Texture2D mMarkerTexture;

        #endregion

        // CONSTRUCTOR 1: Creates a GridBox with an optional texture at position x, y
        public GridBox(Vector2 position, Texture2D boxTexture = null)
        {
            #region Implementation

            // We might change the Box dimension later 
            BoxFrame = new Rectangle((int)position.X, (int)position.Y, sBoxSize, sBoxSize);

            // To keep it optional to draw the GridBox or just use the box for game logic
            if (boxTexture != null)
            {
                BoxTexture = boxTexture;
            }

            // Load Marker Texture
            mMarkerTexture = Globals.Content.Load<Texture2D>("UI\\Marker");

            #endregion
        }

        // Constructor 2: Creates a GridBox with an optional texture
        // This is for passing a new Box to the SetBoxAt(..) in the 
        // Grid, because x and y are going to recalculated anyways.
        protected GridBox(Texture2D boxTexture = null)
        {
            #region Implementation

            // We might change the Box dimension later 
            BoxFrame = new Rectangle(0, 0, sBoxSize, sBoxSize);

            // To keep it optional to draw the GridBox or just use the box for game logic
            if (boxTexture != null)
            {
                BoxTexture = boxTexture;
            }

            // Load Marker Texture
            mMarkerTexture = Globals.Content.Load<Texture2D>("UI\\Marker");

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

        // returns the upper left position
        public Vector2 GetPosition()
        {
            #region Implementation

            return new Vector2(BoxFrame.X, BoxFrame.Y);

            #endregion
        }


        // To check if a pixel x und y is the the box
        public bool ContainsPixel(int px, int py)
        {
            #region Implementation

            return BoxFrame.Contains(px, py);

            #endregion
        }


        /// <summary>
        /// Draws the background texture
        /// If the mouse pointer is over the field, it is additionally marked (red= collision, white = no collision).
        /// </summary>
        public void Draw()
        {
            #region Implementation

            if (BoxTexture == null)
            {
                return;
            }

            var mousePosition = Mouse.GetState().Position;

            // Draws texture of the GridBox
            Globals.SpriteBatch.Draw(BoxTexture, BoxFrame, Color.White);

            // When the mouse pointer is on top of the GridBox, a marker is drawn
            if (BoxFrame.Contains(mousePosition))
            {
                // Is drawn in red = collision or white = no collision
                Globals.SpriteBatch.Draw(mMarkerTexture, BoxFrame, CollisionOn ? Color.Firebrick : Color.White);
            }

            #endregion
        }
    }
}
