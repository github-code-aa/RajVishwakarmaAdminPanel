using Parichay.Bussiness.Model;
using Parichay.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Parichay.Data
{
    [Serializable]
    public class CustomerData : BaseData<Customer>, ICustomerData
    {
        public CustomerData(IContext context, ISPContext spContext) : base(context, spContext)
        {
            _context = context;
            _spContext = spContext;
            _dbset = context.Set<Customer>();
        }
        public SpScalar AddCustomer(CustomerViewModel Model)
        {
            return contextdb.executeSP<SpScalar>("Exec {0} {1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", "_SP_AddCustomerWithAddress", Model.Id,
                Model.FirstName, string.IsNullOrEmpty(Model.MiddleName) ? string.Empty : Model.MiddleName, Model.LastName, Model.Gender, Model.EmailId = Model.EmailId ?? string.Empty, Model.GstNumber,
                Model.MobileNumberOne, Model.MobileNumbertwo = Model.MobileNumbertwo ?? string.Empty, Model.Remark = Model.Remark ?? string.Empty, Model.AddressGridSerialize).ToList().SingleOrDefault();

            //return _spContext.AddCustomerWithAddress(
            //                            Id: deliveryOrder.StatusId,
            //                            FirstName: deliveryOrder.CreatedBy,
            //                            MiddleName: deliveryOrder.CreatedDate,
            //                            LastName: deliveryOrder.IsActive,
            //                            Gender: deliveryOrder.IsLatest,
            //                            EmailId: deliveryOrder.CreatedByRoleId,
            //                            MobileNumberOne: deliveryOrder.ModifiedByRoleId,
            //                            MobileNumbertwo: deliveryOrder.Id,
            //                            Remark: deliveryOrder.FinalRemarks,
            //                            AddressGridSerialize: deliveryOrder.ClosureDate
            //            );
        }

        public List<CustomerViewModel> GetCustomer()
        {
            return _context.Customer
                .Where(x => x.IsActive == true & x.IsDeleted == false)
                .Select(c => new CustomerViewModel { Id = c.Id, FirstName = c.FirstName, MiddleName = c.MiddleName, LastName = c.LastName, Gender = c.Gender, EmailId = c.EmailId, GstNumber = c.GstNumber ,MobileNumberOne=c.MobileNumberOne}).ToList();

        }
        public SpScalar DeleteCustomer(int id)
        {
            return _spContext.getScalarSP<SpScalar>("Exec {0} {1}", "_SP_DeleteCustomerWithAddress", id);
        }

        public Customer GetSingleCustomerById(int id)
        {
            return _context.Customer
                .Where(x => x.IsActive == true & x.IsDeleted == false & x.Id == id).SingleOrDefault();


        }

        public CustomerViewModel GetSerializedCustomerByCutomerId(int id)
        {
            return _spContext.getScalarSP<CustomerViewModel>("Exec {0} {1}", "_SP_GetSerializedCustomerByCutomerId", id);
        }
    }
}
