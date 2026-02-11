using System;

#nullable disable
namespace ScintillaNet;

[Flags]
public enum SearchFlags
{
  Empty = 0,
  WholeWord = 2,
  MatchCase = 4,
  WordStart = 1048576, // 0x00100000
  RegExp = 2097152, // 0x00200000
  Posix = 4194304, // 0x00400000
}
