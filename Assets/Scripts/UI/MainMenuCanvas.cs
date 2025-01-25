using GGJ2025.Managers;
using UnityEngine;

namespace GGJ2025.UI
{
    public class MainMenuCanvas : MonoBehaviour
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Set reference in UI manager
            UIManager.Instance.MainMenu = this;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the game and goes to the level 1 scene.
        /// </summary>
        public void OnStartGameButtonClick()
        {
            MySceneManager.Instance.ChangeScene(MySceneManager.Scenes.Level1);
        }

        /// <summary>
        /// Goes to the credits scene.
        /// </summary>
        public void OnCreditsButtonClick()
        {
            MySceneManager.Instance.ChangeScene(MySceneManager.Scenes.Credits);
        }

        /// <summary>
        /// Quits the game.
        /// </summary>
        public void OnQuitButtonClick()
        {
            Application.Quit();
        }

        #endregion

        #region Private Methods



        #endregion
    }
}