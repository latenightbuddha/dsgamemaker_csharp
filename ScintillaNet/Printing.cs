using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Printing : TopLevelHelper
{
  private PrintDocument _printDocument;

  internal Printing(Scintilla scintilla)
    : base(scintilla)
  {
    this._printDocument = new PrintDocument(scintilla);
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializePageSettings() || this.ShouldSerializePrintDocument();
  }

  public bool Print() => this.Print(true);

  public bool Print(bool showPrintDialog)
  {
    if (showPrintDialog)
    {
      PrintDialog printDialog = new PrintDialog();
      printDialog.Document = (System.Drawing.Printing.PrintDocument) this._printDocument;
      printDialog.AllowCurrentPage = true;
      printDialog.AllowSelection = true;
      printDialog.AllowSomePages = true;
      printDialog.PrinterSettings = this.PageSettings.PrinterSettings;
      if (printDialog.ShowDialog((IWin32Window) this.Scintilla) != DialogResult.OK)
        return false;
      this._printDocument.PrinterSettings = printDialog.PrinterSettings;
      this._printDocument.Print();
      return true;
    }
    this._printDocument.Print();
    return true;
  }

  public DialogResult PrintPreview()
  {
    return new PrintPreviewDialog()
    {
      WindowState = FormWindowState.Maximized,
      Document = ((System.Drawing.Printing.PrintDocument) this._printDocument)
    }.ShowDialog();
  }

  public DialogResult PrintPreview(IWin32Window owner)
  {
    return new PrintPreviewDialog()
    {
      WindowState = FormWindowState.Maximized,
      Document = ((System.Drawing.Printing.PrintDocument) this._printDocument)
    }.ShowDialog(owner);
  }

  public DialogResult ShowPageSetupDialog()
  {
    return new PageSetupDialog()
    {
      PageSettings = ((System.Drawing.Printing.PageSettings) this.PageSettings),
      PrinterSettings = this.PageSettings.PrinterSettings
    }.ShowDialog();
  }

  public DialogResult ShowPageSetupDialog(IWin32Window owner)
  {
    return new PageSetupDialog()
    {
      AllowPrinter = true,
      PageSettings = ((System.Drawing.Printing.PageSettings) this.PageSettings),
      PrinterSettings = this.PageSettings.PrinterSettings
    }.ShowDialog(owner);
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public PrintDocument PrintDocument
  {
    get => this._printDocument;
    set => this._printDocument = value;
  }

  private bool ShouldSerializePrintDocument() => this._printDocument.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Browsable(true)]
  public PageSettings PageSettings
  {
    get => this._printDocument.DefaultPageSettings as PageSettings;
    set => this._printDocument.DefaultPageSettings = (System.Drawing.Printing.PageSettings) value;
  }

  private bool ShouldSerializePageSettings() => this.PageSettings.ShouldSerialize();
}
