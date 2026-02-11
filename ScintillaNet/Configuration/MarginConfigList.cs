using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace ScintillaNet.Configuration;

public class MarginConfigList : List<MarginConfig>
{
  private bool? _inherit;
  private int? _left;
  private int? _right;
  private Color _foldMarginColor;
  private Color _foldMarginHighlightColor;

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }

  public int? Left
  {
    get => this._left;
    set => this._left = value;
  }

  public int? Right
  {
    get => this._right;
    set => this._right = value;
  }

  public Color FoldMarginColor
  {
    get => this._foldMarginColor;
    set => this._foldMarginColor = value;
  }

  public Color FoldMarginHighlightColor
  {
    get => this._foldMarginHighlightColor;
    set => this._foldMarginHighlightColor = value;
  }
}
