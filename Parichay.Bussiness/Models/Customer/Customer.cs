using System;
using System.Collections.Generic;
namespace Parichay.Bussiness.Model
{
    // [Table("Master.Customer")]
    public class Customer : AuditableEntity<Customer>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        
        public string  FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public string GstNumber { get; set; }
        public string MobileNumberOne { get; set; }
        public string MobileNumbertwo { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
