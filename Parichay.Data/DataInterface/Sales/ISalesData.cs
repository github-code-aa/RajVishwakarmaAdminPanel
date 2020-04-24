using System.Collections.Generic;
using Parichay.Bussiness.Model;
using Parichay.Data.Common;
namespace Parichay.Data
{
    public interface ISalesData : IBaseData<Sales>
    {
        List<SalesViewModel> GetCustomer();
        SpScalar DeleteCustomer(int id);

        SalesViewModel GetSerializedSalesBySalesId(int id);

        ReportPDF GetSerializedSalesReportBySalesId(int id);
        SpScalar AddSales(SalesViewModel Model);
        List<SalesIdList> GetSalesIdListbyCustomerId(int CustomerId);
    }
}
