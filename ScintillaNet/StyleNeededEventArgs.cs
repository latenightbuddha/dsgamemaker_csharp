using System;

#nullable disable
namespace ScintillaNet;

public class StyleNeededEventArgs : EventArgs
{
  private Range _range;

  public Range Range => this._range;

  public StyleNeededEventArgs(Range range) => this._range = range;
}
