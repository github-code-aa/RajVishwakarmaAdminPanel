using Parichay.Bussiness.Model;
using Parichay.Data;
using System;
using System.Collections.Generic;

namespace Parichay.Service
{
    [Serializable]
    public class CustomerPaymentService : ICustomerPaymentService
    {
        private readonly ICustomerPaymentData _CustomerPaymentData;
        

        public CustomerPaymentService(ICustomerPaymentData CustomerPaymentData)
        {
            _CustomerPaymentData = CustomerPaymentData;
        }

        public SpScalar AddCustomerPayment(CustomerPaymentViewModel Model)
        {
           return _CustomerPaymentData.AddCustomerpayment(Model);
        }

        public CustomerPaymentViewModel CustomerpaymentSummary(string SalesId)
        {
            return _CustomerPaymentData.CustomerpaymentSummary(SalesId);
        }
    }
}
