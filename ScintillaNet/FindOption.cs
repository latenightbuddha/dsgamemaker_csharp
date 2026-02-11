#nullable disable
namespace ScintillaNet;

public enum FindOption
{
  WholeWord = 2,
  MatchCase = 4,
  WordStart = 1048576, // 0x00100000
  RegularExpression = 2097152, // 0x00200000
  Posix = 4194304, // 0x00400000
}
