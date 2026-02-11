using System.Drawing;

#nullable disable
namespace ScintillaNet.Configuration;

public class StyleConfig
{
  private bool? _bold;
  private StyleCase? _case;
  private ScintillaNet.CharacterSet? _characterSet;
  private string _fontName;
  private Color _foreColor;
  private Color _backColor;
  private bool? _isChangeable;
  private bool? _isHotspot;
  private bool? _isSelectionEolFilled;
  private bool? _isVisible;
  private bool? _italic;
  private int? _size;
  private bool? _underline;
  private int? _number;
  private string _name;
  private bool? _inherit;

  public bool? Bold
  {
    get => this._bold;
    set => this._bold = value;
  }

  public StyleCase? Case
  {
    get => this._case;
    set => this._case = value;
  }

  public ScintillaNet.CharacterSet? CharacterSet
  {
    get => this._characterSet;
    set => this._characterSet = value;
  }

  public string FontName
  {
    get => this._fontName;
    set => this._fontName = value;
  }

  public Color ForeColor
  {
    get => this._foreColor;
    set => this._foreColor = value;
  }

  public Color BackColor
  {
    get => this._backColor;
    set => this._backColor = value;
  }

  public bool? IsChangeable
  {
    get => this._isChangeable;
    set => this._isChangeable = value;
  }

  public bool? IsHotspot
  {
    get => this._isHotspot;
    set => this._isHotspot = value;
  }

  public bool? IsSelectionEolFilled
  {
    get => this._isSelectionEolFilled;
    set => this._isSelectionEolFilled = value;
  }

  public bool? IsVisible
  {
    get => this._isVisible;
    set => this._isVisible = value;
  }

  public bool? Italic
  {
    get => this._italic;
    set => this._italic = value;
  }

  public int? Size
  {
    get => this._size;
    set => this._size = value;
  }

  public bool? Underline
  {
    get => this._underline;
    set => this._underline = value;
  }

  public int? Number
  {
    get => this._number;
    set => this._number = value;
  }

  public string Name
  {
    get => this._name;
    set => this._name = value;
  }

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }

  public override string ToString() => $"Name = \"{this._name}\" Number={this._number.ToString()}";
}
