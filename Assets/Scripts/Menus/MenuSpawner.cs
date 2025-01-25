using System.Collections;
using GGJ2025.Utilities;
using UnityEngine;

namespace GGJ2025.Menus
{
  public class MenuSpawner : MonoBehaviour
  {
    [SerializeField] private PrefabLoader mainMenuLoader = null!;

    private IEnumerator Start()
    {
      yield return mainMenuLoader.WaitForInitialized();
      Instantiate(mainMenuLoader.Prefab, transform, false);
    }
  }
}