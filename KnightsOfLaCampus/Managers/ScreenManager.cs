using System.Collections.Generic;
using System.Linq;
using KnightsOfLaCampus.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Managers
{
    internal sealed class ScreenManager
    {
        // The ScreenManager has to handle a list of IScreen Instances
        // which is external useable like a stack
        private readonly List<IScreen> mScreenStack;

        internal ScreenManager()
        {
            mScreenStack = new List<IScreen>();
        }

        // Adds a Screen to the stack
        internal void AddScreen(IScreen screenAdd)
        {
            mScreenStack.Add(screenAdd);

            // reloading the content
            foreach (var screen in mScreenStack)
            {
                screen.LoadContent();
            }
        }

        // Removes the top item from the stack
        private void RemoveScreen()
        {
            mScreenStack.RemoveAt(mScreenStack.Count - 1);

            // reloading the content
            foreach (var screen in mScreenStack)
            {
                screen.LoadContent();
            }
        }

        // The Update loop 
        internal void Update(GameTime gameTime)
        {
            // Communication with the IScreen instances. If some
            // event triggers the callback methods the following two
            // variables might be != null anymore.
            var toAdd = mScreenStack.Last().NextScreen();
            var toRemove = mScreenStack.Last().PrevScreen();

            // Adds a IScreen instance, which is requested by the instance
            if (toAdd != null) { AddScreen(toAdd); }

            // Removes the IScreen instance, which triggered this event 
            if (toRemove != null) { RemoveScreen();}

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
                        mScreenStack[i-1].Draw(spriteBatch);
                    }
                }
                // Always draw the very top screen
                mScreenStack.Last().Draw(spriteBatch);
            }
        }
    }
}
