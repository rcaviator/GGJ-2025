using UnityEngine;

namespace GGJ2025.Player
{
  [RequireComponent(typeof(Animator))]
  public class DamageFlash : MonoBehaviour
  {
    private Animator animator = null!;
    private static readonly int FlashParam = Animator.StringToHash("Flash");
    private float? previousHealth;

    private void Start()
    {
      animator = GetComponent<Animator>();
      EventManager.StartListening<float>(CustomEventType.PlayerHealth, OnHealthUpdated);
    }

    private void OnHealthUpdated(float value)
    {
      if (value < previousHealth)
      {
        animator.SetTrigger(FlashParam);
      }

      previousHealth = value;
    }

    private void OnDestroy()
    {
      EventManager.StopListening<float>(CustomEventType.PlayerHealth, OnHealthUpdated);
    }
  }
}