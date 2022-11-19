using KnightsOfLaCampus.Source;
using Microsoft.Xna.Framework.Audio;
using System;

namespace KnightsOfLaCampus.Managers
{
    /// <summary>
    /// The class creates a sound effect and the corresponding instance. It is used for management purposes in the sound manager and in the entity classes.
    /// </summary>
    internal sealed class SoundItem
    {
        // Wird später noch benutzt, also sei still Jenkins: "private readonly SoundEffect mSoundEffect;"
        public string NameOfSoundEffect { get; }
        public SoundEffectInstance SoundEffectInstance { get; }

        /// <summary>
        /// Constructor of the class SoundItem
        /// </summary>
        /// <param name="nameOfSoundEffect">The name of the sound effect to find it in the list</param>
        /// <param name="soundPath">The path in which the audio file is located</param>
        public SoundItem(string nameOfSoundEffect, string soundPath)
        {
            NameOfSoundEffect = nameOfSoundEffect;
            if (soundPath == null)
            {
                Console.WriteLine("Error with Sound Path");
            }
            else
            {
                SoundEffect soundEffect = Globals.Content.Load<SoundEffect>(soundPath);
                SoundEffectInstance = soundEffect.CreateInstance();
            }
        }
    }
}
