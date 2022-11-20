using Microsoft.Xna.Framework.Input;
using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KnightsOfLaCampus.Source;

internal sealed class Input
{
    private bool mDragging;
    public Vector2 mNewMousePos;
    private Vector2 mFirstMousePos;
    private MouseState mNewMouse, mOldMouse, mFirstMouse;
    private KeyboardState mKeyboard;

    public Input()
    {

        mNewMouse = Mouse.GetState();
        mOldMouse = mNewMouse;
        mFirstMouse = mNewMouse;
        mNewMousePos = new Vector2(mNewMouse.Position.X, mNewMouse.Position.Y);
        mFirstMousePos = new Vector2(mNewMouse.Position.X, mNewMouse.Position.Y);

        GetMouseAndAdjust();

    }

    public bool IfKeyG()
    {
        return mKeyboard[Keys.G] == KeyState.Down;
    }

    private bool IfMouseValid()
    {
        return mNewMouse.Position.X is >= 0 and <= Globals.ScreenWidth &&
               mNewMouse.Position.Y is >= 0 and <= Globals.ScreenHeight;
    }
    public bool RightClick()
    {
        if (IfMouseValid())
        {
            return mNewMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                   mOldMouse.RightButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        }

        return false;
    }

    public bool LeftClick()
    {
        if (IfMouseValid())
        {
            return mNewMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                   mOldMouse.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        }

        return false;
    }

    public bool LeftClickHold()
    {
        if (mNewMouse.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed ||
            mOldMouse.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed || mNewMouse.Position.X is < 0 or > Globals.ScreenWidth || mNewMouse.Position.Y is < 0 or > Globals.ScreenHeight)
        {
            return false;
        }

        if (Math.Abs(mNewMouse.Position.X - mFirstMouse.Position.X) > 8 || Math.Abs(mNewMouse.Position.Y - mFirstMouse.Position.Y) > 8)
        {
            mDragging = true;
        }

        return true;
    }

    private void GetMouseAndAdjust()
    {
        mNewMouse = Mouse.GetState();
        mNewMousePos = GetScreenPos(mNewMouse);

    }

    private static Vector2 GetScreenPos(MouseState mouse)
    {
        var tempVec = new Vector2(mouse.Position.X, mouse.Position.Y);
        return tempVec;
    }

    public void UpdateOld()
    {
        mOldMouse = mNewMouse;
    }

    public void Update()
    {
        GetMouseAndAdjust();
    }
}