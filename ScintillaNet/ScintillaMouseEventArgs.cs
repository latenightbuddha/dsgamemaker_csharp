using System;

#nullable disable
namespace ScintillaNet;

public class ScintillaMouseEventArgs : EventArgs
{
  private int _x;
  private int _y;
  private int _position;

  public int X
  {
    get => this._x;
    set => this._x = value;
  }

  public int Y
  {
    get => this._y;
    set => this._y = value;
  }

  public int Position
  {
    get => this._position;
    set => this._position = value;
  }

  public ScintillaMouseEventArgs(int x, int y, int position)
  {
    this._x = x;
    this._y = y;
    this._position = position;
  }
}
