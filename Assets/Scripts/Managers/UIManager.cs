using GGJ2025.UI;

namespace GGJ2025.Managers
{
    /// <summary>
    /// UIManager is the singleton that handles all the UI in the game.
    /// </summary>
    class UIManager
    {
        #region Fields

        // Singleton instance of the class
        static UIManager instance;

        #endregion

        #region Constructor

        /// <summary>
        /// Private constructor.
        /// </summary>
        private UIManager()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the instance.
        /// </summary>
        public static UIManager Instance
        {
            get { return instance ??= new UIManager(); }
        }

        /// <summary>
        /// The main menu canvas.
        /// </summary>
        public MainMenuCanvas MainMenu
        { get; set; }

        /// <summary>
        /// The level 1 canvas.
        /// </summary>
        public Level1Canvas Level1
        { get; set; }

        /// <summary>
        /// The win menu canvas.
        /// </summary>
        public WinCanvas Win
        { get; set; }

        /// <summary>
        /// The lose menu canvas.
        /// </summary>
        public LoseCanvas Lose
        { get; set; }

        /// <summary>
        /// The credits menu canvas.
        /// </summary>
        public CreditsCanvas Credits
        { get; set; }

        #endregion
    }
}