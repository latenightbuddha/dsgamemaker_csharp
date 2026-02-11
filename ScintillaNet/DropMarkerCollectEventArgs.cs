using System.ComponentModel;

#nullable disable
namespace ScintillaNet;

public class DropMarkerCollectEventArgs : CancelEventArgs
{
  private DropMarker _dropMarker;

  public DropMarker DropMarker => this._dropMarker;

  public DropMarkerCollectEventArgs(DropMarker dropMarker) => this._dropMarker = dropMarker;
}
