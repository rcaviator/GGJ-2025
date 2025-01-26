using System.Collections;
using System.Diagnostics.CodeAnalysis;
using GGJ2025.Operations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GGJ2025.Utilities
{
  public class PrefabLoader : MonoBehaviour, IAsyncInitialized
  {
    [SerializeField] private AssetReference prefabReference = null!;

    public GameObject? Prefab { get; private set; }

    [MemberNotNullWhen(true, "Prefab")] public bool Initialized => Prefab is not null;

    private IEnumerator Start()
    {
      var load = Addressables.LoadAssetAsync<GameObject>(prefabReference).ToOperation();
      yield return load;
      Prefab = load.Result;
    }
  }
}