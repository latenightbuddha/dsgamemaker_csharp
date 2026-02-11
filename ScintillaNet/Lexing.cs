using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Lexing : TopLevelHelper
{
  private const string DEFAULT_WORDCHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
  private const string DEFAULT_WHITECHARS = " \t\r\n\0";
  private Dictionary<string, int> _styleNameMap = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private Dictionary<string, string> _lexerLanguageMap = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private string _lexerName = "container";
  private KeywordCollection _keywords;
  private string _wordChars;
  internal char[] WordCharsArr;
  internal char[] WhitespaceCharsArr;
  private string _whitespaceChars;
  private string _lineCommentPrefix = string.Empty;
  private string _streamCommentPrefix = string.Empty;
  private string _streamCommentSufix = string.Empty;

  internal Lexing(Scintilla scintilla)
    : base(scintilla)
  {
    this.WhitespaceChars = " \t\r\n\0";
    this.WordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
    this._keywords = new KeywordCollection(scintilla);
    this._lexerLanguageMap.Add("cs", "cpp");
    this._lexerLanguageMap.Add("html", "hypertext");
    this._lexerLanguageMap.Add("xml", "hypertext");
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeLexerName() || this.ShouldSerializeLexer() || this.ShouldSerializeWhitespaceChars() || this.ShouldSerializeWordChars();
  }

  public Lexer Lexer
  {
    get => (Lexer) this.NativeScintilla.GetLexer();
    set
    {
      this.NativeScintilla.SetLexer((int) value);
      this._lexerName = value.ToString().ToLower();
      if (this._lexerName == "null")
        this._lexerName = "";
      this.loadStyleMap();
    }
  }

  private bool ShouldSerializeLexer() => this.Lexer != Lexer.Container;

  private void ResetLexer() => this.Lexer = Lexer.Container;

  public string LexerName
  {
    get => this._lexerName;
    set
    {
      if (string.IsNullOrEmpty(value))
        value = "null";
      this.NativeScintilla.SetLexerLanguage(value.ToLower());
      this._lexerName = value;
      this.loadStyleMap();
    }
  }

  private bool ShouldSerializeLexerName() => this.LexerName != "container";

  private void ResetLexerName() => this.LexerName = "container";

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public KeywordCollection Keywords => this._keywords;

  public void LoadLexerLibrary(string path) => this.NativeScintilla.LoadLexerLibrary(path);

  public void Colorize(int startPos, int endPos)
  {
    this.NativeScintilla.Colourise(startPos, endPos);
  }

  public void Colorize() => this.Colorize(0, -1);

  public string GetProperty(string name)
  {
    string property;
    this.NativeScintilla.GetProperty(name, out property);
    return property;
  }

  public void SetProperty(string name, string value)
  {
    this.NativeScintilla.SetProperty(name, value);
  }

  public string GetPropertyExpanded(string name)
  {
    string propertyExpanded;
    this.NativeScintilla.GetPropertyExpanded(name, out propertyExpanded);
    return propertyExpanded;
  }

  public int GetPropertyInt(string name) => this.GetPropertyInt(name, 0);

  public int GetPropertyInt(string name, int defaultValue)
  {
    return this.NativeScintilla.GetPropertyInt(name, defaultValue);
  }

  public void SetKeywords(int keywordSet, string list)
  {
    this.NativeScintilla.SetKeywords(keywordSet, list);
  }

  public string WordChars
  {
    get => this._wordChars;
    set
    {
      this._wordChars = value;
      this.WordCharsArr = this._wordChars.ToCharArray();
      this.NativeScintilla.SetWordChars(value);
    }
  }

  private bool ShouldSerializeWordChars()
  {
    return this._wordChars != "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
  }

  private void ResetWordChars()
  {
    this.WordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
  }

  [TypeConverter(typeof (WhitespaceStringConverter))]
  public string WhitespaceChars
  {
    get => this._whitespaceChars;
    set
    {
      this._whitespaceChars = value;
      this.WhitespaceCharsArr = this._whitespaceChars.ToCharArray();
      this.NativeScintilla.SetWhitespaceChars(value);
    }
  }

  private bool ShouldSerializeWhitespaceChars() => this._whitespaceChars != " \t\r\n\0";

  private void ResetWhitespaceChars() => this._whitespaceChars = " \t\r\n\0";

  private void loadStyleMap()
  {
    if (this.Scintilla.IsDesignMode)
      return;
    this._styleNameMap.Clear();
    this._styleNameMap.Add("BRACEBAD", 35);
    this._styleNameMap.Add("BRACELIGHT", 34);
    this._styleNameMap.Add("CALLTIP", 38);
    this._styleNameMap.Add("CONTROLCHAR", 36);
    this._styleNameMap.Add("DEFAULT", 32 /*0x20*/);
    this._styleNameMap.Add("LINENUMBER", 33);
    string lower = this.Lexer.ToString().ToLower();
    using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream($"ScintillaNet.Configuration.Builtin.LexerStyleNames.{lower}.txt"))
    {
      if (manifestResourceStream == null)
        return;
      using (StreamReader streamReader = new StreamReader(manifestResourceStream))
      {
        while (!streamReader.EndOfStream)
        {
          string[] strArray = streamReader.ReadLine().Split('=');
          if (strArray.Length == 2)
          {
            string key = strArray[0].Trim();
            int num = int.Parse(strArray[1].Trim());
            this._styleNameMap.Remove(key);
            this._styleNameMap.Add(key, num);
          }
        }
      }
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Dictionary<string, int> StyleNameMap => this._styleNameMap;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Dictionary<string, string> LexerLanguageMap => this._lexerLanguageMap;

  public string LineCommentPrefix
  {
    get => this._lineCommentPrefix;
    set
    {
      if (value == null)
        value = string.Empty;
      this._lineCommentPrefix = value;
    }
  }

  public string StreamCommentPrefix
  {
    get => this._streamCommentPrefix;
    set
    {
      if (value == null)
        value = string.Empty;
      this._streamCommentPrefix = value;
    }
  }

  public string StreamCommentSufix
  {
    get => this._streamCommentSufix;
    set
    {
      if (value == null)
        value = string.Empty;
      this._streamCommentSufix = value;
    }
  }

  public void LineComment()
  {
    if (string.IsNullOrEmpty(this._lineCommentPrefix))
      return;
    this.NativeScintilla.BeginUndoAction();
    Range range = this.Scintilla.Selection.Range;
    int number1 = range.StartingLine.Number;
    int number2 = range.EndingLine.Number;
    int length = this._lineCommentPrefix.Length;
    for (int index = number1; index <= number2; ++index)
    {
      Line line = this.Scintilla.Lines[index];
      int nonWhitespaceChar = this.findFirstNonWhitespaceChar(line.Text);
      if (nonWhitespaceChar >= 0)
      {
        this.Scintilla.InsertText(line.StartPosition + nonWhitespaceChar, this._lineCommentPrefix);
        range.End += length;
      }
    }
    this.NativeScintilla.EndUndoAction();
    range.Select();
  }

  public void LineUncomment()
  {
    if (string.IsNullOrEmpty(this._lineCommentPrefix))
      return;
    this.NativeScintilla.BeginUndoAction();
    Range range1 = this.Scintilla.Selection.Range;
    int number1 = range1.StartingLine.Number;
    int number2 = range1.EndingLine.Number;
    int length = this._lineCommentPrefix.Length;
    for (int index = number1; index <= number2; ++index)
    {
      Line line = this.Scintilla.Lines[index];
      int nonWhitespaceChar = this.findFirstNonWhitespaceChar(line.Text);
      if (nonWhitespaceChar >= 0)
      {
        int startPosition = line.StartPosition + nonWhitespaceChar;
        Range range2 = this.Scintilla.GetRange(startPosition, startPosition + length);
        if (range2.Text == this._lineCommentPrefix)
          range2.Text = string.Empty;
      }
    }
    this.NativeScintilla.EndUndoAction();
  }

  public void ToggleLineComment()
  {
    if (string.IsNullOrEmpty(this._lineCommentPrefix))
      return;
    this.NativeScintilla.BeginUndoAction();
    Range range1 = this.Scintilla.Selection.Range;
    int number1 = range1.StartingLine.Number;
    int number2 = range1.EndingLine.Number;
    int length = this._lineCommentPrefix.Length;
    for (int index = number1; index <= number2; ++index)
    {
      Line line = this.Scintilla.Lines[index];
      int nonWhitespaceChar = this.findFirstNonWhitespaceChar(line.Text);
      if (nonWhitespaceChar >= 0)
      {
        int startPosition = line.StartPosition + nonWhitespaceChar;
        Range range2 = this.Scintilla.GetRange(startPosition, startPosition + length);
        if (range2.Text == this._lineCommentPrefix)
        {
          range2.Text = string.Empty;
          range1.End -= length;
        }
        else
        {
          this.Scintilla.InsertText(line.StartPosition + nonWhitespaceChar, this._lineCommentPrefix);
          range1.End += length;
        }
      }
    }
    this.NativeScintilla.EndUndoAction();
    range1.Select();
  }

  private int findFirstNonWhitespaceChar(string s)
  {
    for (int index = 0; index < s.Length; ++index)
    {
      if (s[index].ToString().IndexOfAny(this.WhitespaceCharsArr) == -1)
        return index;
    }
    return -1;
  }

  public void StreamComment()
  {
    if (string.IsNullOrEmpty(this._streamCommentPrefix) || string.IsNullOrEmpty(this._streamCommentSufix))
      return;
    this.NativeScintilla.BeginUndoAction();
    Range range = this.Scintilla.Selection.Range;
    this.Scintilla.InsertText(range.Start, this._streamCommentPrefix);
    this.Scintilla.InsertText(range.End + this._streamCommentPrefix.Length, this._streamCommentSufix);
    range.End += this._streamCommentPrefix.Length + this._streamCommentSufix.Length;
    range.Select();
    this.NativeScintilla.EndUndoAction();
  }
}
