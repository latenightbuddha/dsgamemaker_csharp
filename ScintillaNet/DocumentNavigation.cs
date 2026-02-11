using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class DocumentNavigation : TopLevelHelper
{
  private bool _supressNext;
  private System.Timers.Timer t;
  private bool _isEnabled = true;
  private int _maxHistorySize = 50;
  public DocumentNavigation.FakeStack _backwardStack = new DocumentNavigation.FakeStack();
  public DocumentNavigation.FakeStack _forewardStack = new DocumentNavigation.FakeStack();
  private int _navigationPointTimeout = 200;

  internal DocumentNavigation(Scintilla scintilla)
    : base(scintilla)
  {
    this.t = new System.Timers.Timer();
    this.t.Interval = this._navigationPointTimeout;
    this.t.Elapsed += new System.Timers.ElapsedEventHandler(this.t_Tick);
    scintilla.SelectionChanged += new EventHandler(this.scintilla_SelectionChanged);
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeIsEnabled() || this.ShouldSerializeMaxHistorySize();
  }

  public void Reset()
  {
    this._backwardStack.Clear();
    this._forewardStack.Clear();
    this.ResetIsEnabled();
    this.ResetMaxHistorySize();
  }

  private void t_Tick(object sender, EventArgs e)
  {
    this.t.Enabled = false;
    int currentPos = this.NativeScintilla.GetCurrentPos();
    if (this._forewardStack.Count != 0 && this._forewardStack.Current.Start == currentPos || this._backwardStack.Count != 0 && this._backwardStack.Current.Start == currentPos)
      return;
    this._backwardStack.Push(this.newRange(currentPos));
  }

  private void scintilla_SelectionChanged(object sender, EventArgs e)
  {
    if (!this._isEnabled)
      return;
    if (!this._supressNext)
    {
      this.t.Enabled = false;
      this.t.Enabled = true;
    }
    else
      this._supressNext = false;
  }

  public bool IsEnabled
  {
    get => this._isEnabled;
    set => this._isEnabled = value;
  }

  private bool ShouldSerializeIsEnabled() => !this._isEnabled;

  private void ResetIsEnabled() => this._isEnabled = true;

  public int MaxHistorySize
  {
    get => this._maxHistorySize;
    set
    {
      this._maxHistorySize = value;
      this._backwardStack.MaxCount = value;
      this._forewardStack.MaxCount = value;
    }
  }

  private bool ShouldSerializeMaxHistorySize() => this._maxHistorySize != 50;

  private void ResetMaxHistorySize() => this._maxHistorySize = 50;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public DocumentNavigation.FakeStack BackwardStack
  {
    get => this._backwardStack;
    set => this._backwardStack = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public DocumentNavigation.FakeStack ForewardStack
  {
    get => this._forewardStack;
    set => this._forewardStack = value;
  }

  public int NavigationPointTimeout
  {
    get => this._navigationPointTimeout;
    set => this._navigationPointTimeout = value;
  }

  private bool ShouldSerializeNavigationPointTimeout() => this._navigationPointTimeout != 200;

  private void ResetNavigationPointTimeout() => this._navigationPointTimeout = 200;

  public void NavigateBackward()
  {
    if (this._backwardStack.Count == 0)
      return;
    int position = this.Scintilla.Caret.Position;
    if (position == this._backwardStack.Current.Start && this._backwardStack.Count == 1)
      return;
    int start = this._backwardStack.Pop().Start;
    if (start != position)
    {
      this._forewardStack.Push(this.newRange(position));
      this.Scintilla.Caret.Goto(start);
    }
    else
    {
      this._forewardStack.Push(this.newRange(start));
      this.Scintilla.Caret.Goto(this._backwardStack.Current.Start);
    }
    this._supressNext = true;
  }

  public void NavigateForward()
  {
    if (!this.CanNavigateForward)
      return;
    int start = this._forewardStack.Pop().Start;
    this._backwardStack.Push(this.newRange(start));
    this.Scintilla.Caret.Goto(start);
    this._supressNext = true;
  }

  [Browsable(false)]
  public bool CanNavigateBackward
  {
    get
    {
      return this._backwardStack.Count != 0 && (this.NativeScintilla.GetCurrentPos() != this._backwardStack.Current.Start || this._backwardStack.Count != 1);
    }
  }

  [Browsable(false)]
  public bool CanNavigateForward => this._forewardStack.Count > 0;

  private DocumentNavigation.NavigationPont newRange(int pos)
  {
    DocumentNavigation.NavigationPont navigationPont = new DocumentNavigation.NavigationPont(pos, this.Scintilla);
    this.Scintilla.ManagedRanges.Add((ManagedRange) navigationPont);
    return navigationPont;
  }

  public class FakeStack : List<DocumentNavigation.NavigationPont>
  {
    private int _maxCount = 50;

    public int MaxCount
    {
      get => this._maxCount;
      set => this._maxCount = value;
    }

    public DocumentNavigation.NavigationPont Pop()
    {
      DocumentNavigation.NavigationPont navigationPont = this[this.Count - 1];
      this.RemoveAt(this.Count - 1);
      return navigationPont;
    }

    public void Push(DocumentNavigation.NavigationPont value)
    {
      this.Add(value);
      if (this.Count <= this.MaxCount)
        return;
      this.RemoveAt(0);
    }

    public DocumentNavigation.NavigationPont Current => this[this.Count - 1];
  }

  public class NavigationPont(int pos, Scintilla scintilla) : ManagedRange(pos, pos, scintilla)
  {
    public override void Dispose()
    {
      this.Scintilla.DocumentNavigation.ForewardStack.Remove(this);
      this.Scintilla.DocumentNavigation.BackwardStack.Remove(this);
      base.Dispose();
    }
  }
}
