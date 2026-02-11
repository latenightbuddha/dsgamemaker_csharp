using ScintillaNet.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public class IncrementalSearcher : UserControl
{
  private IContainer components;
  private Label lblFind;
  private TextBox txtFind;
  private Button btnNext;
  private Button brnPrevious;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button btnHighlightAll;
  private ToolTip toolTip;
  private Button btnClearHighlights;
  private Scintilla _scintilla;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.lblFind = new Label();
    this.txtFind = new TextBox();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this.btnNext = new Button();
    this.brnPrevious = new Button();
    this.btnHighlightAll = new Button();
    this.btnClearHighlights = new Button();
    this.toolTip = new ToolTip(this.components);
    this.flowLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    this.lblFind.AutoSize = true;
    this.lblFind.Dock = DockStyle.Left;
    this.lblFind.Location = new Point(0, 0);
    this.lblFind.Margin = new Padding(0, 0, 3, 0);
    this.lblFind.Name = "lblFind";
    this.lblFind.Size = new Size(27, 22);
    this.lblFind.TabIndex = 0;
    this.lblFind.Text = "&Find";
    this.lblFind.TextAlign = ContentAlignment.MiddleLeft;
    this.txtFind.Location = new Point(33, 1);
    this.txtFind.Margin = new Padding(3, 1, 0, 0);
    this.txtFind.Name = "txtFind";
    this.txtFind.Size = new Size(135, 20);
    this.txtFind.TabIndex = 1;
    this.txtFind.TextChanged += new EventHandler(this.txtFind_TextChanged);
    this.txtFind.KeyDown += new KeyEventHandler(this.txtFind_KeyDown);
    this.flowLayoutPanel1.AutoSize = true;
    this.flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.flowLayoutPanel1.Controls.Add((Control) this.lblFind);
    this.flowLayoutPanel1.Controls.Add((Control) this.txtFind);
    this.flowLayoutPanel1.Controls.Add((Control) this.btnNext);
    this.flowLayoutPanel1.Controls.Add((Control) this.brnPrevious);
    this.flowLayoutPanel1.Controls.Add((Control) this.btnHighlightAll);
    this.flowLayoutPanel1.Controls.Add((Control) this.btnClearHighlights);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.Location = new Point(0, 0);
    this.flowLayoutPanel1.Margin = new Padding(0);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(259, 22);
    this.flowLayoutPanel1.TabIndex = 4;
    this.flowLayoutPanel1.WrapContents = false;
    this.btnNext.AutoSize = true;
    this.btnNext.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.btnNext.FlatAppearance.BorderSize = 0;
    this.btnNext.Image = (Image) Resources.GoToNextMessage___Copy;
    this.btnNext.Location = new Point(171, 0);
    this.btnNext.Margin = new Padding(3, 0, 0, 0);
    this.btnNext.Name = "btnNext";
    this.btnNext.Size = new Size(22, 22);
    this.btnNext.TabIndex = 2;
    this.toolTip.SetToolTip((Control) this.btnNext, "Find Next");
    this.btnNext.UseVisualStyleBackColor = true;
    this.btnNext.Click += new EventHandler(this.btnNext_Click);
    this.brnPrevious.AutoSize = true;
    this.brnPrevious.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.brnPrevious.FlatAppearance.BorderSize = 0;
    this.brnPrevious.Image = (Image) Resources.GoToPreviousMessage;
    this.brnPrevious.Location = new Point(193, 0);
    this.brnPrevious.Margin = new Padding(0);
    this.brnPrevious.Name = "brnPrevious";
    this.brnPrevious.Size = new Size(22, 22);
    this.brnPrevious.TabIndex = 3;
    this.toolTip.SetToolTip((Control) this.brnPrevious, "Find Previous");
    this.brnPrevious.UseVisualStyleBackColor = true;
    this.brnPrevious.Click += new EventHandler(this.brnPrevious_Click);
    this.btnHighlightAll.AutoSize = true;
    this.btnHighlightAll.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.btnHighlightAll.FlatAppearance.BorderSize = 0;
    this.btnHighlightAll.Font = new Font("Microsoft Sans Serif", 1.5f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.btnHighlightAll.ForeColor = Color.SkyBlue;
    this.btnHighlightAll.Image = (Image) Resources.LineColorHS;
    this.btnHighlightAll.Location = new Point(215, 0);
    this.btnHighlightAll.Margin = new Padding(0);
    this.btnHighlightAll.Name = "btnHighlightAll";
    this.btnHighlightAll.Size = new Size(22, 22);
    this.btnHighlightAll.TabIndex = 4;
    this.btnHighlightAll.Text = "&h";
    this.toolTip.SetToolTip((Control) this.btnHighlightAll, "Highlight All Matches (ALT+H)");
    this.btnHighlightAll.UseVisualStyleBackColor = true;
    this.btnHighlightAll.Click += new EventHandler(this.btnHighlightAll_Click);
    this.btnClearHighlights.AutoSize = true;
    this.btnClearHighlights.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.btnClearHighlights.FlatAppearance.BorderSize = 0;
    this.btnClearHighlights.Font = new Font("Microsoft Sans Serif", 1.5f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.btnClearHighlights.Image = (Image) Resources.DeleteHS;
    this.btnClearHighlights.Location = new Point(237, 0);
    this.btnClearHighlights.Margin = new Padding(0);
    this.btnClearHighlights.Name = "btnClearHighlights";
    this.btnClearHighlights.Size = new Size(22, 22);
    this.btnClearHighlights.TabIndex = 5;
    this.btnClearHighlights.Text = "&j";
    this.toolTip.SetToolTip((Control) this.btnClearHighlights, "Clear Highlights (ALT+J)");
    this.btnClearHighlights.UseVisualStyleBackColor = true;
    this.btnClearHighlights.Click += new EventHandler(this.btnClearHighlights_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.BackColor = Color.LightSteelBlue;
    this.Controls.Add((Control) this.flowLayoutPanel1);
    this.Margin = new Padding(0);
    this.Name = nameof (IncrementalSearcher);
    this.Size = new Size(259, 22);
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Scintilla Scintilla
  {
    get => this._scintilla;
    set => this._scintilla = value;
  }

  public IncrementalSearcher() => this.InitializeComponent();

  protected override void OnLeave(EventArgs e)
  {
    this.OnLostFocus(e);
    this.Hide();
  }

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    this.txtFind.Text = string.Empty;
    this.txtFind.BackColor = SystemColors.Window;
    this.moveFormAwayFromSelection();
    if (this.Visible)
      this.txtFind.Focus();
    else
      this.Scintilla.Focus();
  }

  protected override void OnCreateControl()
  {
    base.OnCreateControl();
    this.moveFormAwayFromSelection();
    this.txtFind.Focus();
  }

  private void txtFind_TextChanged(object sender, EventArgs e)
  {
    this.txtFind.BackColor = SystemColors.Window;
    if (this.txtFind.Text == string.Empty)
      return;
    int num = Math.Min(this.Scintilla.Caret.Position, this.Scintilla.Caret.Anchor);
    Range range = this.Scintilla.FindReplace.Find(num, this.Scintilla.TextLength, this.txtFind.Text, this.Scintilla.FindReplace.Window.GetSearchFlags()) ?? this.Scintilla.FindReplace.Find(0, num, this.txtFind.Text, this.Scintilla.FindReplace.Window.GetSearchFlags());
    if (range != null)
      range.Select();
    else
      this.txtFind.BackColor = Color.Tomato;
    this.moveFormAwayFromSelection();
  }

  private void btnNext_Click(object sender, EventArgs e) => this.findNext();

  private void findNext()
  {
    if (this.txtFind.Text == string.Empty)
      return;
    this.Scintilla.FindReplace.FindNext(this.txtFind.Text, true, this.Scintilla.FindReplace.Window.GetSearchFlags())?.Select();
    this.moveFormAwayFromSelection();
  }

  private void brnPrevious_Click(object sender, EventArgs e) => this.findPrevious();

  private void findPrevious()
  {
    if (this.txtFind.Text == string.Empty)
      return;
    this.Scintilla.FindReplace.FindPrevious(this.txtFind.Text, true, this.Scintilla.FindReplace.Window.GetSearchFlags())?.Select();
    this.moveFormAwayFromSelection();
  }

  private void txtFind_KeyDown(object sender, KeyEventArgs e)
  {
    switch (e.KeyCode)
    {
      case Keys.Return:
      case Keys.Down:
        this.findNext();
        e.Handled = true;
        break;
      case Keys.Escape:
        this.Hide();
        break;
      case Keys.Up:
        this.findPrevious();
        e.Handled = true;
        break;
    }
  }

  private void btnHighlightAll_Click(object sender, EventArgs e)
  {
    if (this.txtFind.Text == string.Empty)
      return;
    this.Scintilla.FindReplace.HighlightAll((IList<Range>) this.Scintilla.FindReplace.FindAll(this.txtFind.Text));
  }

  private void btnClearHighlights_Click(object sender, EventArgs e)
  {
    this.Scintilla.FindReplace.ClearAllHighlights();
  }

  public void moveFormAwayFromSelection()
  {
    if (!this.Visible)
      return;
    int position = this.Scintilla.Caret.Position;
    Point pt = new Point(this.Scintilla.PointXFromPosition(position), this.Scintilla.PointYFromPosition(position));
    if (!new Rectangle(this.Location, this.Size).Contains(pt))
      return;
    this.Location = pt.Y >= Screen.PrimaryScreen.Bounds.Height / 2 ? new Point(this.Location.X, pt.Y - this.Height - this.Scintilla.Lines.Current.Height * 2) : new Point(this.Location.X, pt.Y + this.Scintilla.Lines.Current.Height * 2);
  }
}
