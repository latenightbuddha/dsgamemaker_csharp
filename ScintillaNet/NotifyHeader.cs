using System;

#nullable disable
namespace ScintillaNet;

public struct NotifyHeader
{
  public IntPtr hwndFrom;
  public IntPtr idFrom;
  public uint code;
}
