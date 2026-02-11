#nullable disable
namespace ScintillaNet;

public struct UndoRedoFlags(int modificationType)
{
  private const string STRING_FORMAT = "IsUndo\t\t\t\t:{0}\r\nIsRedo\t\t\t\t:{1}\r\nIsMultiStep\t\t\t:{2}\r\nIsLastStep\t\t\t:{3}\r\nIsMultiLine\t\t\t:{4}";
  public bool IsUndo = ((long) modificationType & 32L /*0x20*/) > 0L;
  public bool IsRedo = ((long) modificationType & 64L /*0x40*/) > 0L;
  public bool IsMultiStep = ((long) modificationType & 128L /*0x80*/) > 0L;
  public bool IsLastStep = ((long) modificationType & 256L /*0x0100*/) > 0L;
  public bool IsMultiLine = ((long) modificationType & 4096L /*0x1000*/) > 0L;

  public override string ToString()
  {
    return $"IsUndo\t\t\t\t:{this.IsUndo}\r\nIsRedo\t\t\t\t:{this.IsRedo}\r\nIsMultiStep\t\t\t:{this.IsMultiStep}\r\nIsLastStep\t\t\t:{this.IsLastStep}\r\nIsMultiLine\t\t\t:{this.IsMultiLine}";
  }
}
