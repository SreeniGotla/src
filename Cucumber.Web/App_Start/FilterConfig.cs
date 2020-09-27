using Cucumber.Web.Filters;
using System.Web.Mvc;

namespace Cucumber.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AntiForgeryAttribute());
        }
    }
}
