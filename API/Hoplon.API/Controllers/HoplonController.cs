using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hoplon.Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoplon.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HoplonController : ControllerBase
  {
    private readonly IHoplonCollection _hoplonCollection;
    public HoplonController(IHoplonCollection hoplonCollection)
    {
      _hoplonCollection = hoplonCollection;
    }

    [HttpPost]
    [Route("Add")]
    public bool Add(string key, int subIndex, string value)
    {
      return _hoplonCollection.Add(key, subIndex, value);
    }

    [HttpPost]
    [Route("Get")]
    public IEnumerable<string> Get(string key, int start, int end)
    {
      return _hoplonCollection.Get(key, start, end);
    }

    [HttpPost]
    [Route("Remove")]
    public bool Remove(string key)
    {
      return _hoplonCollection.Remove(key);
    }

    [HttpPost]
    [Route("RemoveValuesFromSubIndex")]
    public bool RemoveValuesFromSubIndex(string key, int subIndex)
    {
      return _hoplonCollection.RemoveValuesFromSubIndex(key, subIndex);
    }

    [HttpPost]
    [Route("IndexOf")]
    public long IndexOf(string key, string value)
    {
      return _hoplonCollection.IndexOf(key, value);
    }
  }
}
