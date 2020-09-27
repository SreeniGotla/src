using System;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Cucumber.Web.Filters
{
    public sealed class AntiForgeryAttribute : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                return;

            if (filterContext.RequestContext.HttpContext.Request.HttpMethod != HttpMethod.Post.Method)
                return;

            AntiForgery.Validate();
        }
    }
}