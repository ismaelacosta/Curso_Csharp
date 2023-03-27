﻿using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;
using Shopping.Enums;
using Shopping.Helpers;

namespace Shopping.Data
{
    public class SeedDb 
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); // Crea la base de datos y aplica las migraciones

            await CheckCategoriesAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();

            await CheckUserAsync("1010", "Roberto", "Acosta","ismael10barca@yopmail.com","3223114620", "Calle Durazno", UserType.Admin);
            
           

        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phone,
                    Adrress = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>
                    {
                        new State
                        {
                            Name = "Antioquia",
                            Cities = new List<City>()
                            {
                                new City {Name  = "Medellin"},
                                new City {Name  = "Envigado"},
                                new City {Name  = "Itagui"},
                                new City {Name  = "Bello"},
                                new City {Name  = "Rio negro"},
                            }
                        },
                        new State
                        {
                            Name = "Bogota",
                            Cities = new List<City>
                            {
                                new City {Name  = "Bosa"},
                                new City {Name  = "Chapinero"},
                                
                            }
                        },

                    },
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCategoriesAsync()
        {
            if(!_context.Categories.Any())
            {
                _context.Categories.Add(new Entities.Category { Name = "Tecnologia" });
                _context.Categories.Add(new Entities.Category { Name = "Ropa" });
                _context.Categories.Add(new Entities.Category { Name = "Calzado" });
                _context.Categories.Add(new Entities.Category { Name = "Belleza" });
                _context.Categories.Add(new Entities.Category { Name = "Nutricion" });
                _context.Categories.Add(new Entities.Category { Name = "Deportes" });
                _context.Categories.Add(new Entities.Category { Name = "Apple" });
                _context.Categories.Add(new Entities.Category { Name = "Mascotas" });

                await _context.SaveChangesAsync();
            }

        }
    }
}
