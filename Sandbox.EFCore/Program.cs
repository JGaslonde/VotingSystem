using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox.EFCore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<Address> Addressess { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Database");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fruit>().Property<int>("Id");
            modelBuilder.Entity<Address>().Property<int>("FruitId");
        }
    }

    public class Fruit
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string PostCode { get; set; }
    }

    public class FruitVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public string PostCode { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            int orangeId = 0;
            using (var ctx = new AppDbContext())
            {
                var orange = new Fruit { Name = "Orange" };
                var apple = new Fruit { Name = "Apple" };

                ctx.Fruits.Add(orange);

                orangeId = ctx.Entry(orange).Property<int>("Id").CurrentValue;

                ctx.Fruits.Add(apple);

                //orange.Address = address;

                ctx.SaveChanges();
            }
            using (var ctx = new AppDbContext())
            {
                var address = new Address { PostCode = "Moon" };

                orangeId = ctx.Entry(address).Property<int>("FruitId").CurrentValue = orangeId;

                ctx.Addressess.Add(address);
                ctx.SaveChanges();
            }
            using (var ctx = new AppDbContext())
            {
                var fruit = ctx.Fruits
                    .Include(x => x.Address)
                    .Select(x => new FruitVm
                    {
                        //Id = ctx.Entry(x).Property<int>("Id").CurrentValue,
                        Name = x.Name,
                        PostCode = x.Address.PostCode
                    })
                    .ToList();
            }

            Console.ReadLine();
        }
    }
}