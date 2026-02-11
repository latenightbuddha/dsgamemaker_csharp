using System;

#nullable disable
namespace ScintillaNet;

public struct RangeToFormat
{
  public IntPtr hdc;
  public IntPtr hdcTarget;
  public PrintRectangle rc;
  public PrintRectangle rcPage;
  public CharacterRange chrg;
}
