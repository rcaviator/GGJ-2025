using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Utilities;

namespace Game.Managers
{
    /// <summary>
    /// GameManager is the main manager for the whole application.
    /// </summary>
    class GameManager
    {
        #region Fields

        // Singleton instance
        static GameManager instance;

        // Singletong instance of level controller
        LevelController levelController;

        // Boolean for pausing the game
        bool isPaused;

        #endregion

        #region Constructor

        /// <summary>
        /// Private constructor.
        /// </summary>
        private GameManager()
        {
            // Create the tag dictionary
            ObjectTags = new Dictionary<Constants.Tags, string>()
            {
                // Default tags
                { Constants.Tags.Untagged, "Untagged" },
                { Constants.Tags.Player, "Player" },
                { Constants.Tags.MainCamera, "MainCamera" },

                // Custom tags

            };

            // Create the list of pausable objects
            PauseableObjects = new List<PauseableObject>();

            // Create the internal level controller
            GameObject newGameObject = new GameObject("LevelController");
            levelController = newGameObject.AddComponent<LevelController>();
            Object.DontDestroyOnLoad(newGameObject);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the GameManager.
        /// </summary>
        public static GameManager Instance
        {
            get { return instance ??= new GameManager(); }
        }

        /// <summary>
        /// The game object tags
        /// </summary>
        public Dictionary<Constants.Tags, string> ObjectTags
        { get; private set; }

        /// <summary>
        /// The list of all pauseable objects.
        /// </summary>
        public List<PauseableObject> PauseableObjects
        { get; set; }

        /// <summary>
        /// Is the game paused?
        /// </summary>
        public bool Paused
        {
            get
            {
                return isPaused;
            }
            set
            {
                // If they are both true, do nothing
                if (value && isPaused)
                {
                    return;
                }

                // Set the boolean
                isPaused = value;

                // Pause the objects
                if (isPaused)
                {
                    // Set audio manager pauses
                    AudioManager.Instance.PauseGamePlaySoundEffects();

                    // Pause all objects
                    foreach (PauseableObject pauseableObject in PauseableObjects)
                    {
                        pauseableObject.PauseObject();
                    }
                }
                // Unpause the objects
                else
                {
                    // Set audio manager unpauses
                    AudioManager.Instance.UnpauseGamePlaySoundEffects();

                    // Unpause all objects
                    foreach (PauseableObject pauseableObject in PauseableObjects)
                    {
                        pauseableObject.UnPauseObject();
                    }
                }
            }
        }

        bool lockHideMouse = false;
        /// <summary>
        /// Locks and hides the mouse.
        /// </summary>
        public bool LockAndHideMouse
        {
            get { return lockHideMouse; }
            set
            {
                lockHideMouse = value;

                if (lockHideMouse)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an object that needs to be pauseable to the list.
        /// </summary>
        /// <param name="pauseObject">The object to add to pausing</param>
        public void AddPauseableObject(PauseableObject pauseObject)
        {
            PauseableObjects.Add(pauseObject);
        }

        /// <summary>
        /// Removes an object from the pauseable list
        /// </summary>
        /// <param name="pauseObject">the object to remove from pausing</param>
        public void RemovePauseableObject(PauseableObject pauseObject)
        {
            PauseableObjects.Remove(pauseObject);
        }

        #endregion

        #region Private Methods



        #endregion

        #region Internal GameObject

        /// <summary>
        /// Internal GameObject for GameManager to have a real-time presence in the Scene.
        /// </summary>
        class LevelController : MonoBehaviour
        {
            #region Fields



            #endregion

            #region Properties



            #endregion

            #region Unity Methods

            /// <summary>
            /// Acts as this object's constructor. Runs at every Scene start.
            /// This object is persistent, so Awake will not serve the same purpose.
            /// </summary>
            private void OnEnable()
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            }

            /// <summary>
            /// Acts as this object's deconstructor. Runs at every Scene unload.
            /// </summary>
            private void OnDisable()
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }

            #endregion

            #region Public Methods



            #endregion

            #region Private Methods

            /// <summary>
            /// OnSceneLoaded acts as this internal GameManager GameObject's
            /// Awake method for each time a scene is loaded.
            /// </summary>
            /// <param name="scene">the unity scene that loaded</param>
            /// <param name="mode">the load mode for this scene</param>
            private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
            {
                switch (scene.name)
                {
                    case Constants.SCENE_MAIN_MENU:
                        Debug.Log("Main menu");
                        break;
                    case Constants.SCENE_LEVEL_1:
                        Debug.Log("Level 1");
                        break;
                    case Constants.SCENE_WIN:
                        Debug.Log("Win");
                        break;
                    case Constants.SCENE_LOSE:
                        Debug.Log("Lose");
                        break;
                    default:
                        break;
                }
            }

            #endregion
        }

        #endregion
    }
}