using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IntroduccionAEFCore.Data.Entities;

namespace IntroduccionAEFCore.Data.Entities.Configurations
{
    public class GenderConfig : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            //---------------------------- GENDER ---------------------------------------------------------------------
            //modelBuilder.Entity<Gender>().HasKey(g => g.Id);
            //modelBuilder.Entity<Gender>().Property(g => g.Name).HasMaxLength(150);
            var cienciaFiccion = new Gender { Id = 5, Name = "Ciencia Ficcion" };
            var animacion = new Gender { Id = 6, Name = "Animacion" };
            builder.HasData(cienciaFiccion, animacion);
            builder.HasIndex(p => p.Name).IsUnique();

        }
    }


}