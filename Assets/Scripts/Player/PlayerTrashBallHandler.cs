using GGJ2025.Soap;
using GGJ2025.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ2025.Player
{
  public class PlayerTrashBallHandler : MonoBehaviour
  {
    [SerializeField] private PrefabLoader projectileLoader = null!;

    private bool collectedTrash;
    private Trash? currentTrash;

    public void OnInput(InputAction.CallbackContext context)
    {
      if (context.phase != InputActionPhase.Performed)
      {
        return;
      }

      if (collectedTrash)
      {
        if (projectileLoader.Initialized && Camera.main is { } cam)
        {
          SetCollectedTrash(false);
          var mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
          var projectile = Instantiate(projectileLoader.Prefab, transform.position, Quaternion.identity);
          projectile.GetComponent<PlayerTrashBall>().SetTargetLocation(mousePosition);
        }
      }
      else if (currentTrash != null && currentTrash.TryGetComponent(out SoapedObject soap) && soap.Cleaned)
      {
        Destroy(currentTrash.gameObject);
        SetCollectedTrash(true);
      }
    }

    private void SetCollectedTrash(bool value)
    {
      collectedTrash = value;
      EventManager.Invoke(CustomEventType.PlayerHasBall, value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out Trash trash))
      {
        currentTrash = trash;
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (currentTrash != null && other.TryGetComponent(out Trash trash) && currentTrash == trash)
      {
        currentTrash = null;
      }
    }
  }
}