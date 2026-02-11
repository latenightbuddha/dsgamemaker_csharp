using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public class GoToDialog : Form
{
  private IContainer components;
  private Label lblCurrentLine;
  private TextBox txtCurrentLine;
  private ErrorProvider err;
  private Button btnCancel;
  private Button btnOK;
  private TextBox txtGotoLine;
  private Label lblGotoLine;
  private TextBox txtMaxLine;
  private Label lblMaxLine;
  private Scintilla _scintilla;
  private int _currentLineNumber;
  private int _maximumLineNumber;
  private int _gotoLineNumber;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.lblCurrentLine = new Label();
    this.txtCurrentLine = new TextBox();
    this.err = new ErrorProvider(this.components);
    this.txtMaxLine = new TextBox();
    this.lblMaxLine = new Label();
    this.txtGotoLine = new TextBox();
    this.lblGotoLine = new Label();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    ((ISupportInitialize) this.err).BeginInit();
    this.SuspendLayout();
    this.lblCurrentLine.AutoSize = true;
    this.lblCurrentLine.Location = new Point(9, 13);
    this.lblCurrentLine.Name = "lblCurrentLine";
    this.lblCurrentLine.Size = new Size(102, 13);
    this.lblCurrentLine.TabIndex = 0;
    this.lblCurrentLine.Text = "&Current line number";
    this.txtCurrentLine.Location = new Point(132, 8);
    this.txtCurrentLine.Name = "txtCurrentLine";
    this.txtCurrentLine.ReadOnly = true;
    this.txtCurrentLine.Size = new Size(63 /*0x3F*/, 21);
    this.txtCurrentLine.TabIndex = 1;
    this.err.ContainerControl = (ContainerControl) this;
    this.txtMaxLine.Location = new Point(132, 33);
    this.txtMaxLine.Name = "txtMaxLine";
    this.txtMaxLine.ReadOnly = true;
    this.txtMaxLine.Size = new Size(63 /*0x3F*/, 21);
    this.txtMaxLine.TabIndex = 3;
    this.lblMaxLine.AutoSize = true;
    this.lblMaxLine.Location = new Point(9, 37);
    this.lblMaxLine.Name = "lblMaxLine";
    this.lblMaxLine.Size = new Size(117, 13);
    this.lblMaxLine.TabIndex = 2;
    this.lblMaxLine.Text = "&Maxmimum line number";
    this.txtGotoLine.Location = new Point(132, 58);
    this.txtGotoLine.Name = "txtGotoLine";
    this.txtGotoLine.Size = new Size(63 /*0x3F*/, 21);
    this.txtGotoLine.TabIndex = 5;
    this.lblGotoLine.AutoSize = true;
    this.lblGotoLine.Location = new Point(9, 61);
    this.lblGotoLine.Name = "lblGotoLine";
    this.lblGotoLine.Size = new Size(91, 13);
    this.lblGotoLine.TabIndex = 4;
    this.lblGotoLine.Text = "&Go to line number";
    this.btnOK.Location = new Point(39, 85);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 6;
    this.btnOK.Text = "OK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(120, 85);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 7;
    this.btnCancel.Text = "Cancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(204, 113);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.txtGotoLine);
    this.Controls.Add((Control) this.lblGotoLine);
    this.Controls.Add((Control) this.txtMaxLine);
    this.Controls.Add((Control) this.lblMaxLine);
    this.Controls.Add((Control) this.txtCurrentLine);
    this.Controls.Add((Control) this.lblCurrentLine);
    this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (GoToDialog);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Go To Line";
    this.Load += new EventHandler(this.GoToDialog_Load);
    ((ISupportInitialize) this.err).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Scintilla Scintilla
  {
    get => this._scintilla;
    set => this._scintilla = value;
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CurrentLineNumber
  {
    get => this._currentLineNumber;
    set => this._currentLineNumber = value;
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MaximumLineNumber
  {
    get => this._maximumLineNumber;
    set => this._maximumLineNumber = value;
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int GotoLineNumber
  {
    get => this._gotoLineNumber;
    set => this._gotoLineNumber = value;
  }

  public GoToDialog() => this.InitializeComponent();

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (int.TryParse(this.txtGotoLine.Text, out this._gotoLineNumber))
    {
      --this._gotoLineNumber;
      if (this._gotoLineNumber < 0 || this._gotoLineNumber >= this._maximumLineNumber)
        this.err.SetError((Control) this.txtGotoLine, "Go to line # must be greater than 0 and less than " + (this._maximumLineNumber + 1).ToString());
      else
        this.DialogResult = DialogResult.OK;
    }
    else
      this.err.SetError((Control) this.txtGotoLine, "Go to line # must be a numeric value");
  }

  protected override void OnActivated(EventArgs e)
  {
    base.OnActivated(e);
    this.moveFormAwayFromSelection();
  }

  private void GoToDialog_Load(object sender, EventArgs e)
  {
    string str = (this._currentLineNumber + 1).ToString();
    this.txtCurrentLine.Text = str;
    this.txtMaxLine.Text = this._maximumLineNumber.ToString();
    this.txtGotoLine.Text = str;
    this.txtGotoLine.Select();
  }

  private void moveFormAwayFromSelection()
  {
    if (!this.Visible)
      return;
    int position = this.Scintilla.Caret.Position;
    Point screen = this.Scintilla.PointToScreen(new Point(this.Scintilla.PointXFromPosition(position), this.Scintilla.PointYFromPosition(position)));
    if (!new Rectangle(this.Location, this.Size).Contains(screen))
      return;
    this.Location = this.Scintilla.PointToScreen(screen.Y >= Screen.PrimaryScreen.Bounds.Height / 2 ? this.Scintilla.PointToClient(new Point(this.Location.X, screen.Y - this.Height - this.Scintilla.Lines.Current.Height * 2)) : this.Scintilla.PointToClient(new Point(this.Location.X, screen.Y + this.Scintilla.Lines.Current.Height * 2)));
  }
}
