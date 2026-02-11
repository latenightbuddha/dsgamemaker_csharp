using System;

#nullable disable
namespace ScintillaNet;

public class Document : ScintillaHelperBase
{
  private IntPtr _handle;

  public IntPtr Handle
  {
    get => this._handle;
    set => this._handle = value;
  }

  internal Document(Scintilla scintilla, IntPtr handle)
    : base(scintilla)
  {
    this._handle = handle;
  }

  public void AddRef() => this.NativeScintilla.AddRefDocument(this._handle);

  public void Release() => this.NativeScintilla.ReleaseDocument(this._handle);

  public override bool Equals(object obj)
  {
    Document document = obj as Document;
    return !(this._handle == IntPtr.Zero) && this._handle.Equals((object) document._handle);
  }

  public override int GetHashCode() => this._handle.GetHashCode();
}
