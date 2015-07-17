﻿using System.Data.Entity.ModelConfiguration;
using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Mapping
{
    class HouseMap : EntityTypeConfiguration<House>
    {
        public HouseMap()
        {
            HasKey(t => t.ID);
            Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.HouseNr).IsRequired();
            Property(t => t.BuildNr).IsRequired();
            Property(t => t.AddressID).IsRequired();
            ToTable("House");
        }
    }
}
