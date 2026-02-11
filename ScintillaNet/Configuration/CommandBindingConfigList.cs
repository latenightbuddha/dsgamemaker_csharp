using System.Collections.Generic;

#nullable disable
namespace ScintillaNet.Configuration;

public class CommandBindingConfigList : List<CommandBindingConfig>
{
  private bool? _inherit;
  private bool? _allowDuplicateBindings;

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }

  public bool? AllowDuplicateBindings
  {
    get => this._allowDuplicateBindings;
    set => this._allowDuplicateBindings = value;
  }
}
