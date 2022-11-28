using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using KnightsOfLaCampus.Source;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfLaCampus.Source
{
    //This class sets the Game Time for day and a night.
    internal sealed class GameTimer
    {
        private readonly Texture2D mTexture;
        private readonly Vector2 mPosition;
        private readonly SpriteFont mFont;
        private readonly Vector2 mTextPosition;
        private string mText;
        private readonly float mTimeLength;
        private float mTimeLeft;
        private bool mIsActive;
        public bool Repeat { get; set; }

        public GameTimer(Texture2D texture, SpriteFont font, Vector2 position, float length)
        {
            mTexture = texture;
            mFont = font;
            mPosition = position;
            mTextPosition = new(position.X + 96, position.Y + 20);
            mTimeLength = length;
            mTimeLeft = length;
        }
        //setting the min, s, ms in the right format
        private void FormatText()
        {
            mText = TimeSpan.FromSeconds(mTimeLeft).ToString(@"mm\:ss\.ff");
        }
        //Activate or Deactivate the Timer
        public void StartStop()
        {
            mIsActive = !mIsActive;
        }
        //Reset the Timer, important for new Maps later
        public void Reset()
        {
            mTimeLeft = mTimeLength;
            FormatText();
        }
        //EventHandler to catch Event (if Timer is Zero)
        public event EventHandler OnTimer;

        //Update the Timer every GameTime Update watch if the timer is under 0.
        //Then send an Event to end Day and start Night or end Night and start Day.
        public void Update(float time)
        {
            if (!mIsActive)
            {
                return;
            }
            mTimeLeft -= time;

            if (mTimeLeft <= 0)
            {
                //If Timer is Zero an Event is thrown
                //Catching by EventHandler
                //Update Day/Night with OnTimer
                OnTimer?.Invoke(this, EventArgs.Empty);
                if (Repeat)
                {
                    Reset();
                }
                else
                {
                    StartStop();
                    mTimeLeft = 0f;
                }

            }

            FormatText();
        }
        //Draw Method, needs an Update maybe.
        public void Draw()
        {
            Globals.SpriteBatch.Draw(mTexture, mPosition, Color.White);
            Globals.SpriteBatch.DrawString(mFont, mText, mTextPosition, Color.Black);
        }
    }
}
