using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace ScintillaNet.Configuration;

public class SnippetsConfigList : List<SnippetsConfig>
{
  private bool? _inherit;
  private Color _activeSnippetColor;
  private int? _activeSnippetIndicator;
  private IndicatorStyle? _activeSnippetIndicatorStyle;
  private IndicatorStyle? _inactiveSnippetIndicatorStyle;
  private Color _inactiveSnippetColor;
  private int? _inactiveSnippetIndicator;
  private bool? _isEnabled;
  private char? _defaultDelimeter;
  private bool? _isOneKeySelectionEmbedEnabled;

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }

  public Color ActiveSnippetColor
  {
    get => this._activeSnippetColor;
    set => this._activeSnippetColor = value;
  }

  public int? ActiveSnippetIndicator
  {
    get => this._activeSnippetIndicator;
    set => this._activeSnippetIndicator = value;
  }

  public IndicatorStyle? ActiveSnippetIndicatorStyle
  {
    get => this._activeSnippetIndicatorStyle;
    set => this._activeSnippetIndicatorStyle = value;
  }

  public IndicatorStyle? InactiveSnippetIndicatorStyle
  {
    get => this._inactiveSnippetIndicatorStyle;
    set => this._inactiveSnippetIndicatorStyle = value;
  }

  public Color InactiveSnippetColor
  {
    get => this._inactiveSnippetColor;
    set => this._inactiveSnippetColor = value;
  }

  public int? InactiveSnippetIndicator
  {
    get => this._inactiveSnippetIndicator;
    set => this._inactiveSnippetIndicator = value;
  }

  public bool? IsEnabled
  {
    get => this._isEnabled;
    set => this._isEnabled = value;
  }

  public char? DefaultDelimeter
  {
    get => this._defaultDelimeter;
    set => this._defaultDelimeter = value;
  }

  public bool? IsOneKeySelectionEmbedEnabled
  {
    get => this._isOneKeySelectionEmbedEnabled;
    set => this._isOneKeySelectionEmbedEnabled = value;
  }
}
