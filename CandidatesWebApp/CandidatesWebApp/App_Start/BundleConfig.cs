using System.Web;
using System.Web.Optimization;

namespace CandidatesWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.5.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-input-mask-phone-number").Include(
                        "~/Scripts/jquery-input-mask-phone-number.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                      "~/Scripts/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/datatables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/candidates-web-app").Include(
                      "~/Scripts/candidates-web-app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/candidates-web-app.css",
                      "~/Content/toastr.min.css",
                      "~/Content/datatables.min.css"));
        }
    }
}
