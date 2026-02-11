using System.ComponentModel;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class IndicatorCollection : TopLevelHelper
{
  internal IndicatorCollection(Scintilla scintilla)
    : base(scintilla)
  {
  }

  public Indicator this[int number] => new Indicator(number, this.Scintilla);

  public void Reset()
  {
    for (int number = 0; number < 32 /*0x20*/; ++number)
      this[number].Reset();
  }
}
