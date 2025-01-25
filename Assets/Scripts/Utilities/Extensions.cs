using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

  public static IEnumerator WaitForInitialized(this ICollection<IAsyncInitialized> objs)
  {
    while (objs.Any(o => !o.Initialized))
    {
      yield return null;
    }
  }
}