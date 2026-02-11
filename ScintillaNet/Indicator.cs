using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Indicator : ScintillaHelperBase
{
  private int _number;

  internal Indicator(int number, Scintilla scintilla)
    : base(scintilla)
  {
    this._number = number;
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeColor() || this.ShouldSerializeIsDrawnUnder() || this.ShouldSerializeStyle();
  }

  public int Number
  {
    get => this._number;
    set => this._number = value;
  }

  public Color Color
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey(this.ToString() + ".Color") ? this.Scintilla.ColorBag[this.ToString() + ".Color"] : Utilities.RgbToColor(this.NativeScintilla.IndicGetFore(this._number));
    }
    set
    {
      this.Scintilla.ColorBag[this.ToString() + ".Color"] = value;
      this.NativeScintilla.IndicSetFore(this._number, Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeColor() => this.Color != this.getDefaultColor();

  public void ResetColor() => this.Color = this.getDefaultColor();

  private Color getDefaultColor()
  {
    if (this._number == 0)
      return Color.FromArgb(0, (int) sbyte.MaxValue, 0);
    if (this._number == 1)
      return Color.FromArgb(0, 0, (int) byte.MaxValue);
    return this._number == 2 ? Color.FromArgb((int) byte.MaxValue, 0, 0) : Color.FromArgb(0, 0, 0);
  }

  public IndicatorStyle Style
  {
    get => (IndicatorStyle) this.NativeScintilla.IndicGetStyle(this._number);
    set => this.NativeScintilla.IndicSetStyle(this._number, (int) value);
  }

  private bool ShouldSerializeStyle() => this.Style != this.getDefaultStyle();

  public void ResetStyle() => this.Style = this.getDefaultStyle();

  private IndicatorStyle getDefaultStyle()
  {
    if (this._number == 0)
      return IndicatorStyle.Squiggle;
    return this._number == 1 ? IndicatorStyle.TT : IndicatorStyle.Plain;
  }

  public bool IsDrawnUnder
  {
    get => this.NativeScintilla.IndicGetUnder(this._number);
    set => this.NativeScintilla.IndicSetUnder(this._number, value);
  }

  private bool ShouldSerializeIsDrawnUnder() => this.IsDrawnUnder;

  public void ResetIsDrawnUnder() => this.IsDrawnUnder = false;

  public void Reset()
  {
    this.ResetColor();
    this.ResetIsDrawnUnder();
    this.ResetStyle();
  }

  public Range Search() => this.Search(this.Scintilla.GetRange());

  public Range Search(Range searchRange)
  {
    int num = this.NativeScintilla.IndicatorEnd(this._number, searchRange.Start);
    int end = this.NativeScintilla.IndicatorEnd(this._number, num);
    return num < 0 || num > searchRange.End || num == end ? (Range) null : new Range(num, end, this.Scintilla);
  }

  public Range Search(Range searchRange, Range startingAfterRange)
  {
    int end1 = startingAfterRange.End;
    if (end1 > this.NativeScintilla.GetTextLength())
      return (Range) null;
    int num = this.NativeScintilla.IndicatorEnd(this._number, end1);
    int end2 = this.NativeScintilla.IndicatorEnd(this._number, num);
    return num < 0 || num > searchRange.End || num == end2 ? (Range) null : new Range(num, end2, this.Scintilla);
  }

  public List<Range> SearchAll() => this.SearchAll(this.Scintilla.GetRange());

  public List<Range> SearchAll(Range searchRange)
  {
    Range startingAfterRange = this.Scintilla.GetRange(-1, -1);
    List<Range> rangeList = new List<Range>();
    do
    {
      startingAfterRange = this.Search(searchRange, startingAfterRange);
      if (startingAfterRange != null)
        rangeList.Add(startingAfterRange);
    }
    while (startingAfterRange != null);
    return rangeList;
  }

  public override string ToString() => nameof (Indicator) + (object) this._number;

  public override bool Equals(object obj)
  {
    return this.IsSameHelperFamily(obj) && ((Indicator) obj).Number == this.Number;
  }

  public override int GetHashCode() => base.GetHashCode();
}
