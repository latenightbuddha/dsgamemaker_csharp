using System.Windows.Forms;

#nullable disable
namespace ScintillaNet;

public struct KeyBinding(Keys keycode, Keys modifiers)
{
  private Keys _keycode = keycode;
  private Keys _modifiers = modifiers;

  public Keys KeyCode
  {
    get => this._keycode;
    set => this._keycode = value;
  }

  public Keys Modifiers
  {
    get => this._modifiers;
    set => this._modifiers = value;
  }

  public override string ToString()
  {
    return ((int) this._keycode).ToString() + ((int) this._modifiers).ToString();
  }

  public override int GetHashCode() => this.ToString().GetHashCode();

  public override bool Equals(object obj)
  {
    return obj is KeyBinding keyBinding && this._keycode == keyBinding._keycode && this._modifiers == keyBinding._modifiers;
  }
}
