using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class DropMarkers : TopLevelHelper
{
  private Stack<DropMarker> _markerStack = new Stack<DropMarker>();
  private static Dictionary<string, Stack<DropMarker>> _sharedStack = new Dictionary<string, Stack<DropMarker>>();
  private string _sharedStackName = string.Empty;
  private DropMarkerList _allDocumentDropMarkers = new DropMarkerList();

  internal DropMarkers(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize() => this.ShouldSerializeSharedStackName();

  public string SharedStackName
  {
    get => this._sharedStackName;
    set
    {
      if (value == null)
        value = string.Empty;
      if (this._sharedStackName == value)
        return;
      if (value == string.Empty)
      {
        this._markerStack = new Stack<DropMarker>();
        if (DropMarkers._sharedStack.ContainsKey(this._sharedStackName) && DropMarkers._sharedStack[this._sharedStackName].Count == 1)
          DropMarkers._sharedStack.Remove(this._sharedStackName);
      }
      else
      {
        if (!DropMarkers._sharedStack.ContainsKey(this._sharedStackName))
          DropMarkers._sharedStack[this._sharedStackName] = new Stack<DropMarker>();
        this._markerStack = DropMarkers._sharedStack[this._sharedStackName];
      }
      this._sharedStackName = value;
    }
  }

  private bool ShouldSerializeSharedStackName() => this._sharedStackName != string.Empty;

  private void ResetSharedStackName() => this._sharedStackName = string.Empty;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Stack<DropMarker> MarkerStack
  {
    get => this._markerStack;
    set => this._markerStack = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DropMarkerList AllDocumentDropMarkers
  {
    get => this._allDocumentDropMarkers;
    set => this._allDocumentDropMarkers = value;
  }

  public DropMarker Drop() => this.Drop(this.NativeScintilla.GetCurrentPos());

  public DropMarker Drop(int position)
  {
    DropMarker dropMarker = new DropMarker(position, position, this.getCurrentTopOffset(), this.Scintilla);
    this._allDocumentDropMarkers.Add(dropMarker);
    this._markerStack.Push(dropMarker);
    this.Scintilla.ManagedRanges.Add((ManagedRange) dropMarker);
    this.Scintilla.Invalidate(dropMarker.GetClientRectangle());
    return dropMarker;
  }

  public void Collect()
  {
    while (this._markerStack.Count > 0)
    {
      DropMarker dropMarker = this._markerStack.Pop();
      if (!dropMarker.IsDisposed)
      {
        if (dropMarker.Collect())
          break;
        this._markerStack.Push(dropMarker);
        break;
      }
    }
  }

  private int getCurrentTopOffset() => -1;
}
