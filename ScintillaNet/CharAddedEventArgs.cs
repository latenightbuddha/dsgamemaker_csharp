using System;

#nullable disable
namespace ScintillaNet;

public class CharAddedEventArgs : EventArgs
{
  private char _ch;

  public char Ch => this._ch;

  public CharAddedEventArgs(char ch) => this._ch = ch;
}
