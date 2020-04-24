using Parichay.Bussiness.Model;
using Parichay.Data;
using System;
using System.Collections.Generic;

namespace Parichay.Service
{
    [Serializable]
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerData _CustomerData;
        private ISecurityCodeService _ISecurityCodeService;

        public CustomerService(ICustomerData CustomerData, ISecurityCodeService ISecurityCodeService)
        {
            _ISecurityCodeService = ISecurityCodeService;
            _CustomerData = CustomerData;
        }
        public SpScalar AddCustomer(CustomerViewModel Model)
        {
            return _CustomerData.AddCustomer(Model);
        }
        public List<CustomerViewModel> GetCustomer()
        {
            return _CustomerData.GetCustomer();
        }

        public SpScalar DeleteCustomer(int id)
        {
          return _CustomerData.DeleteCustomer(id);
        }

        public Customer GetSingleCustomerById(int id)
        {
            return _CustomerData.GetSingleCustomerById(id);
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

        public CustomerViewModel GetSerializedCustomerByCutomerId(int id)
        {
            return _CustomerData.GetSerializedCustomerByCutomerId(id);
        }
    }
}
