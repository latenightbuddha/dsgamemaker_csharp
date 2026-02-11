using System;
using System.Collections.Generic;

#nullable disable
namespace ScintillaNet.Configuration;

public class ResolvedStyleList : Dictionary<int, StyleConfig>
{
  public StyleConfig FindByName(string name)
  {
    foreach (StyleConfig byName in this.Values)
    {
      if (byName.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        return byName;
    }
    return (StyleConfig) null;
  }
}
