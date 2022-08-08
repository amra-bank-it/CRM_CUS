using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class LimitsTrnDaily
    {
        public Guid? PersonId { get; set; }
        public Guid? TransactionTypeId { get; set; }
        public decimal? Amount { get; set; }
        public Guid? CurrencyId { get; set; }
        public decimal? TrnDailyAmount { get; set; }
        public int? TrnDailyCount { get; set; }
        public string? DescriptionRu { get; set; }
    }
}
