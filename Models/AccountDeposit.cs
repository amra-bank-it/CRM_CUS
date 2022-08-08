using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class AccountDeposit
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        public string? Account { get; set; }
        public string? AccountMasked { get; set; }
        public string? Bank { get; set; }
        public Guid? ChannelDepositId { get; set; }
        public bool? Available { get; set; }

        public virtual ChannelDeposit? ChannelDeposit { get; set; }
        public virtual Person? Person { get; set; }
    }
}
