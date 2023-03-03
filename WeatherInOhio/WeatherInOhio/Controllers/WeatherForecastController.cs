using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Xml.Linq;

namespace WeatherInOhio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Acid Rain", "Cownado", "Earthquake", "Doom", "Men", "Nuke", "Oil", "Surreal entites", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }
        [HttpGet("GetEvery/{sort}")]
        public IActionResult GetAll(int? sortStragedy)
        {
            switch (sortStragedy)
            {
                case null:
                    return Ok(Summaries);
                case 1:
                    return Ok(Summaries.OrderBy(x => x).ToList());
                case -1:
                    return Ok(Summaries.OrderByDescending(x => x).ToList());
                default:
                    return BadRequest("Неверный индекс");
            }
        }
        [HttpPost]
        public IActionResult Add(string name)
        {

            Summaries.Add(name);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update(int index,string name)
        {
            if(index <0 || index>=Summaries.Count)
            {
                return BadRequest("Неверный индекс");
            }
            Summaries[index] = name;
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int ind)
        {
            if (ind < 0 || ind >= Summaries.Count)
            {
                return BadRequest("Нельзя удалить несуществующее");
            }
            Summaries.RemoveAt(ind);
            return Ok();
        }
        [HttpGet("GetIndex/{index}")]
        public IActionResult Get(int index)
        {
            string g = "";
            for (int i = 0; i < Summaries.Count; i++)
            {
                if (i == index)
                {
                    g = Summaries[i];
                }
            }
            return Ok(g);
        }
        [HttpGet("GetName/{findbyname}")]
        public IActionResult GetByName(string findbyname)
        {
            int y = 0;
            foreach (string g in Summaries)
            {
                if (g == findbyname)
                {
                    y++;
                }
            }
            return Ok(y);

        }
    }
}