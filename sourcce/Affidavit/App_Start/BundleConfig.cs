using System.Web;
using System.Web.Optimization;

namespace Affidavit
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        , "~/Scripts/jquery.datetimepicker.full.min.js"
                        , "~/Scripts/datepick.js"
                        , "~/Scripts/sobrescrituas.js"));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                        //"~/Scripts/Custom/GlobalScripts.js"
                         "~/Scripts/Custom/loadingDiv.js"
                        , "~/Scripts/Plugins/toastMessage.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive-ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      //"~/Scripts/Plugins/toastMessage.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/Custom/loadingDiv.js",
                      "~/Scripts/Plugins/bootstrap-datepicker/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapSomsight").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/Custom/summernote.js",
                      //"~/Scripts/Custom/bootbox.js",
                      //"~/Scripts/Custom/toastMessage.js",
                      "~/Scripts/Custom/loadingDiv.js",
                      "~/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/AffContent/css").Include(
                "~/Content/estilos_base.css",
                "~/Content/estilos-newsoft.css",
                "~/Content/jquery.datetimepicker.css"
                ));

            bundles.Add(new StyleBundle("~/AffContent_FinalUser/css").Include(
                "~/Content/estilos.css",
                "~/Content/estilos-newsoft.css",
                "~/Content/jquery.datetimepicker.css"
                ));

            bundles.Add(new StyleBundle("~/AffContent/cssadmin").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Plugins/bootstrap-datepicker/bootstrap-datepicker.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/estilos.css",
                      "~/Content/estilos-newsoft.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/cssadmin").Include(
                      "~/Content/sandstone.bootstrap.css",
                      "~/Content/font-awesome/css/font-awesome.css",
                      "~/Content/summernote.css",
                      "~/Content/site.css"
                        ));

        }
    }
}
