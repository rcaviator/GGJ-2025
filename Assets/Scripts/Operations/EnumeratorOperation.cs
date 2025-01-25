using System.Collections;

namespace GGJ2025.Operations;

public class EnumeratorOperation : Operation
{
  private readonly IEnumerator enumerator;

  public EnumeratorOperation(IEnumerator enumerator)
  {
    this.enumerator = enumerator;
  }

  protected override IEnumerator Execute() => enumerator;
}