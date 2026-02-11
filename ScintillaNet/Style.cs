using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Style : ScintillaHelperBase
{
  private int _index;

  internal Style(int index, Scintilla scintilla)
    : base(scintilla)
  {
    this._index = index;
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeBackColor() || this.ShouldSerializeBold() || this.ShouldSerializeCase() || this.ShouldSerializeCharacterSet() || this.ShouldSerializeFontName() || this.ShouldSerializeForeColor() || this.ShouldSerializeIsChangeable() || this.ShouldSerializeIsHotspot() || this.ShouldSerializeIsSelectionEolFilled() || this.ShouldSerializeIsVisible() || this.ShouldSerializeItalic() || this.ShouldSerializeSize() || this.ShouldSerializeUnderline();
  }

  public void Reset()
  {
    this.ResetBackColor();
    this.ResetBold();
    this.ResetCase();
    this.ResetCharacterSet();
    this.ResetFontName();
    this.ResetForeColor();
    this.ResetIsChangeable();
    this.ResetIsHotspot();
    this.ResetIsSelectionEolFilled();
    this.ResetIsVisible();
    this.ResetItalic();
    this.ResetSize();
    this.ResetUnderline();
  }

  public void Apply(int length) => this.Apply(this.NativeScintilla.GetCurrentPos(), length);

  public void Apply(int position, int length)
  {
    this.NativeScintilla.StartStyling(position, (int) byte.MaxValue);
    this.NativeScintilla.SetStyling(length, this._index);
  }

  public int GetTextWidth(string text) => this.NativeScintilla.TextWidth(this._index, text);

  public override string ToString() => nameof (Style) + this._index.ToString();

  public void CopyTo(Style target)
  {
    target.BackColor = this.BackColor;
    target.Bold = this.Bold;
    target.Case = this.Case;
    target.CharacterSet = this.CharacterSet;
    target.FontName = this.FontName;
    target.ForeColor = this.ForeColor;
    target.IsChangeable = this.IsChangeable;
    target.IsHotspot = this.IsHotspot;
    target.IsSelectionEolFilled = this.IsSelectionEolFilled;
    target.IsVisible = this.IsVisible;
    target.Italic = this.Italic;
    target.Size = this.Size;
    target.Underline = this.Underline;
  }

  public Color ForeColor
  {
    get
    {
      if (this.Scintilla.UseForeColor && this._index != 38)
        return this.Scintilla.ForeColor;
      return this.Scintilla.ColorBag.ContainsKey(this.ToString() + ".ForeColor") ? this.Scintilla.ColorBag[this.ToString() + ".ForeColor"] : Utilities.RgbToColor(this.NativeScintilla.StyleGetFore(this._index));
    }
    set
    {
      this.SetForeColorInternal(value);
      if (this._index != 38)
        return;
      this.Scintilla.CallTip.SetForeColorInternal(value);
    }
  }

  internal void SetForeColorInternal(Color value)
  {
    this.Scintilla.ColorBag[this.ToString() + ".ForeColor"] = value;
    this.NativeScintilla.StyleSetFore(this._index, Utilities.ColorToRgb(value));
    if (this._index != 38)
      return;
    this.NativeScintilla.CallTipSetFore(Utilities.ColorToRgb(value));
  }

  private Color getDefaultForeColor()
  {
    if (this._index == 38)
      return SystemColors.InfoText;
    return this.Scintilla.UseForeColor ? this.Scintilla.ForeColor : Color.FromArgb(0, 0, 0);
  }

  private bool ShouldSerializeForeColor() => this.ForeColor != this.getDefaultForeColor();

  private void ResetForeColor() => this.ForeColor = this.getDefaultForeColor();

  internal bool ForeColorNotSet()
  {
    return !this.Scintilla.ColorBag.ContainsKey(this.ToString() + ".ForeColor");
  }

  public Color BackColor
  {
    get
    {
      if (this.Scintilla.UseBackColor && this._index != 38 && this._index != 33)
        return this.Scintilla.BackColor;
      return this.Scintilla.ColorBag.ContainsKey(this.ToString() + ".BackColor") ? this.Scintilla.ColorBag[this.ToString() + ".BackColor"] : Utilities.RgbToColor(this.NativeScintilla.StyleGetBack(this._index));
    }
    set
    {
      this.SetBackColorInternal(value);
      if (this._index != 38)
        return;
      this.Scintilla.CallTip.SetBackColorInternal(value);
    }
  }

  internal void SetBackColorInternal(Color value)
  {
    this.NativeScintilla.StyleSetBack(this._index, Utilities.ColorToRgb(value));
    this.Scintilla.ColorBag[this.ToString() + ".BackColor"] = value;
    if (this._index != 38)
      return;
    this.NativeScintilla.CallTipSetBack(Utilities.ColorToRgb(value));
  }

  private Color getDefaultBackColor()
  {
    if (this._index == 38)
      return SystemColors.Info;
    if (this._index == 33)
      return SystemColors.Control;
    return this.Scintilla.UseBackColor ? this.Scintilla.BackColor : Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
  }

  private bool ShouldSerializeBackColor() => this.BackColor != this.getDefaultBackColor();

  private void ResetBackColor() => this.BackColor = this.getDefaultBackColor();

  internal bool BackColorNotSet()
  {
    return !this.Scintilla.ColorBag.ContainsKey(this.ToString() + ".BackColor");
  }

  public string FontName
  {
    get
    {
      if (this.Scintilla.UseFont && this._index != 38)
        return this.Scintilla.Font.Name;
      if (this.Scintilla.PropertyBag.ContainsKey((object) (this.ToString() + ".FontName")))
        return this.Scintilla.PropertyBag[(object) (this.ToString() + ".FontName")].ToString();
      string fontName;
      this.NativeScintilla.StyleGetFont(this._index, out fontName);
      return fontName;
    }
    set
    {
      this.NativeScintilla.StyleSetFont(this._index, value);
      this.Scintilla.PropertyBag[(object) (this.ToString() + ".FontName")] = (object) value;
      this.Scintilla.PropertyBag[(object) (this.ToString() + ".FontSet")] = (object) true;
    }
  }

  private bool ShouldSerializeFontName() => this.FontName != this.getDefaultFont().Name;

  private void ResetFontName() => this.FontName = this.getDefaultFont().Name;

  public bool Bold
  {
    get
    {
      return this.Scintilla.UseFont && this._index != 38 ? this.Scintilla.Font.Bold : this.NativeScintilla.StyleGetBold(this._index);
    }
    set
    {
      this.NativeScintilla.StyleSetBold(this._index, value);
      this.Scintilla.PropertyBag[(object) (this.ToString() + ".FontSet")] = (object) true;
    }
  }

  private bool ShouldSerializeBold() => this.Bold != this.getDefaultFont().Bold;

  private void ResetBold() => this.Bold = this.getDefaultFont().Bold;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int Index => this._index;

  public bool Italic
  {
    get
    {
      return this.Scintilla.UseFont && this._index != 38 ? this.Scintilla.Font.Italic : this.NativeScintilla.StyleGetItalic(this._index);
    }
    set
    {
      this.NativeScintilla.StyleSetItalic(this._index, value);
      this.Scintilla.PropertyBag[(object) (this.ToString() + ".FontSet")] = (object) true;
    }
  }

  private bool ShouldSerializeItalic() => this.Italic != this.getDefaultFont().Italic;

  private void ResetItalic() => this.Italic = this.getDefaultFont().Italic;

  public bool Underline
  {
    get
    {
      return this.Scintilla.UseFont && this._index != 38 ? this.Scintilla.Font.Underline : this.NativeScintilla.StyleGetUnderline(this._index);
    }
    set
    {
      this.NativeScintilla.StyleSetUnderline(this._index, value);
      this.Scintilla.PropertyBag[(object) (this.ToString() + ".FontSet")] = (object) true;
    }
  }

  private bool ShouldSerializeUnderline() => this.Underline != this.getDefaultFont().Underline;

  private void ResetUnderline() => this.Underline = this.getDefaultFont().Underline;

  public CharacterSet CharacterSet
  {
    get
    {
      return this.Scintilla.UseFont && this._index != 38 ? (CharacterSet) this.Scintilla.Font.GdiCharSet : (CharacterSet) this.NativeScintilla.StyleGetCharacterSet(this._index);
    }
    set
    {
      this.NativeScintilla.StyleSetCharacterSet(this._index, (int) value);
      this.Scintilla.PropertyBag[(object) (this.ToString() + ".FontSet")] = (object) true;
    }
  }

  private CharacterSet getDefaultCharacterSet() => (CharacterSet) this.getDefaultFont().GdiCharSet;

  private bool ShouldSerializeCharacterSet() => this.CharacterSet != this.getDefaultCharacterSet();

  private void ResetCharacterSet() => this.CharacterSet = this.getDefaultCharacterSet();

  public Font Font
  {
    get
    {
      FontStyle style = FontStyle.Regular;
      if (this.Bold)
        style |= FontStyle.Bold;
      if (this.Italic)
        style |= FontStyle.Italic;
      if (this.Underline)
        style |= FontStyle.Underline;
      return new Font(this.FontName, this.Size, style, GraphicsUnit.Point, (byte) this.CharacterSet);
    }
    set
    {
      this.CharacterSet = (CharacterSet) value.GdiCharSet;
      this.FontName = value.Name;
      this.Size = value.SizeInPoints;
      this.Bold = value.Bold;
      this.Italic = value.Italic;
      this.Underline = value.Underline;
    }
  }

  private bool ShouldSerializeFont() => false;

  internal void ResetFont()
  {
    this.Font = this.getDefaultFont();
    this.Scintilla.PropertyBag.Remove((object) (this.ToString() + ".FontSet"));
  }

  internal bool FontNotSet()
  {
    return !this.Scintilla.PropertyBag.ContainsKey((object) (this.ToString() + ".FontSet"));
  }

  private Font getDefaultFont()
  {
    if (this._index == 38)
      return SystemFonts.StatusFont;
    return this.Scintilla.UseFont ? this.Scintilla.Font : new Font(FontFamily.GenericSansSerif, 8f);
  }

  public bool IsSelectionEolFilled
  {
    get => this.NativeScintilla.StyleGetEOLFilled(this._index);
    set => this.NativeScintilla.StyleSetEOLFilled(this._index, value);
  }

  private bool ShouldSerializeIsSelectionEolFilled() => this.IsSelectionEolFilled;

  private void ResetIsSelectionEolFilled() => this.IsSelectionEolFilled = false;

  public StyleCase Case
  {
    get => (StyleCase) this.NativeScintilla.StyleGetCase(this._index);
    set => this.NativeScintilla.StyleSetCase(this._index, (int) value);
  }

  private bool ShouldSerializeCase() => this.Case != StyleCase.Mixed;

  private void ResetCase() => this.Case = StyleCase.Mixed;

  public bool IsVisible
  {
    get => this.NativeScintilla.StyleGetVisible(this._index);
    set => this.NativeScintilla.StyleSetVisible(this._index, value);
  }

  private bool ShouldSerializeIsVisible() => !this.IsVisible;

  private void ResetIsVisible() => this.IsVisible = true;

  public float Size
  {
    get
    {
      if (this.Scintilla.UseFont && this._index != 38)
        return this.Scintilla.Font.SizeInPoints;
      return !this.Scintilla.PropertyBag.ContainsKey((object) (this.ToString() + ".Size")) ? (float) this.NativeScintilla.StyleGetSize(this._index) : (float) this.Scintilla.PropertyBag[(object) (this.ToString() + ".Size")];
    }
    set
    {
      this.NativeScintilla.StyleSetSize(this._index, (int) value);
      this.Scintilla.PropertyBag[(object) (this.ToString() + ".Size")] = (object) value;
      this.Scintilla.PropertyBag[(object) (this.ToString() + ".FontSet")] = (object) true;
    }
  }

  private bool ShouldSerializeSize()
  {
    return (double) this.Size != (double) this.getDefaultFont().SizeInPoints;
  }

  private void ResetSize() => this.Size = this.getDefaultFont().SizeInPoints;

  public bool IsHotspot
  {
    get => this.NativeScintilla.StyleGetHotSpot(this._index);
    set => this.NativeScintilla.StyleSetHotSpot(this._index, value);
  }

  private bool ShouldSerializeIsHotspot() => this.IsHotspot;

  private void ResetIsHotspot() => this.IsHotspot = false;

  public bool IsChangeable
  {
    get => this.NativeScintilla.StyleGetChangeable(this._index);
    set => this.NativeScintilla.StyleSetChangeable(this._index, value);
  }

  private bool ShouldSerializeIsChangeable() => !this.IsChangeable;

  private void ResetIsChangeable() => this.IsChangeable = true;

  public override bool Equals(object obj)
  {
    return this.IsSameHelperFamily(obj) && ((Style) obj).Index == this.Index;
  }

  public override int GetHashCode() => base.GetHashCode();
}
