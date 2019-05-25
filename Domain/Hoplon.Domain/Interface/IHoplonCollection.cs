using Hoplon.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoplon.Domain.Interface
{
  public interface IHoplonCollection
  {
    bool Add(string key, int subIndex, string value);
    IList<string> Get(string key, int start, int end);
    bool Remove(string key);
    bool RemoveValuesFromSubIndex(string key, int subIndex);
    long IndexOf(string key, string value);
    IList<CollectionHoplon> RetornoApoioTeste();
  }
}
