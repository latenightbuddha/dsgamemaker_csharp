using System;
using System.ComponentModel;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Indentation : TopLevelHelper
{
  private SmartIndent _smartIndentType;
  public EventHandler<CharAddedEventArgs> SmartIndentCustomAction;

  internal Indentation(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeBackspaceUnindents() || this.ShouldSerializeIndentWidth() || this.ShouldSerializeShowGuides() || this.ShouldSerializeTabIndents() || this.ShouldSerializeTabWidth() || this.ShouldSerializeUseTabs();
  }

  public int TabWidth
  {
    get => this.NativeScintilla.GetTabWidth();
    set => this.NativeScintilla.SetTabWidth(value);
  }

  private bool ShouldSerializeTabWidth() => this.TabWidth != 8;

  private void ResetTabWidth() => this.TabWidth = 8;

  public bool UseTabs
  {
    get => this.NativeScintilla.GetUseTabs();
    set => this.NativeScintilla.SetUseTabs(value);
  }

  private bool ShouldSerializeUseTabs() => !this.UseTabs;

  private void ResetUseTabs() => this.UseTabs = true;

  public int IndentWidth
  {
    get => this.NativeScintilla.GetIndent();
    set => this.NativeScintilla.SetIndent(value);
  }

  private bool ShouldSerializeIndentWidth() => this.IndentWidth != 0;

  private void ResetIndentWidth() => this.IndentWidth = 0;

  public bool TabIndents
  {
    get => this.NativeScintilla.GetTabIndents();
    set => this.NativeScintilla.SetTabIndents(value);
  }

  private bool ShouldSerializeTabIndents() => !this.TabIndents;

  private void ResetTabIndents() => this.TabIndents = false;

  public bool BackspaceUnindents
  {
    get => this.NativeScintilla.GetBackSpaceUnIndents();
    set => this.NativeScintilla.SetBackSpaceUnIndents(value);
  }

  private bool ShouldSerializeBackspaceUnindents() => this.BackspaceUnindents;

  private void ResetBackspaceUnindents() => this.BackspaceUnindents = false;

  public bool ShowGuides
  {
    get => this.NativeScintilla.GetIndentationGuides();
    set => this.NativeScintilla.SetIndentationGuides(value);
  }

  private bool ShouldSerializeShowGuides() => this.ShowGuides;

  private void ResetShowGuides() => this.ShowGuides = false;

  public SmartIndent SmartIndentType
  {
    get => this._smartIndentType;
    set => this._smartIndentType = value;
  }

  private bool ShouldSerializeSmartIndentType() => this._smartIndentType != SmartIndent.None;

  private void ResetSmartIndentType() => this._smartIndentType = SmartIndent.None;

  internal void CheckSmartIndent(char ch)
  {
    char ch1 = this.Scintilla.EndOfLine.Mode == EndOfLineMode.CR ? '\r' : '\n';
    switch (this.SmartIndentType)
    {
      case SmartIndent.CPP:
      case SmartIndent.CPP2:
        if ((int) ch == (int) ch1)
        {
          Line current = this.Scintilla.Lines.Current;
          Line line = current;
          int num;
          string str;
          do
          {
            line = line.Previous;
            num = line.Indentation;
            str = line.Text.Trim();
            if (str.Length == 0)
              num = -1;
          }
          while (line.Number > 1 && num < 0);
          if (str.EndsWith("{"))
          {
            int position = this.Scintilla.CurrentPos - 1;
            while (position > 0 && this.Scintilla.CharAt(position) != '{')
              --position;
            if (position > 0 && this.Scintilla.Styles.GetStyleAt(position) == (byte) 10)
              num += this.TabWidth;
          }
          current.Indentation = num;
          this.Scintilla.CurrentPos = current.IndentPosition;
          break;
        }
        if (ch != '}')
          break;
        int currentPos = this.Scintilla.CurrentPos;
        Line current1 = this.Scintilla.Lines.Current;
        int indentation1 = current1.Previous.Indentation;
        int position1 = this.Scintilla.SafeBraceMatch(currentPos - 1);
        if (position1 == -1)
          break;
        int indentation2 = this.Scintilla.Lines.FromPosition(position1).Indentation;
        current1.Indentation = indentation2;
        break;
      case SmartIndent.Simple:
        if ((int) ch != (int) ch1)
          break;
        Line current2 = this.Scintilla.Lines.Current;
        current2.Indentation = current2.Previous.Indentation;
        this.Scintilla.CurrentPos = current2.IndentPosition;
        break;
    }
  }

  private void IndentLine(int line, int indent)
  {
    if (indent < 0)
      return;
    int num1 = this.Scintilla.Selection.Start;
    int num2 = this.Scintilla.Selection.End;
    Line line1 = this.Scintilla.Lines[line];
    int indentPosition1 = line1.IndentPosition;
    line1.Indentation = indent;
    int indentPosition2 = line1.IndentPosition;
    int num3 = indentPosition2 - indentPosition1;
    if (indentPosition2 > indentPosition1)
    {
      if (num1 >= indentPosition1)
        num1 += num3;
      if (num2 >= indentPosition1)
        num2 += num3;
    }
    else if (indentPosition2 < indentPosition1)
    {
      if (num1 >= indentPosition2)
      {
        if (num1 >= indentPosition1)
          num1 += num3;
        else
          num1 = indentPosition2;
      }
      if (num2 >= indentPosition2)
      {
        if (num2 >= indentPosition1)
          num2 += num3;
        else
          num2 = indentPosition2;
      }
    }
    this.Scintilla.Selection.Start = num1;
    this.Scintilla.Selection.End = num2;
  }
}
