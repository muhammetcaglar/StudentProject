using FluentValidation;
using StudentAPI.DomainModels;
using StudentAPI.Repositories;
using System.Linq;

namespace StudentAPI.Validators
{
    public class AddStudentRequestValidator : AbstractValidator<addStudentRequest>
    {
        public AddStudentRequestValidator(IStudentRepository studentRepository) { 
        RuleFor(p=> p.FirstName).NotEmpty().WithMessage("İsim boş geçilemez!");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Soyisim boş geçilemez!");
            RuleFor(p => p.DateOfBirth).NotEmpty().WithMessage("Lütfen doğum tarihi giriniz!");
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("Email adresi boş yada geçersiz formatta olamaz!");
            RuleFor(p => p.PhoneNumber).GreaterThan(99999).LessThan(10000000000).WithMessage("Lütfen geçerli bir telefon numarası giriniz!");
            RuleFor(p => p.GenderId).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList()
                .FirstOrDefault(p=> p.Id==id);
                if(gender!=null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Lütfen cinsiyetinizi seçiniz");



            RuleFor(p => p.PostalAddress).NotEmpty().WithMessage("Posta adresiniz boş geçilemez!");
            RuleFor(p=>p.PhysicalAddress).NotEmpty().WithMessage("Fiziksel adresiniz boş geçilemez!");
        }

    }
}
