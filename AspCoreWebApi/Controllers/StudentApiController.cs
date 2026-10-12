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

        public StudentApiController(MyDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudentlist()
        {
            try
            {
                var data = await context.Students.ToListAsync();

                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

    //    [HttpPost]
    //    public async Task<ActionResult<List<Student>>> GetStudent(
    //[FromQuery] string? city)
    //    {
    //        var query = context.Students.AsQueryable();

    //        if (!string.IsNullOrEmpty(city))
    //        {
    //            query = query.Where(x => x.City == city);
    //        }

    //        var data = await query.ToListAsync();

    //        return Ok(data);
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
        #region DeleteMethod
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
        #endregion

        #region GetDataUsingPostMethod//FromBody and Query Param

        [HttpPost("{city}")]
        public async Task<IActionResult> GetStudentDetailsCityWise(string city)
        {
            var cityData = await context.Students
                .Where(s => s.City == city)
                .ToListAsync();

            if (cityData.Count == 0)
            {
                return NotFound("No students found in this city.");
            }

            return Ok(cityData);
        }
        //[HttpPost("Search")]
        //public IActionResult Search([FromBody] Student std) 
        //{
        //    string city = std.City;
        //    return Ok(city);
        //}
        [HttpPost("SearchByCity")]
        public async Task<IActionResult> SearchByCity([FromQuery]string city) 
        {
            var students = await context.Students.Where(s => s.City == city).ToListAsync();
            if (students == null) 
            {
                return NotFound("No student found in this city");
            }
            return Ok(students);
        }


        #endregion
    }
}
