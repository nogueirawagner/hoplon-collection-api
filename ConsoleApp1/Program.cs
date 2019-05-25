using Hoplon.Domain.Interface;
using Hoplon.Domain.Models;
using Hoplon.Domain.Utilitarios.ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
  public class Program
  {

    static void Main(string[] args)
    {

      var hs = new List<string>();
      hs.AddOrdered("a");
      hs.AddOrdered("c");
      hs.AddOrdered("d");
      hs.AddOrdered("b");
      hs.AddOrdered("e");
      hs.AddOrdered("g");
      hs.AddOrdered("i");


      var teste = new Teste();
      teste.Adiciona("ano.nascimento", 1980, "wagner");
      teste.Adiciona("ano.nascimento", 1980, "maria");
      teste.Adiciona("ano.nascimento", 1980, "joao");
      teste.Adiciona("ano.nascimento", 1980, "pedro");

      teste.Adiciona("ano.nascimento", 2000, "pedro");
      teste.Adiciona("ano.nascimento", 2000, "pedro");
      teste.Adiciona("ano.nascimento", 2000, "pedro");


      Console.ReadKey();
    }
  }

  public class Colecao
  {
    public Colecao()
    {
      Valor = new List<KeyValuePair<int, HashSet<string>>>();
    }
    public string Chave { get; set; }

    public List<KeyValuePair<int, HashSet<string>>> Valor { get; set; }
  }

  public class Teste : IHoplonCollection
  {
    public List<Colecao> Colecoes = new List<Colecao>();
  
    public void Adiciona(string chave, int index, string valor)
    {
      var valores = new List<KeyValuePair<int, HashSet<string>>>
      {
        new KeyValuePair<int, HashSet<string>>(index, new HashSet<string> { valor })
      };

      var colecao = new Colecao
      {
        Chave = chave
      };
      colecao.Valor.AddRange(valores);

      Colecoes.Add(colecao);


    }

    public IList<string> Get(string key, int start, int end)
    {
      throw new NotImplementedException();
    }

    public bool Remove(string key)
    {
      throw new NotImplementedException();
    }

    public bool RemoveValuesFromSubIndex(string key, int subIndex)
    {
      throw new NotImplementedException();
    }

    public IList<CollectionHoplon> RetornoTeste()
    {
      throw new NotImplementedException();
    }

    bool IHoplonCollection.Add(string key, int subIndex, string value)
    {
      throw new NotImplementedException();
    }
  }

}
