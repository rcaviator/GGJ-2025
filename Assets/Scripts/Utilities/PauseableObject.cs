using GGJ2025.Managers;
using UnityEngine;

namespace GGJ2025.Utilities
{
    public abstract class PauseableObject : MonoBehaviour
    {
        #region Fields

        // Component references
        protected Rigidbody2D? rBody;
        protected Animator? anim;

        // Rigidbody values
        //float gravity;
        Vector2 storedVelocity;
        //bool isSimulated;

        #endregion

        #region Properties

        /// <summary>
        /// The object ID gathered from Unity's GetInstanceID.
        /// Used for referencing this object in custom data collections.
        /// </summary>
        public int ObjectID
        { get; private set; }

        /// <summary>
        /// The main check for if this object is paused.
        /// </summary>
        public bool IsPaused
        { get; private set; }

        #endregion

        #region Unity Methods

        /// <summary>
        /// Called on object creation. Collects component references and registers
        /// itself to GameManager's PauseableObject list.
        /// </summary>
        protected virtual void Awake()
        {
            // Set component references
            if (GetComponent<Rigidbody2D>())
            {
                rBody = GetComponent<Rigidbody2D>();
            }

            if (GetComponent<Animator>())
            {
                anim = GetComponent<Animator>();
            }

            // Set reference in GameManager
            GameManager.Instance.AddPauseableObject(this);

            // Set the ObjectID
            ObjectID = gameObject.GetInstanceID();
        }

        /// <summary>
        /// Removes this object from GameManager's PauseableObject list.
        /// </summary>
        protected virtual void OnDestroy()
        {
            // Remove this object from the list
            GameManager.Instance.RemovePauseableObject(this);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Pauses the object.
        /// </summary>
        public void PauseObject()
        {
            // Pause this object
            IsPaused = true;

            // Rigidbody
            if (rBody != null)
            {
                // Set values only if applicable
                if (rBody.bodyType != RigidbodyType2D.Kinematic)
                {
                    // Store values and then disable simulated
                    //gravity = rBody.gravityScale;
                    storedVelocity = rBody.linearVelocity;
                    //isSimulated = rBody.simulated;

                    //rBody.gravityScale = 0;
                    rBody.linearVelocity = Vector2.zero;
                    rBody.bodyType = RigidbodyType2D.Kinematic;
                }
            }

            // Animator
            if (anim)
            {
                anim.speed = 0f;
            }
        }

        /// <summary>
        /// Unpauses the object.
        /// </summary>
        public void UnPauseObject()
        {
            // Unpause this object
            IsPaused = false;

            // Rigidbody
            if (rBody)
            {
                // Set values only if applicable
                if (rBody.bodyType == RigidbodyType2D.Kinematic)
                {
                    // Enable simulated and then set stored values back
                    //rBody.simulated = isSimulated;
                    //rBody.gravityScale = gravity;
                    rBody.bodyType = RigidbodyType2D.Dynamic;
                    rBody.linearVelocity = storedVelocity;
                }
            }

            // Animator
            if (anim)
            {
                anim.speed = 1f;
            }
        }

        #endregion

        #region Private Methods



        #endregion
    }
}