using StudentAPI.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace StudentAPI.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }
        public List<Student> GetStudents()
        {
            return context.Student.ToList();           }
    }
}
