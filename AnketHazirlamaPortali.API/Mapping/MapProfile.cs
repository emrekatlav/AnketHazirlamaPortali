using AnketHazirlamaPortali.API.Dtos;
using AnketHazirlamaPortali.API.Models;
using AutoMapper;

namespace AnketHazirlamaPortali.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Survey, SurveyDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppRole, RoleDto>().ReverseMap();

        }
    }
}
