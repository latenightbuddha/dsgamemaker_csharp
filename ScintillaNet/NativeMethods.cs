using System;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace ScintillaNet;

internal static class NativeMethods
{
  internal const int WM_DROPFILES = 563;
  internal const int WM_NOTIFY = 78;
  internal const int WM_PAINT = 15;
  internal const int WM_HSCROLL = 276;
  internal const int WM_VSCROLL = 277;
  internal const int WM_DESTROY = 2;
  internal const int WM_GETTEXT = 13;
  internal const int WM_GETTEXTLENGTH = 14;
  internal const int WM_SETCURSOR = 32 /*0x20*/;
  internal const int WM_USER = 1024 /*0x0400*/;
  internal const int WM_REFLECT = 8192 /*0x2000*/;
  internal const int ERROR_MOD_NOT_FOUND = 126;
  internal static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);

  [DllImport("user32.dll")]
  internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

  [DllImport("user32.dll")]
  internal static extern bool GetUpdateRect(IntPtr hWnd, out RECT lpRect, bool bErase);

  [DllImport("shell32.dll")]
  internal static extern uint DragQueryFile(
    IntPtr hDrop,
    uint iFile,
    [Out] StringBuilder lpszFile,
    uint cch);

  [DllImport("shell32.dll")]
  internal static extern int DragFinish(IntPtr hDrop);

  [DllImport("shell32.dll")]
  internal static extern void DragAcceptFiles(IntPtr hwnd, bool accept);

  [DllImport("kernel32", SetLastError = true)]
  internal static extern IntPtr LoadLibrary(string lpLibFileName);
}
