namespace Parichay.Bussiness.Model
{
    //[Table("Master.Address")]
    public class Address : AuditableEntity<Address>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string AddressName { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string pincode { get; set; }
        public bool DefaultAddress { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
