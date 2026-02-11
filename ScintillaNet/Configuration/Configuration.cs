using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace ScintillaNet.Configuration;

public class Configuration
{
  private string _language;
  private string _autoComplete_List;
  private bool? _autoComplete_ListInherit;
  private string _autoComplete_StopCharacters;
  private char? _autoComplete_ListSeperator;
  private bool? _autoComplete_cancelAtStart;
  private string _autoComplete_fillUpCharacters;
  private bool? _autoComplete_singleLineAccept;
  private bool? _autoComplete_IsCaseSensitive;
  private bool? _autoComplete_AutoHide;
  private bool? _autoComplete_DropRestOfWord;
  private char? _autoComplete_ImageSeperator;
  private int? _autoComplete_MaxHeight;
  private int? _autoComplete_MaxWidth;
  private bool? _autoComplete_AutomaticLengthEntered;
  private Color _callTip_ForeColor;
  private Color _callTip_BackColor;
  private Color _callTip_HighlightTextColor;
  private int? _caret_Width;
  private CaretStyle? _caret_Style;
  private Color _caret_Color;
  private Color _caret_CurrentLineBackgroundColor;
  private bool? _caret_HighlightCurrentLine;
  private int? _caret_CurrentLineBackgroundAlpha;
  private int? _caret_BlinkRate;
  private bool? _caret_IsSticky;
  private bool? _clipboard_ConvertEndOfLineOnPaste;
  private CommandBindingConfigList _commands_KeyBindingList = new CommandBindingConfigList();
  private string _dropMarkers_SharedStackName;
  private bool? _endOfLine_ConvertOnPaste;
  private EndOfLineMode? _endOfLine_Mode;
  private bool? _endOfLine_IsVisisble;
  private bool? _folding_IsEnabled;
  private bool? _folding_UseCompactFolding;
  private FoldMarkerScheme? _folding_MarkerScheme;
  private FoldFlag? _folding_Flags;
  private bool _hasData;
  private Color _hotspot_ActiveForeColor;
  private Color _hotspot_ActiveBackColor;
  private bool? _hotspot_ActiveUnderline;
  private bool? _hotspot_SingleLine;
  private bool? _hotspot_UseActiveForeColor;
  private bool? _hotspot_UseActiveBackColor;
  private int? _indentation_TabWidth;
  private bool? _indentation_UseTabs;
  private int? _indentation_IndentWidth;
  private SmartIndent? _indentation_SmartIndentType;
  private bool? _indentation_TabIndents;
  private bool? _indentation_BackspaceUnindents;
  private bool? _indentation_ShowGuides;
  private IndicatorConfigList _indicator_List = new IndicatorConfigList();
  private string _lexing_WhitespaceChars;
  private string _lexing_WordChars;
  private string _lexing_Language;
  private string _lexing_LineCommentPrefix;
  private string _lexing_StreamCommentSuffix;
  private string _lexing_StreamCommentPrefix;
  private LexerPropertiesConfig _lexing_Properties = new LexerPropertiesConfig();
  private KeyWordConfigList _lexing_Keywords = new KeyWordConfigList();
  private WrapMode? _lineWrap_Mode;
  private WrapVisualFlag? _lineWrap_VisualFlags;
  private WrapVisualLocation? _lineWrap_VisualFlagsLocation;
  private int? _lineWrap_StartIndent;
  private LineCache? _lineWrap_LayoutCache;
  private int? _lineWrap_PositionCacheSize;
  private EdgeMode? _longLines_EdgeMode;
  private int? _longLines_EdgeColumn;
  private Color _longLines_EdgeColor;
  private MarginConfigList _margin_List = new MarginConfigList();
  private MarkersConfigList _markers_List;
  private ScrollBars? _scrolling_ScrollBars;
  private int? _scrolling_XOffset;
  private int? _scrolling_HorizontalWidth;
  private bool? _scrolling_EndAtLastLine;
  private Color _selection_ForeColor;
  private Color _selection_ForeColorUnfocused;
  private Color _selection_BackColorUnfocused;
  private Color _selection_BackColor;
  private bool? _selection_Hidden;
  private bool? _selection_HideSelection;
  private ScintillaNet.SelectionMode? _selection_Mode;
  private SnippetsConfigList _snippetsConfigList = new SnippetsConfigList();
  private StyleConfigList _styles = new StyleConfigList();
  private bool? _undoRedoIsUndoEnabled;
  private Color _whitespace_BackColor;
  private Color _whitespace_ForeColor;
  private WhitespaceMode? _whitespace_Mode;

  public string Language
  {
    get => this._language;
    set => this._language = value;
  }

  public Configuration(string language) => this._language = language;

  public Configuration(XmlDocument configDocument, string language)
  {
    this._language = language;
    this.Load(configDocument);
  }

  public Configuration(string fileName, string language, bool useXmlReader)
  {
    this._language = language;
    this.Load(fileName, useXmlReader);
  }

  public Configuration(Stream inStream, string language, bool useXmlReader)
  {
    this._language = language;
    this.Load(inStream, useXmlReader);
  }

  public Configuration(TextReader txtReader, string language)
  {
    this._language = language;
    this.Load(txtReader);
  }

  public Configuration(XmlReader reader, string language)
  {
    this._language = language;
    this.Load(reader);
  }

  public void Load(string fileName, bool useXmlReader)
  {
    if (useXmlReader)
    {
      this.Load(XmlReader.Create(fileName, new XmlReaderSettings()
      {
        IgnoreComments = true,
        IgnoreWhitespace = true
      }));
    }
    else
    {
      XmlDocument configDocument = new XmlDocument();
      configDocument.PreserveWhitespace = true;
      configDocument.Load(fileName);
      this.Load(configDocument);
    }
  }

  public void Load(Stream inStream, bool useXmlReader)
  {
    if (useXmlReader)
    {
      this.Load(XmlReader.Create(inStream, new XmlReaderSettings()
      {
        IgnoreComments = true,
        IgnoreWhitespace = true
      }));
    }
    else
    {
      XmlDocument configDocument = new XmlDocument();
      configDocument.PreserveWhitespace = true;
      configDocument.Load(inStream);
      this.Load(configDocument);
    }
  }

  public void Load(TextReader txtReader)
  {
    XmlDocument configDocument = new XmlDocument();
    configDocument.PreserveWhitespace = true;
    configDocument.Load(txtReader);
    this.Load(configDocument);
  }

  public void Load(XmlReader reader)
  {
    reader.ReadStartElement();
    while (!reader.EOF)
    {
      if (reader.Name.Equals("language", StringComparison.OrdinalIgnoreCase) && reader.HasAttributes)
      {
        while (reader.MoveToNextAttribute())
        {
          if (reader.Name.Equals("name", StringComparison.OrdinalIgnoreCase) && reader.Value.Equals(this._language, StringComparison.OrdinalIgnoreCase))
          {
            this.readLanguage(reader);
            this._hasData = true;
          }
        }
        if (this._hasData)
          reader.Skip();
        reader.Read();
      }
      else
        reader.Skip();
    }
    reader.Close();
  }

  private void readLanguage(XmlReader reader)
  {
    this._commands_KeyBindingList = new CommandBindingConfigList();
    this._lexing_Properties = new LexerPropertiesConfig();
    this._lexing_Keywords = new KeyWordConfigList();
    this._margin_List = new MarginConfigList();
    this._markers_List = new MarkersConfigList();
    this._snippetsConfigList = new SnippetsConfigList();
    this._styles = new StyleConfigList();
    reader.Read();
    while (reader.NodeType == XmlNodeType.Element)
    {
      switch (reader.Name.ToLower())
      {
        case "autocomplete":
          this.readAutoComplete(reader);
          continue;
        case "calltip":
          this.readCallTip(reader);
          continue;
        case "caret":
          this.readCaret(reader);
          continue;
        case "clipboard":
          this.readClipboard(reader);
          continue;
        case "commands":
          this.readCommands(reader);
          continue;
        case "endofline":
          this.readEndOfLine(reader);
          continue;
        case "folding":
          this.readFolding(reader);
          continue;
        case "hotspot":
          this.readHotSpot(reader);
          continue;
        case "indentation":
          this.readIndentation(reader);
          continue;
        case "indicators":
          this.readIndicators(reader);
          continue;
        case "lexer":
          this.readLexer(reader);
          continue;
        case "linewrap":
          this.readLineWrap(reader);
          continue;
        case "longlines":
          this.readLongLines(reader);
          continue;
        case "margins":
          this.readMargins(reader);
          continue;
        case "markers":
          this.readMarkers(reader);
          continue;
        case "scrolling":
          this.readScrolling(reader);
          continue;
        case "selection":
          this.readSelection(reader);
          continue;
        case "snippets":
          this.readSnippets(reader);
          continue;
        case "styles":
          this.readStyles(reader);
          continue;
        case "undoredo":
          this.readUndoRedo(reader);
          continue;
        case "whitespace":
          this.readWhitespace(reader);
          continue;
        default:
          reader.Skip();
          continue;
      }
    }
  }

  private void readWhitespace(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "backcolor":
            this._whitespace_BackColor = this.getColor(reader.Value);
            continue;
          case "forecolor":
            this._whitespace_ForeColor = this.getColor(reader.Value);
            continue;
          case "mode":
            this._whitespace_Mode = new WhitespaceMode?((WhitespaceMode) Enum.Parse(typeof (WhitespaceMode), reader.Value, true));
            continue;
          default:
            continue;
        }
      }
    }
    reader.Skip();
  }

  private void readSnippets(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "activesnippetcolor":
            this._snippetsConfigList.ActiveSnippetColor = this.getColor(reader.Value);
            continue;
          case "activesnippetindicator":
            this._snippetsConfigList.ActiveSnippetIndicator = this.getInt(reader.Value);
            continue;
          case "inactivesnippetcolor":
            this._snippetsConfigList.InactiveSnippetColor = this.getColor(reader.Value);
            continue;
          case "inactivesnippetindicator":
            this._snippetsConfigList.InactiveSnippetIndicator = this.getInt(reader.Value);
            continue;
          case "activesnippetindicatorstyle":
            this._snippetsConfigList.ActiveSnippetIndicatorStyle = new IndicatorStyle?((IndicatorStyle) Enum.Parse(typeof (IndicatorStyle), reader.Value, true));
            continue;
          case "inactivesnippetindicatorstyle":
            this._snippetsConfigList.InactiveSnippetIndicatorStyle = new IndicatorStyle?((IndicatorStyle) Enum.Parse(typeof (IndicatorStyle), reader.Value, true));
            continue;
          case "defaultdelimeter":
            this._snippetsConfigList.DefaultDelimeter = this.getChar(reader.Value);
            continue;
          case "isenabled":
            this._snippetsConfigList.IsEnabled = this.getBool(reader.Value);
            continue;
          case "isonekeyselectionembedenabled":
            this._snippetsConfigList.IsOneKeySelectionEmbedEnabled = this.getBool(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("snippets", StringComparison.OrdinalIgnoreCase))
      {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("snippet", StringComparison.OrdinalIgnoreCase) && reader.HasAttributes)
        {
          SnippetsConfig snippetsConfig = new SnippetsConfig();
          if (reader.HasAttributes)
          {
            while (reader.MoveToNextAttribute())
            {
              switch (reader.Name.ToLower())
              {
                case "shortcut":
                  snippetsConfig.Shortcut = reader.Value;
                  continue;
                case "delimeter":
                  snippetsConfig.Delimeter = this.getChar(reader.Value);
                  continue;
                case "issurroundswith":
                  snippetsConfig.IsSurroundsWith = this.getBool(reader.Value);
                  continue;
                default:
                  continue;
              }
            }
          }
          reader.MoveToElement();
          snippetsConfig.Code = reader.ReadString();
          this._snippetsConfigList.Add(snippetsConfig);
        }
      }
    }
    reader.Read();
  }

  private StyleConfig getStyleConfigFromElement(XmlReader reader)
  {
    StyleConfig configFromElement = new StyleConfig();
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "name":
            configFromElement.Name = reader.Value;
            continue;
          case "number":
            configFromElement.Number = this.getInt(reader.Value);
            continue;
          case "backcolor":
            configFromElement.BackColor = this.getColor(reader.Value);
            continue;
          case "bold":
            configFromElement.Bold = this.getBool(reader.Value);
            continue;
          case "case":
            configFromElement.Case = new StyleCase?((StyleCase) Enum.Parse(typeof (StyleCase), reader.Value, true));
            continue;
          case "characterset":
            configFromElement.CharacterSet = new CharacterSet?((CharacterSet) Enum.Parse(typeof (CharacterSet), reader.Value, true));
            continue;
          case "fontname":
            configFromElement.FontName = reader.Value;
            continue;
          case "forecolor":
            configFromElement.ForeColor = this.getColor(reader.Value);
            continue;
          case "ischangeable":
            configFromElement.IsChangeable = this.getBool(reader.Value);
            continue;
          case "ishotspot":
            configFromElement.IsHotspot = this.getBool(reader.Value);
            continue;
          case "isselectioneolfilled":
            configFromElement.IsSelectionEolFilled = this.getBool(reader.Value);
            continue;
          case "isvisible":
            configFromElement.IsVisible = this.getBool(reader.Value);
            continue;
          case "italic":
            configFromElement.Italic = this.getBool(reader.Value);
            continue;
          case "size":
            configFromElement.Size = this.getInt(reader.Value);
            continue;
          case "underline":
            configFromElement.Underline = this.getBool(reader.Value);
            continue;
          case "inherit":
            configFromElement.Inherit = this.getBool(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    return configFromElement;
  }

  private void readStyles(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name.ToLower() == "bits")
          this._undoRedoIsUndoEnabled = this.getBool(reader.Value);
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("styles", StringComparison.OrdinalIgnoreCase))
      {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("style", StringComparison.OrdinalIgnoreCase))
          this._styles.Add(this.getStyleConfigFromElement(reader));
        else if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("sublanguage", StringComparison.OrdinalIgnoreCase))
          this.readSubLanguage(reader);
      }
    }
    reader.Read();
  }

  private void readSubLanguage(XmlReader reader)
  {
    string empty = string.Empty;
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name.ToLower() == "name")
          empty = reader.Value;
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("sublanguage", StringComparison.OrdinalIgnoreCase))
      {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("style", StringComparison.OrdinalIgnoreCase))
        {
          StyleConfig configFromElement = this.getStyleConfigFromElement(reader);
          configFromElement.Name = $"{empty}.{configFromElement.Name}";
          this._styles.Add(configFromElement);
        }
      }
    }
    reader.Read();
  }

  private void readUndoRedo(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name.ToLower() == "isundoenabled")
          this._undoRedoIsUndoEnabled = this.getBool(reader.Value);
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readSelection(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "backcolor":
            this._selection_BackColor = this.getColor(reader.Value);
            continue;
          case "backcolorunfocused":
            this._selection_BackColorUnfocused = this.getColor(reader.Value);
            continue;
          case "forecolor":
            this._selection_ForeColor = this.getColor(reader.Value);
            continue;
          case "forecolorunfocused":
            this._selection_ForeColorUnfocused = this.getColor(reader.Value);
            continue;
          case "hidden":
            this._selection_Hidden = this.getBool(reader.Value);
            continue;
          case "hideselection":
            this._selection_HideSelection = this.getBool(reader.Value);
            continue;
          case "mode":
            this._selection_Mode = new ScintillaNet.SelectionMode?((ScintillaNet.SelectionMode) Enum.Parse(typeof (ScintillaNet.SelectionMode), reader.Value, true));
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readScrolling(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "endatlastline":
            this._scrolling_EndAtLastLine = this.getBool(reader.Value);
            continue;
          case "horizontalwidth":
            this._scrolling_HorizontalWidth = this.getInt(reader.Value);
            continue;
          case "scrollbars":
            string str1 = reader.Value.Trim();
            if (str1 != string.Empty)
            {
              ScrollBars? nullable1 = new ScrollBars?();
              string str2 = str1;
              char[] chArray = new char[1]{ ' ' };
              foreach (string str3 in str2.Split(chArray))
              {
                ScrollBars? nullable2 = nullable1;
                ScrollBars scrollBars = (ScrollBars) Enum.Parse(typeof (ScrollBars), str3.Trim(), true);
                nullable1 = nullable2.HasValue ? new ScrollBars?(nullable2.GetValueOrDefault() | scrollBars) : new ScrollBars?();
              }
              if (nullable1.HasValue)
              {
                this._scrolling_ScrollBars = nullable1;
                continue;
              }
              continue;
            }
            continue;
          case "xoffset":
            this._scrolling_XOffset = this.getInt(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readMarkers(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name.ToLower() == "inherit")
          this._markers_List.Inherit = this.getBool(reader.Value);
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("markers", StringComparison.OrdinalIgnoreCase))
      {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("marker", StringComparison.OrdinalIgnoreCase) && reader.HasAttributes)
        {
          MarkersConfig markersConfig = new MarkersConfig();
          while (reader.MoveToNextAttribute())
          {
            switch (reader.Name.ToLower())
            {
              case "alpha":
                markersConfig.Alpha = this.getInt(reader.Value);
                continue;
              case "backcolor":
                markersConfig.BackColor = this.getColor(reader.Value);
                continue;
              case "forecolor":
                markersConfig.ForeColor = this.getColor(reader.Value);
                continue;
              case "name":
                markersConfig.Name = reader.Value;
                continue;
              case "number":
                markersConfig.Number = this.getInt(reader.Value);
                continue;
              case "inherit":
                markersConfig.Inherit = this.getBool(reader.Value);
                continue;
              case "symbol":
                markersConfig.Symbol = new MarkerSymbol?((MarkerSymbol) Enum.Parse(typeof (MarkerSymbol), reader.Value, true));
                continue;
              default:
                continue;
            }
          }
          reader.MoveToElement();
          this._markers_List.Add(markersConfig);
        }
      }
    }
    reader.Read();
  }

  private void readMargins(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "foldmargincolor":
            this._margin_List.FoldMarginColor = this.getColor(reader.Value);
            continue;
          case "foldmarginhighlightcolor":
            this._margin_List.FoldMarginHighlightColor = this.getColor(reader.Value);
            continue;
          case "left":
            this._margin_List.Left = this.getInt(reader.Value);
            continue;
          case "right":
            this._margin_List.Right = this.getInt(reader.Value);
            continue;
          case "inherit":
            this._margin_List.Inherit = this.getBool(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("margins", StringComparison.OrdinalIgnoreCase))
      {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("margin", StringComparison.OrdinalIgnoreCase) && reader.HasAttributes)
        {
          MarginConfig marginConfig = new MarginConfig();
          while (reader.MoveToNextAttribute())
          {
            switch (reader.Name.ToLower())
            {
              case "number":
                marginConfig.Number = int.Parse(reader.Value);
                continue;
              case "inherit":
                marginConfig.Inherit = this.getBool(reader.Value);
                continue;
              case "autotogglemarkernumber":
                marginConfig.AutoToggleMarkerNumber = this.getInt(reader.Value);
                continue;
              case "isclickable":
                marginConfig.IsClickable = this.getBool(reader.Value);
                continue;
              case "isfoldmargin":
                marginConfig.IsFoldMargin = this.getBool(reader.Value);
                continue;
              case "ismarkermargin":
                marginConfig.IsMarkerMargin = this.getBool(reader.Value);
                continue;
              case "type":
                marginConfig.Type = new MarginType?((MarginType) Enum.Parse(typeof (MarginType), reader.Value, true));
                continue;
              case "width":
                marginConfig.Width = this.getInt(reader.Value);
                continue;
              default:
                continue;
            }
          }
          this._margin_List.Add(marginConfig);
          reader.MoveToElement();
        }
      }
    }
    reader.Read();
  }

  private void readLongLines(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "edgecolor":
            this._longLines_EdgeColor = this.getColor(reader.Value);
            continue;
          case "edgecolumn":
            this._longLines_EdgeColumn = this.getInt(reader.Value);
            continue;
          case "edgemode":
            this._longLines_EdgeMode = new EdgeMode?((EdgeMode) Enum.Parse(typeof (EdgeMode), reader.Value, true));
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readLineWrap(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "layoutcache":
            this._lineWrap_LayoutCache = new LineCache?((LineCache) Enum.Parse(typeof (LineCache), reader.Value, true));
            continue;
          case "mode":
            this._lineWrap_Mode = new WrapMode?((WrapMode) Enum.Parse(typeof (WrapMode), reader.Value, true));
            continue;
          case "positioncachesize":
            this._lineWrap_PositionCacheSize = this.getInt(reader.Value);
            continue;
          case "startindent":
            this._lineWrap_StartIndent = this.getInt(reader.Value);
            continue;
          case "visualflags":
            string str1 = reader.Value.Trim();
            if (str1 != string.Empty)
            {
              WrapVisualFlag? nullable1 = new WrapVisualFlag?();
              string str2 = str1;
              char[] chArray = new char[1]{ ' ' };
              foreach (string str3 in str2.Split(chArray))
              {
                WrapVisualFlag? nullable2 = nullable1;
                WrapVisualFlag wrapVisualFlag = (WrapVisualFlag) Enum.Parse(typeof (WrapVisualFlag), str3.Trim(), true);
                nullable1 = nullable2.HasValue ? new WrapVisualFlag?(nullable2.GetValueOrDefault() | wrapVisualFlag) : new WrapVisualFlag?();
              }
              if (nullable1.HasValue)
              {
                this._lineWrap_VisualFlags = nullable1;
                continue;
              }
              continue;
            }
            continue;
          case "visualflagslocation":
            this._lineWrap_VisualFlagsLocation = new WrapVisualLocation?((WrapVisualLocation) Enum.Parse(typeof (WrapVisualLocation), reader.Value, true));
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readLexer(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "whitespacechars":
            this._lexing_WhitespaceChars = reader.Value;
            continue;
          case "wordchars":
            this._lexing_WordChars = reader.Value;
            continue;
          case "lexername":
            this._lexing_Language = reader.Value;
            continue;
          case "linecommentprefix":
            this._lexing_LineCommentPrefix = reader.Value;
            continue;
          case "streamcommentprefix":
            this._lexing_StreamCommentPrefix = reader.Value;
            continue;
          case "streamcommentsuffix":
            this._lexing_StreamCommentSuffix = reader.Value;
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      reader.Read();
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("lexer", StringComparison.OrdinalIgnoreCase))
      {
        if (reader.NodeType == XmlNodeType.Element)
        {
          if (reader.Name.Equals("properties", StringComparison.OrdinalIgnoreCase))
            this.readLexerProperties(reader);
          else if (reader.Name.Equals("keywords", StringComparison.OrdinalIgnoreCase))
            this.readLexerKeywords(reader);
        }
      }
    }
    reader.Read();
  }

  private void readLexerKeywords(XmlReader reader)
  {
    bool? inherit = new bool?();
    int? nullable = new int?();
    string str = (string) null;
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "inherit":
            inherit = this.getBool(reader.Value);
            continue;
          case "list":
            nullable = this.getInt(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
      str = reader.ReadString().Trim();
    this._lexing_Keywords.Add(new KeyWordConfig(nullable.Value, str, inherit));
    reader.Read();
  }

  private void readLexerProperties(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name.ToLower() == "inherit")
          this._lexing_Properties.Inherit = this.getBool(reader.Value);
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("properties", StringComparison.OrdinalIgnoreCase))
      {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("property", StringComparison.OrdinalIgnoreCase) && reader.HasAttributes)
        {
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          while (reader.MoveToNextAttribute())
          {
            switch (reader.Name.ToLower())
            {
              case "name":
                empty1 = reader.Value;
                continue;
              case "value":
                empty2 = reader.Value;
                continue;
              default:
                continue;
            }
          }
          this._lexing_Properties.Add(empty1, empty2);
          reader.MoveToElement();
        }
      }
    }
    reader.Read();
  }

  private void readIndicators(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "inherit":
            this._indicator_List.Inherit = this.getBool(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("indicators", StringComparison.OrdinalIgnoreCase))
      {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("indicator", StringComparison.OrdinalIgnoreCase) && reader.HasAttributes)
        {
          IndicatorConfig indicatorConfig = new IndicatorConfig();
          while (reader.MoveToNextAttribute())
          {
            switch (reader.Name.ToLower())
            {
              case "number":
                indicatorConfig.Number = int.Parse(reader.Value);
                continue;
              case "color":
                indicatorConfig.Color = this.getColor(reader.Value);
                continue;
              case "inherit":
                indicatorConfig.Inherit = this.getBool(reader.Value);
                continue;
              case "isdrawnunder":
                indicatorConfig.IsDrawnUnder = this.getBool(reader.Value);
                continue;
              case "style":
                indicatorConfig.Style = new IndicatorStyle?((IndicatorStyle) Enum.Parse(typeof (IndicatorStyle), reader.Value, true));
                continue;
              default:
                continue;
            }
          }
          this._indicator_List.Add(indicatorConfig);
          reader.MoveToElement();
        }
      }
    }
    reader.Read();
  }

  private void readIndentation(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "backspaceunindents":
            this._indentation_BackspaceUnindents = this.getBool(reader.Value);
            continue;
          case "indentwidth":
            this._indentation_IndentWidth = this.getInt(reader.Value);
            continue;
          case "showguides":
            this._indentation_ShowGuides = this.getBool(reader.Value);
            continue;
          case "tabindents":
            this._indentation_TabIndents = this.getBool(reader.Value);
            continue;
          case "tabwidth":
            this._indentation_TabWidth = this.getInt(reader.Value);
            continue;
          case "usetabs":
            this._indentation_UseTabs = this.getBool(reader.Value);
            continue;
          case "smartindenttype":
            this._indentation_SmartIndentType = new SmartIndent?((SmartIndent) Enum.Parse(typeof (SmartIndent), reader.Value, true));
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readHotSpot(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "activebackcolor":
            this._hotspot_ActiveBackColor = this.getColor(reader.Value);
            continue;
          case "activeforecolor":
            this._hotspot_ActiveForeColor = this.getColor(reader.Value);
            continue;
          case "activeunderline":
            this._hotspot_ActiveUnderline = this.getBool(reader.Value);
            continue;
          case "singleline":
            this._hotspot_SingleLine = this.getBool(reader.Value);
            continue;
          case "useactivebackcolor":
            this._hotspot_UseActiveBackColor = this.getBool(reader.Value);
            continue;
          case "useactiveforecolor":
            this._hotspot_UseActiveForeColor = this.getBool(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readFolding(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "flags":
            string str1 = reader.Value.Trim();
            if (str1 != string.Empty)
            {
              FoldFlag? nullable1 = new FoldFlag?();
              string str2 = str1;
              char[] chArray = new char[1]{ ' ' };
              foreach (string str3 in str2.Split(chArray))
              {
                FoldFlag? nullable2 = nullable1;
                FoldFlag foldFlag = (FoldFlag) Enum.Parse(typeof (FoldFlag), str3.Trim(), true);
                nullable1 = nullable2.HasValue ? new FoldFlag?(nullable2.GetValueOrDefault() | foldFlag) : new FoldFlag?();
              }
              if (nullable1.HasValue)
              {
                this._folding_Flags = nullable1;
                continue;
              }
              continue;
            }
            continue;
          case "IsEnabled":
            this._folding_MarkerScheme = new FoldMarkerScheme?((FoldMarkerScheme) Enum.Parse(typeof (FoldMarkerScheme), reader.Value, true));
            continue;
          case "usecompactfolding":
            this._folding_UseCompactFolding = this.getBool(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readEndOfLine(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "convertonpaste":
            this._endOfLine_ConvertOnPaste = this.getBool(reader.Value);
            continue;
          case "isvisible":
            this._endOfLine_IsVisisble = this.getBool(reader.Value);
            continue;
          case "mode":
            this._endOfLine_Mode = new EndOfLineMode?((EndOfLineMode) Enum.Parse(typeof (EndOfLineMode), reader.Value, true));
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readCommands(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "Inherit":
            this._commands_KeyBindingList.Inherit = this.getBool(reader.Value);
            continue;
          case "AllowDuplicateBindings":
            this._commands_KeyBindingList.AllowDuplicateBindings = this.getBool(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("commands", StringComparison.OrdinalIgnoreCase))
      {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("binding", StringComparison.OrdinalIgnoreCase))
        {
          if (reader.HasAttributes)
          {
            KeyBinding keyBinding = new KeyBinding();
            BindableCommand bindableCommand = (BindableCommand) 0;
            bool? replaceCurrent = new bool?();
            while (reader.MoveToNextAttribute())
            {
              switch (reader.Name.ToLower())
              {
                case "key":
                  keyBinding.KeyCode = Utilities.GetKeys(reader.Value);
                  continue;
                case "modifier":
                  if (reader.Value != string.Empty)
                  {
                    string str1 = reader.Value;
                    char[] chArray = new char[1]{ ' ' };
                    foreach (string str2 in str1.Split(chArray))
                      keyBinding.Modifiers |= (Keys) Enum.Parse(typeof (Keys), str2.Trim(), true);
                    continue;
                  }
                  continue;
                case "command":
                  bindableCommand = (BindableCommand) Enum.Parse(typeof (BindableCommand), reader.Value, true);
                  continue;
                case "replacecurrent":
                  replaceCurrent = this.getBool(reader.Value);
                  continue;
                default:
                  continue;
              }
            }
            this._commands_KeyBindingList.Add(new CommandBindingConfig(keyBinding, replaceCurrent, bindableCommand));
          }
          reader.MoveToElement();
        }
      }
    }
    reader.Read();
  }

  private void readClipboard(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "convertendoflineonpaste":
            this._clipboard_ConvertEndOfLineOnPaste = this.getBool(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readCaret(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "blinkrate":
            string s = reader.Value;
            this._caret_BlinkRate = !(s.ToLower() == "system") ? this.getInt(s) : new int?(SystemInformation.CaretBlinkTime);
            continue;
          case "color":
            this._caret_Color = this.getColor(reader.Value);
            continue;
          case "currentlinebackgroundalpha":
            this._caret_CurrentLineBackgroundAlpha = this.getInt(reader.Value);
            continue;
          case "currentlinebackgroundcolor":
            this._caret_CurrentLineBackgroundColor = this.getColor(reader.Value);
            continue;
          case "highlightcurrentline":
            this._caret_HighlightCurrentLine = this.getBool(reader.Value);
            continue;
          case "issticky":
            this._caret_IsSticky = this.getBool(reader.Value);
            continue;
          case "style":
            this._caret_Style = new CaretStyle?((CaretStyle) Enum.Parse(typeof (CaretStyle), reader.Value, true));
            continue;
          case "width":
            this._caret_Width = this.getInt(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readCallTip(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "backcolor":
            this._callTip_BackColor = this.getColor(reader.Value);
            continue;
          case "forecolor":
            this._callTip_ForeColor = this.getColor(reader.Value);
            continue;
          case "highlighttextcolor":
            this._callTip_HighlightTextColor = this.getColor(reader.Value);
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    reader.Skip();
  }

  private void readAutoComplete(XmlReader reader)
  {
    if (reader.HasAttributes)
    {
      while (reader.MoveToNextAttribute())
      {
        switch (reader.Name.ToLower())
        {
          case "autohide":
            this._autoComplete_AutoHide = this.getBool(reader.Value);
            continue;
          case "automaticlengthentered":
            this._autoComplete_AutomaticLengthEntered = this.getBool(reader.Value);
            continue;
          case "cancelatstart":
            this._autoComplete_cancelAtStart = this.getBool(reader.Value);
            continue;
          case "droprestofword":
            this._autoComplete_DropRestOfWord = this.getBool(reader.Value);
            continue;
          case "fillupcharacters":
            this._autoComplete_fillUpCharacters = reader.Value;
            continue;
          case "imageseperator":
            this._autoComplete_ImageSeperator = this.getChar(reader.Value);
            continue;
          case "iscasesensitive":
            this._autoComplete_IsCaseSensitive = this.getBool(reader.Value);
            continue;
          case "listseperator":
            this._autoComplete_ListSeperator = this.getChar(reader.Value);
            continue;
          case "maxheight":
            this._autoComplete_MaxHeight = this.getInt(reader.Value);
            continue;
          case "maxwidth":
            this._autoComplete_MaxWidth = this.getInt(reader.Value);
            continue;
          case "singlelineaccept":
            this._autoComplete_singleLineAccept = this.getBool(reader.Value);
            continue;
          case "stopcharacters":
            this._autoComplete_StopCharacters = reader.Value;
            continue;
          default:
            continue;
        }
      }
      reader.MoveToElement();
    }
    if (!reader.IsEmptyElement)
    {
      while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("autocomplete", StringComparison.OrdinalIgnoreCase))
      {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("list", StringComparison.OrdinalIgnoreCase))
        {
          if (reader.HasAttributes)
          {
            while (reader.MoveToNextAttribute())
            {
              if (reader.Name.Equals("inherit", StringComparison.OrdinalIgnoreCase))
                this._autoComplete_ListInherit = this.getBool(reader.Value);
            }
            reader.MoveToElement();
          }
          this._autoComplete_List = new Regex("\\s+").Replace(reader.ReadString(), " ").Trim();
        }
      }
    }
    reader.Read();
  }

  public void Load(XmlDocument configDocument)
  {
    XmlNode xmlNode = configDocument.DocumentElement.SelectSingleNode($"./Language[@Name='{this._language}']");
    if (xmlNode == null)
      return;
    if (xmlNode.SelectSingleNode("AutoComplete") is XmlElement xmlElement1)
    {
      this._autoComplete_AutoHide = this.getBool(xmlElement1.GetAttribute("AutoHide"));
      this._autoComplete_AutomaticLengthEntered = this.getBool(xmlElement1.GetAttribute("AutomaticLengthEntered"));
      this._autoComplete_cancelAtStart = this.getBool(xmlElement1.GetAttribute("CancelAtStart"));
      this._autoComplete_DropRestOfWord = this.getBool(xmlElement1.GetAttribute("DropRestOfWord"));
      this._autoComplete_fillUpCharacters = this.getString(xmlElement1.GetAttributeNode("FillUpCharacters"));
      this._autoComplete_ImageSeperator = this.getChar(xmlElement1.GetAttribute("AutomaticLengthEntered"));
      this._autoComplete_IsCaseSensitive = this.getBool(xmlElement1.GetAttribute("IsCaseSensitive"));
      this._autoComplete_ListSeperator = this.getChar(xmlElement1.GetAttribute("ListSeperator"));
      this._autoComplete_MaxHeight = this.getInt(xmlElement1.GetAttribute("MaxHeight"));
      this._autoComplete_MaxWidth = this.getInt(xmlElement1.GetAttribute("MaxWidth"));
      this._autoComplete_singleLineAccept = this.getBool(xmlElement1.GetAttribute("SingleLineAccept"));
      this._autoComplete_StopCharacters = this.getString(xmlElement1.GetAttributeNode("StopCharacters"));
      if (xmlElement1.SelectSingleNode("./List") is XmlElement xmlElement)
      {
        this._autoComplete_ListInherit = this.getBool(xmlElement.GetAttribute("Inherit"));
        this._autoComplete_List = new Regex("\\s+").Replace(xmlElement.InnerText, " ").Trim();
      }
    }
    if (xmlNode.SelectSingleNode("CallTip") is XmlElement xmlElement2)
    {
      this._callTip_BackColor = this.getColor(xmlElement2.GetAttribute("BackColor"));
      this._callTip_ForeColor = this.getColor(xmlElement2.GetAttribute("ForeColor"));
      this._callTip_HighlightTextColor = this.getColor(xmlElement2.GetAttribute("HighlightTextColor"));
    }
    if (xmlNode.SelectSingleNode("Caret") is XmlElement xmlElement3)
    {
      string attribute = xmlElement3.GetAttribute("BlinkRate");
      this._caret_BlinkRate = !(attribute.ToLower() == "system") ? this.getInt(attribute) : new int?(SystemInformation.CaretBlinkTime);
      this._caret_Color = this.getColor(xmlElement3.GetAttribute("Color"));
      this._caret_CurrentLineBackgroundAlpha = this.getInt(xmlElement3.GetAttribute("CurrentLineBackgroundAlpha"));
      this._caret_CurrentLineBackgroundColor = this.getColor(xmlElement3.GetAttribute("CurrentLineBackgroundColor"));
      this._caret_HighlightCurrentLine = this.getBool(xmlElement3.GetAttribute("HighlightCurrentLine"));
      this._caret_IsSticky = this.getBool(xmlElement3.GetAttribute("IsSticky"));
      try
      {
        this._caret_Style = new CaretStyle?((CaretStyle) Enum.Parse(typeof (CaretStyle), xmlElement3.GetAttribute("Style"), true));
      }
      catch (ArgumentException ex)
      {
      }
      this._caret_Width = this.getInt(xmlElement3.GetAttribute("Width"));
    }
    if (xmlNode.SelectSingleNode("Clipboard") is XmlElement xmlElement4)
      this._clipboard_ConvertEndOfLineOnPaste = this.getBool(xmlElement4.GetAttribute("ConvertEndOfLineOnPaste"));
    this._commands_KeyBindingList = new CommandBindingConfigList();
    if (xmlNode.SelectSingleNode("Commands") is XmlElement xmlElement5)
    {
      this._commands_KeyBindingList.Inherit = this.getBool(xmlElement5.GetAttribute("Inherit"));
      this._commands_KeyBindingList.AllowDuplicateBindings = this.getBool(xmlElement5.GetAttribute("AllowDuplicateBindings"));
      foreach (XmlElement selectNode in xmlElement5.SelectNodes("./Binding"))
      {
        KeyBinding keyBinding = new KeyBinding();
        keyBinding.KeyCode = Utilities.GetKeys(selectNode.GetAttribute("Key"));
        string attribute = selectNode.GetAttribute("Modifier");
        if (attribute != string.Empty)
        {
          string str1 = attribute;
          char[] chArray = new char[1]{ ' ' };
          foreach (string str2 in str1.Split(chArray))
            keyBinding.Modifiers |= (Keys) Enum.Parse(typeof (Keys), str2.Trim(), true);
        }
        BindableCommand bindableCommand = (BindableCommand) Enum.Parse(typeof (BindableCommand), selectNode.GetAttribute("Command"), true);
        this._commands_KeyBindingList.Add(new CommandBindingConfig(keyBinding, this.getBool(selectNode.GetAttribute("ReplaceCurrent")), bindableCommand));
      }
    }
    if (xmlNode.SelectSingleNode("EndOfLine") is XmlElement xmlElement6)
    {
      this._endOfLine_ConvertOnPaste = this.getBool(xmlElement6.GetAttribute("ConvertOnPaste"));
      this._endOfLine_IsVisisble = this.getBool(xmlElement6.GetAttribute("IsVisible"));
      try
      {
        this._endOfLine_Mode = new EndOfLineMode?((EndOfLineMode) Enum.Parse(typeof (EndOfLineMode), xmlElement6.GetAttribute("Mode"), true));
      }
      catch (ArgumentException ex)
      {
      }
    }
    if (xmlNode.SelectSingleNode("Folding") is XmlElement xmlElement7)
    {
      string str3 = xmlElement7.GetAttribute("Flags").Trim();
      if (str3 != string.Empty)
      {
        FoldFlag? nullable1 = new FoldFlag?();
        string str4 = str3;
        char[] chArray = new char[1]{ ' ' };
        foreach (string str5 in str4.Split(chArray))
        {
          FoldFlag? nullable2 = nullable1;
          FoldFlag foldFlag = (FoldFlag) Enum.Parse(typeof (FoldFlag), str5.Trim(), true);
          nullable1 = nullable2.HasValue ? new FoldFlag?(nullable2.GetValueOrDefault() | foldFlag) : new FoldFlag?();
        }
        if (nullable1.HasValue)
          this._folding_Flags = nullable1;
      }
      this._folding_IsEnabled = this.getBool(xmlElement7.GetAttribute("IsEnabled"));
      try
      {
        this._folding_MarkerScheme = new FoldMarkerScheme?((FoldMarkerScheme) Enum.Parse(typeof (FoldMarkerScheme), xmlElement7.GetAttribute("MarkerScheme"), true));
      }
      catch (ArgumentException ex)
      {
      }
      this._folding_UseCompactFolding = this.getBool(xmlElement7.GetAttribute("UseCompactFolding"));
    }
    if (xmlNode.SelectSingleNode("HotSpot") is XmlElement xmlElement8)
    {
      this._hotspot_ActiveBackColor = this.getColor(xmlElement8.GetAttribute("ActiveBackColor"));
      this._hotspot_ActiveForeColor = this.getColor(xmlElement8.GetAttribute("ActiveForeColor"));
      this._hotspot_ActiveUnderline = this.getBool(xmlElement8.GetAttribute("ActiveUnderline"));
      this._hotspot_SingleLine = this.getBool(xmlElement8.GetAttribute("SingleLine"));
      this._hotspot_UseActiveBackColor = this.getBool(xmlElement8.GetAttribute("UseActiveBackColor"));
      this._hotspot_UseActiveForeColor = this.getBool(xmlElement8.GetAttribute("UseActiveForeColor"));
    }
    if (xmlNode.SelectSingleNode("Indentation") is XmlElement xmlElement9)
    {
      this._indentation_BackspaceUnindents = this.getBool(xmlElement9.GetAttribute("BackspaceUnindents"));
      this._indentation_IndentWidth = this.getInt(xmlElement9.GetAttribute("IndentWidth"));
      this._indentation_ShowGuides = this.getBool(xmlElement9.GetAttribute("ShowGuides"));
      this._indentation_TabIndents = this.getBool(xmlElement9.GetAttribute("TabIndents"));
      this._indentation_TabWidth = this.getInt(xmlElement9.GetAttribute("TabWidth"));
      this._indentation_UseTabs = this.getBool(xmlElement9.GetAttribute("UseTabs"));
      try
      {
        this._indentation_SmartIndentType = new SmartIndent?((SmartIndent) Enum.Parse(typeof (SmartIndent), xmlElement9.GetAttribute("SmartIndentType"), true));
      }
      catch (ArgumentException ex)
      {
      }
    }
    if (xmlNode.SelectSingleNode("Indicators") is XmlElement xmlElement10)
    {
      this._indicator_List.Inherit = this.getBool(xmlElement10.GetAttribute("Inherit"));
      foreach (XmlElement selectNode in xmlElement10.SelectNodes("Indicator"))
      {
        IndicatorConfig indicatorConfig = new IndicatorConfig();
        indicatorConfig.Number = int.Parse(selectNode.GetAttribute("Number"));
        indicatorConfig.Color = this.getColor(selectNode.GetAttribute("Color"));
        indicatorConfig.Inherit = this.getBool(selectNode.GetAttribute("Inherit"));
        indicatorConfig.IsDrawnUnder = this.getBool(selectNode.GetAttribute("IsDrawnUnder"));
        try
        {
          indicatorConfig.Style = new IndicatorStyle?((IndicatorStyle) Enum.Parse(typeof (IndicatorStyle), selectNode.GetAttribute("Style"), true));
        }
        catch (ArgumentException ex)
        {
        }
        this._indicator_List.Add(indicatorConfig);
      }
    }
    this._lexing_Properties = new LexerPropertiesConfig();
    this._lexing_Keywords = new KeyWordConfigList();
    if (xmlNode.SelectSingleNode("Lexer") is XmlElement xmlElement12)
    {
      this._lexing_WhitespaceChars = this.getString(xmlElement12.GetAttributeNode("WhitespaceChars"));
      this._lexing_WordChars = this.getString(xmlElement12.GetAttributeNode("WordChars"));
      this._lexing_Language = this.getString(xmlElement12.GetAttributeNode("LexerName"));
      this._lexing_LineCommentPrefix = this.getString(xmlElement12.GetAttributeNode("LineCommentPrefix"));
      this._lexing_StreamCommentPrefix = this.getString(xmlElement12.GetAttributeNode("StreamCommentPrefix"));
      this._lexing_StreamCommentSuffix = this.getString(xmlElement12.GetAttributeNode("StreamCommentSuffix"));
      if (xmlElement12.SelectSingleNode("Properties") is XmlElement xmlElement11)
      {
        this._lexing_Properties.Inherit = this.getBool(xmlElement11.GetAttribute("Inherit"));
        foreach (XmlElement selectNode in xmlElement11.SelectNodes("Property"))
          this._lexing_Properties.Add(selectNode.GetAttribute("Name"), selectNode.GetAttribute("Value"));
      }
      foreach (XmlElement selectNode in xmlElement12.SelectNodes("Keywords"))
        this._lexing_Keywords.Add(new KeyWordConfig(this.getInt(selectNode.GetAttribute("List")).Value, selectNode.InnerText.Trim(), this.getBool(selectNode.GetAttribute("Inherit"))));
    }
    if (xmlNode.SelectSingleNode("LineWrap") is XmlElement xmlElement13)
    {
      try
      {
        this._lineWrap_LayoutCache = new LineCache?((LineCache) Enum.Parse(typeof (LineCache), xmlElement13.GetAttribute("LayoutCache"), true));
      }
      catch (ArgumentException ex)
      {
      }
      try
      {
        this._lineWrap_Mode = new WrapMode?((WrapMode) Enum.Parse(typeof (WrapMode), xmlElement13.GetAttribute("Mode"), true));
      }
      catch (ArgumentException ex)
      {
      }
      this._lineWrap_PositionCacheSize = this.getInt(xmlElement13.GetAttribute("PositionCacheSize"));
      this._lineWrap_StartIndent = this.getInt(xmlElement13.GetAttribute("StartIndent"));
      string str6 = xmlElement13.GetAttribute("VisualFlags").Trim();
      if (str6 != string.Empty)
      {
        WrapVisualFlag? nullable3 = new WrapVisualFlag?();
        string str7 = str6;
        char[] chArray = new char[1]{ ' ' };
        foreach (string str8 in str7.Split(chArray))
        {
          WrapVisualFlag? nullable4 = nullable3;
          WrapVisualFlag wrapVisualFlag = (WrapVisualFlag) Enum.Parse(typeof (WrapVisualFlag), str8.Trim(), true);
          nullable3 = nullable4.HasValue ? new WrapVisualFlag?(nullable4.GetValueOrDefault() | wrapVisualFlag) : new WrapVisualFlag?();
        }
        if (nullable3.HasValue)
          this._lineWrap_VisualFlags = nullable3;
      }
      try
      {
        this._lineWrap_VisualFlagsLocation = new WrapVisualLocation?((WrapVisualLocation) Enum.Parse(typeof (WrapVisualLocation), xmlElement13.GetAttribute("VisualFlagsLocation"), true));
      }
      catch (ArgumentException ex)
      {
      }
    }
    if (xmlNode.SelectSingleNode("LongLines") is XmlElement xmlElement14)
    {
      this._longLines_EdgeColor = this.getColor(xmlElement14.GetAttribute("EdgeColor"));
      this._longLines_EdgeColumn = this.getInt(xmlElement14.GetAttribute("EdgeColumn"));
      try
      {
        this._longLines_EdgeMode = new EdgeMode?((EdgeMode) Enum.Parse(typeof (EdgeMode), xmlElement14.GetAttribute("EdgeMode"), true));
      }
      catch (ArgumentException ex)
      {
      }
    }
    this._margin_List = new MarginConfigList();
    if (xmlNode.SelectSingleNode("Margins") is XmlElement xmlElement15)
    {
      this._margin_List.FoldMarginColor = this.getColor(xmlElement15.GetAttribute("FoldMarginColor"));
      this._margin_List.FoldMarginHighlightColor = this.getColor(xmlElement15.GetAttribute("FoldMarginHighlightColor"));
      this._margin_List.Left = this.getInt(xmlElement15.GetAttribute("Left"));
      this._margin_List.Right = this.getInt(xmlElement15.GetAttribute("Right"));
      this._margin_List.Inherit = this.getBool(xmlElement15.GetAttribute("Inherit"));
      foreach (XmlElement selectNode in xmlElement15.SelectNodes("./Margin"))
      {
        MarginConfig marginConfig = new MarginConfig();
        marginConfig.Number = int.Parse(selectNode.GetAttribute("Number"));
        marginConfig.Inherit = this.getBool(selectNode.GetAttribute("Inherit"));
        marginConfig.AutoToggleMarkerNumber = this.getInt(selectNode.GetAttribute("AutoToggleMarkerNumber"));
        marginConfig.IsClickable = this.getBool(selectNode.GetAttribute("IsClickable"));
        marginConfig.IsFoldMargin = this.getBool(selectNode.GetAttribute("IsFoldMargin"));
        marginConfig.IsMarkerMargin = this.getBool(selectNode.GetAttribute("IsMarkerMargin"));
        try
        {
          marginConfig.Type = new MarginType?((MarginType) Enum.Parse(typeof (MarginType), selectNode.GetAttribute("Type"), true));
        }
        catch (ArgumentException ex)
        {
        }
        marginConfig.Width = this.getInt(selectNode.GetAttribute("Width"));
        this._margin_List.Add(marginConfig);
      }
    }
    XmlElement xmlElement16 = xmlNode.SelectSingleNode("Markers") as XmlElement;
    this._markers_List = new MarkersConfigList();
    if (xmlElement16 != null)
    {
      this._markers_List.Inherit = this.getBool(xmlElement16.GetAttribute("Inherit"));
      foreach (XmlElement selectNode in xmlElement16.SelectNodes("Marker"))
      {
        MarkersConfig markersConfig = new MarkersConfig();
        markersConfig.Alpha = this.getInt(selectNode.GetAttribute("Alpha"));
        markersConfig.BackColor = this.getColor(selectNode.GetAttribute("BackColor"));
        markersConfig.ForeColor = this.getColor(selectNode.GetAttribute("ForeColor"));
        markersConfig.Name = this.getString(selectNode.GetAttributeNode("Name"));
        markersConfig.Number = this.getInt(selectNode.GetAttribute("Number"));
        markersConfig.Inherit = this.getBool(selectNode.GetAttribute("Inherit"));
        try
        {
          markersConfig.Symbol = new MarkerSymbol?((MarkerSymbol) Enum.Parse(typeof (MarkerSymbol), selectNode.GetAttribute("Symbol"), true));
        }
        catch (ArgumentException ex)
        {
        }
        this._markers_List.Add(markersConfig);
      }
    }
    if (xmlNode.SelectSingleNode("Scrolling") is XmlElement xmlElement17)
    {
      this._scrolling_EndAtLastLine = this.getBool(xmlElement17.GetAttribute("EndAtLastLine"));
      this._scrolling_HorizontalWidth = this.getInt(xmlElement17.GetAttribute("HorizontalWidth"));
      string str9 = xmlElement17.GetAttribute("ScrollBars").Trim();
      if (str9 != string.Empty)
      {
        ScrollBars? nullable5 = new ScrollBars?();
        string str10 = str9;
        char[] chArray = new char[1]{ ' ' };
        foreach (string str11 in str10.Split(chArray))
        {
          ScrollBars? nullable6 = nullable5;
          ScrollBars scrollBars = (ScrollBars) Enum.Parse(typeof (ScrollBars), str11.Trim(), true);
          nullable5 = nullable6.HasValue ? new ScrollBars?(nullable6.GetValueOrDefault() | scrollBars) : new ScrollBars?();
        }
        if (nullable5.HasValue)
          this._scrolling_ScrollBars = nullable5;
      }
      this._scrolling_XOffset = this.getInt(xmlElement17.GetAttribute("XOffset"));
    }
    if (xmlNode.SelectSingleNode("Selection") is XmlElement xmlElement18)
    {
      this._selection_BackColor = this.getColor(xmlElement18.GetAttribute("BackColor"));
      this._selection_BackColorUnfocused = this.getColor(xmlElement18.GetAttribute("BackColorUnfocused"));
      this._selection_ForeColor = this.getColor(xmlElement18.GetAttribute("ForeColor"));
      this._selection_ForeColorUnfocused = this.getColor(xmlElement18.GetAttribute("ForeColorUnfocused"));
      this._selection_Hidden = this.getBool(xmlElement18.GetAttribute("Hidden"));
      this._selection_HideSelection = this.getBool(xmlElement18.GetAttribute("HideSelection"));
      try
      {
        this._selection_Mode = new ScintillaNet.SelectionMode?((ScintillaNet.SelectionMode) Enum.Parse(typeof (ScintillaNet.SelectionMode), xmlElement18.GetAttribute("Mode"), true));
      }
      catch (ArgumentException ex)
      {
      }
    }
    this._snippetsConfigList = new SnippetsConfigList();
    if (xmlNode.SelectSingleNode("Snippets") is XmlElement xmlElement19)
    {
      this._snippetsConfigList.ActiveSnippetColor = this.getColor(xmlElement19.GetAttribute("ActiveSnippetColor"));
      this._snippetsConfigList.ActiveSnippetIndicator = this.getInt(xmlElement19.GetAttribute("ActiveSnippetIndicator"));
      this._snippetsConfigList.InactiveSnippetColor = this.getColor(xmlElement19.GetAttribute("InactiveSnippetColor"));
      this._snippetsConfigList.InactiveSnippetIndicator = this.getInt(xmlElement19.GetAttribute("InactiveSnippetIndicator"));
      try
      {
        this._snippetsConfigList.ActiveSnippetIndicatorStyle = new IndicatorStyle?((IndicatorStyle) Enum.Parse(typeof (IndicatorStyle), xmlElement19.GetAttribute("ActiveSnippetIndicatorStyle"), true));
      }
      catch (ArgumentException ex)
      {
      }
      try
      {
        this._snippetsConfigList.InactiveSnippetIndicatorStyle = new IndicatorStyle?((IndicatorStyle) Enum.Parse(typeof (IndicatorStyle), xmlElement19.GetAttribute("InactiveSnippetIndicatorStyle"), true));
      }
      catch (ArgumentException ex)
      {
      }
      this._snippetsConfigList.DefaultDelimeter = this.getChar(xmlElement19.GetAttribute("DefaultDelimeter"));
      this._snippetsConfigList.IsEnabled = this.getBool(xmlElement19.GetAttribute("IsEnabled"));
      this._snippetsConfigList.IsOneKeySelectionEmbedEnabled = this.getBool(xmlElement19.GetAttribute("IsOneKeySelectionEmbedEnabled"));
      foreach (XmlElement selectNode in xmlElement19.SelectNodes("Snippet"))
        this._snippetsConfigList.Add(new SnippetsConfig()
        {
          Shortcut = selectNode.GetAttribute("Shortcut"),
          Code = selectNode.InnerText,
          Delimeter = this.getChar(selectNode.GetAttribute("Delimeter")),
          IsSurroundsWith = this.getBool(selectNode.GetAttribute("IsSurroundsWith"))
        });
    }
    this._styles = new StyleConfigList();
    if (xmlNode.SelectSingleNode("Styles") is XmlElement xmlElement20)
    {
      this._styles.Bits = this.getInt(xmlElement20.GetAttribute("Bits"));
      foreach (XmlElement selectNode in xmlElement20.SelectNodes("Style"))
      {
        StyleConfig styleConfig = new StyleConfig();
        styleConfig.Name = selectNode.GetAttribute("Name");
        styleConfig.Number = this.getInt(selectNode.GetAttribute("Number"));
        styleConfig.BackColor = this.getColor(selectNode.GetAttribute("BackColor"));
        styleConfig.Bold = this.getBool(selectNode.GetAttribute("Bold"));
        try
        {
          styleConfig.Case = new StyleCase?((StyleCase) Enum.Parse(typeof (StyleCase), selectNode.GetAttribute("Case"), true));
        }
        catch (ArgumentException ex)
        {
        }
        try
        {
          styleConfig.CharacterSet = new CharacterSet?((CharacterSet) Enum.Parse(typeof (CharacterSet), selectNode.GetAttribute("CharacterSet"), true));
        }
        catch (ArgumentException ex)
        {
        }
        styleConfig.FontName = this.getString(selectNode.GetAttributeNode("FontName"));
        styleConfig.ForeColor = this.getColor(selectNode.GetAttribute("ForeColor"));
        styleConfig.IsChangeable = this.getBool(selectNode.GetAttribute("IsChangeable"));
        styleConfig.IsHotspot = this.getBool(selectNode.GetAttribute("IsHotspot"));
        styleConfig.IsSelectionEolFilled = this.getBool(selectNode.GetAttribute("IsSelectionEolFilled"));
        styleConfig.IsVisible = this.getBool(selectNode.GetAttribute("IsVisible"));
        styleConfig.Italic = this.getBool(selectNode.GetAttribute("Italic"));
        styleConfig.Size = this.getInt(selectNode.GetAttribute("Size"));
        styleConfig.Underline = this.getBool(selectNode.GetAttribute("Underline"));
        styleConfig.Inherit = this.getBool(selectNode.GetAttribute("Inherit"));
        this._styles.Add(styleConfig);
      }
      foreach (XmlElement selectNode1 in xmlElement20.SelectNodes("SubLanguage"))
      {
        string attribute = selectNode1.GetAttribute("Name");
        foreach (XmlElement selectNode2 in selectNode1.SelectNodes("Style"))
        {
          StyleConfig styleConfig = new StyleConfig();
          styleConfig.Name = $"{attribute}.{selectNode2.GetAttribute("Name")}";
          styleConfig.Number = this.getInt(selectNode2.GetAttribute("Number"));
          styleConfig.BackColor = this.getColor(selectNode2.GetAttribute("BackColor"));
          styleConfig.Bold = this.getBool(selectNode2.GetAttribute("Bold"));
          try
          {
            styleConfig.Case = new StyleCase?((StyleCase) Enum.Parse(typeof (StyleCase), selectNode2.GetAttribute("Case"), true));
          }
          catch (ArgumentException ex)
          {
          }
          try
          {
            styleConfig.CharacterSet = new CharacterSet?((CharacterSet) Enum.Parse(typeof (CharacterSet), selectNode2.GetAttribute("CharacterSet"), true));
          }
          catch (ArgumentException ex)
          {
          }
          styleConfig.FontName = this.getString(selectNode2.GetAttributeNode("FontName"));
          styleConfig.ForeColor = this.getColor(selectNode2.GetAttribute("ForeColor"));
          styleConfig.IsChangeable = this.getBool(selectNode2.GetAttribute("IsChangeable"));
          styleConfig.IsHotspot = this.getBool(selectNode2.GetAttribute("IsHotspot"));
          styleConfig.IsSelectionEolFilled = this.getBool(selectNode2.GetAttribute("IsSelectionEolFilled"));
          styleConfig.IsVisible = this.getBool(selectNode2.GetAttribute("IsVisible"));
          styleConfig.Italic = this.getBool(selectNode2.GetAttribute("Italic"));
          styleConfig.Size = this.getInt(selectNode2.GetAttribute("Size"));
          styleConfig.Underline = this.getBool(selectNode2.GetAttribute("Underline"));
          styleConfig.Inherit = this.getBool(selectNode2.GetAttribute("Inherit"));
          this._styles.Add(styleConfig);
        }
      }
    }
    if (xmlNode.SelectSingleNode("UndoRedo") is XmlElement xmlElement21)
      this._undoRedoIsUndoEnabled = this.getBool(xmlElement21.GetAttribute("IsUndoEnabled"));
    if (xmlNode.SelectSingleNode("Whitespace") is XmlElement xmlElement22)
    {
      this._whitespace_BackColor = this.getColor(xmlElement22.GetAttribute("BackColor"));
      this._whitespace_ForeColor = this.getColor(xmlElement22.GetAttribute("ForeColor"));
      this._whitespace_Mode = new WhitespaceMode?((WhitespaceMode) Enum.Parse(typeof (WhitespaceMode), xmlElement22.GetAttribute("Mode"), true));
    }
    configDocument = (XmlDocument) null;
  }

  private string getString(XmlAttribute a) => a?.Value;

  private bool? getBool(string s)
  {
    s = s.ToLower();
    switch (s)
    {
      case "true":
      case "t":
      case "1":
      case "y":
      case "yes":
        return new bool?(true);
      case "false":
      case "f":
      case "0":
      case "n":
      case "no":
        return new bool?(false);
      default:
        return new bool?();
    }
  }

  private int? getInt(string s)
  {
    int result;
    return int.TryParse(s, out result) ? new int?(result) : new int?();
  }

  private Color getColor(string s) => (Color) new ColorConverter().ConvertFromString(s);

  private char? getChar(string s) => string.IsNullOrEmpty(s) ? new char?() : new char?(s[0]);

  public string AutoComplete_List
  {
    get => this._autoComplete_List;
    set => this._autoComplete_List = value;
  }

  public bool? AutoComplete_ListInherits
  {
    get => this._autoComplete_ListInherit;
    set => this._autoComplete_ListInherit = value;
  }

  public string AutoComplete_StopCharacters
  {
    get => this._autoComplete_StopCharacters;
    set => this._autoComplete_StopCharacters = value;
  }

  public char? AutoComplete_ListSeperator
  {
    get => this._autoComplete_ListSeperator;
    set => this._autoComplete_ListSeperator = value;
  }

  public bool? AutoComplete_CancelAtStart
  {
    get => this._autoComplete_cancelAtStart;
    set => this._autoComplete_cancelAtStart = value;
  }

  public string AutoComplete_FillUpCharacters
  {
    get => this._autoComplete_fillUpCharacters;
    set => this._autoComplete_fillUpCharacters = value;
  }

  public bool? AutoComplete_SingleLineAccept
  {
    get => this._autoComplete_singleLineAccept;
    set => this._autoComplete_singleLineAccept = value;
  }

  public bool? AutoComplete_IsCaseSensitive
  {
    get => this._autoComplete_IsCaseSensitive;
    set => this._autoComplete_IsCaseSensitive = value;
  }

  public bool? AutoComplete_AutoHide
  {
    get => this._autoComplete_AutoHide;
    set => this._autoComplete_AutoHide = value;
  }

  public bool? AutoComplete_DropRestOfWord
  {
    get => this._autoComplete_DropRestOfWord;
    set => this._autoComplete_DropRestOfWord = value;
  }

  public char? AutoComplete_ImageSeperator
  {
    get => this._autoComplete_ImageSeperator;
    set => this._autoComplete_ImageSeperator = value;
  }

  public int? AutoComplete_MaxHeight
  {
    get => this._autoComplete_MaxHeight;
    set => this._autoComplete_MaxHeight = value;
  }

  public int? AutoComplete_MaxWidth
  {
    get => this._autoComplete_MaxWidth;
    set => this._autoComplete_MaxWidth = value;
  }

  public bool? AutoComplete_AutomaticLengthEntered
  {
    get => this._autoComplete_AutomaticLengthEntered;
    set => this._autoComplete_AutomaticLengthEntered = value;
  }

  public Color CallTip_ForeColor
  {
    get => this._callTip_ForeColor;
    set => this._callTip_ForeColor = value;
  }

  public Color CallTip_BackColor
  {
    get => this._callTip_BackColor;
    set => this._callTip_BackColor = value;
  }

  public Color CallTip_HighlightTextColor
  {
    get => this._callTip_HighlightTextColor;
    set => this._callTip_HighlightTextColor = value;
  }

  public int? Caret_Width
  {
    get => this._caret_Width;
    set => this._caret_Width = value;
  }

  public CaretStyle? Caret_Style
  {
    get => this._caret_Style;
    set => this._caret_Style = value;
  }

  public Color Caret_Color
  {
    get => this._caret_Color;
    set => this._caret_Color = value;
  }

  public Color Caret_CurrentLineBackgroundColor
  {
    get => this._caret_CurrentLineBackgroundColor;
    set => this._caret_CurrentLineBackgroundColor = value;
  }

  public bool? Caret_HighlightCurrentLine
  {
    get => this._caret_HighlightCurrentLine;
    set => this._caret_HighlightCurrentLine = value;
  }

  public int? Caret_CurrentLineBackgroundAlpha
  {
    get => this._caret_CurrentLineBackgroundAlpha;
    set => this._caret_CurrentLineBackgroundAlpha = value;
  }

  public int? Caret_BlinkRate
  {
    get => this._caret_BlinkRate;
    set => this._caret_BlinkRate = value;
  }

  public bool? Caret_IsSticky
  {
    get => this._caret_IsSticky;
    set => this._caret_IsSticky = value;
  }

  public bool? Clipboard_ConvertEndOfLineOnPaste
  {
    get => this._clipboard_ConvertEndOfLineOnPaste;
    set => this._clipboard_ConvertEndOfLineOnPaste = value;
  }

  public CommandBindingConfigList Commands_KeyBindingList
  {
    get => this._commands_KeyBindingList;
    set => this._commands_KeyBindingList = value;
  }

  public string DropMarkers_SharedStackName
  {
    get => this._dropMarkers_SharedStackName;
    set => this._dropMarkers_SharedStackName = value;
  }

  public bool? EndOfLine_ConvertOnPaste
  {
    get => this._endOfLine_ConvertOnPaste;
    set => this._endOfLine_ConvertOnPaste = value;
  }

  public EndOfLineMode? EndOfLine_Mode
  {
    get => this._endOfLine_Mode;
    set => this._endOfLine_Mode = value;
  }

  public bool? EndOfLine_IsVisisble
  {
    get => this._endOfLine_IsVisisble;
    set => this._endOfLine_IsVisisble = value;
  }

  public bool? Folding_IsEnabled
  {
    get => this._folding_IsEnabled;
    set => this._folding_IsEnabled = value;
  }

  public bool? Folding_UseCompactFolding
  {
    get => this._folding_UseCompactFolding;
    set => this._folding_UseCompactFolding = value;
  }

  public FoldMarkerScheme? Folding_MarkerScheme
  {
    get => this._folding_MarkerScheme;
    set => this._folding_MarkerScheme = value;
  }

  public FoldFlag? Folding_Flags
  {
    get => this._folding_Flags;
    set => this._folding_Flags = value;
  }

  public bool HasData
  {
    get => this._hasData;
    set => this._hasData = value;
  }

  public Color Hotspot_ActiveForeColor
  {
    get => this._hotspot_ActiveForeColor;
    set => this._hotspot_ActiveForeColor = value;
  }

  public Color Hotspot_ActiveBackColor
  {
    get => this._hotspot_ActiveBackColor;
    set => this._hotspot_ActiveBackColor = value;
  }

  public bool? Hotspot_ActiveUnderline
  {
    get => this._hotspot_ActiveUnderline;
    set => this._hotspot_ActiveUnderline = value;
  }

  public bool? Hotspot_SingleLine
  {
    get => this._hotspot_SingleLine;
    set => this._hotspot_SingleLine = value;
  }

  public bool? Hotspot_UseActiveForeColor
  {
    get => this._hotspot_UseActiveForeColor;
    set => this._hotspot_UseActiveForeColor = value;
  }

  public bool? Hotspot_UseActiveBackColor
  {
    get => this._hotspot_UseActiveBackColor;
    set => this._hotspot_UseActiveBackColor = value;
  }

  public int? Indentation_TabWidth
  {
    get => this._indentation_TabWidth;
    set => this._indentation_TabWidth = value;
  }

  public bool? Indentation_UseTabs
  {
    get => this._indentation_UseTabs;
    set => this._indentation_UseTabs = value;
  }

  public int? Indentation_IndentWidth
  {
    get => this._indentation_IndentWidth;
    set => this._indentation_IndentWidth = value;
  }

  public SmartIndent? Indentation_SmartIndentType
  {
    get => this._indentation_SmartIndentType;
    set => this._indentation_SmartIndentType = value;
  }

  public bool? Indentation_TabIndents
  {
    get => this._indentation_TabIndents;
    set => this._indentation_TabIndents = value;
  }

  public bool? Indentation_BackspaceUnindents
  {
    get => this._indentation_BackspaceUnindents;
    set => this._indentation_BackspaceUnindents = value;
  }

  public bool? Indentation_ShowGuides
  {
    get => this._indentation_ShowGuides;
    set => this._indentation_ShowGuides = value;
  }

  public IndicatorConfigList Indicator_List
  {
    get => this._indicator_List;
    set => this._indicator_List = value;
  }

  public string Lexing_WhitespaceChars
  {
    get => this._lexing_WhitespaceChars;
    set => this._lexing_WhitespaceChars = value;
  }

  public string Lexing_WordChars
  {
    get => this._lexing_WordChars;
    set => this._lexing_WordChars = value;
  }

  public string Lexing_Language
  {
    get => this._lexing_Language;
    set => this._lexing_Language = value;
  }

  public string Lexing_LineCommentPrefix
  {
    get => this._lexing_LineCommentPrefix;
    set => this._lexing_LineCommentPrefix = value;
  }

  public string Lexing_StreamCommentSuffix
  {
    get => this._lexing_StreamCommentSuffix;
    set => this._lexing_StreamCommentSuffix = value;
  }

  public string Lexing_StreamCommentPrefix
  {
    get => this._lexing_StreamCommentPrefix;
    set => this._lexing_StreamCommentPrefix = value;
  }

  public LexerPropertiesConfig Lexing_Properties
  {
    get => this._lexing_Properties;
    set => this._lexing_Properties = value;
  }

  public KeyWordConfigList Lexing_Keywords
  {
    get => this._lexing_Keywords;
    set => this._lexing_Keywords = value;
  }

  public WrapMode? LineWrap_Mode
  {
    get => this._lineWrap_Mode;
    set => this._lineWrap_Mode = value;
  }

  public WrapVisualFlag? LineWrap_VisualFlags
  {
    get => this._lineWrap_VisualFlags;
    set => this._lineWrap_VisualFlags = value;
  }

  public WrapVisualLocation? LineWrap_VisualFlagsLocation
  {
    get => this._lineWrap_VisualFlagsLocation;
    set => this._lineWrap_VisualFlagsLocation = value;
  }

  public int? LineWrap_StartIndent
  {
    get => this._lineWrap_StartIndent;
    set => this._lineWrap_StartIndent = value;
  }

  public LineCache? LineWrap_LayoutCache
  {
    get => this._lineWrap_LayoutCache;
    set => this._lineWrap_LayoutCache = value;
  }

  public int? LineWrap_PositionCacheSize
  {
    get => this._lineWrap_PositionCacheSize;
    set => this._lineWrap_PositionCacheSize = value;
  }

  public EdgeMode? LongLines_EdgeMode
  {
    get => this._longLines_EdgeMode;
    set => this._longLines_EdgeMode = value;
  }

  public int? LongLines_EdgeColumn
  {
    get => this._longLines_EdgeColumn;
    set => this._longLines_EdgeColumn = value;
  }

  public Color LongLines_EdgeColor
  {
    get => this._longLines_EdgeColor;
    set => this._longLines_EdgeColor = value;
  }

  public MarginConfigList Margin_List
  {
    get => this._margin_List;
    set => this._margin_List = value;
  }

  public MarkersConfigList Markers_List
  {
    get => this._markers_List;
    set => this._markers_List = value;
  }

  public ScrollBars? Scrolling_ScrollBars
  {
    get => this._scrolling_ScrollBars;
    set => this._scrolling_ScrollBars = value;
  }

  public int? Scrolling_XOffset
  {
    get => this._scrolling_XOffset;
    set => this._scrolling_XOffset = value;
  }

  public int? Scrolling_HorizontalWidth
  {
    get => this._scrolling_HorizontalWidth;
    set => this._scrolling_HorizontalWidth = value;
  }

  public bool? Scrolling_EndAtLastLine
  {
    get => this._scrolling_EndAtLastLine;
    set => this._scrolling_EndAtLastLine = value;
  }

  public Color Selection_ForeColor
  {
    get => this._selection_ForeColor;
    set => this._selection_ForeColor = value;
  }

  public Color Selection_ForeColorUnfocused
  {
    get => this._selection_ForeColorUnfocused;
    set => this._selection_ForeColorUnfocused = value;
  }

  public Color Selection_BackColorUnfocused
  {
    get => this._selection_BackColorUnfocused;
    set => this._selection_BackColorUnfocused = value;
  }

  public Color Selection_BackColor
  {
    get => this._selection_BackColor;
    set => this._selection_BackColor = value;
  }

  public bool? Selection_Hidden
  {
    get => this._selection_Hidden;
    set => this._selection_Hidden = value;
  }

  public bool? Selection_HideSelection
  {
    get => this._selection_HideSelection;
    set => this._selection_HideSelection = value;
  }

  public ScintillaNet.SelectionMode? Selection_Mode
  {
    get => this._selection_Mode;
    set => this._selection_Mode = value;
  }

  public SnippetsConfigList SnippetsConfigList
  {
    get => this._snippetsConfigList;
    set => this._snippetsConfigList = value;
  }

  public StyleConfigList Styles
  {
    get => this._styles;
    set => this._styles = value;
  }

  public bool? UndoRedoIsUndoEnabled
  {
    get => this._undoRedoIsUndoEnabled;
    set => this._undoRedoIsUndoEnabled = value;
  }

  public Color Whitespace_BackColor
  {
    get => this._whitespace_BackColor;
    set => this._whitespace_BackColor = value;
  }

  public Color Whitespace_ForeColor
  {
    get => this._whitespace_ForeColor;
    set => this._whitespace_ForeColor = value;
  }

  public WhitespaceMode? Whitespace_Mode
  {
    get => this._whitespace_Mode;
    set => this._whitespace_Mode = value;
  }
}
