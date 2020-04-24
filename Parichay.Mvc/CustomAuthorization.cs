using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Parichay.Mvc
{
    public class CustomAuthorization
    {
    }

    public class ValidateSession : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           // string ip = "";
            //getIP(out ip);


            if (filterContext.HttpContext.Session["UserData"] == null)
            {
                filterContext.HttpContext.Session["Message"] = "Session Expired";
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Index",
                    area = "",
                }));
            }
           // filterContext.HttpContext.Session.Timeout = 1;
        }

        private void getIP(out string ip)
        {
            string WindowsUsername = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            string strHostName = "";
            ip = GetUser_IP_Name();
            if (ip.Contains("::1"))
            {
                strHostName = System.Net.Dns.GetHostName();

                IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                IPAddress[] addr = ipEntry.AddressList;

                ip = addr[addr.Length - 1].ToString();
            }
            else
            {
                strHostName = ip.Split(':')[1];
                ip = ip.Split(':')[0];
            }
        }

        //ticket 50028
        protected string GetUser_IP_Name()
        {
            string VisitorsIPAddr = string.Empty;

            if (System.Web.HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = System.Web.HttpContext.Current.Request.UserHostAddress + ":" + System.Web.HttpContext.Current.Request.UserHostName;
            }

            return VisitorsIPAddr;

        }
        // end ticket 50028


    }
}