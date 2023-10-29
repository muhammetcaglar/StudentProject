using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DomainModels;
using StudentAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentrepositry;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository studentrepositry, IMapper mapper)
        {
                this.studentrepositry = studentrepositry;
                this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            //With auto mapper
          var students = await studentrepositry.GetStudentsAsync();

          return Ok(mapper.Map<List<Student>>(students));


            //ManuelMapper Yaptığım işlem

            //var domainModelStudents = new List<Student>();
            //foreach (var student in students)
            //{
            //    domainModelStudents.Add(new Student()
            //    {
            //        Id=student.Id,
            //        FirstName=student.FirstName,
            //        LastName=student.LastName,
            //        DateOfBirth=student.DateOfBirth,
            //        Email=student.Email,
            //        PhoneNumber =student.PhoneNumber,
            //        ProfileImageUrl=student.ProfileImageUrl,
            //        GenderId=student.GenderId,
            //        Address=new Address()
            //        {
            //            Id=student.Address.Id,
            //            PhysicalAddress=student.Address.PhysicalAddress,
            //            PostalAddress=student.Address.PostalAddress,
            //        },
            //        Gender =  new Gender()
            //        {
            //            Id = student.Gender.Id,
            //            Description=student.Gender.Description,

            //        }
            //    });
            //}
            //return Ok(domainModelStudents);
        }

 
    }
}
