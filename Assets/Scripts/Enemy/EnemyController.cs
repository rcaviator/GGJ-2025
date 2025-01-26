using System;
using System.Collections;
using GGJ2025.Utilities;
using UnityEngine.Events;

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

        [SerializeField]
        bool focusPlayerInsteadOfTrash;// Boolean to make enemy focus the player


        [SerializeField]
        bool isRanged = false;         // Boolean for if enemy is ranged rather than melee

        public GameObject target;      // Intended target

        public GameObject projectile = null;// Enemy projectile object

        public int damage;             //Damage for attacks

        public int attackDelay = 60;   //Delay between attacks in 1/60 second intervals

        private int delayCounter = 0;  //Tick counter variable for attack delay

        public float projectileSpeed;  //Projectile speed, m/s

        public float speed = 10;       //Speed of enemy, m/s

        private Rigidbody2D rb;        //Rigidbody of this object


        public float minDistance = 1;  //Minimum distance enemy can be to player

        public float biteDistance = 1; //Distance required to bite target

        Vector3 targetPos;             //Target position

        public Animator animator;      //Animator component

        private bool moving = false;
        #endregion

        void Awake() {
            rb = gameObject.GetComponent<Rigidbody2D>();
            animator = gameObject.GetComponent<Animator>();
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
            moving = false;
            animator.SetTrigger("Bite");
            if (target.GetComponent<SoapedObject>()) {
            //TODO: Handle collision with soaped object
            } else if (target.tag == "Player") {
                Debug.Log("om nom");
                target.GetComponent<Health>().Current -= damage;
            } else {
                return;
            }
        }

        /// <summary>
        /// Spawns a projectile that focuses selected target
        /// </summary>
        void ShootProjectile() {
            //Initiates a clone
            animator.SetTrigger("Bite");
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
                moving = true;
                // Move towards target.
                if (moving) {
                    animator.SetTrigger("Move");
                    moving = false;
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            } else {
                moving = false;
                animator.SetTrigger("Idle"); //Play idle animation
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

        void MovingPing() {
            Debug.Log("Schmoovin\'");
        }

        void IdlePing() {
            Debug.Log("Stop");
        }

        void BitePing() {
            Debug.Log("Chompp");
        }
    }
}
