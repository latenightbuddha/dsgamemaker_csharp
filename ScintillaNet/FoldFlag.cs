using System;

#nullable disable
namespace ScintillaNet;

[Flags]
public enum FoldFlag
{
  LineBeforeExpanded = 2,
  LineBeforeContracted = 4,
  LineAfterExpanded = 8,
  LineAfterContracted = 16, // 0x00000010
  LevelNumbers = 64, // 0x00000040
  Box = 1,
}
