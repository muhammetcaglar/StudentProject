using AutoMapper;
using StudentAPI.DomainModels;
using System;
using DataModels = StudentAPI.DataModels;

namespace StudentAPI.Profiles.AfterMaps
{
    public class addStudentRequestAfterMap : IMappingAction<addStudentRequest, DataModels.Student>
    {
        public void Process(addStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new DataModels.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
