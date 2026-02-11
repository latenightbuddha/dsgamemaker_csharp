using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
public static class XpmConverter
{
  public static string DefaultTransparentColor = "#FF00FF";

  public static List<string> ConvertToXPM(ImageList ImageList)
  {
    return XpmConverter.ConvertToXPM(ImageList, XpmConverter.DefaultTransparentColor);
  }

  public static List<string> ConvertToXPM(ImageList imageList, string transparentColor)
  {
    List<string> xpm = new List<string>();
    foreach (Image image in imageList.Images)
    {
      if (image is Bitmap)
        xpm.Add(XpmConverter.ConvertToXPM(image as Bitmap, transparentColor));
    }
    return xpm;
  }

  public static string ConvertToXPM(Bitmap bmp)
  {
    return XpmConverter.ConvertToXPM(bmp, XpmConverter.DefaultTransparentColor);
  }

  public static string ConvertToXPM(Bitmap bmp, string transparentColor)
  {
    StringBuilder stringBuilder = new StringBuilder();
    List<string> stringList = new List<string>();
    List<char> charList = new List<char>();
    int width = bmp.Width;
    int height = bmp.Height;
    stringBuilder.Append("/* XPM */static char * xmp_data[] = {\"").Append(width).Append(" ").Append(height).Append(" ? 1\"");
    int length1 = stringBuilder.Length;
    for (int y = 0; y < height; ++y)
    {
      stringBuilder.Append(",\"");
      for (int x = 0; x < width; ++x)
      {
        string html = ColorTranslator.ToHtml(bmp.GetPixel(x, y));
        int index = stringList.IndexOf(html);
        char ch;
        if (index < 0)
        {
          int num = stringList.Count + 65;
          stringList.Add(html);
          if (num > 90)
            num += 6;
          ch = Encoding.ASCII.GetChars(new byte[1]
          {
            (byte) (num & (int) byte.MaxValue)
          })[0];
          charList.Add(ch);
          stringBuilder.Insert(length1, $",\"{(object) ch} c {html}\"");
          length1 += 14;
        }
        else
          ch = charList[index];
        stringBuilder.Append(ch);
      }
      stringBuilder.Append("\"");
    }
    stringBuilder.Append("};");
    string str = stringBuilder.ToString();
    int length2 = str.IndexOf("?");
    return str.Substring(0, length2) + (object) stringList.Count + str.Substring(length2 + 1).Replace(transparentColor.ToUpper(), "None");
  }
}
