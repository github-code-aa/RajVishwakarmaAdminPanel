using System;
using System.Collections.Generic;
namespace Parichay.Bussiness.Model
{
    // [Table("Master.Customer")]
    public class Customerpayment : AuditableEntity<Customerpayment>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int CustomerId { get; set; }
        public string SalesId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Remarks { get; set; }

    }
}
