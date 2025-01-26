using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ2025.Player
{
  public class TrashTracker : MonoBehaviour
  {
    [SerializeField] private UnityEvent onTrashCleared = new();

    private int trashCount;

    private void Awake()
    {
      EventManager.StartListening<bool>(CustomEventType.Trash, OnTrash);
    }

    private void OnTrash(bool exists)
    {
      if (exists)
      {
        trashCount++;
      }
      else
      {
        trashCount--;
        if (trashCount <= 0)
        {
          StartCoroutine(OnTrashCleared());
        }
      }
    }

    private void OnDestroy()
    {
      EventManager.StopListening<bool>(CustomEventType.Trash, OnTrash);
    }

    private IEnumerator OnTrashCleared()
    {
      yield return new WaitForSeconds(2);
      onTrashCleared.Invoke();
    }
  }
}