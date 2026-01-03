using Microsoft.AspNetCore.Razor.TagHelpers;
using NewMicroservice.Web.Options;

namespace NewMicroservice.Web.TagHelpers
{
    public class CourseThumbnailPictureTagHelper(MicroserviceOption microserviceOption) : TagHelper
    {
        public string? Src { get; set; }


        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";

            var blankCourseThumbnailImagePath = "/images/blankimage.jpg";

            if (string.IsNullOrEmpty(Src))
            {
                output.Attributes.SetAttribute("src", blankCourseThumbnailImagePath);
            }
            else
            {
                var baseUrl = microserviceOption.FileMicroservice.BaseUrl.TrimEnd('/');
                var src = Src.TrimStart('/');
                var courseThumbnailImagePath = $"{baseUrl}/{src}";

                output.Attributes.SetAttribute("src", courseThumbnailImagePath);
            }

            return base.ProcessAsync(context, output);
        }
    }
}
