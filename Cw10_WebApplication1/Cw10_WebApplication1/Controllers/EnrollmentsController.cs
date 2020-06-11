using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Requests;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
	[Route("api/enrollments")]
    [ApiController]
    
    public class EnrollmentsController : ControllerBase
    {
        private readonly IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service) 
        { 
            _service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var response = _service.EnrollStudent(request);
            switch (response.getStatus()) {
                case 200: return Ok(response.getMessage());//(response);
                case 201: return new CreatedAtRouteResult("api/enrollments", response);//(response);
                case 400: return BadRequest(response.getMessage());
                default: return BadRequest();
            }
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            var response = _service.PromoteStudents(request);
            switch (response.getStatus()) {
                case 201: return new CreatedAtRouteResult("api/enrollments/promotions", response);//(response);
                case 404: return NotFound(response.getMessage());
                case 400: return BadRequest(response.getMessage());
                default: return BadRequest("INNY ERROR");
            }

        }
    }


}