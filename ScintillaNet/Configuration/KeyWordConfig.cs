#nullable disable
namespace ScintillaNet.Configuration;

public class KeyWordConfig
{
  private int _list;
  private string _value;
  private bool? _inherit;

  public int List
  {
    get => this._list;
    set => this._list = value;
  }

  public string Value
  {
    get => this._value;
    set => this._value = value;
  }

  public bool? Inherit
  {
    get => this._inherit;
    set => this._inherit = value;
  }

  public KeyWordConfig(int list, string value, bool? inherit)
  {
    this._list = list;
    this._value = value;
    this._inherit = inherit;
  }
}
