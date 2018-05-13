using System.Web;
using System.Web.Optimization;

namespace CoffeeShop
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.IgnoreList.Clear();

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // JQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerynum").Include(
                        "~/Scripts/jquery.number.min.js"));

            // Bootstrap 
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetimepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/canvasjs").Include(
                        "~/Scripts/canvasjs.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-datetimepicker").Include(
                "~/Content/datepicker.css"));


            // Site admin
            bundles.Add(new StyleBundle("~/Content/admin").Include(
                "~/Content/admin.css",
                "~/Content/Plugins/MetisMenu/metisMenu.css",
                "~/Content/fonts/css/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                        "~/Scripts/admin.js",
                        "~/Scripts/Plugins/MetisMenu/metisMenu.min.js"));

            // Site
            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                       "~/Scripts/jquery.nivo.slider.js",
                       "~/Scripts/menu.js"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                "~/Content/site.css",
                "~/Content/slider.css"));
        }
    }
}