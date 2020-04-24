using Parichay.Bussiness.Model;
using Parichay.Service;
using System;
using System.Web;
using System.Web.Mvc;
namespace Parichay.Mvc.Controllers
{
    public class LoginController : Controller
    {
        private IUserService _IUserService;
        public LoginController(IUserService IUserService)
        {
            _IUserService = IUserService;
        }
        public ActionResult Index()
        {
            var UserData = (UserMst)Session["UserData"];
            if (UserData != null)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }


        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(UserMst user)
        {
            try
            {
                var IsvalidUser = _IUserService.CheckValidUser(user.LoginID,user.Password);
                if(IsvalidUser)
                {
                    var userDetail = _IUserService.GetUserByName(user.LoginID);
                    string QualifiedName = $"{userDetail.FirstName } {userDetail.LastName}";

                    //Update session Id in database

                    Session["UserData"] = userDetail;
                    Session["UserName"] = QualifiedName.ToUpper();


                    return Json(new { success = true, Caption = "WELCOME "+ QualifiedName.ToUpper()});
                }
                else
                {
                    return Json(new { success = false, Caption = "USERNAME OR PASSWORD IS INCORRECT" });
                }
            }
            catch
            {
                return Json(new { success = false, ExceptionMessage = "OOPS..!! WE HIT INTO A PROBLEM CONTACT TO YOUR ADMINSTRATOR." });
            }
        }

        public JsonResult ExtendSession()
        {
            Session.Timeout = 20;
            if(Session.Timeout == 20)
            {
                return Json(new { success = true, Message = "SESSION EXTENDED" });
            }
            else
            {
                return Json(new { success = false, Message = "SESSION EXTENDED FAILED" });
            }
        }

        public JsonResult EndSession()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Session.Abandon();
            Session.Clear();
            return Json(new { success = true, Message = "SESSION REMOVED" });
        }

        public JsonResult GetSession()
        {
            var GetTimeOut = System.Web.HttpContext.Current.Session.Timeout;
            int SetTimout = (Convert.ToInt32(GetTimeOut) * 60);
            return Json(SetTimout, JsonRequestBehavior.AllowGet);
        }
    }
}