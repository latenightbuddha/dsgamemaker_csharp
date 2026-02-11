using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class Commands : TopLevelHelper
{
  private Dictionary<KeyBinding, List<BindableCommand>> _boundCommands = new Dictionary<KeyBinding, List<BindableCommand>>();
  private Commands.CommandComparer _commandComparer = new Commands.CommandComparer();
  internal bool StopProcessingCommands;
  private bool _allowDuplicateBindings = true;

  internal Commands(Scintilla scintilla)
    : base(scintilla)
  {
    this.NativeScintilla.ClearAllCmdKeys();
    this._commandComparer.CommandOrder.Add(BindableCommand.AutoCShow, 100);
    this._commandComparer.CommandOrder.Add(BindableCommand.AutoCComplete, 100);
    this._commandComparer.CommandOrder.Add(BindableCommand.AutoCCancel, 100);
    this._commandComparer.CommandOrder.Add(BindableCommand.DoSnippetCheck, 200);
    this._commandComparer.CommandOrder.Add(BindableCommand.AcceptActiveSnippets, 200);
    this._commandComparer.CommandOrder.Add(BindableCommand.CancelActiveSnippets, 200);
    this._commandComparer.CommandOrder.Add(BindableCommand.NextSnippetRange, 200);
    this._commandComparer.CommandOrder.Add(BindableCommand.PreviousSnippetRange, 200);
    this.AddBinding(Keys.Down, Keys.None, BindableCommand.LineDown);
    this.AddBinding(Keys.Down, Keys.Shift, BindableCommand.LineDownExtend);
    this.AddBinding(Keys.Down, Keys.Control, BindableCommand.LineScrollDown);
    this.AddBinding(Keys.Down, Keys.Shift | Keys.Alt, BindableCommand.LineDownRectExtend);
    this.AddBinding(Keys.Up, Keys.None, BindableCommand.LineUp);
    this.AddBinding(Keys.Up, Keys.Shift, BindableCommand.LineUpExtend);
    this.AddBinding(Keys.Up, Keys.Control, BindableCommand.LineScrollUp);
    this.AddBinding(Keys.Up, Keys.Shift | Keys.Alt, BindableCommand.LineUpRectExtend);
    this.AddBinding('[', Keys.Control, BindableCommand.ParaUp);
    this.AddBinding('[', Keys.Shift | Keys.Control, BindableCommand.ParaUpExtend);
    this.AddBinding(']', Keys.Control, BindableCommand.ParaDown);
    this.AddBinding(']', Keys.Shift | Keys.Control, BindableCommand.ParaDownExtend);
    this.AddBinding(Keys.Left, Keys.None, BindableCommand.CharLeft);
    this.AddBinding(Keys.Left, Keys.Shift, BindableCommand.CharLeftExtend);
    this.AddBinding(Keys.Left, Keys.Control, BindableCommand.WordLeft);
    this.AddBinding(Keys.Left, Keys.Shift | Keys.Control, BindableCommand.WordLeftExtend);
    this.AddBinding(Keys.Left, Keys.Shift | Keys.Alt, BindableCommand.CharLeftRectExtend);
    this.AddBinding(Keys.Right, Keys.None, BindableCommand.CharRight);
    this.AddBinding(Keys.Right, Keys.Shift, BindableCommand.CharRightExtend);
    this.AddBinding(Keys.Right, Keys.Control, BindableCommand.WordRight);
    this.AddBinding(Keys.Right, Keys.Shift | Keys.Control, BindableCommand.WordRightExtend);
    this.AddBinding(Keys.Right, Keys.Shift | Keys.Alt, BindableCommand.CharRightRectExtend);
    this.AddBinding('/', Keys.Control, BindableCommand.WordPartLeft);
    this.AddBinding('/', Keys.Shift | Keys.Control, BindableCommand.WordPartLeftExtend);
    this.AddBinding('\\', Keys.Control, BindableCommand.WordPartRight);
    this.AddBinding('\\', Keys.Shift | Keys.Control, BindableCommand.WordPartRightExtend);
    this.AddBinding(Keys.Home, Keys.None, BindableCommand.VCHome);
    this.AddBinding(Keys.Home, Keys.Shift, BindableCommand.VCHomeExtend);
    this.AddBinding(Keys.Home, Keys.Control, BindableCommand.DocumentStart);
    this.AddBinding(Keys.Home, Keys.Shift | Keys.Control, BindableCommand.DocumentStartExtend);
    this.AddBinding(Keys.Home, Keys.Alt, BindableCommand.HomeDisplay);
    this.AddBinding(Keys.Home, Keys.Shift | Keys.Alt, BindableCommand.VCHomeRectExtend);
    this.AddBinding(Keys.End, Keys.None, BindableCommand.LineEnd);
    this.AddBinding(Keys.End, Keys.Shift, BindableCommand.LineEndExtend);
    this.AddBinding(Keys.End, Keys.Control, BindableCommand.DocumentEnd);
    this.AddBinding(Keys.End, Keys.Shift | Keys.Control, BindableCommand.DocumentEndExtend);
    this.AddBinding(Keys.End, Keys.Alt, BindableCommand.LineEndDisplay);
    this.AddBinding(Keys.End, Keys.Shift | Keys.Alt, BindableCommand.LineEndRectExtend);
    this.AddBinding(Keys.Prior, Keys.None, BindableCommand.PageUp);
    this.AddBinding(Keys.Prior, Keys.Shift, BindableCommand.PageUpExtend);
    this.AddBinding(Keys.Prior, Keys.Shift | Keys.Alt, BindableCommand.PageUpRectExtend);
    this.AddBinding(Keys.Next, Keys.None, BindableCommand.PageDown);
    this.AddBinding(Keys.Next, Keys.Shift, BindableCommand.PageDownExtend);
    this.AddBinding(Keys.Next, Keys.Shift | Keys.Alt, BindableCommand.PageDownRectExtend);
    this.AddBinding(Keys.Delete, Keys.None, BindableCommand.Clear);
    this.AddBinding(Keys.Delete, Keys.Shift, BindableCommand.Cut);
    this.AddBinding(Keys.Delete, Keys.Control, BindableCommand.DelWordRight);
    this.AddBinding(Keys.Delete, Keys.Shift | Keys.Control, BindableCommand.DelLineRight);
    this.AddBinding(Keys.Insert, Keys.None, BindableCommand.EditToggleOvertype);
    this.AddBinding(Keys.Insert, Keys.Shift, BindableCommand.Paste);
    this.AddBinding(Keys.Insert, Keys.Control, BindableCommand.Copy);
    this.AddBinding(Keys.Escape, Keys.None, BindableCommand.Cancel);
    this.AddBinding(Keys.Back, Keys.None, BindableCommand.DeleteBack);
    this.AddBinding(Keys.Back, Keys.Shift, BindableCommand.DeleteBack);
    this.AddBinding(Keys.Back, Keys.Control, BindableCommand.DelWordLeft);
    this.AddBinding(Keys.Back, Keys.Alt, BindableCommand.Undo);
    this.AddBinding(Keys.Back, Keys.Shift | Keys.Control, BindableCommand.DelLineLeft);
    this.AddBinding(Keys.Z, Keys.Control, BindableCommand.Undo);
    this.AddBinding(Keys.Y, Keys.Control, BindableCommand.Redo);
    this.AddBinding(Keys.X, Keys.Control, BindableCommand.Cut);
    this.AddBinding(Keys.C, Keys.Control, BindableCommand.Copy);
    this.AddBinding(Keys.V, Keys.Control, BindableCommand.Paste);
    this.AddBinding(Keys.A, Keys.Control, BindableCommand.SelectAll);
    this.AddBinding(Keys.Tab, Keys.None, BindableCommand.Tab);
    this.AddBinding(Keys.Tab, Keys.Shift, BindableCommand.BackTab);
    this.AddBinding(Keys.Return, Keys.None, BindableCommand.NewLine);
    this.AddBinding(Keys.Return, Keys.Shift, BindableCommand.NewLine);
    this.AddBinding(Keys.Add, Keys.Control, BindableCommand.ZoomIn);
    this.AddBinding(Keys.Subtract, Keys.Control, BindableCommand.ZoomOut);
    this.AddBinding(Keys.Divide, Keys.Control, BindableCommand.SetZoom);
    this.AddBinding(Keys.L, Keys.Control, BindableCommand.LineCut);
    this.AddBinding(Keys.L, Keys.Shift | Keys.Control, BindableCommand.LineDelete);
    this.AddBinding(Keys.T, Keys.Shift | Keys.Control, BindableCommand.LineCopy);
    this.AddBinding(Keys.T, Keys.Control, BindableCommand.LineTranspose);
    this.AddBinding(Keys.D, Keys.Control, BindableCommand.SelectionDuplicate);
    this.AddBinding(Keys.U, Keys.Control, BindableCommand.LowerCase);
    this.AddBinding(Keys.U, Keys.Shift | Keys.Control, BindableCommand.UpperCase);
    this.AddBinding(Keys.Space, Keys.Control, BindableCommand.AutoCShow);
    this.AddBinding(Keys.Tab, BindableCommand.DoSnippetCheck);
    this.AddBinding(Keys.Tab, BindableCommand.NextSnippetRange);
    this.AddBinding(Keys.Tab, Keys.Shift, BindableCommand.PreviousSnippetRange);
    this.AddBinding(Keys.Escape, BindableCommand.CancelActiveSnippets);
    this.AddBinding(Keys.Return, BindableCommand.AcceptActiveSnippets);
    this.AddBinding(Keys.P, Keys.Control, BindableCommand.Print);
    this.AddBinding(Keys.P, Keys.Shift | Keys.Control, BindableCommand.PrintPreview);
    this.AddBinding(Keys.F, Keys.Control, BindableCommand.ShowFind);
    this.AddBinding(Keys.H, Keys.Control, BindableCommand.ShowReplace);
    this.AddBinding(Keys.F3, BindableCommand.FindNext);
    this.AddBinding(Keys.F3, Keys.Shift, BindableCommand.FindPrevious);
    this.AddBinding(Keys.I, Keys.Control, BindableCommand.IncrementalSearch);
    this.AddBinding(Keys.Q, Keys.Control, BindableCommand.LineComment);
    this.AddBinding(Keys.Q, Keys.Shift | Keys.Control, BindableCommand.LineUncomment);
    this.AddBinding('-', Keys.Control, BindableCommand.DocumentNavigateBackward);
    this.AddBinding('-', Keys.Shift | Keys.Control, BindableCommand.DocumentNavigateForward);
    this.AddBinding(Keys.J, Keys.Control, BindableCommand.ShowSnippetList);
    this.AddBinding(Keys.M, Keys.Control, BindableCommand.DropMarkerDrop);
    this.AddBinding(Keys.Escape, BindableCommand.DropMarkerCollect);
    this.AddBinding(Keys.G, Keys.Control, BindableCommand.ShowGoTo);
  }

  internal bool ShouldSerialize() => this.ShouldSerializeAllowDuplicateBindings();

  public bool AllowDuplicateBindings
  {
    get => this._allowDuplicateBindings;
    set => this._allowDuplicateBindings = value;
  }

  private bool ShouldSerializeAllowDuplicateBindings() => !this._allowDuplicateBindings;

  private void ResetAllowDuplicateBindings() => this._allowDuplicateBindings = true;

  public void AddBinding(Keys shortcut, Keys modifiers, BindableCommand command)
  {
    KeyBinding key = new KeyBinding(shortcut, modifiers);
    if (!this._boundCommands.ContainsKey(key))
      this._boundCommands.Add(key, new List<BindableCommand>());
    List<BindableCommand> boundCommand = this._boundCommands[key];
    if (!this._allowDuplicateBindings && boundCommand.Contains(command))
      return;
    boundCommand.Add(command);
  }

  public void AddBinding(Keys shortcut, BindableCommand command)
  {
    this.AddBinding(shortcut, Keys.None, command);
  }

  public void AddBinding(char shortcut, BindableCommand command)
  {
    this.AddBinding(Utilities.GetKeys(shortcut), Keys.None, command);
  }

  public void AddBinding(char shortcut, Keys modifiers, BindableCommand command)
  {
    this.AddBinding(Utilities.GetKeys(shortcut), modifiers, command);
  }

  public void RemoveBinding(Keys shortcut, Keys modifiers, BindableCommand command)
  {
    KeyBinding key = new KeyBinding(shortcut, modifiers);
    if (!this._boundCommands.ContainsKey(key))
      return;
    this._boundCommands[key].Remove(command);
  }

  public void RemoveBinding(Keys shortcut, BindableCommand command)
  {
    this.RemoveBinding(shortcut, Keys.None, command);
  }

  public void RemoveBinding(char shortcut, BindableCommand command)
  {
    this.RemoveBinding(Utilities.GetKeys(shortcut), Keys.None, command);
  }

  public void RemoveBinding(char shortcut, Keys modifiers, BindableCommand command)
  {
    this.RemoveBinding(Utilities.GetKeys(shortcut), modifiers, command);
  }

  public void RemoveBinding(Keys shortcut, Keys modifiers)
  {
    this._boundCommands.Remove(new KeyBinding(shortcut, modifiers));
  }

  public void RemoveBinding(Keys shortcut) => this.RemoveBinding(shortcut, Keys.None);

  public void RemoveBinding(char shortcut)
  {
    this.RemoveBinding(Utilities.GetKeys(shortcut), Keys.None);
  }

  public void RemoveBinding(char shortcut, Keys modifiers)
  {
    this.RemoveBinding(Utilities.GetKeys(shortcut), modifiers);
  }

  public void RemoveAllBindings() => this._boundCommands.Clear();

  private List<BindableCommand> GetCommands(Keys shortcut, Keys modifiers)
  {
    KeyBinding key = new KeyBinding(shortcut, modifiers);
    return !this._boundCommands.ContainsKey(key) ? new List<BindableCommand>() : this._boundCommands[key];
  }

  private List<BindableCommand> GetCommands(Keys shortcut) => this.GetCommands(shortcut, Keys.None);

  private List<BindableCommand> GetCommands(char shortcut)
  {
    return this.GetCommands(Utilities.GetKeys(shortcut), Keys.None);
  }

  private List<BindableCommand> GetCommands(char shortcut, Keys modifiers)
  {
    return this.GetCommands(Utilities.GetKeys(shortcut), modifiers);
  }

  public List<KeyBinding> GetKeyBindings(BindableCommand command)
  {
    List<KeyBinding> keyBindings = new List<KeyBinding>();
    foreach (KeyValuePair<KeyBinding, List<BindableCommand>> boundCommand in this._boundCommands)
    {
      if (boundCommand.Value.Contains(command))
        keyBindings.Add(boundCommand.Key);
    }
    return keyBindings;
  }

  public bool Execute(BindableCommand command)
  {
    if (command < (BindableCommand) 10000)
    {
      this.NativeScintilla.SendMessageDirect((uint) command, IntPtr.Zero, IntPtr.Zero);
      return true;
    }
    switch (command - 10001)
    {
      case (BindableCommand) 0:
        this.Scintilla.AutoComplete.Show();
        return true;
      case (BindableCommand) 1:
        return this.Scintilla.Snippets.DoSnippetCheck();
      case (BindableCommand) 2:
        return this.Scintilla.Snippets.NextSnippetRange();
      case (BindableCommand) 3:
        return this.Scintilla.Snippets.PreviousSnippetRange();
      case (BindableCommand) 4:
        return this.Scintilla.Snippets.CancelActiveSnippets();
      case (BindableCommand) 5:
        return this.Scintilla.Snippets.AcceptActiveSnippets();
      case (BindableCommand) 6:
        this.Scintilla.DropMarkers.Drop();
        return true;
      case (BindableCommand) 7:
        this.Scintilla.DropMarkers.Collect();
        return false;
      case (BindableCommand) 8:
        this.Scintilla.Printing.Print();
        return true;
      case (BindableCommand) 9:
        int num = (int) this.Scintilla.Printing.PrintPreview();
        return true;
      case (BindableCommand) 10:
        this.Scintilla.FindReplace.ShowFind();
        return true;
      case (BindableCommand) 11:
        this.Scintilla.FindReplace.ShowReplace();
        return true;
      case (BindableCommand) 12:
        this.Scintilla.FindReplace.Window.FindNext();
        return true;
      case (BindableCommand) 13:
        this.Scintilla.FindReplace.Window.FindPrevious();
        return true;
      case (BindableCommand) 14:
        this.Scintilla.FindReplace.IncrementalSearch();
        return true;
      case (BindableCommand) 15:
        this.Scintilla.Lexing.LineComment();
        return true;
      case (BindableCommand) 16 /*0x10*/:
        this.Scintilla.Lexing.LineUncomment();
        return true;
      case (BindableCommand) 17:
        this.Scintilla.DocumentNavigation.NavigateBackward();
        return true;
      case (BindableCommand) 18:
        this.Scintilla.DocumentNavigation.NavigateForward();
        return true;
      case (BindableCommand) 19:
        this.Scintilla.Lexing.ToggleLineComment();
        return true;
      case (BindableCommand) 20:
        this.Scintilla.Lexing.StreamComment();
        return true;
      case (BindableCommand) 21:
        this.Scintilla.Snippets.ShowSnippetList();
        return true;
      case (BindableCommand) 23:
        this.Scintilla.GoTo.ShowGoToDialog();
        break;
    }
    return false;
  }

  internal bool ProcessKey(KeyEventArgs e)
  {
    this.StopProcessingCommands = false;
    KeyBinding key = new KeyBinding(e.KeyCode, e.Modifiers);
    if (!this._boundCommands.ContainsKey(key))
      return false;
    List<BindableCommand> boundCommand = this._boundCommands[key];
    if (boundCommand.Count == 0)
      return false;
    boundCommand.Sort((IComparer<BindableCommand>) this._commandComparer);
    bool flag = false;
    foreach (BindableCommand command in boundCommand)
    {
      flag |= this.Execute(command);
      if (this.StopProcessingCommands)
        return flag;
    }
    return flag;
  }

  private class CommandComparer : IComparer<BindableCommand>
  {
    private Dictionary<BindableCommand, int> _commandOrder = new Dictionary<BindableCommand, int>();

    public Dictionary<BindableCommand, int> CommandOrder
    {
      get => this._commandOrder;
      set => this._commandOrder = value;
    }

    private int getCommandOrder(BindableCommand cmd)
    {
      return !this._commandOrder.ContainsKey(cmd) ? 0 : this._commandOrder[cmd];
    }

    public int Compare(BindableCommand x, BindableCommand y)
    {
      return this.getCommandOrder(y).CompareTo(this.getCommandOrder(x));
    }
  }
}
