using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (Whitespace.WhitespaceConverter))]
public class Whitespace : TopLevelHelper
{
  private const string BACK_COLORBAG = "Whitespace.BackColor";
  private const string FORE_COLORBAG = "Whitespace.ForeColor";
  private const string ALPHA_EXCEPTION = "Transparent colors are not supported.";

  [NotifyParentProperty(true)]
  [RefreshProperties(RefreshProperties.Repaint)]
  [Description("The background color of whitespace characters.")]
  [DefaultValue(typeof (Color), "")]
  [Category("Appearance")]
  public Color BackColor
  {
    get
    {
      Color backColor;
      this.Scintilla.ColorBag.TryGetValue("Whitespace.BackColor", out backColor);
      return backColor;
    }
    set
    {
      if (value != Color.Empty && value.A < byte.MaxValue)
        throw new ArgumentException("Transparent colors are not supported.");
      if (!(value != this.BackColor))
        return;
      if (value == Color.Empty)
      {
        this.Scintilla.ColorBag.Remove("Whitespace.BackColor");
        this.NativeScintilla.SetWhitespaceBack(false, 0);
      }
      else
      {
        this.Scintilla.ColorBag["Whitespace.BackColor"] = value;
        this.NativeScintilla.SetWhitespaceBack(true, Utilities.ColorToRgb(value));
      }
    }
  }

  [Category("Appearance")]
  [Description("The foreground color of whitespace characters.")]
  [DefaultValue(typeof (Color), "")]
  [NotifyParentProperty(true)]
  [RefreshProperties(RefreshProperties.Repaint)]
  public Color ForeColor
  {
    get
    {
      Color foreColor;
      this.Scintilla.ColorBag.TryGetValue("Whitespace.ForeColor", out foreColor);
      return foreColor;
    }
    set
    {
      if (value != Color.Empty && value.A < byte.MaxValue)
        throw new ArgumentException("Transparent colors are not supported.");
      if (!(value != this.ForeColor))
        return;
      if (value == Color.Empty)
      {
        this.Scintilla.ColorBag.Remove("Whitespace.ForeColor");
        this.NativeScintilla.SetWhitespaceFore(false, 16777215 /*0xFFFFFF*/);
      }
      else
      {
        this.Scintilla.ColorBag["Whitespace.ForeColor"] = value;
        this.NativeScintilla.SetWhitespaceFore(true, Utilities.ColorToRgb(value));
      }
    }
  }

  [DefaultValue(WhitespaceMode.Invisible)]
  [Description("The mode used to display whitespace.")]
  [Category("Appearance")]
  [NotifyParentProperty(true)]
  [RefreshProperties(RefreshProperties.Repaint)]
  public WhitespaceMode Mode
  {
    get => (WhitespaceMode) this.NativeScintilla.GetViewWs();
    set
    {
      if (!Enum.IsDefined(typeof (WhitespaceMode), (object) value))
        throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (WhitespaceMode));
      if (value == this.Mode)
        return;
      this.NativeScintilla.SetViewWs((int) value);
    }
  }

  internal Whitespace(Scintilla scintilla)
    : base(scintilla)
  {
  }

  private class WhitespaceConverter : ExpandableObjectConverter
  {
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType != typeof (string))
        base.ConvertTo(context, culture, value, destinationType);
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (PropertyInfo property in context.PropertyDescriptor.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
          object[] customAttributes = property.GetCustomAttributes(typeof (DefaultValueAttribute), true);
          if (customAttributes != null && customAttributes.Length > 0)
          {
            DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute) customAttributes[0];
            object obj = property.GetValue(value, (object[]) null);
            if (!defaultValueAttribute.Value.Equals(obj))
            {
              if (stringBuilder.Length > 0)
                stringBuilder.Append("; ");
              stringBuilder.Append(property.Name);
              stringBuilder.Append("=");
              TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
              if (converter != null)
                stringBuilder.Append(converter.ConvertToString(obj));
              else if (obj != null)
                stringBuilder.Append(obj.ToString());
            }
          }
        }
        return (object) stringBuilder.ToString();
      }
      catch
      {
        return (object) string.Empty;
      }
    }
  }
}
