using System.Drawing;

#nullable disable
namespace ScintillaNet.Configuration;

public class IndicatorConfig
{
  private bool? _inherit;
  private Color _color;
  private int _number;
  private IndicatorStyle? _style;
  private bool? _isDrawnUnder;

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }

  public Color Color
  {
    get => this._color;
    set => this._color = value;
  }

  public int Number
  {
    get => this._number;
    set => this._number = value;
  }

  public IndicatorStyle? Style
  {
    get => this._style;
    set => this._style = value;
  }

  public bool? IsDrawnUnder
  {
    get => this._isDrawnUnder;
    set => this._isDrawnUnder = value;
  }
}
