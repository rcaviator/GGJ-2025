using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Rendering;

namespace GGJ2025
{
    public class EnemyProjectileBehavior : MonoBehaviour
    {
        // Projectile travel speed in meters/second
        public float speed;
        //Damage of projectile
        public int damage;
        // Projectile's intended target and its transform
        public GameObject target;
        private Transform targetT;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            targetT = target.GetComponent<Transform>();
            //TODO: ???
        }

        // Update is called once per frame
        void FixedUpdate() {

        /// <summary>
        /// Advances projectile towards target
        /// </summary>
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,targetT.position, step);
        }

        /// <summary>
        /// Collision handler
        /// </summary>
        /// <param name="col">Target collider</param>
        void OnTriggerEnter2D(Collider2D col) {
            GameObject colTarget = col.gameObject;
            if (colTarget.GetComponent<SoapedObject>()) {
                //TODO: Handle collision with soaped object
            } else if (colTarget.tag == "Player") {
                //TODO: Handle collision with player
                //TODO: Damage player
            }
            Destroy(this);
        }
    }
}
