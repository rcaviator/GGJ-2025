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
            transform.Translate(moveAmount * 10 * Time.deltaTime);

            
            mousePosition = Mouse.current.position.ReadValue();
            
        }

        #endregion

        #region Public Methods

        public void OnMove(InputAction.CallbackContext context)
        {
            moveAmount = context.ReadValue<Vector2>();
            Vector2.ClampMagnitude(moveAmount, 1);
            transform.Translate(moveAmount * 10 * Time.deltaTime);
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            Instantiate(prefabLoader.Prefab);
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
