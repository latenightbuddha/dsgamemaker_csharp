using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public class GoTo : TopLevelHelper
{
  internal GoTo(Scintilla scintilla)
    : base(scintilla)
  {
  }

  public void Position(int pos) => this.NativeScintilla.GotoPos(pos);

  public void Line(int number) => this.NativeScintilla.GotoLine(number);

  public void ShowGoToDialog()
  {
    GoToDialog goToDialog = new GoToDialog();
    goToDialog.CurrentLineNumber = this.Scintilla.Lines.Current.Number;
    goToDialog.MaximumLineNumber = this.Scintilla.Lines.Count;
    goToDialog.Scintilla = this.Scintilla;
    if (goToDialog.ShowDialog() == DialogResult.OK)
      this.Line(goToDialog.GotoLineNumber);
    this.Scintilla.Focus();
  }
}
