using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class PageSettings : System.Drawing.Printing.PageSettings
{
  public static readonly PageInformation DefaultHeader = new PageInformation(PageInformationBorder.Bottom, InformationType.DocumentName, InformationType.Nothing, InformationType.PageNumber);
  public static readonly PageInformation DefaultFooter = new PageInformation(PageInformationBorder.Top, InformationType.Nothing, InformationType.Nothing, InformationType.Nothing);
  private HeaderInformation m_oHeader;
  private FooterInformation m_oFooter;
  private short m_sFontMagnification;
  private PrintColorMode m_eColorMode;
  private bool baseColor;

  public PageSettings()
  {
    this.baseColor = base.Color;
    this.m_oHeader = new HeaderInformation(PageInformationBorder.Bottom, InformationType.DocumentName, InformationType.Nothing, InformationType.PageNumber);
    this.m_oFooter = new FooterInformation(PageInformationBorder.Top, InformationType.Nothing, InformationType.Nothing, InformationType.Nothing);
    this.m_sFontMagnification = (short) 0;
    this.m_eColorMode = PrintColorMode.Normal;
    base.Margins.Top = 50;
    base.Margins.Left = 50;
    base.Margins.Right = 50;
    base.Margins.Bottom = 50;
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeColor() || this.ShouldSerializeColorMode() || this.ShouldSerializeFontMagnification() || this.ShouldSerializeFooter() || this.ShouldSerializeHeader() || this.ShouldSerializeLandscape() || this.ShouldSerializeMargins();
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public HeaderInformation Header
  {
    get => this.m_oHeader;
    set => this.m_oHeader = value;
  }

  private bool ShouldSerializeHeader() => this.m_oHeader.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public FooterInformation Footer
  {
    get => this.m_oFooter;
    set => this.m_oFooter = value;
  }

  private bool ShouldSerializeFooter() => this.m_oFooter.ShouldSerialize();

  public short FontMagnification
  {
    get => this.m_sFontMagnification;
    set => this.m_sFontMagnification = value;
  }

  private bool ShouldSerializeFontMagnification() => this.m_sFontMagnification != (short) 0;

  private void ResetFontMagnification() => this.m_sFontMagnification = (short) 0;

  public PrintColorMode ColorMode
  {
    get => this.m_eColorMode;
    set => this.m_eColorMode = value;
  }

  private bool ShouldSerializeColorMode() => this.m_eColorMode != PrintColorMode.Normal;

  private void ResetColorMode() => this.m_eColorMode = PrintColorMode.Normal;

  [Browsable(false)]
  public new Rectangle Bounds => base.Bounds;

  public new bool Color
  {
    get => base.Color;
    set => base.Color = value;
  }

  private bool ShouldSerializeColor() => this.Color != this.baseColor;

  private void ResetColor() => this.Color = this.baseColor;

  [Browsable(false)]
  public new float HardMarginX => base.HardMarginX;

  [Browsable(false)]
  public new float HardMarginY => base.HardMarginY;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public new PaperSize PaperSize
  {
    get => base.PaperSize;
    set => base.PaperSize = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public new PaperSource PaperSource
  {
    get => base.PaperSource;
    set => base.PaperSource = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new RectangleF PrintableArea => base.PrintableArea;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public new PrinterResolution PrinterResolution
  {
    get => base.PrinterResolution;
    set => base.PrinterResolution = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public new PrinterSettings PrinterSettings
  {
    get => base.PrinterSettings;
    set => base.PrinterSettings = value;
  }

  public new bool Landscape
  {
    get => base.Landscape;
    set => base.Landscape = value;
  }

  private bool ShouldSerializeLandscape() => this.Landscape;

  private void ResetLandscape() => this.Landscape = false;

  public new Margins Margins
  {
    get => base.Margins;
    set => base.Margins = value;
  }

  private bool ShouldSerializeMargins()
  {
    return this.Margins.Bottom != 50 || this.Margins.Left != 50 || this.Margins.Right != 50 || this.Margins.Bottom != 50;
  }

  private void ResetMargins() => this.Margins = new Margins(50, 50, 50, 50);
}
