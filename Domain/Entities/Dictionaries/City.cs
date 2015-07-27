﻿using Domain.Abstract;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Entities.Dictionaries
{
    public class City : BaseDictionary
    {
        public int CityTypeID { get; set; }

        public int? DistrictID { get; set; }

        public int RegionID { get; set; }

        // FK to CityType
        [IgnoreDataMember]
        public virtual CityType CityType { get; set; }
        // FK to District
        [IgnoreDataMember]
        public virtual District District { get; set; }
        // FK to Region
        [IgnoreDataMember]
        public virtual Region Region { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
