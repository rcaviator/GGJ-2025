using UnityEngine;
using GGJ2025.Utilities;
using UnityEngine.InputSystem;

namespace GGJ2025
{
    public class PlayerBubble : Projectile
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            SetInitialData(Constants.Projectiles.PlayerBubble);

            Direction = Mouse.current.position.ReadValue();
            Direction = Camera.main.ScreenToWorldPoint(Direction);
            Direction = Direction - (Vector2)transform.position;
            Direction = Direction.normalized;

            Size = Random.Range(Size * Constants.PLAYER_BUBBLE_MINIMUM_SIZE_SCALE, Size * Constants.PLAYER_BUBBLE_MAXIMUM_SIZE_SCALE);
            transform.localScale = new Vector3(Size, Size, 1);
            Speed = Random.Range(Speed * Constants.PLAYER_BUBBLE_MINIMUM_SPEED_SCALE, Speed * Constants.PLAYER_BUBBLE_MAXIMUM_SPEED_SCALE);
            transform.Rotate(0, 0, Random.Range(-Constants.PLAYER_BUBBLE_MAXIMUM_ANGLE_SPREAD, Constants.PLAYER_BUBBLE_MAXIMUM_ANGLE_SPREAD));
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        protected override (bool shouldDamage, bool shouldDestroy) GetHitHandling(Collider2D other)
        {
            var shouldDestroy = !other.CompareTag("Player");
            var shouldDamage = shouldDestroy && !other.TryGetComponent<SoapedObject>(out _);
            return (shouldDamage, shouldDestroy);
        }

        #endregion
    }
}
