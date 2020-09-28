
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "homepage",
                url: "Homepage/",
                defaults: new { Controller = "Home", Action = "HomePage" }
                );

            routes.MapRoute(
                name: "post-list",
                url: "post/all-posts/{CategoryName}/{CategoryId}",
                defaults: new { Controller = "Content", Action = "ContentList", CategoryId = UrlParameter.Optional, CategoryName = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "post",
                url: "all-posts/{CategoryName}/{PostTitle}/{PostId}",
                defaults: new
                {
                    Controller = "Content",
                    Action = "Content",
                    PostId = UrlParameter.Optional,
                    CategoryName = UrlParameter.Optional,
                    PostTitle = UrlParameter.Optional
                }
                );


            routes.MapRoute(
                name: "category-list",
                url: "category/all-categories/",
                defaults: new { Controller = "Category", Action = "Categories" }
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "HomePage",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}
