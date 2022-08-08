using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class Transaction
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        public DateTime? ServerTime { get; set; }
        public Guid? TransactionTypeId { get; set; }
        public decimal? Amount { get; set; }
        public Guid? CurrencyId { get; set; }

        public virtual Isocurrency? Currency { get; set; }
        public virtual Person? Person { get; set; }
        public virtual TransactionType? TransactionType { get; set; }
    }
}
