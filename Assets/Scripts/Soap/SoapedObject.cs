using UnityEngine;

namespace GGJ2025
{
    /// <summary>
    /// An object that has been soaped, handles visuals and soap timr.
    /// </summary>
    public class SoapedObject : MonoBehaviour
    {
        private static readonly int SoapedParam = Animator.StringToHash("Soaped");

        /// <summary>
        /// When the object is first soaped, add soap visuals and start timer
        /// </summary>
        private void Awake()
        {
            if (TryGetComponent(out Animator animator))
            {
                animator.SetBool(SoapedParam, true);
            }

            // TODO: Add soap "target" to enemy
            // TODO: Start timer for soap to end and this object be "cleaned"
        }

        /// <summary>
        /// When the object is attacked by an enemy, unsoap it.
        /// </summary>
        private void OnUnsoap()
        {
            Destroy(this); // Only destroys the script, not the game object
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
