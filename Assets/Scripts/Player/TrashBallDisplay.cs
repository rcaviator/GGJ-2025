using UnityEngine;

namespace GGJ2025.Player
{
  public class TrashBallDisplay : MonoBehaviour
  {
    [SerializeField] private GameObject display = null!;

    private void Start()
    {
      EventManager.StartListening<bool>(CustomEventType.PlayerHasBall, OnUpdated);
    }

    private void OnUpdated(bool value)
    {
      display.SetActive(value);
    }

    private void OnDestroy()
    {
      EventManager.StopListening<bool>(CustomEventType.PlayerHasBall, OnUpdated);
    }
  }
}