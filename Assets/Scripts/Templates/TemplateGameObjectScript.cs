using GGJ2025.Utilities;

namespace GGJ2025.Templates // <- UPDATE TO USER-SPECIFIC NAMESPACE
{
    public class TemplateGameObjectScript : PauseableObject // <- RENAME CLASS (AND FILE) AND INHERIT FROM THE APPROPRIATE CLASS
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        /// <summary>
        /// Unity Awake method acts as this object's contructor.
        /// Gather component references, set initial values, etc.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();


        }

        /// <summary>
        /// Unity Start method is called only once and right before
        /// its first rendered frame.
        /// Set any other references that are time and/or order depended
        /// from Awake here.
        /// </summary>
        //protected virtual void Start()
        //{

        //}

        /// <summary>
        /// Main Unity update method. Called once per frame.
        /// </summary>
        protected virtual void Update()
        {
            // Do not process if this game object is paused
            if (IsPaused) return;


        }

        /// <summary>
        /// Framerate-independent update method. Default rate is called
        /// 50 times a second. Physics component updates are often placed here.
        /// </summary>
        //protected virtual void FixedUpdate()
        //{
        //    // Do not process if this game object is paused
        //    if (IsPaused) return;


        //}

        /// <summary>
        /// Secondary Unity update method. Called once per frame
        /// after the main Update method. Camera follow movements
        /// are often placed in here.
        /// </summary>
        //protected virtual void LateUpdate()
        //{
        //    // Do not process if this game object is paused
        //    if (IsPaused) return;


        //}

        /// <summary>
        /// Any additional object cleanup or other game actions
        /// based on this object's destruction can be placed here.
        /// </summary>
        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();


        //}

        /// <summary>
        /// Unity object area trigger enter method.
        /// Collider (set to is-trigger) and Rigidbody components must
        /// be present on this object for this method to trigger.
        /// </summary>
        //protected virtual void OnTriggerEnter(Collider other)
        //{

        //}

        /// <summary>
        /// Unity object area trigger exit method.
        /// Collider (set to is-trigger) and Rigidbody components must
        /// be present on this object for this method to trigger.
        /// </summary>
        //protected virtual void OnTriggerExit(Collider other)
        //{

        //}

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods



        #endregion
    }
}