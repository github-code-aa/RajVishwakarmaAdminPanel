using System.Collections.Generic;
using Parichay.Bussiness.Model;
using Parichay.Data.Common;
namespace Parichay.Data
{
    public interface ICustomerPaymentData : IBaseData<Customerpayment>
    {
        SpScalar AddCustomerpayment(CustomerPaymentViewModel Model);

        CustomerPaymentViewModel CustomerpaymentSummary(string SalesId);

    }
}
