using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Managers
{
    /// <summary>
    /// The Screen Manager has a list of IScreens and can be used to manage them
    /// </summary>
    internal sealed class ScreenManager
    {
        // The ScreenManager has to handle a list of IScreen Instances
        // which is external useable like a stack
        private readonly List<IScreen> mScreenStack;

        /// <summary>
        /// Constructor of the Screen Manager class that creates an empty list of screens.
        /// </summary>
        internal ScreenManager()
        {
            mScreenStack = new List<IScreen>();
        }

        /// <summary>
        /// Adds a Screen to the stack
        /// </summary>
        /// <param name="screenAdd">Screen to be added</param>
        internal void AddScreen(IScreen screenAdd)
        {
            mScreenStack.Add(screenAdd);

            // reloading the content
            mScreenStack.Last().LoadContent();
        }

        /// <summary>
        /// Removes the top item from the stack
        /// </summary>
        private void RemoveScreen()
        {
            mScreenStack.RemoveAt(mScreenStack.Count - 1);
        }

        /// <summary>
        /// The Update loop, which processes the events of the instances
        /// </summary>
        /// <param name="gameTime"></param>
        internal void Update(GameTime gameTime)
        {
            // Communication with the IScreen instances. If some
            // event triggers the callback methods the following two
            // variables might be != null anymore.
            var toAdd = mScreenStack.Last().NextScreen();
            var toRemove = mScreenStack.Last().PrevScreen();
            var backToMenu = mScreenStack.Last().BackToMenu();

            // Adds a IScreen instance, which is requested by the instance
            if (toAdd != null) { AddScreen(toAdd); }

            // Removes the IScreen instance, which triggered this event 
            if (toRemove != null) { RemoveScreen();}

            // Removes all screens up till the menu
            if (backToMenu)
            {
                for (var i = 0; i < mScreenStack.Count; i++)
                {
                    RemoveScreen();
                }
                // Must be here otherwise it is bugged and you can click buttons on not active screens
                mScreenStack[0].LoadContent();
            }

            if (mScreenStack.Count > 0)
            {
                // Iterates over the list (stack) starting with the 2nd element
                for (var i = 1; i < mScreenStack.Count; i++)
                {
                    // If the lower screen has to be updated 
                    if (mScreenStack[i].UpdateLower)
                    {
                        mScreenStack[i - 1].Update(gameTime);
                    }
                }
                // Always update the very top screen
                mScreenStack.Last().Update(gameTime);
            }
        }

        /// <summary>
        /// Draws all the required screens in the stack
        /// </summary>
        internal void Draw(SpriteBatch spriteBatch)
        {
            if (mScreenStack.Count > 0)
            {
                // Iterates over the list (stack) starting with the 2nd element
                for (var i = 1; i < mScreenStack.Count; i++)
                {
                    // If the lower screen has to be drawn 
                    if (mScreenStack[i].DrawLower)
                    {
                        mScreenStack[i-1].Draw();
                    }
                }
                // Always draw the very top screen
                mScreenStack.Last().Draw();
            }
        }
    }
}
