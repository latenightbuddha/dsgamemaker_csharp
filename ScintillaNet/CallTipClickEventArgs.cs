using System;

#nullable disable
namespace ScintillaNet;

public class CallTipClickEventArgs : EventArgs
{
  private CallTipArrow _callTipArrow;
  private int _currentIndex;
  private int _newIndex;
  private OverloadList _overloadList;
  private bool _cancel;
  private int _highlightStart;
  private int _highlightEnd;

  public CallTipArrow CallTipArrow => this._callTipArrow;

  public int CurrentIndex => this._currentIndex;

  public int NewIndex
  {
    get => this._newIndex;
    set => this._newIndex = value;
  }

  public OverloadList OverloadList => this._overloadList;

  public bool Cancel
  {
    get => this._cancel;
    set => this._cancel = value;
  }

  public int HighlightStart
  {
    get => this._highlightStart;
    set => this._highlightStart = value;
  }

  public int HighlightEnd
  {
    get => this._highlightEnd;
    set => this._highlightEnd = value;
  }

  public CallTipClickEventArgs(
    CallTipArrow callTipArrow,
    int currentIndex,
    int newIndex,
    OverloadList overloadList,
    int highlightStart,
    int highlightEnd)
  {
    this._callTipArrow = callTipArrow;
    this._currentIndex = currentIndex;
    this._newIndex = newIndex;
    this._overloadList = overloadList;
    this._highlightStart = highlightStart;
    this._highlightEnd = highlightEnd;
  }
}
