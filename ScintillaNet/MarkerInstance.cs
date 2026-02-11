#nullable disable
namespace ScintillaNet;

public class MarkerInstance : ScintillaHelperBase
{
  private int _handle;
  private Marker _marker;

  internal MarkerInstance(Scintilla scintilla, Marker marker, int handle)
    : base(scintilla)
  {
    this._marker = marker;
    this._handle = handle;
  }

  public int Handle => this._handle;

  public Marker Marker => this._marker;

  public Line Line
  {
    get
    {
      int number = this.NativeScintilla.MarkerLineFromHandle(this._handle);
      return number < 0 ? (Line) null : new Line(this.Scintilla, number);
    }
  }

  public void Delete() => this.NativeScintilla.MarkerDeleteHandle(this._handle);

  public override bool Equals(object obj)
  {
    return this.IsSameHelperFamily(obj) && ((MarkerInstance) obj).Handle == this.Handle;
  }

  public override int GetHashCode() => base.GetHashCode();
}
