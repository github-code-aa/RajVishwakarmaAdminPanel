using Parichay.Bussiness.Model;
using System.Collections.Generic;
namespace Parichay.Service
{
    public interface ISalesService
    {
       
        List<SalesViewModel> GetCustomer();
        SpScalar AddSales(SalesViewModel Model);
        SpScalar DeleteCustomer(int id);
        SalesViewModel GetSerializedSalesBySalesId(int id);
        ReportPDF GetSerializedSalesReportBySalesId(int id);
        bool validateSecurityCode(string purpose, string passkey);
        List<SalesIdList> GetSalesIdListbyCustomerId(int CustomerId);

    }
}
