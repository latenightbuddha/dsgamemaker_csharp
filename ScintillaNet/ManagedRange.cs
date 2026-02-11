using System;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

public class ManagedRange : Range, IDisposable
{
  internal bool PendingDeletion;

  public virtual void Change(int newStart, int newEnd)
  {
    base.Start = newStart;
    base.End = newEnd;
  }

  public virtual bool IsPoint => false;

  public override int Start
  {
    get => base.Start;
    set => base.Start = value;
  }

  public override int End
  {
    get => base.End;
    set => base.End = value;
  }

  protected internal ManagedRange()
  {
  }

  protected internal ManagedRange(Range range)
    : this(range.Start, range.End, range.Scintilla)
  {
  }

  public ManagedRange(int start, int end, Scintilla scintilla)
    : base(start, end, scintilla)
  {
  }

  protected internal virtual void Paint(Graphics g)
  {
  }

  public override int GetHashCode() => base.GetHashCode();

  public override void Dispose()
  {
    if (this.IsDisposed)
      return;
    this.Scintilla.ManagedRanges.Remove(this);
    this.Scintilla = (Scintilla) null;
    this.IsDisposed = true;
  }
}
