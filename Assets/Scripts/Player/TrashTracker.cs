using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ2025.Player
{
  public class TrashTracker : MonoBehaviour
  {
    [SerializeField] private UnityEvent onTrashCleared = new();
        [SerializeField] AudioSource windSource;
        [SerializeField] AudioSource birdSource;

        private int trashCount;
    private int maxTrash;

    private void Awake()
    {
      EventManager.StartListening<bool>(CustomEventType.Trash, OnTrash);
      
      maxTrash = trashCount;
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
        //windSource.volume = trashCount/maxTrash;
        //birdSource.volume = (1-trashCount)/maxTrash;
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