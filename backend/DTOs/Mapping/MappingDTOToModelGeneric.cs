using AutoMapper;

namespace backend.DTOs.Mapping
{
    public class MappingDTOToModelGeneric<E,O> : Profile
    {
        public MappingDTOToModelGeneric()
        {
            CreateMap<E, O>().ReverseMap();
        }
    }
}
