using System;

#nullable disable
namespace ScintillaNet;

[Flags]
public enum WrapVisualLocation
{
  Default = 0,
  EndByText = 1,
  StartByText = 2,
}
