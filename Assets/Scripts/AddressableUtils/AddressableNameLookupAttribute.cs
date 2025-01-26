using System;
using UnityEngine;

namespace GGJ2025.AddressableUtils;

public class AddressableNameLookupAttribute : PropertyAttribute
{
  public AddressableNameLookupAttribute(Type? type = null)
  {
    Type = type;
  }

  public Type? Type { get; }
}