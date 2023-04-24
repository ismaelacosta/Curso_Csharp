using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IntroduccionAEFCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntroduccionAEFCore.Data
{
    public class DataContext : DbContext
    {

        public DbSet<Gender> Genders { set; get; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Commentary> Commentaries { get; set; }
                public DbSet<MovieActor> MovieActors { get; set; }




        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Examine the project and get the configurations for the database tables
        }
    }
}