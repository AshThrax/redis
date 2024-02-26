using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisDemo.Interface;
using RedisDemo.models;

namespace RedisDemo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RedisValuesController : ControllerBase
    {
        private readonly IDistributedCacheService Service;
        
        public RedisValuesController(IDistributedCacheService Service)
        {
            this.Service = Service;
        }

        [HttpGet("{recordId}")]
        public async Task<ActionResult<Todoitems[]>> GetItems(string recordId) 
        {
            var valuesRedis = await Service.GetRecordAsync<Todoitems[]>(recordId);
            return valuesRedis;
        }
        [HttpPost ]
        public async Task<ActionResult> SetItem(string recordId,[FromBody]Todoitems[] todoitems)
        {
          await Service.SetRecordAsync<Todoitems[]>(recordId, todoitems);
            return Ok();
        }
    }
}
