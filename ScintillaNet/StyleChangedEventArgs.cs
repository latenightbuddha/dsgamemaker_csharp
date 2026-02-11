#nullable disable
namespace ScintillaNet;

public class StyleChangedEventArgs : ModifiedEventArgs
{
  private int _position;
  private int _length;

  public int Position => this._position;

  public int Length => this._length;

  internal StyleChangedEventArgs(int position, int length, int modificationType)
    : base(modificationType)
  {
    this._position = position;
    this._length = length;
  }
}
