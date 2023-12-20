using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DataModels;
using StudentAPI.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{ 
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        public GendersController(IStudentRepository studentRepository, IMapper mapper) {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]/")]
        public async Task<IActionResult> GetAllGendersAsync()
        {
            //With auto mapper
            var genderlist = await studentRepository.GetGendersAsync();
            
            if (genderlist == null || !genderlist.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<Gender>>(genderlist));
        }

        
        }
}
