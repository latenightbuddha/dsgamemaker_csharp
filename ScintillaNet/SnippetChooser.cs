using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public class SnippetChooser : UserControl
{
  private string _snippetList = string.Empty;
  private Scintilla _scintilla;
  private IContainer components;
  private ToolTip toolTip;
  private Scintilla txtSnippet;
  private Label lblSnippet;

  public SnippetChooser() => this.InitializeComponent();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SnippetList
  {
    get => this._snippetList;
    set => this._snippetList = value;
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Scintilla Scintilla
  {
    get => this._scintilla;
    set => this._scintilla = value;
  }

  protected override void OnLeave(EventArgs e)
  {
    this.OnLostFocus(e);
    this.Hide();
  }

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    this.txtSnippet.Text = string.Empty;
    this.setPosition();
    if (this.Visible)
    {
      this.txtSnippet.Focus();
      this.txtSnippet.AutoComplete.Show(0, this._snippetList);
    }
    else
      this.Scintilla.Focus();
  }

  protected override void OnCreateControl()
  {
    this.setPosition();
    base.OnCreateControl();
    this.txtSnippet.Focus();
    this.txtSnippet.AutoComplete.Show(0, this._snippetList);
  }

  public void setPosition()
  {
    if (!this.Visible)
      return;
    int position = this.Scintilla.Caret.Position;
    this.Location = new Point(this.Scintilla.PointXFromPosition(position), this.Scintilla.PointYFromPosition(position));
  }

  private void SnippetChooser_Load(object sender, EventArgs e)
  {
    this.txtSnippet.Commands.RemoveAllBindings();
    this.txtSnippet.Commands.AddBinding(Keys.Delete, Keys.None, BindableCommand.Clear);
    this.txtSnippet.Commands.AddBinding(Keys.Back, Keys.None, BindableCommand.DeleteBack);
    this.txtSnippet.Commands.AddBinding('Z', Keys.Control, BindableCommand.Undo);
    this.txtSnippet.Commands.AddBinding('Y', Keys.Control, BindableCommand.Redo);
    this.txtSnippet.Commands.AddBinding('X', Keys.Control, BindableCommand.Cut);
    this.txtSnippet.Commands.AddBinding('C', Keys.Control, BindableCommand.Copy);
    this.txtSnippet.Commands.AddBinding('V', Keys.Control, BindableCommand.Paste);
    this.txtSnippet.Commands.AddBinding('A', Keys.Control, BindableCommand.SelectAll);
    this.txtSnippet.Commands.AddBinding(Keys.Down, Keys.None, BindableCommand.LineDown);
    this.txtSnippet.Commands.AddBinding(Keys.Up, Keys.None, BindableCommand.LineUp);
  }

  private void txtSnippet_KeyDown(object sender, KeyEventArgs e)
  {
    switch (e.KeyCode)
    {
      case Keys.Tab:
      case Keys.Return:
        if (this.txtSnippet.AutoComplete.SelectedIndex < 0)
          break;
        this.txtSnippet.AutoComplete.Accept();
        break;
      case Keys.Escape:
        this.Hide();
        break;
      case Keys.Left:
        this.txtSnippet.Caret.Goto(this.txtSnippet.Caret.Position - 1);
        break;
      case Keys.Right:
        this.txtSnippet.Caret.Goto(this.txtSnippet.Caret.Position + 1);
        break;
    }
  }

  private void txtSnippet_AutoCompleteAccepted(object sender, AutoCompleteAcceptedEventArgs e)
  {
    string selectedText = this.txtSnippet.AutoComplete.SelectedText;
    this.Hide();
    this.Scintilla.Snippets.InsertSnippet(selectedText);
  }

  private void txtSnippet_DocumentChange(object sender, NativeScintillaEventArgs e)
  {
    if (this.txtSnippet.AutoComplete.IsActive || !this.Visible)
      return;
    int position = this.Scintilla.Caret.Position;
    this.Scintilla.Caret.Goto(0);
    this.txtSnippet.AutoComplete.Show(0, this._snippetList);
    this.Scintilla.Caret.Goto(position);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.toolTip = new ToolTip(this.components);
    this.lblSnippet = new Label();
    this.txtSnippet = new Scintilla();
    this.txtSnippet.BeginInit();
    this.SuspendLayout();
    this.lblSnippet.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
    this.lblSnippet.Location = new Point(0, 1);
    this.lblSnippet.Name = "lblSnippet";
    this.lblSnippet.Size = new Size(94, 13);
    this.lblSnippet.TabIndex = 6;
    this.lblSnippet.Text = "Choose Snippet";
    this.lblSnippet.TextAlign = ContentAlignment.MiddleLeft;
    this.txtSnippet.AutoComplete.AutoHide = false;
    this.txtSnippet.AutoComplete.AutomaticLengthEntered = false;
    this.txtSnippet.AutoComplete.CancelAtStart = false;
    this.txtSnippet.AutoComplete.IsCaseSensitive = false;
    this.txtSnippet.AutoComplete.ListString = "";
    this.txtSnippet.BackColor = Color.LightSteelBlue;
    this.txtSnippet.CallTip.BackColor = Color.LightSteelBlue;
    this.txtSnippet.CurrentPos = 0;
    this.txtSnippet.DocumentNavigation.IsEnabled = false;
    this.txtSnippet.Folding.IsEnabled = false;
    this.txtSnippet.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.txtSnippet.Location = new Point(95, 1);
    this.txtSnippet.Margins.Left = 0;
    this.txtSnippet.Margins.Margin1.Width = 0;
    this.txtSnippet.MatchBraces = false;
    this.txtSnippet.Name = "txtSnippet";
    this.txtSnippet.Printing.PageSettings.Color = false;
    this.txtSnippet.Scrolling.ScrollBars = ScrollBars.None;
    this.txtSnippet.Size = new Size(177, 196);
    this.txtSnippet.Styles.CallTip.BackColor = Color.LightSteelBlue;
    this.txtSnippet.TabIndex = 1;
    this.txtSnippet.UseBackColor = true;
    this.txtSnippet.UseFont = true;
    this.txtSnippet.KeyDown += new KeyEventHandler(this.txtSnippet_KeyDown);
    this.txtSnippet.AutoCompleteAccepted += new EventHandler<AutoCompleteAcceptedEventArgs>(this.txtSnippet_AutoCompleteAccepted);
    this.txtSnippet.DocumentChange += new EventHandler<NativeScintillaEventArgs>(this.txtSnippet_DocumentChange);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.BackColor = Color.LightSteelBlue;
    this.BorderStyle = BorderStyle.FixedSingle;
    this.Controls.Add((Control) this.txtSnippet);
    this.Controls.Add((Control) this.lblSnippet);
    this.ForeColor = Color.FromArgb(64 /*0x40*/, 64 /*0x40*/, 64 /*0x40*/);
    this.Name = nameof (SnippetChooser);
    this.Size = new Size(285, 17);
    this.Load += new EventHandler(this.SnippetChooser_Load);
    this.txtSnippet.EndInit();
    this.ResumeLayout(false);
  }
}
