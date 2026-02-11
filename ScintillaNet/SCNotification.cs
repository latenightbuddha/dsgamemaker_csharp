using System;

#nullable disable
namespace ScintillaNet;

public struct SCNotification
{
  public NotifyHeader nmhdr;
  public int position;
  public char ch;
  public int modifiers;
  public int modificationType;
  public IntPtr text;
  public int length;
  public int linesAdded;
  public int message;
  public IntPtr wParam;
  public IntPtr lParam;
  public int line;
  public int foldLevelNow;
  public int foldLevelPrev;
  public int margin;
  public int listType;
  public int x;
  public int y;
}
