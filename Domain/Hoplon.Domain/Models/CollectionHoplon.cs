using System;
using System.Collections.Generic;
using System.Text;

namespace Hoplon.Domain.Models
{
  public class CollectionHoplon
  {
    public CollectionHoplon()
    {
      Value = new List<KeyValuePair<int, List<string>>>();
    }

    public string Key { get; set; }
    public List<KeyValuePair<int, List<string>>> Value { get; set; }
  }
}
