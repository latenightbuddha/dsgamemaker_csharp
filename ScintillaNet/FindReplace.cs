using ScintillaNet.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class FindReplace : TopLevelHelper
{
  private string _lastFindString;
  private Regex _lastFindRegex;
  private FindReplaceDialog _window;
  private IncrementalSearcher _incrementalSearcher;
  private SearchFlags _flags;
  private Marker _marker;
  private Indicator _indicator;
  private List<Range> _lastReplaceAllMatches = new List<Range>();
  private string _lastReplaceAllReplaceString = "";
  private Range _lastReplaceAllRangeToSearch;
  private int _lastReplaceAllOffset;

  internal FindReplace(Scintilla scintilla)
    : base(scintilla)
  {
    this._marker = scintilla.Markers[10];
    this._marker.SetSymbolInternal(MarkerSymbol.Arrows);
    this._indicator = scintilla.Indicators[16 /*0x10*/];
    this._indicator.Color = Color.Purple;
    this._indicator.Style = IndicatorStyle.RoundBox;
    this._window = new FindReplaceDialog();
    this._window.Scintilla = scintilla;
    this._incrementalSearcher = new IncrementalSearcher();
    this._incrementalSearcher.Scintilla = scintilla;
    this._incrementalSearcher.Visible = false;
    scintilla.Controls.Add((Control) this._incrementalSearcher);
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeFlags() || this.ShouldSerializeIndicator() || this.ShouldSerializeMarker();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string LastFindString
  {
    get => this._lastFindString;
    set
    {
      this._lastFindString = value;
      this._lastFindRegex = (Regex) null;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Regex LastFindRegex
  {
    get => this._lastFindRegex;
    set
    {
      this._lastFindRegex = value;
      this._lastFindString = (string) null;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public FindReplaceDialog Window
  {
    get => this._window;
    set => this._window = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public IncrementalSearcher IncrementalSearcher
  {
    get => this._incrementalSearcher;
    set => this._incrementalSearcher = value;
  }

  [Editor(typeof (FlagEnumUIEditor), typeof (UITypeEditor))]
  public SearchFlags Flags
  {
    get => this._flags;
    set => this._flags = value;
  }

  private bool ShouldSerializeFlags() => this._flags != SearchFlags.Empty;

  private void ResetFlags() => this._flags = SearchFlags.Empty;

  public Marker Marker
  {
    get => this._marker;
    set => this._marker = value;
  }

  private bool ShouldSerializeMarker()
  {
    return this._marker.Number != 10 || this._marker.ForeColor != Color.White || this._marker.BackColor != Color.Black || this._marker.Symbol != MarkerSymbol.Arrows;
  }

  private void ResetMarker()
  {
    this._marker.Reset();
    this._marker.Number = 10;
  }

  public Indicator Indicator
  {
    get => this._indicator;
    set => this._indicator = value;
  }

  private bool ShouldSerializeIndicator()
  {
    return this._indicator.Number != 16 /*0x10*/ || this._indicator.Color != Color.Purple || this._indicator.IsDrawnUnder;
  }

  private void ResetIndicator() => this._indicator.Reset();

  public Range Find(string searchString)
  {
    return this.Find(0, this.NativeScintilla.GetTextLength(), searchString, this._flags);
  }

  public Range Find(string searchString, bool searchUp)
  {
    return searchUp ? this.Find(this.NativeScintilla.GetTextLength(), 0, searchString, this._flags) : this.Find(0, this.NativeScintilla.GetTextLength(), searchString, this._flags);
  }

  public Range Find(string searchString, SearchFlags searchflags)
  {
    return this.Find(0, this.NativeScintilla.GetTextLength(), searchString, searchflags);
  }

  public Range Find(string searchString, SearchFlags searchflags, bool searchUp)
  {
    return searchUp ? this.Find(this.NativeScintilla.GetTextLength(), 0, searchString, searchflags) : this.Find(0, this.NativeScintilla.GetTextLength(), searchString, searchflags);
  }

  public Range Find(Range rangeToSearch, string searchString)
  {
    return this.Find(rangeToSearch.Start, rangeToSearch.End, searchString, this._flags);
  }

  public Range Find(Range rangeToSearch, string searchString, bool searchUp)
  {
    return searchUp ? this.Find(rangeToSearch.End, rangeToSearch.Start, searchString, this._flags) : this.Find(rangeToSearch.Start, rangeToSearch.End, searchString, this._flags);
  }

  public Range Find(Range rangeToSearch, string searchString, SearchFlags searchflags)
  {
    return this.Find(rangeToSearch.Start, rangeToSearch.End, searchString, searchflags);
  }

  public Range Find(
    Range rangeToSearch,
    string searchString,
    SearchFlags searchflags,
    bool searchUp)
  {
    return searchUp ? this.Find(rangeToSearch.End, rangeToSearch.Start, searchString, searchflags) : this.Find(rangeToSearch.Start, rangeToSearch.End, searchString, searchflags);
  }

  public unsafe Range Find(int startPos, int endPos, string searchString, SearchFlags flags)
  {
    TextToFind ttf = new TextToFind();
    ttf.chrg.cpMin = startPos;
    ttf.chrg.cpMax = endPos;
    fixed (byte* numPtr = this.Scintilla.Encoding.GetBytes(searchString))
    {
      ttf.lpstrText = (IntPtr) (void*) numPtr;
      int text = this.NativeScintilla.FindText((int) flags, ref ttf);
      return text >= 0 ? new Range(text, text + searchString.Length, this.Scintilla) : (Range) null;
    }
  }

  public Range Find(Regex findExpression)
  {
    return this.Find(new Range(0, this.NativeScintilla.GetTextLength(), this.Scintilla), findExpression, false);
  }

  public Range Find(Regex findExpression, bool searchUp)
  {
    return this.Find(new Range(0, this.NativeScintilla.GetTextLength(), this.Scintilla), findExpression, searchUp);
  }

  public Range Find(int startPos, int endPos, Regex findExpression)
  {
    return this.Find(new Range(startPos, endPos, this.Scintilla), findExpression, false);
  }

  public Range Find(int startPos, int endPos, Regex findExpression, bool searchUp)
  {
    return this.Find(new Range(startPos, endPos, this.Scintilla), findExpression, searchUp);
  }

  public Range Find(Range r, Regex findExpression) => this.Find(r, findExpression, false);

  public Range Find(Range r, Regex findExpression, bool searchUp)
  {
    Match match = findExpression.Match(r.Text);
    if (!match.Success)
      return (Range) null;
    if (!searchUp)
      return new Range(r.Start + match.Index, r.Start + match.Index + match.Length, this.Scintilla);
    Range range = (Range) null;
    for (; match.Success; match = match.NextMatch())
      range = new Range(r.Start + match.Index, r.Start + match.Index + match.Length, this.Scintilla);
    return range;
  }

  public List<Range> FindAll(string searchString) => this.FindAll(searchString, this._flags);

  public List<Range> FindAll(string searchString, SearchFlags flags)
  {
    return this.FindAll(0, this.NativeScintilla.GetTextLength(), searchString, flags);
  }

  public List<Range> FindAll(Range rangeToSearch, string searchString)
  {
    return this.FindAll(rangeToSearch.Start, rangeToSearch.End, searchString, this._flags);
  }

  public List<Range> FindAll(Range rangeToSearch, string searchString, SearchFlags flags)
  {
    return this.FindAll(rangeToSearch.Start, rangeToSearch.End, searchString, this._flags);
  }

  public List<Range> FindAll(int startPos, int endPos, string searchString, SearchFlags flags)
  {
    List<Range> all = new List<Range>();
    while (true)
    {
      Range range = this.Find(startPos, endPos, searchString, flags);
      if (range != null)
      {
        all.Add(range);
        startPos = range.End;
      }
      else
        break;
    }
    return all;
  }

  public List<Range> FindAll(Regex findExpression)
  {
    return this.FindAll(0, this.NativeScintilla.GetTextLength(), findExpression);
  }

  public List<Range> FindAll(int startPos, int endPos, Regex findExpression)
  {
    return this.FindAll(new Range(startPos, endPos, this.Scintilla), findExpression);
  }

  public List<Range> FindAll(Range rangeToSearch, Regex findExpression)
  {
    List<Range> all = new List<Range>();
    while (true)
    {
      Range range = this.Find(rangeToSearch, findExpression);
      if (range != null)
      {
        all.Add(range);
        rangeToSearch = new Range(range.End, rangeToSearch.End, this.Scintilla);
      }
      else
        break;
    }
    return all;
  }

  public Range FindNext(string searchString) => this.FindNext(searchString, true, this._flags);

  public Range FindNext(string searchString, bool wrap)
  {
    return this.FindNext(searchString, wrap, this._flags);
  }

  public Range FindNext(string searchString, SearchFlags flags)
  {
    return this.FindNext(searchString, true, flags);
  }

  public Range FindNext(string searchString, bool wrap, SearchFlags flags)
  {
    Range next = this.Find(this.NativeScintilla.GetCurrentPos(), this.NativeScintilla.GetTextLength(), searchString, flags);
    if (next != null)
      return next;
    return wrap ? this.Find(0, this.NativeScintilla.GetCurrentPos(), searchString, flags) : (Range) null;
  }

  public Range FindNext(string searchString, bool wrap, SearchFlags flags, Range searchRange)
  {
    int position = this.Scintilla.Caret.Position;
    if (!searchRange.PositionInRange(position))
      return this.Find(searchRange.Start, searchRange.End, searchString, flags);
    Range next = this.Find(position, searchRange.End, searchString, flags);
    if (next != null)
      return next;
    return wrap ? this.Find(searchRange.Start, position, searchString, flags) : (Range) null;
  }

  public Range FindNext(Regex findExpression) => this.FindNext(findExpression, false);

  public Range FindNext(Regex findExpression, bool wrap)
  {
    Range next = this.Find(this.NativeScintilla.GetCurrentPos(), this.NativeScintilla.GetTextLength(), findExpression);
    if (next != null)
      return next;
    return wrap ? this.Find(0, this.NativeScintilla.GetCurrentPos(), findExpression) : (Range) null;
  }

  public Range FindNext(Regex findExpression, bool wrap, Range searchRange)
  {
    int position = this.Scintilla.Caret.Position;
    if (!searchRange.PositionInRange(position))
      return this.Find(searchRange.Start, searchRange.End, findExpression, false);
    Range next = this.Find(position, searchRange.End, findExpression);
    if (next != null)
      return next;
    return wrap ? this.Find(searchRange.Start, position, findExpression) : (Range) null;
  }

  public Range ReplaceNext(string searchString, string replaceString)
  {
    return this.ReplaceNext(searchString, replaceString, true, this._flags);
  }

  public Range ReplaceNext(string searchString, string replaceString, bool wrap)
  {
    return this.ReplaceNext(searchString, replaceString, wrap, this._flags);
  }

  public Range ReplaceNext(string searchString, string replaceString, SearchFlags flags)
  {
    return this.ReplaceNext(searchString, replaceString, true, flags);
  }

  public Range ReplaceNext(
    string searchString,
    string replaceString,
    bool wrap,
    SearchFlags flags)
  {
    Range next = this.FindNext(searchString, wrap, flags);
    if (next != null)
    {
      next.Text = replaceString;
      next.End = next.Start + replaceString.Length;
    }
    return next;
  }

  public Range FindPrevious(string searchString)
  {
    return this.FindPrevious(searchString, true, this._flags);
  }

  public Range FindPrevious(string searchString, bool wrap)
  {
    return this.FindPrevious(searchString, wrap, this._flags);
  }

  public Range FindPrevious(string searchString, SearchFlags flags)
  {
    return this.FindPrevious(searchString, true, flags);
  }

  public Range FindPrevious(string searchString, bool wrap, SearchFlags flags)
  {
    Range previous = this.Find(this.NativeScintilla.GetAnchor(), 0, searchString, flags);
    if (previous != null)
      return previous;
    return wrap ? this.Find(this.NativeScintilla.GetTextLength(), this.NativeScintilla.GetCurrentPos(), searchString, flags) : (Range) null;
  }

  public Range FindPrevious(string searchString, bool wrap, SearchFlags flags, Range searchRange)
  {
    int position = this.Scintilla.Caret.Position;
    if (!searchRange.PositionInRange(position))
      return this.Find(searchRange.End, searchRange.Start, searchString, flags);
    int num = this.Scintilla.Caret.Anchor;
    if (!searchRange.PositionInRange(num))
      num = position;
    Range previous = this.Find(num, searchRange.Start, searchString, flags);
    if (previous != null)
      return previous;
    return wrap ? this.Find(searchRange.End, num, searchString, flags) : (Range) null;
  }

  public Range FindPrevious(Regex findExpression) => this.FindPrevious(findExpression, false);

  public Range FindPrevious(Regex findExpression, bool wrap)
  {
    Range previous = this.Find(0, this.NativeScintilla.GetAnchor(), findExpression, true);
    if (previous != null)
      return previous;
    return wrap ? this.Find(this.NativeScintilla.GetCurrentPos(), this.NativeScintilla.GetTextLength(), findExpression, true) : (Range) null;
  }

  public Range FindPrevious(Regex findExpression, bool wrap, Range searchRange)
  {
    int position = this.Scintilla.Caret.Position;
    if (!searchRange.PositionInRange(position))
      return this.Find(searchRange.Start, searchRange.End, findExpression, true);
    int num = this.Scintilla.Caret.Anchor;
    if (!searchRange.PositionInRange(num))
      num = position;
    Range previous = this.Find(searchRange.Start, num, findExpression, true);
    if (previous != null)
      return previous;
    return wrap ? this.Find(num, searchRange.End, findExpression, true) : (Range) null;
  }

  public Range ReplacePrevious(string searchString, string replaceString)
  {
    return this.ReplacePrevious(searchString, replaceString, true, this._flags);
  }

  public Range ReplacePrevious(string searchString, string replaceString, bool wrap)
  {
    return this.ReplacePrevious(searchString, replaceString, wrap, this._flags);
  }

  public Range ReplacePrevious(string searchString, string replaceString, SearchFlags flags)
  {
    return this.ReplacePrevious(searchString, replaceString, true, flags);
  }

  public Range ReplacePrevious(
    string searchString,
    string replaceString,
    bool wrap,
    SearchFlags flags)
  {
    Range previous = this.FindPrevious(searchString, wrap, flags);
    if (previous != null)
    {
      previous.Text = replaceString;
      previous.End = previous.Start + replaceString.Length;
    }
    return previous;
  }

  public List<Range> ReplaceAll(string searchString, string replaceString)
  {
    return this.ReplaceAll(searchString, replaceString, this._flags);
  }

  public List<Range> ReplaceAll(string searchString, string replaceString, SearchFlags flags)
  {
    return this.ReplaceAll(0, this.NativeScintilla.GetTextLength(), searchString, replaceString, flags);
  }

  public List<Range> ReplaceAll(Range rangeToSearch, string searchString, string replaceString)
  {
    return this.ReplaceAll(rangeToSearch.Start, rangeToSearch.End, searchString, replaceString, this._flags);
  }

  public List<Range> ReplaceAll(
    Range rangeToSearch,
    string searchString,
    string replaceString,
    SearchFlags flags)
  {
    return this.ReplaceAll(rangeToSearch.Start, rangeToSearch.End, searchString, replaceString, this._flags);
  }

  public List<Range> ReplaceAll(
    int startPos,
    int endPos,
    string searchString,
    string replaceString,
    SearchFlags flags)
  {
    List<Range> rangeList = new List<Range>();
    this.Scintilla.UndoRedo.BeginUndoAction();
    int num = replaceString.Length - searchString.Length;
    while (true)
    {
      Range range = this.Find(startPos, endPos, searchString, flags);
      if (range != null)
      {
        range.Text = replaceString;
        range.End = startPos = range.Start + replaceString.Length;
        endPos += num;
        rangeList.Add(range);
      }
      else
        break;
    }
    this.Scintilla.UndoRedo.EndUndoAction();
    return rangeList;
  }

  public List<Range> ReplaceAll(Regex findExpression, string replaceString)
  {
    return this.ReplaceAll(0, this.NativeScintilla.GetTextLength(), findExpression, replaceString);
  }

  public List<Range> ReplaceAll(
    int startPos,
    int endPos,
    Regex findExpression,
    string replaceString)
  {
    return this.ReplaceAll(new Range(startPos, endPos, this.Scintilla), findExpression, replaceString);
  }

  public List<Range> ReplaceAll(Range rangeToSearch, Regex findExpression, string replaceString)
  {
    this.Scintilla.UndoRedo.BeginUndoAction();
    this._lastReplaceAllMatches = new List<Range>();
    this._lastReplaceAllReplaceString = replaceString;
    this._lastReplaceAllRangeToSearch = rangeToSearch;
    this._lastReplaceAllOffset = 0;
    findExpression.Replace(rangeToSearch.Text, new MatchEvaluator(this.replaceAllEvaluator));
    this.Scintilla.UndoRedo.EndUndoAction();
    List<Range> replaceAllMatches = this._lastReplaceAllMatches;
    this._lastReplaceAllMatches = (List<Range>) null;
    this._lastReplaceAllReplaceString = (string) null;
    this._lastReplaceAllRangeToSearch = (Range) null;
    return replaceAllMatches;
  }

  private string replaceAllEvaluator(Match m)
  {
    string str = m.Result(this._lastReplaceAllReplaceString);
    int start = this._lastReplaceAllRangeToSearch.Start + m.Index + this._lastReplaceAllOffset;
    int end = start + m.Length;
    Range range = new Range(start, end, this.Scintilla);
    this._lastReplaceAllMatches.Add(range);
    range.Text = str;
    this._lastReplaceAllOffset += str.Length - m.Value.Length;
    return str;
  }

  public void ShowFind()
  {
    if (!this._window.Visible)
      this._window.Show((IWin32Window) this.Scintilla.FindForm());
    this._window.tabAll.SelectedTab = this._window.tabAll.TabPages["tpgFind"];
    Range range = this.Scintilla.Selection.Range;
    if (range.IsMultiLine)
      this._window.chkSearchSelectionF.Checked = true;
    else if (range.Length > 0)
      this._window.cboFindF.Text = range.Text;
    this._window.cboFindF.Select();
    this._window.cboFindF.SelectAll();
  }

  public void ShowReplace()
  {
    if (!this._window.Visible)
      this._window.Show((IWin32Window) this.Scintilla.FindForm());
    this._window.tabAll.SelectedTab = this._window.tabAll.TabPages["tpgReplace"];
    Range range = this.Scintilla.Selection.Range;
    if (range.IsMultiLine)
      this._window.chkSearchSelectionR.Checked = true;
    else if (range.Length > 0)
      this._window.cboFindR.Text = range.Text;
    this._window.cboFindR.Select();
    this._window.cboFindR.SelectAll();
  }

  public List<MarkerInstance> MarkAll(IList<Range> foundRanges)
  {
    List<MarkerInstance> markerInstanceList = new List<MarkerInstance>();
    Line line = new Line(this.Scintilla, -1);
    foreach (Range foundRange in (IEnumerable<Range>) foundRanges)
    {
      Line startingLine = foundRange.StartingLine;
      if (startingLine.Number > line.Number)
        markerInstanceList.Add(this.Marker.AddInstanceTo(foundRange.StartingLine));
      line = startingLine;
    }
    return markerInstanceList;
  }

  public void HighlightAll(IList<Range> foundRanges)
  {
    foreach (Range foundRange in (IEnumerable<Range>) foundRanges)
      foundRange.SetIndicator(this.Indicator.Number);
  }

  public void ClearAllHighlights()
  {
    Indicator indicator = this.Scintilla.FindReplace.Indicator;
    foreach (Range range in indicator.SearchAll())
      range.ClearIndicator(indicator);
  }

  public void IncrementalSearch() => this._incrementalSearcher.Show();
}
