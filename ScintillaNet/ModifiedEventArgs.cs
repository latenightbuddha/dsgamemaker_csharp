using System;

#nullable disable
namespace ScintillaNet;

public abstract class ModifiedEventArgs : EventArgs
{
  private UndoRedoFlags _undoRedoFlags;
  private int _modificationType;

  public int ModificationType
  {
    get => this._modificationType;
    set => this._modificationType = value;
  }

  public UndoRedoFlags UndoRedoFlags
  {
    get => this._undoRedoFlags;
    set => this._undoRedoFlags = value;
  }

  public ModifiedEventArgs(int modificationType)
  {
    this._modificationType = modificationType;
    this._undoRedoFlags = new UndoRedoFlags(modificationType);
  }
}
