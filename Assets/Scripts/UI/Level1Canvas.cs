using GGJ2025.Managers;
using UnityEngine;

namespace GGJ2025.UI
{
    public class Level1Canvas : MonoBehaviour
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Set reference in UI manager
            UIManager.Instance.Level1 = this;
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods



        #endregion
    }
}