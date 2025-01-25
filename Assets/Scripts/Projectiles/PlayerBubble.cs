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
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods



        #endregion
    }
}
