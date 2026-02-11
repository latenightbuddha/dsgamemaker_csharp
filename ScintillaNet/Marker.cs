using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Marker : ScintillaHelperBase
{
  private int _number;

  internal Marker(Scintilla scintilla, int number)
    : base(scintilla)
  {
    this._number = number;
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeAlpha() || this.ShouldSerializeBackColor() || this.ShouldSerializeForeColor() || this.ShouldSerializeSymbol();
  }

  public void Reset()
  {
    this.ResetAlpha();
    this.ResetBackColor();
    this.ResetForeColor();
    this.ResetSymbol();
  }

  public int Number
  {
    get => this._number;
    set => this._number = value;
  }

  public uint Mask => (uint) (1 << this.Number);

  public MarkerSymbol Symbol
  {
    get
    {
      return this.Scintilla.PropertyBag.ContainsKey((object) (this.ToString() + ".Symbol")) ? (MarkerSymbol) this.Scintilla.PropertyBag[(object) (this.ToString() + ".Symbol")] : MarkerSymbol.Circle;
    }
    set
    {
      this.SetSymbolInternal(value);
      this.Scintilla.Folding.MarkerScheme = FoldMarkerScheme.Custom;
    }
  }

  internal void SetSymbolInternal(MarkerSymbol value)
  {
    this.Scintilla.PropertyBag[(object) (this.ToString() + ".Symbol")] = (object) value;
    this.NativeScintilla.MarkerDefine(this._number, (int) value);
  }

  private bool ShouldSerializeSymbol()
  {
    return this.Scintilla.Folding.MarkerScheme == FoldMarkerScheme.Custom && this.Symbol != MarkerSymbol.Circle;
  }

  private void ResetSymbol() => this.Symbol = MarkerSymbol.Circle;

  public Color ForeColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey(this.ToString() + ".ForeColor") ? this.Scintilla.ColorBag[this.ToString() + ".ForeColor"] : Color.Black;
    }
    set
    {
      this.SetForeColorInternal(value);
      this.Scintilla.Folding.MarkerScheme = FoldMarkerScheme.Custom;
    }
  }

  internal void SetForeColorInternal(Color value)
  {
    this.Scintilla.ColorBag[this.ToString() + ".ForeColor"] = value;
    this.NativeScintilla.MarkerSetFore(this._number, Utilities.ColorToRgb(value));
  }

  private bool ShouldSerializeForeColor()
  {
    return this.Scintilla.Folding.MarkerScheme == FoldMarkerScheme.Custom && this.ForeColor != Color.Black;
  }

  private void ResetForeColor() => this.ForeColor = Color.Black;

  public Color BackColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey(this.ToString() + ".BackColor") ? this.Scintilla.ColorBag[this.ToString() + ".BackColor"] : Color.White;
    }
    set
    {
      this.SetBackColorInternal(value);
      this.Scintilla.Folding.MarkerScheme = FoldMarkerScheme.Custom;
    }
  }

  internal void SetBackColorInternal(Color value)
  {
    this.Scintilla.ColorBag[this.ToString() + ".BackColor"] = value;
    this.NativeScintilla.MarkerSetBack(this._number, Utilities.ColorToRgb(value));
  }

  private bool ShouldSerializeBackColor()
  {
    return this.Scintilla.Folding.MarkerScheme == FoldMarkerScheme.Custom && this.BackColor != Color.White;
  }

  private void ResetBackColor() => this.BackColor = Color.White;

  public int Alpha
  {
    get
    {
      try
      {
        return this.Scintilla.PropertyBag.ContainsKey((object) (this.ToString() + ".Alpha")) ? (int) this.Scintilla.PropertyBag[(object) (this.ToString() + nameof (Alpha))] : (int) byte.MaxValue;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
        return (int) byte.MaxValue;
      }
    }
    set
    {
      this.Scintilla.PropertyBag[(object) (this.ToString() + ".Alpha")] = (object) value;
      this.NativeScintilla.MarkerSetAlpha(this._number, value);
    }
  }

  private bool ShouldSerializeAlpha() => this.Alpha != (int) byte.MaxValue;

  private void ResetAlpha() => this.Alpha = (int) byte.MaxValue;

  public override string ToString() => "MarkerNumber" + (object) this._number;

  public void SetImage(string xpmImage)
  {
    this.NativeScintilla.MarkerDefinePixmap(this._number, xpmImage);
  }

  public void SetImage(Bitmap image, Color transparentColor)
  {
    this.NativeScintilla.MarkerDefinePixmap(this._number, XpmConverter.ConvertToXPM(image, Utilities.ColorToHtml(transparentColor)));
  }

  public void SetImage(Bitmap image)
  {
    this.NativeScintilla.MarkerDefinePixmap(this._number, XpmConverter.ConvertToXPM(image));
  }

  public MarkerInstance AddInstanceTo(int line)
  {
    return new MarkerInstance(this.Scintilla, this, this.NativeScintilla.MarkerAdd(line, this._number));
  }

  public MarkerInstance AddInstanceTo(Line line) => this.AddInstanceTo(line.Number);

  public override bool Equals(object obj)
  {
    return this.IsSameHelperFamily(obj) && ((Marker) obj).Number == this.Number;
  }

  public override int GetHashCode() => base.GetHashCode();
}
