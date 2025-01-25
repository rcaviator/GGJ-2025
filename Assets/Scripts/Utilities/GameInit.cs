using System.Collections;
using GGJ2025.Operations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GGJ2025.Utilities
{
  public class GameInit : MonoBehaviour
  {
    [SerializeField] private AssetReference menusScene = null!;

    private IEnumerator Start()
    {
      yield return Addressables.LoadSceneAsync(menusScene).ToOperation();
    }
  }
}