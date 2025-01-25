using UnityEngine;

namespace GGJ2025
{
    public class Projectile : MonoBehaviour
    {
        #region Fields



        #endregion

        #region Properties

        public float Size
        { get; private set; }

        public float Speed
        { get; private set; }

        public Vector2 Direction
        { get; private set; }

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

        }

        #endregion

        #region Public Methods



        #endregion

        #region Protected Methods

        protected void SetInitialData()
        {

        }

        #endregion

        #region Private Methods



        #endregion
    }
}
