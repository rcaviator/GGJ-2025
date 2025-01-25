using System;
using System.Collections;
using UnityEngine;

namespace GGJ2025.Operations;

public abstract class Operation : IEnumerator
{
  private IEnumerator? enumerator;

  public bool MoveNext()
  {
    if (Completed)
    {
      throw new InvalidOperationException($"Tried to call MoveNext on {GetType().Name} that is already completed");
    }

    enumerator ??= Execute();
    try
    {
      if (!enumerator.MoveNext())
      {
        Current = null;
        Completed = true;
      }
      else
      {
        Current = enumerator.Current;
      }
    }
    catch (Exception e)
    {
      Exception = e;
      Debug.LogException(e);
      Completed = true;
    }

    return !Completed;
  }

  void IEnumerator.Reset()
  {
  }

  public object? Current { get; private set; }

  public bool Completed { get; private set; }

  public Exception Exception { get; protected set; }

  protected abstract IEnumerator Execute();
}