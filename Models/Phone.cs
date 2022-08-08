using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class Phone
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        public string? CountryOfOrigin { get; set; }
        public string? FormattedPhone { get; set; }
        public string? CodeNumber { get; set; }
        public string? Phone1 { get; set; }

        public virtual Person? Person { get; set; }
    }
}
