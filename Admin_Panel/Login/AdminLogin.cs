using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin_Panel.Login
{
    public class AdminLogin : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                if (HttpContext.Current.Session["Login"]!=null){

                    base.OnActionExecuted(filterContext);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/Home/Login");
                }

            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("/Home/Login");

            }
        }
    }
}