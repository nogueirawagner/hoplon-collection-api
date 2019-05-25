using Hoplon.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public static bool RemoveOrdered(this List<string> list, string value)
    {
      list.Remove(value);
      list.Sort();
      return true;
    }
  }
}
