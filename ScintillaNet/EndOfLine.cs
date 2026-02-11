using System.ComponentModel;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class EndOfLine : TopLevelHelper
{
  internal EndOfLine(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeIsVisible() || this.ShouldSerializeMode() || this.ShouldSerializeConvertOnPaste();
  }

  public bool ConvertOnPaste
  {
    get => this.NativeScintilla.GetPasteConvertEndings();
    set => this.NativeScintilla.SetPasteConvertEndings(value);
  }

  private bool ShouldSerializeConvertOnPaste() => !this.ConvertOnPaste;

  private void ResetConvertOnPaste() => this.ConvertOnPaste = true;

  public EndOfLineMode Mode
  {
    get => (EndOfLineMode) this.NativeScintilla.GetEolMode();
    set => this.NativeScintilla.SetEolMode((int) value);
  }

  private bool ShouldSerializeMode() => this.Mode != EndOfLineMode.Crlf;

  private void ResetMode() => this.Mode = EndOfLineMode.Crlf;

  public bool IsVisible
  {
    get => this.NativeScintilla.GetViewEol();
    set => this.NativeScintilla.SetViewEol(value);
  }

  private bool ShouldSerializeIsVisible() => this.IsVisible;

  private void ResetIsVisible() => this.IsVisible = false;

  public string EolString
  {
    get
    {
      switch (this.Mode)
      {
        case EndOfLineMode.Crlf:
          return "\r\n";
        case EndOfLineMode.CR:
          return "\r";
        case EndOfLineMode.LF:
          return "\n";
        default:
          return "";
      }
    }
  }

  public void ConvertAllLines(EndOfLineMode toMode)
  {
    this.NativeScintilla.ConvertEols((int) toMode);
  }
}
