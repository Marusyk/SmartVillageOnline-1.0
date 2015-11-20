﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Domain.Abstract;

namespace Domain.Entities.Dictionaries
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateBirth { get; set; }

        public bool Sex { get; set; }

        public bool IsResident { get; set; }

        public int? AddressBirthId { get; set; }

        public int? AddressLiveId { get; set; }

        public int? NationalityId { get; set; }

        public string IdentificationCode { get; set; }

        public string PassSeria { get; set; }

        public int? PassNr { get; set; }

        public DateTime? PassDate { get; set; }

        public int? PassAuthorityId { get; set; }

        public int? FamilyStatusId { get; set; }

        public int? CitizenshipId { get; set; }
        
        public int CatalogId { get; set; }

        public bool IsSojourn { get; set; }

        public byte? Photo { get; set; }

        public string PadFirstName { get; set; }

        public string PadName { get; set; }

        public string PadLastName { get; set; }

        public string DatFirstName { get; set; }

        public string DatName { get; set; }

        public string DatLastName { get; set; }

        // FK
        public virtual Address AddressBith { get; set; }
        public virtual Address AddressLive { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual PassAuthority PassAuthority { get; set; }
        public virtual FamilyStatus FamilyStatus { get; set; }
        public virtual Country Citizenship { get; set; }
        public virtual Catalog Catalog { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<People> Peoples { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Education> Educations { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<PersonDocuments> PersonDocuments { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Employment> Employments { get; set; }

        [IgnoreDataMember]
        public string FullName
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append(FirstName + " ");
                sb.Append(LastName + " ");
                sb.Append(MiddleName);
                return sb.ToString();
            }
        }
    }
}
