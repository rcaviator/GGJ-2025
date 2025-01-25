using GGJ2025.Managers;
using UnityEngine;

namespace GGJ2025.Utilities
{
    /// <summary>
    /// This class is only used to create the manager objects if they do not exist.
    /// </summary>
    public class Initializer : MonoBehaviour
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Unity Methods


        private void Awake()
        {
            // Initialize all the managers
            GameManager.Instance.ToString();
            AudioManager.Instance.ToString();
            MySceneManager.Instance.ToString();
            InputManager.Instance.ToString();
            ResourceManager.Instance.ToString();
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods



        #endregion
    }
}