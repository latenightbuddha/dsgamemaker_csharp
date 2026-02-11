using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class StyleCollection : TopLevelHelper
{
  internal StyleCollection(Scintilla scintilla)
    : base(scintilla)
  {
    this.Bits = 7;
    Style callTip = this.CallTip;
    callTip.ForeColor = SystemColors.InfoText;
    callTip.BackColor = SystemColors.Info;
    callTip.Font = SystemFonts.StatusFont;
    this.LineNumber.BackColor = SystemColors.Control;
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeBits() || this.ShouldSerializeBraceBad() || this.ShouldSerializeBraceLight() || this.ShouldSerializeCallTip() || this.ShouldSerializeControlChar() || this.ShouldSerializeDefault() || this.ShouldSerializeIndentGuide() || this.ShouldSerializeLastPredefined() || this.ShouldSerializeLineNumber() || this.ShouldSerializeMax();
  }

  public void Reset()
  {
    for (int index = 0; index < 32 /*0x20*/; ++index)
      this[index].Reset();
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public Style this[int index] => new Style(index, this.Scintilla);

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public Style this[StylesCommon index] => new Style((int) index, this.Scintilla);

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Style this[string styleName]
  {
    get => new Style(this.Scintilla.Lexing.StyleNameMap[styleName], this.Scintilla);
  }

  [Obsolete("The modern style indicators make this obsolete, this should always be 7")]
  public int Bits
  {
    get => this.NativeScintilla.GetStyleBits();
    set => this.NativeScintilla.SetStyleBits(value);
  }

  private bool ShouldSerializeBits() => this.Bits != 7;

  private void ResetBits() => this.Bits = 7;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Style BraceBad => this[StylesCommon.BraceBad];

  private bool ShouldSerializeBraceBad() => this.BraceBad.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Style BraceLight => this[StylesCommon.BraceLight];

  private bool ShouldSerializeBraceLight() => this.BraceLight.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Style CallTip => this[StylesCommon.CallTip];

  private bool ShouldSerializeCallTip() => this.CallTip.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Style ControlChar => this[StylesCommon.ControlChar];

  private bool ShouldSerializeControlChar() => this.ControlChar.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Style Default => this[StylesCommon.Default];

  private bool ShouldSerializeDefault() => this.BraceBad.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Style IndentGuide => this[StylesCommon.IndentGuide];

  private bool ShouldSerializeIndentGuide() => this.IndentGuide.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Style LastPredefined => this[StylesCommon.LastPredefined];

  private bool ShouldSerializeLastPredefined() => this.LastPredefined.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Style LineNumber => this[StylesCommon.LineNumber];

  private bool ShouldSerializeLineNumber() => this.LineNumber.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Style Max => this[StylesCommon.Max];

  private bool ShouldSerializeMax() => this.Max.ShouldSerialize();

  public void ClearAll() => this.NativeScintilla.StyleClearAll();

  public int GetEndStyled() => this.NativeScintilla.GetEndStyled();

  public byte GetStyleAt(int position) => this.NativeScintilla.GetStyleAt(position);

  public string GetStyleNameAt(int position)
  {
    int styleAt = (int) this.GetStyleAt(position);
    foreach (KeyValuePair<string, int> styleName in this.Scintilla.Lexing.StyleNameMap)
    {
      if (styleName.Value == styleAt)
        return styleName.Key;
    }
    return (string) null;
  }

  public void ResetDefault() => this.NativeScintilla.StyleResetDefault();

  public void ClearDocumentStyle() => this.NativeScintilla.ClearDocumentStyle();
}
