using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ2025
{
    public class GunController: MonoBehaviour
    {
        [Header("References")]    
        [Tooltip("Reference to the sprite renderer to flip the gun when needed")]
        [SerializeField] private SpriteRenderer gunSprite;

        [Tooltip("Reference to the player's sprite renderer")]
        [SerializeField] private SpriteRenderer playerSprite;

        [Tooltip("The point where bullets spawn from")]
        [SerializeField] private Transform firePoint;

        [Header("Layer Settings")]
        [Tooltip("Order in layer value when gun should appear behind player")]
        [SerializeField] private int behindPlayerOrder = -1;
        
        [Tooltip("Order in layer value when gun should appear in front of player")]
        [SerializeField] private int frontPlayerOrder = 1;

        [Header("Settings")]
        [Tooltip("Offset from player center where the gun should be positioned")]
        [SerializeField] private Vector2 gunOffset = new Vector2(0.2f, -0.1f);

        private Camera mainCamera;
        private Vector2 mousePosition;
        private Vector2 currentDirection;
        private Animator playerAnimator;

        // Cache the animator hash IDs
        private readonly int horizontalHash = Animator.StringToHash("Horizontal");
        private readonly int verticalHash = Animator.StringToHash("Vertical");

        public Vector2 FireDirection => currentDirection;
        public Vector3 FirePosition => firePoint != null ? firePoint.position : transform.position;


        private void Start()
        {
            // Cache the main camera reference
            mainCamera = Camera.main;

            // Get reference to player's animator
            playerAnimator = transform.parent.GetComponent<Animator>();
            
            if (playerSprite == null)
            {
                playerSprite = transform.parent.GetComponent<SpriteRenderer>();
            }

            // Create firePoint if it doesn't exist
            if (firePoint == null)
            {
                GameObject firePointObj = new GameObject("FirePoint");
                firePoint = firePointObj.transform;
                firePoint.SetParent(transform);
                // Position it at the tip of the gun - adjust these values based on your gun sprite
                firePoint.localPosition = new Vector3(0.3f, 0, 0);
            }
        }

        private void Update()
        {
            UpdateGunPosition();
            UpdateGunLayering();
        }

        private void UpdateGunPosition()
        {
            // Get current mouse position in screen space
            mousePosition = Mouse.current.position.ReadValue();
            
            // Convert mouse position to world space
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
            mouseWorldPosition.z = 0; // Ensure we stay in 2D space

            // Calculate the direction from gun to mouse
            currentDirection = (mouseWorldPosition - transform.position).normalized;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;

            // Apply rotation to the gun
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Flip the gun sprite if pointing left
            // This assumes your gun sprite is originally pointing right
            if (gunSprite != null)
            {
                gunSprite.flipY = angle > 90 || angle < -90;
            }

            // Update gun position relative to parent (player)
            // This ensures the gun stays at a consistent offset from the player
            transform.localPosition = new Vector3(gunOffset.x, gunOffset.y, 0);
        }

        private void UpdateGunLayering()
        {
            if (gunSprite == null || playerAnimator == null) return;

            // Get the current movement direction from the animator
            float verticalDirection = playerAnimator.GetFloat(verticalHash);
            float horizontalDirection = playerAnimator.GetFloat(horizontalHash);
            
            // Use the larger component to determine the primary direction
            if (Mathf.Abs(verticalDirection) > Mathf.Abs(horizontalDirection))
            {
                // Vertical movement is dominant
                if (verticalDirection < -0.1f)  // Facing upward
                {
                    // Gun should be in front of player when facing up
                    gunSprite.sortingOrder = frontPlayerOrder;
                }
                else if (verticalDirection > 0.1f)  // Facing downward
                {
                    // Gun should be behind player when facing down
                    gunSprite.sortingOrder = behindPlayerOrder;
                }
            }
            else
            {
                // Horizontal movement is dominant
                if (horizontalDirection > 0.1f)  // Facing east/right
                {
                    // Gun should be in front of player when facing east
                    gunSprite.sortingOrder = frontPlayerOrder;
                }
                else if (horizontalDirection < -0.1f)  // Facing west/left
                {
                    // Gun should be behind player when facing west
                    gunSprite.sortingOrder = behindPlayerOrder;
                }
            }
        }

        // This method can be called to adjust the gun's position if needed
        public void SetGunOffset(Vector2 newOffset)
        {
            gunOffset = newOffset;
        }
    }
}