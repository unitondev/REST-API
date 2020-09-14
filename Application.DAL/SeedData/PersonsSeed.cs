using System;
using Application.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DAL.SeedData
{
    public class PersonsSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Persons.Any())
                {
                    return;
                }

                context.Persons.AddRange(
                    new Person
                    {
                        Id = 1,
                        FirstName = "Igor", 
                        LastName = "Igor", 
                        Sex = "M",
                        PrivateInformation = "+912i30192i312"
                    },
                    new Person
                    {
                        Id = 2,
                        FirstName = "Maxim", 
                        LastName = "Maxim", 
                        Sex = "M",
                        PrivateInformation = "+912192i312"
                    },
                    new Person
                    {
                        Id = 3,
                        FirstName = "Anastasia", 
                        LastName = "Anastasia", 
                        Sex = "F",
                        PrivateInformation = "+12i30192i312"
                    },
                    new Person
                    {
                        Id = 4,
                        FirstName = "Viktoria", 
                        LastName = "Viktoria", 
                        Sex = "F",
                        PrivateInformation = "+1902341244"
                    },
                    new Person
                    {
                        Id = 5,
                        FirstName = "Polina", 
                        LastName = "Polina", 
                        Sex = "F",
                        PrivateInformation = "+0112"
                    });

                context.SaveChanges();
            }
        }
    }
}