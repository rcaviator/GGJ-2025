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

        InputAction moveAction = null!;
        InputAction shootAction = null!;

        Vector2 moveAmount;
        Vector2 mousePosition;

        float bubbleSpawnCoolDown;
        float bubbleGunStartSoundDuration;
        float bubbleGunStartSoundTimer;
        bool bubbleGunLoopSoundFired;

        #endregion

        #region Properties

        public bool Initialized
        { get; private set; }

        #endregion

        #region Unity Methods

        private IEnumerator Start()
        {
            yield return prefabLoader.WaitForInitialized();

            moveAction = InputSystem.actions.FindAction("Move");
            shootAction = InputSystem.actions.FindAction("Attack");

            Initialized = true;
        }

        private void Update()
        {
            if (!Initialized)
            {
                return;
            }

            // movement
            moveAmount = moveAction.ReadValue<Vector2>();
            Vector2.ClampMagnitude(moveAmount, 1);
            transform.Translate(moveAmount * Constants.PLAYER_SPEED * Time.deltaTime);

            mousePosition = Mouse.current.position.ReadValue();
            
            // bubble gun control
            if (bubbleSpawnCoolDown >= Constants.PLAYER_BUBBLE_PROJECTILE_COOL_DOWN)
            {
                if (Mouse.current.leftButton.isPressed)
                {
                    bubbleSpawnCoolDown = 0;

                    PlayerBubble bubble = Instantiate(prefabLoader.Prefab, transform.position, Quaternion.identity).GetComponent<PlayerBubble>();
                }
            }
            else
            {
                bubbleSpawnCoolDown += Time.deltaTime;
            }

            // bubble gun sounds
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

        #endregion

        #region Public Methods

        public void OnHealthUpdated(Health health)
        {
            EventManager.Invoke(CustomEventType.PlayerHealth, health.Current / health.Max);
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
