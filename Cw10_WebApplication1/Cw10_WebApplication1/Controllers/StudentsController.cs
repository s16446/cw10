using Cw10_WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private IConfiguration  Configuration { get; set; }

        private IDbService _dbService;

        public StudentsController(IDbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            Configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetStudents(){
            return Ok(_dbService.GetStudents().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            return Ok(_dbService.GetStudent(id));
        }

        [HttpGet("{id}/{semester}")] // student's ID for whom we want to get enrollments
        public IActionResult GetEnrollments(string id, int semester)
        {
            return Ok(_dbService.GetEnrollments(id, semester));
        }

        [HttpPost] // add
        public IActionResult CreateStudent(Student student)
        {
            _dbService.AddStudent(student);
            return Ok(student);
        }

        [HttpPut("{id}")] // update
        public IActionResult UpdateStudent(string id)
        {
            return Ok("Aktualizacja zakonczona: " + id);
        }

        [HttpDelete("{id}")] // delete
        public IActionResult DeleteStudent(string id)
        {
            Student student = _dbService.FindStudent(id);
            if (student != null) 
            {
                _dbService.DeleteStudent(student);
                return Ok("Usuwanie zakonczone" + id);
            }
        else
            return NotFound("Nie znaleziono studenta o indeksie: " + id);
        }

       

    }
}