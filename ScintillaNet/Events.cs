#nullable disable
namespace ScintillaNet;

public enum Events : uint
{
  StyleNeeded = 2000, // 0x000007D0
  CharAdded = 2001, // 0x000007D1
  SavePointReached = 2002, // 0x000007D2
  SavePointLeft = 2003, // 0x000007D3
  ModifyAttemptRO = 2004, // 0x000007D4
  SCKey = 2005, // 0x000007D5
  SCDoubleClick = 2006, // 0x000007D6
  UpdateUI = 2007, // 0x000007D7
  Modified = 2008, // 0x000007D8
  MacroRecord = 2009, // 0x000007D9
  MarginClick = 2010, // 0x000007DA
  NeedShown = 2011, // 0x000007DB
  Painted = 2013, // 0x000007DD
  UserListSelection = 2014, // 0x000007DE
  UriDropped = 2015, // 0x000007DF
  DwellStart = 2016, // 0x000007E0
  DwellEnd = 2017, // 0x000007E1
  SCZoom = 2018, // 0x000007E2
  HotspotClick = 2019, // 0x000007E3
  HotspotDoubleClick = 2020, // 0x000007E4
  CallTipClick = 2021, // 0x000007E5
  AutoCSelection = 2022, // 0x000007E6
}
