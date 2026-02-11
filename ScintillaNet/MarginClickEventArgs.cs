using System;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public class MarginClickEventArgs : EventArgs
{
  private Keys _modifiers;
  private int _position;
  private Line _line;
  private Margin _margin;
  private int _toggleMarkerNumber;
  private bool _toggleFold;

  public Keys Modifiers => this._modifiers;

  public int Position => this._position;

  public Line Line => this._line;

  public Margin Margin => this._margin;

  public int ToggleMarkerNumber
  {
    get => this._toggleMarkerNumber;
    set => this._toggleMarkerNumber = value;
  }

  public bool ToggleFold
  {
    get => this._toggleFold;
    set => this._toggleFold = value;
  }

  public MarginClickEventArgs(
    Keys modifiers,
    int position,
    Line line,
    Margin margin,
    int toggleMarkerNumber,
    bool toggleFold)
  {
    this._modifiers = modifiers;
    this._position = position;
    this._line = line;
    this._margin = margin;
    this._toggleMarkerNumber = toggleMarkerNumber;
    this._toggleFold = toggleFold;
  }
}
