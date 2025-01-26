using GGJ2025.Utilities;
using UnityEngine;

namespace GGJ2025
{
    /// <summary>
    /// Increases Health
    /// </summary>
    public class HealthPickup : MonoBehaviour
    {
        /// <summary>
        /// Replenish Player Health On Collision
        /// </summary>
        /// <param name="coll"></param>
        public void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag("Player") && coll.TryGetComponent(out Health health))
            {
                // replenish health
                health.Current += health.Max;

                // destroy health object
                Destroy(gameObject);
            }
        }
    }
}
