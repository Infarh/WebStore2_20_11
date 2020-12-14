using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")] //http://localhost:5001/api/values
    [Route("api/v1/values")] //http://localhost:5001/api/v1/values
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> __Values = Enumerable.Range(1, 10)
           .Select(i => $"Value {i}")
           .ToList();

        [HttpGet]
        public IEnumerable<string> Get() => __Values;

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) // http://localhost:5001/api/v1/values/5
        {
            if (id < 0)
                return BadRequest();

            if (id >= __Values.Count)
                return NotFound();

            return __Values[id];
        }

        [HttpPost]
        public void Post([FromBody] string value) => __Values.Add(value);

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();

            __Values[id] = value;

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();

            __Values.RemoveAt(id);

            return Ok();
        }
    }
}
