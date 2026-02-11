using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace ScintillaNet.Design;

public class FlagEnumUIEditor : UITypeEditor
{
  private FlagCheckedListBox flagEnumCB;

  public FlagEnumUIEditor()
  {
    this.flagEnumCB = new FlagCheckedListBox();
    this.flagEnumCB.BorderStyle = BorderStyle.None;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (context != null && context.Instance != null && provider != null)
    {
      IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      if (service != null)
      {
        this.flagEnumCB.EnumValue = (Enum) Convert.ChangeType(value, context.PropertyDescriptor.PropertyType);
        service.DropDownControl((Control) this.flagEnumCB);
        return (object) this.flagEnumCB.EnumValue;
      }
    }
    return (object) null;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }
}
