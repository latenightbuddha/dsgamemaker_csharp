using System;
using System.Text;

#nullable disable
namespace ScintillaNet;

public class AutoCompleteAcceptedEventArgs : EventArgs
{
  private string _text;
  private int _wordStartPosition;
  private bool _cancel;

  public string Text => this._text;

  public int WordStartPosition => this._wordStartPosition;

  public bool Cancel
  {
    get => this._cancel;
    set => this._cancel = value;
  }

  public AutoCompleteAcceptedEventArgs(string text) => this._text = text;

  internal AutoCompleteAcceptedEventArgs(SCNotification eventSource, Encoding encoding)
  {
    this._wordStartPosition = (int) eventSource.lParam;
    this._text = Utilities.IntPtrToString(encoding, eventSource.text, eventSource.length);
  }
}
