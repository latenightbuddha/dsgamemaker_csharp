#nullable disable
namespace ScintillaNet;

public enum CaretPolicy
{
  Slop = 1,
  Strict = 4,
  Even = 8,
  Jumps = 16, // 0x00000010
}
