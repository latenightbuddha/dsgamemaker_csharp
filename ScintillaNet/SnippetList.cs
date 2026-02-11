using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

#nullable disable
namespace ScintillaNet;

public class SnippetList : KeyedCollection<string, Snippet>
{
  private SnippetManager _manager;

  internal SnippetList(SnippetManager manager)
  {
    if (this._manager == null)
      return;
    this._manager = manager;
  }

  protected override string GetKeyForItem(Snippet item) => item.Shortcut;

  public Snippet Add(string shortcut, string code)
  {
    return this.Add(shortcut, code, this._manager.DefaultDelimeter);
  }

  public Snippet Add(string shortcut, string code, bool isSurroundsWith)
  {
    return this.Add(shortcut, code, this._manager.DefaultDelimeter, isSurroundsWith);
  }

  public Snippet Add(string shortcut, string code, char delimeter)
  {
    return this.Add(shortcut, code, delimeter, false);
  }

  public Snippet Add(string shortcut, string code, char delimeter, bool isSurroundsWith)
  {
    Snippet snippet = new Snippet(shortcut, code, delimeter, isSurroundsWith);
    this.Add(snippet);
    return snippet;
  }

  public bool TryGetValue(string key, out Snippet snippet)
  {
    if (this.Contains(key))
    {
      snippet = this[key];
      return true;
    }
    snippet = (Snippet) null;
    return false;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Snippet snippet in (IEnumerable<Snippet>) this.Items)
      stringBuilder.Append(snippet.Shortcut).Append(" ");
    if (stringBuilder.Length > 0)
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
    return stringBuilder.ToString();
  }

  public void Sort()
  {
    Snippet[] array = new Snippet[this.Count];
    this.CopyTo(array, 0);
    Array.Sort<Snippet>(array);
    this.Clear();
    this.AddRange((IEnumerable<Snippet>) array);
  }

  public void AddRange(IEnumerable<Snippet> snippets)
  {
    foreach (Snippet snippet in snippets)
      this.Add(snippet);
  }
}
