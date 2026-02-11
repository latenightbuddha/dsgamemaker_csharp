using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class MarginCollection : TopLevelHelper, ICollection<Margin>, IEnumerable<Margin>, IEnumerable
{
  private Margin _margin0;
  private Margin _margin1;
  private Margin _margin2;
  private Margin _margin3;
  private Margin _margin4;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Browsable(true)]
  public Margin Margin0 => this._margin0;

  private bool ShouldSerializeMargin0() => this._margin0.ShouldSerialize();

  private void ResetMargin0() => this._margin0.Reset();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Browsable(true)]
  public Margin Margin1 => this._margin1;

  private bool ShouldSerializeMargin1() => this._margin1.ShouldSerialize();

  private void ResetMargin1() => this._margin0.Reset();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Browsable(true)]
  public Margin Margin2 => this._margin2;

  private bool ShouldSerializeMargin2() => this._margin2.ShouldSerialize();

  private void ResetMargin2() => this._margin0.Reset();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Browsable(true)]
  public Margin Margin3 => this._margin3;

  private bool ShouldSerializeMargin3() => this._margin3.ShouldSerialize();

  private void ResetMargin3() => this._margin0.Reset();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [Browsable(true)]
  public Margin Margin4 => this._margin4;

  private bool ShouldSerializeMargin4() => this._margin4.ShouldSerialize();

  private void ResetMargin4() => this._margin0.Reset();

  protected internal MarginCollection(Scintilla scintilla)
    : base(scintilla)
  {
    this._margin0 = new Margin(scintilla, 0);
    this._margin1 = new Margin(scintilla, 1);
    this._margin2 = new Margin(scintilla, 2);
    this._margin3 = new Margin(scintilla, 3);
    this._margin4 = new Margin(scintilla, 4);
    this._margin2.IsFoldMargin = true;
    this._margin2.IsClickable = true;
  }

  public Margin this[int number]
  {
    get
    {
      switch (number)
      {
        case 0:
          return this._margin0;
        case 1:
          return this._margin1;
        case 2:
          return this._margin2;
        case 3:
          return this._margin3;
        case 4:
          return this._margin4;
        default:
          throw new ArgumentException(nameof (number), "Number must be greater than or equal to 0 AND less than 5");
      }
    }
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeFoldMarginColor() || this.ShouldSerializeFoldMarginHighlightColor() || this.ShouldSerializeLeft() || this.ShouldSerializeRight() || this.ShouldSerializeMargin0() || this.ShouldSerializeMargin1() || this.ShouldSerializeMargin2() || this.ShouldSerializeMargin3() || this.ShouldSerializeMargin4();
  }

  public void Reset()
  {
    this.ResetMargin0();
    this.ResetMargin1();
    this.ResetMargin2();
    this.ResetMargin3();
    this.ResetMargin4();
  }

  public int Left
  {
    get => this.NativeScintilla.GetMarginLeft();
    set => this.NativeScintilla.SetMarginLeft(value);
  }

  private bool ShouldSerializeLeft() => this.Left != 1;

  private void ResetLeft() => this.Left = 1;

  public int Right
  {
    get => this.NativeScintilla.GetMarginRight();
    set => this.NativeScintilla.SetMarginRight(value);
  }

  private bool ShouldSerializeRight() => this.Right != 1;

  private void ResetRight() => this.Right = 1;

  public Color FoldMarginColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("Margins.FoldMarginColor") ? this.Scintilla.ColorBag["Margins.FoldMarginColor"] : Color.Transparent;
    }
    set
    {
      if (value == Color.Transparent)
        this.Scintilla.ColorBag.Remove("Margins.FoldMarginColor");
      else
        this.Scintilla.ColorBag["Margins.FoldMarginColor"] = value;
      this.NativeScintilla.SetFoldMarginColour(true, Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeFoldMarginColor() => this.FoldMarginColor != Color.Transparent;

  private void ResetFoldMarginColor() => this.FoldMarginColor = Color.Transparent;

  public Color FoldMarginHighlightColor
  {
    get
    {
      return this.Scintilla.ColorBag.ContainsKey("Margins.FoldMarginHighlightColor") ? this.Scintilla.ColorBag["Margins.FoldMarginHighlightColor"] : Color.Transparent;
    }
    set
    {
      if (value == Color.Transparent)
        this.Scintilla.ColorBag.Remove("Margins.FoldMarginHighlightColor");
      else
        this.Scintilla.ColorBag["Margins.FoldMarginHighlightColor"] = value;
      this.NativeScintilla.SetFoldMarginColour(true, Utilities.ColorToRgb(value));
    }
  }

  private bool ShouldSerializeFoldMarginHighlightColor()
  {
    return this.FoldMarginHighlightColor != Color.Transparent;
  }

  private void ResetFoldMarginHighlightColor() => this.FoldMarginHighlightColor = Color.Transparent;

  public void Add(Margin item)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public void Clear() => throw new Exception("The method or operation is not implemented.");

  public bool Contains(Margin item) => true;

  public void CopyTo(Margin[] array, int arrayIndex)
  {
    Array.Copy((Array) this.ToArray(), 0, (Array) array, arrayIndex, 5);
  }

  public Margin[] ToArray()
  {
    return new Margin[5]
    {
      this._margin0,
      this._margin1,
      this._margin2,
      this._margin3,
      this._margin4
    };
  }

  [Browsable(false)]
  public int Count => 5;

  [Browsable(false)]
  public bool IsReadOnly => true;

  public bool Remove(Margin item)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public IEnumerator<Margin> GetEnumerator()
  {
    return (IEnumerator<Margin>) new List<Margin>((IEnumerable<Margin>) this.ToArray()).GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new List<Margin>((IEnumerable<Margin>) this.ToArray()).GetEnumerator();
  }
}
