using System.Collections;

namespace GGJ2025.Utilities;

public static class Extensions
{
  public static IEnumerator WaitForInitialized(this IAsyncInitialized obj)
  {
    while (!obj.Initialized)
    {
      yield return null;
    }
  }
}