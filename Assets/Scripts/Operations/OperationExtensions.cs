using System.Collections;

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
}