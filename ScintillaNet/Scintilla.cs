using ScintillaNet.Configuration;
using ScintillaNet.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[Designer(typeof (ScintillaDesigner))]
[Docking(DockingBehavior.Ask)]
[DefaultBindingProperty("Text")]
[DefaultProperty("Text")]
[DefaultEvent("DocumentChanged")]
public class Scintilla : Control, INativeScintilla, ISupportInitialize
{
  private const uint TEXT_MODIFIED_FLAGS = 3075;
  public const string DefaultDllName = "SciLexer_x64.dll";
  private static readonly object _styleNeededEventKeyNative = new object();
  private static readonly object _charAddedEventKeyNative = new object();
  private static readonly object _savePointReachedEventKeyNative = new object();
  private static readonly object _savePointLeftEventKeyNative = new object();
  private static readonly object _modifyAttemptROEventKey = new object();
  private static readonly object _keyEventKey = new object();
  private static readonly object _doubleClickEventKey = new object();
  private static readonly object _updateUIEventKey = new object();
  private static readonly object _macroRecordEventKeyNative = new object();
  private static readonly object _marginClickEventKeyNative = new object();
  private static readonly object _modifiedEventKey = new object();
  private static readonly object _changeEventKey = new object();
  private static readonly object _needShownEventKey = new object();
  private static readonly object _paintedEventKey = new object();
  private static readonly object _userListSelectionEventKeyNative = new object();
  private static readonly object _uriDroppedEventKeyNative = new object();
  private static readonly object _dwellStartEventKeyNative = new object();
  private static readonly object _dwellEndEventKeyNative = new object();
  private static readonly object _zoomEventKey = new object();
  private static readonly object _hotSpotClickEventKey = new object();
  private static readonly object _hotSpotDoubleClickEventKey = new object();
  private static readonly object _callTipClickEventKeyNative = new object();
  private static readonly object _autoCSelectionEventKey = new object();
  private static readonly object _indicatorClickKeyNative = new object();
  private static readonly object _indicatorReleaseKeyNative = new object();
  private Scintilla.LastSelection lastSelection = new Scintilla.LastSelection();
  private System.Timers.Timer _textChangedTimer;
  private static readonly int _modifiedState = BitVector32.CreateMask();
  private static readonly int _acceptsReturnState = BitVector32.CreateMask(Scintilla._modifiedState);
  private static readonly int _acceptsTabState = BitVector32.CreateMask(Scintilla._acceptsReturnState);
  private BitVector32 _state;
  private Whitespace _whitespace;
  private Dictionary<string, Color> _colorBag = new Dictionary<string, Color>();
  private Hashtable _propertyBag = new Hashtable();
  private static bool _sciLexerLoaded = false;
  private bool _allowDrop;
  private AutoComplete _autoComplete;
  private CallTip _callTip;
  private string _caption;
  private CaretInfo _caret;
  private Clipboard _clipboard;
  private Commands _commands;
  private ConfigurationManager _configurationManager;
  private DocumentHandler _documentHandler;
  private DocumentNavigation _documentNavigation;
  private DropMarkers _dropMarkers;
  private EndOfLine _endOfLine;
  internal static readonly IList<Encoding> ValidCodePages = (IList<Encoding>) new Encoding[8]
  {
    Encoding.ASCII,
    Encoding.UTF8,
    Encoding.Unicode,
    Encoding.GetEncoding(932),
    Encoding.GetEncoding(936),
    Encoding.GetEncoding(949),
    Encoding.GetEncoding(950),
    Encoding.GetEncoding(1361)
  };
  private Encoding _encoding;
  private FindReplace _findReplace;
  private Folding _folding;
  private GoTo _goto;
  private HotspotStyle _hotspotStyle;
  private IndicatorCollection _indicators;
  private Indentation _indentation;
  private bool _isBraceMatching;
  private bool _isCustomPaintingEnabled = true;
  private Lexing _lexing;
  private LinesCollection _lines;
  private LineWrap _lineWrap;
  private LongLines _longLines;
  private List<ManagedRange> _managedRanges = new List<ManagedRange>();
  private MarginCollection _margins;
  private MarkerCollection _markers;
  private bool _matchBraces = true;
  private INativeScintilla _ns;
  private Printing _printing;
  private Scrolling _scrolling;
  private Selection _selection;
  private SearchFlags _searchFlags;
  private SnippetManager _snippets;
  private StyleCollection _styles;
  private bool _supressControlCharacters = true;
  private UndoRedo _undoRedo;
  private bool _useForeColor;
  private bool _useFont;
  private bool _useBackColor;
  private static readonly object _loadEventKey = new object();
  private static readonly object _textInsertedEventKey = new object();
  private static readonly object _textDeletedEventKey = new object();
  private static readonly object _beforeTextInsertEventKey = new object();
  private static readonly object _beforeTextDeleteEventKey = new object();
  private static readonly object _documentChangeEventKey = new object();
  private static readonly object _foldChangedEventKey = new object();
  private static readonly object _markerChangedEventKey = new object();
  private static readonly object _styleNeededEventKey = new object();
  private static readonly object _charAddedEventKey = new object();
  private static readonly object _modifiedChangedEventKey = new object();
  private static readonly object _readOnlyModifyAttemptEventKey = new object();
  private static readonly object _selectionChangedEventKey = new object();
  private static readonly object _linesNeedShownEventKey = new object();
  private static readonly object _uriDroppedEventKey = new object();
  private static readonly object _dwellStartEventKey = new object();
  private static readonly object _dwellEndEventKey = new object();
  private static readonly object _zoomChangedEventKey = new object();
  private static readonly object _hotspotClickedEventKey = new object();
  private static readonly object _hotspotDoubleClickedEventKey = new object();
  private static readonly object _dropMarkerCollectEventKey = new object();
  private static readonly object _callTipClickEventKey = new object();
  private static readonly object _autoCompleteAcceptedEventKey = new object();
  private static readonly object _marginClickEventKey = new object();
  private static readonly object _indicatorClickEventKey = new object();
  private static readonly object _scrollEventKey = new object();
  private static readonly object _macroRecordEventKey = new object();
  private static readonly object _userListEventKey = new object();
  private static readonly object _fileDropEventKey = new object();
  private static readonly object _textChangedKey = new object();
  private List<TopLevelHelper> _helpers = new List<TopLevelHelper>();
  private bool _isInitializing;

  IntPtr INativeScintilla.SendMessageDirect(uint msg, IntPtr wParam, IntPtr lParam)
  {
    if (this.IsDisposed)
      return IntPtr.Zero;
    Message m = new Message();
    m.Msg = (int) msg;
    m.WParam = wParam;
    m.LParam = lParam;
    m.HWnd = this.Handle;
    this.DefWndProc(ref m);
    return m.Result;
  }

  int INativeScintilla.SendMessageDirect(uint msg)
  {
    return (int) this._ns.SendMessageDirect(msg, IntPtr.Zero, IntPtr.Zero);
  }

  int INativeScintilla.SendMessageDirect(uint msg, int wParam, int lParam)
  {
    return (int) this._ns.SendMessageDirect(msg, (IntPtr) wParam, (IntPtr) lParam);
  }

  int INativeScintilla.SendMessageDirect(uint msg, int wParam, uint lParam)
  {
    int lParam1 = (int) lParam;
    return (int) this._ns.SendMessageDirect(msg, (IntPtr) wParam, (IntPtr) lParam1);
  }

  int INativeScintilla.SendMessageDirect(uint msg, int wParam)
  {
    return (int) this._ns.SendMessageDirect(msg, (IntPtr) wParam, IntPtr.Zero);
  }

  int INativeScintilla.SendMessageDirect(uint msg, VOID wParam, int lParam)
  {
    return (int) this._ns.SendMessageDirect(msg, IntPtr.Zero, (IntPtr) lParam);
  }

  int INativeScintilla.SendMessageDirect(uint msg, bool wParam, int lParam)
  {
    return (int) this._ns.SendMessageDirect(msg, (IntPtr) (wParam ? 1 : 0), (IntPtr) lParam);
  }

  int INativeScintilla.SendMessageDirect(uint msg, bool wParam)
  {
    return (int) this._ns.SendMessageDirect(msg, (IntPtr) (wParam ? 1 : 0), IntPtr.Zero);
  }

  int INativeScintilla.SendMessageDirect(uint msg, int wParam, bool lParam)
  {
    return (int) this._ns.SendMessageDirect(msg, (IntPtr) wParam, (IntPtr) (lParam ? 1 : 0));
  }

  int INativeScintilla.SendMessageDirect(uint msg, out string text)
  {
    int length = this._ns.SendMessageDirect(msg, 0, 0);
    return this._ns.SendMessageDirect(msg, IntPtr.Zero, out text, length);
  }

  int INativeScintilla.SendMessageDirect(uint msg, int wParam, out string text)
  {
    int length = this._ns.SendMessageDirect(msg, 0, 0);
    return this._ns.SendMessageDirect(msg, (IntPtr) wParam, out text, length);
  }

  unsafe int INativeScintilla.SendMessageDirect(
    uint msg,
    IntPtr wParam,
    out string text,
    int length)
  {
    byte[] bytes = new byte[length + 1];
    IntPtr num;
    fixed (byte* lParam = bytes)
    {
      num = this._ns.SendMessageDirect(msg, wParam, (IntPtr) (void*) lParam);
      if (lParam[length - 1] == (byte) 0)
        --length;
    }
    text = this._encoding.GetString(bytes, 0, length);
    return (int) num;
  }

  unsafe int INativeScintilla.SendMessageDirect(uint msg, int wParam, string lParam)
  {
    fixed (byte* lParam1 = this._encoding.GetBytes(Scintilla.ZeroTerminated(lParam)))
      return (int) this._ns.SendMessageDirect(msg, (IntPtr) wParam, (IntPtr) (void*) lParam1);
  }

  unsafe int INativeScintilla.SendMessageDirect(uint msg, VOID NULL, string lParam)
  {
    fixed (byte* lParam1 = this._encoding.GetBytes(Scintilla.ZeroTerminated(lParam)))
      return (int) this._ns.SendMessageDirect(msg, IntPtr.Zero, (IntPtr) (void*) lParam1);
  }

  unsafe int INativeScintilla.SendMessageDirect(uint msg, string wParam, string lParam)
  {
    fixed (byte* wParam1 = this._encoding.GetBytes(Scintilla.ZeroTerminated(wParam)))
      fixed (byte* lParam1 = this._encoding.GetBytes(Scintilla.ZeroTerminated(lParam)))
        return (int) this._ns.SendMessageDirect(msg, (IntPtr) (void*) wParam1, (IntPtr) (void*) lParam1);
  }

  unsafe int INativeScintilla.SendMessageDirect(uint msg, string wParam, out string stringResult)
  {
    IntPtr num;
    fixed (byte* wParam1 = this._encoding.GetBytes(Scintilla.ZeroTerminated(wParam)))
    {
      int count = (int) this._ns.SendMessageDirect(msg, (IntPtr) (void*) wParam1, IntPtr.Zero);
      byte[] bytes = new byte[count + 1];
      fixed (byte* lParam = bytes)
        num = this._ns.SendMessageDirect(msg, (IntPtr) (void*) wParam1, (IntPtr) (void*) lParam);
      stringResult = this._encoding.GetString(bytes, 0, count);
    }
    return (int) num;
  }

  unsafe int INativeScintilla.SendMessageDirect(uint msg, string wParam, int lParam)
  {
    fixed (byte* wParam1 = this._encoding.GetBytes(Scintilla.ZeroTerminated(wParam)))
      return (int) this._ns.SendMessageDirect(msg, (IntPtr) (void*) wParam1, (IntPtr) lParam);
  }

  unsafe int INativeScintilla.SendMessageDirect(uint msg, string wParam)
  {
    fixed (byte* wParam1 = this._encoding.GetBytes(Scintilla.ZeroTerminated(wParam)))
      return (int) this._ns.SendMessageDirect(msg, (IntPtr) (void*) wParam1, IntPtr.Zero);
  }

  private static string ZeroTerminated(string param)
  {
    if (string.IsNullOrEmpty(param))
      return "\0";
    return !param.EndsWith("\0") ? param + "\0" : param;
  }

  int INativeScintilla.GetText(int length, out string text)
  {
    return this._ns.SendMessageDirect(2182U, (IntPtr) length, out text, length);
  }

  void INativeScintilla.SetText(string text) => this._ns.SendMessageDirect(2181U, VOID.NULL, text);

  void INativeScintilla.SetSavePoint() => this._ns.SendMessageDirect(2014U, 0, 0);

  int INativeScintilla.GetLine(int line, out string text)
  {
    int length = this._ns.SendMessageDirect(2153U, line, 0);
    if (length != 0)
      return this._ns.SendMessageDirect(2153U, (IntPtr) line, out text, length);
    text = string.Empty;
    return 0;
  }

  void INativeScintilla.ReplaceSel(string text)
  {
    this._ns.SendMessageDirect(2170U, VOID.NULL, text);
  }

  void INativeScintilla.SetReadOnly(bool readOnly)
  {
    this._ns.SendMessageDirect(2171U, readOnly, 0);
  }

  bool INativeScintilla.GetReadOnly() => this._ns.SendMessageDirect(2140U, 0, 0) != 0;

  unsafe int INativeScintilla.GetTextRange(ref TextRange tr)
  {
    fixed (TextRange* lParam = &tr)
      return (int) this._ns.SendMessageDirect(2162U, IntPtr.Zero, (IntPtr) (void*) lParam);
  }

  void INativeScintilla.Allocate(int bytes) => this._ns.SendMessageDirect(2446U, bytes, 0);

  void INativeScintilla.AddText(int length, string s)
  {
    this._ns.SendMessageDirect(2001U, length, s);
  }

  unsafe void INativeScintilla.AddStyledText(int length, byte[] s)
  {
    fixed (byte* lParam = s)
      this._ns.SendMessageDirect(2002U, (IntPtr) length, (IntPtr) (void*) lParam);
  }

  void INativeScintilla.AppendText(int length, string s)
  {
    this._ns.SendMessageDirect(2282U, length, s);
  }

  void INativeScintilla.InsertText(int pos, string text)
  {
    this._ns.SendMessageDirect(2003U, pos, text);
  }

  void INativeScintilla.ClearAll() => this._ns.SendMessageDirect(2004U, 0, 0);

  void INativeScintilla.ClearDocumentStyle() => this._ns.SendMessageDirect(2005U, 0, 0);

  char INativeScintilla.GetCharAt(int position)
  {
    return (char) this._ns.SendMessageDirect(2007U, position, 0);
  }

  byte INativeScintilla.GetStyleAt(int position)
  {
    return (byte) this._ns.SendMessageDirect(2010U, position, 0);
  }

  unsafe void INativeScintilla.GetStyledText(ref TextRange tr)
  {
    fixed (TextRange* lParam = &tr)
      this._ns.SendMessageDirect(2015U, IntPtr.Zero, (IntPtr) (void*) lParam);
  }

  void INativeScintilla.SetStyleBits(int bits) => this._ns.SendMessageDirect(2090U, bits, 0);

  int INativeScintilla.GetStyleBits() => this._ns.SendMessageDirect(2091U, 0, 0);

  int INativeScintilla.TargetAsUtf8(out string s) => throw new NotSupportedException();

  int INativeScintilla.EncodeFromUtf8(string utf8, out string encoded)
  {
    throw new NotSupportedException();
  }

  int INativeScintilla.SetLengthForEncode(int bytes) => throw new NotSupportedException();

  unsafe int INativeScintilla.FindText(int searchFlags, ref TextToFind ttf)
  {
    fixed (TextToFind* lParam = &ttf)
      return (int) this._ns.SendMessageDirect(2150U, (IntPtr) searchFlags, (IntPtr) (void*) lParam);
  }

  void INativeScintilla.SearchAnchor() => this._ns.SendMessageDirect(2366U, 0, 0);

  int INativeScintilla.SearchNext(int searchFlags, string text)
  {
    return this._ns.SendMessageDirect(2367U, searchFlags, text);
  }

  int INativeScintilla.SearchPrev(int searchFlags, string text)
  {
    return this._ns.SendMessageDirect(2368U, searchFlags, text);
  }

  void INativeScintilla.SetTargetStart(int pos) => this._ns.SendMessageDirect(2190U, pos, 0);

  int INativeScintilla.GetTargetStart() => this._ns.SendMessageDirect(2191U, 0, 0);

  void INativeScintilla.SetTargetEnd(int pos) => this._ns.SendMessageDirect(2192U, pos, 0);

  int INativeScintilla.GetTargetEnd() => this._ns.SendMessageDirect(2193U, 0, 0);

  void INativeScintilla.TargetFromSelection() => this._ns.SendMessageDirect(2287U, 0, 0);

  void INativeScintilla.SetSearchFlags(int searchFlags)
  {
    this._ns.SendMessageDirect(2198U, searchFlags, 0);
  }

  int INativeScintilla.GetSearchFlags() => this._ns.SendMessageDirect(2199U, 0, 0);

  int INativeScintilla.SearchInTarget(int length, string text)
  {
    return this._ns.SendMessageDirect(2197U, length, text);
  }

  int INativeScintilla.ReplaceTarget(int length, string text)
  {
    return this._ns.SendMessageDirect(2194U, length, text);
  }

  int INativeScintilla.ReplaceTargetRE(int length, string text)
  {
    return this._ns.SendMessageDirect(2195U, length, text);
  }

  void INativeScintilla.SetOvertype(bool overType)
  {
    this._ns.SendMessageDirect(2186U, overType, 0);
  }

  bool INativeScintilla.GetOvertype() => this._ns.SendMessageDirect(2187U, 0, 0) != 0;

  void INativeScintilla.Cut() => this._ns.SendMessageDirect(2177U, 0, 0);

  void INativeScintilla.Copy() => this._ns.SendMessageDirect(2178U, 0, 0);

  void INativeScintilla.Paste() => this._ns.SendMessageDirect(2179U, 0, 0);

  void INativeScintilla.Clear() => this._ns.SendMessageDirect(2180U, 0, 0);

  bool INativeScintilla.CanPaste() => this._ns.SendMessageDirect(2173U, 0, 0) != 0;

  void INativeScintilla.CopyRange(int start, int end)
  {
    this._ns.SendMessageDirect(2419U, start, end);
  }

  void INativeScintilla.CopyText(int length, string text)
  {
    this._ns.SendMessageDirect(2420U, length, text);
  }

  void INativeScintilla.SetPasteConvertEndings(bool convert)
  {
    this._ns.SendMessageDirect(2467U, convert, 0);
  }

  bool INativeScintilla.GetPasteConvertEndings() => this._ns.SendMessageDirect(2468U, 0, 0) != 0;

  void INativeScintilla.SetStatus(int status) => this._ns.SendMessageDirect(2382U, status, 0);

  int INativeScintilla.GetStatus() => this._ns.SendMessageDirect(2383U, 0, 0);

  void INativeScintilla.Undo() => this._ns.SendMessageDirect(2176U, 0, 0);

  bool INativeScintilla.CanUndo() => this._ns.SendMessageDirect(2174U, 0, 0) != 0;

  void INativeScintilla.EmptyUndoBuffer() => this._ns.SendMessageDirect(2175U, 0, 0);

  void INativeScintilla.Redo() => this._ns.SendMessageDirect(2011U, 0, 0);

  bool INativeScintilla.CanRedo() => this._ns.SendMessageDirect(2016U, 0, 0) != 0;

  void INativeScintilla.SetUndoCollection(bool collectUndo)
  {
    this._ns.SendMessageDirect(2012U, collectUndo, 0);
  }

  bool INativeScintilla.GetUndoCollection() => this._ns.SendMessageDirect(2019U, 0, 0) != 0;

  void INativeScintilla.BeginUndoAction() => this._ns.SendMessageDirect(2078U, 0, 0);

  void INativeScintilla.EndUndoAction() => this._ns.SendMessageDirect(2079U, 0, 0);

  int INativeScintilla.GetTextLength() => this._ns.SendMessageDirect(2183U, 0, 0);

  int INativeScintilla.GetLength() => this._ns.SendMessageDirect(2006U, 0, 0);

  int INativeScintilla.GetLineCount() => this._ns.SendMessageDirect(2154U, 0, 0);

  int INativeScintilla.GetFirstVisibleLine() => this._ns.SendMessageDirect(2152U, 0, 0);

  int INativeScintilla.LinesOnScreen() => this._ns.SendMessageDirect(2370U, 0, 0);

  bool INativeScintilla.GetModify() => this._ns.SendMessageDirect(2159U, 0, 0) != 0;

  void INativeScintilla.SetSel(int anchorPos, int currentPos)
  {
    this._ns.SendMessageDirect(2160U, anchorPos, currentPos);
  }

  void INativeScintilla.GotoPos(int position) => this._ns.SendMessageDirect(2025U, position, 0);

  void INativeScintilla.GotoLine(int line) => this._ns.SendMessageDirect(2024U, line, 0);

  void INativeScintilla.SetCurrentPos(int position)
  {
    this._ns.SendMessageDirect(2141U, position, 0);
  }

  int INativeScintilla.GetCurrentPos() => this._ns.SendMessageDirect(2008U, 0, 0);

  void INativeScintilla.SetAnchor(int position) => this._ns.SendMessageDirect(2026U, position, 0);

  int INativeScintilla.GetAnchor() => this._ns.SendMessageDirect(2009U, 0, 0);

  void INativeScintilla.SetSelectionStart(int position)
  {
    this._ns.SendMessageDirect(2142U, position, 0);
  }

  int INativeScintilla.GetSelectionStart() => this._ns.SendMessageDirect(2143U, 0, 0);

  void INativeScintilla.SetSelectionEnd(int position)
  {
    this._ns.SendMessageDirect(2144U, position, 0);
  }

  int INativeScintilla.GetSelectionEnd() => this._ns.SendMessageDirect(2145U, 0, 0);

  void INativeScintilla.SelectAll() => this._ns.SendMessageDirect(2013U, 0, 0);

  int INativeScintilla.LineFromPosition(int pos) => this._ns.SendMessageDirect(2166U, pos, 0);

  int INativeScintilla.PositionFromLine(int line) => this._ns.SendMessageDirect(2167U, line, 0);

  int INativeScintilla.GetLineEndPosition(int line) => this._ns.SendMessageDirect(2136U, line, 0);

  int INativeScintilla.LineLength(int line) => this._ns.SendMessageDirect(2350U, line, 0);

  int INativeScintilla.GetColumn(int position) => this._ns.SendMessageDirect(2129U, position, 0);

  int INativeScintilla.FindColumn(int line, int column)
  {
    return this._ns.SendMessageDirect(2456U, line, column);
  }

  int INativeScintilla.PositionFromPoint(int x, int y) => this._ns.SendMessageDirect(2022U, x, y);

  int INativeScintilla.PositionFromPointClose(int x, int y)
  {
    return this._ns.SendMessageDirect(2023U, x, y);
  }

  int INativeScintilla.PointXFromPosition(int position)
  {
    return this._ns.SendMessageDirect(2164U, VOID.NULL, position);
  }

  int INativeScintilla.PointYFromPosition(int position)
  {
    return this._ns.SendMessageDirect(2165U, VOID.NULL, position);
  }

  void INativeScintilla.HideSelection(bool hide) => this._ns.SendMessageDirect(2163U, hide, 0);

  void INativeScintilla.GetSelText(out string text)
  {
    int length = this._ns.GetSelectionEnd() - this._ns.GetSelectionStart() + 1;
    this._ns.SendMessageDirect(2161U, IntPtr.Zero, out text, length);
  }

  int INativeScintilla.GetCurLine(int textLen, out string text)
  {
    return this._ns.SendMessageDirect(2027U, (IntPtr) textLen, out text, textLen);
  }

  bool INativeScintilla.SelectionIsRectangle() => this._ns.SendMessageDirect(2372U, 0, 0) != 0;

  void INativeScintilla.SetSelectionMode(int mode) => this._ns.SendMessageDirect(2422U, mode, 0);

  int INativeScintilla.GetSelectionMode() => this._ns.SendMessageDirect(2423U, 0, 0);

  int INativeScintilla.GetLineSelStartPosition(int line)
  {
    return this._ns.SendMessageDirect(2424U, line, 0);
  }

  int INativeScintilla.GetLineSelEndPosition(int line)
  {
    return this._ns.SendMessageDirect(2425U, line, 0);
  }

  void INativeScintilla.MoveCaretInsideView() => this._ns.SendMessageDirect(2401U, 0, 0);

  int INativeScintilla.WordEndPosition(int position, bool onlyWordCharacters)
  {
    return this._ns.SendMessageDirect(2267U, position, onlyWordCharacters);
  }

  int INativeScintilla.WordStartPosition(int position, bool onlyWordCharacters)
  {
    return this._ns.SendMessageDirect(2266U, position, onlyWordCharacters);
  }

  int INativeScintilla.PositionBefore(int position)
  {
    return this._ns.SendMessageDirect(2417U, position, 0);
  }

  int INativeScintilla.PositionAfter(int position)
  {
    return this._ns.SendMessageDirect(2418U, position, 0);
  }

  int INativeScintilla.TextWidth(int styleNumber, string text)
  {
    return this._ns.SendMessageDirect(2276U, styleNumber, text);
  }

  int INativeScintilla.TextHeight(int line) => this._ns.SendMessageDirect(2279U, line, 0);

  void INativeScintilla.ChooseCaretX() => this._ns.SendMessageDirect(2399U, 0, 0);

  void INativeScintilla.LineScroll(int columns, int lines)
  {
    this._ns.SendMessageDirect(2168U, columns, lines);
  }

  void INativeScintilla.ScrollCaret() => this._ns.SendMessageDirect(2169U, 0, 0);

  void INativeScintilla.SetXCaretPolicy(int caretPolicy, int caretSlop)
  {
    this._ns.SendMessageDirect(2402U, caretPolicy, caretSlop);
  }

  void INativeScintilla.SetYCaretPolicy(int caretPolicy, int caretSlop)
  {
    this._ns.SendMessageDirect(2403U, caretPolicy, caretSlop);
  }

  void INativeScintilla.SetVisiblePolicy(int visiblePolicy, int visibleSlop)
  {
    this._ns.SendMessageDirect(2394U, visiblePolicy, visibleSlop);
  }

  void INativeScintilla.SetHScrollBar(bool visible)
  {
    this._ns.SendMessageDirect(2130U, visible, 0);
  }

  bool INativeScintilla.GetHScrollBar() => this._ns.SendMessageDirect(2131U, 0, 0) != 0;

  void INativeScintilla.SetVScrollBar(bool visible)
  {
    this._ns.SendMessageDirect(2280U, visible, 0);
  }

  bool INativeScintilla.GetVScrollBar() => this._ns.SendMessageDirect(2281U, 0, 0) != 0;

  int INativeScintilla.GetXOffset() => this._ns.SendMessageDirect(2398U, 0, 0);

  void INativeScintilla.SetXOffset(int xOffset) => this._ns.SendMessageDirect(2397U, xOffset, 0);

  void INativeScintilla.SetScrollWidth(int pixelWidth)
  {
    this._ns.SendMessageDirect(2274U, pixelWidth, 0);
  }

  int INativeScintilla.GetScrollWidth() => this._ns.SendMessageDirect(2275U, 0, 0);

  void INativeScintilla.SetEndAtLastLine(bool endAtLastLine)
  {
    this._ns.SendMessageDirect(2277U, endAtLastLine, 0);
  }

  bool INativeScintilla.GetEndAtLastLine() => this._ns.SendMessageDirect(2278U, 0, 0) != 0;

  void INativeScintilla.SetViewWs(int wsMode) => this._ns.SendMessageDirect(2021U, wsMode, 0);

  int INativeScintilla.GetViewWs() => this._ns.SendMessageDirect(2020U, 0, 0);

  void INativeScintilla.SetWhitespaceFore(bool useWhitespaceForeColour, int colour)
  {
    this._ns.SendMessageDirect(2084U, useWhitespaceForeColour, colour);
  }

  void INativeScintilla.SetWhitespaceBack(bool useWhitespaceBackColour, int colour)
  {
    this._ns.SendMessageDirect(2085U, useWhitespaceBackColour, colour);
  }

  void INativeScintilla.SetCursor(int curType) => this._ns.SendMessageDirect(2386U, curType, 0);

  int INativeScintilla.GetCursor() => this._ns.SendMessageDirect(2387U, 0, 0);

  void INativeScintilla.SetMouseDownCaptures(bool captures)
  {
    this._ns.SendMessageDirect(2384U, captures, 0);
  }

  bool INativeScintilla.GetMouseDownCaptures() => this._ns.SendMessageDirect(2385U, 0, 0) != 0;

  void INativeScintilla.SetEolMode(int eolMode) => this._ns.SendMessageDirect(2031U, eolMode, 0);

  int INativeScintilla.GetEolMode() => this._ns.SendMessageDirect(2030U, 0, 0);

  void INativeScintilla.ConvertEols(int eolMode) => this._ns.SendMessageDirect(2029U, eolMode, 0);

  void INativeScintilla.SetViewEol(bool visible) => this._ns.SendMessageDirect(2356U, visible, 0);

  bool INativeScintilla.GetViewEol() => this._ns.SendMessageDirect(2355U, 0, 0) != 0;

  int INativeScintilla.GetEndStyled() => this._ns.SendMessageDirect(2028U, 0, 0);

  void INativeScintilla.StartStyling(int position, int mask)
  {
    this._ns.SendMessageDirect(2032U, position, mask);
  }

  void INativeScintilla.SetStyling(int length, int style)
  {
    this._ns.SendMessageDirect(2033U, length, style);
  }

  void INativeScintilla.SetStylingEx(int length, string styles)
  {
    this._ns.SendMessageDirect(2073U, length, styles);
  }

  void INativeScintilla.SetLineState(int line, int value)
  {
    this._ns.SendMessageDirect(2092U, line, value);
  }

  int INativeScintilla.GetLineState(int line) => this._ns.SendMessageDirect(2093U, line, 0);

  int INativeScintilla.GetMaxLineState() => this._ns.SendMessageDirect(2094U, 0, 0);

  void INativeScintilla.StyleResetDefault() => this._ns.SendMessageDirect(2058U, 0, 0);

  void INativeScintilla.StyleClearAll() => this._ns.SendMessageDirect(2050U, 0, 0);

  void INativeScintilla.StyleSetFont(int styleNumber, string fontName)
  {
    this._ns.SendMessageDirect(2056U, styleNumber, fontName);
  }

  void INativeScintilla.StyleGetFont(int styleNumber, out string fontName)
  {
    int length = this._ns.SendMessageDirect(2486U, 0, 0);
    string lParam;
    this._ns.SendMessageDirect(2486U, (IntPtr) styleNumber, out lParam, length);
    fontName = lParam;
  }

  void INativeScintilla.StyleSetSize(int styleNumber, int sizeInPoints)
  {
    this._ns.SendMessageDirect(2055U, styleNumber, sizeInPoints);
  }

  int INativeScintilla.StyleGetSize(int styleNumber) => this._ns.SendMessageDirect(2485U, 0, 0);

  void INativeScintilla.StyleSetBold(int styleNumber, bool bold)
  {
    this._ns.SendMessageDirect(2053U, styleNumber, bold);
  }

  bool INativeScintilla.StyleGetBold(int styleNumber)
  {
    return this._ns.SendMessageDirect(2483U, styleNumber, 0) != 0;
  }

  void INativeScintilla.StyleSetItalic(int styleNumber, bool italic)
  {
    this._ns.SendMessageDirect(2054U, styleNumber, italic);
  }

  bool INativeScintilla.StyleGetItalic(int styleNumber)
  {
    return this._ns.SendMessageDirect(2484U, styleNumber, 0) != 0;
  }

  void INativeScintilla.StyleSetUnderline(int styleNumber, bool underline)
  {
    this._ns.SendMessageDirect(2059U, styleNumber, underline);
  }

  bool INativeScintilla.StyleGetUnderline(int styleNumber)
  {
    return this._ns.SendMessageDirect(2488U, styleNumber, 0) != 0;
  }

  void INativeScintilla.StyleSetFore(int styleNumber, int colour)
  {
    this._ns.SendMessageDirect(2051U, styleNumber, colour);
  }

  int INativeScintilla.StyleGetFore(int styleNumber)
  {
    return this._ns.SendMessageDirect(2481U, styleNumber, 0);
  }

  void INativeScintilla.StyleSetBack(int styleNumber, int colour)
  {
    this._ns.SendMessageDirect(2052U, styleNumber, colour);
  }

  int INativeScintilla.StyleGetBack(int styleNumber)
  {
    return this._ns.SendMessageDirect(2482U, styleNumber, 0);
  }

  void INativeScintilla.StyleSetEOLFilled(int styleNumber, bool eolFilled)
  {
    this._ns.SendMessageDirect(2057U, styleNumber, eolFilled);
  }

  bool INativeScintilla.StyleGetEOLFilled(int styleNumber)
  {
    return this._ns.SendMessageDirect(2487U, styleNumber, 0) != 0;
  }

  void INativeScintilla.StyleSetCharacterSet(int styleNumber, int charSet)
  {
    this._ns.SendMessageDirect(2066U, styleNumber, charSet);
  }

  int INativeScintilla.StyleGetCharacterSet(int styleNumber)
  {
    return this._ns.SendMessageDirect(2490U, styleNumber, 0);
  }

  void INativeScintilla.StyleSetCase(int styleNumber, int caseMode)
  {
    this._ns.SendMessageDirect(2060U, styleNumber, caseMode);
  }

  int INativeScintilla.StyleGetCase(int styleNumber)
  {
    return this._ns.SendMessageDirect(2489U, styleNumber, 0);
  }

  void INativeScintilla.StyleSetVisible(int styleNumber, bool visible)
  {
    this._ns.SendMessageDirect(2074U, styleNumber, visible);
  }

  bool INativeScintilla.StyleGetVisible(int styleNumber)
  {
    return this._ns.SendMessageDirect(2491U, styleNumber, 0) != 0;
  }

  void INativeScintilla.StyleSetChangeable(int styleNumber, bool changeable)
  {
    this._ns.SendMessageDirect(2099U, styleNumber, changeable);
  }

  bool INativeScintilla.StyleGetChangeable(int styleNumber)
  {
    return this._ns.SendMessageDirect(2492U, styleNumber, 0) != 0;
  }

  void INativeScintilla.StyleSetHotSpot(int styleNumber, bool hotspot)
  {
    this._ns.SendMessageDirect(2409U, styleNumber, hotspot);
  }

  bool INativeScintilla.StyleGetHotSpot(int styleNumber)
  {
    return this._ns.SendMessageDirect(2493U, styleNumber, 0) != 0;
  }

  int INativeScintilla.GetHotSpotActiveBack() => this._ns.SendMessageDirect(2495U, 0, 0);

  int INativeScintilla.GetHotSpotActiveFore() => this._ns.SendMessageDirect(2494U, 0, 0);

  bool INativeScintilla.GetHotSpotActiveUnderline() => this._ns.SendMessageDirect(2496U, 0, 0) != 0;

  bool INativeScintilla.GetHotSpotSingleLine() => this._ns.SendMessageDirect(2497U, 0, 0) != 0;

  int INativeScintilla.IndicatorEnd(int indicator, int position)
  {
    return this._ns.SendMessageDirect(2509U, indicator, position);
  }

  int INativeScintilla.IndicatorStart(int indicator, int position)
  {
    return this._ns.SendMessageDirect(2508U, indicator, position);
  }

  int INativeScintilla.IndicatorValueAt(int indicator, int position)
  {
    return this._ns.SendMessageDirect(2507U, indicator, position);
  }

  bool INativeScintilla.IndicGetUnder(int indicatorNumber)
  {
    return this._ns.SendMessageDirect(2511U, indicatorNumber, 0) != 0;
  }

  void INativeScintilla.IndicSetUnder(int indicatorNumber, bool under)
  {
    this._ns.SendMessageDirect(2510U, indicatorNumber, under);
  }

  void INativeScintilla.SetIndicatorValue(int value) => this._ns.SendMessageDirect(2502U, value, 0);

  void INativeScintilla.SetPositionCache(int size) => this._ns.SendMessageDirect(2514U, size, 0);

  void INativeScintilla.SetSelFore(bool useSelectionForeColour, int colour)
  {
    this._ns.SendMessageDirect(2067U, useSelectionForeColour, colour);
  }

  void INativeScintilla.SetSelBack(bool useSelectionBackColour, int colour)
  {
    this._ns.SendMessageDirect(2068U, useSelectionBackColour, colour);
  }

  void INativeScintilla.SetCaretFore(int alpha) => this._ns.SendMessageDirect(2069U, alpha, 0);

  int INativeScintilla.GetCaretFore() => this._ns.SendMessageDirect(2138U, 0, 0);

  void INativeScintilla.SetCaretLineVisible(bool colour)
  {
    this._ns.SendMessageDirect(2096U, colour, 0);
  }

  bool INativeScintilla.GetCaretLineVisible() => this._ns.SendMessageDirect(2095U, 0, 0) != 0;

  void INativeScintilla.SetCaretLineBack(int show) => this._ns.SendMessageDirect(2098U, show, 0);

  int INativeScintilla.GetCaretLineBack() => this._ns.SendMessageDirect(2097U, 0, 0);

  void INativeScintilla.SetCaretLineBackAlpha(int alpha)
  {
    this._ns.SendMessageDirect(2470U, alpha, 0);
  }

  int INativeScintilla.GetCaretLineBackAlpha() => this._ns.SendMessageDirect(2471U, 0, 0);

  void INativeScintilla.SetCaretPeriod(int milliseconds)
  {
    this._ns.SendMessageDirect(2076U, milliseconds, 0);
  }

  int INativeScintilla.GetCaretPeriod() => this._ns.SendMessageDirect(2075U, 0, 0);

  void INativeScintilla.SetCaretWidth(int pixels) => this._ns.SendMessageDirect(2188U, pixels, 0);

  int INativeScintilla.GetCaretWidth() => this._ns.SendMessageDirect(2189U, 0, 0);

  void INativeScintilla.SetHotspotActiveFore(bool useHotSpotForeColour, int colour)
  {
    this._ns.SendMessageDirect(2410U, useHotSpotForeColour, colour);
  }

  void INativeScintilla.SetHotspotActiveBack(bool useHotSpotBackColour, int colour)
  {
    this._ns.SendMessageDirect(2411U, useHotSpotBackColour, colour);
  }

  void INativeScintilla.SetHotspotActiveUnderline(bool underline)
  {
    this._ns.SendMessageDirect(2412U, underline, 0);
  }

  void INativeScintilla.SetHotspotSingleLine(bool singleLine)
  {
    this._ns.SendMessageDirect(2421U, singleLine, 0);
  }

  void INativeScintilla.SetControlCharSymbol(int symbol)
  {
    this._ns.SendMessageDirect(2388U, symbol, 0);
  }

  int INativeScintilla.GetControlCharSymbol() => this._ns.SendMessageDirect(2389U, 0, 0);

  bool INativeScintilla.GetCaretSticky() => this._ns.SendMessageDirect(2457U, 0, 0) != 0;

  void INativeScintilla.SetCaretSticky(bool useCaretStickyBehaviour)
  {
    this._ns.SendMessageDirect(2458U, useCaretStickyBehaviour, 0);
  }

  void INativeScintilla.ToggleCaretSticky() => this._ns.SendMessageDirect(2459U, 0, 0);

  int INativeScintilla.GetCaretStyle() => this._ns.SendMessageDirect(2513U, 0, 0);

  void INativeScintilla.SetCaretStyle(int style) => this._ns.SendMessageDirect(2512U, style, 0);

  void INativeScintilla.SetMarginTypeN(int margin, int type)
  {
    this._ns.SendMessageDirect(2240U, margin, type);
  }

  int INativeScintilla.GetMarginTypeN(int margin) => this._ns.SendMessageDirect(2241U, margin, 0);

  void INativeScintilla.SetMarginWidthN(int margin, int pixelWidth)
  {
    this._ns.SendMessageDirect(2242U, margin, pixelWidth);
  }

  int INativeScintilla.GetMarginWidthN(int margin) => this._ns.SendMessageDirect(2243U, margin, 0);

  void INativeScintilla.SetMarginMaskN(int margin, int mask)
  {
    this._ns.SendMessageDirect(2244U, margin, mask);
  }

  int INativeScintilla.GetMarginMaskN(int margin) => this._ns.SendMessageDirect(2245U, margin, 0);

  void INativeScintilla.SetMarginSensitiveN(int margin, bool sensitive)
  {
    this._ns.SendMessageDirect(2246U, margin, sensitive);
  }

  bool INativeScintilla.GetMarginSensitiveN(int margin)
  {
    return this._ns.SendMessageDirect(2247U, margin, 0) != 0;
  }

  void INativeScintilla.SetMarginLeft(int pixels) => this._ns.SendMessageDirect(2155U, 0, pixels);

  int INativeScintilla.GetMarginLeft() => this._ns.SendMessageDirect(2156U, 0, 0);

  void INativeScintilla.SetMarginRight(int pixels) => this._ns.SendMessageDirect(2157U, 0, pixels);

  int INativeScintilla.GetMarginRight() => this._ns.SendMessageDirect(2158U, 0, 0);

  void INativeScintilla.SetFoldMarginColour(bool useSetting, int colour)
  {
    this._ns.SendMessageDirect(2290U, useSetting, colour);
  }

  void INativeScintilla.SetFoldMarginHiColour(bool useSetting, int colour)
  {
    this._ns.SendMessageDirect(2291U, useSetting, colour);
  }

  void INativeScintilla.SetUsePalette(bool allowPaletteUse)
  {
    this._ns.SendMessageDirect(2039U, allowPaletteUse, 0);
  }

  bool INativeScintilla.GetUsePalette() => this._ns.SendMessageDirect(2139U, 0, 0) != 0;

  void INativeScintilla.SetBufferedDraw(bool isBuffered)
  {
    this._ns.SendMessageDirect(2035U, isBuffered, 0);
  }

  bool INativeScintilla.GetBufferedDraw() => this._ns.SendMessageDirect(2034U, 0, 0) != 0;

  void INativeScintilla.SetTwoPhaseDraw(bool twoPhase)
  {
    this._ns.SendMessageDirect(2284U, twoPhase, 0);
  }

  bool INativeScintilla.GetTwoPhaseDraw() => this._ns.SendMessageDirect(2283U, 0, 0) != 0;

  void INativeScintilla.SetCodePage(int codePage)
  {
    this._ns.SendMessageDirect(2037U, codePage, 0);
    this._encoding = Encoding.GetEncoding(codePage);
  }

  int INativeScintilla.GetCodePage() => this._ns.SendMessageDirect(2137U, 0, 0);

  void INativeScintilla.SetWordChars(string chars)
  {
    this._ns.SendMessageDirect(2077U, VOID.NULL, chars);
  }

  void INativeScintilla.SetWhitespaceChars(string chars)
  {
    this._ns.SendMessageDirect(2443U, VOID.NULL, chars);
  }

  void INativeScintilla.SetCharsDefault() => this._ns.SendMessageDirect(2444U, 0, 0);

  void INativeScintilla.GrabFocus() => this._ns.SendMessageDirect(2400U, 0, 0);

  void INativeScintilla.SetFocus(bool focus) => this._ns.SendMessageDirect(2380U, focus, 0);

  bool INativeScintilla.GetFocus() => this._ns.SendMessageDirect(2381U, 0, 0) != 0;

  void INativeScintilla.BraceHighlight(int pos1, int pos2)
  {
    this._ns.SendMessageDirect(2351U, pos1, pos2);
  }

  void INativeScintilla.BraceBadLight(int pos1) => this._ns.SendMessageDirect(2352U, pos1, 0);

  int INativeScintilla.BraceMatch(int pos, int maxReStyle)
  {
    return this._ns.SendMessageDirect(2353U, pos, maxReStyle);
  }

  void INativeScintilla.SetTabWidth(int widthInChars)
  {
    this._ns.SendMessageDirect(2036U, widthInChars, 0);
  }

  int INativeScintilla.GetTabWidth() => this._ns.SendMessageDirect(2121U, 0, 0);

  void INativeScintilla.SetUseTabs(bool useTabs) => this._ns.SendMessageDirect(2124U, useTabs, 0);

  bool INativeScintilla.GetUseTabs() => this._ns.SendMessageDirect(2125U, 0, 0) != 0;

  void INativeScintilla.SetIndent(int widthInChars)
  {
    this._ns.SendMessageDirect(2122U, widthInChars, 0);
  }

  int INativeScintilla.GetIndent() => this._ns.SendMessageDirect(2123U, 0, 0);

  void INativeScintilla.SetTabIndents(bool tabIndents)
  {
    this._ns.SendMessageDirect(2260U, tabIndents, 0);
  }

  bool INativeScintilla.GetTabIndents() => this._ns.SendMessageDirect(2261U, 0, 0) != 0;

  void INativeScintilla.SetBackSpaceUnIndents(bool bsUnIndents)
  {
    this._ns.SendMessageDirect(2262U, bsUnIndents, 0);
  }

  bool INativeScintilla.GetBackSpaceUnIndents() => this._ns.SendMessageDirect(2263U, 0, 0) != 0;

  void INativeScintilla.SetLineIndentation(int line, int indentation)
  {
    this._ns.SendMessageDirect(2126U, line, indentation);
  }

  int INativeScintilla.GetLineIndentation(int line) => this._ns.SendMessageDirect(2127U, line, 0);

  int INativeScintilla.GetLineIndentPosition(int line)
  {
    return this._ns.SendMessageDirect(2128U, line, 0);
  }

  void INativeScintilla.SetIndentationGuides(bool view)
  {
    this._ns.SendMessageDirect(2132U, view, 0);
  }

  bool INativeScintilla.GetIndentationGuides() => this._ns.SendMessageDirect(2133U, 0, 0) != 0;

  void INativeScintilla.SetHighlightGuide(int column)
  {
    this._ns.SendMessageDirect(2134U, column, 0);
  }

  int INativeScintilla.GetHighlightGuide() => this._ns.SendMessageDirect(2135U, 0, 0);

  void INativeScintilla.MarkerDefine(int markerNumber, int markerSymbol)
  {
    this._ns.SendMessageDirect(2040U, markerNumber, markerSymbol);
  }

  void INativeScintilla.MarkerDefinePixmap(int markerNumber, string xpm)
  {
    this._ns.SendMessageDirect(2049U, markerNumber, xpm);
  }

  void INativeScintilla.MarkerSetFore(int markerNumber, int colour)
  {
    this._ns.SendMessageDirect(2041U, markerNumber, colour);
  }

  void INativeScintilla.MarkerSetBack(int markerNumber, int colour)
  {
    this._ns.SendMessageDirect(2042U, markerNumber, colour);
  }

  void INativeScintilla.MarkerSetAlpha(int markerNumber, int alpha)
  {
    this._ns.SendMessageDirect(2476U, markerNumber, alpha);
  }

  int INativeScintilla.MarkerAdd(int line, int markerNumber)
  {
    return this._ns.SendMessageDirect(2043U, line, markerNumber);
  }

  void INativeScintilla.MarkerAddSet(int line, uint markerMask)
  {
    this._ns.SendMessageDirect(2466U, line, markerMask);
  }

  void INativeScintilla.MarkerDelete(int line, int markerNumber)
  {
    this._ns.SendMessageDirect(2044U, line, markerNumber);
  }

  void INativeScintilla.MarkerDeleteAll(int markerNumber)
  {
    this._ns.SendMessageDirect(2045U, markerNumber, 0);
  }

  int INativeScintilla.MarkerGet(int line) => this._ns.SendMessageDirect(2046U, line, 0);

  int INativeScintilla.MarkerNext(int lineStart, uint markerMask)
  {
    return this._ns.SendMessageDirect(2047U /*0x07FF*/, lineStart, markerMask);
  }

  int INativeScintilla.MarkerPrevious(int lineStart, uint markerMask)
  {
    return this._ns.SendMessageDirect(2048U /*0x0800*/, lineStart, markerMask);
  }

  int INativeScintilla.MarkerLineFromHandle(int handle)
  {
    return this._ns.SendMessageDirect(2017U, handle, 0);
  }

  void INativeScintilla.MarkerDeleteHandle(int handle)
  {
    this._ns.SendMessageDirect(2018U, handle, 0);
  }

  void INativeScintilla.IndicSetStyle(int indicatorNumber, int indicatorStyle)
  {
    this._ns.SendMessageDirect(2080U, indicatorNumber, indicatorStyle);
  }

  int INativeScintilla.IndicGetStyle(int indicatorNumber)
  {
    return this._ns.SendMessageDirect(2081U, indicatorNumber, 0);
  }

  void INativeScintilla.IndicSetFore(int indicatorNumber, int colour)
  {
    this._ns.SendMessageDirect(2082U, indicatorNumber, colour);
  }

  int INativeScintilla.IndicGetFore(int indicatorNumber)
  {
    return this._ns.SendMessageDirect(2083U, indicatorNumber, 0);
  }

  int INativeScintilla.GetIndicatorCurrent() => this._ns.SendMessageDirect(2501U, 0, 0);

  int INativeScintilla.GetIndicatorValue() => this._ns.SendMessageDirect(2503U, 0, 0);

  int INativeScintilla.GetPositionCache() => this._ns.SendMessageDirect(2515U, 0, 0);

  uint INativeScintilla.IndicatorAllOnFor(int position)
  {
    return (uint) this._ns.SendMessageDirect(2506U, position, 0);
  }

  void INativeScintilla.IndicatorClearRange(int position, int fillLength)
  {
    this._ns.SendMessageDirect(2505U, position, fillLength);
  }

  void INativeScintilla.IndicatorFillRange(int position, int fillLength)
  {
    this._ns.SendMessageDirect(2504U, position, fillLength);
  }

  void INativeScintilla.SetIndicatorCurrent(int indicator)
  {
    this._ns.SendMessageDirect(2500U, indicator, 0);
  }

  void INativeScintilla.AutoCShow(int lenEntered, string list)
  {
    this._ns.SendMessageDirect(2100U, lenEntered, list);
  }

  void INativeScintilla.AutoCCancel() => this._ns.SendMessageDirect(2101U, 0, 0);

  bool INativeScintilla.AutoCActive() => this._ns.SendMessageDirect(2102U, 0, 0) != 0;

  int INativeScintilla.AutoCPosStart() => this._ns.SendMessageDirect(2103U, 0, 0);

  void INativeScintilla.AutoCComplete() => this._ns.SendMessageDirect(2104U, 0, 0);

  void INativeScintilla.AutoCStops(string chars)
  {
    this._ns.SendMessageDirect(2105U, VOID.NULL, chars);
  }

  void INativeScintilla.AutoCSetSeparator(char separator)
  {
    this._ns.SendMessageDirect(2106U, (int) separator, 0);
  }

  char INativeScintilla.AutoCGetSeparator() => (char) this._ns.SendMessageDirect(2107U, 0, 0);

  void INativeScintilla.AutoCSelect(string select)
  {
    this._ns.SendMessageDirect(2108U, VOID.NULL, select);
  }

  int INativeScintilla.AutoCGetCurrent() => this._ns.SendMessageDirect(2445U, 0, 0);

  void INativeScintilla.AutoCSetCancelAtStart(bool cancel)
  {
    this._ns.SendMessageDirect(2110U, cancel, 0);
  }

  bool INativeScintilla.AutoCGetCancelAtStart() => this._ns.SendMessageDirect(2111U, 0, 0) != 0;

  void INativeScintilla.AutoCSetFillUps(string chars)
  {
    this._ns.SendMessageDirect(2112U, VOID.NULL, chars);
  }

  void INativeScintilla.AutoCSetChooseSingle(bool chooseSingle)
  {
    this._ns.SendMessageDirect(2113U, chooseSingle, 0);
  }

  bool INativeScintilla.AutoCGetChooseSingle() => this._ns.SendMessageDirect(2114U, 0, 0) != 0;

  void INativeScintilla.AutoCSetIgnoreCase(bool ignoreCase)
  {
    this._ns.SendMessageDirect(2115U, ignoreCase, 0);
  }

  bool INativeScintilla.AutoCGetIgnoreCase() => this._ns.SendMessageDirect(2116U, 0, 0) != 0;

  void INativeScintilla.AutoCSetAutoHide(bool autoHide)
  {
    this._ns.SendMessageDirect(2118U, autoHide, 0);
  }

  bool INativeScintilla.AutoCGetAutoHide() => this._ns.SendMessageDirect(2119U, 0, 0) != 0;

  void INativeScintilla.AutoCSetDropRestOfWord(bool dropRestOfWord)
  {
    this._ns.SendMessageDirect(2270U, dropRestOfWord, 0);
  }

  bool INativeScintilla.AutoCGetDropRestOfWord() => this._ns.SendMessageDirect(2271U, 0, 0) != 0;

  void INativeScintilla.RegisterImage(int type, string xpmData)
  {
    this._ns.SendMessageDirect(2405U, type, xpmData);
  }

  void INativeScintilla.ClearRegisteredImages() => this._ns.SendMessageDirect(2408U, 0, 0);

  void INativeScintilla.AutoCSetTypeSeparator(char separatorCharacter)
  {
    this._ns.SendMessageDirect(2286U, (int) separatorCharacter, 0);
  }

  char INativeScintilla.AutoCGetTypeSeparator() => (char) this._ns.SendMessageDirect(2285U, 0, 0);

  void INativeScintilla.AutoCSetMaxHeight(int rowCount)
  {
    this._ns.SendMessageDirect(2210U, rowCount, 0);
  }

  int INativeScintilla.AutoCGetMaxHeight() => this._ns.SendMessageDirect(2211U, 0, 0);

  void INativeScintilla.AutoCSetMaxWidth(int characterCount)
  {
    this._ns.SendMessageDirect(2208U, characterCount, 0);
  }

  int INativeScintilla.AutoCGetMaxWidth() => this._ns.SendMessageDirect(2209U, 0, 0);

  void INativeScintilla.UserListShow(int listType, string list)
  {
    this._ns.SendMessageDirect(2117U, listType, list);
  }

  void INativeScintilla.CallTipShow(int posStart, string definition)
  {
    this._ns.SendMessageDirect(2200U, posStart, definition);
  }

  void INativeScintilla.CallTipCancel() => this._ns.SendMessageDirect(2201U, 0, 0);

  bool INativeScintilla.CallTipActive() => this._ns.SendMessageDirect(2202U, 0, 0) != 0;

  int INativeScintilla.CallTipGetPosStart() => this._ns.SendMessageDirect(2203U, 0, 0);

  void INativeScintilla.CallTipSetHlt(int hlStart, int hlEnd)
  {
    this._ns.SendMessageDirect(2204U, hlStart, hlEnd);
  }

  void INativeScintilla.CallTipSetBack(int colour) => this._ns.SendMessageDirect(2205U, colour, 0);

  void INativeScintilla.CallTipSetFore(int colour) => this._ns.SendMessageDirect(2206U, colour, 0);

  void INativeScintilla.CallTipSetForeHlt(int colour)
  {
    this._ns.SendMessageDirect(2207U, colour, 0);
  }

  void INativeScintilla.CallTipUseStyle(int tabsize)
  {
    this._ns.SendMessageDirect(2212U, tabsize, 0);
  }

  void INativeScintilla.LineDown() => this._ns.SendMessageDirect(2300U, 0, 0);

  void INativeScintilla.LineDownExtend() => this._ns.SendMessageDirect(2301U, 0, 0);

  void INativeScintilla.LineDownRectExtend() => this._ns.SendMessageDirect(2426U, 0, 0);

  void INativeScintilla.LineScrollDown() => this._ns.SendMessageDirect(2342U, 0, 0);

  void INativeScintilla.LineUp() => this._ns.SendMessageDirect(2302U, 0, 0);

  void INativeScintilla.LineUpExtend() => this._ns.SendMessageDirect(2303U /*0x08FF*/, 0, 0);

  void INativeScintilla.LineUpRectExtend() => this._ns.SendMessageDirect(2427U, 0, 0);

  void INativeScintilla.LineScrollUp() => this._ns.SendMessageDirect(2343U, 0, 0);

  void INativeScintilla.ParaDown() => this._ns.SendMessageDirect(2413U, 0, 0);

  void INativeScintilla.ParaDownExtend() => this._ns.SendMessageDirect(2414U, 0, 0);

  void INativeScintilla.ParaUp() => this._ns.SendMessageDirect(2415U, 0, 0);

  void INativeScintilla.ParaUpExtend() => this._ns.SendMessageDirect(2416U, 0, 0);

  void INativeScintilla.CharLeft() => this._ns.SendMessageDirect(2304U /*0x0900*/, 0, 0);

  void INativeScintilla.CharLeftExtend() => this._ns.SendMessageDirect(2305U, 0, 0);

  void INativeScintilla.CharLeftRectExtend() => this._ns.SendMessageDirect(2428U, 0, 0);

  void INativeScintilla.CharRight() => this._ns.SendMessageDirect(2306U, 0, 0);

  void INativeScintilla.CharRightExtend() => this._ns.SendMessageDirect(2307U, 0, 0);

  void INativeScintilla.CharRightRectExtend() => this._ns.SendMessageDirect(2429U, 0, 0);

  void INativeScintilla.WordLeft() => this._ns.SendMessageDirect(2308U, 0, 0);

  void INativeScintilla.WordLeftExtend() => this._ns.SendMessageDirect(2309U, 0, 0);

  void INativeScintilla.WordRight() => this._ns.SendMessageDirect(2310U, 0, 0);

  void INativeScintilla.WordRightExtend() => this._ns.SendMessageDirect(2311U, 0, 0);

  void INativeScintilla.WordLeftEnd() => this._ns.SendMessageDirect(2439U, 0, 0);

  void INativeScintilla.WordLeftEndExtend() => this._ns.SendMessageDirect(2440U, 0, 0);

  void INativeScintilla.WordRightEnd() => this._ns.SendMessageDirect(2441U, 0, 0);

  void INativeScintilla.WordRightEndExtend() => this._ns.SendMessageDirect(2442U, 0, 0);

  void INativeScintilla.WordPartLeft() => this._ns.SendMessageDirect(2390U, 0, 0);

  void INativeScintilla.WordPartLeftExtend() => this._ns.SendMessageDirect(2391U, 0, 0);

  void INativeScintilla.WordPartRight() => this._ns.SendMessageDirect(2392U, 0, 0);

  void INativeScintilla.WordPartRightExtend() => this._ns.SendMessageDirect(2393U, 0, 0);

  void INativeScintilla.Home() => this._ns.SendMessageDirect(2312U, 0, 0);

  void INativeScintilla.HomeExtend() => this._ns.SendMessageDirect(2313U, 0, 0);

  void INativeScintilla.HomeRectExtend() => this._ns.SendMessageDirect(2430U, 0, 0);

  void INativeScintilla.HomeDisplay() => this._ns.SendMessageDirect(2345U, 0, 0);

  void INativeScintilla.HomeDisplayExtend() => this._ns.SendMessageDirect(2346U, 0, 0);

  void INativeScintilla.HomeWrap() => this._ns.SendMessageDirect(2349U, 0, 0);

  void INativeScintilla.HomeWrapExtend() => this._ns.SendMessageDirect(2450U, 0, 0);

  void INativeScintilla.VCHome() => this._ns.SendMessageDirect(2331U, 0, 0);

  void INativeScintilla.VCHomeExtend() => this._ns.SendMessageDirect(2332U, 0, 0);

  void INativeScintilla.VCHomeRectExtend() => this._ns.SendMessageDirect(2431U, 0, 0);

  void INativeScintilla.VCHomeWrap() => this._ns.SendMessageDirect(2453U, 0, 0);

  void INativeScintilla.VCHomeWrapExtend() => this._ns.SendMessageDirect(2454U, 0, 0);

  void INativeScintilla.LineEnd() => this._ns.SendMessageDirect(2314U, 0, 0);

  void INativeScintilla.LineEndExtend() => this._ns.SendMessageDirect(2315U, 0, 0);

  void INativeScintilla.LineEndRectExtend() => this._ns.SendMessageDirect(2432U, 0, 0);

  void INativeScintilla.LineEndDisplay() => this._ns.SendMessageDirect(2347U, 0, 0);

  void INativeScintilla.LineEndDisplayExtend() => this._ns.SendMessageDirect(2348U, 0, 0);

  void INativeScintilla.LineEndWrap() => this._ns.SendMessageDirect(2451U, 0, 0);

  void INativeScintilla.LineEndWrapExtend() => this._ns.SendMessageDirect(2452U, 0, 0);

  void INativeScintilla.DocumentStart() => this._ns.SendMessageDirect(2316U, 0, 0);

  void INativeScintilla.DocumentStartExtend() => this._ns.SendMessageDirect(2317U, 0, 0);

  void INativeScintilla.DocumentEnd() => this._ns.SendMessageDirect(2318U, 0, 0);

  void INativeScintilla.DocumentEndExtend() => this._ns.SendMessageDirect(2319U, 0, 0);

  void INativeScintilla.PageUp() => this._ns.SendMessageDirect(2320U, 0, 0);

  void INativeScintilla.PageUpExtend() => this._ns.SendMessageDirect(2321U, 0, 0);

  void INativeScintilla.PageUpRectExtend() => this._ns.SendMessageDirect(2433U, 0, 0);

  void INativeScintilla.PageDown() => this._ns.SendMessageDirect(2322U, 0, 0);

  void INativeScintilla.PageDownExtend() => this._ns.SendMessageDirect(2323U, 0, 0);

  void INativeScintilla.PageDownRectExtend() => this._ns.SendMessageDirect(2434U, 0, 0);

  void INativeScintilla.StutteredPageUp() => this._ns.SendMessageDirect(2435U, 0, 0);

  void INativeScintilla.StutteredPageUpExtend() => this._ns.SendMessageDirect(2436U, 0, 0);

  void INativeScintilla.StutteredPageDown() => this._ns.SendMessageDirect(2437U, 0, 0);

  void INativeScintilla.StutteredPageDownExtend() => this._ns.SendMessageDirect(2438U, 0, 0);

  void INativeScintilla.DeleteBack() => this._ns.SendMessageDirect(2326U, 0, 0);

  void INativeScintilla.DeleteBackNotLine() => this._ns.SendMessageDirect(2344U, 0, 0);

  void INativeScintilla.DelWordLeft() => this._ns.SendMessageDirect(2335U, 0, 0);

  void INativeScintilla.DelWordRight() => this._ns.SendMessageDirect(2336U, 0, 0);

  void INativeScintilla.DelLineLeft() => this._ns.SendMessageDirect(2395U, 0, 0);

  void INativeScintilla.DelLineRight() => this._ns.SendMessageDirect(2396U, 0, 0);

  void INativeScintilla.LineDelete() => this._ns.SendMessageDirect(2338U, 0, 0);

  void INativeScintilla.LineCut() => this._ns.SendMessageDirect(2337U, 0, 0);

  void INativeScintilla.LineCopy() => this._ns.SendMessageDirect(2455U, 0, 0);

  void INativeScintilla.LineTranspose() => this._ns.SendMessageDirect(2339U, 0, 0);

  void INativeScintilla.LineDuplicate() => this._ns.SendMessageDirect(2404U, 0, 0);

  void INativeScintilla.LowerCase() => this._ns.SendMessageDirect(2340U, 0, 0);

  void INativeScintilla.UpperCase() => this._ns.SendMessageDirect(2341U, 0, 0);

  void INativeScintilla.Cancel() => this._ns.SendMessageDirect(2325U, 0, 0);

  void INativeScintilla.EditToggleOvertype() => this._ns.SendMessageDirect(2324U, 0, 0);

  void INativeScintilla.NewLine() => this._ns.SendMessageDirect(2329U, 0, 0);

  void INativeScintilla.FormFeed() => this._ns.SendMessageDirect(2330U, 0, 0);

  void INativeScintilla.Tab() => this._ns.SendMessageDirect(2327U, 0, 0);

  void INativeScintilla.BackTab() => this._ns.SendMessageDirect(2328U, 0, 0);

  void INativeScintilla.SelectionDuplicate() => this._ns.SendMessageDirect(2469U, 0, 0);

  void INativeScintilla.AssignCmdKey(int keyDefinition, int sciCommand)
  {
    this._ns.SendMessageDirect(2070U, keyDefinition, sciCommand);
  }

  void INativeScintilla.ClearCmdKey(int keyDefinition)
  {
    this._ns.SendMessageDirect(2071U, keyDefinition, 0);
  }

  void INativeScintilla.ClearAllCmdKeys() => this._ns.SendMessageDirect(2072U, 0, 0);

  void INativeScintilla.Null() => this._ns.SendMessageDirect(2172U, 0, 0);

  void INativeScintilla.UsePopUp(bool bEnablePopup)
  {
    this._ns.SendMessageDirect(2371U, bEnablePopup, 0);
  }

  void INativeScintilla.StartRecord() => this._ns.SendMessageDirect(3001U, 0, 0);

  void INativeScintilla.StopRecord() => this._ns.SendMessageDirect(3002U, 0, 0);

  unsafe int INativeScintilla.FormatRange(bool bDraw, ref RangeToFormat pfr)
  {
    fixed (RangeToFormat* lParam = &pfr)
      return (int) this._ns.SendMessageDirect(2151U, (IntPtr) (bDraw ? 1 : 0), (IntPtr) (void*) lParam);
  }

  void INativeScintilla.SetPrintMagnification(int magnification)
  {
    this._ns.SendMessageDirect(2146U, magnification, 0);
  }

  int INativeScintilla.GetPrintMagnification() => this._ns.SendMessageDirect(2147U, 0, 0);

  void INativeScintilla.SetPrintColourMode(int mode) => this._ns.SendMessageDirect(2148U, mode, 0);

  int INativeScintilla.GetPrintColourMode() => this._ns.SendMessageDirect(2149U, 0, 0);

  int INativeScintilla.GetPrintWrapMode() => this._ns.SendMessageDirect(2407U, 0, 0);

  void INativeScintilla.SetPrintWrapMode(int wrapMode)
  {
    this._ns.SendMessageDirect(2406U, wrapMode, 0);
  }

  IntPtr INativeScintilla.GetDirectFunction()
  {
    return this._ns.SendMessageDirect(2184U, IntPtr.Zero, IntPtr.Zero);
  }

  IntPtr INativeScintilla.GetDirectPointer()
  {
    return this._ns.SendMessageDirect(2185U, IntPtr.Zero, IntPtr.Zero);
  }

  IntPtr INativeScintilla.GetDocPointer()
  {
    return this._ns.SendMessageDirect(2357U, IntPtr.Zero, IntPtr.Zero);
  }

  void INativeScintilla.SetDocPointer(IntPtr pDoc)
  {
    this._ns.SendMessageDirect(2358U, IntPtr.Zero, pDoc);
  }

  IntPtr INativeScintilla.CreateDocument() => (IntPtr) this._ns.SendMessageDirect(2375U, 0, 0);

  void INativeScintilla.AddRefDocument(IntPtr pDoc)
  {
    this._ns.SendMessageDirect(2376U, IntPtr.Zero, pDoc);
  }

  void INativeScintilla.ReleaseDocument(IntPtr pDoc)
  {
    this._ns.SendMessageDirect(2377U, IntPtr.Zero, pDoc);
  }

  int INativeScintilla.VisibleFromDocLine(int docLine)
  {
    return this._ns.SendMessageDirect(2220U, docLine, 0);
  }

  int INativeScintilla.DocLineFromVisible(int displayLine)
  {
    return this._ns.SendMessageDirect(2221U, displayLine, 0);
  }

  void INativeScintilla.ShowLines(int lineStart, int lineEnd)
  {
    this._ns.SendMessageDirect(2226U, lineStart, lineEnd);
  }

  void INativeScintilla.HideLines(int lineStart, int lineEnd)
  {
    this._ns.SendMessageDirect(2227U, lineStart, lineEnd);
  }

  bool INativeScintilla.GetLineVisible(int line) => this._ns.SendMessageDirect(2228U, line, 0) != 0;

  void INativeScintilla.SetFoldLevel(int line, uint level)
  {
    this._ns.SendMessageDirect(2222U, line, level);
  }

  uint INativeScintilla.GetFoldLevel(int line) => (uint) this._ns.SendMessageDirect(2223U, line, 0);

  void INativeScintilla.SetFoldFlags(int flags) => this._ns.SendMessageDirect(2233U, flags, 0);

  int INativeScintilla.GetLastChild(int line, int level)
  {
    return this._ns.SendMessageDirect(2224U, line, level);
  }

  int INativeScintilla.GetFoldParent(int line) => this._ns.SendMessageDirect(2225U, line, 0);

  void INativeScintilla.SetFoldExpanded(int line, bool expanded)
  {
    this._ns.SendMessageDirect(2229U, line, expanded);
  }

  bool INativeScintilla.GetFoldExpanded(int line)
  {
    return this._ns.SendMessageDirect(2230U, line, 0) != 0;
  }

  void INativeScintilla.ToggleFold(int line) => this._ns.SendMessageDirect(2231U, line, 0);

  void INativeScintilla.EnsureVisible(int line) => this._ns.SendMessageDirect(2232U, line, 0);

  void INativeScintilla.EnsureVisibleEnforcePolicy(int line)
  {
    this._ns.SendMessageDirect(2234U, line, 0);
  }

  void INativeScintilla.SetWrapMode(int wrapMode) => this._ns.SendMessageDirect(2268U, wrapMode, 0);

  int INativeScintilla.GetWrapMode() => this._ns.SendMessageDirect(2269U, 0, 0);

  void INativeScintilla.SetWrapVisualFlags(int wrapVisualFlags)
  {
    this._ns.SendMessageDirect(2460U, wrapVisualFlags, 0);
  }

  int INativeScintilla.GetWrapVisualFlags() => this._ns.SendMessageDirect(2461U, 0, 0);

  void INativeScintilla.SetWrapVisualFlagsLocation(int wrapVisualFlagsLocation)
  {
    this._ns.SendMessageDirect(2462U, wrapVisualFlagsLocation, 0);
  }

  int INativeScintilla.GetWrapVisualFlagsLocation() => this._ns.SendMessageDirect(2463U, 0, 0);

  void INativeScintilla.SetWrapStartIndent(int indent)
  {
    this._ns.SendMessageDirect(2464U, indent, 0);
  }

  int INativeScintilla.GetWrapStartIndent() => this._ns.SendMessageDirect(2465U, 0, 0);

  void INativeScintilla.SetLayoutCache(int cacheMode)
  {
    this._ns.SendMessageDirect(2272U, cacheMode, 0);
  }

  int INativeScintilla.GetLayoutCache() => this._ns.SendMessageDirect(2273U, 0, 0);

  void INativeScintilla.LinesJoin() => this._ns.SendMessageDirect(2288U, 0, 0);

  void INativeScintilla.LinesSplit(int pixelWidth)
  {
    this._ns.SendMessageDirect(2289U, pixelWidth, 0);
  }

  int INativeScintilla.WrapCount(int docLine) => this._ns.SendMessageDirect(2235U, docLine, 0);

  void INativeScintilla.ZoomIn() => this._ns.SendMessageDirect(2333U, 0, 0);

  void INativeScintilla.ZoomOut() => this._ns.SendMessageDirect(2334U, 0, 0);

  void INativeScintilla.SetZoom(int zoomInPoints)
  {
    this._ns.SendMessageDirect(2373U, zoomInPoints, 0);
  }

  int INativeScintilla.GetZoom() => this._ns.SendMessageDirect(2374U, 0, 0);

  void INativeScintilla.SetEdgeMode(int mode) => this._ns.SendMessageDirect(2363U, mode, 0);

  int INativeScintilla.GetEdgeMode() => this._ns.SendMessageDirect(2362U, 0, 0);

  void INativeScintilla.SetEdgeColumn(int column) => this._ns.SendMessageDirect(2361U, column, 0);

  int INativeScintilla.GetEdgeColumn() => this._ns.SendMessageDirect(2360U, 0, 0);

  void INativeScintilla.SetEdgeColour(int colour) => this._ns.SendMessageDirect(2365U, colour, 0);

  int INativeScintilla.GetEdgeColour() => this._ns.SendMessageDirect(2364U, 0, 0);

  void INativeScintilla.SetLexer(int lexer) => this._ns.SendMessageDirect(4001U, lexer, 0);

  int INativeScintilla.GetLexer() => this._ns.SendMessageDirect(4002U, 0, 0);

  void INativeScintilla.SetLexerLanguage(string name)
  {
    this._ns.SendMessageDirect(4006U, VOID.NULL, name);
  }

  void INativeScintilla.LoadLexerLibrary(string path)
  {
    this._ns.SendMessageDirect(4007U, VOID.NULL, path);
  }

  void INativeScintilla.Colourise(int start, int end)
  {
    this._ns.SendMessageDirect(4003U, start, end);
  }

  void INativeScintilla.SetProperty(string key, string value)
  {
    this._ns.SendMessageDirect(4004U, key, value);
  }

  void INativeScintilla.GetProperty(string key, out string value)
  {
    this._ns.SendMessageDirect(4008U, key, out value);
  }

  void INativeScintilla.GetPropertyExpanded(string key, out string value)
  {
    this._ns.SendMessageDirect(4009U, key, out value);
  }

  int INativeScintilla.GetPropertyInt(string key, int @default)
  {
    return this._ns.SendMessageDirect(4010U, key, @default);
  }

  void INativeScintilla.SetKeywords(int keywordSet, string keyWordList)
  {
    this._ns.SendMessageDirect(4005U, keywordSet, keyWordList);
  }

  int INativeScintilla.GetStyleBitsNeeded() => this._ns.SendMessageDirect(4011U, 0, 0);

  internal void FireStyleNeeded(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._styleNeededEventKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._styleNeededEventKeyNative])((object) this, ea);
    this.OnStyleNeeded(new StyleNeededEventArgs(new Range(this._ns.PositionFromLine(this._ns.LineFromPosition(this._ns.GetEndStyled())), ea.SCNotification.position, this)));
  }

  internal void FireCharAdded(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._charAddedEventKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._charAddedEventKeyNative])((object) this, ea);
    this.OnCharAdded(new CharAddedEventArgs(ea.SCNotification.ch));
  }

  internal void FireSavePointReached(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._savePointReachedEventKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._savePointReachedEventKeyNative])((object) this, ea);
    this.Modified = false;
  }

  internal void FireSavePointLeft(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._savePointLeftEventKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._savePointLeftEventKeyNative])((object) this, ea);
    this.Modified = true;
  }

  internal void FireModifyAttemptRO(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._modifyAttemptROEventKey] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._modifyAttemptROEventKey])((object) this, ea);
    this.OnReadOnlyModifyAttempt(EventArgs.Empty);
  }

  internal void FireKey(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._keyEventKey] == null)
      return;
    ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._keyEventKey])((object) this, ea);
  }

  internal void FireDoubleClick(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._doubleClickEventKey] == null)
      return;
    ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._doubleClickEventKey])((object) this, ea);
  }

  internal void FireUpdateUI(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._updateUIEventKey] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._updateUIEventKey])((object) this, ea);
    if (this.lastSelection.Start != this.Selection.Start || this.lastSelection.End != this.Selection.End || this.lastSelection.Length != this.Selection.Length)
      this.OnSelectionChanged(EventArgs.Empty);
    this.lastSelection.Start = this.Selection.Start;
    this.lastSelection.End = this.Selection.End;
    this.lastSelection.Length = this.Selection.Length;
  }

  internal void FireMacroRecord(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._macroRecordEventKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._macroRecordEventKeyNative])((object) this, ea);
    this.OnMacroRecord(new MacroRecordEventArgs(ea));
  }

  internal void FireMarginClick(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._marginClickEventKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._marginClickEventKeyNative])((object) this, ea);
    this.FireMarginClick(ea.SCNotification);
  }

  internal void FireModified(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._modifiedEventKey] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._modifiedEventKey])((object) this, ea);
    SCNotification scNotification = ea.SCNotification;
    int modificationType = scNotification.modificationType;
    if (((long) modificationType & 3075L) > 0L)
    {
      TextModifiedEventArgs e = new TextModifiedEventArgs(modificationType, ((long) modificationType | 16L /*0x10*/) > 0L, scNotification.line, scNotification.position, scNotification.length, scNotification.linesAdded, Utilities.IntPtrToString(this._encoding, scNotification.text, scNotification.length));
      bool flag = false;
      if (((long) modificationType & 2048L /*0x0800*/) > 0L)
      {
        this.OnBeforeTextDelete(e);
        flag = true;
      }
      else if (((long) modificationType & 1024L /*0x0400*/) > 0L)
      {
        this.OnBeforeTextInsert(e);
        flag = true;
      }
      else if (((long) modificationType & 2L) > 0L)
      {
        this.OnTextDeleted(e);
        flag = true;
      }
      else if (((long) modificationType & 1L) > 0L)
      {
        this.OnTextInserted(e);
        flag = true;
      }
      if (flag)
        this._textChangedTimer.Start();
    }
    else if (((long) modificationType & 8L) > 0L)
      this.OnFoldChanged(new FoldChangedEventArgs(scNotification.line, scNotification.foldLevelNow, scNotification.foldLevelPrev, scNotification.modificationType));
    else if (((long) modificationType & 4L) > 0L)
      this.OnStyleChanged((EventArgs) new StyleChangedEventArgs(scNotification.position, scNotification.length, scNotification.modificationType));
    else if (((long) modificationType & 512L /*0x0200*/) > 0L)
      this.OnMarkerChanged(new MarkerChangedEventArgs(scNotification.line, scNotification.modificationType));
    this.OnDocumentChange(ea);
  }

  private void textChangedTimer_Tick(object sender, EventArgs e)
  {
    this._textChangedTimer.Stop();
    this.OnTextChanged();
  }

  internal void FireChange(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._changeEventKey] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._changeEventKey])((object) this, ea);
    this.OnTextChanged(EventArgs.Empty);
  }

  internal void FireNeedShown(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._needShownEventKey] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._needShownEventKey])((object) this, ea);
    int position = ea.SCNotification.position;
    this.OnLinesNeedShown(new LinesNeedShownEventArgs(this._ns.LineFromPosition(position), this._ns.LineFromPosition(position + ea.SCNotification.length - 1)));
  }

  internal void FirePainted(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._paintedEventKey] == null)
      return;
    ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._paintedEventKey])((object) this, ea);
  }

  internal void FireUserListSelection(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._userListSelectionEventKeyNative] == null)
      return;
    ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._userListSelectionEventKeyNative])((object) this, ea);
  }

  internal void FireUriDropped(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._uriDroppedEventKeyNative] == null)
      return;
    ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._uriDroppedEventKeyNative])((object) this, ea);
  }

  internal void FireDwellStart(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._dwellStartEventKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._dwellStartEventKeyNative])((object) this, ea);
    this.OnDwellStart(new ScintillaMouseEventArgs(ea.SCNotification.x, ea.SCNotification.y, ea.SCNotification.position));
  }

  internal void FireDwellEnd(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._dwellEndEventKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._dwellEndEventKeyNative])((object) this, ea);
    this.OnDwellEnd(new ScintillaMouseEventArgs(ea.SCNotification.x, ea.SCNotification.y, ea.SCNotification.position));
  }

  internal void FireZoom(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._zoomEventKey] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._zoomEventKey])((object) this, ea);
    this.OnZoomChanged(EventArgs.Empty);
  }

  internal void FireHotSpotClick(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._hotSpotClickEventKey] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._hotSpotClickEventKey])((object) this, ea);
    Point client = this.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
    this.OnHotspotClick(new ScintillaMouseEventArgs(client.X, client.Y, ea.SCNotification.position));
  }

  internal void FireHotSpotDoubleclick(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._hotSpotDoubleClickEventKey] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._hotSpotDoubleClickEventKey])((object) this, ea);
    Point client = this.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
    this.OnHotspotDoubleClick(new ScintillaMouseEventArgs(client.X, client.Y, ea.SCNotification.position));
  }

  internal void FireCallTipClick(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._callTipClickEventKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._callTipClickEventKeyNative])((object) this, ea);
    this.FireCallTipClick(ea.SCNotification.position);
  }

  internal void FireAutoCSelection(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._autoCSelectionEventKey] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._autoCSelectionEventKey])((object) this, ea);
    this.OnAutoCompleteAccepted(new AutoCompleteAcceptedEventArgs(ea.SCNotification, this._encoding));
  }

  internal void FireIndicatorClick(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._indicatorClickKeyNative] != null)
      ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._indicatorClickKeyNative])((object) this, ea);
    this.OnIndicatorClick(new ScintillaMouseEventArgs(ea.SCNotification.x, ea.SCNotification.y, ea.SCNotification.position));
  }

  internal void FireIndicatorRelease(NativeScintillaEventArgs ea)
  {
    if ((object) this.Events[Scintilla._indicatorReleaseKeyNative] == null)
      return;
    ((EventHandler<NativeScintillaEventArgs>) this.Events[Scintilla._indicatorReleaseKeyNative])((object) this, ea);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.StyleNeeded
  {
    add => this.Events.AddHandler(Scintilla._styleNeededEventKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._styleNeededEventKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.CharAdded
  {
    add => this.Events.AddHandler(Scintilla._charAddedEventKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._charAddedEventKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.SavePointReached
  {
    add => this.Events.AddHandler(Scintilla._savePointReachedEventKeyNative, (Delegate) value);
    remove
    {
      this.Events.RemoveHandler(Scintilla._savePointReachedEventKeyNative, (Delegate) value);
    }
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.SavePointLeft
  {
    add => this.Events.AddHandler(Scintilla._savePointLeftEventKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._savePointLeftEventKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.ModifyAttemptRO
  {
    add => this.Events.AddHandler(Scintilla._modifyAttemptROEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._modifyAttemptROEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.Key
  {
    add => this.Events.AddHandler(Scintilla._keyEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._keyEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.DoubleClick
  {
    add => this.Events.AddHandler(Scintilla._doubleClickEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._doubleClickEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.UpdateUI
  {
    add => this.Events.AddHandler(Scintilla._updateUIEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._updateUIEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.MacroRecord
  {
    add => this.Events.AddHandler(Scintilla._macroRecordEventKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._macroRecordEventKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.MarginClick
  {
    add => this.Events.AddHandler(Scintilla._marginClickEventKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._marginClickEventKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.Modified
  {
    add => this.Events.AddHandler(Scintilla._modifiedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._modifiedEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.Change
  {
    add => this.Events.AddHandler(Scintilla._changeEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._changeEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.NeedShown
  {
    add => this.Events.AddHandler(Scintilla._needShownEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._needShownEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.Painted
  {
    add => this.Events.AddHandler(Scintilla._paintedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._paintedEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.UserListSelection
  {
    add => this.Events.AddHandler(Scintilla._userListSelectionEventKeyNative, (Delegate) value);
    remove
    {
      this.Events.RemoveHandler(Scintilla._userListSelectionEventKeyNative, (Delegate) value);
    }
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.UriDropped
  {
    add => this.Events.AddHandler(Scintilla._uriDroppedEventKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._uriDroppedEventKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.DwellStart
  {
    add => this.Events.AddHandler(Scintilla._dwellStartEventKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._dwellStartEventKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.DwellEnd
  {
    add => this.Events.AddHandler(Scintilla._dwellEndEventKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._dwellEndEventKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.Zoom
  {
    add => this.Events.AddHandler(Scintilla._zoomEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._zoomEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.HotSpotClick
  {
    add => this.Events.AddHandler(Scintilla._hotSpotClickEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._hotSpotClickEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.HotSpotDoubleclick
  {
    add => this.Events.AddHandler(Scintilla._hotSpotDoubleClickEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._hotSpotDoubleClickEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.CallTipClick
  {
    add => this.Events.AddHandler(Scintilla._callTipClickEventKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._callTipClickEventKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.AutoCSelection
  {
    add => this.Events.AddHandler(Scintilla._autoCSelectionEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._autoCSelectionEventKey, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.IndicatorClick
  {
    add => this.Events.AddHandler(Scintilla._indicatorClickKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._indicatorClickKeyNative, (Delegate) value);
  }

  event EventHandler<NativeScintillaEventArgs> INativeScintilla.IndicatorRelease
  {
    add => this.Events.AddHandler(Scintilla._indicatorReleaseKeyNative, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._indicatorReleaseKeyNative, (Delegate) value);
  }

  void INativeScintilla.SetModEventMask(int modEventMask)
  {
    this._ns.SendMessageDirect(2359U, modEventMask, 0);
  }

  int INativeScintilla.GetModEventMask() => this._ns.SendMessageDirect(2378U, 0, 0);

  void INativeScintilla.SetMouseDwellTime(int mouseDwellTime)
  {
    this._ns.SendMessageDirect(2264U, mouseDwellTime, 0);
  }

  int INativeScintilla.GetMouseDwellTime() => this._ns.SendMessageDirect(2265U, 0, 0);

  internal Dictionary<string, Color> ColorBag => this._colorBag;

  internal Hashtable PropertyBag => this._propertyBag;

  public Scintilla()
  {
    this._state = new BitVector32(0);
    this._state[Scintilla._acceptsReturnState] = true;
    this._state[Scintilla._acceptsTabState] = true;
    this._ns = (INativeScintilla) this;
    this._textChangedTimer = new System.Timers.Timer();
    this._textChangedTimer.Interval = 1;
    this._textChangedTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.textChangedTimer_Tick);
    this._caption = this.GetType().FullName;
    this.Encoding = Encoding.UTF8;
    this._ns.StyleClearAll();
    this._caret = new CaretInfo(this);
    this._lines = new LinesCollection(this);
    this._selection = new Selection(this);
    this._indicators = new IndicatorCollection(this);
    this._snippets = new SnippetManager(this);
    this._margins = new MarginCollection(this);
    this._scrolling = new Scrolling(this);
    this._whitespace = new Whitespace(this);
    this._endOfLine = new EndOfLine(this);
    this._clipboard = new Clipboard(this);
    this._undoRedo = new UndoRedo(this);
    this._dropMarkers = new DropMarkers(this);
    this._hotspotStyle = new HotspotStyle(this);
    this._callTip = new CallTip(this);
    this._styles = new StyleCollection(this);
    this._indentation = new Indentation(this);
    this._markers = new MarkerCollection(this);
    this._autoComplete = new AutoComplete(this);
    this._documentHandler = new DocumentHandler(this);
    this._lineWrap = new LineWrap(this);
    this._lexing = new Lexing(this);
    this._longLines = new LongLines(this);
    this._commands = new Commands(this);
    this._folding = new Folding(this);
    this._configurationManager = new ConfigurationManager(this);
    this._printing = new Printing(this);
    this._findReplace = new FindReplace(this);
    this._documentNavigation = new DocumentNavigation(this);
    this._goto = new GoTo(this);
    this._helpers.AddRange((IEnumerable<TopLevelHelper>) new TopLevelHelper[28]
    {
      (TopLevelHelper) this._caret,
      (TopLevelHelper) this._lines,
      (TopLevelHelper) this._selection,
      (TopLevelHelper) this._indicators,
      (TopLevelHelper) this._snippets,
      (TopLevelHelper) this._margins,
      (TopLevelHelper) this._scrolling,
      (TopLevelHelper) this._whitespace,
      (TopLevelHelper) this._endOfLine,
      (TopLevelHelper) this._clipboard,
      (TopLevelHelper) this._undoRedo,
      (TopLevelHelper) this._dropMarkers,
      (TopLevelHelper) this._hotspotStyle,
      (TopLevelHelper) this._styles,
      (TopLevelHelper) this._indentation,
      (TopLevelHelper) this._markers,
      (TopLevelHelper) this._autoComplete,
      (TopLevelHelper) this._documentHandler,
      (TopLevelHelper) this._lineWrap,
      (TopLevelHelper) this._lexing,
      (TopLevelHelper) this._longLines,
      (TopLevelHelper) this._commands,
      (TopLevelHelper) this._folding,
      (TopLevelHelper) this._configurationManager,
      (TopLevelHelper) this._printing,
      (TopLevelHelper) this._findReplace,
      (TopLevelHelper) this._documentNavigation,
      (TopLevelHelper) this._goto
    });
    this.BackColor = SystemColors.Window;
    this.ForeColor = SystemColors.WindowText;
  }

    protected override void Dispose(bool disposing)
    {
        foreach (ScintillaHelperBase helper in this._helpers)
            helper.Dispose();

        if (disposing && this.IsHandleCreated)
        {
            Message msg = new Message()
            {
                Msg = 2,
                HWnd = this.Handle
            };

            this.DefWndProc(ref msg);
        }
        base.Dispose(disposing);
    }

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 2)
    {
      if (this.IsHandleCreated)
      {
        NativeMethods.SetParent(this.Handle, NativeMethods.HWND_MESSAGE);
        return;
      }
    }
    else
    {
      if (m.Msg == 15)
      {
        base.WndProc(ref m);
        if (!this._isCustomPaintingEnabled)
          return;
        RECT lpRect;
        if (!NativeMethods.GetUpdateRect(this.Handle, out lpRect, false))
          lpRect = (RECT) this.ClientRectangle;
        this.CreateGraphics().SetClip((Rectangle) lpRect);
        this.OnPaint(new PaintEventArgs(this.CreateGraphics(), (Rectangle) lpRect));
        return;
      }
      if (m.Msg == 563)
      {
        this.handleFileDrop(m.WParam);
        return;
      }
      if (m.Msg == 32 /*0x20*/)
      {
        this.DefWndProc(ref m);
        return;
      }
      if (m.Msg == 13)
      {
        m.WParam = (IntPtr) (this.Caption.Length + 1);
        Marshal.Copy(this.Caption.ToCharArray(), 0, m.LParam, this.Caption.Length);
        m.Result = (IntPtr) this.Caption.Length;
        return;
      }
      if (m.Msg == 14)
      {
        m.Result = (IntPtr) this.Caption.Length;
        return;
      }
      if ((m.Msg ^ 8192 /*0x2000*/) == 78)
      {
        this.ReflectNotify(ref m);
        return;
      }
      if (m.Msg >= 10000)
      {
        this._commands.Execute((BindableCommand) m.Msg);
        return;
      }
    }
    if (m.Msg == 276 || m.Msg == 277)
      this.FireScroll(ref m);
    else
      base.WndProc(ref m);
  }

  private void ReflectNotify(ref Message m)
  {
    SCNotification structure = (SCNotification) Marshal.PtrToStructure(m.LParam, typeof (SCNotification));
    NativeScintillaEventArgs ea = new NativeScintillaEventArgs(m, structure);
    switch (structure.nmhdr.code)
    {
      case 768 /*0x0300*/:
        this.FireChange(ea);
        break;
      case 2000:
        this.FireStyleNeeded(ea);
        break;
      case 2001:
        this.FireCharAdded(ea);
        break;
      case 2002:
        this.FireSavePointReached(ea);
        break;
      case 2003:
        this.FireSavePointLeft(ea);
        break;
      case 2004:
        this.FireModifyAttemptRO(ea);
        break;
      case 2005:
        this.FireKey(ea);
        break;
      case 2006:
        this.FireDoubleClick(ea);
        break;
      case 2007:
        this.FireUpdateUI(ea);
        break;
      case 2008:
        this.FireModified(ea);
        break;
      case 2009:
        this.FireMacroRecord(ea);
        break;
      case 2010:
        this.FireMarginClick(ea);
        break;
      case 2011:
        this.FireNeedShown(ea);
        break;
      case 2013:
        this.FirePainted(ea);
        break;
      case 2014:
        this.FireUserListSelection(ea);
        break;
      case 2015:
        this.FireUriDropped(ea);
        break;
      case 2016:
        this.FireDwellStart(ea);
        break;
      case 2017:
        this.FireDwellEnd(ea);
        break;
      case 2018:
        this.FireZoom(ea);
        break;
      case 2019:
        this.FireHotSpotClick(ea);
        break;
      case 2020:
        this.FireHotSpotDoubleclick(ea);
        break;
      case 2021:
        this.FireCallTipClick(ea);
        break;
      case 2022:
        this.FireAutoCSelection(ea);
        break;
      case 2023:
        this.FireIndicatorClick(ea);
        break;
      case 2024:
        this.FireIndicatorRelease(ea);
        break;
    }
  }

  protected override CreateParams CreateParams
  {
    get
    {
      this.SetStyle(ControlStyles.UserPaint, false);
      if (!Scintilla._sciLexerLoaded)
      {
        if (NativeMethods.LoadLibrary(DefaultDllName /* "SciLexer_x64.dll" */) == IntPtr.Zero)
        {
          int lastWin32Error = Marshal.GetLastWin32Error();
          if (lastWin32Error == 126)
            throw new FileNotFoundException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "The Scintilla library could not be found. Please place the library in a searchable path such as the application or '{0}' directory.", (object) Environment.SystemDirectory), (Exception) new Win32Exception(lastWin32Error));
          throw new Win32Exception(lastWin32Error);
        }
        Scintilla._sciLexerLoaded = true;
      }
      CreateParams createParams = base.CreateParams;
      createParams.ClassName = nameof (Scintilla);
      return createParams;
    }
  }

  protected override bool IsInputKey(Keys keyData)
  {
    if ((keyData & Keys.Shift) != Keys.None)
      keyData ^= Keys.Shift;
    switch (keyData)
    {
      case Keys.Tab:
        return this._state[Scintilla._acceptsTabState];
      case Keys.Return:
        return this._state[Scintilla._acceptsReturnState];
      case Keys.Left:
      case Keys.Up:
      case Keys.Right:
      case Keys.Down:
      case Keys.F:
        return true;
      default:
        return base.IsInputKey(keyData);
    }
  }

  protected override void OnKeyPress(KeyPressEventArgs e)
  {
    if (this._supressControlCharacters && e.KeyChar < ' ')
      e.Handled = true;
    Snippet snippet;
    if (this._snippets.IsEnabled && this._snippets.IsOneKeySelectionEmbedEnabled && this._selection.Length > 0 && this._snippets.List.TryGetValue(e.KeyChar.ToString(), out snippet) && snippet.IsSurroundsWith)
    {
      this._snippets.InsertSnippet(snippet);
      e.Handled = true;
    }
    base.OnKeyPress(e);
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (e.Handled)
      return;
    e.SuppressKeyPress = this._commands.ProcessKey(e);
  }

  internal void FireKeyDown(KeyEventArgs e) => this.OnKeyDown(e);

  protected override bool ProcessKeyMessage(ref Message m)
  {
    return (int) m.WParam == 13 && !this.AcceptsReturn || base.ProcessKeyMessage(ref m);
  }

  protected override Size DefaultSize => new Size(200, 100);

  protected override Cursor DefaultCursor => Cursors.IBeam;

  protected override void OnLostFocus(EventArgs e)
  {
    if (this.Selection.HideSelection)
      this._ns.HideSelection(true);
    this._ns.SetSelBack(this.Selection.BackColorUnfocused != Color.Transparent, Utilities.ColorToRgb(this.Selection.BackColorUnfocused));
    this._ns.SetSelFore(this.Selection.ForeColorUnfocused != Color.Transparent, Utilities.ColorToRgb(this.Selection.ForeColorUnfocused));
    base.OnLostFocus(e);
  }

  protected override void OnGotFocus(EventArgs e)
  {
    if (!this.Selection.Hidden)
      this._ns.HideSelection(false);
    this._ns.SetSelBack(this.Selection.BackColor != Color.Transparent, Utilities.ColorToRgb(this.Selection.BackColor));
    this._ns.SetSelFore(this.Selection.ForeColor != Color.Transparent, Utilities.ColorToRgb(this.Selection.ForeColor));
    base.OnGotFocus(e);
  }

  protected override void OnDoubleClick(EventArgs e)
  {
    base.OnDoubleClick(e);
    if (!this._isBraceMatching)
      return;
    int num1 = this.CurrentPos - 1;
    switch (this.CharAt(num1))
    {
      case '(':
      case '[':
      case '{':
        if (this.PositionIsOnComment(num1))
          break;
        int num2 = num1;
        int num3 = this._ns.BraceMatch(num1, 0) + 1;
        this._selection.Start = num2;
        this._selection.End = num3;
        break;
    }
  }

  protected override void OnCreateControl()
  {
    base.OnCreateControl();
    this.OnLoad(EventArgs.Empty);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    this.paintRanges(e.Graphics);
  }

  [DefaultValue(true)]
  [Description("Indicates if return characters are accepted as text input.")]
  [Category("Behavior")]
  public bool AcceptsReturn
  {
    get => this._state[Scintilla._acceptsReturnState];
    set => this._state[Scintilla._acceptsReturnState] = value;
  }

  [Category("Behavior")]
  [Description("Indicates if tab characters are accepted as text input.")]
  [DefaultValue(true)]
  public bool AcceptsTab
  {
    get => this._state[Scintilla._acceptsTabState];
    set => this._state[Scintilla._acceptsTabState] = value;
  }

  public override bool AllowDrop
  {
    get => this._allowDrop;
    set
    {
      NativeMethods.DragAcceptFiles(this.Handle, value);
      this._allowDrop = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Category("Behavior")]
  public AutoComplete AutoComplete => this._autoComplete;

  private bool ShouldSerializeAutoComplete() => this._autoComplete.ShouldSerialize();

  [DefaultValue(typeof (Color), "Window")]
  public override Color BackColor
  {
    get
    {
      return this._colorBag.ContainsKey(nameof (BackColor)) ? this._colorBag[nameof (BackColor)] : SystemColors.Window;
    }
    set
    {
      Color backColor = this.BackColor;
      if (value == SystemColors.Window)
        this._colorBag.Remove(nameof (BackColor));
      else
        this._colorBag[nameof (BackColor)] = value;
      if (!this._useBackColor)
        return;
      for (int index = 0; index < 128 /*0x80*/; ++index)
      {
        if (index != 33)
          this.Styles[index].BackColor = value;
      }
    }
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  [Browsable(false)]
  public override Image BackgroundImage
  {
    get => base.BackgroundImage;
    set => base.BackgroundImage = value;
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  [Browsable(false)]
  public override ImageLayout BackgroundImageLayout
  {
    get => base.BackgroundImageLayout;
    set => base.BackgroundImageLayout = value;
  }

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public CallTip CallTip
  {
    get => this._callTip;
    set => this._callTip = value;
  }

  private bool ShouldSerializeCallTip() => this._callTip.ShouldSerialize();

  [Description("Win32 Window Caption")]
  [Category("Behavior")]
  public string Caption
  {
    get => this._caption;
    set
    {
      if (!(this._caption != value))
        return;
      this._caption = value;
      base.Text = value;
    }
  }

  private void ResetCaption() => this.Caption = this.GetType().FullName;

  private bool ShouldSerializeCaption() => this.Caption != this.GetType().FullName;

  [Category("Appearance")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public CaretInfo Caret => this._caret;

  private bool ShouldSerializeCaret() => this._caret.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Category("Behavior")]
  public Clipboard Clipboard => this._clipboard;

  private bool ShouldSerializeClipboard() => this._clipboard.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int CurrentPos
  {
    get => this.NativeInterface.GetCurrentPos();
    set => this.NativeInterface.GotoPos(value);
  }

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Commands Commands
  {
    get => this._commands;
    set => this._commands = value;
  }

  private bool ShouldSerializeCommands() => this._commands.ShouldSerialize();

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public ConfigurationManager ConfigurationManager
  {
    get => this._configurationManager;
    set => this._configurationManager = value;
  }

  private bool ShouldSerializeConfigurationManager()
  {
    return this._configurationManager.ShouldSerialize();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DocumentHandler DocumentHandler
  {
    get => this._documentHandler;
    set => this._documentHandler = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Category("Behavior")]
  public DocumentNavigation DocumentNavigation
  {
    get => this._documentNavigation;
    set => this._documentNavigation = value;
  }

  private bool ShouldSerializeDocumentNavigation() => this._documentNavigation.ShouldSerialize();

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public DropMarkers DropMarkers => this._dropMarkers;

  private bool ShouldSerializeDropMarkers() => this._dropMarkers.ShouldSerialize();

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public EndOfLine EndOfLine
  {
    get => this._endOfLine;
    set => this._endOfLine = value;
  }

  private bool ShouldSerializeEndOfLine() => this._endOfLine.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public Encoding Encoding
  {
    get => this._encoding;
    set
    {
      this._encoding = Scintilla.ValidCodePages.Contains(value) ? value : throw new EncoderFallbackException("Scintilla only supports the following Encodings: " + Scintilla.ValidCodePages.ToString());
      this._ns.SetCodePage(this._encoding.CodePage);
    }
  }

  [Category("Behavior")]
  public FindReplace FindReplace
  {
    get => this._findReplace;
    set => this._findReplace = value;
  }

  private bool ShouldSerializeFindReplace() => this._findReplace.ShouldSerialize();

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Folding Folding
  {
    get => this._folding;
    set => this._folding = value;
  }

  private bool ShouldSerializeFolding() => this._folding.ShouldSerialize();

  public override Font Font
  {
    get => base.Font;
    set
    {
      if (value == null && this.Parent != null)
        value = this.Parent.Font;
      else if (value == null)
        value = Control.DefaultFont;
      Font font = base.Font;
      if (this._useFont)
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (index != 38)
            this.Styles[index].Font = value;
        }
      }
      if (base.Font.Equals((object) value))
        return;
      base.Font = value;
    }
  }

  protected override void OnFontChanged(EventArgs e)
  {
    this.Font = this.Font;
    base.OnFontChanged(e);
  }

  public override void ResetFont()
  {
    if (this.Parent != null)
      this.Font = this.Parent.Font;
    else
      this.Font = Control.DefaultFont;
  }

  private new bool ShouldSerializeFont() => this.Parent == null || this.Font != this.Parent.Font;

  [DefaultValue(typeof (Color), "WindowText")]
  public override Color ForeColor
  {
    get
    {
      return this._colorBag.ContainsKey(nameof (ForeColor)) ? this._colorBag[nameof (ForeColor)] : base.ForeColor;
    }
    set
    {
      Color foreColor = this.ForeColor;
      if (value == SystemColors.WindowText)
        this._colorBag.Remove(nameof (ForeColor));
      else
        this._colorBag[nameof (ForeColor)] = value;
      if (this._useForeColor)
      {
        for (int index = 0; index < 32 /*0x20*/; ++index)
        {
          if (index != 33)
            this.Styles[index].ForeColor = value;
        }
      }
      base.ForeColor = value;
    }
  }

  protected override void OnForeColorChanged(EventArgs e)
  {
    this.ForeColor = base.ForeColor;
    base.OnForeColorChanged(e);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public GoTo GoTo
  {
    get => this._goto;
    set => this._goto = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Category("Appearance")]
  public HotspotStyle HotspotStyle
  {
    get => this._hotspotStyle;
    set => this._hotspotStyle = value;
  }

  private bool ShouldSerializeHotspotStyle() => this._hotspotStyle.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public IndicatorCollection Indicators => this._indicators;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Category("Behavior")]
  public Indentation Indentation
  {
    get => this._indentation;
    set => this._indentation = value;
  }

  private bool ShouldSerializeIndentation() => this._indentation.ShouldSerialize();

  [Category("Behavior")]
  [DefaultValue(false)]
  public bool IsBraceMatching
  {
    get => this._isBraceMatching;
    set => this._isBraceMatching = value;
  }

  internal int SafeBraceMatch(int position)
  {
    int num1 = (int) this.CharAt(position);
    int textLength = this.TextLength;
    int num2 = 0;
    Lexer lexer = this._lexing.Lexer;
    this._lexing.Colorize(0, -1);
    bool flag = this.PositionIsOnComment(position, lexer);
    int num3;
    int num4;
    switch (num1)
    {
      case 40:
        num3 = 41;
        goto label_22;
      case 41:
        num4 = 40;
        break;
      case 91:
        num3 = 93;
        goto label_22;
      case 93:
        num4 = 91;
        break;
      case 123:
        num3 = 125;
        goto label_22;
      case 125:
        num4 = 123;
        break;
      default:
        return -1;
    }
    while (position >= 0)
    {
      --position;
      int num5 = (int) this.CharAt(position);
      if (num5 == num1)
      {
        if (flag == this.PositionIsOnComment(position, lexer))
          ++num2;
      }
      else if (num5 == num4 && flag == this.PositionIsOnComment(position, lexer))
      {
        --num2;
        if (num2 < 0)
          return position;
      }
    }
    return -1;
label_22:
    while (position < textLength)
    {
      ++position;
      int num6 = (int) this.CharAt(position);
      if (num6 == num1)
      {
        if (flag == this.PositionIsOnComment(position, lexer))
          ++num2;
      }
      else if (num6 == num3 && flag == this.PositionIsOnComment(position, lexer))
      {
        --num2;
        if (num2 < 0)
          return position;
      }
    }
    return -1;
  }

  [Category("Behavior")]
  [DefaultValue(true)]
  public bool IsCustomPaintingEnabled
  {
    get => this._isCustomPaintingEnabled;
    set => this._isCustomPaintingEnabled = value;
  }

  [Category("Behavior")]
  [DefaultValue(false)]
  public bool IsReadOnly
  {
    get => this._ns.GetReadOnly();
    set => this._ns.SetReadOnly(value);
  }

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Lexing Lexing
  {
    get => this._lexing;
    set => this._lexing = value;
  }

  private bool ShouldSerializeLexing() => this._lexing.ShouldSerialize();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public LinesCollection Lines => this._lines;

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public LineWrap LineWrap
  {
    get => this._lineWrap;
    set => this._lineWrap = value;
  }

  private bool ShouldSerializeLineWrap() => this.LineWrap.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Category("Behavior")]
  public LongLines LongLines
  {
    get => this._longLines;
    set => this._longLines = value;
  }

  private bool ShouldSerializeLongLines() => this._longLines.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public List<ManagedRange> ManagedRanges => this._managedRanges;

  [Category("Appearance")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Browsable(true)]
  public MarginCollection Margins => this._margins;

  private bool ShouldSerializeMargins() => this._margins.ShouldSerialize();

  private void ResetMargins() => this._margins.Reset();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Category("Behavior")]
  public MarkerCollection Markers
  {
    get => this._markers;
    set => this._markers = value;
  }

  private bool ShouldSerializeMarkers() => this._markers.ShouldSerialize();

  [DefaultValue(true)]
  [Category("Behavior")]
  public bool MatchBraces
  {
    get => this._matchBraces;
    set
    {
      this._matchBraces = value;
      if (value)
        return;
      this._ns.BraceHighlight(-1, -1);
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool Modified
  {
    get => this._state[Scintilla._modifiedState];
    set
    {
      if (this._state[Scintilla._modifiedState] == value)
        return;
      this._state[Scintilla._modifiedState] = value;
      if (!value)
        this._ns.SetSavePoint();
      this.OnModifiedChanged(EventArgs.Empty);
    }
  }

  [DefaultValue(true)]
  [Category("Behavior")]
  public bool MouseDownCaptures
  {
    get => this.NativeInterface.GetMouseDownCaptures();
    set => this.NativeInterface.SetMouseDownCaptures(value);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INativeScintilla NativeInterface => (INativeScintilla) this;

  [Category("Behavior")]
  [DefaultValue(false)]
  public bool OverType
  {
    get => this._ns.GetOvertype();
    set => this._ns.SetOvertype(value);
  }

  [Category("Layout")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Printing Printing
  {
    get => this._printing;
    set => this._printing = value;
  }

  private bool ShouldSerializePrinting() => this._printing.ShouldSerialize();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public unsafe byte[] RawText
  {
    get
    {
      int wParam = this.NativeInterface.GetTextLength() + 1;
      if (wParam == 1)
        return new byte[1];
      byte[] rawText = new byte[wParam];
      fixed (byte* lParam = rawText)
      {
        this._ns.SendMessageDirect(2182U, (IntPtr) wParam, (IntPtr) (void*) lParam);
        return rawText;
      }
    }
    set
    {
      if (value == null || value.Length == 0)
      {
        this._ns.ClearAll();
      }
      else
      {
        if (value[value.Length - 1] != (byte) 0)
        {
          Array.Resize<byte>(ref value, value.Length + 1);
          value[value.Length - 1] = (byte) 0;
        }
        fixed (byte* lParam = value)
          this._ns.SendMessageDirect(2181U, IntPtr.Zero, (IntPtr) (void*) lParam);
      }
    }
  }

  [Category("Layout")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Scrolling Scrolling
  {
    get => this._scrolling;
    set => this._scrolling = value;
  }

  private bool ShouldSerializeScrolling() => this._scrolling.ShouldSerialize();

  [Category("Appearance")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public Selection Selection => this._selection;

  private bool ShouldSerializeSelection() => this._selection.ShouldSerialize();

  [DefaultValue(SearchFlags.Empty)]
  [Editor(typeof (FlagEnumUIEditor), typeof (UITypeEditor))]
  [Category("Behavior")]
  public SearchFlags SearchFlags
  {
    get => this._searchFlags;
    set => this._searchFlags = value;
  }

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public SnippetManager Snippets => this._snippets;

  private bool ShouldSerializeSnippets() => this._snippets.ShouldSerialize();

  [Category("Appearance")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public StyleCollection Styles
  {
    get => this._styles;
    set => this._styles = value;
  }

  private bool ShouldSerializeStyles() => this._styles.ShouldSerialize();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool SupressControlCharacters
  {
    get => this._supressControlCharacters;
    set => this._supressControlCharacters = value;
  }

  [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", typeof (UITypeEditor))]
  public override string Text
  {
    get
    {
      string text;
      this._ns.GetText(this._ns.GetLength() + 1, out text);
      return text;
    }
    set
    {
      if (string.IsNullOrEmpty(value))
        this._ns.ClearAll();
      else
        this._ns.SetText(value);
    }
  }

  [Browsable(false)]
  public int TextLength => this.NativeInterface.GetTextLength();

  [Category("Behavior")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public UndoRedo UndoRedo => this._undoRedo;

  public bool ShouldSerializeUndoRedo() => this._undoRedo.ShouldSerialize();

  [DefaultValue(false)]
  [Category("Appearance")]
  public bool UseForeColor
  {
    get => this._useForeColor;
    set
    {
      this._useForeColor = value;
      if (!value)
        return;
      this.ForeColor = this.ForeColor;
    }
  }

  [DefaultValue(false)]
  [Category("Appearance")]
  public bool UseFont
  {
    get => this._useFont;
    set
    {
      this._useFont = value;
      if (!value)
        return;
      this.Font = this.Font;
    }
  }

  [DefaultValue(false)]
  [Category("Appearance")]
  public bool UseBackColor
  {
    get => this._useBackColor;
    set
    {
      this._useBackColor = value;
      if (!value)
        return;
      this.BackColor = this.BackColor;
    }
  }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool UseWaitCursor
  {
    get => base.UseWaitCursor;
    set
    {
      base.UseWaitCursor = value;
      if (value)
        this.NativeInterface.SetCursor(4);
      else
        this.NativeInterface.SetCursor(-1);
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Description("The display mode and style of whitespace characters.")]
  [Category("Appearance")]
  public Whitespace Whitespace => this._whitespace;

  [DefaultValue(0)]
  [Description("Defines the current scaling factor of the text display; 0 is normal viewing.")]
  [Category("Appearance")]
  public int Zoom
  {
    get => this._ns.GetZoom();
    set => this._ns.SetZoom(value);
  }

  private void handleFileDrop(IntPtr hDrop)
  {
    StringBuilder lpszFile1 = (StringBuilder) null;
    uint num1 = NativeMethods.DragQueryFile(hDrop, uint.MaxValue, lpszFile1, 0U);
    List<string> stringList = new List<string>();
    for (uint iFile = 0; iFile < num1; ++iFile)
    {
      StringBuilder lpszFile2 = new StringBuilder(512 /*0x0200*/);
      int num2 = (int) NativeMethods.DragQueryFile(hDrop, iFile, lpszFile2, 512U /*0x0200*/);
      stringList.Add(lpszFile2.ToString());
    }
    NativeMethods.DragFinish(hDrop);
    this.OnFileDrop(new FileDropEventArgs(stringList.ToArray()));
  }

  private List<ManagedRange> managedRangesInRange(int firstPos, int lastPos)
  {
    List<ManagedRange> managedRangeList = new List<ManagedRange>();
    foreach (ManagedRange managedRange in this._managedRanges)
    {
      if (managedRange.Start >= firstPos && managedRange.Start <= lastPos)
        managedRangeList.Add(managedRange);
    }
    return managedRangeList;
  }

  private void paintRanges(Graphics g)
  {
    int firstVisibleLine = this._ns.GetFirstVisibleLine();
    int num = firstVisibleLine + this._ns.LinesOnScreen();
    int firstPos = this._ns.PositionFromLine(firstVisibleLine);
    int lastPos = this._ns.PositionFromLine(num + 1) - 1;
    if (lastPos < 0)
      lastPos = this._ns.GetLength();
    List<ManagedRange> managedRangeList = this.managedRangesInRange(firstPos, lastPos);
    g.SmoothingMode = SmoothingMode.AntiAlias;
    foreach (ManagedRange managedRange in managedRangeList)
      managedRange.Paint(g);
  }

  [Category("Behavior")]
  [Description("Occurs when the control is first loaded.")]
  public event EventHandler Load
  {
    add => this.Events.AddHandler(Scintilla._loadEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._loadEventKey, (Delegate) value);
  }

  protected virtual void OnLoad(EventArgs e)
  {
    if (!(this.Events[Scintilla._loadEventKey] is EventHandler eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Description("Occurs when the text has changed.")]
  [Category("Scintilla")]
  public event EventHandler<EventArgs> TextChanged
  {
    add => this.Events.AddHandler(Scintilla._textChangedKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._textChangedKey, (Delegate) value);
  }

  protected virtual void OnTextChanged()
  {
    if (!(this.Events[Scintilla._textChangedKey] is EventHandler<EventArgs> eventHandler))
      return;
    eventHandler((object) this, EventArgs.Empty);
  }

  [Description("Occurs when the text or styling of the document changes or is about to change.")]
  [Category("Scintilla")]
  public event EventHandler<NativeScintillaEventArgs> DocumentChange
  {
    add => this.Events.AddHandler(Scintilla._documentChangeEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._documentChangeEventKey, (Delegate) value);
  }

  protected virtual void OnDocumentChange(NativeScintillaEventArgs e)
  {
    if (!(this.Events[Scintilla._documentChangeEventKey] is EventHandler<NativeScintillaEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Description("Occurs when a user clicks on a call tip.")]
  [Category("Scintilla")]
  public event EventHandler<CallTipClickEventArgs> CallTipClick
  {
    add => this.Events.AddHandler(Scintilla._callTipClickEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._callTipClickEventKey, (Delegate) value);
  }

  internal void FireCallTipClick(int arrow)
  {
    CallTipArrow callTipArrow = (CallTipArrow) arrow;
    OverloadList overloadList = this.CallTip.OverloadList;
    CallTipClickEventArgs e;
    if (overloadList == null)
    {
      e = new CallTipClickEventArgs(callTipArrow, -1, -1, (OverloadList) null, this.CallTip.HighlightStart, this.CallTip.HighlightEnd);
    }
    else
    {
      int newIndex = overloadList.CurrentIndex;
      switch (callTipArrow)
      {
        case CallTipArrow.Up:
          if (overloadList.CurrentIndex == 0)
          {
            newIndex = overloadList.Count - 1;
            break;
          }
          --newIndex;
          break;
        case CallTipArrow.Down:
          if (overloadList.CurrentIndex == overloadList.Count - 1)
          {
            newIndex = 0;
            break;
          }
          ++newIndex;
          break;
      }
      e = new CallTipClickEventArgs(callTipArrow, overloadList.CurrentIndex, newIndex, overloadList, this.CallTip.HighlightStart, this.CallTip.HighlightEnd);
    }
    this.OnCallTipClick(e);
    if (e.Cancel)
    {
      this.CallTip.Cancel();
    }
    else
    {
      if (overloadList != null)
      {
        this.CallTip.OverloadList = e.OverloadList;
        this.CallTip.OverloadList.CurrentIndex = e.NewIndex;
        this.CallTip.ShowOverloadInternal();
      }
      this.CallTip.HighlightStart = e.HighlightStart;
      this.CallTip.HighlightEnd = e.HighlightEnd;
    }
  }

  protected virtual void OnCallTipClick(CallTipClickEventArgs e)
  {
    if (!(this.Events[Scintilla._callTipClickEventKey] is EventHandler<CallTipClickEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when the user makes a selection from the auto-complete list.")]
  public event EventHandler<AutoCompleteAcceptedEventArgs> AutoCompleteAccepted
  {
    add => this.Events.AddHandler(Scintilla._autoCompleteAcceptedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._autoCompleteAcceptedEventKey, (Delegate) value);
  }

  protected virtual void OnAutoCompleteAccepted(AutoCompleteAcceptedEventArgs e)
  {
    if (this.Events[Scintilla._autoCompleteAcceptedEventKey] is EventHandler<AutoCompleteAcceptedEventArgs> eventHandler)
      eventHandler((object) this, e);
    if (!e.Cancel)
      return;
    this.AutoComplete.Cancel();
  }

  [Category("Scintilla")]
  [Description("Occurs when text has been inserted into the document.")]
  public event EventHandler<TextModifiedEventArgs> TextInserted
  {
    add => this.Events.AddHandler(Scintilla._textInsertedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._textInsertedEventKey, (Delegate) value);
  }

  protected virtual void OnTextInserted(TextModifiedEventArgs e)
  {
    if (!(this.Events[Scintilla._textInsertedEventKey] is EventHandler<TextModifiedEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Description("Occurs when text has been removed from the document.")]
  [Category("Scintilla")]
  public event EventHandler<TextModifiedEventArgs> TextDeleted
  {
    add => this.Events.AddHandler(Scintilla._textDeletedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._textDeletedEventKey, (Delegate) value);
  }

  protected virtual void OnTextDeleted(TextModifiedEventArgs e)
  {
    if (!(this.Events[Scintilla._textDeletedEventKey] is EventHandler<TextModifiedEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Description("Occurs when text is about to be inserted into the document.")]
  [Category("Scintilla")]
  public event EventHandler<TextModifiedEventArgs> BeforeTextInsert
  {
    add => this.Events.AddHandler(Scintilla._beforeTextInsertEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._beforeTextInsertEventKey, (Delegate) value);
  }

  protected virtual void OnBeforeTextInsert(TextModifiedEventArgs e)
  {
    List<ManagedRange> managedRangeList = new List<ManagedRange>();
    foreach (ManagedRange managedRange in this._managedRanges)
    {
      if (managedRange.Start == e.Position && managedRange.PendingDeletion)
      {
        managedRange.PendingDeletion = false;
        ManagedRange lmr = managedRange;
        this.BeginInvoke((Delegate) (() => lmr.Change(e.Position, e.Position + e.Length)));
      }
      if (managedRange.IsPoint)
      {
        if (managedRange.Start >= e.Position)
          managedRange.Change(managedRange.Start + e.Length, managedRange.End + e.Length);
        else if (managedRange.End >= e.Position)
          managedRange.Change(managedRange.Start, managedRange.End + e.Length);
      }
      else if (managedRange.Start > e.Position)
        managedRange.Change(managedRange.Start + e.Length, managedRange.End + e.Length);
      else if (managedRange.End >= e.Position)
        managedRange.Change(managedRange.Start, managedRange.End + e.Length);
    }
    if (!(this.Events[Scintilla._beforeTextInsertEventKey] is EventHandler<TextModifiedEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when text is about to be removed from the document.")]
  public event EventHandler<TextModifiedEventArgs> BeforeTextDelete
  {
    add => this.Events.AddHandler(Scintilla._beforeTextDeleteEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._beforeTextDeleteEventKey, (Delegate) value);
  }

  protected virtual void OnBeforeTextDelete(TextModifiedEventArgs e)
  {
    int position = e.Position;
    int num = position + e.Length;
    List<ManagedRange> managedRangeList = new List<ManagedRange>();
    foreach (ManagedRange managedRange in this._managedRanges)
    {
      if (managedRange.Start >= position && managedRange.End <= num)
      {
        if (!managedRange.IsPoint && e.Position == managedRange.Start && e.Position + e.Length == managedRange.End)
        {
          managedRange.Change(managedRange.Start, managedRange.Start);
        }
        else
        {
          managedRange.Change(-1, -1);
          managedRangeList.Add(managedRange);
        }
      }
      else if (managedRange.Start >= num)
        managedRange.Change(managedRange.Start - e.Length, managedRange.End - e.Length);
      else if (managedRange.Start >= position && managedRange.Start <= num)
        managedRange.Change(position, managedRange.End - e.Length);
      else if (managedRange.Start < position && managedRange.End >= position && managedRange.End >= num)
        managedRange.Change(managedRange.Start, managedRange.End - e.Length);
      else if (managedRange.Start < position && managedRange.End >= position && managedRange.End < num)
        managedRange.Change(managedRange.Start, position);
    }
    foreach (ScintillaHelperBase scintillaHelperBase in managedRangeList)
      scintillaHelperBase.Dispose();
    if (!(this.Events[Scintilla._beforeTextDeleteEventKey] is EventHandler<TextModifiedEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Description("Occurs when a folding change has occurred.")]
  [Category("Scintilla")]
  public event EventHandler<FoldChangedEventArgs> FoldChanged
  {
    add => this.Events.AddHandler(Scintilla._foldChangedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._foldChangedEventKey, (Delegate) value);
  }

  protected virtual void OnFoldChanged(FoldChangedEventArgs e)
  {
    if (!(this.Events[Scintilla._foldChangedEventKey] is EventHandler<FoldChangedEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Description("Occurs when one or more markers has changed in a line of text.")]
  [Category("Scintilla")]
  public event EventHandler<MarkerChangedEventArgs> MarkerChanged
  {
    add => this.Events.AddHandler(Scintilla._markerChangedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._markerChangedEventKey, (Delegate) value);
  }

  protected virtual void OnMarkerChanged(MarkerChangedEventArgs e)
  {
    if (!(this.Events[Scintilla._markerChangedEventKey] is EventHandler<MarkerChangedEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when the a clicks or releases the mouse on text that has an indicator.")]
  public event EventHandler<ScintillaMouseEventArgs> IndicatorClick
  {
    add => this.Events.AddHandler(Scintilla._indicatorClickEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._indicatorClickEventKey, (Delegate) value);
  }

  protected virtual void OnIndicatorClick(ScintillaMouseEventArgs e)
  {
    if (!(this.Events[Scintilla._indicatorClickEventKey] is EventHandler<ScintillaMouseEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when the mouse was clicked inside a margin that was marked as sensitive.")]
  public event EventHandler<MarginClickEventArgs> MarginClick
  {
    add => this.Events.AddHandler(Scintilla._marginClickEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._marginClickEventKey, (Delegate) value);
  }

  protected virtual void OnMarginClick(MarginClickEventArgs e)
  {
    if (this.Events[Scintilla._marginClickEventKey] is EventHandler<MarginClickEventArgs> eventHandler)
      eventHandler((object) this, e);
    if (e.ToggleMarkerNumber >= 0)
    {
      int num = (int) Math.Pow(2.0, (double) e.ToggleMarkerNumber);
      if ((e.Line.GetMarkerMask() & num) == num)
        e.Line.DeleteMarker(e.ToggleMarkerNumber);
      else
        e.Line.AddMarker(e.ToggleMarkerNumber);
    }
    if (!e.ToggleFold)
      return;
    e.Line.ToggleFoldExpanded();
  }

  internal void FireMarginClick(SCNotification n)
  {
    ScintillaNet.Margin margin = this.Margins[n.margin];
    Keys modifiers = Keys.None;
    if ((n.modifiers & 4) == 4)
      modifiers |= Keys.Alt;
    if ((n.modifiers & 2) == 2)
      modifiers |= Keys.Control;
    if ((n.modifiers & 1) == 1)
      modifiers |= Keys.Shift;
    this.OnMarginClick(new MarginClickEventArgs(modifiers, n.position, this.Lines.FromPosition(n.position), margin, margin.AutoToggleMarkerNumber, margin.IsFoldMargin));
  }

  [Description("Occurs when the control is about to display or print text that requires styling.")]
  [Category("Scintilla")]
  public event EventHandler<StyleNeededEventArgs> StyleNeeded
  {
    add => this.Events.AddHandler(Scintilla._styleNeededEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._styleNeededEventKey, (Delegate) value);
  }

  protected virtual void OnStyleNeeded(StyleNeededEventArgs e)
  {
    if (!(this.Events[Scintilla._styleNeededEventKey] is EventHandler<StyleNeededEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when the user types a text character.")]
  public event EventHandler<CharAddedEventArgs> CharAdded
  {
    add => this.Events.AddHandler(Scintilla._charAddedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._charAddedEventKey, (Delegate) value);
  }

  protected virtual void OnCharAdded(CharAddedEventArgs e)
  {
    if (this.Events[Scintilla._charAddedEventKey] is EventHandler<CharAddedEventArgs> eventHandler)
      eventHandler((object) this, e);
    if (this._indentation.SmartIndentType == SmartIndent.None)
      return;
    this._indentation.CheckSmartIndent(e.Ch);
  }

  [Description("Occurs when the value of the Modified property changes.")]
  [Category("Property Changed")]
  public event EventHandler ModifiedChanged
  {
    add => this.Events.AddHandler(Scintilla._modifiedChangedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._modifiedChangedEventKey, (Delegate) value);
  }

  protected virtual void OnModifiedChanged(EventArgs e)
  {
    if (!(this.Events[Scintilla._modifiedChangedEventKey] is EventHandler eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when a user tries to modifiy text when in read-only mode.")]
  public event EventHandler ReadOnlyModifyAttempt
  {
    add => this.Events.AddHandler(Scintilla._readOnlyModifyAttemptEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._readOnlyModifyAttemptEventKey, (Delegate) value);
  }

  protected virtual void OnReadOnlyModifyAttempt(EventArgs e)
  {
    if (!(this.Events[Scintilla._readOnlyModifyAttemptEventKey] is EventHandler eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when the selection has changed.")]
  public event EventHandler SelectionChanged
  {
    add => this.Events.AddHandler(Scintilla._selectionChangedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._selectionChangedEventKey, (Delegate) value);
  }

  protected virtual void OnSelectionChanged(EventArgs e)
  {
    if (this.Events[Scintilla._selectionChangedEventKey] is EventHandler eventHandler)
      eventHandler((object) this, e);
    if (!this._isBraceMatching || this._selection.Length != 0)
      return;
    int num = this.CurrentPos - 1;
    int pos1 = -1;
    int pos2 = -1;
    switch (this.CharAt(num))
    {
      case '(':
      case ')':
      case '[':
      case ']':
      case '{':
      case '}':
        if (!this.PositionIsOnComment(num))
        {
          pos1 = num;
          pos2 = this._ns.BraceMatch(num, 0);
          break;
        }
        break;
      default:
        this.CharAt(this.CurrentPos);
        break;
    }
    this._ns.BraceHighlight(pos1, pos2);
  }

  [Category("Scintilla")]
  [Description("Occurs when a range of lines that is currently invisible should be made visible.")]
  public event EventHandler<LinesNeedShownEventArgs> LinesNeedShown
  {
    add => this.Events.AddHandler(Scintilla._linesNeedShownEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._linesNeedShownEventKey, (Delegate) value);
  }

  protected virtual void OnLinesNeedShown(LinesNeedShownEventArgs e)
  {
    if (!(this.Events[Scintilla._linesNeedShownEventKey] is EventHandler<LinesNeedShownEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when the user hovers the mouse (dwells) in one position for the dwell period.")]
  public event EventHandler<ScintillaMouseEventArgs> DwellStart
  {
    add => this.Events.AddHandler(Scintilla._dwellStartEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._dwellStartEventKey, (Delegate) value);
  }

  protected virtual void OnDwellStart(ScintillaMouseEventArgs e)
  {
    if (!(this.Events[Scintilla._dwellStartEventKey] is EventHandler<ScintillaMouseEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when a dwell (hover) activity has ended.")]
  public event EventHandler<ScintillaMouseEventArgs> DwellEnd
  {
    add => this.Events.AddHandler(Scintilla._dwellEndEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._dwellEndEventKey, (Delegate) value);
  }

  protected virtual void OnDwellEnd(ScintillaMouseEventArgs e)
  {
    if (!(this.Events[Scintilla._dwellEndEventKey] is EventHandler<ScintillaMouseEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when the user zooms the display using the keyboard or the Zoom property is set.")]
  public event EventHandler ZoomChanged
  {
    add => this.Events.AddHandler(Scintilla._zoomChangedEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._zoomChangedEventKey, (Delegate) value);
  }

  protected virtual void OnZoomChanged(EventArgs e)
  {
    if (!(this.Events[Scintilla._zoomChangedEventKey] is EventHandler eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Description("Occurs when a user clicks on text with the hotspot style.")]
  [Category("Scintilla")]
  public event EventHandler<ScintillaMouseEventArgs> HotspotClick
  {
    add => this.Events.AddHandler(Scintilla._hotSpotClickEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._hotSpotClickEventKey, (Delegate) value);
  }

  protected virtual void OnHotspotClick(ScintillaMouseEventArgs e)
  {
    if (!(this.Events[Scintilla._hotSpotClickEventKey] is EventHandler<ScintillaMouseEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Description("Occurs when a user double-clicks on text with the hotspot style.")]
  [Category("Scintilla")]
  public event EventHandler<ScintillaMouseEventArgs> HotspotDoubleClick
  {
    add => this.Events.AddHandler(Scintilla._hotSpotDoubleClickEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._hotSpotDoubleClickEventKey, (Delegate) value);
  }

  protected virtual void OnHotspotDoubleClick(ScintillaMouseEventArgs e)
  {
    if (!(this.Events[Scintilla._hotSpotDoubleClickEventKey] is EventHandler<ScintillaMouseEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs when a DropMarker is about to be collected.")]
  public event EventHandler<DropMarkerCollectEventArgs> DropMarkerCollect
  {
    add => this.Events.AddHandler(Scintilla._dropMarkerCollectEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._dropMarkerCollectEventKey, (Delegate) value);
  }

  protected internal virtual void OnDropMarkerCollect(DropMarkerCollectEventArgs e)
  {
    if (!(this.Events[Scintilla._dropMarkerCollectEventKey] is EventHandler<DropMarkerCollectEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Action")]
  [Description("Occurs when the control is scrolled.")]
  public event EventHandler<ScrollEventArgs> Scroll
  {
    add => this.Events.AddHandler(Scintilla._scrollEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._scrollEventKey, (Delegate) value);
  }

  internal void FireScroll(ref Message m)
  {
    ScrollEventType type = (ScrollEventType) Utilities.SignedLoWord(m.WParam);
    ScrollOrientation scroll;
    int oldValue;
    int newValue;
    if (m.Msg == 276)
    {
      scroll = ScrollOrientation.HorizontalScroll;
      oldValue = this._ns.GetXOffset();
      base.WndProc(ref m);
      newValue = this._ns.GetXOffset();
    }
    else
    {
      scroll = ScrollOrientation.VerticalScroll;
      oldValue = this._ns.GetFirstVisibleLine();
      base.WndProc(ref m);
      newValue = this._ns.GetFirstVisibleLine();
    }
    this.OnScroll(new ScrollEventArgs(type, oldValue, newValue, scroll));
  }

  protected virtual void OnScroll(ScrollEventArgs e)
  {
    if (!(this.Events[Scintilla._scrollEventKey] is EventHandler<ScrollEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Category("Scintilla")]
  [Description("Occurs each time a recordable change occurs.")]
  public event EventHandler<MacroRecordEventArgs> MacroRecord
  {
    add => this.Events.AddHandler(Scintilla._macroRecordEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._macroRecordEventKey, (Delegate) value);
  }

  protected virtual void OnMacroRecord(MacroRecordEventArgs e)
  {
    if (!(this.Events[Scintilla._macroRecordEventKey] is EventHandler<MacroRecordEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  [Description("Occurs when a user drops a file on the control.")]
  [Category("Scintilla")]
  public event EventHandler<FileDropEventArgs> FileDrop
  {
    add => this.Events.AddHandler(Scintilla._fileDropEventKey, (Delegate) value);
    remove => this.Events.RemoveHandler(Scintilla._fileDropEventKey, (Delegate) value);
  }

  protected virtual void OnFileDrop(FileDropEventArgs e)
  {
    if (!(this.Events[Scintilla._fileDropEventKey] is EventHandler<FileDropEventArgs> eventHandler))
      return;
    eventHandler((object) this, e);
  }

  public Range AppendText(string text)
  {
    int textLength = this.TextLength;
    this.NativeInterface.AppendText(text.Length, text);
    return this.GetRange(textLength, this.TextLength);
  }

  public Range InsertText(string text)
  {
    this.NativeInterface.AddText(text.Length, text);
    return this.GetRange(this._caret.Position, text.Length);
  }

  public Range InsertText(int position, string text)
  {
    this.NativeInterface.InsertText(position, text);
    return this.GetRange(position, text.Length);
  }

  public char CharAt(int position) => this._ns.GetCharAt(position);

  public Range GetRange(int startPosition, int endPosition)
  {
    return new Range(startPosition, endPosition, this);
  }

  public Range GetRange(int position) => new Range(position, position + 1, this);

  public Range GetRange() => new Range(0, this._ns.GetTextLength(), this);

  public int GetColumn(int position) => this._ns.GetColumn(position);

  public int FindColumn(int line, int column) => this._ns.FindColumn(line, column);

  public int PositionFromPoint(int x, int y) => this._ns.PositionFromPoint(x, y);

  public int PositionFromPointClose(int x, int y) => this._ns.PositionFromPointClose(x, y);

  public int PointXFromPosition(int position) => this._ns.PointXFromPosition(position);

  public int PointYFromPosition(int position) => this._ns.PointYFromPosition(position);

  public void ZoomIn() => this._ns.ZoomIn();

  private void ZoomOut() => this._ns.ZoomOut();

  public bool PositionIsOnComment(int position)
  {
    return this.PositionIsOnComment(position, this._lexing.Lexer);
  }

  public bool PositionIsOnComment(int position, Lexer lexer)
  {
    int styleAt = (int) this._styles.GetStyleAt(position);
    return (lexer == Lexer.Python || lexer == Lexer.Lisp) && styleAt == 1 || styleAt == 12 || (lexer == Lexer.Cpp || lexer == Lexer.Pascal || lexer == Lexer.Tcl || lexer == Lexer.Bullant) && styleAt == 1 || styleAt == 2 || styleAt == 3 || styleAt == 15 || styleAt == 17 || styleAt == 18 || (lexer == Lexer.Hypertext || lexer == Lexer.Xml) && styleAt == 9 || styleAt == 20 || styleAt == 29 || styleAt == 30 || styleAt == 42 || styleAt == 43 || styleAt == 44 || styleAt == 57 || styleAt == 58 || styleAt == 59 || styleAt == 72 || styleAt == 82 || styleAt == 92 || styleAt == 107 || styleAt == 124 || styleAt == 125 || (lexer == Lexer.Perl || lexer == Lexer.Ruby || lexer == Lexer.Clw || lexer == Lexer.Bash) && styleAt == 2 || lexer == Lexer.Sql && styleAt == 1 || styleAt == 2 || styleAt == 3 || styleAt == 13 || styleAt == 15 || styleAt == 17 || styleAt == 18 || (lexer == Lexer.VB || lexer == Lexer.Properties || lexer == Lexer.MakeFile || lexer == Lexer.Batch || lexer == Lexer.Diff || lexer == Lexer.Conf || lexer == Lexer.Ave || lexer == Lexer.Eiffel || lexer == Lexer.EiffelKw || lexer == Lexer.Tcl || lexer == Lexer.VBScript || lexer == Lexer.MatLab || lexer == Lexer.Fortran || lexer == Lexer.F77 || lexer == Lexer.Lout || lexer == Lexer.Mmixal || lexer == Lexer.Yaml || lexer == Lexer.PowerBasic || lexer == Lexer.ErLang || lexer == Lexer.Octave || lexer == Lexer.Kix || lexer == Lexer.Asn1) && styleAt == 1 || lexer == Lexer.Latex && styleAt == 4 || (lexer == Lexer.Lua || lexer == Lexer.EScript || lexer == Lexer.Verilog) && styleAt == 1 || styleAt == 2 || styleAt == 3 || lexer == Lexer.Ada && styleAt == 10 || (lexer == Lexer.Baan || lexer == Lexer.Pov || lexer == Lexer.Ps || lexer == Lexer.Forth || lexer == Lexer.MsSql || lexer == Lexer.Gui4Cli || lexer == Lexer.Au3 || lexer == Lexer.Apdl || lexer == Lexer.Vhdl || lexer == Lexer.Rebol) && styleAt == 1 || styleAt == 2 || lexer == Lexer.Asm && styleAt == 1 || styleAt == 11 || lexer == Lexer.Nsis && styleAt == 1 || styleAt == 18 || lexer == Lexer.Specman && styleAt == 2 || styleAt == 3 || lexer == Lexer.Tads3 && styleAt == 3 || styleAt == 4 || lexer == Lexer.CSound && styleAt == 1 || styleAt == 9 || lexer == Lexer.Caml && styleAt == 12 || styleAt == 13 || styleAt == 14 || styleAt == 15 || lexer == Lexer.Haskell && styleAt == 13 || styleAt == 14 || styleAt == 15 || styleAt == 16 /*0x10*/ || lexer == Lexer.Flagship && styleAt == 1 || styleAt == 2 || styleAt == 3 || styleAt == 4 || styleAt == 5 || styleAt == 6 || lexer == Lexer.Smalltalk && styleAt == 3 || lexer == Lexer.Css && styleAt == 9;
  }

  public void AddLastLineEnd()
  {
    EndOfLineMode mode = this._endOfLine.Mode;
    string text = "\r\n";
    switch (mode)
    {
      case EndOfLineMode.CR:
        text = "\r";
        break;
      case EndOfLineMode.LF:
        text = "\n";
        break;
    }
    int startPosition = this.TextLength - text.Length;
    if (startPosition >= 0 && !(this.GetRange(startPosition, startPosition + text.Length).Text != text))
      return;
    this.AppendText(text);
  }

  public string GetWordFromPosition(int position)
  {
    return this.GetRange(this.NativeInterface.WordStartPosition(position, true), this.NativeInterface.WordEndPosition(position, true)).Text;
  }

  internal bool IsDesignMode => this.DesignMode;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    protected internal List<TopLevelHelper> Helpers
  {
    get => this._helpers;
    set => this._helpers = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  internal bool IsInitializing
  {
    get => this._isInitializing;
    set => this._isInitializing = value;
  }

  public void BeginInit() => this._isInitializing = true;

  public void EndInit()
  {
    this._isInitializing = false;
    foreach (ScintillaHelperBase helper in this._helpers)
      helper.Initialize();
  }

  private class LastSelection
  {
    private int start;
    private int end;
    private int length;

    public int Start
    {
      get => this.start;
      set => this.start = value;
    }

    public int End
    {
      get => this.end;
      set => this.end = value;
    }

    public int Length
    {
      get => this.length;
      set => this.length = value;
    }
  }
}
