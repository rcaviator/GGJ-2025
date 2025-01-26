using System.Collections;
using GGJ2025.Managers;
using UnityEngine;

namespace GGJ2025.Utilities
{
  public class GameInit : MonoBehaviour
  {
    [SerializeField] private SceneLoader menusSceneLoader = null!;

    private IEnumerator Start()
    {
      yield return AudioManager.Instance.WaitForInitialized();
      yield return menusSceneLoader.LoadOperation();
    }
  }
}