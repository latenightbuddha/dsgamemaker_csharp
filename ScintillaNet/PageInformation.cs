using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

public class PageInformation
{
  private const int c_iBorderSpace = 2;
  public static readonly Font DefaultFont = new Font(FontFamily.GenericSansSerif, 8f);
  private int m_iMargin;
  private Font m_oFont;
  private PageInformationBorder m_eBorder;
  private InformationType m_eLeft;
  private InformationType m_eCenter;
  private InformationType m_eRight;

  public PageInformation()
    : this(PageInformationBorder.None, InformationType.Nothing, InformationType.Nothing, InformationType.Nothing)
  {
  }

  public PageInformation(
    PageInformationBorder eBorder,
    InformationType eLeft,
    InformationType eCenter,
    InformationType eRight)
    : this(3, PageInformation.DefaultFont, eBorder, eLeft, eCenter, eRight)
  {
  }

  public PageInformation(
    int iMargin,
    Font oFont,
    PageInformationBorder eBorder,
    InformationType eLeft,
    InformationType eCenter,
    InformationType eRight)
  {
    this.m_iMargin = iMargin;
    this.m_oFont = oFont;
    this.m_eBorder = eBorder;
    this.m_eLeft = eLeft;
    this.m_eCenter = eCenter;
    this.m_eRight = eRight;
  }

  public virtual int Margin
  {
    get => this.m_iMargin;
    set => this.m_iMargin = value;
  }

  public virtual Font Font
  {
    get => this.m_oFont;
    set => this.m_oFont = value;
  }

  public virtual PageInformationBorder Border
  {
    get => this.m_eBorder;
    set => this.m_eBorder = value;
  }

  public virtual InformationType Left
  {
    get => this.m_eLeft;
    set => this.m_eLeft = value;
  }

  public virtual InformationType Center
  {
    get => this.m_eCenter;
    set => this.m_eCenter = value;
  }

  public virtual InformationType Right
  {
    get => this.m_eRight;
    set => this.m_eRight = value;
  }

  [Browsable(false)]
  public bool Display
  {
    get
    {
      return this.m_eLeft != InformationType.Nothing || this.m_eCenter != InformationType.Nothing || this.m_eRight != InformationType.Nothing;
    }
  }

  [Browsable(false)]
  public int Height
  {
    get
    {
      int height = this.Font.Height;
      switch (this.m_eBorder)
      {
        case PageInformationBorder.Top:
        case PageInformationBorder.Bottom:
          height += 2;
          break;
        case PageInformationBorder.Box:
          height += 4;
          break;
      }
      return height;
    }
  }

  public void Draw(Graphics oGraphics, Rectangle oBounds, string strDocumentName, int iPageNumber)
  {
    StringFormat format = new StringFormat(StringFormat.GenericDefault);
    Pen black1 = Pens.Black;
    Brush black2 = Brushes.Black;
    switch (this.m_eBorder)
    {
      case PageInformationBorder.Top:
        oGraphics.DrawLine(black1, oBounds.Left, oBounds.Top, oBounds.Right, oBounds.Top);
        break;
      case PageInformationBorder.Bottom:
        oGraphics.DrawLine(black1, oBounds.Left, oBounds.Bottom, oBounds.Right, oBounds.Bottom);
        break;
      case PageInformationBorder.Box:
        oGraphics.DrawRectangle(black1, oBounds);
        oBounds = new Rectangle(oBounds.Left + 2, oBounds.Top, oBounds.Width - 4, oBounds.Height);
        break;
    }
    format.LineAlignment = StringAlignment.Center;
    format.Alignment = StringAlignment.Near;
    switch (this.m_eLeft)
    {
      case InformationType.PageNumber:
        oGraphics.DrawString("Page " + (object) iPageNumber, this.m_oFont, black2, (RectangleF) oBounds, format);
        break;
      case InformationType.DocumentName:
        oGraphics.DrawString(strDocumentName, this.m_oFont, black2, (RectangleF) oBounds, format);
        break;
    }
    format.Alignment = StringAlignment.Center;
    switch (this.m_eCenter)
    {
      case InformationType.PageNumber:
        oGraphics.DrawString("Page " + (object) iPageNumber, this.m_oFont, black2, (RectangleF) oBounds, format);
        break;
      case InformationType.DocumentName:
        oGraphics.DrawString(strDocumentName, this.m_oFont, black2, (RectangleF) oBounds, format);
        break;
    }
    format.Alignment = StringAlignment.Far;
    switch (this.m_eRight)
    {
      case InformationType.PageNumber:
        oGraphics.DrawString("Page " + (object) iPageNumber, this.m_oFont, black2, (RectangleF) oBounds, format);
        break;
      case InformationType.DocumentName:
        oGraphics.DrawString(strDocumentName, this.m_oFont, black2, (RectangleF) oBounds, format);
        break;
    }
  }
}
