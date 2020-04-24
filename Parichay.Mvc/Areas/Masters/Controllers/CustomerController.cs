using Parichay.Bussiness.Model;
using Parichay.Service;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Parichay.Mvc.Areas.Masters.Controllers
{
    [ValidateSession]
    public class CustomerController : Controller
    {
        // GET: Masters/Customer
        private ICustomerService _ICustomerService;

        public CustomerController(ICustomerService ICustomerService)
        {
            _ICustomerService = ICustomerService;
        }
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public JsonResult PoopulateCustomer()
        {
            var result = _ICustomerService.GetCustomer();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AutofillCustomer(string Prefix)
        {
            var ObjList = _ICustomerService.GetCustomer();
            if (Prefix.ToLower() == "all")
            {
                var allresult = ObjList.Select(N => new { N.FullName, N.Id }).ToList();
                return Json(allresult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = ObjList.Where(x => x.FullName.ToLower().Contains(Prefix.ToLower())).Take(5).Select(N => new { N.FullName, N.Id }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create(CustomerViewModel model, int id)
        {
            ViewBag.ModalAccesFor = "CustomerMaster";
           
            if (id == 0)
            {
                // New Add Form
                model.Id = id;
                return View(model);
            }
            else
            {
                model = _ICustomerService.GetSerializedCustomerByCutomerId(id);
                //Edit Form
                return View(model);
            }

        }

        public ActionResult LoadForm(int id)
        {
            if (id == 0)
            {
                // New Add Form
                var result = "NEW";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
               var result = _ICustomerService.GetSerializedCustomerByCutomerId(id);
                //Edit Form
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateEditCustomer(CustomerViewModel model)
        {
            var result = _ICustomerService.AddCustomer(model);
            if (result.STATUS.Contains("SUCCESS"))
                return Json(new { success = true, Message = "ADDED" });
            else
                return Json(new { success = false, Message = "FAILED" });
        }


        [HttpPost]
        public ActionResult Delete(int id,string purpose,string passkey)
        {
            try
            {

                // VALIDATE WITH RIGHT SECURITYCODE
                if (_ICustomerService.validateSecurityCode(purpose, passkey))
                {
                    //FINAL DELETE
                    var status = _ICustomerService.DeleteCustomer(id);
                    return Json(new { success = true, Message = "DELETED" });
                }
                else
                {
                    return Json(new { success = false, ErrorMessage = "WRONGPASSKEY" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, ErrorMessage = "Exception" });
            }
        }


    }
}