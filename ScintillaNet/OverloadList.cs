using System.Collections.Generic;

#nullable disable
namespace ScintillaNet;

public class OverloadList : List<string>
{
  private int _currentIndex;

  public int CurrentIndex
  {
    get => this._currentIndex;
    internal set => this._currentIndex = value;
  }

  public string Current
  {
    get => this[this._currentIndex];
    set => this._currentIndex = this.IndexOf(value);
  }

  public OverloadList()
  {
  }

  public OverloadList(IEnumerable<string> collection)
    : base(collection)
  {
  }

  public OverloadList(int capacity)
    : base(capacity)
  {
  }
}
