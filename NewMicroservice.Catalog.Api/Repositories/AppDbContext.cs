using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using NewMicroservice.Catalog.Api.Features.Categories;
using NewMicroservice.Catalog.Api.Features.Courses;
using System.Reflection;

namespace NewMicroservice.Catalog.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }

        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
                .Options;

            return new AppDbContext(optionsBuilder);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());




        }
    }

}
