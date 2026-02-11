using System.Drawing;

#nullable disable
namespace ScintillaNet;

public class SnippetLinkEnd : ManagedRange
{
  internal SnippetLinkEnd(int start, Scintilla scintilla)
    : base(start, start, scintilla)
  {
  }

  public override void Change(int newStart, int newEnd)
  {
    this.Invalidate();
    base.Change(newStart, newEnd);
  }

  public void Invalidate()
  {
    if (this.Scintilla == null || this.Start <= 0)
      return;
    INativeScintilla scintilla = (INativeScintilla) this.Scintilla;
    this.Scintilla.Invalidate(new Rectangle(scintilla.PointXFromPosition(this.Start) - 2, scintilla.PointYFromPosition(this.Start) + scintilla.TextHeight(0) - 2, 5, 5));
  }

  public override bool IsPoint => true;

  protected internal override void Paint(Graphics g)
  {
    base.Paint(g);
    if (this.IsDisposed)
      return;
    INativeScintilla scintilla = (INativeScintilla) this.Scintilla;
    int x = scintilla.PointXFromPosition(this.Start);
    int y = scintilla.PointYFromPosition(this.Start) + scintilla.TextHeight(0) - 2;
    g.FillPolygon(Brushes.Lime, new Point[3]
    {
      new Point(x - 2, y + 4),
      new Point(x, y),
      new Point(x + 2, y + 4)
    });
    g.DrawPolygon(Pens.Green, new Point[3]
    {
      new Point(x - 2, y + 4),
      new Point(x, y),
      new Point(x + 2, y + 4)
    });
  }

  public override void Dispose()
  {
    if (this.IsDisposed)
      return;
    this.Invalidate();
    base.Dispose();
  }
}
