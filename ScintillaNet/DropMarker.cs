using System;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

public class DropMarker : ManagedRange
{
  private int _topOffset;
  private Guid _key = Guid.NewGuid();

  public int TopOffset
  {
    get => this._topOffset;
    set => this._topOffset = value;
  }

  public Guid Key
  {
    get => this._key;
    set => this._key = value;
  }

  internal DropMarker(int start, int end, int topOffset, Scintilla scintilla)
    : base(start, end, scintilla)
  {
    this.Start = start;
    this.End = end;
    this._topOffset = topOffset;
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
    this.Scintilla.Invalidate(this.GetClientRectangle());
  }

  public override bool IsPoint => this.Start == this.End;

  protected internal override void Paint(Graphics g)
  {
    base.Paint(g);
    if (this.IsDisposed)
      return;
    int x = this.NativeScintilla.PointXFromPosition(this.Start);
    int y = this.NativeScintilla.PointYFromPosition(this.Start) + this.NativeScintilla.TextHeight(0) - 2;
    g.FillPolygon(Brushes.Red, new Point[3]
    {
      new Point(x - 2, y + 4),
      new Point(x, y),
      new Point(x + 2, y + 4)
    });
    g.DrawPolygon(Pens.DarkRed, new Point[3]
    {
      new Point(x - 2, y + 4),
      new Point(x, y),
      new Point(x + 2, y + 4)
    });
  }

  public bool Collect() => this.Collect(true);

  internal bool Collect(bool dispose)
  {
    DropMarkerCollectEventArgs e = new DropMarkerCollectEventArgs(this);
    this.Scintilla.OnDropMarkerCollect(e);
    if (e.Cancel)
      return false;
    this.GotoStart();
    if (dispose)
      this.Dispose();
    return true;
  }

  public override void Dispose()
  {
    if (this.IsDisposed)
      return;
    this.Scintilla.DropMarkers.AllDocumentDropMarkers.Remove(this);
    this.Invalidate();
    base.Dispose();
  }

  public Rectangle GetClientRectangle()
  {
    return new Rectangle(this.NativeScintilla.PointXFromPosition(this.Start) - 2, this.NativeScintilla.PointYFromPosition(this.Start) + this.NativeScintilla.TextHeight(0) - 2, 5, 5);
  }

  public override bool Equals(object obj)
  {
    return this.IsSameHelperFamily(obj) && ((DropMarker) obj).Key == this.Key;
  }

  public override int GetHashCode() => base.GetHashCode();
}
