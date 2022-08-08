using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class Isocurrency
    {
        public Isocurrency()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string? NameCurrency { get; set; }
        public string? CodeCurrency { get; set; }
        public string? DescriptionRu { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
