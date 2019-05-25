using System.Collections.Generic;
using System.Linq;
using Hoplon.Domain.Interface;
using Hoplon.Domain.Models;
using Hoplon.Domain.Utilitarios;
using Hoplon.Domain.Utilitarios.ExtensionMethods;

namespace Hoplon.Domain.Implementation
{
  public class Aux
  {
    public int subIndex { get; set; }
  }

  public class HoplonCollection : IHoplonCollection
  {
    private List<CollectionHoplon> _collectionHoplon = new List<CollectionHoplon>();

    public bool Add(string key, int subIndex, string value)
    {
      var collection = new CollectionHoplon();

      // Ja existe a chave?
      var chave = _collectionHoplon.FirstOrDefault(s => s.Key == key);
      if (chave != null)
      {
        // Já existe o valor em algum subIndex?
        var anyValue = chave.Value.Any(s => s.Value.Any(x => x == value));

        if (anyValue)
        {
          // Se já existe qual é o subIndex?
          var subIndexFind = Utils.GetIndexValue(chave.Value, value);

          if (subIndexFind != subIndex)
          {
            // Remover do subIndex encontrado
            var dictIndex = chave.Value.Where(s => s.Key == subIndexFind).FirstOrDefault();
            dictIndex.Value.RemoveOrdered(value);

            // Adiciona no subIndex atual, ele já existe? 
            var existSubIndex = chave.Value.Any(s => s.Key == subIndex);

            if (!existSubIndex)
            {
              var valor = new KeyValuePair<int, List<string>>(subIndex, new List<string> { value });
              chave.Value.AddOrdered(valor);
              return false;
            }
            else
            {
              chave.Value.First(s => s.Key == subIndex).Value.AddOrdered(value);
              return false;
            }
          }
        }
        // O valor não existe em nenhum subIndex.
        else
        {
          // Já existe o subIndex?
          var anySubIndex = chave.Value.Any(s => s.Key == subIndex);

          if (anySubIndex)
          {
            chave.Value.First(s => s.Key == subIndex).Value.AddOrdered(value);
            return true;
          }
          else
          {
            var valor = new KeyValuePair<int, List<string>>(subIndex, new List<string> { value });
            chave.Value.AddOrdered(valor);
            return true;
          }
        }
      }
      else
      {
        var valor = new KeyValuePair<int, List<string>>(subIndex, new List<string> { value });
        collection.Key = key;
        collection.Value.AddOrdered(valor);
        _collectionHoplon.AddOrdered(collection);

        return true;
      }
      return false;
    }

    public IList<string> Get(string key, int start, int end)
    {
      throw new System.NotImplementedException();
    }

    public bool Remove(string key)
    {
      throw new System.NotImplementedException();
    }

    public bool RemoveValuesFromSubIndex(string key, int subIndex)
    {
      throw new System.NotImplementedException();
    }

    /// <summary>
    /// Este método está implementado somente para apoiar nos testes automatizados... 
    /// Talvez eu ainda irei retirar este método caso necessário.
    /// </summary>
    /// <returns></returns>
    public IList<CollectionHoplon> RetornoTeste()
    {
      return _collectionHoplon;
    }
  }
}
