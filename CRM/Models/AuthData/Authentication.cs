using CRM.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace CRM.Models.AuthData
{
    public class Authentication : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (GlobalFunctions.GetUserId()==0)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                     { "controller", "Home" },
                     { "action", "Index" }
                });
            }
        }
    }
    public class Authorized : ActionFilterAttribute, IAuthorizationFilter
    {
       public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (GlobalFunctions.GetRoleId() == 0 || GlobalFunctions.GetRoleId() != 2)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
 
    }
}