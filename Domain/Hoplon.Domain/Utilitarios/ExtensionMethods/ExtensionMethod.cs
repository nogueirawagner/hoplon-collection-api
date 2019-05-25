using Hoplon.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    #region Refatorar
#pragma warning disable CS1030 // diretiva de #aviso
#warning Alterar a abordagem de KeyValuePair para dict ou outra estrutura que implemente o IComparable para reduzir a complexidade do algoritmo.

    public static bool AddOrdered(this List<KeyValuePair<int, List<string>>> list, KeyValuePair<int, List<string>> value)
    {
      List<KeyValuePair<int, List<string>>> aux = new List<KeyValuePair<int, List<string>>>();
      list.Add(value);

      // Isso aqui é um cancer, acrescenta mais sobrecarga, pois cria uma copia da lista em vez de classifica no lugar.
      // Infelizmente deixar assim por falta de prazo, e tb por ter feito decisão errada.
      aux.AddRange(list);
      var ordenado = aux.OrderBy(s => s.Key);

      list.Clear();
      list.AddRange(ordenado);

      return true;
    }

    public static bool AddOrdered(this List<CollectionHoplon> list, CollectionHoplon value)
    {
      List<CollectionHoplon> aux = new List<CollectionHoplon>();
      list.Add(value);
      aux.AddRange(list);
      var ordenado = aux.OrderBy(s => s.Key);

      list.Clear();
      list.AddRange(ordenado);
      return true;
    }

#pragma warning restore CS1030 // diretiva de #aviso

    #endregion Refatorar

    public static bool RemoveOrdered(this List<string> list, string value)
    {
      list.Remove(value);
      list.Sort();
      return true;
    }

    public static int GetValueValid(this int start)
    {
      if (start < 0)
        return 0;
      return start;
    }

    public static List<string> ToLower(this List<string> list)
    {
      for (int i = 0; i < list.Count(); i++)
        list[i] = list[i].ToLower();
      return list;
    }
  }
}
