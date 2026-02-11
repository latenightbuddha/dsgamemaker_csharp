using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class LongLines : TopLevelHelper
{
  internal LongLines(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeEdgeColor() || this.ShouldSerializeEdgeColumn() || this.ShouldSerializeEdgeMode();
  }

  public EdgeMode EdgeMode
  {
    get => (EdgeMode) this.NativeScintilla.GetEdgeMode();
    set => this.NativeScintilla.SetEdgeMode((int) value);
  }

  private bool ShouldSerializeEdgeMode() => this.EdgeMode != EdgeMode.None;

  private void ResetEdgeMode() => this.EdgeMode = EdgeMode.None;

  public int EdgeColumn
  {
    get => this.NativeScintilla.GetEdgeColumn();
    set => this.NativeScintilla.SetEdgeColumn(value);
  }

  private bool ShouldSerializeEdgeColumn() => this.EdgeColumn != 0;

  private void ResetEdgeColumn() => this.EdgeColumn = 0;

  public Color EdgeColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("LongLines.EdgeColor") ? this.Scintilla.ColorBag["LongLines.EdgeColor"] : Color.Silver;
    }
    set
    {
      if (value == Color.Silver)
        this.Scintilla.ColorBag.Remove("LongLines.EdgeColor");
      this.Scintilla.ColorBag["LongLines.EdgeColor"] = value;
      this.NativeScintilla.SetEdgeColour(Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeEdgeColor() => this.EdgeColor != Color.Silver;

  private void ResetEdgeColor() => this.EdgeColor = Color.Silver;
}
