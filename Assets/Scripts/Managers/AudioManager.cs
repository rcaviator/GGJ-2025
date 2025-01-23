using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    #region Public Audio Type Enums

    /// <summary>
    /// Music enum.
    /// </summary>
    public enum MusicSoundEffect
    {
        // Default
        None,

        // Menus
        MainMenu,
        Win,
        Lose,
        Credits,

        // Levels
        Level1,
    }

    /// <summary>
    /// UI sound effects enum.
    /// </summary>
    public enum UISoundEffect
    {
        // Default
        None,

        // Menus
        MenuButtonFocused, MenuButtonClickAdvance, MenuButtonClickBack,

        // UI
        Generic, Pause, Unpause, Exit, Reset,
    }

    /// <summary>
    /// Game play sound effects enum.
    /// </summary>
    public enum GameSoundEffect
    {
        // Default
        None,


    }

    #endregion

    /// <summary>
    /// AudioManager is the singleton that handles all the audio in the game.
    /// </summary>
    [Serializable]
    class AudioManager
    {
        #region Fields

        // Singleton instance
        static AudioManager instance;

        // Music, UI, and game sound effect dictionaries
        Dictionary<MusicSoundEffect, AudioClip> musicSoundEffectsDict;
        Dictionary<UISoundEffect, AudioClip> uiSoundEffectsDict;
        Dictionary<GameSoundEffect, AudioClip> GameSoundEffectsDict;

        // GameObject for audio sources
        GameObject audioController;

        // Audio source references
        AudioSource musicAudioSource;
        AudioSource uiAudioSource;
        AudioSource gameAudioSource;

        // Music volume
        float musicVolume;

        // SFX volume
        float sfxVolume;

        // UI volume
        float uiVolume;

        // Reference to currently playing background music
        MusicSoundEffect currentMusic;

        #endregion

        #region Constructor

        /// <summary>
        /// Private AudioManager contructor.
        /// </summary>
        private AudioManager()
        {
            // Create and populate the music dictionary
            musicSoundEffectsDict = new Dictionary<MusicSoundEffect, AudioClip>()
            { 
                // Leave MusicSoundEffect.None out
                { MusicSoundEffect.MainMenu, Resources.Load<AudioClip>("Audio/Music/") },
                { MusicSoundEffect.Level1, Resources.Load<AudioClip>("Audio/Music/") },
                { MusicSoundEffect.Win, Resources.Load<AudioClip>("Audio/Music/") },
                { MusicSoundEffect.Lose, Resources.Load<AudioClip>("Audio/Music/") },
                { MusicSoundEffect.Credits, Resources.Load<AudioClip>("Audio/Music/") },
            };

            // Create and populate the UI dictionary
            uiSoundEffectsDict = new Dictionary<UISoundEffect, AudioClip>()
            {
                // Leave UISoundEffect.None out
                { UISoundEffect.MenuButtonFocused, Resources.Load<AudioClip>("Audio/UI/") },
                { UISoundEffect.MenuButtonClickAdvance, Resources.Load<AudioClip>("Audio/UI/") },
                { UISoundEffect.MenuButtonClickBack, Resources.Load<AudioClip>("Audio/UI/") },
                { UISoundEffect.Generic, Resources.Load<AudioClip>("Audio/UI/") },
                { UISoundEffect.Pause, Resources.Load<AudioClip>("Audio/UI/") },
                { UISoundEffect.Unpause, Resources.Load<AudioClip>("Audio/UI/") },
                { UISoundEffect.Exit, Resources.Load<AudioClip>("Audio/UI/") },
                { UISoundEffect.Reset, Resources.Load<AudioClip>("Audio/UI/") },
            };

            // Create and populate the game dictionary
            GameSoundEffectsDict = new Dictionary<GameSoundEffect, AudioClip>()
            {
                // Leave GameSoundEffect.None out
                //{ GameSoundEffect.None , Resources.Load<AudioClip>("Audio/Effects/")},
            };

            // Create the audio GameObject and save it
            audioController = new GameObject("AudioController");
            UnityEngine.Object.DontDestroyOnLoad(audioController);

            // Create audio source references
            musicAudioSource = audioController.AddComponent<AudioSource>();
            uiAudioSource = audioController.AddComponent<AudioSource>();
            gameAudioSource = audioController.AddComponent<AudioSource>();

            // Set audio sources for ignore pausing
            musicAudioSource.ignoreListenerPause = true;
            uiAudioSource.ignoreListenerPause = true;
            gameAudioSource.ignoreListenerPause = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the AudioManager.
        /// </summary>
        public static AudioManager Instance
        {
            get { return instance ??= new AudioManager(); }
        }

        /// <summary>
        /// The music volume.
        /// </summary>
        public float MusicVolume
        {
            get { return musicVolume; }
            set
            {
                musicVolume = value;
                musicVolume = Mathf.Clamp(musicVolume, 0f, 1f);
                musicAudioSource.volume = musicVolume;
            }
        }

        /// <summary>
        /// The UI volume.
        /// </summary>
        public float UIVolume
        {
            get { return uiVolume; }
            set
            {
                uiVolume = value;
                uiVolume = Mathf.Clamp(uiVolume, 0f, 1f);
                uiAudioSource.volume = uiVolume;
            }
        }

        /// <summary>
        /// The sound effects volume.
        /// </summary>
        public float SoundEffectsVolume
        {
            get { return sfxVolume; }
            set
            {
                sfxVolume = value;
                sfxVolume = Mathf.Clamp(sfxVolume, 0f, 1f);
                gameAudioSource.volume = sfxVolume;
            }
        }

        #endregion

        #region Music

        /// <summary>
        /// Plays a background music on loop. If a track is already playing,
        /// stops the current track and plays new one.
        /// </summary>
        /// <param name="name">the name of the track to play</param>
        public void PlayMusic(MusicSoundEffect name)
        {
            if (!musicSoundEffectsDict.ContainsKey(name))
            {
                return;
            }

            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Stop();
                musicAudioSource.clip = musicSoundEffectsDict[name];
                musicAudioSource.Play();
                musicAudioSource.loop = true;
            }
            else
            {
                musicAudioSource.clip = musicSoundEffectsDict[name];
                musicAudioSource.Play();
                musicAudioSource.loop = true;
            }

            currentMusic = name;
        }

        /// <summary>
        /// Stops the current music.
        /// </summary>
        public void StopMusic()
        {
            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Stop();
                currentMusic = MusicSoundEffect.None;
            }
        }

        /// <summary>
        /// Pauses the current music.
        /// </summary>
        public void PauseMusic()
        {
            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Pause();
            }
        }

        /// <summary>
        /// Unpauses the current music.
        /// </summary>
        public void UnpauseMusic()
        {
            if (!musicAudioSource.isPlaying)
            {
                musicAudioSource.UnPause();
            }
        }

        /// <summary>
        /// Gets the enum of the current music playing.
        /// </summary>
        /// <returns>the current music enum</returns>
        public MusicSoundEffect WhatMusicIsPlaying()
        {
            return currentMusic;
        }

        #endregion

        #region UI

        /// <summary>
        /// Plays a UI sound effect.
        /// </summary>
        /// <param name="name">the name of the sound effect</param>
        public void PlayUISoundEffect(UISoundEffect name)
        {
            if (uiSoundEffectsDict.ContainsKey(name))
            {
                uiAudioSource.PlayOneShot(uiSoundEffectsDict[name]);
            }
        }

        #endregion

        #region Game SFX

        /// <summary>
        /// Plays a game play sound effect.
        /// </summary>
        /// <param name="name">the name of the sound effect</param>
        public void PlayGamePlaySoundEffect(GameSoundEffect name)
        {
            if (GameSoundEffectsDict.ContainsKey(name))
            {
                gameAudioSource.PlayOneShot(GameSoundEffectsDict[name]);
            }
        }

        /// <summary>
        /// Pauses the game play sound effects with AudioListener.
        /// </summary>
        public void PauseGamePlaySoundEffects()
        {
            if (!AudioListener.pause)
            {
                AudioListener.pause = true;
            }
        }

        /// <summary>
        /// Unpauses the game play sound effects with AudioListener.
        /// </summary>
        public void UnpauseGamePlaySoundEffects()
        {
            if (AudioListener.pause)
            {
                AudioListener.pause = false;
            }
        }

        /// <summary>
        /// Gets a game sound effect audio clip.
        /// </summary>
        /// <param name="effect">the clip to get</param>
        /// <returns>the audio clip</returns>
        public AudioClip GetAudioClip(GameSoundEffect effect)
        {
            if (!GameSoundEffectsDict.ContainsKey(effect))
            {
                return null;
            }

            return GameSoundEffectsDict[effect];
        }

        #endregion
    }
}