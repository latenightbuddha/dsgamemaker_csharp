using System;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public class NativeScintillaEventArgs : EventArgs
{
  private Message _msg;
  private SCNotification _notification;

  public Message Msg => this._msg;

  public SCNotification SCNotification => this._notification;

  public NativeScintillaEventArgs(Message msg, SCNotification notification)
  {
    this._msg = msg;
    this._notification = notification;
  }
}
