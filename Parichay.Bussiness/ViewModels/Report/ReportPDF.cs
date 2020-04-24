using System;
using System.Collections.Generic;

namespace Parichay.Bussiness.Model
{
    public class ReportPDF
    {
        public string FULLNAME { get; set; }
        public string MOBILENUMBER { get; set; }
        public string GSTNUMBER { get; set; }
        public string ADDRESSLINEONE { get; set; }
        public string ADDRESSLINETWO { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }
        public string COUNTRY { get; set; }
        public string PINCODE { get; set; }
        public string SALESDATE { get; set; }
        public string SALESID { get; set; }
        public string REMARKS { get; set; }
        public decimal SALESAMOUNT { get; set; }
        public string TAXSLAB { get; set; }
        public decimal IGST { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal SALESAMOUNTGST { get; set; }
        public decimal DISCOUNT { get; set; }
        public string SALESITEM { get; set; }

       public List<SALESITEMSERIALZED> SALESITEMS { get; set; }

    }
    public class SALESITEMSERIALZED
    {
        public int ITEMNO { get; set; }
        public string ITEMNAME { get; set; }
        public string HSN { get; set; }
        public decimal QUNATITY { get; set; }
        public string WARRANTY { get; set; }
        public decimal PRICE { get; set; }
        public decimal AMOUNT { get; set; }
    }



}
