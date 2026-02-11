using System;
using System.Collections.Generic;

#nullable disable
namespace ScintillaNet;

public class Snippet : IComparable<Snippet>
{
  internal const char RealDelimeter = '\u0001';
  public char DefaultDelimeter = '$';
  private string _realCode;
  private string _shortcut;
  private char _delimeter;
  private string _code;
  private List<string> _languages = new List<string>();
  private bool _isSurroundsWith;

  public Snippet(string shortcut, string code)
    : this(shortcut, code, '$', false)
  {
  }

  public Snippet(string shortcut, string code, char delimeter, bool isSurroundsWith)
  {
    this._isSurroundsWith = isSurroundsWith;
    this._shortcut = shortcut;
    this._delimeter = delimeter;
    this.Code = code;
  }

  internal string RealCode
  {
    get => this._realCode;
    set => this._realCode = value;
  }

  public string Shortcut
  {
    get => this._shortcut;
    set => this._shortcut = value;
  }

  public char Delimeter
  {
    get => this._delimeter;
    set => this._delimeter = value;
  }

  public string Code
  {
    get => this._code;
    set
    {
      this._code = value;
      this._realCode = this._code.Replace(this._delimeter, '\u0001');
    }
  }

  public List<string> Languages
  {
    get => this._languages;
    set => this._languages = value;
  }

  public bool IsSurroundsWith
  {
    get => this._isSurroundsWith;
    set => this._isSurroundsWith = value;
  }

  public int CompareTo(Snippet other)
  {
    return StringComparer.OrdinalIgnoreCase.Compare(this._shortcut, other._shortcut);
  }
}
