using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Parichay.Bussiness.Model
{
    //[Table("Global.UserMst")]
    public class UserMst : AuditableEntity<UserMst>
    {
        public UserMst()
        {
        }

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="PLEASE ENTER USERNAME")]
        public int ParentId { get; set; }
        public string LoginID { get; set; }

        [Required(ErrorMessage = "PLEASE ENTER PASSWORD")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string PhoneNumber { get; set; }
        public string LoggedInIp { get; set; }
        public DateTime LastLoginDateTime { get; set; }
    }
}
