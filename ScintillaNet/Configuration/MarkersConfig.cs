using System.Drawing;

#nullable disable
namespace ScintillaNet.Configuration;

public class MarkersConfig
{
  private int? _alpha;
  private Color _backColor;
  private Color _foreColor;
  private string _name;
  private int? _number;
  private MarkerSymbol? _symbol;
  private bool? _inherit;

  public int? Alpha
  {
    get => this._alpha;
    set => this._alpha = value;
  }

  public Color BackColor
  {
    get => this._backColor;
    set => this._backColor = value;
  }

  public Color ForeColor
  {
    get => this._foreColor;
    set => this._foreColor = value;
  }

  public string Name
  {
    get => this._name;
    set => this._name = value;
  }

  public int? Number
  {
    get => this._number;
    set => this._number = value;
  }

  public MarkerSymbol? Symbol
  {
    get => this._symbol;
    set => this._symbol = value;
  }

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }
}
