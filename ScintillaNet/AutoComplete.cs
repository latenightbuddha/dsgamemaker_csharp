using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class AutoComplete : TopLevelHelper
{
  private System.Collections.Generic.List<string> _list = new System.Collections.Generic.List<string>();
  private string _stopCharacters = string.Empty;
  private string _fillUpCharacters = string.Empty;
  private bool _automaticLengthEntered = true;

  internal AutoComplete(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeAutoHide() || this.ShouldSerializeCancelAtStart() || this.ShouldSerializeDropRestOfWord() || this.ShouldSerializeFillUpCharacters() || this.ShouldSerializeImageSeparator() || this.ShouldSerializeIsCaseSensitive() || this.ShouldSerializeListSeparator() || this.ShouldSerializeMaxHeight() || this.ShouldSerializeMaxWidth() || this.ShouldSerializeSingleLineAccept() || this.ShouldSerializeStopCharacters();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.Collections.Generic.List<string> List
  {
    get => this._list;
    set
    {
      if (value == null)
        value = new System.Collections.Generic.List<string>();
      this._list = value;
    }
  }

  public string ListString
  {
    get => this.getListString((IEnumerable<string>) this._list);
    set => this._list = new System.Collections.Generic.List<string>((IEnumerable<string>) value.Split(this.ListSeparator));
  }

  public void Show(int lengthEntered, string list)
  {
    if (list == string.Empty)
      this._list = new System.Collections.Generic.List<string>();
    else
      this._list = new System.Collections.Generic.List<string>((IEnumerable<string>) list.Split(this.ListSeparator));
    this.Show(lengthEntered, list, true);
  }

  internal void Show(int lengthEntered, string list, bool dontSplit)
  {
    int lenEntered = lengthEntered;
    if (lenEntered < 0)
      lenEntered = this.getLengthEntered();
    this.NativeScintilla.AutoCShow(lenEntered, list);
    if (this.IsActive || lengthEntered >= 0)
      return;
    this.NativeScintilla.AutoCShow(0, list);
  }

  public void Show() => this.Show(-1, this.getListString((IEnumerable<string>) this._list), false);

  public void Show(string list) => this.Show(-1, list);

  private int getLengthEntered()
  {
    if (!this._automaticLengthEntered)
      return 0;
    int currentPos = this.NativeScintilla.GetCurrentPos();
    return currentPos - this.NativeScintilla.WordStartPosition(currentPos, true);
  }

  public void Show(int lengthEntered, IEnumerable<string> list)
  {
    this._list = new System.Collections.Generic.List<string>(list);
    this.Show(-1);
  }

  public void Show(IEnumerable<string> list)
  {
    this._list = new System.Collections.Generic.List<string>(list);
    this.Show(-1);
  }

  public void Show(int lengthEntered)
  {
    this.Show(lengthEntered, this.getListString((IEnumerable<string>) this._list), false);
  }

  public void ShowUserList(int listType, string list)
  {
    this.NativeScintilla.UserListShow(listType, list);
  }

  public void ShowUserList(int listType, IEnumerable<string> list)
  {
    this.Show(listType, this.getListString(list), true);
  }

  public void Cancel() => this.NativeScintilla.AutoCCancel();

  [Browsable(false)]
  public bool IsActive => this.NativeScintilla.AutoCActive();

  [Browsable(false)]
  public int LastStartPosition => this.NativeScintilla.AutoCPosStart();

  public void Accept() => this.NativeScintilla.AutoCComplete();

  public string StopCharacters
  {
    get => this._stopCharacters;
    set
    {
      this._stopCharacters = value;
      this.NativeScintilla.AutoCStops(value);
    }
  }

  private bool ShouldSerializeStopCharacters() => this._stopCharacters != string.Empty;

  private void ResetStopCharacters() => this._stopCharacters = string.Empty;

  [TypeConverter(typeof (WhitespaceStringConverter))]
  public char ListSeparator
  {
    get => this.NativeScintilla.AutoCGetSeparator();
    set => this.NativeScintilla.AutoCSetSeparator(value);
  }

  private bool ShouldSerializeListSeparator() => this.ListSeparator != ' ';

  private void ResetListSeparator() => this.ListSeparator = ' ';

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string SelectedText
  {
    get => this._list[this.NativeScintilla.AutoCGetCurrent()];
    set => this.NativeScintilla.AutoCSelect(value);
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public int SelectedIndex
  {
    get => this.NativeScintilla.AutoCGetCurrent();
    set => this.SelectedText = this._list[value];
  }

  public bool CancelAtStart
  {
    get => this.NativeScintilla.AutoCGetCancelAtStart();
    set => this.NativeScintilla.AutoCSetCancelAtStart(value);
  }

  private bool ShouldSerializeCancelAtStart() => !this.CancelAtStart;

  private void ResetCancelAtStart() => this.CancelAtStart = true;

  public string FillUpCharacters
  {
    get => this._fillUpCharacters;
    set
    {
      this._fillUpCharacters = value;
      this.NativeScintilla.AutoCSetFillUps(value);
    }
  }

  private bool ShouldSerializeFillUpCharacters() => this._fillUpCharacters != string.Empty;

  private void ResetFillUpCharacters() => this._fillUpCharacters = string.Empty;

  public bool SingleLineAccept
  {
    get => this.NativeScintilla.AutoCGetChooseSingle();
    set => this.NativeScintilla.AutoCSetChooseSingle(value);
  }

  private bool ShouldSerializeSingleLineAccept() => this.SingleLineAccept;

  private void ResetSingleLineAccept() => this.SingleLineAccept = false;

  public bool IsCaseSensitive
  {
    get => !this.NativeScintilla.AutoCGetIgnoreCase();
    set => this.NativeScintilla.AutoCSetIgnoreCase(!value);
  }

  private bool ShouldSerializeIsCaseSensitive() => !this.IsCaseSensitive;

  private void ResetIsCaseSensitive() => this.IsCaseSensitive = true;

  public bool AutoHide
  {
    get => this.NativeScintilla.AutoCGetAutoHide();
    set => this.NativeScintilla.AutoCSetAutoHide(value);
  }

  private bool ShouldSerializeAutoHide() => !this.AutoHide;

  private void ResetAutoHide() => this.AutoHide = true;

  public bool DropRestOfWord
  {
    get => this.NativeScintilla.AutoCGetDropRestOfWord();
    set => this.NativeScintilla.AutoCSetDropRestOfWord(value);
  }

  private bool ShouldSerializeDropRestOfWord() => this.DropRestOfWord;

  private void ResetDropRestOfWord() => this.DropRestOfWord = false;

  public void RegisterImage(int type, string xpmImage)
  {
    this.NativeScintilla.RegisterImage(type, xpmImage);
  }

  private void RegisterImage(int type, Bitmap image, Color transparentColor)
  {
    this.NativeScintilla.RegisterImage(type, XpmConverter.ConvertToXPM(image, Utilities.ColorToHtml(transparentColor)));
  }

  private void RegisterImage(int type, Bitmap image)
  {
    this.NativeScintilla.RegisterImage(type, XpmConverter.ConvertToXPM(image));
  }

  public void RegisterImages(IList<string> xpmImages)
  {
    for (int index = 0; index < xpmImages.Count; ++index)
      this.NativeScintilla.RegisterImage(index, xpmImages[index]);
  }

  public void RegisterImages(IList<Bitmap> images)
  {
    for (int index = 0; index < images.Count; ++index)
      this.RegisterImage(index, images[index]);
  }

  public void RegisterImages(IList<Bitmap> images, Color transparentColor)
  {
    for (int index = 0; index < images.Count; ++index)
      this.RegisterImage(index, images[index], transparentColor);
  }

  public void RegisterImages(ImageList images)
  {
    this.RegisterImages((IList<string>) XpmConverter.ConvertToXPM(images));
  }

  public void RegisterImages(ImageList images, Color transparentColor)
  {
    this.RegisterImages((IList<string>) XpmConverter.ConvertToXPM(images, Utilities.ColorToHtml(transparentColor)));
  }

  public void ClearRegisteredImages() => this.NativeScintilla.ClearRegisteredImages();

  public char ImageSeparator
  {
    get => this.NativeScintilla.AutoCGetTypeSeparator();
    set => this.NativeScintilla.AutoCSetTypeSeparator(value);
  }

  private bool ShouldSerializeImageSeparator() => this.ImageSeparator != '?';

  private void ResetImageSeparator() => this.ImageSeparator = '?';

  public int MaxHeight
  {
    get => this.NativeScintilla.AutoCGetMaxHeight();
    set => this.NativeScintilla.AutoCSetMaxHeight(value);
  }

  private bool ShouldSerializeMaxHeight() => this.MaxHeight != 5;

  private void ResetMaxHeight() => this.MaxHeight = 5;

  public int MaxWidth
  {
    get => this.NativeScintilla.AutoCGetMaxWidth();
    set => this.NativeScintilla.AutoCSetMaxWidth(value);
  }

  private bool ShouldSerializeMaxWidth() => this.MaxWidth != 0;

  private void ResetMaxWidth() => this.MaxWidth = 0;

  public bool AutomaticLengthEntered
  {
    get => this._automaticLengthEntered;
    set => this._automaticLengthEntered = value;
  }

  private bool ShouldSerializeAutomaticLengthEntered() => !this.AutomaticLengthEntered;

  private void ResetAutomaticLengthEntered() => this.AutomaticLengthEntered = true;

  private string getListString(IEnumerable<string> list)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string str in list)
      stringBuilder.Append(str).Append(" ");
    if (stringBuilder.Length > 1)
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
    return stringBuilder.ToString().Trim();
  }
}
