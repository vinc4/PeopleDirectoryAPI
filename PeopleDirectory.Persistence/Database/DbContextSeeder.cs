using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PeopleDirectory.Application.Enums;
using PeopleDirectory.Intergration.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeopleDirectory.Persistence.Database
{
    public class DbContextSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly string[] _countries = { "USA", "Canada", "UK", "France", "Germany" };
        private readonly string[][] _cities;

        public DbContextSeeder(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _cities = new string[][]
             {
                new string[] { "New York", "Los Angeles", "Chicago" }, // USA
                new string[] { "Toronto", "Montreal", "Vancouver" }, // Canada
                new string[] { "London", "Manchester", "Birmingham" }, // UK
                new string[] { "Paris", "Lyon", "Marseille" }, // France
                new string[] { "Berlin", "Munich", "Hamburg" } // Germany
            };

        }


        public async Task SeedAsync()
        {
            // Check if the user already exists
            var existingUser = await _userManager.FindByEmailAsync("admin@gmail.com");
            if (existingUser == null)
            {
                // Create a defaultUser
                var defaultUser = new User
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Firstname = "firstname",
                    LastName = "lastname",
                    CreatedBy = "admin",
                    UpdatedBy = "admin", 
                };

                // Use UserManager to create the user
                var result = await _userManager.CreateAsync(defaultUser, "123456");


                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(defaultUser, Roles.Admin);

                    Console.WriteLine("Admin user created successfully.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Admin user already exists.");
            }
        }

        public async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Check if the "admin" role already exists
            var adminRoleExists = await roleManager.RoleExistsAsync(Roles.Admin);
            if (!adminRoleExists)
            {
                // Create the "admin" role
                var adminRole = new IdentityRole(Roles.Admin);
                await roleManager.CreateAsync(adminRole);

                Console.WriteLine("Admin role created successfully.");
            }
            else
            {
                Console.WriteLine("Admin role already exists.");
            }
        }

        public async Task SeedClientsAsync()
        {
            if (_context.Clients.Any())
            {
                Console.WriteLine("Clients table already seeded. Skipping...");
                return;
            }




        var clients = new List<Clients>();

            // Same Country, Same City
            foreach (var country in _countries)
            {
                var city = _cities[Array.IndexOf(_countries, country)][0];
                clients.AddRange(CreateClients(country, city, 1));
            }

            // Same Country, Different Cities
            foreach (var country in _countries)
            {
                for (int i = 1; i < _cities[Array.IndexOf(_countries, country)].Length; i++)
                {
                    var city = _cities[Array.IndexOf(_countries, country)][i];
                    clients.AddRange(CreateClients(country, city, 1));
                }
            }
            // Different Countries, Different Cities
            for (int i = 0; i < _countries.Length - 1; i++)
            {
                for (int j = i + 1; j < _countries.Length; j++)
                {
                    clients.AddRange(CreateClients(_countries[i], _cities[j][0], 1));
                }
            }

            await _context.Clients.AddRangeAsync(clients);
            await _context.SaveChangesAsync();
        }

        private List<Clients> CreateClients(string country, string city, int count)
        {
            var clients = new List<Clients>();
            for (int i = 0; i < count; i++)
            {
                clients.Add(new Clients
                {
                    Name = $"Client {country}-{city}-{i + 1}",
                    Surname = "Doe",
                    MobileNumber = "123-456-7890",
                    Gender = "M",
                    EmailAddress = $"client{country}{city}{i + 1}@example.com",
                    Country = country,
                    City = city,
                });
            }
            return clients;
        }



    }
}

