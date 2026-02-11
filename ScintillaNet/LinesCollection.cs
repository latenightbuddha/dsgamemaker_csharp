using System;
using System.Collections;

#nullable disable
namespace ScintillaNet;

public class LinesCollection : TopLevelHelper, ICollection, IEnumerable
{
  protected internal LinesCollection(Scintilla scintilla)
    : base(scintilla)
  {
  }

  public Line this[int index] => new Line(this.Scintilla, index);

  public Line Current => this[this.Scintilla.Caret.LineNumber];

  public Line FirstVisible => this[this.NativeScintilla.GetFirstVisibleLine()];

  public int VisibleCount => this.NativeScintilla.LinesOnScreen();

  public Line[] VisibleLines
  {
    get
    {
      int firstVisibleLine = this.NativeScintilla.GetFirstVisibleLine();
      int num = firstVisibleLine + this.VisibleCount + 1;
      if (num > this.Count)
        num = this.Count;
      Line[] visibleLines = new Line[num - firstVisibleLine];
      for (int displayLine = firstVisibleLine; displayLine < num; ++displayLine)
        visibleLines[displayLine - firstVisibleLine] = this.FromVisibleLineNumber(displayLine);
      return visibleLines;
    }
  }

  public Line FromPosition(int position) => this[this.NativeScintilla.LineFromPosition(position)];

  public Line FromVisibleLineNumber(int displayLine)
  {
    return new Line(this.Scintilla, this.NativeScintilla.DocLineFromVisible(displayLine));
  }

  public Line GetMaxLineWithState()
  {
    int maxLineState = this.NativeScintilla.GetMaxLineState();
    return maxLineState < 0 ? (Line) null : this[maxLineState];
  }

  public void Hide(int startLine, int endLine)
  {
    this.NativeScintilla.HideLines(startLine, endLine);
  }

  private void Show(int startLine, int endLine)
  {
    this.NativeScintilla.ShowLines(startLine, endLine);
  }

  public void CopyTo(Array array, int index)
  {
    if (array == null)
      throw new ArgumentNullException(nameof (array));
    if (index < 0)
      throw new ArgumentOutOfRangeException(nameof (index));
    if (index >= array.Length)
      throw new ArgumentException("index is equal to or greater than the length of array.");
    int count = this.Count;
    if (count > array.Length - index)
      throw new ArgumentException("The number of elements in the source ICollection is greater than the available space from number to the end of the destination array.");
    for (int index1 = index; index1 < count; ++index1)
      array.SetValue((object) this[index1], index1);
  }

  public int Count => this.NativeScintilla.GetLineCount();

  public bool IsSynchronized => false;

  public object SyncRoot => throw new Exception("The method or operation is not implemented.");

  public IEnumerator GetEnumerator() => (IEnumerator) new LinesCollection.LinesEnumerator(this);

  private class LinesEnumerator : IEnumerator
  {
    private LinesCollection _lines;
    private int _index = -1;
    private int _count;

    public LinesEnumerator(LinesCollection lines)
    {
      this._lines = lines;
      this._count = lines.Count;
    }

    public object Current => (object) this._lines[this._index];

    public bool MoveNext() => ++this._index < this._count;

    public void Reset() => this._index = -1;
  }
}
