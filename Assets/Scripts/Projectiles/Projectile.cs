using UnityEngine;
using GGJ2025.Utilities;

namespace GGJ2025
{
    public abstract class Projectile : MonoBehaviour
    {
        #region Fields



        #endregion

        #region Properties

        public float Size
        { get; protected set; }

        public float Speed
        { get; protected set; }

        public Vector2 Direction
        { get; set; }

        public float LifeTime
        { get; private set; }

        public float CurrentTime
        { get; private set; }

        public float Damage
        { get; private set; }    

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {

        }

        protected virtual void Update()
        {
            if (CurrentTime < LifeTime)
            {
                CurrentTime += Time.deltaTime;

                transform.Translate(Direction * (Speed * Time.deltaTime));
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region Public Methods



        #endregion

        #region Protected Methods

        protected void SetInitialData(Constants.Projectiles projectileType)
        {
            Constants.Projectile retrievedProjectile = Constants.GetProjectileData(projectileType);

            Size = retrievedProjectile.SIZE;
            Speed = retrievedProjectile.SPEED;
            LifeTime = retrievedProjectile.LIFETIME;
            Damage = retrievedProjectile.DAMAGE;
        }

        protected abstract (bool shouldDamage, bool shouldDestroy) GetHitHandling(Collider2D other);

        #endregion

        #region Private Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            var (shouldDamage, shouldDestroy) = GetHitHandling(other);
            if (shouldDamage && other.TryGetComponent(out Health health))
            {
                health.Current -= Damage;
            }

            if (shouldDestroy)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
