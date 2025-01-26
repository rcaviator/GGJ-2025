using GGJ2025.Operations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GGJ2025.Utilities
{
  public class SceneLoader : MonoBehaviour
  {
    [SerializeField] private AssetReference menusScene = null!;

    public Operation LoadOperation() => Addressables.LoadSceneAsync(menusScene).ToOperation();

    public void Load() => LoadOperation().Execute();
  }
}