﻿using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Dictionaries;

namespace Domain.Mapping.Dictionaries
{
    public class FamilyStatusMap : EntityTypeConfiguration<FamilyStatus>
    {
        public FamilyStatusMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(50);
            HasMany(a => a.Persons).WithOptional(p => p.FamilyStatus).HasForeignKey(p => p.FamilyStatusId);
            Property(t => t.LastUpdUS).IsRequired().HasMaxLength(50);
            ToTable("FamilyStatus");
        }
    }
}
