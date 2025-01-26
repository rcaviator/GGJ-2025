using UnityEngine;

namespace GGJ2025.Operations;

public class OperationExecutor : MonoBehaviour
{
  private static OperationExecutor? instance;

  public static OperationExecutor Instance
  {
    get
    {
      if (instance is null)
      {
        var obj = new GameObject(nameof(OperationExecutor));
        DontDestroyOnLoad(obj);
        instance = obj.AddComponent<OperationExecutor>();
      }

      return instance;
    }
  }
}