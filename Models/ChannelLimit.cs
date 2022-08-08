using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class ChannelLimit
    {
        public ChannelLimit()
        {
            ChannelDeposits = new HashSet<ChannelDeposit>();
        }

        public Guid Id { get; set; }
        public decimal? TrnDailyAmount { get; set; }
        public decimal? TrnMonthlyAmount { get; set; }
        public int? TrnDailyCount { get; set; }
        public int? TrnMonthlyCount { get; set; }
        public string? DescriptionRu { get; set; }
        public Guid? TransactionTypeId { get; set; }

        public virtual TransactionType? TransactionType { get; set; }
        public virtual ICollection<ChannelDeposit> ChannelDeposits { get; set; }
    }
}
