using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class ChannelDeposit
    {
        public ChannelDeposit()
        {
            AccountDeposits = new HashSet<AccountDeposit>();
        }

        public Guid Id { get; set; }
        public string? ChannelName { get; set; }
        public string? GroupName { get; set; }
        public string? DescriptionRu { get; set; }
        public string? DescriptionEn { get; set; }
        public Guid? ChannelLimitId { get; set; }

        public virtual ChannelLimit? ChannelLimit { get; set; }
        public virtual ICollection<AccountDeposit> AccountDeposits { get; set; }
    }
}
