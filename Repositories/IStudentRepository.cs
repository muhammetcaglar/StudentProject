using StudentAPI.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAPI.Repositories
{
    public interface IStudentRepository
    {
        
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(Guid studentId);
        Task<List<Gender>> GetGendersAsync();

        Task<bool> Exists(Guid studentId);
        Task<Student> UpdateStudent(Guid studentId, Student student);

        Task<Student> addStudent(Student student);


        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);
        Task<Student> DeleteStudent(Guid studentId);
    }
}
