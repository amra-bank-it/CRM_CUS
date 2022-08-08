using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class TypeDocument
    {
        public TypeDocument()
        {
            Passports = new HashSet<Passport>();
        }

        public Guid Id { get; set; }
        public string? CodeDocument { get; set; }
        public string? CountryDocument { get; set; }
        public string? CodeCountryDocument { get; set; }
        public string? DescriptionRu { get; set; }
        public string? DecriptionEn { get; set; }

        public virtual ICollection<Passport> Passports { get; set; }
    }
}
