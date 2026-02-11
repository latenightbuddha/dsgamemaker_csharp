using System;
using System.Runtime.InteropServices;

#nullable disable
namespace ScintillaNet;

public class Range : ScintillaHelperBase, IComparable
{
  private int _end;
  private int _start;

  public bool Collapsed => this._start == this._end;

  public virtual int End
  {
    get => this._end;
    set => this._end = value;
  }

  public virtual int Start
  {
    get => this._start;
    set => this._start = value;
  }

  protected internal Range()
    : base((Scintilla) null)
  {
  }

  public Range(int start, int end, Scintilla scintilla)
    : base(scintilla)
  {
    if (start < end)
    {
      this._start = start;
      this._end = end;
    }
    else
    {
      this._start = end;
      this._end = start;
    }
  }

  public int Length => this._end - this._start;

  public string Text
  {
    get
    {
      if (this.Start < 0 || this.End < 0 || this.Scintilla == null)
        return string.Empty;
      TextRange tr = new TextRange();
      try
      {
        tr.lpstrText = Marshal.AllocHGlobal(this.Length + 1);
        tr.chrg.cpMin = this._start;
        tr.chrg.cpMax = this._end;
        int textRange = this.NativeScintilla.GetTextRange(ref tr);
        return Utilities.IntPtrToString(this.Scintilla.Encoding, tr.lpstrText, textRange) ?? string.Empty;
      }
      finally
      {
        Marshal.FreeHGlobal(tr.lpstrText);
      }
    }
    set
    {
      this.NativeScintilla.SetTargetStart(this._start);
      this.NativeScintilla.SetTargetEnd(this._end);
      this.NativeScintilla.ReplaceTarget(-1, value);
    }
  }

  public byte[] StyledText
  {
    get
    {
      if (this.Start < 0 || this.End < 0 || this.Scintilla == null)
        return new byte[0];
      int length = this.Length * 2 + 2;
      TextRange tr = new TextRange();
      tr.lpstrText = Marshal.AllocHGlobal(length);
      tr.chrg.cpMin = this._start;
      tr.chrg.cpMax = this._end;
      this.NativeScintilla.GetStyledText(ref tr);
      byte[] destination = new byte[length];
      Marshal.Copy(tr.lpstrText, destination, 0, length);
      Marshal.FreeHGlobal(tr.lpstrText);
      return destination;
    }
  }

  public void Copy() => this.Copy(CopyFormat.Text);

  public void Copy(CopyFormat format)
  {
    if (format == CopyFormat.Text)
      this.NativeScintilla.CopyRange(this._start, this._end);
    else if (format == CopyFormat.Rtf)
      throw new NotImplementedException("Someday...");
  }

  public void Select() => this.NativeScintilla.SetSel(this._start, this._end);

  public void GotoStart() => this.NativeScintilla.GotoPos(this._start);

  public void GotoEnd() => this.NativeScintilla.GotoPos(this._end);

  public bool PositionInRange(int position) => position >= this._start && position <= this._end;

  public bool IntersectsWith(Range otherRange)
  {
    return otherRange.PositionInRange(this._start) | otherRange.PositionInRange(this._end) | this.PositionInRange(otherRange.Start) | this.PositionInRange(otherRange.End);
  }

  public void SetStyle(string styleName)
  {
    this.SetStyle(this.Scintilla.Lexing.StyleNameMap[styleName]);
  }

  public void SetStyle(byte styleMask, string styleName)
  {
    this.SetStyle(styleMask, this.Scintilla.Lexing.StyleNameMap[styleName]);
  }

  public void SetStyle(int style) => this.SetStyle(byte.MaxValue, style);

  public void SetStyle(byte styleMask, int style)
  {
    this.NativeScintilla.StartStyling(this._start, (int) styleMask);
    this.NativeScintilla.SetStyling(this.Length, style);
  }

  public void SetIndicator(int indicator)
  {
    this.NativeScintilla.SetIndicatorCurrent(indicator);
    this.NativeScintilla.IndicatorFillRange(this._start, this.Length);
  }

  public void SetIndicator(int indicator, int value)
  {
    this.NativeScintilla.SetIndicatorValue(value);
    this.NativeScintilla.SetIndicatorCurrent(indicator);
    this.NativeScintilla.IndicatorFillRange(this._start, this.Length);
  }

  public void ClearIndicator(int indicator)
  {
    this.NativeScintilla.SetIndicatorCurrent(indicator);
    this.NativeScintilla.IndicatorClearRange(this._start, this.Length);
  }

  public void ClearIndicator(Indicator indicator)
  {
    this.NativeScintilla.SetIndicatorCurrent(indicator.Number);
    this.NativeScintilla.IndicatorClearRange(this._start, this.Length);
  }

  public override bool Equals(object obj)
  {
    return obj is Range range && range._start == this._start && range._end == this._end;
  }

  public override int GetHashCode() => base.GetHashCode();

  public Line StartingLine => new Line(this.Scintilla, this.startingLine);

  private int startingLine => this.NativeScintilla.LineFromPosition(this._start);

  private int endingLine => this.NativeScintilla.LineFromPosition(this._end);

  public Line EndingLine => new Line(this.Scintilla, this.endingLine);

  public bool IsMultiLine => !this.StartingLine.Equals((object) this.EndingLine);

  public void HideLines() => this.NativeScintilla.HideLines(this.startingLine, this.endingLine);

  public void ShowLines() => this.NativeScintilla.ShowLines(this.startingLine, this.endingLine);

  public void SplitLines(int pixelWidth)
  {
    this.NativeScintilla.SetTargetStart(this._start);
    this.NativeScintilla.SetTargetEnd(this._end);
    this.NativeScintilla.LinesSplit(pixelWidth);
  }

  public void SplitLines() => this.SplitLines(0);

  public void JoinLines()
  {
    this.NativeScintilla.SetTargetStart(this._start);
    this.NativeScintilla.SetTargetEnd(this._end);
    this.NativeScintilla.LinesJoin();
  }

  public void Colorize() => this.NativeScintilla.Colourise(this._start, this._end);

  public void StripTrailingSpaces()
  {
    this.NativeScintilla.BeginUndoAction();
    for (int startingLine = this.startingLine; startingLine < this.endingLine; ++startingLine)
    {
      int num = this.NativeScintilla.PositionFromLine(startingLine);
      int lineEndPosition = this.NativeScintilla.GetLineEndPosition(startingLine);
      int position = lineEndPosition - 1;
      for (char charAt = this.NativeScintilla.GetCharAt(position); position >= num && (charAt == ' ' || charAt == '\t'); charAt = this.NativeScintilla.GetCharAt(position))
        --position;
      if (position == num - 1)
      {
        for (char charAt = this.NativeScintilla.GetCharAt(position + 1); position < lineEndPosition && charAt == '\t'; charAt = this.NativeScintilla.GetCharAt(position + 1))
          ++position;
      }
      if (position < lineEndPosition - 1)
      {
        this.NativeScintilla.SetTargetStart(position + 1);
        this.NativeScintilla.SetTargetEnd(lineEndPosition);
        this.NativeScintilla.ReplaceTarget(0, string.Empty);
      }
    }
    this.NativeScintilla.EndUndoAction();
  }

  public void ExpandAllFolds()
  {
    for (int startingLine = this.startingLine; startingLine < this.endingLine; ++startingLine)
    {
      this.NativeScintilla.SetFoldExpanded(startingLine, true);
      this.NativeScintilla.ShowLines(startingLine + 1, startingLine + 1);
    }
  }

  public void CollapseAllFolds()
  {
    for (int startingLine = this.startingLine; startingLine < this.endingLine; ++startingLine)
    {
      int lastChild = this.NativeScintilla.GetLastChild(startingLine, -1);
      this.NativeScintilla.SetFoldExpanded(startingLine, false);
      this.NativeScintilla.HideLines(startingLine + 1, lastChild);
    }
  }

  public override string ToString()
  {
    return $"{{Start={(object) this._start}, End={(object) this._end}, Length={(object) this.Length}}}";
  }

  public int CompareTo(object otherObj)
  {
    if (!(otherObj is Range range) || range._start < this._start)
      return 1;
    if (range._start > this._start)
      return -1;
    if (range._end < this._end)
      return 1;
    return range._end > this._end ? -1 : 0;
  }
}
