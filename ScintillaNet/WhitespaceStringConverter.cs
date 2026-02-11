using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace ScintillaNet;

public class WhitespaceStringConverter : TypeConverter
{
  private static readonly Regex rr = new Regex("\\{0x([0123456789abcdef]{1,4})\\}", RegexOptions.IgnoreCase);

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (char) || sourceType == typeof (string);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (char) || destinationType == typeof (string);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    string str = this.convertFrom(value.ToString());
    return context.PropertyDescriptor.ComponentType == typeof (char) ? (object) str[0] : (object) str;
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    string str = this.convertTo(value.ToString());
    return destinationType == typeof (char) ? (object) str[0] : (object) str;
  }

  public override bool IsValid(ITypeDescriptorContext context, object value) => true;

  private string convertTo(string nativeString)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (char ch in nativeString)
    {
      if (ch > ' ')
      {
        stringBuilder.Append(ch);
      }
      else
      {
        switch (ch)
        {
          case '\t':
            stringBuilder.Append("{TAB}");
            continue;
          case '\n':
            stringBuilder.Append("{LF}");
            continue;
          case '\r':
            stringBuilder.Append("{CR}");
            continue;
          case ' ':
            stringBuilder.Append("{SPACE}");
            continue;
          default:
            stringBuilder.Append($"{{0x{((int) ch).ToString("x4")}}}");
            continue;
        }
      }
    }
    return stringBuilder.ToString();
  }

  private string convertFrom(string value)
  {
    for (Match match = WhitespaceStringConverter.rr.Match(value); match.Success; match = WhitespaceStringConverter.rr.Match(value))
    {
      int num = int.Parse(match.Value.Substring(3, match.Length - 4), NumberStyles.AllowHexSpecifier);
      value = value.Replace(match.Value, ((char) num).ToString());
    }
    value = value.Replace("{TAB}", "\t").Replace("{LF}", "\r").Replace("{CR}", "\n").Replace("{SPACE}", " ");
    return value;
  }
}
