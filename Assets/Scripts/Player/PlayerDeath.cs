using System.Collections;
using GGJ2025.Operations;
using GGJ2025.Utilities;
using UnityEngine;

namespace GGJ2025.Player
{
  public class PlayerDeath : MonoBehaviour
  {
    [SerializeField] private SceneLoader sceneLoader = null!;

    public void OnDeath()
    {
      RunDeath().Execute();
    }

    private IEnumerator RunDeath()
    {
      sceneLoader.Load();
      yield break;
    }
  }
}