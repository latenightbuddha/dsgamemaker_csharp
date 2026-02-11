using System.Collections.ObjectModel;

#nullable disable
namespace ScintillaNet.Configuration;

public class MarkersConfigList : KeyedCollection<int, MarkersConfig>
{
  private bool? _inherit;

  protected override int GetKeyForItem(MarkersConfig item) => item.Number.Value;

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }
}
