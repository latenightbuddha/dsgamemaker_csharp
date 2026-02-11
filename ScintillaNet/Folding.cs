using ScintillaNet.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Folding : TopLevelHelper
{
  private FoldFlag _flags;
  private FoldMarkerScheme _markerScheme;

  internal Folding(Scintilla scintilla)
    : base(scintilla)
  {
    this.IsEnabled = true;
    this.UseCompactFolding = false;
    this.MarkerScheme = FoldMarkerScheme.BoxPlusMinus;
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeFlags() || this.ShouldSerializeIsEnabled() || this.ShouldSerializeMarkerScheme() || this.ShouldSerializeUseCompactFolding();
  }

  public bool IsEnabled
  {
    get => this.Scintilla.Lexing.GetProperty("fold") == "1";
    set
    {
      string str = !value ? "0" : "1";
      this.Scintilla.Lexing.SetProperty("fold", str);
      this.Scintilla.Lexing.SetProperty("fold.html", str);
    }
  }

  private bool ShouldSerializeIsEnabled() => !this.IsEnabled;

  private void ResetIsEnabled() => this.IsEnabled = true;

  [Editor(typeof (FlagEnumUIEditor), typeof (UITypeEditor))]
  [Category("Appearance")]
  public FoldFlag Flags
  {
    get => this._flags;
    set
    {
      this._flags = value;
      this.NativeScintilla.SetFoldFlags((int) value);
    }
  }

  private bool ShouldSerializeFlags() => this.Flags != (FoldFlag) 0;

  private void ResetFlags() => this.Flags = (FoldFlag) 0;

  public bool UseCompactFolding
  {
    get => this.Scintilla.Lexing.GetProperty("fold.compact") == "1";
    set
    {
      string str = "0";
      if (value)
        str = "1";
      this.Scintilla.Lexing.SetProperty("fold.compact", str);
    }
  }

  private bool ShouldSerializeUseCompactFolding() => this.UseCompactFolding;

  private void ResetUseCompactFolding() => this.UseCompactFolding = false;

  public FoldMarkerScheme MarkerScheme
  {
    get => this._markerScheme;
    set
    {
      this._markerScheme = value;
      if (value == FoldMarkerScheme.Custom)
        return;
      MarkerCollection markers = this.Scintilla.Markers;
      markers.Folder.SetBackColorInternal(Color.Gray);
      markers.FolderEnd.SetBackColorInternal(Color.Gray);
      markers.FolderOpen.SetBackColorInternal(Color.Gray);
      markers.FolderOpenMid.SetBackColorInternal(Color.Gray);
      markers.FolderOpenMidTail.SetBackColorInternal(Color.Gray);
      markers.FolderSub.SetBackColorInternal(Color.Gray);
      markers.FolderTail.SetBackColorInternal(Color.Gray);
      markers.Folder.SetForeColorInternal(Color.White);
      markers.FolderEnd.SetForeColorInternal(Color.White);
      markers.FolderOpen.SetForeColorInternal(Color.White);
      markers.FolderOpenMid.SetForeColorInternal(Color.White);
      markers.FolderOpenMidTail.SetForeColorInternal(Color.White);
      markers.FolderSub.SetForeColorInternal(Color.White);
      markers.FolderTail.SetForeColorInternal(Color.White);
      switch (value)
      {
        case FoldMarkerScheme.PlusMinus:
          markers.Folder.SetSymbolInternal(MarkerSymbol.Plus);
          markers.FolderEnd.SetSymbolInternal(MarkerSymbol.Empty);
          markers.FolderOpen.SetSymbolInternal(MarkerSymbol.Minus);
          markers.FolderOpenMid.SetSymbolInternal(MarkerSymbol.Empty);
          markers.FolderOpenMidTail.SetSymbolInternal(MarkerSymbol.Empty);
          markers.FolderSub.SetSymbolInternal(MarkerSymbol.Empty);
          markers.FolderTail.SetSymbolInternal(MarkerSymbol.Empty);
          break;
        case FoldMarkerScheme.BoxPlusMinus:
          markers.Folder.SetSymbolInternal(MarkerSymbol.BoxPlus);
          markers.FolderEnd.SetSymbolInternal(MarkerSymbol.BoxPlusConnected);
          markers.FolderOpen.SetSymbolInternal(MarkerSymbol.BoxMinus);
          markers.FolderOpenMid.SetSymbolInternal(MarkerSymbol.BoxMinusConnected);
          markers.FolderOpenMidTail.SetSymbolInternal(MarkerSymbol.LCorner);
          markers.FolderSub.SetSymbolInternal(MarkerSymbol.VLine);
          markers.FolderTail.SetSymbolInternal(MarkerSymbol.LCorner);
          break;
        case FoldMarkerScheme.CirclePlusMinus:
          markers.Folder.SetSymbolInternal(MarkerSymbol.CirclePlus);
          markers.FolderEnd.SetSymbolInternal(MarkerSymbol.CirclePlusConnected);
          markers.FolderOpen.SetSymbolInternal(MarkerSymbol.CircleMinus);
          markers.FolderOpenMid.SetSymbolInternal(MarkerSymbol.CircleMinusConnected);
          markers.FolderOpenMidTail.SetSymbolInternal(MarkerSymbol.LCornerCurve);
          markers.FolderSub.SetSymbolInternal(MarkerSymbol.VLine);
          markers.FolderTail.SetSymbolInternal(MarkerSymbol.LCornerCurve);
          break;
        case FoldMarkerScheme.Arrow:
          markers.Folder.SetSymbolInternal(MarkerSymbol.Arrow);
          markers.FolderEnd.SetSymbolInternal(MarkerSymbol.Empty);
          markers.FolderOpen.SetSymbolInternal(MarkerSymbol.ArrowDown);
          markers.FolderOpenMid.SetSymbolInternal(MarkerSymbol.Empty);
          markers.FolderOpenMidTail.SetSymbolInternal(MarkerSymbol.Empty);
          markers.FolderSub.SetSymbolInternal(MarkerSymbol.Empty);
          markers.FolderTail.SetSymbolInternal(MarkerSymbol.Empty);
          break;
      }
    }
  }

  private bool ShouldSerializeMarkerScheme() => this._markerScheme != FoldMarkerScheme.BoxPlusMinus;

  private void ResetMarkerScheme() => this.MarkerScheme = FoldMarkerScheme.BoxPlusMinus;
}
