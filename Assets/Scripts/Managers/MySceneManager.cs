using System.Collections.Generic;
using System.Linq;
using GGJ2025.Utilities;
using UnityEngine.SceneManagement;

namespace GGJ2025.Managers
{
    /// <summary>
    /// MySceneManager is the singleton that handles all Scene information.
    /// </summary>
    class MySceneManager
    {
        #region Fields

        /// <summary>
        /// Enum for all the scenes in the application.
        /// </summary>
        public enum Scenes
        {
            // Default
            None,

            // Menu scenes
            MainMenu,
            Win,
            Lose,
            Credits,

            // Game scenes
            Level1,
        }

        // Singleton instance
        static MySceneManager instance;

        // Dictionary of scenes
        Dictionary<Scenes, string> sceneDict;

        // Dictionary for what soundtrack to play per scene
        Dictionary<Scenes, MusicSoundEffect> soundtrackDict;

        #endregion

        #region Constructor

        /// <summary>
        /// Private constructor. Initalizes scene and soundtrack dictionaries
        /// as well as registering scene load and unload events.
        /// </summary>
        private MySceneManager()
        {
            // Initialize the scene dictionary
            sceneDict = new Dictionary<Scenes, string>()
            {
                { Scenes.MainMenu, Constants.SCENE_MAIN_MENU },
                { Scenes.Level1, Constants.SCENE_LEVEL_1 },
                { Scenes.Win, Constants.SCENE_WIN },
                { Scenes.Lose, Constants.SCENE_LOSE },
                { Scenes.Credits, Constants.SCENE_CREDITS },
            };

            // Initialize the soundtrack dictionary
            soundtrackDict = new Dictionary<Scenes, MusicSoundEffect>()
            {
                { Scenes.MainMenu, MusicSoundEffect.MainMenu },
                { Scenes.Level1, MusicSoundEffect.Level1 },
                { Scenes.Win, MusicSoundEffect.Win },
                { Scenes.Lose, MusicSoundEffect.Lose },
                { Scenes.Credits, MusicSoundEffect.Credits },
            };

            // Register the scene loaded delegate
            SceneManager.sceneLoaded += OnLevelLoaded;

            // Register scene unloaded delegate
            SceneManager.sceneUnloaded += OnLevelUnloaded;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the scene manager.
        /// </summary>
        public static MySceneManager Instance
        {
            get { return instance ??= new MySceneManager(); }
        }

        /// <summary>
        /// The current scene.
        /// </summary>
        public Scenes CurrentScene
        { get; set; }

        /// <summary>
        /// The previous scene.
        /// </summary>
        public Scenes PreviousScene
        { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the scene.
        /// </summary>
        /// <param name="name">the scene to change to</param>
        public void ChangeScene(Scenes name)
        {
            if (sceneDict.ContainsKey(name))
            {
                SceneManager.LoadScene(sceneDict[name]);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called when the scene changes and things need to update on scene change.
        /// </summary>
        /// <param name="scene">the new scene</param>
        /// <param name="mode">loaded scene mode</param>
        void OnLevelLoaded(Scene scene, LoadSceneMode mode)
        {
            // Get the scene reference
            CurrentScene = sceneDict.Keys.First(t => sceneDict[t] == scene.name);

            // If the soundtrack dictionary doesn't contain a soundtrack reference
            // for the current scene, stop any music and return
            if (!soundtrackDict.ContainsKey(CurrentScene))
            {
                AudioManager.Instance.StopMusic();
                return;
            }

            // Change soundtracks if the new scene requires it
            if (!(soundtrackDict[CurrentScene] == AudioManager.Instance.WhatMusicIsPlaying()))
            {
                AudioManager.Instance.PlayMusic(soundtrackDict[CurrentScene]);
            }
        }

        /// <summary>
        /// Called when the scene is unloaded and things need to update.
        /// </summary>
        /// <param name="scene">the unloaded scene</param>
        void OnLevelUnloaded(Scene scene)
        {
            PreviousScene = sceneDict.Keys.First(x => sceneDict[x] == scene.name);
        }

        #endregion
    }
}