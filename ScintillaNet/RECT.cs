using System;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[Serializable]
public struct RECT(int left_, int top_, int right_, int bottom_)
{
  public int Left = left_;
  public int Top = top_;
  public int Right = right_;
  public int Bottom = bottom_;

  public int Height => this.Bottom - this.Top;

  public int Width => this.Right - this.Left;

  public Size Size => new Size(this.Width, this.Height);

  public Point Location => new Point(this.Left, this.Top);

  public Rectangle ToRectangle()
  {
    return Rectangle.FromLTRB(this.Left, this.Top, this.Right, this.Bottom);
  }

  public static RECT FromRectangle(Rectangle rectangle)
  {
    return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
  }

  public override int GetHashCode()
  {
    return this.Left ^ (this.Top << 13 | this.Top >> 19) ^ (this.Width << 26 | this.Width >> 6) ^ (this.Height << 7 | this.Height >> 25);
  }

  public static implicit operator Rectangle(RECT rect) => rect.ToRectangle();

  public static implicit operator RECT(Rectangle rect) => RECT.FromRectangle(rect);
}
