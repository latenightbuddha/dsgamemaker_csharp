using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Scrolling : TopLevelHelper
{
  internal Scrolling(Scintilla scintilla)
    : base(scintilla)
  {
  }

  public ScrollBars ScrollBars
  {
    get
    {
      bool hscrollBar = this.NativeScintilla.GetHScrollBar();
      bool vscrollBar = this.NativeScintilla.GetVScrollBar();
      if (hscrollBar && vscrollBar)
        return ScrollBars.Both;
      if (hscrollBar)
        return ScrollBars.Horizontal;
      return vscrollBar ? ScrollBars.Vertical : ScrollBars.None;
    }
    set
    {
      this.NativeScintilla.SetHScrollBar((value & ScrollBars.Horizontal) > ScrollBars.None);
      this.NativeScintilla.SetVScrollBar((value & ScrollBars.Vertical) > ScrollBars.None);
    }
  }

  private bool ShouldSerializeScrollBars() => this.ScrollBars != ScrollBars.Both;

  private void ResetScrollBars() => this.ScrollBars = ScrollBars.Both;

  public int XOffset
  {
    get => this.NativeScintilla.GetXOffset();
    set => this.NativeScintilla.SetXOffset(value);
  }

  private bool ShouldSerializeXOffset() => this.XOffset != 0;

  internal void ResetXOffset() => this.XOffset = 0;

  public int HorizontalWidth
  {
    get => this.NativeScintilla.GetScrollWidth();
    set => this.NativeScintilla.SetScrollWidth(value);
  }

  private bool ShouldSerializeHorizontalWidth() => this.HorizontalWidth != 2000;

  internal void ResetHorizontalWidth() => this.HorizontalWidth = 2000;

  public bool EndAtLastLine
  {
    get => this.NativeScintilla.GetEndAtLastLine();
    set => this.NativeScintilla.SetEndAtLastLine(value);
  }

  private bool ShouldSerializeEndAtLastLine() => !this.EndAtLastLine;

  internal void ResetEndAtLastLine() => this.EndAtLastLine = true;

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeEndAtLastLine() || this.ShouldSerializeHorizontalWidth() || this.ShouldSerializeScrollBars() || this.ShouldSerializeXOffset();
  }

  public void ScrollBy(int columns, int lines) => this.NativeScintilla.LineScroll(columns, lines);

  public void ScrollToCaret() => this.NativeScintilla.ScrollCaret();
}
