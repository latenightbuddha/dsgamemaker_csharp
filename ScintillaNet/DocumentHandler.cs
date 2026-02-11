#nullable disable
namespace ScintillaNet;

public class DocumentHandler : TopLevelHelper
{
  internal DocumentHandler(Scintilla scintilla)
    : base(scintilla)
  {
  }

  public Document Current
  {
    get => new Document(this.Scintilla, this.NativeScintilla.GetDocPointer());
    set => this.NativeScintilla.SetDocPointer(value.Handle);
  }

  public Document Create() => new Document(this.Scintilla, this.NativeScintilla.CreateDocument());
}
