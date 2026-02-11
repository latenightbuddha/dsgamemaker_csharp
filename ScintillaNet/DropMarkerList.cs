using System;
using System.Collections.ObjectModel;

#nullable disable
namespace ScintillaNet;

public class DropMarkerList : KeyedCollection<Guid, DropMarker>
{
  protected override Guid GetKeyForItem(DropMarker item) => item.Key;
}
