#nullable disable
namespace ScintillaNet;

public enum CharacterSet
{
  Ansi = 0,
  Default = 1,
  Symbol = 2,
  Mac = 77, // 0x0000004D
  ShiftJis = 128, // 0x00000080
  Hangul = 129, // 0x00000081
  Johab = 130, // 0x00000082
  Gb2312 = 134, // 0x00000086
  Chinesebig5 = 136, // 0x00000088
  Greek = 161, // 0x000000A1
  Turkish = 162, // 0x000000A2
  Vietnamese = 163, // 0x000000A3
  Hebrew = 177, // 0x000000B1
  Arabic = 178, // 0x000000B2
  Baltic = 186, // 0x000000BA
  Russian = 204, // 0x000000CC
  Thai = 222, // 0x000000DE
  EastEurope = 238, // 0x000000EE
  Oem = 255, // 0x000000FF
  CharSet885915 = 1000, // 0x000003E8
  Cyrillic = 1251, // 0x000004E3
}
