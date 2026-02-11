using System.Collections.Generic;

#nullable disable
namespace ScintillaNet.Configuration;

public class StyleConfigList : List<StyleConfig>
{
  private int? _bits;
  private bool? _inherit;

  public int? Bits
  {
    get => this._bits;
    set => this._bits = value;
  }

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }
}
