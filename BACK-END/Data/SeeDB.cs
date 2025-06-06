﻿using BACK_END.Helpers;
using LIBRARY.Shared.Entity;
using LIBRARY.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace BACK_END.Data
{
    public class SeeDB
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserHelper _userHelper;

        public SeeDB(ApplicationDbContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckStatesAsync();
            await CheckCitiesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Juan", "Zuluaga", "zulu@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "JuanZuluaga.jpeg", UserType.Admin);
            await CheckUserAsync("2020", "Ledys", "Bedoya", "ledys@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "LedysBedoya.jpeg", UserType.User);
            await CheckUserAsync("3030", "Brad", "Pitt", "brad@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Brad.jpg", UserType.User);
            await CheckUserAsync("4040", "Angelina", "Jolie", "angelina@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Angelina.jpg", UserType.User);
            await CheckUserAsync("5050", "Bob", "Marley", "bob@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "bob.jpg", UserType.User);
            await CheckProductsAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country { Name = "Colombia" });
                _context.Countries.Add(new Country { Name = "Estados Unidos" });
                _context.Countries.Add(new Country { Name = "México" });
                _context.Countries.Add(new Country { Name = "Argentina" });
                _context.Countries.Add(new Country { Name = "España" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckStatesAsync()
        {
            if (!_context.States.Any())
            {
                var colombia = await _context.Countries.FirstOrDefaultAsync(c => c.Name == "Colombia");
                var estadosUnidos = await _context.Countries.FirstOrDefaultAsync(c => c.Name == "Estados Unidos");
                var mexico = await _context.Countries.FirstOrDefaultAsync(c => c.Name == "México");

                if (colombia != null)
                {
                    _context.States.Add(new State { Name = "Antioquia", CountryId = colombia.Id });
                    _context.States.Add(new State { Name = "Cundinamarca", CountryId = colombia.Id });
                    _context.States.Add(new State { Name = "Valle del Cauca", CountryId = colombia.Id });
                    _context.States.Add(new State { Name = "Atlántico", CountryId = colombia.Id });
                }

                if (estadosUnidos != null)
                {
                    _context.States.Add(new State { Name = "California", CountryId = estadosUnidos.Id });
                    _context.States.Add(new State { Name = "Texas", CountryId = estadosUnidos.Id });
                    _context.States.Add(new State { Name = "Florida", CountryId = estadosUnidos.Id });
                }

                if (mexico != null)
                {
                    _context.States.Add(new State { Name = "Ciudad de México", CountryId = mexico.Id });
                    _context.States.Add(new State { Name = "Jalisco", CountryId = mexico.Id });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCitiesAsync()
        {
            if (!_context.Cities.Any())
            {
                var antioquia = await _context.States.FirstOrDefaultAsync(s => s.Name == "Antioquia");
                var cundinamarca = await _context.States.FirstOrDefaultAsync(s => s.Name == "Cundinamarca");
                var valle = await _context.States.FirstOrDefaultAsync(s => s.Name == "Valle del Cauca");
                var atlantico = await _context.States.FirstOrDefaultAsync(s => s.Name == "Atlántico");

                if (antioquia != null)
                {
                    _context.Cities.Add(new City { Name = "Medellín", StateId = antioquia.Id });
                    _context.Cities.Add(new City { Name = "Bello", StateId = antioquia.Id });
                    _context.Cities.Add(new City { Name = "Envigado", StateId = antioquia.Id });
                    _context.Cities.Add(new City { Name = "Itagüí", StateId = antioquia.Id });
                }

                if (cundinamarca != null)
                {
                    _context.Cities.Add(new City { Name = "Bogotá", StateId = cundinamarca.Id });
                    _context.Cities.Add(new City { Name = "Soacha", StateId = cundinamarca.Id });
                    _context.Cities.Add(new City { Name = "Chía", StateId = cundinamarca.Id });
                }

                if (valle != null)
                {
                    _context.Cities.Add(new City { Name = "Cali", StateId = valle.Id });
                    _context.Cities.Add(new City { Name = "Palmira", StateId = valle.Id });
                }

                if (atlantico != null)
                {
                    _context.Cities.Add(new City { Name = "Barranquilla", StateId = atlantico.Id });
                    _context.Cities.Add(new City { Name = "Soledad", StateId = atlantico.Id });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Adidas Barracuda", 270000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "adidas_barracuda.png" });
                await AddProductAsync("Adidas Superstar", 250000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "Adidas_superstar.png" });
                await AddProductAsync("AirPods", 1300000M, 12F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "airpos.png", "airpos2.png" });
                await AddProductAsync("Audifonos Bose", 870000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "audifonos_bose.png" });
                await AddProductAsync("Bicicleta Ribble", 12000000M, 6F, new List<string>() { "Deportes" }, new List<string>() { "bicicleta_ribble.png" });
                await AddProductAsync("Camisa Cuadros", 56000M, 24F, new List<string>() { "Ropa" }, new List<string>() { "camisa_cuadros.png" });
                await AddProductAsync("Casco Bicicleta", 820000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "casco_bicicleta.png", "casco.png" });
                await AddProductAsync("iPad", 2300000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "ipad.png" });
                await AddProductAsync("iPhone 13", 5200000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "iphone13.png", "iphone13b.png", "iphone13c.png", "iphone13d.png" });
                await AddProductAsync("Mac Book Pro", 12100000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "mac_book_pro.png" });
                await AddProductAsync("Mancuernas", 370000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "mancuernas.png" });
                await AddProductAsync("Mascarilla Cara", 26000M, 100F, new List<string>() { "Belleza" }, new List<string>() { "mascarilla_cara.png" });
                await AddProductAsync("New Balance 530", 180000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance530.png" });
                await AddProductAsync("New Balance 565", 179000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance565.png" });
                await AddProductAsync("Nike Air", 233000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_air.png" });
                await AddProductAsync("Nike Zoom", 249900M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_zoom.png" });
                await AddProductAsync("Buso Adidas Mujer", 134000M, 12F, new List<string>() { "Ropa", "Deportes" }, new List<string>() { "buso_adidas.png" });
                await AddProductAsync("Suplemento Boots Original", 15600M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "Boost_Original.png" });
                await AddProductAsync("Whey Protein", 252000M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "whey_protein.png" });
                await AddProductAsync("Arnes Mascota", 25000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "arnes_mascota.png" });
                await AddProductAsync("Cama Mascota", 99000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "cama_mascota.png" });
                await AddProductAsync("Teclado Gamer", 67000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "teclado_gamer.png" });
                await AddProductAsync("Silla Gamer", 980000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "silla_gamer.png" });
                await AddProductAsync("Mouse Gamer", 132000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "mouse_gamer.png" });
                await _context.SaveChangesAsync();
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
                ProdCategories = new List<ProdCategory>(),
                ProductImages = new List<ProductImage>()
            };

            // Agregar categorías
            foreach (var categoryName in categories)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                if (category != null)
                {
                    product.ProdCategories.Add(new ProdCategory { Category = category });
                }
            }

            // Agregar imágenes
            foreach (var imageName in images)
            {
                product.ProductImages.Add(new ProductImage
                {
                    ImageUrl = $"/images/products/{imageName}",
                    ImageId = imageName
                });
            }

            _context.Products.Add(product);
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Autos" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Comida" });
                _context.Categories.Add(new Category { Name = "Cosmeticos" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Erótica" });
                _context.Categories.Add(new Category { Name = "Ferreteria" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Jardín" });
                _context.Categories.Add(new Category { Name = "Jugetes" });
                _context.Categories.Add(new Category { Name = "Lenceria" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);

            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                if (city == null)
                {
                    city = await _context.Cities.FirstOrDefaultAsync();
                }

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    CityId = city!.Id,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                // Agregar imagen de usuario si existe
                if (!string.IsNullOrEmpty(image))
                {
                    var userImage = new UserImage
                    {
                        UserId = user.Id,
                        ImageUrl = $"/images/users/{image}",
                        ImageId = image
                    };
                    _context.UserImages.Add(userImage);
                    await _context.SaveChangesAsync();
                }

                //var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                //await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }
    }
}