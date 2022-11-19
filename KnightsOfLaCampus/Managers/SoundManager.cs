using System.Collections.Generic;

namespace KnightsOfLaCampus.Managers
{
    /// <summary>
    /// This class can be used to manage the music of the game and play individual sounds of the entities
    /// </summary>
    internal sealed class SoundManager
    {
        private readonly List<SoundItem> mBackgroundMusic = new List<SoundItem>();   // All music included in the game
        private readonly List<SoundItem> mSoundEffects = new List<SoundItem>();  // All sound effects included in the game

        // Could be loaded from options later on
        private readonly float mSoundEffectVolume;
        private readonly float mMusicVolume;

        // Wird später noch benutzt, also sei still Jenkins:  "private int mCurrentBackgroundMusicId;"

        /// <summary>
        /// Constructor of the SoundManager class
        /// </summary>
        public SoundManager()
        {
            mBackgroundMusic.Add(new SoundItem("BackgroundDay", "Audio\\Music\\BackgroundDay"));
            mMusicVolume = 0.6f;
            mSoundEffectVolume = 1f;
        }

        /// <summary>
        /// Function to save sound to the list of all sound effects of the game
        /// </summary>
        /// <param name="nameOfSoundEffect">The name of the sound effect to find it in the list</param>
        /// <param name="soundEffectPath">The path in which the audio file is located</param>
        public void AddSoundEffect(string nameOfSoundEffect, string soundEffectPath)
        {
            mSoundEffects.Add(new SoundItem(nameOfSoundEffect, soundEffectPath));
        }

        /// <summary>
        /// Function with which the song that is currently being played can be changed.
        /// </summary>
        /// <param name="musicId">0 = Day, 1 = Night, 2 = Menu - Song</param>
        public void ChangeMusic(int musicId)
        {
            // Wird später noch benutzt, also sei still Jenkins: "mCurrentBackgroundMusicId = musicId;"
            mBackgroundMusic[musicId].SoundEffectInstance.Volume = mMusicVolume;    // Could be saved locally later or loaded from options
            mBackgroundMusic[musicId].SoundEffectInstance.IsLooped = true;
            mBackgroundMusic[musicId].SoundEffectInstance.Play();
        }

        /* Wird später noch benutzt, also sei still Jenkins: /// <summary>
        /// Stops the music currently playing -> more elegant solution later on
        /// </summary>
        public void StopCurrentMusic()
        "{"
            "mBackgroundMusic[mCurrentBackgroundMusicId].SoundEffectInstance.Stop();"
        "}"*/

        /// <summary>
        /// Plays the given SoundEffect
        /// </summary>
        /// <param name="nameOfSoundEffect">The name of the sound effect to find it in the list</param>
        public void PlaySound(string nameOfSoundEffect)
        {
            foreach (var soundEffect in mSoundEffects)
            {
                if (soundEffect != null && soundEffect.NameOfSoundEffect == nameOfSoundEffect)
                {
                    soundEffect.SoundEffectInstance.Volume = mSoundEffectVolume;
                    soundEffect.SoundEffectInstance.Play();
                }
            }
        }

        /// <summary>
        /// Stops the given SoundEffect
        /// </summary>
        /// <param name="nameOfSoundEffect">The name of the sound effect to find it in the list</param>
        public void StopSound(string nameOfSoundEffect)
        {
            foreach (var soundEffect in mSoundEffects)
            {
                if (soundEffect != null && soundEffect.NameOfSoundEffect == nameOfSoundEffect)
                {
                    soundEffect.SoundEffectInstance.Volume = mSoundEffectVolume;
                    soundEffect.SoundEffectInstance.Stop();
                }
            }
        }
    }
}
