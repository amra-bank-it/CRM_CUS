using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class Address
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? House { get; set; }
        public string? Building { get; set; }
        public string? Apartment { get; set; }
        public Guid? AddressTypeId { get; set; }

        public virtual AddressType? AddressType { get; set; }
        public virtual Person? Person { get; set; }
    }
}
