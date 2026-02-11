using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace ScintillaNet.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
[CompilerGenerated]
[DebuggerNonUserCode]
internal class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Resources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (object.ReferenceEquals((object) ScintillaNet.Properties.Resources.resourceMan, (object) null))
        ScintillaNet.Properties.Resources.resourceMan = new ResourceManager("ScintillaNet.Properties.Resources", typeof (ScintillaNet.Properties.Resources).Assembly);
      return ScintillaNet.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => ScintillaNet.Properties.Resources.resourceCulture;
    set => ScintillaNet.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap DeleteHS
  {
    get
    {
      return (Bitmap) ScintillaNet.Properties.Resources.ResourceManager.GetObject(nameof (DeleteHS), ScintillaNet.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap GoToNextMessage
  {
    get
    {
      return (Bitmap) ScintillaNet.Properties.Resources.ResourceManager.GetObject(nameof (GoToNextMessage), ScintillaNet.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap GoToNextMessage___Copy
  {
    get
    {
      return (Bitmap) ScintillaNet.Properties.Resources.ResourceManager.GetObject("GoToNextMessage - Copy", ScintillaNet.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap GoToPreviousMessage
  {
    get
    {
      return (Bitmap) ScintillaNet.Properties.Resources.ResourceManager.GetObject(nameof (GoToPreviousMessage), ScintillaNet.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap LineColorHS
  {
    get
    {
      return (Bitmap) ScintillaNet.Properties.Resources.ResourceManager.GetObject(nameof (LineColorHS), ScintillaNet.Properties.Resources.resourceCulture);
    }
  }
}
