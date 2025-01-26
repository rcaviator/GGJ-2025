using UnityEngine;

namespace GGJ2025.Player
{
  [RequireComponent(typeof(Animator))]
  public class DamageFlash : MonoBehaviour
  {
    private Animator animator = null!;
    private static readonly int FlashParam = Animator.StringToHash("Flash");
    private float previousHealth = 1;

    int damageValue = 0;
    public AudioSource damageSource;

    public AudioClip damageSFX;

    private void Start()
    {
      animator = GetComponent<Animator>();
      EventManager.StartListening<float>(CustomEventType.PlayerHealth, OnHealthUpdated);
    }

    private void OnHealthUpdated(float value)
    {
           // damageValue = Random.Range(0, 9);
      if (value < previousHealth)
      {
        animator.SetTrigger(FlashParam);
        damageSource.Play();
      }

      previousHealth = value;
    }

    private void OnDestroy()
    {
      EventManager.StopListening<float>(CustomEventType.PlayerHealth, OnHealthUpdated);
    }
  }
}