using System;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public class MacroRecordEventArgs : EventArgs
{
  private Message _recordedMessage;

  public Message RecordedMessage => this._recordedMessage;

  public MacroRecordEventArgs(Message recordedMessage) => this._recordedMessage = recordedMessage;

  public MacroRecordEventArgs(NativeScintillaEventArgs ea)
  {
    this._recordedMessage = ea.Msg;
    this._recordedMessage.LParam = ea.SCNotification.lParam;
    this._recordedMessage.WParam = ea.SCNotification.wParam;
  }
}
