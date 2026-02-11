using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public static class Utilities
{
  public static int ColorToRgb(Color c)
  {
    return (int) c.R + ((int) c.G << 8) + ((int) c.B << 16 /*0x10*/);
  }

  public static Color RgbToColor(int color)
  {
    return Color.FromArgb(color & (int) byte.MaxValue, (color & 65280) >> 8, (color & 16711680 /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static string ColorToHtml(Color c)
  {
    return $"#{c.R.ToString("X2", (IFormatProvider) null)}{c.G.ToString("X2", (IFormatProvider) null)}{c.B.ToString("X2", (IFormatProvider) null)}";
  }

  public static int SignedLoWord(IntPtr loWord)
  {
    return (int) (short) ((int) (long) loWord & (int) ushort.MaxValue);
  }

  public static int SignedHiWord(IntPtr hiWord)
  {
    return (int) (short) ((int) (long) hiWord >> 16 /*0x10*/ & (int) ushort.MaxValue);
  }

  public static string IntPtrToString(Encoding encoding, IntPtr ptr, int length)
  {
    if (ptr == IntPtr.Zero)
      return (string) null;
    if (length == 0)
      return string.Empty;
    byte[] numArray = new byte[length];
    Marshal.Copy(ptr, numArray, 0, length);
    if (numArray[numArray.Length - 1] == (byte) 0)
      --length;
    return encoding.GetString(numArray, 0, length);
  }

  public static uint GetMarkerMask(IEnumerable<int> markers)
  {
    uint markerMask = 0;
    foreach (int marker in markers)
      markerMask |= (uint) (1 << marker);
    return markerMask;
  }

  public static uint GetMarkerMask(IEnumerable<Marker> markers)
  {
    uint markerMask = 0;
    foreach (Marker marker in markers)
      markerMask |= marker.Mask;
    return markerMask;
  }

  public static Keys GetKeys(char c)
  {
    switch (c)
    {
      case '-':
        return Keys.OemMinus;
      case '/':
        return Keys.OemQuestion;
      case '[':
        return Keys.OemOpenBrackets;
      case '\\':
        return Keys.OemPipe;
      case ']':
        return Keys.OemCloseBrackets;
      case '`':
        return Keys.Oemtilde;
      default:
        return (Keys) Enum.Parse(typeof (Keys), c.ToString(), true);
    }
  }

  public static Keys GetKeys(string s)
  {
    switch (s)
    {
      case "/":
        return Keys.OemQuestion;
      case "`":
        return Keys.Oemtilde;
      case "[":
        return Keys.OemOpenBrackets;
      case "\\":
        return Keys.OemPipe;
      case "]":
        return Keys.OemCloseBrackets;
      case "-":
        return Keys.OemMinus;
      default:
        return (Keys) Enum.Parse(typeof (Keys), s, true);
    }
  }
}
