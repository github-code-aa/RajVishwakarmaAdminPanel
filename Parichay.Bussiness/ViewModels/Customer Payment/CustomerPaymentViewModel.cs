using System;
using System.Collections.Generic;

namespace Parichay.Bussiness.Model
{
    public class CustomerPaymentViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string SalesId { get; set; }
        public List<SalesIdList> SalesIdLists { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Remarks { get; set; }

        public double TOTALAMOUNT { get; set; }
        public double PAIDAMOUNT { get; set; }
        public double PENDINGAMOUNT { get; set; }
        public string HISTORY { get; set; }
        public List<summarypayment> PaymentSummary { get; set; }

    }

    public class SalesIdList
    {
        public string SalesId { get; set; }
    }

    public class summarypayment
    {
        public double PaymentAmount { get; set; }
        public string CreatedDate { get; set; }
        public string Remarks { get; set; }


        
    }
}
