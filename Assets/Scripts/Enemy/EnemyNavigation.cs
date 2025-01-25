using System;
using UnityEngine;

namespace GGJ2025
{
    /// <summary>
    /// Enemy behavior script.
    /// Targets trash by default
    /// </summary>
    public class EnemyNavigation : MonoBehaviour
    {
        // Boolean to make enemy focus the player
        public bool focusPlayerInsteadOfTrash;

        // Boolean for if enemy is ranged rather than melee
        public bool isRanged = false;
        // Intended target
        public GameObject target;
        // Enemy projectile object
        public GameObject projectile = null;
        //Damage for attacks
        public int damage;
        //Delay between attacks in 1/60 second intervals
        public int attackDelay = 60;
        //Projectile speed, m/s
        public float projectileSpeed;
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        void Start()
        {
            //Set target
            if (focusPlayerInsteadOfTrash) {
                // TODO: Add player as target
            } else {
                // TODO: Set target as nearest trash pile
            }

        }

        // Update is called once per frame
        void Update()
        {
            //TODO: Movement and attacks
        }


        void Bite() {
            //TODO: Detect target within certain distance
            //TODO: Damage target
        }

        /// <summary>
        /// Spawns a projectile that focuses selected target
        /// </summary>
        void SpawnProjectile() {
            GameObject projClone = Instantiate(projectile, transform);
            EnemyProjectileBehavior projBehavior = projClone.GetComponent<EnemyProjectileBehavior>();
            projBehavior.target = target;
            projBehavior.damage = damage;
            projBehavior.speed = projectileSpeed;

        }

    }
}
