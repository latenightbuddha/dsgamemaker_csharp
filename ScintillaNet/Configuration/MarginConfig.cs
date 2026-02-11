#nullable disable
namespace ScintillaNet.Configuration;

public class MarginConfig
{
  private int _number;
  private bool? _inherit;
  private MarginType? _type;
  private bool? _isFoldMargin;
  private bool? _isMarkerMargin;
  private int? _width;
  private bool? _isClickable;
  private int? _autoToggleMarkerNumber;

  public int Number
  {
    get => this._number;
    set => this._number = value;
  }

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }

  public MarginType? Type
  {
    get => this._type;
    set => this._type = value;
  }

  public bool? IsFoldMargin
  {
    get => this._isFoldMargin;
    set => this._isFoldMargin = value;
  }

  public bool? IsMarkerMargin
  {
    get => this._isMarkerMargin;
    set => this._isMarkerMargin = value;
  }

  public int? Width
  {
    get => this._width;
    set => this._width = value;
  }

  public bool? IsClickable
  {
    get => this._isClickable;
    set => this._isClickable = value;
  }

  public int? AutoToggleMarkerNumber
  {
    get => this._autoToggleMarkerNumber;
    set => this._autoToggleMarkerNumber = value;
  }
}
