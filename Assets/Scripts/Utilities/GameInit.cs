using System.Collections;
using UnityEngine;

namespace GGJ2025.Utilities
{
  public class GameInit : MonoBehaviour
  {
    [SerializeField] private SceneLoader menusSceneLoader = null!;

    private IEnumerator Start()
    {
      yield return menusSceneLoader.LoadOperation();
    }
  }
}