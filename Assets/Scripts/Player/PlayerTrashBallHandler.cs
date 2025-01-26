using System.Collections.Generic;
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
    private readonly List<Trash> currentTrash = new();

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
      else
      {
        foreach (var trash in currentTrash)
        {
          if (trash.TryGetComponent(out SoapedObject soap) && soap.Cleaned)
          {
            Destroy(trash.gameObject);
            SetCollectedTrash(true);
            break;
          }
        }
      }
    }

    private void SetCollectedTrash(bool value)
    {
      collectedTrash = value;
      EventManager.Invoke(CustomEventType.PlayerHasBall, value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out Trash trash) && !currentTrash.Contains(trash))
      {
        currentTrash.Add(trash);
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.TryGetComponent(out Trash trash))
      {
        currentTrash.Remove(trash);
      }
    }
  }
}