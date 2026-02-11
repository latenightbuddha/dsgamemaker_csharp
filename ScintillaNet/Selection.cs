using System;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Selection : TopLevelHelper
{
  private Color _backColorUnfocused = Color.LightGray;
  private bool _hidden;
  private bool _hideSelection;

  protected internal Selection(Scintilla scintilla)
    : base(scintilla)
  {
    this.NativeScintilla.SetSelBack(this.BackColor != Color.Transparent, Utilities.ColorToRgb(this.BackColor));
    this.NativeScintilla.SetSelFore(this.ForeColor != Color.Transparent, Utilities.ColorToRgb(this.ForeColor));
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeBackColor() | this.ShouldSerializeBackColorUnfocused() | this.ShouldSerializeForeColor() | this.ShouldSerializeForeColorUnfocused() | this.ShouldSerializeHidden() | this.ShouldSerializeHideSelection() | this.ShouldSerializeMode();
  }

  [Browsable(false)]
  public bool IsRectangle => this.NativeScintilla.SelectionIsRectangle();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Range Range
  {
    get
    {
      return new Range(this.NativeScintilla.GetSelectionStart(), this.NativeScintilla.GetSelectionEnd(), this.Scintilla);
    }
    set => this.NativeScintilla.SetSel(value.Start, value.End);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int Length
  {
    get => Math.Abs(this.End - this.Start);
    set => this.End = this.Start + value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public string Text
  {
    get
    {
      string text;
      this.NativeScintilla.GetSelText(out text);
      return text;
    }
    set
    {
      if (string.IsNullOrEmpty(value))
        this.Clear();
      else
        this.NativeScintilla.ReplaceSel(value);
    }
  }

  public Color ForeColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("Selection.ForeColor") ? this.Scintilla.ColorBag["Selection.ForeColor"] : SystemColors.HighlightText;
    }
    set
    {
      if (value == SystemColors.HighlightText)
        this.Scintilla.ColorBag.Remove("Selection.ForeColor");
      else
        this.Scintilla.ColorBag["Selection.ForeColor"] = value;
      if (!this.Scintilla.ContainsFocus)
        return;
      this.NativeScintilla.SetSelFore(value != Color.Transparent, Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeForeColor() => this.ForeColor != SystemColors.HighlightText;

  private void ResetForeColor() => this.ForeColor = SystemColors.HighlightText;

  public Color ForeColorUnfocused
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("Selection.ForeColorUnfocused") ? this.Scintilla.ColorBag["Selection.ForeColorUnfocused"] : SystemColors.HighlightText;
    }
    set
    {
      if (value == this.ForeColorUnfocused)
        return;
      if (value == SystemColors.HighlightText)
        this.Scintilla.ColorBag.Remove("Selection.ForeColorUnfocused");
      else
        this.Scintilla.ColorBag["Selection.ForeColorUnfocused"] = value;
      if (this.Scintilla.ContainsFocus)
        return;
      this.NativeScintilla.SetSelFore(value != Color.Transparent, Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeForeColorUnfocused()
  {
    return this.ForeColorUnfocused != SystemColors.HighlightText;
  }

  private void ResetForeColorUnfocused() => this.ForeColorUnfocused = SystemColors.HighlightText;

  public Color BackColorUnfocused
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("Selection.BackColorUnfocused") ? this.Scintilla.ColorBag["Selection.BackColorUnfocused"] : Color.LightGray;
    }
    set
    {
      if (value == this.BackColorUnfocused)
        return;
      if (value == Color.LightGray)
        this.Scintilla.ColorBag.Remove("Selection.BackColorUnfocused");
      else
        this.Scintilla.ColorBag["Selection.BackColorUnfocused"] = value;
      if (this.Scintilla.ContainsFocus)
        return;
      this.NativeScintilla.SetSelBack(value != Color.Transparent, Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeBackColorUnfocused() => this.BackColorUnfocused != Color.LightGray;

  private void ResetBackColorUnfocused() => this.BackColorUnfocused = Color.LightGray;

  public Color BackColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("Selection.BackColor") ? this.Scintilla.ColorBag["Selection.BackColor"] : SystemColors.Highlight;
    }
    set
    {
      if (value == this.BackColor)
        return;
      if (value == SystemColors.Highlight)
        this.Scintilla.ColorBag.Remove("Selection.BackColor");
      else
        this.Scintilla.ColorBag["Selection.BackColor"] = value;
      if (!this.Scintilla.ContainsFocus)
        return;
      this.NativeScintilla.SetSelBack(value != Color.Transparent, Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeBackColor() => this.BackColor != SystemColors.Highlight;

  private void ResetBackColor() => this.BackColor = SystemColors.Highlight;

  public bool Hidden
  {
    get => this._hidden;
    set
    {
      this._hidden = value;
      this.NativeScintilla.HideSelection(value);
    }
  }

  private bool ShouldSerializeHidden() => this._hidden;

  private void ResetHidden() => this.Hidden = false;

  public bool HideSelection
  {
    get => this._hideSelection;
    set
    {
      this._hideSelection = value;
      if (this.Scintilla.ContainsFocus)
        return;
      this.NativeScintilla.HideSelection(value);
    }
  }

  private bool ShouldSerializeHideSelection() => this._hideSelection;

  private void ResetHideSelection() => this._hideSelection = false;

  public SelectionMode Mode
  {
    get => (SelectionMode) this.NativeScintilla.GetSelectionMode();
    set => this.NativeScintilla.SetSelectionMode((int) value);
  }

  private bool ShouldSerializeMode() => this.Mode != SelectionMode.Stream;

  private void ResetMode() => this.Mode = SelectionMode.Stream;

  public int Start
  {
    get => this.NativeScintilla.GetSelectionStart();
    set => this.NativeScintilla.SetSelectionStart(value);
  }

  private bool ShouldSerializeStart() => this.Start != 0;

  private void ResetStart() => this.Start = 0;

  public int End
  {
    get => this.NativeScintilla.GetSelectionEnd();
    set => this.NativeScintilla.SetSelectionEnd(value);
  }

  private bool ShouldSerializeEnd() => this.End != 0;

  private void ResetEnd() => this.End = 0;

  public void SelectAll() => this.NativeScintilla.SelectAll();

  public void SelectNone() => this.NativeScintilla.SetSel(-1, -1);

  public void Clear() => this.NativeScintilla.Clear();
}
