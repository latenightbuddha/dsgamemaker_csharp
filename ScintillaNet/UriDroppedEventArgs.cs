using System;

#nullable disable
namespace ScintillaNet;

public class UriDroppedEventArgs : EventArgs
{
  private string _uriText;

  public string UriText
  {
    get => this._uriText;
    set => this._uriText = value;
  }

  public UriDroppedEventArgs(string uriText) => this._uriText = uriText;
}
