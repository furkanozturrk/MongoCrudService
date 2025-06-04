using AutoMapper;
using MongoCrudService.Dto;
using MongoCrudService.Model;

namespace MongoCrudService.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDetails, InsertRecordDto>().ReverseMap();
        }
    }
}
