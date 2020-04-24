using Parichay.Bussiness.Model;
using Parichay.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Parichay.Data
{
    [Serializable]
    public class CustomerPaymentData : BaseData<Customerpayment>, ICustomerPaymentData
    {
        public CustomerPaymentData(IContext context, ISPContext spContext) : base(context, spContext)
        {
            _context = context;
            _spContext = spContext;
            _dbset = context.Set<Customerpayment>();
        }
        

        public SpScalar AddCustomerpayment(CustomerPaymentViewModel Model)
        {
            return contextdb.executeSP<SpScalar>("Exec {0} {1},{2},{3},{4}", "ADDCUSTOMERPAYMENT", Model.CustomerId,Model.SalesId,Model.PaymentAmount,Model.Remarks).ToList().SingleOrDefault();

        }

        public CustomerPaymentViewModel CustomerpaymentSummary(string SalesId)
        {
            return contextdb.executeSP<CustomerPaymentViewModel>("Exec {0} {1}", "_SP_CUSTOMERPAYMENTDSUMMARY", SalesId).ToList().SingleOrDefault();
        }
    }
}
