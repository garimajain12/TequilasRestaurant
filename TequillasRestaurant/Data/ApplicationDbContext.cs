using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Emit;
using TequillasRestaurant.Models;

namespace TequillasRestaurant.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);

            //Seed Data
            modelBuilder.Entity<Category>().HasData(
               new Category { CategoryId = 1, Name = "Appetizer" },
               new Category { CategoryId = 2, Name = "Entree" },
               new Category { CategoryId = 3, Name = "Side Dish" },
               new Category { CategoryId = 4, Name = "Dessert" },
               new Category { CategoryId = 5, Name = "Beverage" }
            );

            // Vegetarian Ingredients
            modelBuilder.Entity<Ingredient>().HasData(
               new Ingredient { IngredientId = 1, Name = "Paneer" },
               new Ingredient { IngredientId = 2, Name = "Mushroom" },
               new Ingredient { IngredientId = 3, Name = "Tofu" },
               new Ingredient { IngredientId = 4, Name = "Capsicum" },
               new Ingredient { IngredientId = 5, Name = "Carrot" },
               new Ingredient { IngredientId = 6, Name = "Cucumber" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Paneer Taco",
                    Description = "A delicious taco filled with paneer and fresh vegetables",
                    Price = 2.50m,
                    Stock = 100,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Mushroom Burrito",
                    Description = "A flavorful burrito stuffed with mushrooms and beans",
                    Price = 3.50m,
                    Stock = 80,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Tofu Quesadilla",
                    Description = "A tasty quesadilla with tofu and melted cheese",
                    Price = 3.99m,
                    Stock = 90,
                    CategoryId = 2
                }
            );

            modelBuilder.Entity<ProductIngredient>().HasData(
               new ProductIngredient { ProductId = 1, IngredientId = 1 },
               new ProductIngredient { ProductId = 1, IngredientId = 4 },
               new ProductIngredient { ProductId = 1, IngredientId = 5 },
               new ProductIngredient { ProductId = 1, IngredientId = 6 },
               new ProductIngredient { ProductId = 2, IngredientId = 2 },
               new ProductIngredient { ProductId = 2, IngredientId = 4 },
               new ProductIngredient { ProductId = 2, IngredientId = 5 },
               new ProductIngredient { ProductId = 2, IngredientId = 6 },
               new ProductIngredient { ProductId = 3, IngredientId = 3 },
               new ProductIngredient { ProductId = 3, IngredientId = 4 },
               new ProductIngredient { ProductId = 3, IngredientId = 5 },
               new ProductIngredient { ProductId = 3, IngredientId = 6 }
            );



        }
    }
}
