﻿using System.Data.Entity.ModelConfiguration;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Mapping.Dictionaries
{
    public class FamilyStatusMap : EntityTypeConfiguration<FamilyStatus>
    {
        public FamilyStatusMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(50);
            Property(t => t.LastUpdUS).IsRequired().HasMaxLength(50);
            ToTable("FamilyStatus");
        }
    }
}