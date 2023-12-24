using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DomainModels;
using StudentAPI.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentrepository;
        private readonly IImageRepository IImageRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository studentrepositry, IMapper mapper, IImageRepository IImageRepository)
        {
            this.studentrepository = studentrepositry;
            this.mapper = mapper;
            this.IImageRepository = IImageRepository;

        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            //With auto mapper
            var students = await studentrepository.GetStudentsAsync();

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

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            //With auto mapper
            var student = await studentrepository.GetStudentAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {


            if (await studentrepository.Exists(studentId))
            {
                var updatedStudent = await studentrepository.UpdateStudent(studentId, mapper.Map<DataModels.Student>(request));
                if (updatedStudent != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
            return NotFound();

            //var student = await studentrepositry.GetStudentAsync(studentId);

            //if (student == null)
            //{
            //    return NotFound();
            //}

            //return Ok(mapper.Map<Student>(student));
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {


            if (await studentrepository.Exists(studentId))
            {
                var student = await studentrepository.DeleteStudent(studentId);
                if (student != null)
                {
                    return Ok(mapper.Map<Student>(student));
                }
            }
            return NotFound();


        }

        [HttpPost]
        [Route("[controller]/add")]
        public async Task<IActionResult> addStudentAsync([FromBody] addStudentRequest request)
        {

            var student = await studentrepository.addStudent(mapper.Map<DataModels.Student>(request));


            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id }, mapper.Map<Student>(student));
        }

        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            var validExtensions = new List<string>
            {
               ".jpeg",
               ".png",
               ".gif",
               ".jpg"
            };

            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                {
                    if (await studentrepository.Exists(studentId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                        var fileImagePath = await IImageRepository.Upload(profileImage, fileName);

                        if (await studentrepository.UpdateProfileImage(studentId, fileImagePath))
                        {
                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                    }
                }

                return BadRequest("This is not a valid Image format");
            }

            return NotFound();
        }
    }
}

