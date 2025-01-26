using GGJ2025.Utilities;
using UnityEngine;

namespace GGJ2025.Soap
{
  public class Trash : MonoBehaviour
  {
    public void OnHealthEmpty(Health health)
    {
      gameObject.AddComponent<SoapedObject>();

      // Reset health in case of unsoap
      health.Current = health.Max;
    }
  }
}