using System;
using System.Collections;
using GGJ2025.Utilities;

// using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

namespace GGJ2025
{
    /// <summary>
    /// Enemy behavior script.
    /// Targets trash by default
    /// </summary>
    public class EnemyNavigation : MonoBehaviour
    {
        #region Fields
        // Boolean to make enemy focus the player
        [SerializeField]
        bool focusPlayerInsteadOfTrash;

        // Boolean for if enemy is ranged rather than melee
        [SerializeField]
        bool isRanged = false;
        // Intended target
        public GameObject target;
        // Enemy projectile object
        public GameObject projectile = null;
        //Damage for attacks
        public int damage;
        //Delay between attacks in 1/60 second intervals
        public int attackDelay = 60;
        //Tick counter variable for attack delay
        private int delayCounter = 0;
        //Projectile speed, m/s
        public float projectileSpeed;
        //Speed of enemy, m/s
        public float speed = 10;
        //Rigidbody of this object
        private Rigidbody2D rb;
        //Target position
        //Minimum distance enemy can be to player
        public float minDistance = 1;
        //Distance required to bite target
        public float biteDistance = 1;
        Vector3 targetPos;
        #endregion

        void Awake() {
            rb = gameObject.GetComponent<Rigidbody2D>();
            //Set target
            if (focusPlayerInsteadOfTrash) {
                //Set player as target
                target = GameObject.FindWithTag("Player");
                // TODO: Add player as target
            } else {
                //Sets target as closest trash object to enemy
                target = GetClosestTrash();
            }
        }
        void Start()
        {

            //Sets a reference to target's position
            targetPos = target.GetComponent<Transform>().position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //Updates target position
            targetPos = target.GetComponent<Transform>().position;
            MoveTowardsTarget();

            //Increments delay counter variable
            delayCounter++;

            //Checks if delay counter meets or exceeds attack spacing delay

            if (delayCounter >= attackDelay) {
                delayCounter = 0;
                if (isRanged) {
                    ShootProjectile();
                } else {
                     //Checks if target is within certain distance
                    float distance = Vector3.Distance(transform.position, target.transform.position);
                    if(distance <= biteDistance) {
                        Bite();
                    }
                }

            }


        }

        /// <summary>
        /// Bites a thing
        /// </summary>
        void Bite() {

            if (target.GetComponent<SoapedObject>()) {
            //TODO: Handle collision with soaped object
            } else if (target.tag == "Player") {
                Debug.Log("om nom");
                target.GetComponent<Health>().Current -= damage;
            } else {
                return;
            }

            //TODO: Damage target
        }

        /// <summary>
        /// Spawns a projectile that focuses selected target
        /// </summary>
        void ShootProjectile() {
            //Initiates a clone
            GameObject projClone = Instantiate(projectile, transform.position, transform.rotation);
            EnemyProjectileBehavior projBehavior = projClone.GetComponent<EnemyProjectileBehavior>();
            projBehavior.target = target;
            projBehavior.damage = damage;
            projBehavior.speed = projectileSpeed;

        }

        /// <summary>
        /// Gets closest trash instance to enemy
        /// </summary>
        /// <returns> Nearest trash instancem</returns>
        GameObject GetClosestTrash() {
            //Initiates closest trash object as null
            GameObject closest = null;
            //Sets initial distance as infinity
            float minimumDistance = Mathf.Infinity;

            Vector3 currentPosition = transform.position;
            Transform trashT = null;

            //Iterates through every trash object, finding closest one. Greedy algorithm.
            foreach (GameObject trash in GameObject.FindGameObjectsWithTag("Trash")) {
                trashT = trash.GetComponent<Transform>();
                float distance = Vector3.Distance(trashT.position, currentPosition);
                if (distance < minimumDistance) {
                    minimumDistance = distance;
                    closest = trash;
                }
            }

            return closest;
        }

        /// <summary>
        /// Move towards target
        /// </summary>
        void MoveTowardsTarget() {
            //Distance to step during frame / tick
            float step = speed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, targetPos);
            // Debug.Log(distance);
            // Check if the distance to target is greater than or equal to minimum distance
            if (distance >= minDistance) {
                // Move towards target.
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            }
        }

        IEnumerator OnTriggerEnter2D(Collider2D collider) {
            if (collider.tag == "Trash Ball") {
                Destroy(this.gameObject);
            } else if (collider.tag == ("Bubble")) {
                Destroy(collider.gameObject);
                Debug.Log("STOP IN THE NAME OF THE LOL");
                float tempSpeed = speed;
                speed = 0;
                yield return new WaitForSeconds(2);
                Debug.Log("FREE2GO");
                speed = tempSpeed;
            }
        }
    }
}
