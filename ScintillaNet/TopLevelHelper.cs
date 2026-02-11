#nullable disable
namespace ScintillaNet;

public class TopLevelHelper : ScintillaHelperBase
{
  internal TopLevelHelper(Scintilla scintilla)
    : base(scintilla)
  {
  }

  public override bool Equals(object obj) => this.IsSameHelperFamily(obj);

  public override int GetHashCode() => base.GetHashCode();
}
