using AutoMapper;
using TSUApplicationApi.DTOs;
using TSUApplicationApi.Entities;

namespace TSUApplicationApi.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Semester, SemesterDto>();
            CreateMap<Subject, SubjectItemDto>();
            CreateMap<Subject, SubjectDetailDto>();
        }
    }
}
