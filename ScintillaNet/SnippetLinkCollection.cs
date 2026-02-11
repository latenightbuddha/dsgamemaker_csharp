using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace ScintillaNet;

public class SnippetLinkCollection : 
  IDictionary<string, SnippetLink>,
  ICollection<KeyValuePair<string, SnippetLink>>,
  IEnumerable<KeyValuePair<string, SnippetLink>>,
  IList<SnippetLink>,
  ICollection<SnippetLink>,
  IEnumerable<SnippetLink>,
  IEnumerable
{
  private List<SnippetLink> _snippetLinks = new List<SnippetLink>();
  private SnippetLinkEnd _endPoint;
  private int _activeLinkIndex = -1;
  private bool _isActive;
  private SnippetLinkRange _activeRange;

  public void Add(string key, SnippetLink value)
  {
    if (!key.Equals(value.Key, StringComparison.CurrentCultureIgnoreCase))
      throw new ArgumentException("Key argument must == value.Key", nameof (key));
    if (this.ContainsKey(key))
      throw new ArgumentException("Key already exists", nameof (key));
    this._snippetLinks.Add(value);
  }

  public bool ContainsKey(string key)
  {
    foreach (SnippetLink snippetLink in this._snippetLinks)
    {
      if (snippetLink.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase))
        return true;
    }
    return false;
  }

  public ICollection<string> Keys
  {
    get
    {
      string[] keys = new string[this._snippetLinks.Count];
      for (int index = 0; index < this._snippetLinks.Count; ++index)
        keys[index] = this._snippetLinks[index].Key;
      return (ICollection<string>) keys;
    }
  }

  public bool Remove(string key)
  {
    for (int index = 0; index < this._snippetLinks.Count; ++index)
    {
      if (this._snippetLinks[index].Key.Equals(key, StringComparison.CurrentCultureIgnoreCase))
      {
        this._snippetLinks.RemoveAt(index);
        return true;
      }
    }
    return false;
  }

  public bool TryGetValue(string key, out SnippetLink value)
  {
    value = (SnippetLink) null;
    for (int index = 0; index < this._snippetLinks.Count; ++index)
    {
      if (this._snippetLinks[index].Key.Equals(key, StringComparison.CurrentCultureIgnoreCase))
      {
        value = this._snippetLinks[index];
        return true;
      }
    }
    return false;
  }

  public ICollection<SnippetLink> Values
  {
    get
    {
      SnippetLink[] values = new SnippetLink[this._snippetLinks.Count];
      for (int index = 0; index < this._snippetLinks.Count; ++index)
        values[index] = this._snippetLinks[index];
      return (ICollection<SnippetLink>) values;
    }
  }

  public SnippetLink this[string key]
  {
    get
    {
      for (int index = 0; index < this._snippetLinks.Count; ++index)
      {
        if (this._snippetLinks[index].Key.Equals(key, StringComparison.CurrentCultureIgnoreCase))
          return this._snippetLinks[index];
      }
      throw new KeyNotFoundException();
    }
    set
    {
      if (!key.Equals(value.Key, StringComparison.CurrentCultureIgnoreCase))
        throw new ArgumentException("Key argument must == value.Key", nameof (key));
      for (int index = 0; index < this._snippetLinks.Count; ++index)
      {
        if (this._snippetLinks[index].Key.Equals(key, StringComparison.CurrentCultureIgnoreCase))
        {
          this._snippetLinks[index] = value;
          return;
        }
      }
      this._snippetLinks.Add(value);
    }
  }

  public void Add(KeyValuePair<string, SnippetLink> item) => this.Add(item.Key, item.Value);

  public void Clear()
  {
    List<ManagedRange> managedRangeList = new List<ManagedRange>();
    foreach (SnippetLink snippetLink in this._snippetLinks)
    {
      foreach (Range range in snippetLink.Ranges)
      {
        ManagedRange managedRange = range as ManagedRange;
        managedRangeList.Add(managedRange);
      }
    }
    this._snippetLinks.Clear();
    foreach (ScintillaHelperBase scintillaHelperBase in managedRangeList)
      scintillaHelperBase.Dispose();
  }

  public bool Contains(KeyValuePair<string, SnippetLink> item) => this.ContainsKey(item.Key);

  public void CopyTo(KeyValuePair<string, SnippetLink>[] array, int arrayIndex)
  {
    throw new Exception("The method or operation is not implemented.");
  }

  public int Count => this._snippetLinks.Count;

  public bool IsReadOnly => false;

  public bool Remove(KeyValuePair<string, SnippetLink> item) => this.Remove(item.Key);

  public IEnumerator<KeyValuePair<string, SnippetLink>> GetEnumerator()
  {
    throw new Exception("The method or operation is not implemented.");
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._snippetLinks.GetEnumerator();

  public int IndexOf(SnippetLink item) => this._snippetLinks.IndexOf(item);

  public void Insert(int index, SnippetLink item) => this._snippetLinks.Insert(index, item);

  public void RemoveAt(int index) => this._snippetLinks.RemoveAt(index);

  public SnippetLink this[int index]
  {
    get => this._snippetLinks[index];
    set => this._snippetLinks[index] = value;
  }

  public void Add(SnippetLink item) => this.Add(item.Key, item);

  public bool Contains(SnippetLink item) => this._snippetLinks.Contains(item);

  public void CopyTo(SnippetLink[] array, int arrayIndex)
  {
    this._snippetLinks.CopyTo(array, arrayIndex);
  }

  public bool Remove(SnippetLink item) => this._snippetLinks.Remove(item);

  IEnumerator<SnippetLink> IEnumerable<SnippetLink>.GetEnumerator()
  {
    return (IEnumerator<SnippetLink>) this._snippetLinks.GetEnumerator();
  }

  public SnippetLinkEnd EndPoint
  {
    get => this._endPoint;
    set => this._endPoint = value;
  }

  public SnippetLink ActiveSnippetLink
  {
    get
    {
      return this._activeLinkIndex < 0 || this._activeLinkIndex >= this._snippetLinks.Count ? (SnippetLink) null : this._snippetLinks[this._activeLinkIndex];
    }
    set
    {
      if (value == null)
        this._activeLinkIndex = -1;
      else
        this._activeLinkIndex = this._snippetLinks.IndexOf(value);
    }
  }

  public SnippetLink NextActiveSnippetLink
  {
    get
    {
      int activeLinkIndex = this._activeLinkIndex;
      if (activeLinkIndex < 0)
        return (SnippetLink) null;
      int index;
      if ((index = activeLinkIndex + 1) >= this._snippetLinks.Count)
        index = 0;
      return this._snippetLinks[index];
    }
  }

  public SnippetLink PreviousActiveSnippetLink
  {
    get
    {
      int activeLinkIndex = this._activeLinkIndex;
      if (activeLinkIndex < 0)
        return (SnippetLink) null;
      int index;
      if ((index = activeLinkIndex - 1) < 0)
        index = this._snippetLinks.Count - 1;
      return this._snippetLinks[index];
    }
  }

  public bool IsActive
  {
    get => this._isActive;
    set => this._isActive = value;
  }

  public SnippetLinkRange ActiveRange
  {
    get => this._activeRange;
    set => this._activeRange = value;
  }
}
