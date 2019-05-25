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
      var collection = new CollectionHoplon(); // o(n)

      // Ja existe a chave?
      var chave = _collectionHoplon.FirstOrDefault(s => s.Key == key);  // o(n)
      if (chave != null) // o(n)
      {
        // Já existe o valor em algum subIndex?
        var anyValue = chave.Value.Any(s => s.Value.Any(x => x == value)); // o(n)

        if (anyValue)
        {
          // Se já existe qual é o subIndex?
          var subIndexFind = Utils.GetIndexValue(chave.Value, value); // O(n²)

          if (subIndexFind != subIndex) // O(n)
          {
            // Remover do subIndex encontrado
            var dictIndex = chave.Value.Where(s => s.Key == subIndexFind).FirstOrDefault(); // O(n) na verdade nao sei o que faz por trás... 
            dictIndex.Value.RemoveOrdered(value); // O(n)

            // Adiciona no subIndex atual, ele já existe? 
            var existSubIndex = chave.Value.Any(s => s.Key == subIndex);  // O(n)

            if (!existSubIndex)
            {
              var valor = new KeyValuePair<int, List<string>>(subIndex, new List<string> { value }); // O(n)
              chave.Value.AddOrdered(valor); // O(n)
              return false; // O(n)
            }
            else
            {
              chave.Value.First(s => s.Key == subIndex).Value.AddOrdered(value); // O(n²)
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
            chave.Value.First(s => s.Key == subIndex).Value.AddOrdered(value); // O(n²)
            return true;
          }
          else
          {
            var valor = new KeyValuePair<int, List<string>>(subIndex, new List<string> { value });
            chave.Value.AddOrdered(valor); // O(n²)
            return true;
          }
        }
      }
      else
      {
        var valor = new KeyValuePair<int, List<string>>(subIndex, new List<string> { value });
        collection.Key = key;
        collection.Value.AddOrdered(valor); // O(n²)
        _collectionHoplon.AddOrdered(collection); // O(n²)

        return true;
      }
      return false;
    }
    // O(6n² + 11n) = O(n²).

    public IList<string> Get(string key, int start, int end)
    {
      IEnumerable<string> retorno;
      var chave = _collectionHoplon.Where(s => s.Key == key); // O(n)

      retorno = chave.SelectMany(s => s.Value.SelectMany(x => x.Value).AsEnumerable()).AsEnumerable(); // O(n)
      int qtdElementos = retorno.Count(); // O(n)

      if (end > 0)
      {
        return retorno.Skip(start.GetValueValid()).Take(end).ToList(); // O(n)
      }
      else if (end == 0)
      {
        return retorno.Skip(start.GetValueValid()).ToList(); // O(n)
      }
      else if (end < 0)
      {
        if (end == -1)
          return retorno.Skip(start.GetValueValid()).Take(qtdElementos).ToList(); // ToList O(n)
        if (end < -1) // O(n)
        {
          end *= (-1);
          end = (end - 1) > qtdElementos ? qtdElementos : (qtdElementos - end) + 1; // O(n)
          return retorno.Skip(start.GetValueValid()).Take(end).ToList(); // O(1) + O(1) + O(n) = O(n)
        }
      }

      return retorno.Skip(start.GetValueValid()).Take(end).ToList(); // O(1) + O(1) + O(n)
    }
    // Resultado: O(10n)


    public long IndexOf(string key, string value)
    {
      List<string> retorno; // O(1)
      var chave = _collectionHoplon.Where(s => s.Key == key); // O(n)

      retorno = chave.SelectMany(s => s.Value.SelectMany(x => x.Value).AsEnumerable()).AsEnumerable().ToList(); // O(n) + O(n) + O(n) + O(n) = O(4n) :. O(n)
      return retorno.ToLower().IndexOf(value.ToLower()); // O(n²)
    }
    // Se fosse uma regra inserir só dados lower/upper case não precisaria desse toLower extendido... 
    // Está aí porque se pesquisar por caracter minusculo não encontra o maiusculo.
    // Estudar uma melhor forma pra resolver e fzer o algoritmo voltar a ser O(n)
    // O(n² + n) :. O(n²)

    public bool Remove(string key)
    {
      var chave = _collectionHoplon.FirstOrDefault(s => s.Key == key); // O(n)
      _collectionHoplon.Remove(chave); // O(n)

      return chave != null ? true : false;
    }
    // O(n)

    public bool RemoveValuesFromSubIndex(string key, int subIndex)
    {
      var chave = _collectionHoplon.FirstOrDefault(s => s.Key == key); // O(n)
      if (chave != null)
      {
        var sub = chave.Value.FirstOrDefault(s => s.Key == subIndex); // O(n)
        chave.Value.Remove(sub); //O(n)

        return sub.Value != null ? true : false; // O(n)
      }
      return false;
    }
    // O(4n) :. O(n)

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
