namespace Parichay.Bussiness.Model
{
    public class SecurePassword : AuditableEntity<SecurePassword>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Purpose { get; set; }
        public string Password { get; set; }
    }
}
