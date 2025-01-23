using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    #region Prefabs Enum

    /// <summary>
    /// Enum for all the Prefabs in the application.
    /// </summary>
    public enum Prefabs
    {
        // Default
        None,

        // UI
        

        // Effects


        // Player
        Player,

        // Enemies


        // Projectiles


        // Utilities
        

        // Debug
        
    }

    #endregion

    /// <summary>
    /// ResourceManager handles loading all the prefabs and resources.
    /// </summary>
    class ResourceManager
    {
        #region Fields

        // Singleton instance
        static ResourceManager instance;

        // The dictionary to hold all the loaded prefabs
        Dictionary<Prefabs, GameObject> prefabDictionary;

        #endregion

        #region Constructor

        /// <summary>
        /// Private constructor. Initializes the prefab dictionary.
        /// </summary>
        private ResourceManager()
        {
            // Create and initialize the prefab dictionary
            prefabDictionary = new Dictionary<Prefabs, GameObject>()
            {
                // UI


                // Effects
            

                // Player
                { Prefabs.Player, Resources.Load<GameObject>("Prefabs/") },

                // Enemies
                
            
                // Projectiles


                // Utilities
                

                // Debug
                
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the ResourceManager.
        /// </summary>
        public static ResourceManager Instance
        {
            get { return instance ??= new ResourceManager(); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the prefab from the prefab dictionary.
        /// </summary>
        /// <param name="getPrefab">the name of the prefab to get</param>
        /// <returns>the requested prefab, else null</returns>
        public GameObject GetPrefab(Prefabs getPrefab)
        {
            if (!prefabDictionary.ContainsKey(getPrefab))
            {
                return null;
            }

            return prefabDictionary[getPrefab];
        }

        #endregion

        #region Private Methods



        #endregion
    }
}