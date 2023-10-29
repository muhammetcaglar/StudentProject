using AutoMapper;
using StudentAPI.DataModels;
using DomainModels= StudentAPI.DomainModels;

namespace StudentAPI.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Student, DomainModels.Student>().ReverseMap();
            CreateMap<Address, DomainModels.Address>().ReverseMap();
            CreateMap<Gender, DomainModels.Gender>().ReverseMap();
        }
    }
}
