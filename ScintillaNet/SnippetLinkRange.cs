using System.Collections.Generic;

#nullable disable
namespace ScintillaNet;

public class SnippetLinkRange : ManagedRange
{
  private string _key;
  private List<SnippetLinkRange> _parent;
  private bool _active;

  public string Key
  {
    get => this._key;
    set => this._key = value;
  }

  public List<SnippetLinkRange> Parent
  {
    get => this._parent;
    set => this._parent = value;
  }

  public SnippetLinkRange(int start, int end, Scintilla scintilla, string key)
  {
    this.Scintilla = scintilla;
    this.Start = start;
    this.End = end;
    this._key = key;
  }

  public override void Dispose()
  {
    if (this.IsDisposed)
      return;
    this._parent.Remove(this);
    base.Dispose();
  }

  internal void Init() => this.Scintilla.ManagedRanges.Add((ManagedRange) this);

  public bool Active
  {
    get => this._active;
    set
    {
      this._active = value;
      if (value)
      {
        this.ClearIndicator(this.Scintilla.Snippets.InactiveSnippetIndicator);
        this.SetIndicator(this.Scintilla.Snippets.ActiveSnippetIndicator);
      }
      else
      {
        this.SetIndicator(this.Scintilla.Snippets.InactiveSnippetIndicator);
        this.ClearIndicator(this.Scintilla.Snippets.ActiveSnippetIndicator);
      }
    }
  }
}
