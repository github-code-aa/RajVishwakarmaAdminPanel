using System;
namespace Parichay.Bussiness.Model
{
    public class SalesViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string SalesId { get; set; }
        public string SalesDate { get; set; }
        public string CreatedDate { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal Discount { get; set; }

        public string TaxSlab { get; set; }
        public int DefaultCustomerAddressId { get; set; }
        public double Amount { get; set; }
        public string Remarks { get; set; }

        public string SalesItemGridSerialize { get; set; }
        public int SalesItemId { get; set; }
        public string ItemName { get; set; }
        public int Warranty { get; set; }
        public int Qunatity { get; set; }
        public string HSN { get; set; }
        public int ItemAmount { get; set; }
        public int SalesItemSalesId { get; set; }
    }

}
