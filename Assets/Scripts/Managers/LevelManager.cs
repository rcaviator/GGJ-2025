using System;
using System.Collections;
using GGJ2025.Operations;
using GGJ2025.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GGJ2025.Managers
{
  public class LevelManager : MonoBehaviour
  {
    [SerializeField] private GameObject loadingCurtain = null!;
    [SerializeField] private AssetReference[] levels = Array.Empty<AssetReference>();

    private IEnumerator Start()
    {
      loadingCurtain.SetActive(true);

      // For now, just load the first level
      var load = Addressables.InstantiateAsync(levels[0], transform).ToOperation();
      yield return load;
      if (load.Result != null)
      {
        yield return load.Result.GetComponentsInChildren<IAsyncInitialized>().WaitForInitialized();
      }

      loadingCurtain.SetActive(false);
    }
  }
}