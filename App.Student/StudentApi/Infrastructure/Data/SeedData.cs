using Bogus;
using Microsoft.EntityFrameworkCore;
using StudentApi.Models.Entity;
using System;

namespace StudentApi.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StudentDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<StudentDbContext>>()))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                if (context.Students.Any())
                {
                    return;
                }
                // Bogus: Generate 20 fake students
                var faker = new Faker<Student>()
                    .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                    .RuleFor(s => s.LastName, f => f.Name.LastName())
                    .RuleFor(s => s.DateOfBirth, f => f.Date.Between(new DateTime(2005, 1, 1), new DateTime(2015, 12, 31)))
                    .RuleFor(s => s.Email, f => f.Internet.Email())
                    .RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber())
                    .RuleFor(s => s.Address, f => f.Address.FullAddress());

                var students = faker.Generate(20);
                await context.Students.AddRangeAsync(students);
                await context.SaveChangesAsync();
            }
        }
    }
}
