using System.Collections;
using UnityEngine;

namespace GGJ2025
{
    /// <summary>
    /// An object that has been soaped, handles visuals and soap timr.
    /// </summary>
    public class SoapedObject : MonoBehaviour
    {
        private static readonly int SoapedParam = Animator.StringToHash("Soaped");

        public bool Cleaned { get; private set; }

        /// <summary>
        /// When the object is first soaped, add soap visuals and start timer
        /// </summary>
        private void Start()
        {
            if (TryGetComponent(out Animator animator))
            {
                animator.SetBool(SoapedParam, true);
            }
        }

        /// <summary>
        /// When the object is attacked by an enemy, unsoap it.
        /// </summary>
        private void OnUnsoap()
        {
            Destroy(this); // Only destroys the script, not the game object
        }

        public void OnCleaned()
        {
            // Call from animation event
            Cleaned = true;
        }

        private void OnDestroy()
        {
            if (TryGetComponent(out Animator animator))
            {
                animator.SetBool(SoapedParam, false);
            }
        }
    }
}
