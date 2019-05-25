using Hoplon.Domain.Interface;
using Hoplon.Domain.Models;
using Hoplon.Domain.Utilitarios.ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

      //var x = hs.Take(0); //end - pega da posicao 0 até a posicao setada.
      //var y = hs.Skip(3); //start - pula essas posicoes setadas e pega a partir delas
      var j = hs.Skip(0).Take(5);


      //Console.WriteLine("Lista Inteira");
      //foreach (var h in hs)
      //  Console.WriteLine("valor {0}", h);

      //Console.WriteLine("\nTake");
      //foreach (var xx in x)
      //  Console.WriteLine("valor {0}", xx);

      //Console.WriteLine("\nSkip");
      //foreach (var yy in y)
      //  Console.WriteLine("valor {0}", yy);

      Console.WriteLine("\nJuntos");
      foreach (var jj in j)
        Console.WriteLine("valor {0}", jj);
      Console.ReadKey();
    }
  }
}
