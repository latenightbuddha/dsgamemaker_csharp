using System;

#nullable disable
namespace ScintillaNet;

public class LinesNeedShownEventArgs : EventArgs
{
  private int _firstLine;
  private int _lastLine;

  public int FirstLine => this._firstLine;

  public int LastLine
  {
    get => this._lastLine;
    set => this._lastLine = value;
  }

  public LinesNeedShownEventArgs(int startLine, int endLine)
  {
    this._firstLine = startLine;
    this._lastLine = endLine;
  }
}
