using Parichay.Bussiness.Model;
using Parichay.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Parichay.Data
{
    [Serializable]
    public class SalesData : BaseData<Sales>, ISalesData
    {
        public SalesData(IContext context, ISPContext spContext) : base(context, spContext)
        {
            _context = context;
            _spContext = spContext;
            _dbset = context.Set<Sales>();
        }
   

        public List<SalesViewModel> GetCustomer()
        {
            return contextdb.executeSP<SalesViewModel>("Exec {0}", "_SP_GetSalesView").ToList();
            
        }
        public SpScalar DeleteCustomer(int id)
        {
            return _spContext.getScalarSP<SpScalar>("Exec {0} {1}", "_SP_DELETESALESWITHSALESITEMS", id);
        }

        public SpScalar AddSales(SalesViewModel Model)
        {
            return _spContext.getScalarSP<SpScalar>("Exec {0} {1},{2},{3},{4},{5},{6},{7}", "_SP_ADDSALESWITHSALESITEM", Model.Id, Model.CustomerId, Model.SalesItemGridSerialize, Model.SalesDate = Model.SalesDate ?? string.Empty, Model.Remarks = Model.Remarks ?? string.Empty, Model.TaxSlab, Model.Discount);
        }

        public SalesViewModel GetSerializedSalesBySalesId(int id)
        {
            return _spContext.getScalarSP<SalesViewModel>("Exec {0} {1}", "_SP_GETSERIALIZEDSALESBYSALESID", id);
        }
        public ReportPDF GetSerializedSalesReportBySalesId(int id)
        {
            return _spContext.getScalarSP<ReportPDF>("Exec {0} {1}", "SP_GETSERIALIZED_SALESREPORT", id);
        }

        public List<SalesIdList> GetSalesIdListbyCustomerId(int CustomerId)
        {
            return contextdb.executeSP<SalesIdList>("Exec {0} {1}", "GetSalesIdonCustomerId",CustomerId).ToList();
        }
    }
}
