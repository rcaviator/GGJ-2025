using System.Collections;
using GGJ2025.Operations;
using UnityEngine;

namespace GGJ2025.Utilities;

public class PrefabLoader : MonoBehaviour
{
  [SerializeField] private string prefabName = string.Empty;

  public GameObject? Prefab { get; private set; }

  private IEnumerator Start()
  {
    var load = new LoadAsset<GameObject>(prefabName);
    yield return load;
    Prefab = load.Result;
  }
}