#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Drawing;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
#endregion

namespace Monogame_00
{
    public class McMouseControl
    {
        public bool mDragging, mRightDrag;

        public Vector2 mNewMousePos, mOldMousePos, mFirstMousePos, mNewMouseAdjustedPos, mSystemCursorPos, mScreenLoc;

        public MouseState mNewMouse, mOldMouse, mFirstMouse;

        public McMouseControl()
        {
            mDragging = false;

            mNewMouse = Mouse.GetState();
            mOldMouse = mNewMouse;
            mFirstMouse = mNewMouse;

            mNewMousePos = new Vector2(mNewMouse.Position.X, mNewMouse.Position.Y);
            mOldMousePos = new Vector2(mNewMouse.Position.X, mNewMouse.Position.Y);
            mFirstMousePos = new Vector2(mNewMouse.Position.X, mNewMouse.Position.Y);

            GetMouseAndAdjust();

            //screenLoc = new Vector2((int)(systemCursorPos.X/Globals.screenWidth), (int)(systemCursorPos.Y/Globals.screenHeight));

        }

        #region Properties

        public MouseState First
        {
            get { return mFirstMouse; }
        }

        public MouseState New
        {
            get { return mNewMouse; }
        }

        public MouseState Old
        {
            get { return mOldMouse; }
        }

        #endregion

        public void Update()
        {
            GetMouseAndAdjust();


            if(mNewMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && mOldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                mFirstMouse = mNewMouse;
                mFirstMousePos = mNewMousePos = GetScreenPos(mFirstMouse);
            }

            
        }

        public void UpdateOld()
        {
            mOldMouse = mNewMouse;
            mOldMousePos = GetScreenPos(mOldMouse);
        }

        public virtual float GetDistanceFromClick()
        {
            return Globals.GetDistance(mNewMousePos, mFirstMousePos);
        }

        public virtual void GetMouseAndAdjust()
        {
            mNewMouse = Mouse.GetState();
            mNewMousePos = GetScreenPos(mNewMouse);

        }




        public int GetMouseWheelChange()
        {
            return mNewMouse.ScrollWheelValue - mOldMouse.ScrollWheelValue;
        }


        public Vector2 GetScreenPos(MouseState mouse)
        {
            Vector2 tempVec = new Vector2(mouse.Position.X, mouse.Position.Y);
            return tempVec;
        }

        public virtual bool LeftClick()
        {
            if( mNewMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && mOldMouse.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && mNewMouse.Position.X >= 0 && mNewMouse.Position.X <= Globals.mScreenWidth && mNewMouse.Position.Y >= 0 && mNewMouse.Position.Y <= Globals.mScreenHeight)
            {
                return true;
            }

            return false;
        }

        public virtual bool LeftClickHold()
        {
            bool holding = false;

            if( mNewMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && mOldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && mNewMouse.Position.X >= 0 && mNewMouse.Position.X <= Globals.mScreenWidth && mNewMouse.Position.Y >= 0 && mNewMouse.Position.Y <= Globals.mScreenHeight)
            {
                holding = true;

                if(Math.Abs(mNewMouse.Position.X - mFirstMouse.Position.X) > 8 || Math.Abs(mNewMouse.Position.Y - mFirstMouse.Position.Y) > 8)
                {
                    mDragging = true;
                }
            }

            return holding;
        }

        public virtual bool LeftClickRelease()
        {
            if(mNewMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released && mOldMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                mDragging = false;
                return true;
            }

            return false;
        }

        public virtual bool RightClick()
        {
            if(mNewMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && mOldMouse.RightButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && mNewMouse.Position.X >= 0 && mNewMouse.Position.X <= Globals.mScreenWidth && mNewMouse.Position.Y >= 0 && mNewMouse.Position.Y <= Globals.mScreenHeight)
            {
                return true;
            }

            return false;
        }

        public virtual bool RightClickHold()
        {
            bool holding = false;

            if( mNewMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && mOldMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && mNewMouse.Position.X >= 0 && mNewMouse.Position.X <= Globals.mScreenWidth && mNewMouse.Position.Y >= 0 && mNewMouse.Position.Y <= Globals.mScreenHeight)
            {
                holding = true;

                if(Math.Abs(mNewMouse.Position.X - mFirstMouse.Position.X) > 8 || Math.Abs(mNewMouse.Position.Y - mFirstMouse.Position.Y) > 8)
                {
                    mRightDrag = true;
                }
            }

            return holding;
        }

        public virtual bool RightClickRelease()
        {
            if( mNewMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released && mOldMouse.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                mDragging = false;
                return true;
            }

            return false;
        }

        public void SetFirst()
        {

        }
    }
}
