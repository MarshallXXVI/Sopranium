using Microsoft.Xna.Framework;

namespace KnightsOfLaCampus.Screens
{
    /// <summary>
    /// Interface to manage different screens with each other
    /// </summary>
    public interface IScreen
    {
        // Flags for drawing/updating the lower Screen
        public bool DrawLower { get; }
        public bool UpdateLower { get; }

        /// <summary>
        /// Loads the used content of the screen
        /// </summary>
        public void LoadContent();

        public void Update(GameTime gameTime);

        // Communication methods for the ScreenManager

        /// <summary>
        /// If an event (like a button click) is triggered, this method will 
        /// return a new instance of IScreen, that is supposed to be add to
        /// the stack. If not the return is null
        /// </summary>
        /// <returns>New instance of IScreen or null</returns>
        public IScreen NextScreen();

        /// <summary>
        /// If an event (like a button click) is triggered, this method will
        /// return the current instance (this). If not the return is null.
        /// </summary>
        /// <returns>current instance or null</returns>
        public IScreen PrevScreen();

        /// <summary>
        /// Removes all screens on the stack except the first (Main)
        /// </summary>
        public bool BackToMenu();

        /// <summary>
        /// Draws the used content of the screen
        /// </summary>
        public void Draw();
    }
}
