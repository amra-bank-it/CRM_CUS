using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            ChannelLimits = new HashSet<ChannelLimit>();
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string? NameType { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ChannelLimit> ChannelLimits { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
