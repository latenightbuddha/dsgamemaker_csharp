using System.ComponentModel;

#nullable disable
namespace ScintillaNet;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class UndoRedo : TopLevelHelper
{
  internal UndoRedo(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize() => this.ShouldSerializeIsUndoEnabled();

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool CanUndo => this.NativeScintilla.CanUndo();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool CanRedo => this.NativeScintilla.CanRedo();

  public bool IsUndoEnabled
  {
    get => this.NativeScintilla.GetUndoCollection();
    set => this.NativeScintilla.SetUndoCollection(value);
  }

  private bool ShouldSerializeIsUndoEnabled() => !this.IsUndoEnabled;

  private void ResetIsUndoEnabled() => this.IsUndoEnabled = true;

  public void BeginUndoAction() => this.NativeScintilla.BeginUndoAction();

  public void EndUndoAction() => this.NativeScintilla.EndUndoAction();

  public void Undo() => this.NativeScintilla.Undo();

  public void Redo() => this.NativeScintilla.Redo();

  public void EmptyUndoBuffer() => this.NativeScintilla.EmptyUndoBuffer();
}
