using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class FooterInformation : PageInformation
{
  public FooterInformation()
    : base(PageInformationBorder.None, InformationType.Nothing, InformationType.Nothing, InformationType.Nothing)
  {
  }

  public FooterInformation(
    PageInformationBorder eBorder,
    InformationType eLeft,
    InformationType eCenter,
    InformationType eRight)
    : base(3, PageInformation.DefaultFont, eBorder, eLeft, eCenter, eRight)
  {
  }

  public FooterInformation(
    int iMargin,
    Font oFont,
    PageInformationBorder eBorder,
    InformationType eLeft,
    InformationType eCenter,
    InformationType eRight)
    : base(iMargin, oFont, eBorder, eLeft, eCenter, eRight)
  {
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeBorder() || this.ShouldSerializeCenter() || this.ShouldSerializeFont() || this.ShouldSerializeLeft() || this.ShouldSerializeMargin() || this.ShouldSerializeRight();
  }

  public override int Margin
  {
    get => base.Margin;
    set => base.Margin = value;
  }

  private bool ShouldSerializeMargin() => this.Margin != 3;

  private void ResetMargin() => this.Margin = 3;

  public override Font Font
  {
    get => base.Font;
    set => base.Font = value;
  }

  private bool ShouldSerializeFont() => !PageInformation.DefaultFont.Equals((object) this.Font);

  private void ResetFont() => this.Font = PageInformation.DefaultFont;

  public override PageInformationBorder Border
  {
    get => base.Border;
    set => base.Border = value;
  }

  private bool ShouldSerializeBorder() => this.Border != PageInformationBorder.Top;

  private void ResetBorder() => this.Border = PageInformationBorder.Top;

  public override InformationType Center
  {
    get => base.Center;
    set => base.Center = value;
  }

  private bool ShouldSerializeCenter() => this.Center != InformationType.Nothing;

  private void ResetCenter() => this.Center = InformationType.Nothing;

  public override InformationType Left
  {
    get => base.Left;
    set => base.Left = value;
  }

  private bool ShouldSerializeLeft() => this.Left != InformationType.Nothing;

  private void ResetLeft() => this.Left = InformationType.Nothing;

  public override InformationType Right
  {
    get => base.Right;
    set => base.Right = value;
  }

  private bool ShouldSerializeRight() => this.Right != InformationType.Nothing;

  private void ResetRight() => this.Right = InformationType.Nothing;
}
