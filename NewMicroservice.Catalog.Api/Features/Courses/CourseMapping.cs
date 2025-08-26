using NewMicroservice.Catalog.Api.Features.Courses.Create;
using NewMicroservice.Catalog.Api.Features.Courses.Dto;
using NewMicroservice.Catalog.Api.Features.Courses.GetAll;
using NewMicroservice.Catalog.Api.Features.Courses.Update;

namespace NewMicroservice.Catalog.Api.Features.Courses
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>().ReverseMap();
            CreateMap<UpdateCourseCommand, Course>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

        }
    }
}
