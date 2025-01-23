using UnityEngine;
using Game.Managers;

namespace Game.UI
{
    public class WinCanvas : MonoBehaviour
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Set reference in UI manager
            UIManager.Instance.Win = this;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Goes to the main menu scene.
        /// </summary>
        public void OnMainMenuButtonClick()
        {
            MySceneManager.Instance.ChangeScene(MySceneManager.Scenes.MainMenu);
        }

        #endregion

        #region Private Methods



        #endregion
    }
}