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

            Size = Random.Range(Size * .75f, Size * 1.25f);
            transform.localScale = new Vector3(Size, Size, 1);
            Speed = Random.Range(Speed * .75f, Speed * 1.25f);
            transform.Rotate(0, 0, Random.Range(-15, 15));
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
