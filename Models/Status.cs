using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class Status
    {
        public Guid? SourceId { get; set; }
        public Guid? SourceTab { get; set; }
        public DateTime? CreateStamp { get; set; }
        public DateTime? StatusStamp { get; set; }
        public string? Description { get; set; }
    }
}
