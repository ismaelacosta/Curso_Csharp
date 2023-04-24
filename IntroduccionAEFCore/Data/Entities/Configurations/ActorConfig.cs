using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntroduccionAEFCore.Data.Entities.Configurations
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            //---------------------------- ACTOR ---------------------------------------------------------------------
            // modelBuilder.Entity<Actor>().Property(a => a.Name).HasMaxLength(150);
            builder.Property(a => a.BirthDate).HasColumnType("date");
            builder.Property(a => a.MoneySaved).HasPrecision(18, 2);   //16 digitos . 2 digitos
        }
    }
}