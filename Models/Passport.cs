using System;
using System.Collections.Generic;

namespace CRM_CUS.Models
{
    public partial class Passport
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? TypeDocumentId { get; set; }
        public string? Serial { get; set; }
        public string? Number { get; set; }
        public DateTime? IdDate { get; set; }
        public string? IdWhom { get; set; }
        public string? IdWhomCode { get; set; }
        public DateTime? IdExpireDate { get; set; }

        public virtual Person? Person { get; set; }
        public virtual TypeDocument? TypeDocument { get; set; }
    }
}
