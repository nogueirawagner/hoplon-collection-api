using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoplon.Domain.Interface;
using Hoplon.Domain.Models;
using Hoplon.Domain.Utilitarios;
using Hoplon.Domain.Utilitarios.ExtensionMethods;

namespace Hoplon.Domain.Implementation
{
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
      IEnumerable<string> retorno;
      var chave = _collectionHoplon.Where(s => s.Key == key);

      retorno = chave.SelectMany(s => s.Value.SelectMany(x => x.Value).AsEnumerable()).AsEnumerable();
      int qtdElementos = retorno.Count();

      if (end > 0)
      {
        return retorno.Skip(start.GetValueValid()).Take(end).ToList();
      }
      else if (end == 0)
      {
        return retorno.Skip(start.GetValueValid()).ToList();
      }
      else if (end < 0)
      {
        if (end == -1)
          return retorno.Skip(start.GetValueValid()).Take(qtdElementos).ToList();
        if (end < -1)
        {
          end *= (-1);
          end = (end - 1) > qtdElementos ? qtdElementos : (qtdElementos - end) + 1;
          return retorno.Skip(start.GetValueValid()).Take(end).ToList();
        }
      }

      return retorno.Skip(start.GetValueValid()).Take(end).ToList();
    }

    public long IndexOf(string key, string value)
    {
      List<string> retorno;
      var chave = _collectionHoplon.Where(s => s.Key == key);

      retorno = chave.SelectMany(s => s.Value.SelectMany(x => x.Value).AsEnumerable()).AsEnumerable().ToList();
      return retorno.ToLower().IndexOf(value.ToLower());
    }

    public bool Remove(string key)
    {
      var chave = _collectionHoplon.FirstOrDefault(s => s.Key == key);
      _collectionHoplon.Remove(chave);

      return chave != null ? true : false;
    }

    public bool RemoveValuesFromSubIndex(string key, int subIndex)
    {
      var chave = _collectionHoplon.FirstOrDefault(s => s.Key == key);
      if (chave != null)
      {
        var sub = chave.Value.FirstOrDefault(s => s.Key == subIndex);
        chave.Value.Remove(sub);

        return sub.Value != null ? true : false;
      }
      return false;
    }

    /// <summary>
    /// Este método está implementado somente para apoiar nos testes automatizados... 
    /// Talvez eu ainda irei retirar este método caso necessário.
    /// </summary>
    /// <returns></returns>
    public IList<CollectionHoplon> RetornoApoioTeste()
    {
      return _collectionHoplon;
    }
  }
}
