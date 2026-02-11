using System;

#nullable disable
namespace ScintillaNet;

public struct TextToFind
{
  public CharacterRange chrg;
  public IntPtr lpstrText;
  public CharacterRange chrgText;
}
