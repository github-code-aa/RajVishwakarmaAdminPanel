using Newtonsoft.Json;
using Parichay.Bussiness.Model;
using Parichay.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parichay.Mvc.Areas.Transactions.Controllers
{
    [ValidateSession]
    public class CustomerPaymentController : Controller
    {
        // GET: Transactions/CustomerPayment
        private ICustomerPaymentService _CustomerpaymentService;
        public CustomerPaymentController(ICustomerPaymentService CustomerpaymentService)
        {
            _CustomerpaymentService = CustomerpaymentService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Create(CustomerPaymentViewModel model)
        {
            var result = _CustomerpaymentService.AddCustomerPayment(model);
            if (result.STATUS.Contains("SUCCESS"))
                return Json(new { success = true, Message = "ADDED" });
            else
                return Json(new { success = false, Message = "FAILED" });

        }

        [HttpGet]
        public JsonResult PaymentSummary(string SalesId)
        {
            var result = _CustomerpaymentService.CustomerpaymentSummary(SalesId);
            var PaymentSummarys =new List<summarypayment>();
            try
            {
                 PaymentSummarys = JsonConvert.DeserializeObject<List<summarypayment>>(result.HISTORY);
            }
            catch
            {
                PaymentSummarys = new List<summarypayment>();
            }

            var data = new CustomerPaymentViewModel
            {
                TOTALAMOUNT = result.TOTALAMOUNT,
                PAIDAMOUNT = result.PAIDAMOUNT,
                PENDINGAMOUNT = result.PENDINGAMOUNT,
                PaymentSummary = PaymentSummarys
            };

            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}