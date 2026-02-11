using System.Collections.ObjectModel;

#nullable disable
namespace ScintillaNet.Configuration;

public class KeyWordConfigList : KeyedCollection<int, KeyWordConfig>
{
  protected override int GetKeyForItem(KeyWordConfig item) => item.List;
}
