using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsAPIController : ControllerBase
    {
        public List<string> fruits = new List<string>()
        {
            "Apple","Banana","Kela","Orage","Grapes"
        };
        [HttpGet]
        public List<string> Getfruits()
        {
            return fruits;
        }

        [HttpGet("{id}")]
        public string GetfruitsByElements(int id) 
        {
            return fruits.ElementAt(id);

        }
    }
}
