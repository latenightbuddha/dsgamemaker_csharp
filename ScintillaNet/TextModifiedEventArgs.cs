using System;

#nullable disable
namespace ScintillaNet;

public class TextModifiedEventArgs : ModifiedEventArgs
{
  private const string STRING_FORMAT = "ModificationTypeFlags\t:{0}\r\nPosition\t\t\t:{1}\r\nLength\t\t\t\t:{2}\r\nLinesAddedCount\t\t:{3}\r\nText\t\t\t\t:{4}\r\nIsUserChange\t\t\t:{5}\r\nMarkerChangeLine\t\t:{6}";
  private int _position;
  private int _length;
  private int _linesAddedCount;
  private string _text;
  private bool _isUserChange;
  private int _markerChangedLine;

  public override string ToString()
  {
    return $"ModificationTypeFlags\t:{this.ModificationType}\r\nPosition\t\t\t:{this._position}\r\nLength\t\t\t\t:{this._length}\r\nLinesAddedCount\t\t:{this._linesAddedCount}\r\nText\t\t\t\t:{this._text}\r\nIsUserChange\t\t\t:{this._isUserChange}\r\nMarkerChangeLine\t\t:{this._markerChangedLine}" + Environment.NewLine + this.UndoRedoFlags.ToString();
  }

  public bool IsUserChange => this._isUserChange;

  public int MarkerChangedLine => this._markerChangedLine;

  public int Position => this._position;

  public int Length => this._length;

  public int LinesAddedCount => this._linesAddedCount;

  public string Text => this._text;

  public TextModifiedEventArgs(
    int modificationType,
    bool isUserChange,
    int markerChangedLine,
    int position,
    int length,
    int linesAddedCount,
    string text)
    : base(modificationType)
  {
    this._isUserChange = isUserChange;
    this._markerChangedLine = markerChangedLine;
    this._position = position;
    this._length = length;
    this._linesAddedCount = linesAddedCount;
    this._text = text;
  }
}
