using System;
using System.Collections.Generic;
using System.Text;

namespace Hoplon.Domain.Utilitarios.ExtensionMethods
{
  public static class ExtensionMethod
  {
    public static bool AddOrdered(this List<string> list, string value)
    {
      list.Add(value);
      list.Sort();
      return true;
    }

    public static bool AddOrdered(this List<KeyValuePair<int, List<string>>> list, KeyValuePair<int, List<string>> value)
    {
      list.Add(value);

      return true;
    }

    
    public static bool RemoveOrdered(this List<string> list, string value)
    {
      list.Remove(value);
      list.Sort();
      return true;
    }
  }
}
