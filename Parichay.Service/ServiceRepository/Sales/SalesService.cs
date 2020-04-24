using Parichay.Bussiness.Model;
using Parichay.Data;
using System;
using System.Collections.Generic;

namespace Parichay.Service
{
    [Serializable]
    public class SalesService : ISalesService
    {

        private ISalesData _ISalesData;
        private ISecurityCodeService _ISecurityCodeService;
        public SalesService(ISalesData SalesData, ISecurityCodeService ISecurityCodeService)
        {
            _ISecurityCodeService = ISecurityCodeService;
            _ISalesData = SalesData;

        }

        public List<SalesViewModel> GetCustomer()
        {
            return _ISalesData.GetCustomer();
        }

        public bool validateSecurityCode(string purpose, string passkey)
        {
            var Validate = _ISecurityCodeService.GetSinglepasskeyDetail(purpose, passkey);
            if (Validate == null)
                return false;
            if (Validate.Purpose == purpose && Validate.Password == passkey)
                return true;
            else
                return false;
        }

        public SpScalar DeleteCustomer(int id)
        {
            return _ISalesData.DeleteCustomer(id);
        }

        public SpScalar AddSales(SalesViewModel Model)
        {
            return _ISalesData.AddSales(Model);
        }

        public SalesViewModel GetSerializedSalesBySalesId(int id)
        {
            return _ISalesData.GetSerializedSalesBySalesId(id);
        }

        public ReportPDF GetSerializedSalesReportBySalesId(int id)
        {
            return _ISalesData.GetSerializedSalesReportBySalesId(id);
        }

        public List<SalesIdList> GetSalesIdListbyCustomerId(int CustomerId)
        {
            return _ISalesData.GetSalesIdListbyCustomerId(CustomerId);
        }
    }
}
