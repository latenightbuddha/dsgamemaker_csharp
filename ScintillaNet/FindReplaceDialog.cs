using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public class FindReplaceDialog : Form
{
  private IContainer components;
  private TabPage tpgFind;
  private TabPage tpgReplace;
  internal TabControl tabAll;
  private Label lblFindF;
  private Label lblSearchTypeF;
  private RadioButton rdoRegexF;
  private RadioButton rdoStandardF;
  private GroupBox grpOptionsF;
  private Panel pnlRegexpOptionsF;
  private Panel pnlStandardOptionsF;
  private CheckBox chkWordStartF;
  private CheckBox chkWholeWordF;
  private CheckBox chkMatchCaseF;
  private CheckBox chkEcmaScriptF;
  private CheckBox chkCultureInvariantF;
  private CheckBox chkCompiledF;
  private CheckBox chkRightToLeftF;
  private CheckBox chkMultilineF;
  private CheckBox chkIgnorePatternWhitespaceF;
  private CheckBox chkIgnoreCaseF;
  private CheckBox chkExplicitCaptureF;
  private CheckBox chkSinglelineF;
  private Button btnFindPrevious;
  private Button btnFindNext;
  private CheckBox chkWrapF;
  private Button btnClear;
  private Button btnFindAll;
  private CheckBox chkHighlightMatches;
  private CheckBox chkMarkLine;
  private StatusStrip statusStrip;
  private ComboBox cboReplace;
  private Label lblReplace;
  private CheckBox chkWrapR;
  private Button btnReplacePrevious;
  private Button btnReplaceNext;
  private GroupBox grdOptionsR;
  private Panel pnlRegexpOptionsR;
  private CheckBox chkSinglelineR;
  private CheckBox chkRightToLeftR;
  private CheckBox chkMultilineR;
  private CheckBox chkIgnorePatternWhitespaceR;
  private CheckBox chkIgnoreCaseR;
  private CheckBox chkExplicitCaptureR;
  private CheckBox chkEcmaScriptR;
  private CheckBox chkCultureInvariantR;
  private CheckBox chkCompiledR;
  private Panel pnlStandardOptionsR;
  private CheckBox chkWordStartR;
  private CheckBox chkWholeWordR;
  private CheckBox chkMatchCaseR;
  private Label lblSearchTypeR;
  private RadioButton rdoRegexR;
  private RadioButton rdoStandardR;
  private Label lblFindR;
  private Button btnReplaceAll;
  private ToolStripStatusLabel lblStatus;
  public GroupBox grpFindAll;
  internal CheckBox chkSearchSelectionF;
  internal CheckBox chkSearchSelectionR;
  internal ComboBox cboFindF;
  internal ComboBox cboFindR;
  private List<string> _mruFind;
  private List<string> _mruReplace;
  private int _mruMaxCount = 10;
  private Scintilla _scintilla;
  private Range _searchRange;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tabAll = new TabControl();
    this.tpgFind = new TabPage();
    this.grpFindAll = new GroupBox();
    this.btnClear = new Button();
    this.btnFindAll = new Button();
    this.chkHighlightMatches = new CheckBox();
    this.chkMarkLine = new CheckBox();
    this.chkSearchSelectionF = new CheckBox();
    this.chkWrapF = new CheckBox();
    this.btnFindPrevious = new Button();
    this.btnFindNext = new Button();
    this.cboFindF = new ComboBox();
    this.grpOptionsF = new GroupBox();
    this.pnlStandardOptionsF = new Panel();
    this.chkWordStartF = new CheckBox();
    this.chkWholeWordF = new CheckBox();
    this.chkMatchCaseF = new CheckBox();
    this.pnlRegexpOptionsF = new Panel();
    this.chkSinglelineF = new CheckBox();
    this.chkRightToLeftF = new CheckBox();
    this.chkMultilineF = new CheckBox();
    this.chkIgnorePatternWhitespaceF = new CheckBox();
    this.chkIgnoreCaseF = new CheckBox();
    this.chkExplicitCaptureF = new CheckBox();
    this.chkEcmaScriptF = new CheckBox();
    this.chkCultureInvariantF = new CheckBox();
    this.chkCompiledF = new CheckBox();
    this.lblSearchTypeF = new Label();
    this.rdoRegexF = new RadioButton();
    this.rdoStandardF = new RadioButton();
    this.lblFindF = new Label();
    this.tpgReplace = new TabPage();
    this.btnReplaceAll = new Button();
    this.cboReplace = new ComboBox();
    this.lblReplace = new Label();
    this.chkSearchSelectionR = new CheckBox();
    this.chkWrapR = new CheckBox();
    this.btnReplacePrevious = new Button();
    this.btnReplaceNext = new Button();
    this.cboFindR = new ComboBox();
    this.grdOptionsR = new GroupBox();
    this.pnlStandardOptionsR = new Panel();
    this.chkWordStartR = new CheckBox();
    this.chkWholeWordR = new CheckBox();
    this.chkMatchCaseR = new CheckBox();
    this.pnlRegexpOptionsR = new Panel();
    this.chkSinglelineR = new CheckBox();
    this.chkRightToLeftR = new CheckBox();
    this.chkMultilineR = new CheckBox();
    this.chkIgnorePatternWhitespaceR = new CheckBox();
    this.chkIgnoreCaseR = new CheckBox();
    this.chkExplicitCaptureR = new CheckBox();
    this.chkEcmaScriptR = new CheckBox();
    this.chkCultureInvariantR = new CheckBox();
    this.chkCompiledR = new CheckBox();
    this.lblSearchTypeR = new Label();
    this.rdoRegexR = new RadioButton();
    this.rdoStandardR = new RadioButton();
    this.lblFindR = new Label();
    this.statusStrip = new StatusStrip();
    this.lblStatus = new ToolStripStatusLabel();
    this.tabAll.SuspendLayout();
    this.tpgFind.SuspendLayout();
    this.grpFindAll.SuspendLayout();
    this.grpOptionsF.SuspendLayout();
    this.pnlStandardOptionsF.SuspendLayout();
    this.pnlRegexpOptionsF.SuspendLayout();
    this.tpgReplace.SuspendLayout();
    this.grdOptionsR.SuspendLayout();
    this.pnlStandardOptionsR.SuspendLayout();
    this.pnlRegexpOptionsR.SuspendLayout();
    this.statusStrip.SuspendLayout();
    this.SuspendLayout();
    this.tabAll.Controls.Add((Control) this.tpgFind);
    this.tabAll.Controls.Add((Control) this.tpgReplace);
    this.tabAll.Dock = DockStyle.Fill;
    this.tabAll.Location = new Point(0, 0);
    this.tabAll.Name = "tabAll";
    this.tabAll.SelectedIndex = 0;
    this.tabAll.Size = new Size(499, 291);
    this.tabAll.TabIndex = 0;
    this.tabAll.SelectedIndexChanged += new EventHandler(this.tabAll_SelectedIndexChanged);
    this.tpgFind.Controls.Add((Control) this.grpFindAll);
    this.tpgFind.Controls.Add((Control) this.chkSearchSelectionF);
    this.tpgFind.Controls.Add((Control) this.chkWrapF);
    this.tpgFind.Controls.Add((Control) this.btnFindPrevious);
    this.tpgFind.Controls.Add((Control) this.btnFindNext);
    this.tpgFind.Controls.Add((Control) this.cboFindF);
    this.tpgFind.Controls.Add((Control) this.grpOptionsF);
    this.tpgFind.Controls.Add((Control) this.lblSearchTypeF);
    this.tpgFind.Controls.Add((Control) this.rdoRegexF);
    this.tpgFind.Controls.Add((Control) this.rdoStandardF);
    this.tpgFind.Controls.Add((Control) this.lblFindF);
    this.tpgFind.Location = new Point(4, 22);
    this.tpgFind.Name = "tpgFind";
    this.tpgFind.Padding = new Padding(3);
    this.tpgFind.Size = new Size(491, 265);
    this.tpgFind.TabIndex = 0;
    this.tpgFind.Text = "Find";
    this.tpgFind.UseVisualStyleBackColor = true;
    this.grpFindAll.Controls.Add((Control) this.btnClear);
    this.grpFindAll.Controls.Add((Control) this.btnFindAll);
    this.grpFindAll.Controls.Add((Control) this.chkHighlightMatches);
    this.grpFindAll.Controls.Add((Control) this.chkMarkLine);
    this.grpFindAll.Location = new Point(5, 176 /*0xB0*/);
    this.grpFindAll.Name = "grpFindAll";
    this.grpFindAll.Size = new Size(209, 65);
    this.grpFindAll.TabIndex = 8;
    this.grpFindAll.TabStop = false;
    this.grpFindAll.Text = "Find All";
    this.btnClear.Location = new Point(116, 37);
    this.btnClear.Name = "btnClear";
    this.btnClear.Size = new Size(88, 23);
    this.btnClear.TabIndex = 3;
    this.btnClear.Text = "C&lear";
    this.btnClear.UseVisualStyleBackColor = true;
    this.btnClear.Click += new EventHandler(this.btnClear_Click);
    this.btnFindAll.Location = new Point(116, 13);
    this.btnFindAll.Name = "btnFindAll";
    this.btnFindAll.Size = new Size(88, 23);
    this.btnFindAll.TabIndex = 2;
    this.btnFindAll.Text = "Find &All";
    this.btnFindAll.UseVisualStyleBackColor = true;
    this.btnFindAll.Click += new EventHandler(this.btnFindAll_Click);
    this.chkHighlightMatches.AutoSize = true;
    this.chkHighlightMatches.Location = new Point(6, 37);
    this.chkHighlightMatches.Name = "chkHighlightMatches";
    this.chkHighlightMatches.Size = new Size(110, 17);
    this.chkHighlightMatches.TabIndex = 1;
    this.chkHighlightMatches.Text = "&Highlight Matches";
    this.chkHighlightMatches.UseVisualStyleBackColor = true;
    this.chkMarkLine.AutoSize = true;
    this.chkMarkLine.Location = new Point(6, 20);
    this.chkMarkLine.Name = "chkMarkLine";
    this.chkMarkLine.Size = new Size(71, 17);
    this.chkMarkLine.TabIndex = 0;
    this.chkMarkLine.Text = "&Mark Line";
    this.chkMarkLine.UseVisualStyleBackColor = true;
    this.chkSearchSelectionF.AutoSize = true;
    this.chkSearchSelectionF.Location = new Point(251, 72);
    this.chkSearchSelectionF.Name = "chkSearchSelectionF";
    this.chkSearchSelectionF.Size = new Size(105, 17);
    this.chkSearchSelectionF.TabIndex = 6;
    this.chkSearchSelectionF.Text = "Search Selection";
    this.chkSearchSelectionF.UseVisualStyleBackColor = true;
    this.chkWrapF.AutoSize = true;
    this.chkWrapF.Checked = true;
    this.chkWrapF.CheckState = CheckState.Checked;
    this.chkWrapF.Location = new Point(251, 55);
    this.chkWrapF.Name = "chkWrapF";
    this.chkWrapF.Size = new Size(52, 17);
    this.chkWrapF.TabIndex = 5;
    this.chkWrapF.Text = "&Wrap";
    this.chkWrapF.UseVisualStyleBackColor = true;
    this.btnFindPrevious.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnFindPrevious.Location = new Point(375, 188);
    this.btnFindPrevious.Name = "btnFindPrevious";
    this.btnFindPrevious.Size = new Size(107, 23);
    this.btnFindPrevious.TabIndex = 9;
    this.btnFindPrevious.Text = "Find &Previous";
    this.btnFindPrevious.UseVisualStyleBackColor = true;
    this.btnFindPrevious.Click += new EventHandler(this.btnFindPrevious_Click);
    this.btnFindNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnFindNext.Location = new Point(375, 212);
    this.btnFindNext.Name = "btnFindNext";
    this.btnFindNext.Size = new Size(107, 23);
    this.btnFindNext.TabIndex = 10;
    this.btnFindNext.Text = "Find &Next";
    this.btnFindNext.UseVisualStyleBackColor = true;
    this.btnFindNext.Click += new EventHandler(this.btnFindNext_Click);
    this.cboFindF.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cboFindF.FormattingEnabled = true;
    this.cboFindF.Location = new Point(59, 6);
    this.cboFindF.Name = "cboFindF";
    this.cboFindF.Size = new Size(424, 21);
    this.cboFindF.TabIndex = 1;
    this.grpOptionsF.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grpOptionsF.Controls.Add((Control) this.pnlStandardOptionsF);
    this.grpOptionsF.Controls.Add((Control) this.pnlRegexpOptionsF);
    this.grpOptionsF.Location = new Point(4, 94);
    this.grpOptionsF.Name = "grpOptionsF";
    this.grpOptionsF.Size = new Size(481, 77);
    this.grpOptionsF.TabIndex = 7;
    this.grpOptionsF.TabStop = false;
    this.grpOptionsF.Text = "Options";
    this.pnlStandardOptionsF.Controls.Add((Control) this.chkWordStartF);
    this.pnlStandardOptionsF.Controls.Add((Control) this.chkWholeWordF);
    this.pnlStandardOptionsF.Controls.Add((Control) this.chkMatchCaseF);
    this.pnlStandardOptionsF.Dock = DockStyle.Fill;
    this.pnlStandardOptionsF.Location = new Point(3, 17);
    this.pnlStandardOptionsF.Name = "pnlStandardOptionsF";
    this.pnlStandardOptionsF.Size = new Size(475, 57);
    this.pnlStandardOptionsF.TabIndex = 0;
    this.chkWordStartF.AutoSize = true;
    this.chkWordStartF.Location = new Point(10, 37);
    this.chkWordStartF.Name = "chkWordStartF";
    this.chkWordStartF.Size = new Size(79, 17);
    this.chkWordStartF.TabIndex = 2;
    this.chkWordStartF.Text = "W&ord Start";
    this.chkWordStartF.UseVisualStyleBackColor = true;
    this.chkWholeWordF.AutoSize = true;
    this.chkWholeWordF.Location = new Point(10, 20);
    this.chkWholeWordF.Name = "chkWholeWordF";
    this.chkWholeWordF.Size = new Size(85, 17);
    this.chkWholeWordF.TabIndex = 1;
    this.chkWholeWordF.Text = "Whole Wor&d";
    this.chkWholeWordF.UseVisualStyleBackColor = true;
    this.chkMatchCaseF.AutoSize = true;
    this.chkMatchCaseF.Location = new Point(10, 3);
    this.chkMatchCaseF.Name = "chkMatchCaseF";
    this.chkMatchCaseF.Size = new Size(82, 17);
    this.chkMatchCaseF.TabIndex = 0;
    this.chkMatchCaseF.Text = "Match &Case";
    this.chkMatchCaseF.UseVisualStyleBackColor = true;
    this.pnlRegexpOptionsF.Controls.Add((Control) this.chkSinglelineF);
    this.pnlRegexpOptionsF.Controls.Add((Control) this.chkRightToLeftF);
    this.pnlRegexpOptionsF.Controls.Add((Control) this.chkMultilineF);
    this.pnlRegexpOptionsF.Controls.Add((Control) this.chkIgnorePatternWhitespaceF);
    this.pnlRegexpOptionsF.Controls.Add((Control) this.chkIgnoreCaseF);
    this.pnlRegexpOptionsF.Controls.Add((Control) this.chkExplicitCaptureF);
    this.pnlRegexpOptionsF.Controls.Add((Control) this.chkEcmaScriptF);
    this.pnlRegexpOptionsF.Controls.Add((Control) this.chkCultureInvariantF);
    this.pnlRegexpOptionsF.Controls.Add((Control) this.chkCompiledF);
    this.pnlRegexpOptionsF.Dock = DockStyle.Fill;
    this.pnlRegexpOptionsF.Location = new Point(3, 17);
    this.pnlRegexpOptionsF.Name = "pnlRegexpOptionsF";
    this.pnlRegexpOptionsF.Size = new Size(475, 57);
    this.pnlRegexpOptionsF.TabIndex = 1;
    this.chkSinglelineF.AutoSize = true;
    this.chkSinglelineF.Location = new Point(279, 37);
    this.chkSinglelineF.Name = "chkSinglelineF";
    this.chkSinglelineF.Size = new Size(70, 17);
    this.chkSinglelineF.TabIndex = 8;
    this.chkSinglelineF.Text = "Singleline";
    this.chkSinglelineF.UseVisualStyleBackColor = true;
    this.chkRightToLeftF.AutoSize = true;
    this.chkRightToLeftF.Location = new Point(279, 20);
    this.chkRightToLeftF.Name = "chkRightToLeftF";
    this.chkRightToLeftF.Size = new Size(88, 17);
    this.chkRightToLeftF.TabIndex = 5;
    this.chkRightToLeftF.Text = "Right To Left";
    this.chkRightToLeftF.UseVisualStyleBackColor = true;
    this.chkMultilineF.AutoSize = true;
    this.chkMultilineF.Location = new Point(279, 3);
    this.chkMultilineF.Name = "chkMultilineF";
    this.chkMultilineF.Size = new Size(64 /*0x40*/, 17);
    this.chkMultilineF.TabIndex = 2;
    this.chkMultilineF.Text = "Multiline";
    this.chkMultilineF.UseVisualStyleBackColor = true;
    this.chkIgnorePatternWhitespaceF.AutoSize = true;
    this.chkIgnorePatternWhitespaceF.Location = new Point(113, 37);
    this.chkIgnorePatternWhitespaceF.Name = "chkIgnorePatternWhitespaceF";
    this.chkIgnorePatternWhitespaceF.Size = new Size(156, 17);
    this.chkIgnorePatternWhitespaceF.TabIndex = 7;
    this.chkIgnorePatternWhitespaceF.Text = "I&gnore Pattern Whitespace";
    this.chkIgnorePatternWhitespaceF.UseVisualStyleBackColor = true;
    this.chkIgnoreCaseF.AutoSize = true;
    this.chkIgnoreCaseF.Location = new Point(113, 20);
    this.chkIgnoreCaseF.Name = "chkIgnoreCaseF";
    this.chkIgnoreCaseF.Size = new Size(85, 17);
    this.chkIgnoreCaseF.TabIndex = 4;
    this.chkIgnoreCaseF.Text = "&Ignore Case";
    this.chkIgnoreCaseF.UseVisualStyleBackColor = true;
    this.chkExplicitCaptureF.AutoSize = true;
    this.chkExplicitCaptureF.Location = new Point(113, 3);
    this.chkExplicitCaptureF.Name = "chkExplicitCaptureF";
    this.chkExplicitCaptureF.Size = new Size(101, 17);
    this.chkExplicitCaptureF.TabIndex = 1;
    this.chkExplicitCaptureF.Text = "E&xplicit Capture";
    this.chkExplicitCaptureF.UseVisualStyleBackColor = true;
    this.chkEcmaScriptF.AutoSize = true;
    this.chkEcmaScriptF.Location = new Point(3, 37);
    this.chkEcmaScriptF.Name = "chkEcmaScriptF";
    this.chkEcmaScriptF.Size = new Size(84, 17);
    this.chkEcmaScriptF.TabIndex = 6;
    this.chkEcmaScriptF.Text = "ECMA Script";
    this.chkEcmaScriptF.UseVisualStyleBackColor = true;
    this.chkEcmaScriptF.CheckedChanged += new EventHandler(this.chkEcmaScript_CheckedChanged);
    this.chkCultureInvariantF.AutoSize = true;
    this.chkCultureInvariantF.Location = new Point(3, 20);
    this.chkCultureInvariantF.Name = "chkCultureInvariantF";
    this.chkCultureInvariantF.Size = new Size(108, 17);
    this.chkCultureInvariantF.TabIndex = 3;
    this.chkCultureInvariantF.Text = "C&ulture Invariant";
    this.chkCultureInvariantF.UseVisualStyleBackColor = true;
    this.chkCompiledF.AutoSize = true;
    this.chkCompiledF.Location = new Point(3, 3);
    this.chkCompiledF.Name = "chkCompiledF";
    this.chkCompiledF.Size = new Size(69, 17);
    this.chkCompiledF.TabIndex = 0;
    this.chkCompiledF.Text = "&Compiled";
    this.chkCompiledF.UseVisualStyleBackColor = true;
    this.lblSearchTypeF.AutoSize = true;
    this.lblSearchTypeF.Location = new Point(8, 52);
    this.lblSearchTypeF.Name = "lblSearchTypeF";
    this.lblSearchTypeF.Size = new Size(67, 13);
    this.lblSearchTypeF.TabIndex = 2;
    this.lblSearchTypeF.Text = "Search Type";
    this.rdoRegexF.AutoSize = true;
    this.rdoRegexF.Location = new Point(102, 71);
    this.rdoRegexF.Name = "rdoRegexF";
    this.rdoRegexF.Size = new Size(117, 17);
    this.rdoRegexF.TabIndex = 4;
    this.rdoRegexF.Text = "Regular &Expression";
    this.rdoRegexF.UseVisualStyleBackColor = true;
    this.rdoRegexF.CheckedChanged += new EventHandler(this.rdoStandardF_CheckedChanged);
    this.rdoStandardF.AutoSize = true;
    this.rdoStandardF.Checked = true;
    this.rdoStandardF.Location = new Point(27, 71);
    this.rdoStandardF.Name = "rdoStandardF";
    this.rdoStandardF.Size = new Size(69, 17);
    this.rdoStandardF.TabIndex = 3;
    this.rdoStandardF.TabStop = true;
    this.rdoStandardF.Text = "&Standard";
    this.rdoStandardF.UseVisualStyleBackColor = true;
    this.rdoStandardF.CheckedChanged += new EventHandler(this.rdoStandardF_CheckedChanged);
    this.lblFindF.AutoSize = true;
    this.lblFindF.Location = new Point(8, 10);
    this.lblFindF.Name = "lblFindF";
    this.lblFindF.Size = new Size(27, 13);
    this.lblFindF.TabIndex = 0;
    this.lblFindF.Text = "&Find";
    this.tpgReplace.Controls.Add((Control) this.btnReplaceAll);
    this.tpgReplace.Controls.Add((Control) this.cboReplace);
    this.tpgReplace.Controls.Add((Control) this.lblReplace);
    this.tpgReplace.Controls.Add((Control) this.chkSearchSelectionR);
    this.tpgReplace.Controls.Add((Control) this.chkWrapR);
    this.tpgReplace.Controls.Add((Control) this.btnReplacePrevious);
    this.tpgReplace.Controls.Add((Control) this.btnReplaceNext);
    this.tpgReplace.Controls.Add((Control) this.cboFindR);
    this.tpgReplace.Controls.Add((Control) this.grdOptionsR);
    this.tpgReplace.Controls.Add((Control) this.lblSearchTypeR);
    this.tpgReplace.Controls.Add((Control) this.rdoRegexR);
    this.tpgReplace.Controls.Add((Control) this.rdoStandardR);
    this.tpgReplace.Controls.Add((Control) this.lblFindR);
    this.tpgReplace.Location = new Point(4, 22);
    this.tpgReplace.Name = "tpgReplace";
    this.tpgReplace.Padding = new Padding(3);
    this.tpgReplace.Size = new Size(491, 265);
    this.tpgReplace.TabIndex = 1;
    this.tpgReplace.Text = "Replace";
    this.tpgReplace.UseVisualStyleBackColor = true;
    this.btnReplaceAll.Location = new Point(7, 212);
    this.btnReplaceAll.Name = "btnReplaceAll";
    this.btnReplaceAll.Size = new Size(107, 23);
    this.btnReplaceAll.TabIndex = 10;
    this.btnReplaceAll.Text = "Replace &All";
    this.btnReplaceAll.UseVisualStyleBackColor = true;
    this.btnReplaceAll.Click += new EventHandler(this.btnReplaceAll_Click);
    this.cboReplace.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cboReplace.FormattingEnabled = true;
    this.cboReplace.Location = new Point(59, 28);
    this.cboReplace.Name = "cboReplace";
    this.cboReplace.Size = new Size(424, 21);
    this.cboReplace.TabIndex = 3;
    this.lblReplace.AutoSize = true;
    this.lblReplace.Location = new Point(8, 32 /*0x20*/);
    this.lblReplace.Name = "lblReplace";
    this.lblReplace.Size = new Size(45, 13);
    this.lblReplace.TabIndex = 2;
    this.lblReplace.Text = "&Replace";
    this.chkSearchSelectionR.AutoSize = true;
    this.chkSearchSelectionR.Location = new Point(251, 72);
    this.chkSearchSelectionR.Name = "chkSearchSelectionR";
    this.chkSearchSelectionR.Size = new Size(105, 17);
    this.chkSearchSelectionR.TabIndex = 8;
    this.chkSearchSelectionR.Text = "Search Selection";
    this.chkSearchSelectionR.UseVisualStyleBackColor = true;
    this.chkWrapR.AutoSize = true;
    this.chkWrapR.Checked = true;
    this.chkWrapR.CheckState = CheckState.Checked;
    this.chkWrapR.Location = new Point(251, 55);
    this.chkWrapR.Name = "chkWrapR";
    this.chkWrapR.Size = new Size(52, 17);
    this.chkWrapR.TabIndex = 7;
    this.chkWrapR.Text = "&Wrap";
    this.chkWrapR.UseVisualStyleBackColor = true;
    this.btnReplacePrevious.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnReplacePrevious.Location = new Point(375, 188);
    this.btnReplacePrevious.Name = "btnReplacePrevious";
    this.btnReplacePrevious.Size = new Size(107, 23);
    this.btnReplacePrevious.TabIndex = 11;
    this.btnReplacePrevious.Text = "Replace &Previous";
    this.btnReplacePrevious.UseVisualStyleBackColor = true;
    this.btnReplacePrevious.Click += new EventHandler(this.btnReplacePrevious_Click);
    this.btnReplaceNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnReplaceNext.Location = new Point(375, 212);
    this.btnReplaceNext.Name = "btnReplaceNext";
    this.btnReplaceNext.Size = new Size(107, 23);
    this.btnReplaceNext.TabIndex = 12;
    this.btnReplaceNext.Text = "Replace &Next";
    this.btnReplaceNext.UseVisualStyleBackColor = true;
    this.btnReplaceNext.Click += new EventHandler(this.btnReplaceNext_Click);
    this.cboFindR.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cboFindR.FormattingEnabled = true;
    this.cboFindR.Location = new Point(59, 6);
    this.cboFindR.Name = "cboFindR";
    this.cboFindR.Size = new Size(424, 21);
    this.cboFindR.TabIndex = 1;
    this.grdOptionsR.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.grdOptionsR.Controls.Add((Control) this.pnlStandardOptionsR);
    this.grdOptionsR.Controls.Add((Control) this.pnlRegexpOptionsR);
    this.grdOptionsR.Location = new Point(4, 94);
    this.grdOptionsR.Name = "grdOptionsR";
    this.grdOptionsR.Size = new Size(481, 77);
    this.grdOptionsR.TabIndex = 9;
    this.grdOptionsR.TabStop = false;
    this.grdOptionsR.Text = "Options";
    this.pnlStandardOptionsR.Controls.Add((Control) this.chkWordStartR);
    this.pnlStandardOptionsR.Controls.Add((Control) this.chkWholeWordR);
    this.pnlStandardOptionsR.Controls.Add((Control) this.chkMatchCaseR);
    this.pnlStandardOptionsR.Dock = DockStyle.Fill;
    this.pnlStandardOptionsR.Location = new Point(3, 17);
    this.pnlStandardOptionsR.Name = "pnlStandardOptionsR";
    this.pnlStandardOptionsR.Size = new Size(475, 57);
    this.pnlStandardOptionsR.TabIndex = 0;
    this.chkWordStartR.AutoSize = true;
    this.chkWordStartR.Location = new Point(10, 37);
    this.chkWordStartR.Name = "chkWordStartR";
    this.chkWordStartR.Size = new Size(79, 17);
    this.chkWordStartR.TabIndex = 2;
    this.chkWordStartR.Text = "W&ord Start";
    this.chkWordStartR.UseVisualStyleBackColor = true;
    this.chkWholeWordR.AutoSize = true;
    this.chkWholeWordR.Location = new Point(10, 20);
    this.chkWholeWordR.Name = "chkWholeWordR";
    this.chkWholeWordR.Size = new Size(85, 17);
    this.chkWholeWordR.TabIndex = 1;
    this.chkWholeWordR.Text = "Whole Wor&d";
    this.chkWholeWordR.UseVisualStyleBackColor = true;
    this.chkMatchCaseR.AutoSize = true;
    this.chkMatchCaseR.Location = new Point(10, 3);
    this.chkMatchCaseR.Name = "chkMatchCaseR";
    this.chkMatchCaseR.Size = new Size(82, 17);
    this.chkMatchCaseR.TabIndex = 0;
    this.chkMatchCaseR.Text = "Match &Case";
    this.chkMatchCaseR.UseVisualStyleBackColor = true;
    this.pnlRegexpOptionsR.Controls.Add((Control) this.chkSinglelineR);
    this.pnlRegexpOptionsR.Controls.Add((Control) this.chkRightToLeftR);
    this.pnlRegexpOptionsR.Controls.Add((Control) this.chkMultilineR);
    this.pnlRegexpOptionsR.Controls.Add((Control) this.chkIgnorePatternWhitespaceR);
    this.pnlRegexpOptionsR.Controls.Add((Control) this.chkIgnoreCaseR);
    this.pnlRegexpOptionsR.Controls.Add((Control) this.chkExplicitCaptureR);
    this.pnlRegexpOptionsR.Controls.Add((Control) this.chkEcmaScriptR);
    this.pnlRegexpOptionsR.Controls.Add((Control) this.chkCultureInvariantR);
    this.pnlRegexpOptionsR.Controls.Add((Control) this.chkCompiledR);
    this.pnlRegexpOptionsR.Dock = DockStyle.Fill;
    this.pnlRegexpOptionsR.Location = new Point(3, 17);
    this.pnlRegexpOptionsR.Name = "pnlRegexpOptionsR";
    this.pnlRegexpOptionsR.Size = new Size(475, 57);
    this.pnlRegexpOptionsR.TabIndex = 1;
    this.chkSinglelineR.AutoSize = true;
    this.chkSinglelineR.Location = new Point(279, 37);
    this.chkSinglelineR.Name = "chkSinglelineR";
    this.chkSinglelineR.Size = new Size(70, 17);
    this.chkSinglelineR.TabIndex = 8;
    this.chkSinglelineR.Text = "Singleline";
    this.chkSinglelineR.UseVisualStyleBackColor = true;
    this.chkRightToLeftR.AutoSize = true;
    this.chkRightToLeftR.Location = new Point(279, 20);
    this.chkRightToLeftR.Name = "chkRightToLeftR";
    this.chkRightToLeftR.Size = new Size(88, 17);
    this.chkRightToLeftR.TabIndex = 7;
    this.chkRightToLeftR.Text = "Right To Left";
    this.chkRightToLeftR.UseVisualStyleBackColor = true;
    this.chkMultilineR.AutoSize = true;
    this.chkMultilineR.Location = new Point(279, 3);
    this.chkMultilineR.Name = "chkMultilineR";
    this.chkMultilineR.Size = new Size(64 /*0x40*/, 17);
    this.chkMultilineR.TabIndex = 6;
    this.chkMultilineR.Text = "Multiline";
    this.chkMultilineR.UseVisualStyleBackColor = true;
    this.chkIgnorePatternWhitespaceR.AutoSize = true;
    this.chkIgnorePatternWhitespaceR.Location = new Point(113, 37);
    this.chkIgnorePatternWhitespaceR.Name = "chkIgnorePatternWhitespaceR";
    this.chkIgnorePatternWhitespaceR.Size = new Size(156, 17);
    this.chkIgnorePatternWhitespaceR.TabIndex = 5;
    this.chkIgnorePatternWhitespaceR.Text = "I&gnore Pattern Whitespace";
    this.chkIgnorePatternWhitespaceR.UseVisualStyleBackColor = true;
    this.chkIgnoreCaseR.AutoSize = true;
    this.chkIgnoreCaseR.Location = new Point(113, 20);
    this.chkIgnoreCaseR.Name = "chkIgnoreCaseR";
    this.chkIgnoreCaseR.Size = new Size(85, 17);
    this.chkIgnoreCaseR.TabIndex = 4;
    this.chkIgnoreCaseR.Text = "&Ignore Case";
    this.chkIgnoreCaseR.UseVisualStyleBackColor = true;
    this.chkExplicitCaptureR.AutoSize = true;
    this.chkExplicitCaptureR.Location = new Point(113, 3);
    this.chkExplicitCaptureR.Name = "chkExplicitCaptureR";
    this.chkExplicitCaptureR.Size = new Size(101, 17);
    this.chkExplicitCaptureR.TabIndex = 3;
    this.chkExplicitCaptureR.Text = "E&xplicit Capture";
    this.chkExplicitCaptureR.UseVisualStyleBackColor = true;
    this.chkEcmaScriptR.AutoSize = true;
    this.chkEcmaScriptR.Location = new Point(3, 37);
    this.chkEcmaScriptR.Name = "chkEcmaScriptR";
    this.chkEcmaScriptR.Size = new Size(84, 17);
    this.chkEcmaScriptR.TabIndex = 2;
    this.chkEcmaScriptR.Text = "ECMA Script";
    this.chkEcmaScriptR.UseVisualStyleBackColor = true;
    this.chkEcmaScriptR.CheckedChanged += new EventHandler(this.chkEcmaScript_CheckedChanged);
    this.chkCultureInvariantR.AutoSize = true;
    this.chkCultureInvariantR.Location = new Point(3, 20);
    this.chkCultureInvariantR.Name = "chkCultureInvariantR";
    this.chkCultureInvariantR.Size = new Size(108, 17);
    this.chkCultureInvariantR.TabIndex = 1;
    this.chkCultureInvariantR.Text = "C&ulture Invariant";
    this.chkCultureInvariantR.UseVisualStyleBackColor = true;
    this.chkCompiledR.AutoSize = true;
    this.chkCompiledR.Location = new Point(3, 3);
    this.chkCompiledR.Name = "chkCompiledR";
    this.chkCompiledR.Size = new Size(69, 17);
    this.chkCompiledR.TabIndex = 0;
    this.chkCompiledR.Text = "&Compiled";
    this.chkCompiledR.UseVisualStyleBackColor = true;
    this.lblSearchTypeR.AutoSize = true;
    this.lblSearchTypeR.Location = new Point(8, 52);
    this.lblSearchTypeR.Name = "lblSearchTypeR";
    this.lblSearchTypeR.Size = new Size(67, 13);
    this.lblSearchTypeR.TabIndex = 4;
    this.lblSearchTypeR.Text = "Search Type";
    this.rdoRegexR.AutoSize = true;
    this.rdoRegexR.Location = new Point(102, 71);
    this.rdoRegexR.Name = "rdoRegexR";
    this.rdoRegexR.Size = new Size(117, 17);
    this.rdoRegexR.TabIndex = 6;
    this.rdoRegexR.Text = "Regular &Expression";
    this.rdoRegexR.UseVisualStyleBackColor = true;
    this.rdoRegexR.CheckedChanged += new EventHandler(this.rdoStandardR_CheckedChanged);
    this.rdoStandardR.AutoSize = true;
    this.rdoStandardR.Checked = true;
    this.rdoStandardR.Location = new Point(27, 71);
    this.rdoStandardR.Name = "rdoStandardR";
    this.rdoStandardR.Size = new Size(69, 17);
    this.rdoStandardR.TabIndex = 5;
    this.rdoStandardR.TabStop = true;
    this.rdoStandardR.Text = "&Standard";
    this.rdoStandardR.UseVisualStyleBackColor = true;
    this.rdoStandardR.CheckedChanged += new EventHandler(this.rdoStandardR_CheckedChanged);
    this.lblFindR.AutoSize = true;
    this.lblFindR.Location = new Point(8, 10);
    this.lblFindR.Name = "lblFindR";
    this.lblFindR.Size = new Size(27, 13);
    this.lblFindR.TabIndex = 0;
    this.lblFindR.Text = "&Find";
    this.statusStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.lblStatus
    });
    this.statusStrip.Location = new Point(0, 269);
    this.statusStrip.Name = "statusStrip";
    this.statusStrip.Size = new Size(499, 22);
    this.statusStrip.SizingGrip = false;
    this.statusStrip.TabIndex = 1;
    this.statusStrip.Text = "xxx";
    this.lblStatus.Name = "lblStatus";
    this.lblStatus.Size = new Size(0, 17);
    this.AcceptButton = (IButtonControl) this.btnFindNext;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(499, 291);
    this.Controls.Add((Control) this.statusStrip);
    this.Controls.Add((Control) this.tabAll);
    this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FindReplaceDialog);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Text = "Find and Replace";
    this.FormClosing += new FormClosingEventHandler(this.FindReplaceDialog_FormClosing);
    this.tabAll.ResumeLayout(false);
    this.tpgFind.ResumeLayout(false);
    this.tpgFind.PerformLayout();
    this.grpFindAll.ResumeLayout(false);
    this.grpFindAll.PerformLayout();
    this.grpOptionsF.ResumeLayout(false);
    this.pnlStandardOptionsF.ResumeLayout(false);
    this.pnlStandardOptionsF.PerformLayout();
    this.pnlRegexpOptionsF.ResumeLayout(false);
    this.pnlRegexpOptionsF.PerformLayout();
    this.tpgReplace.ResumeLayout(false);
    this.tpgReplace.PerformLayout();
    this.grdOptionsR.ResumeLayout(false);
    this.pnlStandardOptionsR.ResumeLayout(false);
    this.pnlStandardOptionsR.PerformLayout();
    this.pnlRegexpOptionsR.ResumeLayout(false);
    this.pnlRegexpOptionsR.PerformLayout();
    this.statusStrip.ResumeLayout(false);
    this.statusStrip.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<string> MruFind
  {
    get => this._mruFind;
    set
    {
      this._mruFind = value;
      this.cboFindF.DataSource = (object) value;
      this.cboFindR.DataSource = (object) value;
    }
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<string> MruReplace
  {
    get => this._mruReplace;
    set
    {
      this._mruReplace = value;
      this.cboReplace.DataSource = (object) value;
    }
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int MruMaxCount
  {
    get => this._mruMaxCount;
    set => this._mruMaxCount = value;
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Scintilla Scintilla
  {
    get => this._scintilla;
    set => this._scintilla = value;
  }

  public FindReplaceDialog()
  {
    this.InitializeComponent();
    this._mruFind = new List<string>();
    this._mruReplace = new List<string>();
  }

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    this.cboFindF.DataSource = this.cboFindR.DataSource = (object) this._mruFind;
    this.cboReplace.DataSource = (object) this._mruReplace;
  }

  protected override void OnActivated(EventArgs e)
  {
    if (this.Scintilla.Selection.Length > 0)
    {
      this.chkSearchSelectionF.Enabled = true;
      this.chkSearchSelectionR.Enabled = true;
    }
    else
    {
      this.chkSearchSelectionF.Enabled = false;
      this.chkSearchSelectionR.Enabled = false;
      this.chkSearchSelectionF.Checked = false;
      this.chkSearchSelectionR.Checked = false;
    }
    this._searchRange = (Range) null;
    this.lblStatus.Text = string.Empty;
    this.moveFormAwayFromSelection();
    base.OnActivated(e);
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    List<KeyBinding> keyBindings1 = this.Scintilla.Commands.GetKeyBindings(BindableCommand.FindNext);
    List<KeyBinding> keyBindings2 = this.Scintilla.Commands.GetKeyBindings(BindableCommand.FindPrevious);
    List<KeyBinding> keyBindings3 = this.Scintilla.Commands.GetKeyBindings(BindableCommand.ShowFind);
    List<KeyBinding> keyBindings4 = this.Scintilla.Commands.GetKeyBindings(BindableCommand.ShowReplace);
    KeyBinding keyBinding = new KeyBinding(e.KeyCode, e.Modifiers);
    if (keyBindings1.Contains(keyBinding) || keyBindings2.Contains(keyBinding) || keyBindings3.Contains(keyBinding) || keyBindings4.Contains(keyBinding))
      this.Scintilla.FireKeyDown(e);
    if (e.KeyCode == Keys.Escape)
      this.Hide();
    base.OnKeyDown(e);
  }

  private void FindReplaceDialog_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (e.CloseReason != CloseReason.UserClosing)
      return;
    e.Cancel = true;
    this.Hide();
  }

  private void rdoStandardF_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rdoStandardF.Checked)
      this.pnlStandardOptionsF.BringToFront();
    else
      this.pnlRegexpOptionsF.BringToFront();
  }

  private void rdoStandardR_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rdoStandardR.Checked)
      this.pnlStandardOptionsR.BringToFront();
    else
      this.pnlRegexpOptionsR.BringToFront();
  }

  private void tabAll_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.tabAll.SelectedTab == this.tpgFind)
    {
      this.cboFindF.Text = this.cboFindR.Text;
      this.rdoStandardF.Checked = this.rdoStandardR.Checked;
      this.rdoRegexF.Checked = this.rdoRegexR.Checked;
      this.chkWrapF.Checked = this.chkWrapR.Checked;
      this.chkSearchSelectionF.Checked = this.chkSearchSelectionR.Checked;
      this.chkMatchCaseF.Checked = this.chkMatchCaseR.Checked;
      this.chkWholeWordF.Checked = this.chkWholeWordR.Checked;
      this.chkWordStartF.Checked = this.chkWordStartR.Checked;
      this.chkCompiledF.Checked = this.chkCompiledR.Checked;
      this.chkCultureInvariantF.Checked = this.chkCultureInvariantR.Checked;
      this.chkEcmaScriptF.Checked = this.chkEcmaScriptR.Checked;
      this.chkExplicitCaptureF.Checked = this.chkExplicitCaptureR.Checked;
      this.chkIgnoreCaseF.Checked = this.chkIgnoreCaseR.Checked;
      this.chkIgnorePatternWhitespaceF.Checked = this.chkIgnorePatternWhitespaceR.Checked;
      this.chkMultilineF.Checked = this.chkMultilineR.Checked;
      this.chkRightToLeftF.Checked = this.chkRightToLeftR.Checked;
      this.chkSinglelineF.Checked = this.chkSinglelineR.Checked;
      this.AcceptButton = (IButtonControl) this.btnFindNext;
    }
    else
    {
      this.cboFindR.Text = this.cboFindF.Text;
      this.rdoStandardR.Checked = this.rdoStandardF.Checked;
      this.rdoRegexR.Checked = this.rdoRegexF.Checked;
      this.chkWrapR.Checked = this.chkWrapF.Checked;
      this.chkSearchSelectionR.Checked = this.chkSearchSelectionF.Checked;
      this.chkMatchCaseR.Checked = this.chkMatchCaseF.Checked;
      this.chkWholeWordR.Checked = this.chkWholeWordF.Checked;
      this.chkWordStartR.Checked = this.chkWordStartF.Checked;
      this.chkCompiledR.Checked = this.chkCompiledF.Checked;
      this.chkCultureInvariantR.Checked = this.chkCultureInvariantF.Checked;
      this.chkEcmaScriptR.Checked = this.chkEcmaScriptF.Checked;
      this.chkExplicitCaptureR.Checked = this.chkExplicitCaptureF.Checked;
      this.chkIgnoreCaseR.Checked = this.chkIgnoreCaseF.Checked;
      this.chkIgnorePatternWhitespaceR.Checked = this.chkIgnorePatternWhitespaceF.Checked;
      this.chkMultilineR.Checked = this.chkMultilineF.Checked;
      this.chkRightToLeftR.Checked = this.chkRightToLeftF.Checked;
      this.chkSinglelineR.Checked = this.chkSinglelineF.Checked;
      this.AcceptButton = (IButtonControl) this.btnReplaceNext;
    }
  }

  private Range findNextF(bool searchUp)
  {
    Range nextF;
    if (this.rdoRegexF.Checked)
    {
      Regex findExpression = new Regex(this.cboFindF.Text, this.GetRegexOptions());
      if (this.chkSearchSelectionF.Checked)
      {
        if (this._searchRange == null)
          this._searchRange = this.Scintilla.Selection.Range;
        nextF = !searchUp ? this.Scintilla.FindReplace.FindNext(findExpression, this.chkWrapF.Checked, this._searchRange) : this.Scintilla.FindReplace.FindPrevious(findExpression, this.chkWrapF.Checked, this._searchRange);
      }
      else
      {
        this._searchRange = (Range) null;
        nextF = !searchUp ? this.Scintilla.FindReplace.FindNext(findExpression, this.chkWrapF.Checked) : this.Scintilla.FindReplace.FindPrevious(findExpression, this.chkWrapF.Checked);
      }
    }
    else if (this.chkSearchSelectionF.Checked)
    {
      if (this._searchRange == null)
        this._searchRange = this.Scintilla.Selection.Range;
      nextF = !searchUp ? this.Scintilla.FindReplace.FindNext(this.cboFindF.Text, this.chkWrapF.Checked, this.GetSearchFlags(), this._searchRange) : this.Scintilla.FindReplace.FindPrevious(this.cboFindF.Text, this.chkWrapF.Checked, this.GetSearchFlags(), this._searchRange);
    }
    else
    {
      this._searchRange = (Range) null;
      nextF = !searchUp ? this.Scintilla.FindReplace.FindNext(this.cboFindF.Text, this.chkWrapF.Checked, this.GetSearchFlags()) : this.Scintilla.FindReplace.FindPrevious(this.cboFindF.Text, this.chkWrapF.Checked, this.GetSearchFlags());
    }
    return nextF;
  }

  private Range findNextR(bool searchUp, ref Regex rr)
  {
    Range nextR;
    if (this.rdoRegexR.Checked)
    {
      if (rr == null)
        rr = new Regex(this.cboFindR.Text, this.GetRegexOptions());
      if (this.chkSearchSelectionR.Checked)
      {
        if (this._searchRange == null)
          this._searchRange = this.Scintilla.Selection.Range;
        nextR = !searchUp ? this.Scintilla.FindReplace.FindNext(rr, this.chkWrapR.Checked, this._searchRange) : this.Scintilla.FindReplace.FindPrevious(rr, this.chkWrapR.Checked, this._searchRange);
      }
      else
      {
        this._searchRange = (Range) null;
        nextR = !searchUp ? this.Scintilla.FindReplace.FindNext(rr, this.chkWrapR.Checked) : this.Scintilla.FindReplace.FindPrevious(rr, this.chkWrapR.Checked);
      }
    }
    else if (this.chkSearchSelectionF.Checked)
    {
      if (this._searchRange == null)
        this._searchRange = this.Scintilla.Selection.Range;
      nextR = !searchUp ? this.Scintilla.FindReplace.FindNext(this.cboFindR.Text, this.chkWrapR.Checked, this.GetSearchFlags(), this._searchRange) : this.Scintilla.FindReplace.FindPrevious(this.cboFindR.Text, this.chkWrapR.Checked, this.GetSearchFlags(), this._searchRange);
    }
    else
    {
      this._searchRange = (Range) null;
      nextR = !searchUp ? this.Scintilla.FindReplace.FindNext(this.cboFindR.Text, this.chkWrapF.Checked, this.GetSearchFlags()) : this.Scintilla.FindReplace.FindPrevious(this.cboFindR.Text, this.chkWrapF.Checked, this.GetSearchFlags());
    }
    return nextR;
  }

  public SearchFlags GetSearchFlags()
  {
    SearchFlags searchFlags = SearchFlags.Empty;
    if (this.tabAll.SelectedTab == this.tpgFind)
    {
      if (this.chkMatchCaseF.Checked)
        searchFlags |= SearchFlags.MatchCase;
      if (this.chkWholeWordF.Checked)
        searchFlags |= SearchFlags.WholeWord;
      if (this.chkWordStartF.Checked)
        searchFlags |= SearchFlags.WordStart;
    }
    else
    {
      if (this.chkMatchCaseR.Checked)
        searchFlags |= SearchFlags.MatchCase;
      if (this.chkWholeWordR.Checked)
        searchFlags |= SearchFlags.WholeWord;
      if (this.chkWordStartR.Checked)
        searchFlags |= SearchFlags.WordStart;
    }
    return searchFlags;
  }

  public RegexOptions GetRegexOptions()
  {
    RegexOptions regexOptions = RegexOptions.None;
    if (this.tabAll.SelectedTab == this.tpgFind)
    {
      if (this.chkCompiledF.Checked)
        regexOptions |= RegexOptions.Compiled;
      if (this.chkCultureInvariantF.Checked)
        regexOptions |= RegexOptions.Compiled;
      if (this.chkEcmaScriptF.Checked)
        regexOptions |= RegexOptions.ECMAScript;
      if (this.chkExplicitCaptureF.Checked)
        regexOptions |= RegexOptions.ExplicitCapture;
      if (this.chkIgnoreCaseF.Checked)
        regexOptions |= RegexOptions.IgnoreCase;
      if (this.chkIgnorePatternWhitespaceF.Checked)
        regexOptions |= RegexOptions.IgnorePatternWhitespace;
      if (this.chkMultilineF.Checked)
        regexOptions |= RegexOptions.Multiline;
      if (this.chkRightToLeftF.Checked)
        regexOptions |= RegexOptions.RightToLeft;
      if (this.chkSinglelineF.Checked)
        regexOptions |= RegexOptions.Singleline;
    }
    else
    {
      if (this.chkCompiledR.Checked)
        regexOptions |= RegexOptions.Compiled;
      if (this.chkCultureInvariantR.Checked)
        regexOptions |= RegexOptions.Compiled;
      if (this.chkEcmaScriptR.Checked)
        regexOptions |= RegexOptions.ECMAScript;
      if (this.chkExplicitCaptureR.Checked)
        regexOptions |= RegexOptions.ExplicitCapture;
      if (this.chkIgnoreCaseR.Checked)
        regexOptions |= RegexOptions.IgnoreCase;
      if (this.chkIgnorePatternWhitespaceR.Checked)
        regexOptions |= RegexOptions.IgnorePatternWhitespace;
      if (this.chkMultilineR.Checked)
        regexOptions |= RegexOptions.Multiline;
      if (this.chkRightToLeftR.Checked)
        regexOptions |= RegexOptions.RightToLeft;
      if (this.chkSinglelineR.Checked)
        regexOptions |= RegexOptions.Singleline;
    }
    return regexOptions;
  }

  private void chkEcmaScript_CheckedChanged(object sender, EventArgs e)
  {
    if (((CheckBox) sender).Checked)
    {
      this.chkExplicitCaptureF.Checked = false;
      this.chkExplicitCaptureR.Checked = false;
      this.chkExplicitCaptureF.Enabled = false;
      this.chkExplicitCaptureR.Enabled = false;
      this.chkIgnorePatternWhitespaceF.Checked = false;
      this.chkIgnorePatternWhitespaceR.Checked = false;
      this.chkIgnorePatternWhitespaceF.Enabled = false;
      this.chkIgnorePatternWhitespaceR.Enabled = false;
      this.chkRightToLeftF.Checked = false;
      this.chkRightToLeftR.Checked = false;
      this.chkRightToLeftF.Enabled = false;
      this.chkRightToLeftR.Enabled = false;
      this.chkSinglelineF.Checked = false;
      this.chkSinglelineR.Checked = false;
      this.chkSinglelineF.Enabled = false;
      this.chkSinglelineR.Enabled = false;
    }
    else
    {
      this.chkExplicitCaptureF.Enabled = true;
      this.chkIgnorePatternWhitespaceF.Enabled = true;
      this.chkRightToLeftF.Enabled = true;
      this.chkSinglelineF.Enabled = true;
      this.chkExplicitCaptureR.Enabled = true;
      this.chkIgnorePatternWhitespaceR.Enabled = true;
      this.chkRightToLeftR.Enabled = true;
      this.chkSinglelineR.Enabled = true;
    }
  }

  private void addFindMru()
  {
    string text = this.cboFindF.Text;
    this._mruFind.Remove(text);
    this._mruFind.Insert(0, text);
    if (this._mruFind.Count > this._mruMaxCount)
      this._mruFind.RemoveAt(this._mruFind.Count - 1);
    this.cboFindF.DataSource = this.cboFindR.DataSource = (object) this._mruFind;
  }

  private void addReplacMru()
  {
    string text1 = this.cboFindR.Text;
    this._mruFind.Remove(text1);
    this._mruFind.Insert(0, text1);
    if (this._mruFind.Count > this._mruMaxCount)
      this._mruFind.RemoveAt(this._mruFind.Count - 1);
    string text2 = this.cboReplace.Text;
    if (text2 != string.Empty)
    {
      this._mruReplace.Remove(text2);
      this._mruReplace.Insert(0, text2);
      if (this._mruReplace.Count > this._mruMaxCount)
        this._mruReplace.RemoveAt(this._mruReplace.Count - 1);
    }
    this.cboFindF.DataSource = this.cboFindR.DataSource = (object) this._mruFind;
    this.cboReplace.DataSource = (object) this._mruReplace;
  }

  private void btnFindAll_Click(object sender, EventArgs e)
  {
    if (this.cboFindF.Text == string.Empty)
      return;
    this.addFindMru();
    this.lblStatus.Text = string.Empty;
    List<Range> all;
    if (this.rdoRegexF.Checked)
    {
      Regex findExpression;
      try
      {
        findExpression = new Regex(this.cboFindF.Text, this.GetRegexOptions());
      }
      catch (ArgumentException ex)
      {
        this.lblStatus.Text = "Error in Regular Expression: " + ex.Message;
        return;
      }
      if (this.chkSearchSelectionF.Checked)
      {
        if (this._searchRange == null)
          this._searchRange = this.Scintilla.Selection.Range;
        all = this.Scintilla.FindReplace.FindAll(this._searchRange, findExpression);
      }
      else
      {
        this._searchRange = (Range) null;
        all = this.Scintilla.FindReplace.FindAll(findExpression);
      }
    }
    else if (this.chkSearchSelectionF.Checked)
    {
      if (this._searchRange == null)
        this._searchRange = this.Scintilla.Selection.Range;
      all = this.Scintilla.FindReplace.FindAll(this._searchRange, this.cboFindF.Text, this.GetSearchFlags());
    }
    else
    {
      this._searchRange = (Range) null;
      all = this.Scintilla.FindReplace.FindAll(this.cboFindF.Text, this.GetSearchFlags());
    }
    this.lblStatus.Text = "Total found: " + all.Count.ToString();
    this.btnClear_Click((object) null, (EventArgs) null);
    if (this.chkMarkLine.Checked)
      this.Scintilla.FindReplace.MarkAll((IList<Range>) all);
    if (!this.chkHighlightMatches.Checked)
      return;
    this.Scintilla.FindReplace.HighlightAll((IList<Range>) all);
  }

  private void btnClear_Click(object sender, EventArgs e)
  {
    this.Scintilla.Markers.DeleteAll(this.Scintilla.FindReplace.Marker);
    this.Scintilla.FindReplace.ClearAllHighlights();
  }

  private void btnReplacePrevious_Click(object sender, EventArgs e)
  {
    if (this.cboFindR.Text == string.Empty)
      return;
    this.addReplacMru();
    this.lblStatus.Text = string.Empty;
    Range range;
    try
    {
      range = this.replaceNext(true);
    }
    catch (ArgumentException ex)
    {
      this.lblStatus.Text = "Error in Regular Expression: " + ex.Message;
      return;
    }
    if (range == null)
    {
      this.lblStatus.Text = "Match could not be found";
    }
    else
    {
      if (range.Start > this.Scintilla.Caret.Anchor)
      {
        if (this.chkSearchSelectionR.Checked)
          this.lblStatus.Text = "Search match wrapped to the begining of the selection";
        else
          this.lblStatus.Text = "Search match wrapped to the begining of the document";
      }
      range.Select();
      this.moveFormAwayFromSelection();
    }
  }

  private Range replaceNext(bool searchUp)
  {
    Regex rr = (Regex) null;
    Range range = this.Scintilla.Selection.Range;
    if (range.Length > 0)
    {
      if (this.rdoRegexR.Checked)
      {
        rr = new Regex(this.cboFindR.Text, this.GetRegexOptions());
        string text = range.Text;
        if (range.Equals((object) this.Scintilla.FindReplace.Find(range, rr, false)))
        {
          if (searchUp)
            range.Text = rr.Replace(text, this.cboReplace.Text);
          else
            this.Scintilla.Selection.Text = rr.Replace(text, this.cboReplace.Text);
        }
      }
      else if (range.Equals((object) this.Scintilla.FindReplace.Find(range, this.cboFindR.Text, false)))
      {
        if (searchUp)
          range.Text = this.cboReplace.Text;
        else
          this.Scintilla.Selection.Text = this.cboReplace.Text;
      }
    }
    return this.findNextR(searchUp, ref rr);
  }

  private void btnReplaceNext_Click(object sender, EventArgs e)
  {
    if (this.cboFindR.Text == string.Empty)
      return;
    this.addReplacMru();
    this.lblStatus.Text = string.Empty;
    Range range;
    try
    {
      range = this.replaceNext(false);
    }
    catch (ArgumentException ex)
    {
      this.lblStatus.Text = "Error in Regular Expression: " + ex.Message;
      return;
    }
    if (range == null)
    {
      this.lblStatus.Text = "Match could not be found";
    }
    else
    {
      if (range.Start < this.Scintilla.Caret.Anchor)
      {
        if (this.chkSearchSelectionR.Checked)
          this.lblStatus.Text = "Search match wrapped to the begining of the selection";
        else
          this.lblStatus.Text = "Search match wrapped to the begining of the document";
      }
      range.Select();
      this.moveFormAwayFromSelection();
    }
  }

  private void btnReplaceAll_Click(object sender, EventArgs e)
  {
    if (this.cboFindR.Text == string.Empty)
      return;
    this.lblStatus.Text = string.Empty;
    List<Range> rangeList;
    if (this.rdoRegexR.Checked)
    {
      Regex findExpression;
      try
      {
        findExpression = new Regex(this.cboFindR.Text, this.GetRegexOptions());
      }
      catch (ArgumentException ex)
      {
        this.lblStatus.Text = "Error in Regular Expression: " + ex.Message;
        return;
      }
      if (this.chkSearchSelectionR.Checked)
      {
        if (this._searchRange == null)
          this._searchRange = this.Scintilla.Selection.Range;
        rangeList = this.Scintilla.FindReplace.ReplaceAll(this._searchRange, findExpression, this.cboReplace.Text);
      }
      else
      {
        this._searchRange = (Range) null;
        rangeList = this.Scintilla.FindReplace.ReplaceAll(findExpression, this.cboReplace.Text);
      }
    }
    else if (this.chkSearchSelectionR.Checked)
    {
      if (this._searchRange == null)
        this._searchRange = this.Scintilla.Selection.Range;
      rangeList = this.Scintilla.FindReplace.ReplaceAll(this._searchRange, this.cboFindR.Text, this.cboReplace.Text, this.GetSearchFlags());
    }
    else
    {
      this._searchRange = (Range) null;
      rangeList = this.Scintilla.FindReplace.ReplaceAll(this.cboFindR.Text, this.cboReplace.Text, this.GetSearchFlags());
    }
    this.lblStatus.Text = "Total Replaced: " + rangeList.Count.ToString();
  }

  private void btnFindNext_Click(object sender, EventArgs e) => this.FindNext();

  public void FindNext()
  {
    if (this.cboFindF.Text == string.Empty)
      return;
    this.addFindMru();
    this.lblStatus.Text = string.Empty;
    Range nextF;
    try
    {
      nextF = this.findNextF(false);
    }
    catch (ArgumentException ex)
    {
      this.lblStatus.Text = "Error in Regular Expression: " + ex.Message;
      return;
    }
    if (nextF == null)
    {
      this.lblStatus.Text = "Match could not be found";
    }
    else
    {
      if (nextF.Start < this.Scintilla.Caret.Anchor)
      {
        if (this.chkSearchSelectionF.Checked)
          this.lblStatus.Text = "Search match wrapped to the begining of the selection";
        else
          this.lblStatus.Text = "Search match wrapped to the begining of the document";
      }
      nextF.Select();
      this.moveFormAwayFromSelection();
    }
  }

  private void btnFindPrevious_Click(object sender, EventArgs e) => this.FindPrevious();

  public void FindPrevious()
  {
    if (this.cboFindF.Text == string.Empty)
      return;
    this.addFindMru();
    this.lblStatus.Text = string.Empty;
    Range nextF;
    try
    {
      nextF = this.findNextF(true);
    }
    catch (ArgumentException ex)
    {
      this.lblStatus.Text = "Error in Regular Expression: " + ex.Message;
      return;
    }
    if (nextF == null)
    {
      this.lblStatus.Text = "Match could not be found";
    }
    else
    {
      if (nextF.Start > this.Scintilla.Caret.Position)
      {
        if (this.chkSearchSelectionF.Checked)
          this.lblStatus.Text = "Search match wrapped to the end of the selection";
        else
          this.lblStatus.Text = "Search match wrapped to the end of the document";
      }
      nextF.Select();
      this.moveFormAwayFromSelection();
    }
  }

  public void moveFormAwayFromSelection()
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
