#nullable disable
namespace ScintillaNet;

public enum FoldLevel
{
  Base = 1024, // 0x00000400
  NumberMask = 4095, // 0x00000FFF
  WhiteFlag = 4096, // 0x00001000
  HeaderFlag = 8192, // 0x00002000
  BoxHeaderFlag = 16384, // 0x00004000
  BoxFooterFlag = 32768, // 0x00008000
  Contracted = 65536, // 0x00010000
  Unindent = 131072, // 0x00020000
}
