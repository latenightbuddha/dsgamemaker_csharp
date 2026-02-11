#nullable disable
namespace ScintillaNet;

public enum ModificationFlags
{
  InsertText = 1,
  DeleteText = 2,
  ChangeStyle = 4,
  ChangeFold = 8,
  User = 16, // 0x00000010
  Undo = 32, // 0x00000020
  Redo = 64, // 0x00000040
  StepInUndoRedo = 256, // 0x00000100
  ChangeMarker = 512, // 0x00000200
  BeforeInsert = 1024, // 0x00000400
  BeforeDelete = 2048, // 0x00000800
}
