using System.Web.Optimization;

namespace Blog.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
         
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/bundles/layout/js").Include("~/Content/assets/js/jquery.min.js",
                                                                        "~/Content/assets/js/jquery.scrolly.min.js",
                                                                        "~/Content/assets/js/jquery.scrollex.min.js",
                                                                        "~/Content/assets/js/browser.min.js",
                                                                        "~/Content/assets/js/breakpoints.min.js",
                                                                        "~/Content/assets/js/util.js",
                                                                        "~/Content/assets/js/main.js",
                                                                        "~/Content/assets/js/sweetalert.min.js"));

            bundles.Add(new StyleBundle("~/bundles/layout/css").Include("~/Content/assets/css/main.css",
                                                                        "~/Content/assets/css/noscript.css",
                                                                        "~/Content/PagedList.css"));


        }
    }
}   