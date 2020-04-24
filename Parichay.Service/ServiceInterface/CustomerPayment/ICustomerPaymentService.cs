using Parichay.Bussiness.Model;
using System.Collections.Generic;
namespace Parichay.Service
{
    public interface ICustomerPaymentService
    {
        SpScalar AddCustomerPayment(CustomerPaymentViewModel Model);
        CustomerPaymentViewModel CustomerpaymentSummary(string SalesId);
    }
}
