using System.Collections.Generic;
using Parichay.Bussiness.Model;
using Parichay.Data.Common;
namespace Parichay.Data
{
    public interface ICustomerData : IBaseData<Customer>
    {
        List<CustomerViewModel> GetCustomer();
        SpScalar AddCustomer(CustomerViewModel Model);
        CustomerViewModel GetSerializedCustomerByCutomerId(int id);
        Customer GetSingleCustomerById(int id);
        SpScalar DeleteCustomer(int id);
    }
}
