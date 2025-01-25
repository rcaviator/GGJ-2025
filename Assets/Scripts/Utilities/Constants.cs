using System.Collections.Generic;

// Workaround for init keyword to function in this .NET version
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace GGJ2025.Utilities
{
    /// <summary>
    /// All constants for the entire application.
    /// </summary>
    public static class Constants
    {
        #region Fields

        // Dictionary for storing all initial game entity data constants
        private static Dictionary<GameEntity, GameEntityData> gameEntities;

        #endregion

        #region Object IDs

        /// <summary>
        /// All object IDs used for saving, loading, and instantiation.
        /// </summary>
        //public enum ObjectIDs
        //{
        //    // Default
        //    None,

        //    // Utilities


        //    // Environment


        //    // UI elements

        //}

        /// <summary>
        /// Enum for the type of tag an object is associated with.
        /// Used for collision checking.
        /// </summary>
        public enum Tags
        {
            // Default tags
            None,
            Untagged,
            Default,
            MainCamera,
            Player,

            // Custom tags

        }

        #endregion

        #region Application Constants

        // Global


        // Settings constants


        #endregion

        #region Scenes

        // Scene names
        public const string SCENE_MAIN_MENU = "MainMenu";
        public const string SCENE_LEVEL_1 = "Level1";
        public const string SCENE_WIN = "Win";
        public const string SCENE_LOSE = "Lose";
        public const string SCENE_CREDITS = "Credits";

        #endregion

        #region Lighting



        #endregion

        #region Camera

        //// Camera pan and tilt sensitivity
        //public const float CAMERA_LOOK_SENSITIVITY_X = 2f;
        //public const float CAMERA_LOOK_SENSITIVITY_Y = 2f;

        //// Camera angle clamps
        //public const float CAMERA_VIEW_UP_BOUNDS = -80f;
        //public const float CAMERA_VIEW_DOWN_BOUNDS = 80f;

        //// Camera zoom change distance amounts
        //public const float CAMERA_ZOOM_AMOUNT_Y = .5f;
        //public const float CAMERA_ZOOM_AMOUNT_Z = 2f;

        //// Camera zoom minimum and maximum level amounts
        //public const int CAMERA_ZOOM_LEVEL_MINIMUM = 1;
        //public const int CAMERA_ZOOM_LEVEL_MAXIMUM = 4;

        //// Camera free fly mode speeds
        //public const float CAMERA_FREE_FLY_SLOW_SPEED = 10f;
        //public const float CAMERA_FREE_FLY_FAST_SPEED = 30f;

        //// Camera raycast timer
        //public const float CAMERA_RAYCAST_TIMER = .1f;

        #endregion

        #region Game Enums and Utilities

        // Template enum and constants struct. Make as many pairs as needed

        /// <summary>
        /// The game object entity type.
        /// </summary>
        public enum GameEntity
        {
            None,

        }

        /// <summary>
        /// Data structure for each game object's initial data constants.
        /// </summary>
        public readonly struct GameEntityData
        {
            // Enums


            // Movement
            public float MOVEMENT_ACCELERATION_RATE { get; init; }
            public float MOVEMENT_DECELERATION_RATE { get; init; }
            public float MOVEMENT_FORWARD_MAX_SPEED { get; init; }
            public float MOVEMENT_BACKWARD_MAX_SPEED { get; init; }
            public float MOVEMENT_LEFT_TURN_RATE { get; init; }
            public float MOVEMENT_RIGHT_TURN_RATE { get; init; }
            public float MOVEMENT_JUMP_FORCE_AMOUNT { get; init; }

            // Stats

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves initialization constants for a specified game entity.
        /// </summary>
        /// <param name="entity">the game entity to retrieve initial data</param>
        /// <returns>its initial data</returns>
        public static GameEntityData GetGameData(GameEntity entity)
        {
            // Create the dicitionary on the first call
            if (gameEntities == null)
            {
                gameEntities = new Dictionary<GameEntity, GameEntityData>();
                InitializeGameData();
            }

            // Return None if the entity does not exist in the dictionary
            if (!gameEntities.ContainsKey(entity))
            {
                return gameEntities[GameEntity.None];
            }

            return gameEntities[entity];
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the game entity constants dictionary.
        /// </summary>
        private static void InitializeGameData()
        {
            #region None

            // None
            GameEntityData none = new GameEntityData()
            {
                // Movement
                MOVEMENT_ACCELERATION_RATE = 0f,
                MOVEMENT_DECELERATION_RATE = 0f,
                MOVEMENT_FORWARD_MAX_SPEED = 0f,
                MOVEMENT_BACKWARD_MAX_SPEED = 0f,
                MOVEMENT_LEFT_TURN_RATE = 0f,
                MOVEMENT_RIGHT_TURN_RATE = 0f,
                MOVEMENT_JUMP_FORCE_AMOUNT = 0f,

                // Stats

            };
            gameEntities.Add(GameEntity.None, none);

            #endregion
        }

        #endregion
    }
}