#nullable disable
namespace ScintillaNet.Configuration;

public struct CommandBindingConfig(
  KeyBinding keyBinding,
  bool? replaceCurrent,
  BindableCommand bindableCommand)
{
  public KeyBinding KeyBinding = keyBinding;
  public bool? ReplaceCurrent = replaceCurrent;
  public BindableCommand BindableCommand = bindableCommand;
}
