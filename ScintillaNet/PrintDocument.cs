using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class PrintDocument : System.Drawing.Printing.PrintDocument
{
  private Scintilla m_oScintillaControl;
  private int m_iPosition;
  private int m_iPrintEnd;
  private int m_iCurrentPage;

  public PrintDocument(Scintilla oScintillaControl)
  {
    this.m_oScintillaControl = oScintillaControl;
    this.DefaultPageSettings = (System.Drawing.Printing.PageSettings) new PageSettings();
  }

  internal bool ShouldSerialize() => base.DocumentName != "document" || this.OriginAtMargins;

  protected override void OnBeginPrint(PrintEventArgs e)
  {
    base.OnBeginPrint(e);
    this.m_iPosition = 0;
    this.m_iPrintEnd = this.m_oScintillaControl.TextLength;
    this.m_iCurrentPage = 1;
  }

  protected override void OnEndPrint(PrintEventArgs e) => base.OnEndPrint(e);

  protected override void OnPrintPage(PrintPageEventArgs e)
  {
    base.OnPrintPage(e);
    HeaderInformation header = ((PageSettings) this.DefaultPageSettings).Header;
    FooterInformation footer = ((PageSettings) this.DefaultPageSettings).Footer;
    Rectangle marginBounds = e.MarginBounds;
    bool isPreview = this.PrintController.IsPreview;
    if (!isPreview)
      e.Graphics.TranslateTransform(-e.PageSettings.HardMarginX, -e.PageSettings.HardMarginY);
    if (e.PageSettings is PageSettings)
    {
      PageSettings pageSettings = (PageSettings) e.PageSettings;
      header = pageSettings.Header;
      footer = pageSettings.Footer;
      this.m_oScintillaControl.NativeInterface.SetPrintMagnification((int) pageSettings.FontMagnification);
      this.m_oScintillaControl.NativeInterface.SetPrintColourMode((int) pageSettings.ColorMode);
    }
    Rectangle oBounds1 = this.DrawHeader(e.Graphics, marginBounds, (PageInformation) header);
    Rectangle oBounds2 = this.DrawFooter(e.Graphics, oBounds1, (PageInformation) footer);
    if (!isPreview)
      oBounds2.Offset((int) -(double) e.PageSettings.HardMarginX, (int) -(double) e.PageSettings.HardMarginY);
    this.DrawCurrentPage(e.Graphics, oBounds2);
    ++this.m_iCurrentPage;
    e.HasMorePages = this.m_iPosition < this.m_iPrintEnd;
  }

  private Rectangle DrawHeader(Graphics oGraphics, Rectangle oBounds, PageInformation oHeader)
  {
    if (!oHeader.Display)
      return oBounds;
    Rectangle oBounds1 = new Rectangle(oBounds.Left, oBounds.Top, oBounds.Width, oHeader.Height);
    oHeader.Draw(oGraphics, oBounds1, this.DocumentName, this.m_iCurrentPage);
    return new Rectangle(oBounds.Left, oBounds.Top + oBounds1.Height + oHeader.Margin, oBounds.Width, oBounds.Height - oBounds1.Height - oHeader.Margin);
  }

  private Rectangle DrawFooter(Graphics oGraphics, Rectangle oBounds, PageInformation oFooter)
  {
    if (!oFooter.Display)
      return oBounds;
    int height = oFooter.Height;
    Rectangle oBounds1 = new Rectangle(oBounds.Left, oBounds.Bottom - height, oBounds.Width, height);
    oFooter.Draw(oGraphics, oBounds1, this.DocumentName, this.m_iCurrentPage);
    return new Rectangle(oBounds.Left, oBounds.Top, oBounds.Width, oBounds.Height - oBounds1.Height - oFooter.Margin);
  }

  private void DrawCurrentPage(Graphics oGraphics, Rectangle oBounds)
  {
    Point[] pts = new Point[2]
    {
      new Point(oBounds.Left, oBounds.Top),
      new Point(oBounds.Right, oBounds.Bottom)
    };
    oGraphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, pts);
    PrintRectangle printRectangle = new PrintRectangle(pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
    RangeToFormat pfr = new RangeToFormat();
    pfr.hdc = pfr.hdcTarget = oGraphics.GetHdc();
    pfr.rc = pfr.rcPage = printRectangle;
    pfr.chrg.cpMin = this.m_iPosition;
    pfr.chrg.cpMax = this.m_iPrintEnd;
    this.m_iPosition = this.m_oScintillaControl.NativeInterface.FormatRange(true, ref pfr);
  }

  public new string DocumentName
  {
    get => base.DocumentName;
    set => base.DocumentName = value;
  }

  private bool ShouldSerializeDocumentName() => this.DocumentName != "document";

  private void ResetDocumentName() => this.DocumentName = "document";

  public new bool OriginAtMargins
  {
    get => base.OriginAtMargins;
    set => base.OriginAtMargins = value;
  }

  private bool ShouldSerializeOriginAtMargins() => this.OriginAtMargins;

  private void ResetOriginAtMargins() => this.OriginAtMargins = false;
}
