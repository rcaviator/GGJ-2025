using UnityEngine;
using UnityEngine.Events;

namespace GGJ2025.Utilities
{
  public class Health : MonoBehaviour
  {
    [SerializeField] private float max = 1;
    [SerializeField] private UnityEvent<Health> onUpdated = new();
    [SerializeField] private UnityEvent<Health> onEmpty = new();

    private float current;

    private void Start()
    {
      current = max;
    }

    public float Max => max;

    public float Current
    {
      get => current;
      set
      {
        current = Mathf.Clamp(value, 0, max);
        onUpdated.Invoke(this);
        if (value <= 0)
        {
          onEmpty.Invoke(this);
        }
      }
    }
  }
}