using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            Addresses = new HashSet<Address>();
        }

        public Guid Id { get; set; }
        public string? NameType { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
