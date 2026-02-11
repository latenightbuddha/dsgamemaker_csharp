#nullable disable
namespace ScintillaNet;

public struct PrintRectangle(int iLeft, int iTop, int iRight, int iBottom)
{
  public int Left = iLeft;
  public int Top = iTop;
  public int Right = iRight;
  public int Bottom = iBottom;
}
