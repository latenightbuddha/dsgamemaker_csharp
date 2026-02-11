using System.Collections.Generic;

#nullable disable
namespace ScintillaNet;

public class SnippetLink
{
  private string _key;
  private List<SnippetLinkRange> _ranges = new List<SnippetLinkRange>();

  public string Key
  {
    get => this._key;
    set => this._key = value;
  }

  public List<SnippetLinkRange> Ranges
  {
    get => this._ranges;
    set => this._ranges = value;
  }

  public SnippetLink(string key) => this._key = key;
}
