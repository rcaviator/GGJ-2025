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

        // Dictionary for storing all initial projectile data constants
        private static Dictionary<Projectiles, Projectile>? projectileDic;

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

        #region Player

        public const float PLAYER_HEALTH = 100f;
        public const float PLAYER_SPEED = 5.0f;
        public const float PLAYER_BUBBLE_PROJECTILE_COOL_DOWN = .1f;

        #endregion

        #region Player Bubble

        public const float PLAYER_BUBBLE_MINIMUM_SIZE_SCALE = .5f;
        public const float PLAYER_BUBBLE_MAXIMUM_SIZE_SCALE = 1.25f;
        public const float PLAYER_BUBBLE_MINIMUM_SPEED_SCALE = .5f;
        public const float PLAYER_BUBBLE_MAXIMUM_SPEED_SCALE = .75f;
        public const float PLAYER_BUBBLE_MAXIMUM_ANGLE_SPREAD = 15f;

        #endregion

        #region Game Enums and Utilities

        /// <summary>
        /// The game object entity type.
        /// </summary>
        public enum Projectiles
        {
            None,
            PlayerBubble,
            EnemyProjectile,
            PlayerTrashBall
        }

        /// <summary>
        /// Data structure for each projectile's initial data constants.
        /// </summary>
        public readonly struct Projectile
        {
            public float SIZE { get; init; }
            public float SPEED { get; init; }
            public float LIFETIME { get; init; }
            public float DAMAGE { get; init; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves initialization constants for a specified projectile.
        /// </summary>
        /// <param name="projectile">the projectile to retrieve initial data</param>
        /// <returns>its initial data</returns>
        public static Projectile GetProjectileData(Projectiles projectile)
        {
            // Create the dicitionary on the first call
            if (projectileDic == null)
            {
                projectileDic = new Dictionary<Projectiles, Projectile>();
                InitializeProjectileData();
            }

            // Return None if the entity does not exist in the dictionary
            if (!projectileDic.ContainsKey(projectile))
            {
                return projectileDic[Projectiles.None];
            }

            return projectileDic[projectile];
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the projectile constants dictionary.
        /// </summary>
        private static void InitializeProjectileData()
        {
            #region None

            Projectile none = new Projectile()
            {
                SIZE = 1,
                SPEED = 1,
                LIFETIME = 1,
                DAMAGE = 1,
            };
            projectileDic!.Add(Projectiles.None, none);

            #endregion

            #region Player Bubble

            Projectile playerBubble = new Projectile()
            {
                SIZE = 2,
                SPEED = 5,
                LIFETIME = 2,
                DAMAGE = 1,
            };
            projectileDic!.Add(Projectiles.PlayerBubble, playerBubble);

            #endregion

            #region Enemy Projectile

            Projectile enemyProjectile = new Projectile()
            {
                SIZE = 1,
                SPEED = 5,
                LIFETIME = 2,
                DAMAGE = 1,
            };
            projectileDic!.Add(Projectiles.EnemyProjectile, enemyProjectile);

            #endregion

            projectileDic.Add(Projectiles.PlayerTrashBall, new()
            {
                SIZE = 2,
                SPEED = 6,
                LIFETIME = float.MaxValue,
                DAMAGE = 0,
            });
        }

        #endregion
    }
}