using System;
using System.ComponentModel;

#nullable disable
namespace ScintillaNet;

public abstract class ScintillaHelperBase : IDisposable
{
  private Scintilla _scintilla;
  private INativeScintilla _nativeScintilla;
  private bool _isDisposed;

  protected internal Scintilla Scintilla
  {
    get => this._scintilla;
    set
    {
      this._scintilla = value;
      this._nativeScintilla = (INativeScintilla) this._scintilla;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool IsDisposed
  {
    get => this._isDisposed;
    set => this._isDisposed = value;
  }

  private void _scintilla_Load(object sender, EventArgs e) => this.Initialize();

  protected internal virtual void Initialize()
  {
  }

  protected internal INativeScintilla NativeScintilla => this._nativeScintilla;

  protected internal ScintillaHelperBase(Scintilla scintilla)
  {
    this._scintilla = scintilla;
    this._nativeScintilla = (INativeScintilla) scintilla;
  }

  public virtual void Dispose() => this._isDisposed = true;

  public abstract override bool Equals(object obj);

  protected bool IsSameHelperFamily(object obj)
  {
    return obj is ScintillaHelperBase scintillaHelperBase && this._scintilla != null && scintillaHelperBase._scintilla != null && this._scintilla.Equals((object) scintillaHelperBase._scintilla) && this.GetType().IsAssignableFrom(obj.GetType());
  }

  public override int GetHashCode() => base.GetHashCode();
}
