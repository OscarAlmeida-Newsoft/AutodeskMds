using System.Web;
using System.Web.Optimization;

namespace SOMSight
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bxslider/jquery.bxslider.min.js"
                        )
                        );

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                         //"~/Scripts/Custom/GlobalScripts.js"
                         "~/Scripts/Custom/loadingDiv.js"
                        //, "~/Scripts/Plugins/toastMessage.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/estilos.css",
                      "~/Scripts/bxslider/jquery.bxslider.css"));
        }
    }
}
