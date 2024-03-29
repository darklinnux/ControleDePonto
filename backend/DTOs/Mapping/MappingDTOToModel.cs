using AutoMapper;
using backend.Domain.Entities;
namespace backend.DTOs.Mapping
{
    public class MappingDTOToModel : AutoMapper.Profile
    {
        public MappingDTOToModel()
        {
            CreateMap<Title, TitleDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTOCreate>().ReverseMap();
            CreateMap<EmployeeMarking, EmployeMarkingDTO>().ReverseMap();
            CreateMap<EmployeeMarking, EmployeeMarkingDTOReport>();

        }
    }
}
