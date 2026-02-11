using System;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class CallTip : TopLevelHelper
{
  public const char UpArrow = '\u0001';
  public const char DownArrow = '\u0002';
  private int _lastPos = -1;
  private OverloadList _overloadList;
  private string _message;
  private int _highlightStart = -1;
  private int _highlightEnd = -1;

  internal CallTip(Scintilla scintilla)
    : base(scintilla)
  {
    this.NativeScintilla.CallTipUseStyle(10);
    this.Scintilla.BeginInvoke((Delegate) (() =>
    {
      this.HighlightTextColor = this.HighlightTextColor;
      this.ForeColor = this.ForeColor;
      this.BackColor = this.BackColor;
    }));
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeBackColor() || this.ShouldSerializeForeColor() || this.ShouldSerializeHighlightEnd() || this.ShouldSerializeHighlightStart() || this.ShouldSerializeHighlightTextColor();
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public OverloadList OverloadList
  {
    get => this._overloadList;
    set => this._overloadList = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string Message
  {
    get => this._message;
    set => this._message = value;
  }

  public void ShowOverload(
    OverloadList overloadList,
    int position,
    uint startIndex,
    int highlightStart,
    int highlightEnd)
  {
    this._lastPos = position;
    this._overloadList = overloadList;
    this._overloadList.CurrentIndex = (int) startIndex;
    this._highlightEnd = highlightEnd;
    this._highlightStart = highlightStart;
    this.ShowOverloadInternal();
  }

  public void ShowOverload(OverloadList overloadList, int position)
  {
    this.ShowOverload(overloadList, position, 0U, -1, -1);
  }

  public void ShowOverload(
    OverloadList overloadList,
    int position,
    int highlightStart,
    int highlightEnd)
  {
    this.ShowOverload(overloadList, position, 0U, highlightStart, highlightEnd);
  }

  public void ShowOverload(OverloadList overloadList, uint startIndex)
  {
    this.ShowOverload(overloadList, -1, startIndex, -1, -1);
  }

  public void ShowOverload(
    OverloadList overloadList,
    uint startIndex,
    int highlightStart,
    int highlightEnd)
  {
    this.ShowOverload(overloadList, -1, startIndex, highlightStart, highlightEnd);
  }

  public void ShowOverload(OverloadList overloadList)
  {
    this.ShowOverload(overloadList, -1, 0U, -1, -1);
  }

  public void ShowOverload(OverloadList overloadList, int highlightStart, int highlightEnd)
  {
    this.ShowOverload(overloadList, -1, 0U, highlightStart, highlightEnd);
  }

  public void ShowOverload(int position, uint startIndex)
  {
    this.ShowOverload(this._overloadList, position, startIndex, -1, -1);
  }

  public void ShowOverload(int position, uint startIndex, int highlightStart, int highlightEnd)
  {
    this.ShowOverload(this._overloadList, position, startIndex, highlightStart, highlightEnd);
  }

  public void ShowOverload(int position)
  {
    this.ShowOverload(this._overloadList, position, 0U, -1, -1);
  }

  public void ShowOverload(int position, int highlightStart, int highlightEnd)
  {
    this.ShowOverload(this._overloadList, position, 0U, highlightStart, highlightEnd);
  }

  public void ShowOverload(uint startIndex)
  {
    this.ShowOverload(this._overloadList, -1, startIndex, -1, -1);
  }

  public void ShowOverload(uint startIndex, int highlightStart, int highlightEnd)
  {
    this.ShowOverload(this._overloadList, -1, startIndex, highlightStart, highlightEnd);
  }

  public void ShowOverload(int highlightStart, int highlightEnd)
  {
    this.ShowOverload(this._overloadList, -1, 0U, highlightStart, highlightEnd);
  }

  public void ShowOverload() => this.ShowOverload(this._overloadList, -1, 0U, -1, -1);

  public void Show(string message, int position, int highlightStart, int higlightEnd)
  {
    this._lastPos = position;
    if (position < 0)
      position = this.NativeScintilla.GetCurrentPos();
    this._overloadList = (OverloadList) null;
    this._message = message;
    this.NativeScintilla.CallTipShow(position, message);
    this.HighlightStart = highlightStart;
    this.HighlightEnd = this.HighlightEnd;
  }

  public void Show(string message) => this.Show(message, -1, -1, -1);

  public void Show(string message, int highlightStart, int higlightEnd)
  {
    this.Show(message, -1, highlightStart, higlightEnd);
  }

  public void Show(string message, int position) => this.Show(message, position, -1, -1);

  public void Show(int position) => this.Show(this._message, position, -1, -1);

  public void Show(int position, int highlightStart, int higlightEnd)
  {
    this.Show(this._message, position, highlightStart, higlightEnd);
  }

  public void Show(int highlightStart, int higlightEnd)
  {
    this.Show(this._message, -1, highlightStart, higlightEnd);
  }

  public void Show() => this.Show(this._message, -1, -1, -1);

  internal void ShowOverloadInternal()
  {
    int posStart = this._lastPos;
    if (posStart < 0)
      posStart = this.NativeScintilla.GetCurrentPos();
    string definition = string.Format("\u0001 {1} of {2} \u0002 {0}", (object) this._overloadList.Current, (object) (this._overloadList.CurrentIndex + 1), (object) this._overloadList.Count);
    this.NativeScintilla.CallTipCancel();
    this.NativeScintilla.CallTipShow(posStart, definition);
    this.NativeScintilla.CallTipSetHlt(this._highlightStart, this._highlightEnd);
  }

  public void Cancel() => this.NativeScintilla.CallTipCancel();

  public Color ForeColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("CallTip.ForeColor") ? this.Scintilla.ColorBag["CallTip.ForeColor"] : SystemColors.InfoText;
    }
    set
    {
      this.SetForeColorInternal(value);
      this.Scintilla.Styles.CallTip.SetForeColorInternal(value);
    }
  }

  internal void SetForeColorInternal(Color value)
  {
    if (value == SystemColors.InfoText)
      this.Scintilla.ColorBag.Remove("CallTip.ForeColor");
    else
      this.Scintilla.ColorBag["CallTip.ForeColor"] = value;
    this.NativeScintilla.CallTipSetFore(Utilities.ColorToRgb(value));
  }

  private bool ShouldSerializeForeColor() => this.ForeColor != SystemColors.InfoText;

  private void ResetForeColor() => this.ForeColor = SystemColors.InfoText;

  public Color BackColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("CallTip.BackColor") ? this.Scintilla.ColorBag["CallTip.BackColor"] : SystemColors.Info;
    }
    set
    {
      this.SetBackColorInternal(value);
      this.Scintilla.Styles.CallTip.SetBackColorInternal(value);
    }
  }

  internal void SetBackColorInternal(Color value)
  {
    if (value == SystemColors.Info)
      this.Scintilla.ColorBag.Remove("CallTip.BackColor");
    else
      this.Scintilla.ColorBag["CallTip.BackColor"] = value;
    this.NativeScintilla.CallTipSetBack(Utilities.ColorToRgb(value));
  }

  private bool ShouldSerializeBackColor() => this.BackColor != SystemColors.Info;

  private void ResetBackColor() => this.BackColor = SystemColors.Info;

  public Color HighlightTextColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("CallTip.HighlightTextColor") ? this.Scintilla.ColorBag["CallTip.HighlightTextColor"] : SystemColors.Highlight;
    }
    set
    {
      if (value == SystemColors.Highlight)
        this.Scintilla.ColorBag.Remove("CallTip.HighlightTextColor");
      else
        this.Scintilla.ColorBag["CallTip.HighlightTextColor"] = value;
      this.NativeScintilla.CallTipSetForeHlt(Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeHighlightTextColor()
  {
    return this.HighlightTextColor != SystemColors.Highlight;
  }

  private void ResetHighlightTextColor() => this.HighlightTextColor = SystemColors.Highlight;

  public int HighlightStart
  {
    get => this._highlightStart;
    set
    {
      this._highlightStart = value;
      this.NativeScintilla.CallTipSetHlt(this._highlightStart, this._highlightStart);
    }
  }

  private bool ShouldSerializeHighlightStart() => this._highlightStart >= 0;

  private void ResetHighlightStart() => this._highlightStart = -1;

  public int HighlightEnd
  {
    get => this._highlightEnd;
    set
    {
      this._highlightEnd = value;
      this.NativeScintilla.CallTipSetHlt(this._highlightStart, this._highlightEnd);
    }
  }

  private bool ShouldSerializeHighlightEnd() => this._highlightEnd >= 0;

  private void ResetHighlightEnd() => this._highlightEnd = -1;

  public bool IsActive => this.NativeScintilla.CallTipActive();

  public void Hide() => this.NativeScintilla.CallTipCancel();
}
