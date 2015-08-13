﻿using System.Data.Entity.ModelConfiguration;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Mapping
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(50);
            HasMany(a => a.Regions).WithRequired(p => p.Country).HasForeignKey(p => p.CountryID);
            HasMany(a => a.Persons).WithOptional(p => p.Citizenship).HasForeignKey(p => p.CitizenshipId);
            Property(t => t.LastUpdUS).IsRequired().HasMaxLength(50);
            ToTable("Country");
        }
    }
}
