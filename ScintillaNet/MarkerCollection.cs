using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class MarkerCollection : TopLevelHelper
{
  internal MarkerCollection(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeFolder() || this.ShouldSerializeFolderEnd() || this.ShouldSerializeFolderOpen() || this.ShouldSerializeFolderOpenMid() || this.ShouldSerializeFolderOpenMidTail() || this.ShouldSerializeFolderSub() || this.ShouldSerializeFolderTail();
  }

  public void Reset()
  {
    for (int markerNumber = 0; markerNumber < 32 /*0x20*/; ++markerNumber)
      this[markerNumber].Reset();
  }

  public void AddInstanceSet(int line, uint markerMask)
  {
    this.NativeScintilla.MarkerAddSet(line, markerMask);
  }

  public void AddInstanceSet(Line line, uint markerMask)
  {
    this.AddInstanceSet(line.Number, markerMask);
  }

  public void AddInstanceSet(Line line, IEnumerable<Marker> markers)
  {
    this.AddInstanceSet(line, Utilities.GetMarkerMask(markers));
  }

  public void DeleteInstance(int line, int markerNumber)
  {
    this.NativeScintilla.MarkerDelete(line, markerNumber);
  }

  public void DeleteInstance(int line, Marker marker) => this.DeleteInstance(line, marker.Number);

  public void DeleteAll(int marker) => this.NativeScintilla.MarkerDeleteAll(marker);

  public void DeleteAll(Marker marker) => this.NativeScintilla.MarkerDeleteAll(marker.Number);

  public void DeleteAll() => this.NativeScintilla.MarkerDeleteAll(-1);

  public int GetMarkerMask(int line) => this.NativeScintilla.MarkerGet(line);

  public int GetMarkerMask(Line line) => this.NativeScintilla.MarkerGet(line.Number);

  public List<Marker> GetMarkers(Line line) => this.GetMarkers(line.Number);

  public List<Marker> GetMarkers(int line)
  {
    List<Marker> markers = new List<Marker>();
    int markerMask = this.GetMarkerMask(line);
    for (int number = 0; number < 32 /*0x20*/; ++number)
    {
      if ((markerMask & number) == number)
        markers.Add(new Marker(this.Scintilla, number));
    }
    return markers;
  }

  public Line FindNextMarker(int line, uint markerMask)
  {
    int number = this.NativeScintilla.MarkerNext(line, markerMask);
    return number < 0 ? (Line) null : new Line(this.Scintilla, number);
  }

  public Line FindNextMarker(Line line, uint markerMask)
  {
    return this.FindNextMarker(line.Number, markerMask);
  }

  public Line FindNextMarker(Line line, Marker marker)
  {
    return this.FindNextMarker(line.Number, (uint) marker.Number);
  }

  public Line FindNextMarker(Line line, IEnumerable<int> markers)
  {
    return this.FindNextMarker(line.Number, Utilities.GetMarkerMask(markers));
  }

  public Line FindNextMarker(Line line, IEnumerable<Marker> markers)
  {
    return this.FindNextMarker(line.Number, Utilities.GetMarkerMask(markers));
  }

  public Line FindNextMarker(int line, Marker marker)
  {
    return this.FindNextMarker(line, (uint) marker.Number);
  }

  public Line FindNextMarker(Marker marker)
  {
    return this.FindNextMarker(this.nextLine(), (uint) marker.Number);
  }

  public Line FindNextMarker(uint markerMask) => this.FindNextMarker(this.nextLine(), markerMask);

  public Line FindNextMarker(IEnumerable<int> markers)
  {
    return this.FindNextMarker(this.nextLine(), Utilities.GetMarkerMask(markers));
  }

  public Line FindNextMarker(IEnumerable<Marker> markers)
  {
    return this.FindNextMarker(this.nextLine(), Utilities.GetMarkerMask(markers));
  }

  public Line FindNextMarker(int line) => this.FindNextMarker(line, uint.MaxValue);

  public Line FindNextMarker(Line line) => this.FindNextMarker(line.Number, uint.MaxValue);

  public Line FindNextMarker() => this.FindNextMarker(this.nextLine(), uint.MaxValue);

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Marker this[int markerNumber] => new Marker(this.Scintilla, markerNumber);

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Marker FolderEnd => new Marker(this.Scintilla, 25);

  private bool ShouldSerializeFolderEnd() => this.FolderEnd.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Marker FolderOpenMid => new Marker(this.Scintilla, 26);

  private bool ShouldSerializeFolderOpenMid() => this.FolderOpenMid.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Marker FolderOpenMidTail => new Marker(this.Scintilla, 27);

  private bool ShouldSerializeFolderOpenMidTail() => this.FolderOpenMidTail.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Marker FolderTail => new Marker(this.Scintilla, 28);

  private bool ShouldSerializeFolderTail() => this.FolderTail.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Marker FolderSub => new Marker(this.Scintilla, 29);

  private bool ShouldSerializeFolderSub() => this.FolderSub.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Marker Folder => new Marker(this.Scintilla, 30);

  private bool ShouldSerializeFolder() => this.Folder.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Marker FolderOpen => new Marker(this.Scintilla, 31 /*0x1F*/);

  private bool ShouldSerializeFolderOpen() => this.FolderOpen.ShouldSerialize();

  public Line FindPreviousMarker(int line, uint markerMask)
  {
    int number = this.NativeScintilla.MarkerPrevious(line, markerMask);
    return number < 0 ? (Line) null : new Line(this.Scintilla, number);
  }

  public Line FindPreviousMarker(Line line, uint markerMask)
  {
    return this.FindPreviousMarker(line.Number, markerMask);
  }

  public Line FindPreviousMarker(Line line, Marker marker)
  {
    return this.FindPreviousMarker(line.Number, (uint) marker.Number);
  }

  public Line FindPreviousMarker(Line line, IEnumerable<int> markers)
  {
    return this.FindPreviousMarker(line.Number, Utilities.GetMarkerMask(markers));
  }

  public Line FindPreviousMarker(Line line, IEnumerable<Marker> markers)
  {
    return this.FindPreviousMarker(line.Number, Utilities.GetMarkerMask(markers));
  }

  public Line FindPreviousMarker(int line, Marker marker)
  {
    return this.FindPreviousMarker(line, (uint) marker.Number);
  }

  public Line FindPreviousMarker(Marker marker)
  {
    return this.FindPreviousMarker(this.prevLine(), (uint) marker.Number);
  }

  public Line FindPreviousMarker(uint markerMask)
  {
    return this.FindPreviousMarker(this.prevLine(), markerMask);
  }

  public Line FindPreviousMarker(int line) => this.FindPreviousMarker(line, uint.MaxValue);

  public Line FindPreviousMarker(IEnumerable<int> markers)
  {
    return this.FindPreviousMarker(this.prevLine(), Utilities.GetMarkerMask(markers));
  }

  public Line FindPreviousMarker(IEnumerable<Marker> markers)
  {
    return this.FindPreviousMarker(this.nextLine(), Utilities.GetMarkerMask(markers));
  }

  public Line FindPreviousMarker(Line line) => this.FindPreviousMarker(line.Number, uint.MaxValue);

  public Line FindPreviousMarker() => this.FindPreviousMarker(this.prevLine(), uint.MaxValue);

  private int nextLine()
  {
    return this.NativeScintilla.LineFromPosition(this.NativeScintilla.GetCurrentPos()) + 1;
  }

  private int prevLine()
  {
    return this.NativeScintilla.LineFromPosition(this.NativeScintilla.GetCurrentPos()) - 1;
  }
}
