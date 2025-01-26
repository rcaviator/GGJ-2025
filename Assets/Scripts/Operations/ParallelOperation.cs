using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GGJ2025.Operations;

public class ParallelOperation : Operation
{
  private readonly Operation[] operations;

  public ParallelOperation(IEnumerable<Operation> operations)
  {
    this.operations = operations.ToArray();
  }

  protected override IEnumerator Execute()
  {
    while (true)
    {
      var done = true;
      foreach (var operation in operations)
      {
        // TODO: not sure this handles nested IEnumerators correctly.
        if (!operation.Completed && operation.MoveNext())
        {
          done = false;
        }
      }

      if (done)
      {
        break;
      }

      yield return null;
    }
  }
}