using System.Collections.Generic;

#nullable disable
namespace ScintillaNet;

public class Line : ScintillaHelperBase
{
  private int _number;

  protected internal Line(Scintilla scintilla, int number)
    : base(scintilla)
  {
    this._number = number;
  }

  public int Number
  {
    get => this._number;
    set => this._number = value;
  }

  public string Text
  {
    get
    {
      string text;
      this.NativeScintilla.GetLine(this._number, out text);
      return text;
    }
    set
    {
      this.NativeScintilla.SetTargetStart(this.StartPosition);
      this.NativeScintilla.SetTargetEnd(this.EndPosition);
      this.NativeScintilla.ReplaceTarget(-1, value);
    }
  }

  public Range Range => this.Scintilla.GetRange(this.StartPosition, this.EndPosition);

  public void Select() => this.NativeScintilla.SetSel(this.StartPosition, this.EndPosition);

  public int StartPosition => this.NativeScintilla.PositionFromLine(this._number);

  public int EndPosition => this.NativeScintilla.GetLineEndPosition(this._number);

  public int Length => this.NativeScintilla.LineLength(this._number);

  public int Height => this.NativeScintilla.TextHeight(this._number);

  public void Goto() => this.NativeScintilla.GotoLine(this._number);

  public int LineState
  {
    get => this.NativeScintilla.GetLineState(this._number);
    set => this.NativeScintilla.SetLineState(this._number, value);
  }

  public int Indentation
  {
    get => this.NativeScintilla.GetLineIndentation(this._number);
    set => this.NativeScintilla.SetLineIndentation(this._number, value);
  }

  public int IndentPosition => this.NativeScintilla.GetLineIndentPosition(this._number);

  public int SelectionStartPosition => this.NativeScintilla.GetLineSelStartPosition(this._number);

  public int SelectionEndPosition => this.NativeScintilla.GetLineSelEndPosition(this._number);

  public List<Marker> GetMarkers()
  {
    List<Marker> markers = new List<Marker>();
    int markerMask = this.GetMarkerMask();
    int num = 1;
    for (int number = 0; number < 32 /*0x20*/; ++number)
    {
      if ((markerMask & num) != 0)
        markers.Add(new Marker(this.Scintilla, number));
      num += num;
    }
    return markers;
  }

  public Line FindNextMarker(Marker marker) => this.FindNextMarker(marker.Mask);

  public Line FindPreviousMarker(Marker marker) => this.FindPreviousMarker(marker.Mask);

  public Line DeleteMarker(int markerNumber)
  {
    this.NativeScintilla.MarkerDelete(this._number, markerNumber);
    return this;
  }

  public Line DeleteMarker(Marker marker)
  {
    this.NativeScintilla.MarkerDelete(this._number, marker.Number);
    return this;
  }

  public MarkerInstance AddMarker(int markerNumber)
  {
    return new MarkerInstance(this.Scintilla, new Marker(this.Scintilla, markerNumber), this.NativeScintilla.MarkerAdd(this._number, markerNumber));
  }

  public MarkerInstance AddMarker(Marker marker)
  {
    return new MarkerInstance(this.Scintilla, marker, this.NativeScintilla.MarkerAdd(this._number, marker.Number));
  }

  public Line AddMarkerSet(uint markerMask)
  {
    this.NativeScintilla.MarkerAddSet(this._number, markerMask);
    return this;
  }

  public Line AddMarkerSet(IEnumerable<Marker> markers)
  {
    this.AddMarkerSet(Utilities.GetMarkerMask(markers));
    return this;
  }

  public Line AddMarkerSet(IEnumerable<int> markers)
  {
    this.AddMarkerSet(Utilities.GetMarkerMask(markers));
    return this;
  }

  public Line DeleteMarkerSet(IEnumerable<int> markerNumbers)
  {
    foreach (int markerNumber in markerNumbers)
      this.NativeScintilla.MarkerDelete(this._number, markerNumber);
    return this;
  }

  public Line DeleteMarkerSet(IEnumerable<Marker> markers)
  {
    foreach (Marker marker in markers)
      this.NativeScintilla.MarkerDelete(this._number, marker.Number);
    return this;
  }

  public Line DeleteAllMarkers()
  {
    this.DeleteMarker(-1);
    return this;
  }

  public int GetMarkerMask() => this.NativeScintilla.MarkerGet(this._number);

  public Line FindNextMarker(uint markerMask)
  {
    int index = this.NativeScintilla.MarkerNext(this._number + 1, markerMask);
    return index < 0 ? (Line) null : this.Scintilla.Lines[index];
  }

  public Line FindNextMarker(IEnumerable<int> markers)
  {
    return this.FindNextMarker(Utilities.GetMarkerMask(markers));
  }

  public Line FindNextMarker(IEnumerable<Marker> markers)
  {
    return this.FindNextMarker(Utilities.GetMarkerMask(markers));
  }

  public Line FindPreviousMarker(uint markerMask)
  {
    int index = this.NativeScintilla.MarkerPrevious(this._number - 1, markerMask);
    return index < 0 ? (Line) null : this.Scintilla.Lines[index];
  }

  public Line FindPreviousMarker(IEnumerable<int> markers)
  {
    return this.FindPreviousMarker(Utilities.GetMarkerMask(markers));
  }

  public Line FindPreviousMarker(IEnumerable<Marker> markers)
  {
    return this.FindPreviousMarker(Utilities.GetMarkerMask(markers));
  }

  public int VisibleLineNumber => this.NativeScintilla.VisibleFromDocLine(this._number);

  public bool IsVisible
  {
    get => this.NativeScintilla.GetLineVisible(this._number);
    set
    {
      if (value)
        this.NativeScintilla.ShowLines(this._number, this._number);
      else
        this.NativeScintilla.HideLines(this._number, this._number);
    }
  }

  public void EnsureVisible() => this.NativeScintilla.EnsureVisible(this._number);

  public int FoldLevel
  {
    get => (int) this.NativeScintilla.GetFoldLevel(this._number) & 4095 /*0x0FFF*/;
    set
    {
      uint num = this.NativeScintilla.GetFoldLevel(this._number) & 12288U /*0x3000*/;
      this.NativeScintilla.SetFoldLevel(this._number, (uint) value | num);
    }
  }

  public bool IsFoldPoint
  {
    get
    {
      return ((int) this.NativeScintilla.GetFoldLevel(this._number) & 8192 /*0x2000*/) == 8192 /*0x2000*/;
    }
    set
    {
      if (value)
        this.NativeScintilla.SetFoldLevel(this._number, this.NativeScintilla.GetFoldLevel(this._number) | 8192U /*0x2000*/);
      else
        this.NativeScintilla.SetFoldLevel(this._number, this.NativeScintilla.GetFoldLevel(this._number) & 4294959103U);
    }
  }

  public bool IsFoldWhitespace
  {
    get
    {
      return ((int) this.NativeScintilla.GetFoldLevel(this._number) & 4096 /*0x1000*/) == 4096 /*0x1000*/;
    }
    set
    {
      if (value)
        this.NativeScintilla.SetFoldLevel(this._number, this.NativeScintilla.GetFoldLevel(this._number) | 4096U /*0x1000*/);
      else
        this.NativeScintilla.SetFoldLevel(this._number, this.NativeScintilla.GetFoldLevel(this._number) & 4294963199U);
    }
  }

  public void ToggleFoldExpanded() => this.NativeScintilla.ToggleFold(this._number);

  public bool FoldExpanded
  {
    get => this.NativeScintilla.GetFoldExpanded(this._number);
    set => this.NativeScintilla.SetFoldExpanded(this._number, value);
  }

  public Line GetLastFoldChild() => this.GetLastFoldChild(-1);

  public Line GetLastFoldChild(int level)
  {
    int lastChild = this.NativeScintilla.GetLastChild(this._number, level);
    return lastChild < 0 ? (Line) null : new Line(this.Scintilla, lastChild);
  }

  public Line FoldParent
  {
    get
    {
      int foldParent = this.NativeScintilla.GetFoldParent(this._number);
      return foldParent < 0 ? (Line) null : new Line(this.Scintilla, foldParent);
    }
  }

  public int WrapCount => this.NativeScintilla.WrapCount(this._number);

  public override bool Equals(object obj)
  {
    return obj is Line line && line.Scintilla == this.Scintilla && line._number == this._number;
  }

  public override string ToString() => "Line " + this._number.ToString();

  public override int GetHashCode() => base.GetHashCode();

  public Line Next => new Line(this.Scintilla, this._number + 1);

  public Line Previous => new Line(this.Scintilla, this._number - 1);
}
