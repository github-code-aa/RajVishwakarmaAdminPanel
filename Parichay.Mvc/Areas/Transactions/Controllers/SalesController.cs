using Newtonsoft.Json;
using Parichay.Bussiness.Model;
using Parichay.Service;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Parichay.Mvc.Areas.Transactions.Controllers
{
    [ValidateSession]
    public class SalesController : Controller
    {
        private ISalesService _ISalesService;
        public SalesController(ISalesService ISalesService)
        {
            _ISalesService = ISalesService;
        }

        // GET: Transactions/Sales
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult PoopulateSales()
        {
            var result = _ISalesService.GetCustomer();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(SalesViewModel model, int id)
        {
            //ViewBag.ModalAccesFor = "Sales";

            if (id == 0)
            {
                // New Add Form
                model.Id = id;
                return View(model);
            }
            else
            {
                model = _ISalesService.GetSerializedSalesBySalesId(id);
                //Edit Form
                return View(model);
            }

        }

        [HttpPost]
        public ActionResult CreateEditSales(SalesViewModel model)
        {
            var result = _ISalesService.AddSales(model);
            if (result.STATUS.Contains("SUCCESS"))
                return Json(new { success = true, Message = "ADDED" });
            else
                return Json(new { success = false, Message = "FAILED" });
        }

        [HttpPost]
        public ActionResult Delete(int id, string purpose, string passkey)
        {
            try
            {

                // VALIDATE WITH RIGHT SECURITYCODE
                if (_ISalesService.validateSecurityCode(purpose, passkey))
                {
                    //FINAL DELETE
                    var status = _ISalesService.DeleteCustomer(id);
                    return Json(new { success = true, Message = "DELETED" });
                }
                else
                {
                    return Json(new { success = false, ErrorMessage = "WRONGPASSKEY" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrorMessage = "Exception" });
            }
        }

        public ActionResult InvoicePDF()
        {
            var result = _ISalesService.GetSerializedSalesReportBySalesId(1);

            var salesItemList = JsonConvert.DeserializeObject<List<SALESITEMSERIALZED>>(result.SALESITEM);

            var Report = new ReportPDF
            {
                SALESITEMS = salesItemList,
                FULLNAME = result.FULLNAME,
                MOBILENUMBER = result.MOBILENUMBER,
                GSTNUMBER = result.GSTNUMBER,
                ADDRESSLINEONE = result.ADDRESSLINEONE,
                ADDRESSLINETWO = result.ADDRESSLINETWO,
                STATE = result.STATE,
                CITY = result.CITY,
                COUNTRY = result.COUNTRY,
                PINCODE = result.PINCODE,
                SALESDATE = result.SALESDATE,
                SALESID = result.SALESID,
                REMARKS = result.REMARKS,
                SALESAMOUNT = result.SALESAMOUNT,
                TAXSLAB = result.TAXSLAB,
                IGST = result.IGST,
                CGST = result.CGST,
                SGST = result.SGST,
                SALESAMOUNTGST = result.SALESAMOUNTGST
            };
            return View(Report);
        }

        [AllowAnonymous]
        
        public ActionResult InvoicePDFDownload(int id)
        {

            var result = _ISalesService.GetSerializedSalesReportBySalesId(id);

            var salesItemList = JsonConvert.DeserializeObject<List<SALESITEMSERIALZED>>(result.SALESITEM);

            var Report = new ReportPDF
            {
                SALESITEMS = salesItemList,
                FULLNAME = result.FULLNAME,
                MOBILENUMBER =result.MOBILENUMBER,
                GSTNUMBER = result.GSTNUMBER,
                ADDRESSLINEONE= result.ADDRESSLINEONE,
                ADDRESSLINETWO=result.ADDRESSLINETWO,
                STATE=result.STATE,
                CITY = result.CITY,
                COUNTRY = result.COUNTRY,
                PINCODE = result.PINCODE,
                SALESDATE = result.SALESDATE,
                SALESID = result.SALESID,
                REMARKS = result.REMARKS,
                SALESAMOUNT = result.SALESAMOUNT,
                TAXSLAB = result.TAXSLAB,
                IGST = result.IGST,
                CGST = result.CGST,
                SGST = result.SGST,
                SALESAMOUNTGST = result.SALESAMOUNTGST,
                DISCOUNT=result.DISCOUNT
            };


            ViewBag.ReportNameTitle= result.SALESID;



            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            string dir = "~/Areas/Transactions/Views/Sales/";
            string ext = ".cshtml";
            var filename = "INVOICE_" + result.SALESID + ".pdf";
            string fullpath = Path.Combine(Server.MapPath("~/temp"), filename);
            var file = new ViewAsPdf(dir + "InvoicePDF" + ext, Report)
            {
                FileName = result.SALESID,
                CustomSwitches = "--print-media-type",
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0),
                PageWidth = 210,
                PageHeight = 297,
                Cookies = cookieCollection
               // IsGrayScale = true
            };
            var bytearray = file.BuildPdf(this.ControllerContext);
            var filestream = new FileStream(fullpath, FileMode.Create, FileAccess.Write);
            filestream.Write(bytearray, 0, bytearray.Length);
            filestream.Close();
            return Json(new { fileName = filename });
        }

        [HttpGet]
        [DeleteFile] //Action Filter, it will auto delete the file after download, 
        public ActionResult Download(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/temp"), file);

            return File(fullPath, "application/pdf", file);
        }

        public JsonResult GetSalesIdByCustomerId(int customerId)
        {
            var result = _ISalesService.GetSalesIdListbyCustomerId(customerId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}