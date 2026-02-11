using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class HotspotStyle : TopLevelHelper
{
  private bool _useActiveForeColor = true;
  private bool _useActiveBackColor = true;

  internal HotspotStyle(Scintilla scintilla)
    : base(scintilla)
  {
    this.ActiveForeColor = SystemColors.HotTrack;
    this.ActiveBackColor = SystemColors.Window;
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeActiveBackColor() || this.ShouldSerializeActiveForeColor() || this.ShouldSerializeActiveUnderline() || this.ShouldSerializeSingleLine() || this.ShouldSerializeUseActiveBackColor() || this.ShouldSerializeUseActiveForeColor();
  }

  public Color ActiveForeColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("HotspotStyle.ActiveForeColor") ? this.Scintilla.ColorBag["HotspotStyle.ActiveForeColor"] : Utilities.RgbToColor(this.NativeScintilla.GetHotSpotActiveFore());
    }
    set
    {
      if (value.IsKnownColor)
        this.Scintilla.ColorBag["HotspotStyle.ActiveForeColor"] = value;
      else
        this.Scintilla.ColorBag.Remove("HotspotStyle.ActiveForeColor");
      this.NativeScintilla.SetHotspotActiveFore(this._useActiveForeColor, Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeActiveForeColor() => this.ActiveForeColor != SystemColors.HotTrack;

  private void ResetActiveForeColor() => this.ActiveForeColor = SystemColors.HotTrack;

  public Color ActiveBackColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("HotspotStyle.ActiveBackColor") ? this.Scintilla.ColorBag["HotspotStyle.ActiveBackColor"] : Utilities.RgbToColor(this.NativeScintilla.GetHotSpotActiveBack());
    }
    set
    {
      if (value.IsKnownColor)
        this.Scintilla.ColorBag["HotspotStyle.ActiveBackColor"] = value;
      else
        this.Scintilla.ColorBag.Remove("HotspotStyle.ActiveBackColor");
      this.NativeScintilla.SetHotspotActiveBack(this._useActiveBackColor, Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeActiveBackColor() => this.ActiveBackColor != SystemColors.Window;

  private void ResetActiveBackColor() => this.ActiveBackColor = SystemColors.Window;

  public bool ActiveUnderline
  {
    get => this.NativeScintilla.GetHotSpotActiveUnderline();
    set => this.NativeScintilla.SetHotspotActiveUnderline(value);
  }

  private bool ShouldSerializeActiveUnderline() => !this.ActiveUnderline;

  private void ResetActiveUnderline() => this.ActiveUnderline = true;

  public bool SingleLine
  {
    get => this.NativeScintilla.GetHotSpotSingleLine();
    set => this.NativeScintilla.SetHotspotSingleLine(value);
  }

  private bool ShouldSerializeSingleLine() => !this.SingleLine;

  private void ResetSingleLine() => this.SingleLine = true;

  public bool UseActiveForeColor
  {
    get => this._useActiveForeColor;
    set => this._useActiveForeColor = value;
  }

  private bool ShouldSerializeUseActiveForeColor() => !this.UseActiveForeColor;

  private void ResetUseActiveForeColor() => this.UseActiveForeColor = true;

  public bool UseActiveBackColor
  {
    get => this._useActiveBackColor;
    set => this._useActiveBackColor = value;
  }

  private bool ShouldSerializeUseActiveBackColor() => !this.UseActiveBackColor;

  private void ResetUseActiveBackColor() => this.UseActiveBackColor = true;
}
