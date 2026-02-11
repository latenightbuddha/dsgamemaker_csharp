#nullable disable
namespace ScintillaNet.Configuration;

public class SnippetsConfig
{
  private string _shortcut;
  private string _code;
  private char? _delimeter;
  private bool? _isSurroundsWith;

  public string Shortcut
  {
    get => this._shortcut;
    set => this._shortcut = value;
  }

  public string Code
  {
    get => this._code;
    set => this._code = value;
  }

  public char? Delimeter
  {
    get => this._delimeter;
    set => this._delimeter = value;
  }

  public bool? IsSurroundsWith
  {
    get => this._isSurroundsWith;
    set => this._isSurroundsWith = value;
  }
}
