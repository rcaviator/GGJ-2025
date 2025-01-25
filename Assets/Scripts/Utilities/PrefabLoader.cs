using System.Collections;
using System.Diagnostics.CodeAnalysis;
using GGJ2025.AddressableUtils;
using GGJ2025.Operations;
using UnityEngine;

namespace GGJ2025.Utilities
{
  public class PrefabLoader : MonoBehaviour, IAsyncInitialized
  {
    [SerializeField, AddressableNameLookup]
    private string prefabName = string.Empty;

    public GameObject? Prefab { get; private set; }

    [MemberNotNullWhen(true)]
    public bool Initialized => Prefab is not null;

    private IEnumerator Start()
    {
      var load = new LoadAsset<GameObject>(prefabName);
      yield return load;
      Prefab = load.Result;
    }
  }
}