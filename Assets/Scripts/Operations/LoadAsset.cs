using System.Collections;
using UnityEngine.AddressableAssets;

namespace GGJ2025.Operations;

public class LoadAsset<T> : Operation
{
  private readonly string asset;

  public T? Result { get; private set; }

  public LoadAsset(string asset)
  {
    this.asset = asset;
  }

  protected override IEnumerator Execute()
  {
    var operation = Addressables.LoadAssetAsync<T>(asset);
    while (!operation.IsDone)
    {
      yield return null;
    }

    if (operation.OperationException != null)
    {
      Exception = operation.OperationException;
    }
    else
    {
      Result = operation.Result;
    }
  }
}