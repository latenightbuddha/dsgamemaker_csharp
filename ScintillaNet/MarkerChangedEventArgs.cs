#nullable disable
namespace ScintillaNet;

public class MarkerChangedEventArgs : ModifiedEventArgs
{
  private int _line;

  public int Line
  {
    get => this._line;
    set => this._line = value;
  }

  public MarkerChangedEventArgs(int line, int modificationType)
    : base(modificationType)
  {
    this._line = line;
  }
}
