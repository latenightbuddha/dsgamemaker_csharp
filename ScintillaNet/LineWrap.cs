using ScintillaNet.Design;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class LineWrap : TopLevelHelper
{
  internal LineWrap(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeLayoutCache() || this.ShouldSerializePositionCacheSize() || this.ShouldSerializeStartIndent() || this.ShouldSerializeVisualFlags() || this.ShouldSerializeVisualFlagsLocation() || this.ShouldSerializeMode();
  }

  public WrapMode Mode
  {
    get => (WrapMode) this.NativeScintilla.GetWrapMode();
    set => this.NativeScintilla.SetWrapMode((int) value);
  }

  private bool ShouldSerializeMode() => this.Mode != WrapMode.None;

  private void ResetMode() => this.Mode = WrapMode.None;

  [Editor(typeof (FlagEnumUIEditor), typeof (UITypeEditor))]
  public WrapVisualFlag VisualFlags
  {
    get => (WrapVisualFlag) this.NativeScintilla.GetWrapVisualFlags();
    set => this.NativeScintilla.SetWrapVisualFlags((int) value);
  }

  private bool ShouldSerializeVisualFlags() => this.VisualFlags != WrapVisualFlag.None;

  private void ResetVisualFlags() => this.VisualFlags = WrapVisualFlag.None;

  [Editor(typeof (FlagEnumUIEditor), typeof (UITypeEditor))]
  public WrapVisualLocation VisualFlagsLocation
  {
    get => (WrapVisualLocation) this.NativeScintilla.GetWrapVisualFlagsLocation();
    set => this.NativeScintilla.SetWrapVisualFlagsLocation((int) value);
  }

  private bool ShouldSerializeVisualFlagsLocation()
  {
    return this.VisualFlagsLocation != WrapVisualLocation.Default;
  }

  private void ResetVisualFlagsLocation() => this.VisualFlagsLocation = WrapVisualLocation.Default;

  public int StartIndent
  {
    get => this.NativeScintilla.GetWrapStartIndent();
    set => this.NativeScintilla.SetWrapStartIndent(value);
  }

  private bool ShouldSerializeStartIndent() => this.StartIndent != 0;

  private void ResetStartIndent() => this.StartIndent = 0;

  public LineCache LayoutCache
  {
    get => (LineCache) this.NativeScintilla.GetLayoutCache();
    set => this.NativeScintilla.SetLayoutCache((int) value);
  }

  private bool ShouldSerializeLayoutCache() => this.LayoutCache != LineCache.Caret;

  private void ResetLayoutCache() => this.LayoutCache = LineCache.Caret;

  public int PositionCacheSize
  {
    get => this.NativeScintilla.GetPositionCache();
    set => this.NativeScintilla.SetPositionCache(value);
  }

  private bool ShouldSerializePositionCacheSize() => this.PositionCacheSize != 1024 /*0x0400*/;

  private void ResetPositionCacheSize() => this.PositionCacheSize = 1024 /*0x0400*/;
}
