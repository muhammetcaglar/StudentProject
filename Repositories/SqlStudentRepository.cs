using Microsoft.EntityFrameworkCore;
using StudentAPI.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }

       

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();       
        }

        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(p => p.Id == studentId);

        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
         return await   context.Student.AnyAsync(p=> p.Id == studentId);
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student student)
        {
            var existingStuddent = await GetStudentAsync(studentId);
            if (existingStuddent != null)
            {
                existingStuddent.FirstName= student.FirstName;
                existingStuddent.LastName= student.LastName;
                existingStuddent.PhoneNumber= student.PhoneNumber;
                existingStuddent.DateOfBirth= student.DateOfBirth; 
                existingStuddent.GenderId= student.GenderId;
                existingStuddent.Email= student.Email;
                existingStuddent.Address.PhysicalAddress= student.Address.PhysicalAddress;
                existingStuddent.Address.PostalAddress= student.Address.PostalAddress;
                await context.SaveChangesAsync();
                return existingStuddent;
            }
            return null;
        }
    }
}
