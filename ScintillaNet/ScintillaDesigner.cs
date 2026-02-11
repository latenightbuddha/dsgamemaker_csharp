using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Design;

#nullable disable
namespace ScintillaNet;

internal class ScintillaDesigner : ControlDesigner
{
  public override void InitializeNewComponent(IDictionary defaultValues)
  {
    base.InitializeNewComponent(defaultValues);
    PropertyDescriptor property = TypeDescriptor.GetProperties((object) this.Component)["Text"];
    if (property == null || property.PropertyType != typeof (string) || property.IsReadOnly || !property.IsBrowsable)
      return;
    property.SetValue((object) this.Component, (object) string.Empty);
  }
}
