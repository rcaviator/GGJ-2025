using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GGJ2025.Operations;
using GGJ2025.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GGJ2025.Managers
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
        //None,

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
        //None,

       


        // Player Bubble
        BubbleGunStart, BubbleGunLoop, BubbleHit,

        // Enemies
        TrashFootstep, TrashBite, TrashSpit,

        // Player
        StartMove,MoveLoop,
    }

    #endregion

    /// <summary>
    /// AudioManager is the singleton that handles all the audio in the game.
    /// </summary>
    [Serializable]
    class AudioManager : IAsyncInitialized
    {
        #region Fields

        // Singleton instance
        static AudioManager? instance;

        // Music, UI, and game sound effect dictionaries
        Dictionary<MusicSoundEffect, AudioClip> musicSoundEffectsDict = new();
        Dictionary<UISoundEffect, AudioClip> uiSoundEffectsDict = new();
        Dictionary<GameSoundEffect, AudioClip> GameSoundEffectsDict = new();

        // GameObject for audio sources
        GameObject audioController;

        // Audio source references
        AudioSource musicAudioSource;
        AudioSource uiAudioSource;
        AudioSource gameAudioSource;
        AudioSource loopedGameAudioSource;

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
            Initialize().Execute();

            // Create the audio GameObject and save it
            audioController = new GameObject("AudioController");
            UnityEngine.Object.DontDestroyOnLoad(audioController);

            // Create audio source references
            musicAudioSource = audioController.AddComponent<AudioSource>();
            uiAudioSource = audioController.AddComponent<AudioSource>();
            gameAudioSource = audioController.AddComponent<AudioSource>();
            loopedGameAudioSource = audioController.AddComponent<AudioSource>();

            // Set audio sources for ignore pausing
            musicAudioSource.ignoreListenerPause = true;
            uiAudioSource.ignoreListenerPause = true;
            gameAudioSource.ignoreListenerPause = false;
            loopedGameAudioSource.ignoreListenerPause = false;

            loopedGameAudioSource.loop = true;
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
                loopedGameAudioSource.volume = sfxVolume;
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


        public void PlayLoopedGamePlaySoundEffect(GameSoundEffect name)
        {
            if (GameSoundEffectsDict.ContainsKey(name))
            {
                loopedGameAudioSource.clip = GameSoundEffectsDict[name];
                loopedGameAudioSource.Play();
            }
        }


        public void StopLoopedGameSoundEffect()
        {
            if (loopedGameAudioSource.isPlaying)
            {
                loopedGameAudioSource.Stop();
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
        public AudioClip? GetAudioClip(GameSoundEffect effect)
        {
            if (!GameSoundEffectsDict.ContainsKey(effect))
            {
                return null;
            }

            return GameSoundEffectsDict[effect];
        }

        #endregion

        public bool Initialized { get; private set; }

        private IEnumerator Initialize()
        {
            var mappingsLoad = Addressables.LoadAssetAsync<AudioMappings>("AudioMappings").ToOperation();
            yield return mappingsLoad;
            if (mappingsLoad.Result is { } mappings)
            {
                var musicOperations = mappings.musicMappings.Select(LoadMapping).ToList();
                var uiOperations = mappings.uiMappings.Select(LoadMapping).ToList();
                var gameOperations = mappings.gameMappings.Select(LoadMapping).ToList();
                yield return new ParallelOperation(musicOperations.Concat(uiOperations).Concat(gameOperations));

                PopulateClips(musicSoundEffectsDict, musicOperations, mappings.musicMappings);
                PopulateClips(uiSoundEffectsDict, uiOperations, mappings.uiMappings);
                PopulateClips(GameSoundEffectsDict, gameOperations, mappings.gameMappings);
            }

            Initialized = true;
        }

        private static AddressablesLoadOperation<AudioClip> LoadMapping<T>(AudioMappings.AudioMapping<T> m)
            where T : Enum =>
            Addressables.LoadAssetAsync<AudioClip>(m.sound).ToOperation();

        private static void PopulateClips<T>(Dictionary<T, AudioClip> dictionary,
            IReadOnlyList<AddressablesLoadOperation<AudioClip>> loadOperations,
            IReadOnlyList<AudioMappings.AudioMapping<T>> mappings) where T : Enum
        {
            for (var i = 0; i < loadOperations.Count; i++)
            {
                if (loadOperations[i].Result is {} clip)
                {
                    dictionary.Add(mappings[i].type, clip);
                }
            }
        }
    }
}