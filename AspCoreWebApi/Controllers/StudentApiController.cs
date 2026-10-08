using AspCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly MyDbContext context;
        private readonly ILogger<StudentApiController> logger;

        public StudentApiController(MyDbContext context, ILogger<StudentApiController> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudentlist()
        {
            
                logger.LogInformation("Getting All Students");
                var data = await context.Students.ToListAsync();
                logger.LogInformation($"Students: {data}");
                return Ok(data);
            
            
        }

    //    [HttpPost("city")]
    //    public ActionResult GetStudentByCity(
    //[FromBody] string? city)

    //    {
    //        try
    //        {
    //            var query = context.Students.AsQueryable();

    //            if (!string.IsNullOrEmpty(city))
    //            {
    //                query = query.Where(x => x.City == city);
    //            }

    //            var data =  query.ToListAsync();

    //            return Ok(data);
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            else 
            {
                return student;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Createstudent(Student std)
        {
            await context.Students.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Updatestudent(int id, Student std)
        {
            if (id !=std.StudentId) 
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(std);

        }

        [HttpDelete]
        public async Task<ActionResult<Student>> Deletestudent(int id)
        {
            var std = await context.Students.FindAsync(id);
            if (std==null)
            {
                return NotFound();
            }
            context.Students.Remove(std);
            await context.SaveChangesAsync();
            return Ok();

        }



    }
}
