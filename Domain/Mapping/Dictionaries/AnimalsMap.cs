﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Entities.Dictionaries;

namespace Domain.Mapping.Dictionaries
{
    public class AnimalsMap : EntityTypeConfiguration<Animals>
    {
        public AnimalsMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(50);
            Property(t => t.LastUpdUS).IsRequired().HasMaxLength(50);
            ToTable("Animals");
        }
    }
}
