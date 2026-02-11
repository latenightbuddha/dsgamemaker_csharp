using System.ComponentModel;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Clipboard : TopLevelHelper
{
  internal Clipboard(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize() => this.ShouldSerializeConvertEndOfLineOnPaste();

  public void Copy() => this.NativeScintilla.Copy();

  public void Copy(string text) => this.NativeScintilla.CopyText(text.Length, text);

  public void Copy(Range rangeToCopy) => this.Copy(rangeToCopy.Start, rangeToCopy.End);

  public void Copy(int positionStart, int positionEnd)
  {
    this.NativeScintilla.CopyRange(positionStart, positionEnd);
  }

  public void Cut() => this.NativeScintilla.Cut();

  public void Paste() => this.NativeScintilla.Paste();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool CanPaste => this.NativeScintilla.CanPaste();

  public bool ConvertEndOfLineOnPaste
  {
    get => this.NativeScintilla.GetPasteConvertEndings();
    set => this.NativeScintilla.SetPasteConvertEndings(value);
  }

  private bool ShouldSerializeConvertEndOfLineOnPaste() => !this.ConvertEndOfLineOnPaste;

  private void ResetConvertEndOfLineOnPaste() => this.ConvertEndOfLineOnPaste = true;
}
