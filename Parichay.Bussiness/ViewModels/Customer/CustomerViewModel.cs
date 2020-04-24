using System;

namespace Parichay.Bussiness.Model
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public string GstNumber { get; set; }
        public string MobileNumberOne { get; set; }
        public string MobileNumbertwo { get; set; }
        public string Remark { get; set; }

        public string AddressID { get; set; }
        public string AddressName { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string pincode { get; set; }
        public bool isDefault { get; set; }
        public string AddressGridSerialize { get; set; }




        private string _fullNmame;
        public string FullName {
            get { return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName); }
            set
            {
                _fullNmame = value;
            }
        }






    }
}
