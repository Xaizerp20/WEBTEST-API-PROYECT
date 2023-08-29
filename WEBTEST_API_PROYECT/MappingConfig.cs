using AutoMapper;
using WEBTEST_API_PROYECT.Models;
using WEBTEST_API_PROYECT.Models.Dto;

namespace WEBTEST_API_PROYECT
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<TestWeb, TestWebDto>();
            CreateMap<TestWebDto, TestWeb>();

            CreateMap<TestWeb, TestWebCreateDto>().ReverseMap();
            CreateMap<TestWeb, TestWebUpdateDto>().ReverseMap();

        }
    }
}
