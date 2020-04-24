using System;
using System.Collections.Generic;
namespace Parichay.Bussiness.Model
{
    // [Table("Master.Customer")]
    public class SalesItem : AuditableEntity<SalesItem>
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Warranty { get; set; }
        public double Qunatity { get; set; }
        public string HSN { get; set; }
        public double Amount { get; set; }
        public int SalesId { get; set; }
        public virtual Sales Sale { get; set; }

    }
}
