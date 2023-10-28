using StudentAPI.DataModels;
using System.Collections.Generic;

namespace StudentAPI.Repositories
{
    public interface IStudentRepository
    {
        
        List<Student> GetStudents();

    }
}
