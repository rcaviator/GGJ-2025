using GGJ2025.Managers;
using GGJ2025.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ2025
{
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        PrefabLoader prefabLoader = null!;

        [SerializeField]
        private GunController gunController = null!;

        // Animation-related fields:
        [SerializeField]
        private Animator animator = null!;      // Reference to animator component

        // Animation parameter hashes for better performance:
        private readonly int horizantalHash = Animator.StringToHash("Horizontal");
        private readonly int verticalHash = Animator.StringToHash("Vertical");
        private readonly int isMovingHash = Animator.StringToHash("IsMoving");
        private readonly int isIdleHash = Animator.StringToHash("IsIdle");

        private readonly int dieHash = Animator.StringToHash("Die");
        private readonly int isDeadHash = Animator.StringToHash("IsDead");

        InputAction moveAction = null!;
        InputAction shootAction = null!;

        Vector2 moveAmount;
        Vector2 mousePosition;

        float bubbleSpawnCoolDown;
        float bubbleGunStartSoundDuration;
        float bubbleGunStartSoundTimer;
        bool bubbleGunLoopSoundFired;

        bool isDead = false;

        #endregion

        #region Properties

        public bool Initialized
        { get; private set; }

        public Vector2 MovementAmount => moveAmount;

        #endregion

        #region Unity Methods

        private IEnumerator Start()
        {
            // Wait for prefab loader:
            yield return prefabLoader.WaitForInitialized();

            // Get input actions:
            moveAction = InputSystem.actions.FindAction("Move");
            shootAction = InputSystem.actions.FindAction("Attack");

            // Get the Animator component:
            animator = GetComponent<Animator>();
            if (animator == null) {
                Debug.LogError("PlayerController: Animator component not found!");
            }

            Initialized = true;
        }

        private void Update()
        {
            if (!Initialized)
            {
                return;
            }

            HandleMovement();
            HandleShooting();
            UpdateAnimations();
        }

        #endregion

        #region Private Methods

        private void HandleMovement()
        {
            moveAmount = moveAction.ReadValue<Vector2>();
            Vector2.ClampMagnitude(moveAmount, 1);
            
            transform.Translate(moveAmount * Constants.PLAYER_SPEED * Time.deltaTime);

            mousePosition = Mouse.current.position.ReadValue();
        }

        private void HandleShooting()
        {
            // Don't allow shooting if there's no gun:
            if (gunController == null) return;

            if (bubbleSpawnCoolDown >= Constants.PLAYER_BUBBLE_PROJECTILE_COOL_DOWN)
            {
                if (Mouse.current.leftButton.isPressed)
                {
                    bubbleSpawnCoolDown = 0;

                    // Spawn the bubble at the gun's fire position
                    PlayerBubble bubble = Instantiate(
                        prefabLoader.Prefab, 
                        gunController.FirePosition, 
                        Quaternion.identity
                    ).GetComponent<PlayerBubble>();

                    if (bubble != null) {
                        bubble.SetInitialDirection(gunController.FireDirection);
                    }
                }
            }
            else
            {
                bubbleSpawnCoolDown += Time.deltaTime;
            }

            HandleBubbleGunSounds();
        }

        private void HandleBubbleGunSounds()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.BubbleGunStart);

                bubbleGunStartSoundDuration = AudioManager.Instance.GetAudioClip(GameSoundEffect.BubbleGunStart).length;
            }
            

            if (bubbleGunStartSoundTimer < bubbleGunStartSoundDuration && Mouse.current.leftButton.IsPressed())
            {
                bubbleGunStartSoundTimer += Time.deltaTime;
            }
            else if (bubbleGunStartSoundTimer > bubbleGunStartSoundDuration && Mouse.current.leftButton.IsPressed() && !bubbleGunLoopSoundFired)
            {
                bubbleGunLoopSoundFired = true;
                AudioManager.Instance.PlayLoopedGamePlaySoundEffect(GameSoundEffect.BubbleGunLoop);
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                AudioManager.Instance.StopLoopedGameSoundEffect();
                bubbleGunStartSoundTimer = 0;
                bubbleGunLoopSoundFired = false;
            }
        }

        private void UpdateAnimations()
        {
            if (animator == null || isDead) return; 
            
            // Convert mouse position from screen to world coordinates:
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

            bool isMoving = moveAmount.magnitude > 0.01f;
            bool isShooting = Mouse.current.leftButton.isPressed;

            Vector2 currentDirection;

            if (isShooting)
            {
                // Use aim direction when shooting
                currentDirection = (mouseWorldPosition - transform.position).normalized;
            }
            else if (isMoving)
            {
                // Use movement direction when moving
                currentDirection = moveAmount.normalized;                  
            }
            else 
            {
                // When idle, keep the last direction but update the idle state
                currentDirection = new Vector2(
                    animator.GetFloat(horizantalHash),
                    animator.GetFloat(verticalHash)
                );
            }

            // Update the direction in the animator
            animator.SetFloat(horizantalHash, currentDirection.x);
            animator.SetFloat(verticalHash, currentDirection.y);

            // Update state booleans
            animator.SetBool(isMovingHash, isMoving);
            animator.SetBool(isIdleHash, !isMoving && !isShooting);
        }

        #endregion

        #region Public Methods

        public void OnHealthUpdated(Health health)
        {
            EventManager.Invoke(CustomEventType.PlayerHealth, health.Current / health.Max);
        }

        public void OnPlayerDeath()
        {
            if (!isDead) {
                isDead = true;
                animator.SetTrigger(dieHash);
                animator.SetBool(isDeadHash, true);

                // Optionally disable movement and other player actions:
                enabled = false;
            }
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
