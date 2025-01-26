using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ2025
{
    public class WheelParticleController : MonoBehaviour
    {
        [Header("Particle Settings")]
        [Tooltip("The main particle system component")]
        [SerializeField] private ParticleSystem wheelParticles;

        [Tooltip("Minimum speed required to show particles")]
        [SerializeField] private float minimumSpeedForParticles = 0.1f;

        [Tooltip("How quickly particles respond to direction changes")]
        [SerializeField] private float particleRotationSpeed = 10f;

        [Header("Emission Settings")]
        [Tooltip("Base rate of particle emission")]
        [SerializeField] private float baseEmissionRate = 10f;
        
        [Tooltip("How much to multiply emission by based on speed")]
        [SerializeField] private float emissionSpeedMultiplier = 5f;

        private PlayerController playerController;
        private ParticleSystem.EmissionModule emissionModule;
        private ParticleSystem.MainModule mainModule;
        private Vector2 lastMovementDirection;

        private void Start()
        {
            // Get references
            playerController = GetComponentInParent<PlayerController>();
            
            // If no particle system is assigned, create one
            if (wheelParticles == null)
            {
                CreateDefaultParticleSystem();
            }

            // Cache particle system modules
            emissionModule = wheelParticles.emission;
            mainModule = wheelParticles.main;

            // Initialize emission rate to 0
            emissionModule.rateOverTime = 0;
        }

        private void Update()
        {
            if (playerController == null) return;

            // Get current movement from the player controller
            Vector2 movement = playerController.MovementAmount;
            float speed = movement.magnitude;

            // Update particle emission based on speed
            if (speed > minimumSpeedForParticles)
            {
                // Calculate emission rate based on speed
                float currentEmissionRate = baseEmissionRate + (speed * emissionSpeedMultiplier);
                emissionModule.rateOverTime = currentEmissionRate;

                // Update particle direction - opposite to movement
                if (movement != Vector2.zero)
                {
                    lastMovementDirection = movement.normalized;
                    
                    // Calculate the angle for the particles to point opposite to movement
                    float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg + 180f;
                    
                    // Smoothly rotate the particle system
                    Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                    transform.rotation = Quaternion.Lerp(
                        transform.rotation, 
                        targetRotation, 
                        Time.deltaTime * particleRotationSpeed
                    );
                }

                if (!wheelParticles.isPlaying)
                {
                    wheelParticles.Play();
                }
            }
            else
            {
                emissionModule.rateOverTime = 0;
                if (wheelParticles.isPlaying)
                {
                    wheelParticles.Stop();
                }
            }
        }

        private void CreateDefaultParticleSystem()
        {
            // Create a new particle system
            GameObject particleObj = new GameObject("WheelParticles");
            particleObj.transform.SetParent(transform);
            particleObj.transform.localPosition = Vector3.zero;
            wheelParticles = particleObj.AddComponent<ParticleSystem>();

            // Configure the particle system
            var main = wheelParticles.main;
            main.loop = true;
            main.playOnAwake = false;
            main.duration = 1f;
            main.startSpeed = 1f;
            main.startSize = 0.1f;
            main.startLifetime = 0.5f;
            main.gravityModifier = 0.1f;
            main.simulationSpace = ParticleSystemSimulationSpace.World;

            // Configure the shape module for a cone emission
            var shape = wheelParticles.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.angle = 15f;
            shape.radius = 0.1f;

            // Add and configure the color over lifetime module
            var colorOverLifetime = wheelParticles.colorOverLifetime;
            colorOverLifetime.enabled = true;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { 
                    new GradientColorKey(new Color(0, 0, 0f), 0.0f),  // Brown color
                    new GradientColorKey(new Color(0, 0, 0f), 1.0f) 
                },
                new GradientAlphaKey[] { 
                    new GradientAlphaKey(1.0f, 0.0f), 
                    new GradientAlphaKey(0.0f, 1.0f) 
                }
            );
            colorOverLifetime.color = gradient;

            // Configure renderer
            var renderer = wheelParticles.GetComponent<ParticleSystemRenderer>();
            renderer.sortingLayerID = SortingLayer.NameToID("Default");
            renderer.sortingOrder = -1; // Behind the player
        }
    }
}