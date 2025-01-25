using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GGJ2025.Operations;

public static class OperationExtensions
{
  public static void Execute(this Operation operation)
  {
    OperationExecutor.Instance.StartCoroutine(operation);
  }

  public static void Execute(this IEnumerator enumerator)
  {
    new EnumeratorOperation(enumerator).Execute();
  }

  public static AddressablesLoadOperation<T> ToOperation<T>(this AsyncOperationHandle<T> handle) => new(handle);
}