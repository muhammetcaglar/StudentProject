using Microsoft.AspNetCore.Mvc;
using StudentAPI.Repositories;
using System;

namespace StudentAPI.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentrepositry;


        public StudentsController(IStudentRepository studentrepositry)
        {
                this.studentrepositry = studentrepositry;
        }

        [HttpGet]
        [Route("[controller]")]
        public IActionResult GetAllStudents()
        {
            return Ok(studentrepositry.GetStudents());
        }

 
    }
}
