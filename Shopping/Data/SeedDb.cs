using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;
using Shopping.Enums;
using Shopping.Helpers;

namespace Shopping.Data
{
    public class SeedDb 
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;

        public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); // Crea la base de datos y aplica las migraciones

            await CheckCategoriesAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckProductsAsync();

            await CheckUserAsync("1010", "Roberto", "Acosta", "ismael@yopmail.com", "3223114620", "Calle Durazno","chicharito.jpg", UserType.Admin);
            await CheckUserAsync("2020", "Rafael", "Acosta", "rafa@yopmail.com", "3223114620", "Calle Durazno", "cristiano.jpg", UserType.User);
            await CheckUserAsync("3030", "Dulce", "Acosta", "dulce@yopmail.com", "3223114620", "Calle Durazno","messi.png",UserType.User);

        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            string image,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {

                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{image}", "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Adrress = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    ImageId = imageId,
                    
                    UserType = userType,
                };

                

                await _userHelper.AddUserAsync(user,"123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
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
                _context.Categories.Add(new Entities.Category { Name = "Alimentos" });
                _context.Categories.Add(new Entities.Category { Name = "Deportes" });
                _context.Categories.Add(new Entities.Category { Name = "Apple" });
                _context.Categories.Add(new Entities.Category { Name = "Mascotas" });

                await _context.SaveChangesAsync();
            }

        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Productcs.Any())
            {
                await AddProductAsync("Balon Void", 320M, 12F, new List<string>() {"Deportes" }, new List<string>() { "balon.jpg" });
                await AddProductAsync("Camisa", 50M, 20F, new List<string>() { "Ropa", "Deportes" }, new List<string>() { "camisa.png" });
                await AddProductAsync("Chetos", 30M, 12F, new List<string>() { "Alimentos" }, new List<string>() { "chetos.png" });
                await AddProductAsync("Iphone", 22000M, 10F, new List<string>() { "Tecnologia", "Apple" }, new List<string>() { "iphone.jpg" });
                await AddProductAsync("Leche Lala", 50M, 6F, new List<string>() { "Alimentos" }, new List<string>() { "leche.png" });
                await AddProductAsync("Mouse Gamer", 500M, 24F, new List<string>() { "Tecnologia" }, new List<string>() { "mouse.jpg" });
                await AddProductAsync("Galletas Oreo", 18M, 12F, new List<string>() { "Alimentos" }, new List<string>() { "oreo.jpg" });
                await AddProductAsync("Tennis Nike", 1200M, 6F, new List<string>() { "Calzado" }, new List<string>() { "tennis.png" });
                await AddProductAsync("Sobre Whiskas", 32M, 6F, new List<string>() { "Mascotas" }, new List<string>() { "whiskas.jpg" });

            }
        }

        private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
        {
            Product product = new()
            {
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                ProductCategories = new List<ProductCategory>(),
                ProductImages = new List<ProductImage>()
            };

            foreach (string? category in categories)
            {
                product.ProductCategories.Add(new ProductCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }


            foreach (string? image in images)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\products\\{image}", "products");
                product.ProductImages.Add(new ProductImage { ImageId = imageId });
            }

            _context.Productcs.Add(product);
        }

    }
}
