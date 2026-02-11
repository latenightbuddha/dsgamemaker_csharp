using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet.Design;

public class FlagCheckedListBox : CheckedListBox
{
  private System.ComponentModel.Container components;
  private bool isUpdatingCheckStates;
  private System.Type enumType;
  private Enum enumValue;

  public FlagCheckedListBox() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent() => this.CheckOnClick = true;

  public FlagCheckedListBoxItem Add(int v, string c)
  {
    FlagCheckedListBoxItem checkedListBoxItem = new FlagCheckedListBoxItem(v, c);
    this.Items.Add((object) checkedListBoxItem);
    return checkedListBoxItem;
  }

  public FlagCheckedListBoxItem Add(FlagCheckedListBoxItem item)
  {
    this.Items.Add((object) item);
    return item;
  }

  protected override void OnItemCheck(ItemCheckEventArgs e)
  {
    base.OnItemCheck(e);
    if (this.isUpdatingCheckStates)
      return;
    this.UpdateCheckedItems(this.Items[e.Index] as FlagCheckedListBoxItem, e.NewValue);
  }

  protected void UpdateCheckedItems(int value)
  {
    this.isUpdatingCheckStates = true;
    for (int index = 0; index < this.Items.Count; ++index)
    {
      FlagCheckedListBoxItem checkedListBoxItem = this.Items[index] as FlagCheckedListBoxItem;
      if (checkedListBoxItem.value == 0)
        this.SetItemChecked(index, value == 0);
      else if ((checkedListBoxItem.value & value) == checkedListBoxItem.value && checkedListBoxItem.value != 0)
        this.SetItemChecked(index, true);
      else
        this.SetItemChecked(index, false);
    }
    this.isUpdatingCheckStates = false;
  }

  protected void UpdateCheckedItems(FlagCheckedListBoxItem composite, CheckState cs)
  {
    if (composite.value == 0)
      this.UpdateCheckedItems(0);
    int num = 0;
    for (int index = 0; index < this.Items.Count; ++index)
    {
      FlagCheckedListBoxItem checkedListBoxItem = this.Items[index] as FlagCheckedListBoxItem;
      if (this.GetItemChecked(index))
        num |= checkedListBoxItem.value;
    }
    this.UpdateCheckedItems(cs != CheckState.Unchecked ? num | composite.value : num & ~composite.value);
  }

  public int GetCurrentValue()
  {
    int currentValue = 0;
    for (int index = 0; index < this.Items.Count; ++index)
    {
      FlagCheckedListBoxItem checkedListBoxItem = this.Items[index] as FlagCheckedListBoxItem;
      if (this.GetItemChecked(index))
        currentValue |= checkedListBoxItem.value;
    }
    return currentValue;
  }

  private void FillEnumMembers()
  {
    foreach (string name in Enum.GetNames(this.enumType))
      this.Add((int) Convert.ChangeType(Enum.Parse(this.enumType, name), typeof (int)), name);
  }

  private void ApplyEnumValue()
  {
    this.UpdateCheckedItems((int) Convert.ChangeType((object) this.enumValue, typeof (int)));
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Enum EnumValue
  {
    get => (Enum) Enum.ToObject(this.enumType, this.GetCurrentValue());
    set
    {
      this.Items.Clear();
      this.enumValue = value;
      this.enumType = value.GetType();
      this.FillEnumMembers();
      this.ApplyEnumValue();
    }
  }
}
