using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Margin : ScintillaHelperBase
{
  private int _number;
  private int _autoToggleMarkerNumber = -1;

  protected internal Margin(Scintilla scintilla, int number)
    : base(scintilla)
  {
    this._number = number;
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeIsFoldMargin() || this.ShouldSerializeIsClickable() || this.ShouldSerializeType() || this.ShouldSerializeWidth() || this.ShouldSerializeAutoToggleMarkerNumber() || this.ShouldSerializeIsMarkerMargin();
  }

  public void Reset()
  {
    this.ResetAutoToggleMarkerNumber();
    this.ResetIsClickable();
    this.ResetIsFoldMargin();
    this.ResetIsMarkerMargin();
    this.ResetType();
    this.ResetWidth();
  }

  public override string ToString()
  {
    if (this._number == 0)
      return "(Default Line Numbers)";
    if (this._number == 1)
      return "(Default Markers)";
    return this._number == 2 ? "(Default Folds)" : base.ToString();
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int Number => this._number;

  public MarginType Type
  {
    get => (MarginType) this.NativeScintilla.GetMarginTypeN(this._number);
    set => this.NativeScintilla.SetMarginTypeN(this._number, (int) value);
  }

  private bool ShouldSerializeType()
  {
    return this._number == 0 ? this.Type != MarginType.Number : this.Type != MarginType.Symbol;
  }

  internal void ResetType()
  {
    if (this._number == 0)
      this.Type = MarginType.Number;
    else
      this.Type = MarginType.Symbol;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int Mask
  {
    get => this.NativeScintilla.GetMarginMaskN(this._number);
    set => this.NativeScintilla.SetMarginMaskN(this._number, value);
  }

  public bool IsFoldMargin
  {
    get => (this.Mask & -33554432 /*0xFE000000*/) == -33554432 /*0xFE000000*/;
    set
    {
      if (value)
        this.Mask |= -33554432 /*0xFE000000*/;
      else
        this.Mask &= 33554431 /*0x01FFFFFF*/;
    }
  }

  private bool ShouldSerializeIsFoldMargin()
  {
    return this._number == 2 ? !this.IsFoldMargin : this.IsFoldMargin;
  }

  internal void ResetIsFoldMargin()
  {
    if (this._number == 2)
      this.IsFoldMargin = true;
    else
      this.IsFoldMargin = false;
  }

  public bool IsMarkerMargin
  {
    get => (this.Mask & 33554431 /*0x01FFFFFF*/) == 33554431 /*0x01FFFFFF*/;
    set
    {
      if (value)
        this.Mask |= 33554431 /*0x01FFFFFF*/;
      else
        this.Mask &= -33554432 /*0xFE000000*/;
    }
  }

  private bool ShouldSerializeIsMarkerMargin()
  {
    return this._number == 1 ? !this.IsMarkerMargin : this.IsMarkerMargin;
  }

  internal void ResetIsMarkerMargin()
  {
    if (this._number == 1)
      this.IsMarkerMargin = true;
    else
      this.IsMarkerMargin = false;
  }

  public int Width
  {
    get => this.NativeScintilla.GetMarginWidthN(this._number);
    set => this.NativeScintilla.SetMarginWidthN(this._number, value);
  }

  private bool ShouldSerializeWidth()
  {
    return this._number == 1 ? this.Width != 16 /*0x10*/ : this.Width != 0;
  }

  internal void ResetWidth()
  {
    if (this._number == 1)
      this.Width = 16 /*0x10*/;
    else
      this.Width = 0;
  }

  public bool IsClickable
  {
    get => this.NativeScintilla.GetMarginSensitiveN(this._number);
    set => this.NativeScintilla.SetMarginSensitiveN(this._number, value);
  }

  private bool ShouldSerializeIsClickable()
  {
    return this._number == 2 ? !this.IsClickable : this.IsClickable;
  }

  internal void ResetIsClickable()
  {
    if (this._number == 2)
      this.IsClickable = true;
    else
      this.IsClickable = false;
  }

  public int AutoToggleMarkerNumber
  {
    get => this._autoToggleMarkerNumber;
    set => this._autoToggleMarkerNumber = value;
  }

  private bool ShouldSerializeAutoToggleMarkerNumber() => this._autoToggleMarkerNumber != -1;

  private void ResetAutoToggleMarkerNumber() => this._autoToggleMarkerNumber = -1;

  public Rectangle GetClientRectangle()
  {
    int x = 0;
    for (int margin = 0; margin < this._number; ++margin)
      x += this.NativeScintilla.GetMarginWidthN(margin);
    return new Rectangle(x, 0, this.Width, this.Scintilla.ClientSize.Height);
  }

  public override bool Equals(object obj)
  {
    return this.IsSameHelperFamily(obj) && this.Number == ((Margin) obj).Number;
  }

  public override int GetHashCode() => base.GetHashCode();
}
