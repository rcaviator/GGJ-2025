using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GGJ2025.Operations;

public class AddressablesLoadOperation<T> : Operation
{
  private readonly AsyncOperationHandle<T> handle;

  public AddressablesLoadOperation(AsyncOperationHandle<T> handle)
  {
    this.handle = handle;
  }

  public T? Result { get; private set; }

  protected override IEnumerator Execute()
  {
    while (!handle.IsDone)
    {
      yield return null;
    }

    if (handle.OperationException != null)
    {
      Exception = handle.OperationException;
      Debug.LogException(Exception);
    }
    else
    {
      Result = handle.Result;
    }
  }
}