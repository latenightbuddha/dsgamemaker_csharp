#nullable disable
namespace ScintillaNet.Design;

public class FlagCheckedListBoxItem
{
  public int value;
  public string caption;

  public FlagCheckedListBoxItem(int v, string c)
  {
    this.value = v;
    this.caption = c;
  }

  public override string ToString() => this.caption;

  public bool IsFlag => (this.value & this.value - 1) == 0;

  public bool IsMemberFlag(FlagCheckedListBoxItem composite)
  {
    return this.IsFlag && (this.value & composite.value) == this.value;
  }
}
