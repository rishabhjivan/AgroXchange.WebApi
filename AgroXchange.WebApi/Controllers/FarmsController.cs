using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AgroXchange.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FarmsController : ControllerBase
    {
        [HttpGet]
        public dynamic Get()
        {
            IList<Farm> farms = new List<Farm>();
            farms.Add(new Farm { Id = 1, Name = "Abc Farm", OwnerId = Guid.Parse("dc1b6700-1a73-42e7-bebd-4df1fb37e274") });
            farms.Add(new Farm { Id = 2, Name = "Xyz Farm" });
            return farms;
        }

        [HttpGet("{id}", Name = "Get")]
        public Farm Get(int id)
        {
            return new Farm { Id = 1, Name = "Abc Farm", OwnerId = Guid.Parse("dc1b6700-1a73-42e7-bebd-4df1fb37e274") };
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Farm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
