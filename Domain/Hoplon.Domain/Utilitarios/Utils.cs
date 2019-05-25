using System.Collections.Generic;

namespace Hoplon.Domain.Utilitarios
{
  public static class Utils
  {
    /// <summary>
    /// Pega index por valor.
    /// </summary>
    /// <param name="colection"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int GetIndexValue(List<KeyValuePair<int, List<string>>> colection, string value)
    {
      // Utilizar parallel mesmo não sendo thread-safe, 
      // pois neste ponto ñ estou considerando a coleção ser alterada.

      int index = 0;
      foreach (var col in colection)  // o(n) 
      {
        foreach (var val in col.Value) // n O(n) = O(n²)
        {
          if (val == value)  // n O(n) = O(n²)
          {
            index = col.Key; // n O(n) = O(n²)
            break; // n O(n) = O(n²)
          }
        }
      }
      return index; // O(n) 
    }
    // logo esta fn O(3n² + 2n)
    // risca 3 e desconsidera n 
    // = O(n²)
  }
}
