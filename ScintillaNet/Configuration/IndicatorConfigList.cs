using System.Collections.ObjectModel;

#nullable disable
namespace ScintillaNet.Configuration;

public class IndicatorConfigList : KeyedCollection<int, IndicatorConfig>
{
  private bool? _inherit;

  protected override int GetKeyForItem(IndicatorConfig item) => item.Number;

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }
}
