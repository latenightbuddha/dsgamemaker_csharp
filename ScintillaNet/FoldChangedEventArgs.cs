#nullable disable
namespace ScintillaNet;

public class FoldChangedEventArgs : ModifiedEventArgs
{
  private int _line;
  private int _newFoldLevel;
  private int _previousFoldLevel;

  public int Line
  {
    get => this._line;
    set => this._line = value;
  }

  public int NewFoldLevel => this._newFoldLevel;

  public int PreviousFoldLevel => this._previousFoldLevel;

  public FoldChangedEventArgs(
    int line,
    int newFoldLevel,
    int previousFoldLevel,
    int modificationType)
    : base(modificationType)
  {
    this._line = line;
    this._newFoldLevel = newFoldLevel;
    this._previousFoldLevel = previousFoldLevel;
  }
}
