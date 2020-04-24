using Parichay.Bussiness.Model;
using System.Collections.Generic;
namespace Parichay.Service
{
    public interface ICustomerService
    {
        SpScalar AddCustomer(CustomerViewModel Model);
        List<CustomerViewModel> GetCustomer();
        Customer GetSingleCustomerById(int id);
        CustomerViewModel GetSerializedCustomerByCutomerId(int id);
        SpScalar DeleteCustomer(int id);

        bool validateSecurityCode(string purpose, string passkey);
    }
}
