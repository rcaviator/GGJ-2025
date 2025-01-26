using UnityEngine;
using UnityEngine.UI;

namespace GGJ2025.Player
{
  public class HealthBar : MonoBehaviour
  {
    [SerializeField] private Image bar = null!;

    private void Start()
    {
      EventManager.StartListening<float>(CustomEventType.PlayerHealth, OnHealthUpdated);
    }

    private void OnHealthUpdated(float value)
    {
      bar.fillAmount = value;
    }

    private void OnDestroy()
    {
      EventManager.StopListening<float>(CustomEventType.PlayerHealth, OnHealthUpdated);
    }
  }
}