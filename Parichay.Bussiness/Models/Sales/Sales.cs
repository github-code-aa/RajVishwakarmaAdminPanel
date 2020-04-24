using System;
using System.Collections.Generic;
namespace Parichay.Bussiness.Model
{
    // [Table("Master.Customer")]
    public class Sales : AuditableEntity<Sales>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string SalesId { get; set; }
        public string SalesDate { get; set; }
        public int CustomerId { get; set; }
        public int DefaultCustomerAddressId { get; set; }
        public double Amount { get; set; }
        public string Remarks { get; set; }
        public decimal Discount { get; set; }
        public string TaxSlab { get; set; }
        public virtual ICollection<SalesItem> SalesItem { get; set; }


    }
}
