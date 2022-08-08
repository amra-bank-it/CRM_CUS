using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class LimitsTrnMonth
    {
        public Guid? PersonId { get; set; }
        public Guid? TransactionTypeId { get; set; }
        public decimal? Amount { get; set; }
        public Guid? CurrencyId { get; set; }
        public decimal? TrnMonthlyAmount { get; set; }
        public int? TrnMonthlyCount { get; set; }
        public string? DescriptionRu { get; set; }
    }
}
