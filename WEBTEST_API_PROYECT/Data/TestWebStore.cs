using WEBTEST_API_PROYECT.Models.Dto;

namespace WEBTEST_API_PROYECT.Data
{
    public static class TestWebStore
    {
        public static List<TestWebDto> testWebList = new List<TestWebDto>
        {
                new TestWebDto { Id = 1, Name = "Vista a la Pisicina"},
                new TestWebDto { Id = 2, Name = "Vista a la Playa"}
        };
    }
}   
