using System.Web;
using System.Web.Mvc;

namespace Uppgift_4_Mertan
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
