using MongoDB.Driver;
using NewMicroservice.Catalog.Api.Features.Categories;
using NewMicroservice.Catalog.Api.Features.Courses;

namespace NewMicroservice.Catalog.Api.Repositories
{
    public static class SeedData
    {
        public static async Task AddSeedDataExt(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
                if (!dbContext.Categories.Any())
                {
                    var categories = new List<Category>
                    {
                        new(){Id = NewId.NextSequentialGuid(), Name = "Development"},
                        new(){Id = NewId.NextSequentialGuid(), Name = "Business"},
                        new(){Id = NewId.NextSequentialGuid(), Name = "IT & Software"},
                        new(){Id = NewId.NextSequentialGuid(), Name = "Office Productivity"},
                        new(){Id = NewId.NextSequentialGuid(), Name = "Personal Development"},

                    };
                    await dbContext.Categories.AddRangeAsync(categories);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Courses.Any())
                {
                    var randomUserId = NewId.NextSequentialGuid();
                    var categories = await dbContext.Categories.FirstAsync();
                    var courses = new List<Course>
                    {
                        new()
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "Learn ASP.NET Core 7",
                            Description = "ASP.NET Core 7 is a cross-platform, high-performance framework for building modern, cloud-based, Internet-connected applications.",
                            Price = 49.99M,
                            Feature =  new Feature {Duration = 130,Rating = 3,EducatorFullName = "AET" },
                            ImageUrl = "https://aet.microservice.com/catalog/1.png",
                            CategoryId = categories.Id,
                            CreatedDate = DateTime.UtcNow,
                            UserId = randomUserId
                        },
                        new()
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "Mastering Entity Framework Core 7",
                            Description = "Entity Framework Core 7 is a lightweight, extensible, and cross-platform version of the popular Entity Framework data access technology.",
                            Price = 39.99M,
                            Feature =  new Feature {Duration = 120,Rating = 4,EducatorFullName = "AET" },
                            ImageUrl = "https://aet.microservice.com/catalog/2.png",
                            CategoryId = categories.Id,
                            CreatedDate = DateTime.UtcNow,
                            UserId = randomUserId
                        },
                        new()
                        {
                            Id = NewId.NextSequentialGuid(),
                            Name = "C# Programming for Beginners",
                            Description = "C# is a modern, object-oriented programming language developed by Microsoft that is widely used for building Windows applications, web applications, and games.",
                            Price = 29.99M,
                            Feature =  new Feature {Duration = 110,Rating =1,EducatorFullName = "AET" },
                            ImageUrl = "https://aet.microservice.com/catalog/3.png",
                            CategoryId = categories.Id,
                            CreatedDate = DateTime.UtcNow,
                            UserId = randomUserId
                        }
                    };
                    await dbContext.Courses.AddRangeAsync(courses);
                    await dbContext.SaveChangesAsync();

                }
            }
            ;


        }
    }
}
