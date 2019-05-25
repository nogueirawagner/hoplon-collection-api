using System.Collections.Generic;

namespace Hoplon.Domain.Utilitarios
{
  public static class Utils
  {
    public static int GetIndexValue(List<KeyValuePair<int, List<string>>> colection, string value)
    {
      int index = 0;
      foreach (var col in colection)
      {
        foreach (var val in col.Value)
        {
          if (val == value)
          {
            index = col.Key;
            break;
          }
        }
      }
      return index;
    }
  }
}
