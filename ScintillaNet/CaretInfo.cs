using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class CaretInfo : TopLevelHelper
{
  protected internal CaretInfo(Scintilla scintilla)
    : base(scintilla)
  {
    this.BlinkRate = SystemInformation.CaretBlinkTime;
    this.Width = SystemInformation.CaretWidth;
  }

  protected internal bool ShouldSerialize()
  {
    return this.ShouldSerializeBlinkRate() || this.ShouldSerializeColor() || this.ShouldSerializeCurrentLineBackgroundColor() || this.ShouldSerializeWidth() || this.ShouldSerializeHighlightCurrentLine() || this.ShouldSerializeCurrentLineBackgroundAlpha() || this.ShouldSerializeStyle() || this.ShouldSerializeIsSticky();
  }

  public void EnsureVisible() => this.NativeScintilla.ScrollCaret();

  public override string ToString() => !this.ShouldSerialize() ? string.Empty : base.ToString();

  public void ChooseCaretX() => this.NativeScintilla.ChooseCaretX();

  public int Width
  {
    get => this.NativeScintilla.GetCaretWidth();
    set => this.NativeScintilla.SetCaretWidth(value);
  }

  private bool ShouldSerializeWidth() => this.Width != SystemInformation.CaretWidth;

  private void ResetWidth() => this.Width = SystemInformation.CaretWidth;

  public CaretStyle Style
  {
    get => (CaretStyle) this.NativeScintilla.GetCaretStyle();
    set => this.NativeScintilla.SetCaretStyle((int) value);
  }

  private bool ShouldSerializeStyle() => this.Style != CaretStyle.Line;

  private void ResetStyle() => this.Style = CaretStyle.Line;

  public Color Color
  {
    get
    {
      if (this.Scintilla.ColorBag.ContainsKey("Caret.Color"))
        return this.Scintilla.ColorBag["Caret.Color"];
      Color color = Utilities.RgbToColor(this.NativeScintilla.GetCaretFore());
      return color == Color.FromArgb(0, 0, 0) ? Color.Black : color;
    }
    set
    {
      if (value == this.Color)
        return;
      if (value.IsKnownColor)
      {
        if (this.Color == Color.Black)
          this.Scintilla.ColorBag.Remove("Caret.Color");
        else
          this.Scintilla.ColorBag["Caret.Color"] = value;
      }
      this.NativeScintilla.SetCaretFore(Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeColor() => this.Color != Color.Black;

  private void ResetColor() => this.Color = Color.Black;

  public Color CurrentLineBackgroundColor
  {
    get
    {
      if (this.Scintilla.ColorBag.ContainsKey("Caret.CurrentLineBackgroundColor"))
        return this.Scintilla.ColorBag["Caret.CurrentLineBackgroundColor"];
      Color color = Utilities.RgbToColor(this.NativeScintilla.GetCaretLineBack());
      return color.ToArgb() == Color.Yellow.ToArgb() ? Color.Yellow : color;
    }
    set
    {
      if (value == this.Color)
        return;
      if (value.IsKnownColor)
      {
        if (this.Color == Color.Yellow)
          this.Scintilla.ColorBag.Remove("Caret.CurrentLineBackgroundColor");
        else
          this.Scintilla.ColorBag["Caret.CurrentLineBackgroundColor"] = value;
      }
      this.NativeScintilla.SetCaretLineBack(Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeCurrentLineBackgroundColor()
  {
    return this.CurrentLineBackgroundColor != Color.Yellow;
  }

  private void ResetCurrentLineBackgroundColor() => this.CurrentLineBackgroundColor = Color.Yellow;

  public bool HighlightCurrentLine
  {
    get => this.NativeScintilla.GetCaretLineVisible();
    set => this.NativeScintilla.SetCaretLineVisible(value);
  }

  private bool ShouldSerializeHighlightCurrentLine() => this.HighlightCurrentLine;

  private void ResetHighlightCurrentLine() => this.HighlightCurrentLine = false;

  public int CurrentLineBackgroundAlpha
  {
    get => this.NativeScintilla.GetCaretLineBackAlpha();
    set => this.NativeScintilla.SetCaretLineBackAlpha(value);
  }

  private bool ShouldSerializeCurrentLineBackgroundAlpha()
  {
    return this.CurrentLineBackgroundAlpha != 256 /*0x0100*/;
  }

  private void ResetCurrentLineBackgroundAlpha()
  {
    this.CurrentLineBackgroundAlpha = 256 /*0x0100*/;
  }

  public int BlinkRate
  {
    get => this.NativeScintilla.GetCaretPeriod();
    set => this.NativeScintilla.SetCaretPeriod(value);
  }

  private bool ShouldSerializeBlinkRate() => this.BlinkRate != SystemInformation.CaretBlinkTime;

  private void ResetBlinkRate() => this.BlinkRate = SystemInformation.CaretBlinkTime;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int Position
  {
    get => this.NativeScintilla.GetCurrentPos();
    set => this.NativeScintilla.SetCurrentPos(value);
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int Anchor
  {
    get => this.NativeScintilla.GetAnchor();
    set => this.NativeScintilla.SetAnchor(value);
  }

  public bool IsSticky
  {
    get => this.NativeScintilla.GetCaretSticky();
    set => this.NativeScintilla.SetCaretSticky(value);
  }

  private bool ShouldSerializeIsSticky() => this.IsSticky;

  private void ResetIsSticky() => this.IsSticky = false;

  public void Goto(int position) => this.NativeScintilla.GotoPos(position);

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int LineNumber
  {
    get => this.NativeScintilla.LineFromPosition(this.NativeScintilla.GetCurrentPos());
    set => this.NativeScintilla.GotoLine(value);
  }

  public void BringIntoView() => this.NativeScintilla.MoveCaretInsideView();
}
