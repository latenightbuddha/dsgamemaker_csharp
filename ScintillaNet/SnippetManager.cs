using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class SnippetManager : TopLevelHelper
{
  private SnippetChooser _snipperChooser;
  private System.Timers.Timer _snippetLinkTimer = new System.Timers.Timer();
  private bool _pendingUndo;
  private readonly Regex snippetRegex1 = new Regex(string.Format("(?<dm>{0}DropMarker(?<dmi>\\[[0-9]*\\])?{0})|(?<c>{0}caret{0})|(?<a>{0}anchor{0})|(?<e>{0}end{0})|(?<s>{0}selected{0})|(?<l>{0}.+?(?<li>\\[[0-9]*\\])?{0})", (object) '\u0001'), RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
  private SnippetLinkCollection _snippetLinks = new SnippetLinkCollection();
  private bool _isOneKeySelectionEmbedEnabled;
  private Color _activeSnippetColor = Color.Lime;
  private int _activeSnippetIndicator = 15;
  private IndicatorStyle _activeSnippetIndicatorStyle = IndicatorStyle.RoundBox;
  private char _defaultDelimeter = '$';
  private bool _isEnabled = true;
  private Color _inactiveSnippetColor = Color.Lime;
  private int _inactiveSnippetIndicator = 16 /*0x10*/;
  private IndicatorStyle _inactiveSnippetIndicatorStyle = IndicatorStyle.Box;
  private SnippetList _list;

  public SnippetManager(Scintilla scintilla)
    : base(scintilla)
  {
    this._list = new SnippetList(this);
    this._snippetLinkTimer.Interval = 1;
    this._snippetLinkTimer.Elapsed += new System.Timers.ElapsedEventHandler(this._snippetLinkTimer_Tick);
    this.IsEnabled = this._isEnabled;
  }

  internal void SetIndicators()
  {
    this.Scintilla.Indicators[this._activeSnippetIndicator].Style = this._activeSnippetIndicatorStyle;
    this.Scintilla.Indicators[this._activeSnippetIndicator].Color = this._activeSnippetColor;
    this.Scintilla.Indicators[this._inactiveSnippetIndicator].Style = this._inactiveSnippetIndicatorStyle;
    this.Scintilla.Indicators[this._inactiveSnippetIndicator].Color = this._inactiveSnippetColor;
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeActiveSnippetColor() || this.ShouldSerializeActiveSnippetIndicator() || this.ShouldSerializeActiveSnippetIndicatorStyle() || this.ShouldSerializeInactiveSnippetColor() || this.ShouldSerializeInactiveSnippetIndicator() || this.ShouldSerializeInactiveSnippetIndicatorStyle() || this.ShouldSerializeIsOneKeySelectionEmbedEnabled() || this.ShouldSerializeIsEnabled() || this.ShouldSerializeDefaultDelimeter();
  }

  public bool NextSnippetRange()
  {
    if (!this._snippetLinks.IsActive || this.Scintilla.AutoComplete.IsActive)
      return false;
    SnippetLink activeSnippetLink = this._snippetLinks.NextActiveSnippetLink;
    if (activeSnippetLink == null)
      return false;
    while (activeSnippetLink.Ranges.Count == 0)
    {
      this._snippetLinks.Remove(activeSnippetLink);
      activeSnippetLink = this._snippetLinks.NextActiveSnippetLink;
      if (activeSnippetLink == null)
      {
        this.Scintilla.Commands.StopProcessingCommands = true;
        return true;
      }
    }
    activeSnippetLink.Ranges[0].Select();
    this.Scintilla.Commands.StopProcessingCommands = true;
    return true;
  }

  public bool PreviousSnippetRange()
  {
    if (!this._snippetLinks.IsActive || this.Scintilla.AutoComplete.IsActive)
      return false;
    SnippetLink activeSnippetLink = this._snippetLinks.PreviousActiveSnippetLink;
    if (activeSnippetLink == null)
      return false;
    while (activeSnippetLink.Ranges.Count == 0)
    {
      this._snippetLinks.Remove(activeSnippetLink);
      activeSnippetLink = this._snippetLinks.PreviousActiveSnippetLink;
      if (activeSnippetLink == null)
      {
        this.Scintilla.Commands.StopProcessingCommands = true;
        return true;
      }
    }
    activeSnippetLink.Ranges[0].Select();
    this.Scintilla.Commands.StopProcessingCommands = true;
    return true;
  }

  public bool CancelActiveSnippets()
  {
    if (!this._snippetLinks.IsActive || this.Scintilla.AutoComplete.IsActive)
      return false;
    this.IsActive = false;
    this.Scintilla.Commands.StopProcessingCommands = true;
    return true;
  }

  public bool AcceptActiveSnippets()
  {
    if (this._snippetLinks.IsActive && !this.Scintilla.AutoComplete.IsActive)
    {
      int position = this.Scintilla.Caret.Position;
      bool flag = false;
      foreach (SnippetLink snippetLink in (IEnumerable<SnippetLink>) this._snippetLinks.Values)
      {
        foreach (Range range in snippetLink.Ranges)
        {
          if (range.PositionInRange(position))
          {
            flag = true;
            break;
          }
        }
        if (flag)
          break;
      }
      if (flag)
      {
        this.cascadeSnippetLinkRangeChange(this._snippetLinks.ActiveSnippetLink, this._snippetLinks.ActiveRange);
        if (this._snippetLinks.EndPoint != null)
          this.Scintilla.Caret.Goto(this._snippetLinks.EndPoint.Start);
        this.IsActive = false;
        this.Scintilla.Commands.StopProcessingCommands = true;
        return true;
      }
    }
    return false;
  }

  public bool DoSnippetCheck()
  {
    if (!this._isEnabled || this._snippetLinks.IsActive || this.Scintilla.AutoComplete.IsActive || this.Scintilla.Selection.Length > 0)
      return false;
    int currentPos = this.Scintilla.NativeInterface.GetCurrentPos();
    if (currentPos <= 0 || this.Scintilla.NativeInterface.GetCharAt(currentPos - 1).ToString().IndexOfAny(this.Scintilla.Lexing.WhitespaceCharsArr) >= 0)
      return false;
    switch ((int) this.Scintilla.NativeInterface.GetStyleAt(currentPos - 1) & 31 /*0x1F*/)
    {
      case 1:
      case 2:
      case 4:
        return false;
      default:
        int num = this.Scintilla.NativeInterface.WordStartPosition(currentPos, true);
        Snippet snippet;
        if (!this._list.TryGetValue(this.Scintilla.GetRange(num, currentPos).Text, out snippet))
          return false;
        this.InsertSnippet(snippet, num);
        this.Scintilla.Commands.StopProcessingCommands = true;
        return true;
    }
  }

  public void InsertSnippet(string shortcut)
  {
    Snippet snippet;
    if (!this._list.TryGetValue(shortcut, out snippet))
      return;
    this.InsertSnippet(snippet, Math.Min(this.NativeScintilla.GetCurrentPos(), this.NativeScintilla.GetAnchor()));
  }

  public void InsertSnippet(Snippet snip)
  {
    this.InsertSnippet(snip, Math.Min(this.NativeScintilla.GetCurrentPos(), this.NativeScintilla.GetAnchor()));
  }

  internal void InsertSnippet(Snippet snip, int startPos)
  {
    this.NativeScintilla.BeginUndoAction();
    this.IsActive = false;
    string input = snip.RealCode;
    int num1 = 0;
    string text = this.Scintilla.Lines.Current.Text;
    if (text != string.Empty)
    {
      for (; num1 < text.Length; ++num1)
      {
        switch (text[num1])
        {
          case '\t':
          case ' ':
            continue;
          default:
            goto label_4;
        }
      }
    }
label_4:
    string str1 = this.Scintilla.Selection.Text;
    if (str1 != string.Empty)
      this.Scintilla.Selection.Clear();
    if (num1 > 0)
    {
      string str2 = text.Substring(0, num1);
      input = input.Replace(Environment.NewLine, this.Scintilla.EndOfLine.EolString + str2);
      str1 = str1.Replace(Environment.NewLine, this.Scintilla.EndOfLine.EolString + str2);
    }
    int num2 = -1;
    int num3 = -1;
    int num4 = -1;
    SortedList<int, int> sortedList1 = new SortedList<int, int>();
    SortedList<int, SnippetLinkRange> sortedList2 = new SortedList<int, SnippetLinkRange>();
    System.Collections.Generic.List<SnippetLinkRange> snippetLinkRangeList = new System.Collections.Generic.List<SnippetLinkRange>();
    for (Match match = this.snippetRegex1.Match(input); match.Success; match = this.snippetRegex1.Match(input))
    {
      if (match.Groups["dm"].Success)
      {
        if (match.Groups["dmi"].Success)
          sortedList1[int.Parse(match.Groups["dmi"].Value)] = match.Groups["dm"].Index;
        else
          sortedList1[sortedList1.Count] = match.Groups["dm"].Index;
        input = input.Remove(match.Groups["dm"].Index, match.Groups["dm"].Length);
      }
      else if (match.Groups["c"].Success)
      {
        num3 = match.Groups["c"].Index;
        input = input.Remove(match.Groups["c"].Index, match.Groups["c"].Length);
      }
      else if (match.Groups["a"].Success)
      {
        num2 = match.Groups["a"].Index;
        input = input.Remove(match.Groups["a"].Index, match.Groups["a"].Length);
      }
      else if (match.Groups["e"].Success)
      {
        num4 = match.Groups["e"].Index;
        input = input.Remove(match.Groups["e"].Index, match.Groups["e"].Length);
      }
      else if (match.Groups["s"].Success)
      {
        int index = match.Groups["s"].Index;
        input = input.Remove(match.Groups["s"].Index, match.Groups["s"].Length).Insert(match.Groups["s"].Index, str1);
      }
      else if (match.Groups["l"].Success)
      {
        Group group1 = match.Groups["l"];
        if (match.Groups["li"].Success)
        {
          Group group2 = match.Groups["li"];
          string key1 = group1.Value.Substring(1, group1.Value.Length - group2.Length - 2);
          int key2 = int.Parse(group2.Value.Substring(1, group2.Value.Length - 2));
          int start = startPos + group1.Index;
          int end = start + group1.Length - group2.Length - 2;
          sortedList2[key2] = new SnippetLinkRange(start, end, this.Scintilla, key1);
          input = input.Remove(group1.Index, 1).Remove(group1.Index - 2 + group1.Length - group2.Length, group2.Length + 1);
        }
        else
        {
          string key = group1.Value.Substring(1, group1.Value.Length - 2);
          int start = startPos + group1.Index;
          int end = start + group1.Length - 2;
          snippetLinkRangeList.Add(new SnippetLinkRange(start, end, this.Scintilla, key));
          input = input.Remove(group1.Index, 1).Remove(group1.Index + group1.Length - 2, 1);
        }
      }
    }
    this.Scintilla.GetRange(startPos, this.NativeScintilla.GetCurrentPos()).Text = input;
    SnippetLinkRange[] snippetLinkRangeArray = new SnippetLinkRange[sortedList2.Count + snippetLinkRangeList.Count];
    for (int key = 0; key < sortedList2.Values.Count; ++key)
      snippetLinkRangeArray[key] = sortedList2[key];
    for (int index = 0; index < snippetLinkRangeList.Count; ++index)
      snippetLinkRangeArray[index + sortedList2.Count] = snippetLinkRangeList[index];
    foreach (SnippetLinkRange range in snippetLinkRangeArray)
      this.addSnippetLink(range);
    foreach (SnippetLinkRange snippetLinkRange in snippetLinkRangeArray)
      snippetLinkRange.Init();
    if (this._snippetLinks.Count > 0)
    {
            System.Timers.Timer t = new System.Timers.Timer();
      t.Interval = 10;
      t.Elapsed += (System.Timers.ElapsedEventHandler) ((sender, te) =>
      {
        t.Dispose();
        this.IsActive = true;
      });
      t.Start();
    }
    for (int index = sortedList1.Count - 1; index >= 0; --index)
      this.Scintilla.DropMarkers.Drop(startPos + sortedList1.Values[index]);
    if (num3 >= 0)
      this.Scintilla.Caret.Goto(startPos + num3);
    else
      this.Scintilla.Caret.Goto(startPos + input.Length);
    if (num2 >= 0)
      this.Scintilla.Caret.Anchor = startPos + num2;
    if (num4 >= 0)
    {
      if (snippetLinkRangeArray.Length > 0)
      {
        SnippetLinkEnd snippetLinkEnd = new SnippetLinkEnd(num4 + startPos, this.Scintilla);
        this.Scintilla.ManagedRanges.Add((ManagedRange) snippetLinkEnd);
        this._snippetLinks.EndPoint = snippetLinkEnd;
      }
      else
        this.Scintilla.Caret.Goto(num4 + startPos);
    }
    this.NativeScintilla.EndUndoAction();
  }

  private void _snippetLinkTimer_Tick(object sender, EventArgs e)
  {
    this._snippetLinkTimer.Enabled = false;
    Range range1 = this.Scintilla.Selection.Range;
    if (!this._snippetLinks.IsActive)
      return;
    SnippetLink oldActiveSnippetLink = this._snippetLinks.ActiveSnippetLink;
    SnippetLinkRange oldActiveRange = this._snippetLinks.ActiveRange;
    if (oldActiveRange == null || !oldActiveRange.IntersectsWith(range1) && !oldActiveRange.Equals((object) range1))
      return;
    this.Scintilla.BeginInvoke((Delegate) (() =>
    {
      this.cascadeSnippetLinkRangeChange(oldActiveSnippetLink, oldActiveRange);
      foreach (SnippetLink snippetLink in (IEnumerable<SnippetLink>) this._snippetLinks.Values)
      {
        foreach (Range range2 in snippetLink.Ranges)
        {
          if (snippetLink == this._snippetLinks.ActiveSnippetLink)
          {
            range2.ClearIndicator(this.Scintilla.Snippets.InactiveSnippetIndicator);
            range2.SetIndicator(this.Scintilla.Snippets.ActiveSnippetIndicator);
          }
          else
          {
            range2.SetIndicator(this.Scintilla.Snippets.InactiveSnippetIndicator);
            range2.ClearIndicator(this.Scintilla.Snippets.ActiveSnippetIndicator);
          }
        }
      }
      if (this._pendingUndo)
      {
        this._pendingUndo = false;
        this.Scintilla.UndoRedo.EndUndoAction();
      }
      this.Scintilla.NativeInterface.Colourise(0, -1);
    }));
  }

  private void Scintilla_TextInserted(object sender, TextModifiedEventArgs e)
  {
    if (!this._snippetLinks.IsActive || !e.UndoRedoFlags.IsUndo && !e.UndoRedoFlags.IsRedo)
      return;
    this.Scintilla.NativeInterface.Colourise(0, -1);
  }

  private void Scintilla_BeforeTextInsert(object sender, TextModifiedEventArgs e)
  {
    if (!this._snippetLinks.IsActive || this._pendingUndo || e.UndoRedoFlags.IsUndo || e.UndoRedoFlags.IsRedo)
      return;
    this._pendingUndo = true;
    this.Scintilla.UndoRedo.BeginUndoAction();
    this._snippetLinkTimer.Enabled = true;
  }

  private void Scintilla_BeforeTextDelete(object sender, TextModifiedEventArgs e)
  {
    if (!this._isEnabled)
      return;
    if (this._snippetLinks.IsActive && !this._pendingUndo && !e.UndoRedoFlags.IsUndo && !e.UndoRedoFlags.IsRedo)
    {
      this._pendingUndo = true;
      this.Scintilla.UndoRedo.BeginUndoAction();
      this._snippetLinkTimer.Enabled = true;
    }
    ManagedRange managedRange1 = (ManagedRange) null;
    if (e.UndoRedoFlags.IsUndo && this._snippetLinks.IsActive)
    {
      foreach (ManagedRange managedRange2 in this.Scintilla.ManagedRanges)
      {
        if (managedRange2.Start == e.Position && managedRange2.Length == e.Length && managedRange2.Length > 1)
        {
          managedRange1 = managedRange2;
          ++managedRange2.End;
        }
      }
    }
    if (this._snippetLinks.IsActive && this._snippetLinks.EndPoint != null && this._snippetLinks.EndPoint.Scintilla == null)
    {
      SnippetLinkEnd snippetLinkEnd = new SnippetLinkEnd(e.Position, this.Scintilla);
      this.Scintilla.ManagedRanges.Add((ManagedRange) snippetLinkEnd);
      this._snippetLinks.EndPoint = snippetLinkEnd;
    }
    if (managedRange1 != null)
      managedRange1.End = managedRange1.Start;
    bool flag = true;
    foreach (SnippetLink snippetLink in (IEnumerable<SnippetLink>) this._snippetLinks.Values)
    {
      if (snippetLink.Ranges.Count > 0)
      {
        foreach (ScintillaHelperBase range in snippetLink.Ranges)
        {
          if (range.Scintilla != null)
          {
            flag = false;
            break;
          }
        }
      }
      if (!flag)
        break;
    }
    if (!flag || !this.IsActive)
      return;
    this.IsActive = false;
  }

  private void Scintilla_SelectionChanged(object sender, EventArgs e)
  {
    Range range1 = this.Scintilla.Selection.Range;
    if (!this._snippetLinks.IsActive)
      return;
    SnippetLink activeSnippetLink = this._snippetLinks.ActiveSnippetLink;
    SnippetLinkRange activeRange = this._snippetLinks.ActiveRange;
    this._snippetLinks.ActiveSnippetLink = (SnippetLink) null;
    this._snippetLinks.ActiveRange = (SnippetLinkRange) null;
    for (int index = 0; index < this._snippetLinks.Count; ++index)
    {
      SnippetLink snippetLink = this._snippetLinks[index];
      foreach (SnippetLinkRange range2 in snippetLink.Ranges)
      {
        if (range2.IntersectsWith(range1))
        {
          this._snippetLinks.ActiveSnippetLink = snippetLink;
          this._snippetLinks.ActiveRange = range2;
          break;
        }
      }
      if (this._snippetLinks.ActiveRange != null)
        break;
    }
    foreach (SnippetLink snippetLink in (IEnumerable<SnippetLink>) this._snippetLinks.Values)
    {
      foreach (Range range3 in snippetLink.Ranges)
      {
        if (snippetLink == this._snippetLinks.ActiveSnippetLink)
        {
          range3.ClearIndicator(this.Scintilla.Snippets.InactiveSnippetIndicator);
          range3.SetIndicator(this.Scintilla.Snippets.ActiveSnippetIndicator);
        }
        else
        {
          range3.SetIndicator(this.Scintilla.Snippets.InactiveSnippetIndicator);
          range3.ClearIndicator(this.Scintilla.Snippets.ActiveSnippetIndicator);
        }
      }
    }
  }

  public bool IsOneKeySelectionEmbedEnabled
  {
    get => this._isOneKeySelectionEmbedEnabled;
    set => this._isOneKeySelectionEmbedEnabled = value;
  }

  private bool ShouldSerializeIsOneKeySelectionEmbedEnabled()
  {
    return this._isOneKeySelectionEmbedEnabled;
  }

  private void ResetIsOneKeySelectionEmbedEnabled() => this._isOneKeySelectionEmbedEnabled = false;

  private void cascadeSnippetLinkRangeChange(
    SnippetLink oldActiveSnippetLink,
    SnippetLinkRange oldActiveRange)
  {
    this.Scintilla.ManagedRanges.Sort();
    int num = 0;
    string text = oldActiveRange.Text;
    this.Scintilla.NativeInterface.SetModEventMask(0);
    foreach (ManagedRange managedRange in this.Scintilla.ManagedRanges)
    {
      if (num != 0)
        managedRange.Change(managedRange.Start + num, managedRange.End + num);
      if (managedRange is SnippetLinkRange snippetLinkRange && oldActiveSnippetLink.Ranges.Contains(snippetLinkRange) && !(snippetLinkRange.Text == text))
      {
        int length = snippetLinkRange.Length;
        snippetLinkRange.Text = text;
        snippetLinkRange.End += text.Length - length;
        num += text.Length - length;
      }
    }
    this.Scintilla.NativeInterface.SetModEventMask(28671 /*0x6FFF*/);
  }

  private SnippetLinkRange addSnippetLink(SnippetLinkRange range)
  {
    string key = range.Key;
    SnippetLink snippetLink = (SnippetLink) null;
    for (int index = 0; index < this._snippetLinks.Count; ++index)
    {
      if (this._snippetLinks[index].Key.Equals(key, StringComparison.CurrentCultureIgnoreCase))
      {
        snippetLink = this._snippetLinks[index];
        break;
      }
    }
    if (snippetLink == null)
    {
      snippetLink = new SnippetLink(key);
      this._snippetLinks.Add(snippetLink);
    }
    snippetLink.Ranges.Add(range);
    range.Parent = snippetLink.Ranges;
    return range;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsActive
  {
    get => this._snippetLinks.IsActive;
    internal set
    {
      this._snippetLinks.IsActive = value;
      if (value)
      {
        this.SetIndicators();
        this._snippetLinks[0].Ranges[0].Select();
      }
      else
      {
        foreach (SnippetLink snippetLink in (IEnumerable<SnippetLink>) this._snippetLinks.Values)
        {
          foreach (Range range in snippetLink.Ranges)
          {
            range.ClearIndicator(this.Scintilla.Snippets.InactiveSnippetIndicator);
            range.ClearIndicator(this.Scintilla.Snippets.ActiveSnippetIndicator);
          }
        }
        this._snippetLinks.Clear();
        if (this._snippetLinks.EndPoint == null)
          return;
        this._snippetLinks.EndPoint.Dispose();
        this._snippetLinks.EndPoint = (SnippetLinkEnd) null;
      }
    }
  }

  public Color ActiveSnippetColor
  {
    get => this._activeSnippetColor;
    set => this._activeSnippetColor = value;
  }

  private bool ShouldSerializeActiveSnippetColor() => this._activeSnippetColor != Color.Lime;

  private void ResetActiveSnippetColor() => this._activeSnippetColor = Color.Lime;

  public int ActiveSnippetIndicator
  {
    get => this._activeSnippetIndicator;
    set => this._activeSnippetIndicator = value;
  }

  private bool ShouldSerializeActiveSnippetIndicator() => this._activeSnippetIndicator != 15;

  private void ResetActiveSnippetIndicator() => this._activeSnippetIndicator = 15;

  public IndicatorStyle ActiveSnippetIndicatorStyle
  {
    get => this._activeSnippetIndicatorStyle;
    set => this._activeSnippetIndicatorStyle = value;
  }

  private bool ShouldSerializeActiveSnippetIndicatorStyle()
  {
    return this._activeSnippetIndicatorStyle != IndicatorStyle.RoundBox;
  }

  private void ResetActiveSnippetIndicatorStyle()
  {
    this._activeSnippetIndicatorStyle = IndicatorStyle.RoundBox;
  }

  public char DefaultDelimeter
  {
    get => this._defaultDelimeter;
    set => this._defaultDelimeter = value;
  }

  private bool ShouldSerializeDefaultDelimeter() => this._defaultDelimeter != '$';

  private void ResetDefaultDelimeter() => this._defaultDelimeter = '$';

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public bool IsEnabled
  {
    get => this._isEnabled;
    set
    {
      this._isEnabled = value;
      if (value)
      {
        this.Scintilla.TextInserted += new EventHandler<TextModifiedEventArgs>(this.Scintilla_TextInserted);
        this.Scintilla.BeforeTextInsert += new EventHandler<TextModifiedEventArgs>(this.Scintilla_BeforeTextInsert);
        this.Scintilla.BeforeTextDelete += new EventHandler<TextModifiedEventArgs>(this.Scintilla_BeforeTextDelete);
        this.Scintilla.SelectionChanged += new EventHandler(this.Scintilla_SelectionChanged);
      }
      else
      {
        this.Scintilla.TextInserted -= new EventHandler<TextModifiedEventArgs>(this.Scintilla_TextInserted);
        this.Scintilla.BeforeTextInsert -= new EventHandler<TextModifiedEventArgs>(this.Scintilla_BeforeTextInsert);
        this.Scintilla.BeforeTextDelete -= new EventHandler<TextModifiedEventArgs>(this.Scintilla_BeforeTextDelete);
        this.Scintilla.SelectionChanged -= new EventHandler(this.Scintilla_SelectionChanged);
      }
    }
  }

  private bool ShouldSerializeIsEnabled() => !this._isEnabled;

  private void ResetIsEnabled() => this._isEnabled = true;

  public Color InactiveSnippetColor
  {
    get => this._inactiveSnippetColor;
    set => this._inactiveSnippetColor = value;
  }

  private bool ShouldSerializeInactiveSnippetColor() => this._inactiveSnippetColor != Color.Lime;

  private void ResetInactiveSnippetColor() => this._inactiveSnippetColor = Color.Lime;

  public int InactiveSnippetIndicator
  {
    get => this._inactiveSnippetIndicator;
    set => this._inactiveSnippetIndicator = value;
  }

  private bool ShouldSerializeInactiveSnippetIndicator()
  {
    return this._inactiveSnippetIndicator != 16 /*0x10*/;
  }

  private void ResetInactiveSnippetIndicator() => this._inactiveSnippetIndicator = 16 /*0x10*/;

  public IndicatorStyle InactiveSnippetIndicatorStyle
  {
    get => this._inactiveSnippetIndicatorStyle;
    set => this._inactiveSnippetIndicatorStyle = value;
  }

  private bool ShouldSerializeInactiveSnippetIndicatorStyle()
  {
    return this._inactiveSnippetIndicatorStyle != IndicatorStyle.Box;
  }

  private void ResetInactiveSnippetIndicatorStyle()
  {
    this._inactiveSnippetIndicatorStyle = IndicatorStyle.Box;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public SnippetList List
  {
    get => this._list;
    set => this._list = value;
  }

  public void ShowSnippetList()
  {
    if (this._list.Count == 0)
      return;
    if (this._snipperChooser == null)
    {
      this._snipperChooser = new SnippetChooser();
      this._snipperChooser.Scintilla = this.Scintilla;
      this._snipperChooser.SnippetList = this._list.ToString();
      this._snipperChooser.Scintilla.Controls.Add((Control) this._snipperChooser);
    }
    this._snipperChooser.SnippetList = this._list.ToString();
    this._snipperChooser.Show();
  }

  public void ShowSurroundWithList()
  {
    SnippetList snippetList = new SnippetList((SnippetManager) null);
    foreach (Snippet snippet in (Collection<Snippet>) this._list)
    {
      if (snippet.IsSurroundsWith)
        snippetList.Add(snippet);
    }
    if (snippetList.Count == 0)
      return;
    if (this._snipperChooser == null)
    {
      this._snipperChooser = new SnippetChooser();
      this._snipperChooser.Scintilla = this.Scintilla;
      this._snipperChooser.SnippetList = this._list.ToString();
      this._snipperChooser.Scintilla.Controls.Add((Control) this._snipperChooser);
    }
    this._snipperChooser.SnippetList = snippetList.ToString();
    this._snipperChooser.Show();
  }
}
