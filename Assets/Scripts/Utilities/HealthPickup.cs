using GGJ2025.Player;
using GGJ2025.Utilities;
using UnityEditor;
using UnityEngine;

namespace GGJ2025
{
    /// <summary>
    /// Increases Health
    /// </summary>
    public class HealthPickup : MonoBehaviour
    {
        // Health fields
        Health Health;
        HealthBar HealthBar;

        private void Start()
        {
            Health.GetComponent<Health>();
            HealthBar.GetComponent<HealthBar>();
        }
        /// <summary>
        /// Replenish Player Health On Collision
        /// </summary>
        /// <param name="coll"></param>
        public void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                // replenish health
                Health.Current = Health.Max;
                // destroy health object
                Destroy(gameObject);
            }
        }
    }
}
