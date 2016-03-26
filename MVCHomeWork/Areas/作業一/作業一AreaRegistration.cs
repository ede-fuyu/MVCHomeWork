using System.Web.Mvc;

namespace MVCHomeWork.Areas.作業一
{
    public class 作業一AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "作業一";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "作業一_default",
                "作業一/{controller}/{action}/{id}",
                new { action = "主頁", id = UrlParameter.Optional }
            );
        }
    }
}