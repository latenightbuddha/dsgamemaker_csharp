using System.Collections.Generic;

#nullable disable
namespace ScintillaNet.Configuration;

public class LexerPropertiesConfig : Dictionary<string, string>
{
  private bool? _inherit;

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }
}
