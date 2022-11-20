using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KnightsOfLaCampus.Screens;

public interface IScreen
{
    // Flags for drawing/updating the lower Screen
    public bool DrawLower { get; }
    public bool UpdateLower { get; }

    public void LoadContent();
    public void Update(GameTime gameTime);

    // Communication methods for the ScreenManager
        
    // If an event (like a button click) is triggered, this method will 
    // return a new instance of IScreen, that is supposed to be add to
    // the stack. If not the return is null
    public IScreen NextScreen();

    // If an event (like a button click) is triggered, this method will 
    // return the current instance (this). If not the return is null.
    public IScreen PrevScreen();

    public void Draw(SpriteBatch spriteBatch);
}