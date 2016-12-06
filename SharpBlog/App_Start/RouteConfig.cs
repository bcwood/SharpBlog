using System.Web.Mvc;
using System.Web.Routing;

namespace SharpBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Post",
                url: "posts/{slug}",
                defaults: new { controller = "Home", action = "Post" }
            );

            routes.MapRoute(
                name: "Tag",
                url: "tag/{slug}",
                defaults: new { controller = "Home", action = "Tag" }
            );

            routes.MapRoute(
                name: "Page",
                url: "{slug}",
                defaults: new { controller = "Home", action = "Page" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
