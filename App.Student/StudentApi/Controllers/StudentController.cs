using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Application.Interfaces;
using StudentApi.Models.DTOs;
using StudentApi.Models.ViewModel;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<StudentViewModel>>> GetStudents()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> GetStudent(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpPost]
        public async Task<ActionResult> AddStudent([FromBody] StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new { message = "One or more validation errors occurred.", errors });
            }

            try
            {
                await _studentService.AddAsync(studentDto);
                return CreatedAtAction(nameof(GetStudent), new { id = studentDto.Id }, studentDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
        {
            if (id != studentDto.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the request body.");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return BadRequest(new { message = "Validation failed.", errors });
            }

            try
            {
                await _studentService.UpdateAsync(studentDto);
                return Ok(studentDto); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred.", error = ex.Message });
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
