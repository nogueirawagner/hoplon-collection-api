using Hoplon.Domain.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hoplon.Tests
{
  [TestClass]
  public class HoplonTests
  {
    [TestMethod]
    [Description("Inserir elementos no mesmo subIndex sem verificar retorno")]
    public void TesteInserirElementosNoMesmoSubIndex()
    {
      var hc = new HoplonCollection();
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "pedro"));
      Assert.IsFalse(hc.Add("ano.nascimento", 1981, "pedro"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "joao"));

      var retornoTeste = hc.RetornoTeste();
      Assert.AreEqual(retornoTeste.Count, 1);
      Assert.AreEqual(retornoTeste.First().Key, "ano.nascimento");

      // Verificar quantidade de subIndex deve ser 2.
      Assert.AreEqual(retornoTeste.First().Value.Count, 2);

      var subIndex1980 = retornoTeste.First().Value.First(s => s.Key == 1980);
      Assert.AreEqual(subIndex1980.Value.Count, 1);
      Assert.AreEqual(subIndex1980.Value.First(), "joao");

      var subIndex1981 = retornoTeste.First().Value.First(s => s.Key == 1981);
      Assert.AreEqual(subIndex1981.Value.Count, 1);
      Assert.AreEqual(subIndex1981.Value.First(), "pedro");
    }

    [TestMethod]
    [Description("Inserir elementos no mesmo subIndex e subIndex diferente")]
    public void TesteInserirElementosNoMesmoEDiferenteSubIndex()
    {
      var hc = new HoplonCollection();
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "pedro"));
      Assert.IsFalse(hc.Add("ano.nascimento", 1981, "pedro"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "joao"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1982, "paulo"));

      var retorno = hc.RetornoTeste();
      Assert.AreEqual(retorno.Count, 1);

      var sub1980 = retorno.First().Value.First(s => s.Key == 1980);
      Assert.AreEqual(sub1980.Value.Count, 1);
      Assert.AreEqual(sub1980.Value.First(), "joao");

      var sub1981 = retorno.First().Value.First(s => s.Key == 1981);
      Assert.AreEqual(sub1981.Value.Count, 1);
      Assert.AreEqual(sub1981.Value.First(), "pedro");

      var sub1982 = retorno.First().Value.First(s => s.Key == 1982);
      Assert.AreEqual(sub1982.Value.Count, 1);
      Assert.AreEqual(sub1982.Value.First(), "paulo");
    }

    [TestMethod]
    [Description("Inserir elementos repetidos no mesmo subIndex")]
    public void TesteInserirElementosSubIndex()
    {
      var hc = new HoplonCollection();
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "pedro"));
      Assert.IsFalse(hc.Add("ano.nascimento", 1980, "pedro"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "joao"));

      var retorno = hc.RetornoTeste();
      Assert.AreEqual(retorno.First().Key, "ano.nascimento");

      var subIndx = retorno.First().Value.First();
      Assert.IsTrue(subIndx.Value.Any(s => s == "pedro"));
      Assert.IsTrue(subIndx.Value.Any(s => s == "joao"));
    }

    [TestMethod]
    [Description(@"Inserir elementos iguais em mais de um subIndex de mesma chave, 
                  somente no subIndex 2020 que deverá conter os tres resultados")]
    public void TesteInserirElementosMaisSubIndex()
    {
      var hc = new HoplonCollection();
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "pedro"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "maria"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "joao"));

      Assert.IsFalse(hc.Add("ano.nascimento", 2000, "pedro"));
      Assert.IsFalse(hc.Add("ano.nascimento", 2000, "maria"));
      Assert.IsFalse(hc.Add("ano.nascimento", 2000, "joao"));

      Assert.IsFalse(hc.Add("ano.nascimento", 2015, "pedro"));
      Assert.IsFalse(hc.Add("ano.nascimento", 2015, "maria"));
      Assert.IsFalse(hc.Add("ano.nascimento", 2015, "joao"));

      Assert.IsFalse(hc.Add("ano.nascimento", 2020, "pedro"));
      Assert.IsFalse(hc.Add("ano.nascimento", 2020, "maria"));
      Assert.IsFalse(hc.Add("ano.nascimento", 2020, "joao"));

      var retorno = hc.RetornoTeste();
      Assert.AreEqual(retorno.First().Key, "ano.nascimento");
      Assert.AreEqual(retorno.First().Value.Count, 4);

      //subIndx 1980
      var si1980 = retorno.First().Value.First(s => s.Key == 1980);
      Assert.AreEqual(si1980.Value.Count, 0);

      //subIndx 2000
      var si2000 = retorno.First().Value.First(s => s.Key == 2000);
      Assert.AreEqual(si2000.Value.Count, 0);

      //subIndx 2015
      var si2015 = retorno.First().Value.First(s => s.Key == 2015);
      Assert.AreEqual(si2015.Value.Count, 0);

      //subIndx 2020
      var si2020 = retorno.First().Value.First(s => s.Key == 2020);
      Assert.AreEqual(si2020.Value.Count, 3);
      Assert.IsTrue(si2020.Value.Any(s => s == "pedro"));
      Assert.IsTrue(si2020.Value.Any(s => s == "joao"));
      Assert.IsTrue(si2020.Value.Any(s => s == "maria"));
    }

    [TestMethod]
    [Description(@"Inserir elementos diferentes em mais de um subIndex de mesma chave, 
                  deverá conter resultados em todos os subIndex")]
    public void TesteInserirElementosDiferentesMaisSubIndex()
    {
      var hc = new HoplonCollection();
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "pedro"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "maria"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "joao"));

      Assert.IsTrue(hc.Add("ano.nascimento", 2000, "wagner"));
      Assert.IsTrue(hc.Add("ano.nascimento", 2000, "eros"));
      Assert.IsTrue(hc.Add("ano.nascimento", 2000, "arthur"));

      Assert.IsTrue(hc.Add("ano.nascimento", 2015, "layane"));
      Assert.IsTrue(hc.Add("ano.nascimento", 2015, "lidia"));
      Assert.IsTrue(hc.Add("ano.nascimento", 2015, "pai"));

      Assert.IsTrue(hc.Add("ano.nascimento", 2020, "cachorro"));
      Assert.IsTrue(hc.Add("ano.nascimento", 2020, "ford"));
      Assert.IsTrue(hc.Add("ano.nascimento", 2020, "focus"));

      var retorno = hc.RetornoTeste();
      Assert.AreEqual(retorno.First().Key, "ano.nascimento");
      Assert.AreEqual(retorno.First().Value.Count, 4);

      //subIndx 1980
      var si1980 = retorno.First().Value.First(s => s.Key == 1980);
      Assert.AreEqual(si1980.Value.Count, 3);
      Assert.IsTrue(si1980.Value.Any(s => s == "pedro"));
      Assert.IsTrue(si1980.Value.Any(s => s == "joao"));
      Assert.IsTrue(si1980.Value.Any(s => s == "maria"));

      //subIndx 2000
      var si2000 = retorno.First().Value.First(s => s.Key == 2000);
      Assert.AreEqual(si2000.Value.Count, 3);
      Assert.IsTrue(si2000.Value.Any(s => s == "wagner"));
      Assert.IsTrue(si2000.Value.Any(s => s == "eros"));
      Assert.IsTrue(si2000.Value.Any(s => s == "arthur"));

      //subIndx 2015
      var si2015 = retorno.First().Value.First(s => s.Key == 2015);
      Assert.AreEqual(si2015.Value.Count, 3);
      Assert.IsTrue(si2015.Value.Any(s => s == "layane"));
      Assert.IsTrue(si2015.Value.Any(s => s == "lidia"));
      Assert.IsTrue(si2015.Value.Any(s => s == "pai"));

      //subIndx 2020
      var si2020 = retorno.First().Value.First(s => s.Key == 2020);
      Assert.AreEqual(si2020.Value.Count, 3);
      Assert.IsTrue(si2020.Value.Any(s => s == "cachorro"));
      Assert.IsTrue(si2020.Value.Any(s => s == "ford"));
      Assert.IsTrue(si2020.Value.Any(s => s == "focus"));
    }

    [TestMethod]
    [Description("Inserir elementos em chaves diferentes com subIndex iguais")]
    public void TesteInserirElementosEmChavesDiferentesComSubIndexIguais()
    {
      var hc = new HoplonCollection();
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "pedro"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "maria"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "joao"));

      Assert.IsTrue(hc.Add("cidades", 1980, "pedro"));
      Assert.IsTrue(hc.Add("cidades", 1980, "maria"));
      Assert.IsTrue(hc.Add("cidades", 1980, "joao"));

      var retorno = hc.RetornoTeste();

      // Qtd de chaves
      Assert.AreEqual(retorno.Count, 2);

      // Chave ano.nasc
      var anoNasc = retorno.First(s => s.Key == "ano.nascimento");
      Assert.AreEqual(anoNasc.Value.Count, 1); // Apenas um subIndice 
      Assert.IsTrue(anoNasc.Value.Any(s => s.Key == 1980));

      // subIndices de anoNasc
      var siAnoNasc = anoNasc.Value.First().Value;
      Assert.IsTrue(siAnoNasc.Any(s => s == "pedro"));
      Assert.IsTrue(siAnoNasc.Any(s => s == "maria"));
      Assert.IsTrue(siAnoNasc.Any(s => s == "joao"));

      // Chave cidades
      var cidades = retorno.First(s => s.Key == "cidades");
      Assert.AreEqual(cidades.Value.Count, 1); // Apenas um subIndice 
      Assert.IsTrue(cidades.Value.Any(s => s.Key == 1980));

      // subIndices de cidades
      var siCidades = cidades.Value.First().Value;
      Assert.IsTrue(siCidades.Any(s => s == "pedro"));
      Assert.IsTrue(siCidades.Any(s => s == "maria"));
      Assert.IsTrue(siCidades.Any(s => s == "joao"));
    }

    [TestMethod]
    [Description("Inserir elementos em chaves diferentes com subIndex diferentes")]
    public void TesteInserirElementosEmChavesDiferentesComSubIndexDiferentes()
    {
      var hc = new HoplonCollection();
      Assert.IsTrue(hc.Add("ano.nascimento", 1980, "pedro"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1981, "maria"));
      Assert.IsTrue(hc.Add("ano.nascimento", 1982, "joao"));

      Assert.IsTrue(hc.Add("cidades", 2000, "anapolis"));
      Assert.IsTrue(hc.Add("cidades", 2001, "goiania"));
      Assert.IsTrue(hc.Add("cidades", 2002, "florianopolis"));

      Assert.IsTrue(hc.Add("carros", 8000, "focus"));
      Assert.IsTrue(hc.Add("carros", 8001, "mustang"));
      Assert.IsTrue(hc.Add("carros", 8002, "fusion"));

      Assert.IsTrue(hc.Add("jogos", 5000, "basquete"));
      Assert.IsTrue(hc.Add("jogos", 5001, "futebol"));
      Assert.IsTrue(hc.Add("jogos", 5002, "voley"));


      var retorno = hc.RetornoTeste();

      // Qtd de chaves
      Assert.AreEqual(retorno.Count, 4);

      // Chave ano.nasc
      var anoNasc = retorno.First(s => s.Key == "ano.nascimento");
      Assert.AreEqual(anoNasc.Value.Count, 3);
      Assert.IsTrue(anoNasc.Value.Any(s => s.Key == 1980));
      Assert.IsTrue(anoNasc.Value.Any(s => s.Key == 1981));
      Assert.IsTrue(anoNasc.Value.Any(s => s.Key == 1982));

      // nenhum diferente destes 3.
      Assert.AreEqual(anoNasc.Value.Count(s => s.Key != 1980 && s.Key != 1981 && s.Key != 1982), 0);

      // verifica o valor de cada subIndice.
      Assert.AreEqual(anoNasc.Value.First(s => s.Key == 1980).Value.First(), "pedro");
      Assert.AreEqual(anoNasc.Value.First(s => s.Key == 1981).Value.First(), "maria");
      Assert.AreEqual(anoNasc.Value.First(s => s.Key == 1982).Value.First(), "joao");

      // verifica que cada subIndice tem apenas um valor
      Assert.AreEqual(anoNasc.Value.First(s => s.Key == 1980).Value.Count, 1);
      Assert.AreEqual(anoNasc.Value.First(s => s.Key == 1981).Value.Count, 1);
      Assert.AreEqual(anoNasc.Value.First(s => s.Key == 1982).Value.Count, 1);

      // Chave cidades
      var cidades = retorno.First(s => s.Key == "cidades");
      Assert.AreEqual(cidades.Value.Count, 3); // Apenas um subIndice 
      Assert.IsTrue(cidades.Value.Any(s => s.Key == 2000));
      Assert.IsTrue(cidades.Value.Any(s => s.Key == 2001));
      Assert.IsTrue(cidades.Value.Any(s => s.Key == 2002));

      // nenhum diferente destes 3.
      Assert.AreEqual(cidades.Value.Count(s => s.Key != 2000 && s.Key != 2001 && s.Key != 2002), 0);

      // verifica o valor de cada subIndice.
      Assert.AreEqual(cidades.Value.First(s => s.Key == 2000).Value.First(), "anapolis");
      Assert.AreEqual(cidades.Value.First(s => s.Key == 2001).Value.First(), "goiania");
      Assert.AreEqual(cidades.Value.First(s => s.Key == 2002).Value.First(), "florianopolis");

      // verifica que cada subIndice tem apenas um valor
      Assert.AreEqual(cidades.Value.First(s => s.Key == 2000).Value.Count, 1);
      Assert.AreEqual(cidades.Value.First(s => s.Key == 2001).Value.Count, 1);
      Assert.AreEqual(cidades.Value.First(s => s.Key == 2002).Value.Count, 1);

      // Chave carros
      var carros = retorno.First(s => s.Key == "carros");
      Assert.AreEqual(carros.Value.Count, 3); // Apenas um subIndice 
      Assert.IsTrue(carros.Value.Any(s => s.Key == 8000));
      Assert.IsTrue(carros.Value.Any(s => s.Key == 8001));
      Assert.IsTrue(carros.Value.Any(s => s.Key == 8002));

      // nenhum diferente destes 3.
      Assert.AreEqual(carros.Value.Count(s => s.Key != 8000 && s.Key != 8001 && s.Key != 8002), 0);

      // verifica o valor de cada subIndice.
      Assert.AreEqual(carros.Value.First(s => s.Key == 8000).Value.First(), "focus");
      Assert.AreEqual(carros.Value.First(s => s.Key == 8001).Value.First(), "mustang");
      Assert.AreEqual(carros.Value.First(s => s.Key == 8002).Value.First(), "fusion");

      // verifica que cada subIndice tem apenas um valor
      Assert.AreEqual(carros.Value.First(s => s.Key == 8000).Value.Count, 1);
      Assert.AreEqual(carros.Value.First(s => s.Key == 8001).Value.Count, 1);
      Assert.AreEqual(carros.Value.First(s => s.Key == 8002).Value.Count, 1);

      // Chave jogos
      var jogos = retorno.First(s => s.Key == "jogos");
      Assert.AreEqual(jogos.Value.Count, 3); // Apenas um subIndice 
      Assert.IsTrue(jogos.Value.Any(s => s.Key == 5000));
      Assert.IsTrue(jogos.Value.Any(s => s.Key == 5001));
      Assert.IsTrue(jogos.Value.Any(s => s.Key == 5002));

      // nenhum diferente destes 3.
      Assert.AreEqual(jogos.Value.Count(s => s.Key != 5000 && s.Key != 5001 && s.Key != 5002), 0);

      // verifica o valor de cada subIndice.
      Assert.AreEqual(jogos.Value.First(s => s.Key == 5000).Value.First(), "basquete");
      Assert.AreEqual(jogos.Value.First(s => s.Key == 5001).Value.First(), "futebol");
      Assert.AreEqual(jogos.Value.First(s => s.Key == 5002).Value.First(), "voley");

      // verifica que cada subIndice tem apenas um valor
      Assert.AreEqual(jogos.Value.First(s => s.Key == 5000).Value.Count, 1);
      Assert.AreEqual(jogos.Value.First(s => s.Key == 5001).Value.Count, 1);
      Assert.AreEqual(jogos.Value.First(s => s.Key == 5002).Value.Count, 1);
    }

    [TestMethod]
    [Description("Testar ordenacao de valores de cada subindice")]
    public void TestarOrdenacaoDeValoresDeCadaSubIndice()
    {
      var hc = new HoplonCollection();
      Assert.IsTrue(hc.Add("nomes", 1980, "Carlos"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Willian"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Cristiano"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Cristiano Craujo"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Cristiano Braujo"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Cristiano Araujo"));

      Assert.IsTrue(hc.Add("nomes", 1981, "Mercia"));
      Assert.IsTrue(hc.Add("nomes", 1981, "Maria Alves"));
      Assert.IsTrue(hc.Add("nomes", 1981, "Maria Borboleta"));
      Assert.IsTrue(hc.Add("nomes", 1981, "Maria Coró"));
      Assert.IsTrue(hc.Add("nomes", 1981, "Maria Carla"));

      Assert.IsTrue(hc.Add("nomes", 1982, "Ana Julia"));
      Assert.IsTrue(hc.Add("nomes", 1982, "Wagner"));
      Assert.IsTrue(hc.Add("nomes", 1982, "Roberto"));
      Assert.IsTrue(hc.Add("nomes", 1982, "Bruno"));
      Assert.IsTrue(hc.Add("nomes", 1982, "Paulo"));

      var retorno = hc.RetornoTeste();

      string[] compare1980 = { "Carlos", "Cristiano", "Cristiano Araujo", "Cristiano Braujo", "Cristiano Craujo", "Willian" };
      string[] compare1981 = { "Maria Alves", "Maria Borboleta", "Maria Carla", "Maria Coró", "Mercia" };
      string[] compare1982 = { "Ana Julia", "Bruno", "Paulo", "Roberto", "Wagner" };

      // Qtd de chaves
      Assert.AreEqual(retorno.Count, 1);

      // subIndice 1980
      var si1980 = retorno.First().Value.First(s => s.Key == 1980);
      var tam = si1980.Value.Count();
      for (int i = 0; i < tam; i++)
        Assert.AreEqual(si1980.Value[i], compare1980[i]);

      // subIndice 1981
      var si1981 = retorno.First().Value.First(s => s.Key == 1981);
      tam = si1981.Value.Count();
      for (int i = 0; i < tam; i++)
        Assert.AreEqual(si1981.Value[i], compare1981[i]);

      // subIndice 1982
      var si1982 = retorno.First().Value.First(s => s.Key == 1982);
      tam = si1982.Value.Count();
      for (int i = 0; i < tam; i++)
        Assert.AreEqual(si1982.Value[i], compare1982[i]);
    }

    [TestMethod]
    [Description("Testar ordenacao de valores de cada subIndice")]
    public void TestarOrdenacaoDeSubIndice()
    {
      var hc = new HoplonCollection();
      Assert.IsTrue(hc.Add("nomes", 2001, "Gabriel"));
      Assert.IsTrue(hc.Add("nomes", 2001, "Julia"));
      Assert.IsTrue(hc.Add("nomes", 2001, "Estelio"));
      Assert.IsTrue(hc.Add("nomes", 2001, "Marcos"));
      Assert.IsTrue(hc.Add("nomes", 2001, "Layane"));

      Assert.IsTrue(hc.Add("nomes", 1982, "Ana Julia"));
      Assert.IsTrue(hc.Add("nomes", 1982, "Wagner"));
      Assert.IsTrue(hc.Add("nomes", 1982, "Roberto"));
      Assert.IsTrue(hc.Add("nomes", 1982, "Bruno"));
      Assert.IsTrue(hc.Add("nomes", 1982, "Paulo"));

      Assert.IsTrue(hc.Add("nomes", 1980, "Carlos"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Willian"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Cristiano"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Cristiano Craujo"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Cristiano Braujo"));
      Assert.IsTrue(hc.Add("nomes", 1980, "Cristiano Araujo"));

      Assert.IsTrue(hc.Add("nomes", 1981, "Mercia"));
      Assert.IsTrue(hc.Add("nomes", 1981, "Maria Alves"));
      Assert.IsTrue(hc.Add("nomes", 1981, "Maria Borboleta"));
      Assert.IsTrue(hc.Add("nomes", 1981, "Maria Coró"));
      Assert.IsTrue(hc.Add("nomes", 1981, "Maria Carla"));

     




      var retorno = hc.RetornoTeste();

      string[] compare1980 = { "Carlos", "Cristiano", "Cristiano Araujo", "Cristiano Braujo", "Cristiano Craujo", "Willian" };
      string[] compare1981 = { "Maria Alves", "Maria Borboleta", "Maria Carla", "Maria Coró", "Mercia" };
      string[] compare1982 = { "Ana Julia", "Bruno", "Paulo", "Roberto", "Wagner" };

      // Qtd de chaves
      Assert.AreEqual(retorno.Count, 1);

      // subIndice 1980
      var si1980 = retorno.First().Value.First(s => s.Key == 1980);
      var tam = si1980.Value.Count();
      for (int i = 0; i < tam; i++)
        Assert.AreEqual(si1980.Value[i], compare1980[i]);

      // subIndice 1981
      var si1981 = retorno.First().Value.First(s => s.Key == 1981);
      tam = si1981.Value.Count();
      for (int i = 0; i < tam; i++)
        Assert.AreEqual(si1981.Value[i], compare1981[i]);

      // subIndice 1982
      var si1982 = retorno.First().Value.First(s => s.Key == 1982);
      tam = si1982.Value.Count();
      for (int i = 0; i < tam; i++)
        Assert.AreEqual(si1982.Value[i], compare1982[i]);
    }
  }
}
