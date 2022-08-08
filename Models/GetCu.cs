using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class GetCu
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid Id { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Birthplace { get; set; }
        public string? Gender { get; set; }
        public string? CountryOfOrigin { get; set; }
        public int? Phone { get; set; }
        public string? FormattedPhone { get; set; }
        public string? CodeNumber { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? House { get; set; }
        public string? Building { get; set; }
        public string? Apartment { get; set; }
        public string? NameType { get; set; }
        public string? Description { get; set; }
        public string? CodeDocument { get; set; }
        public string? CountryDocument { get; set; }
        public string? CodeCountryDocument { get; set; }
        public string? DescriptionRu { get; set; }
        public string? DecriptionEn { get; set; }
        public string? Serial { get; set; }
        public string? Number { get; set; }
        public DateTime? IdDate { get; set; }
        public string? IdWhom { get; set; }
        public string? IdWhomCode { get; set; }
        public DateTime? StatusStamp { get; set; }
        public string? StPersDescription { get; set; }
    }
}
