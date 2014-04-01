using System.Web;
using System.Web.Optimization;

namespace CommonP
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                          "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
                   "~/Scripts/underscore-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
               "~/Scripts/jquery.unobtrusive*",
               "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerycontrol").Include(
               "~/Scripts/formcontrol/control-*"
               ));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                "~/Scripts/base/base-*"
            ));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/site.css",
                         "~/Content/form.css",
                         "~/Content/button.css",
                         "~/Content/icon.css"));


            bundles.Add(new StyleBundle("~/Content/themes/icon").Include("~/Content/themes/icon.css"));

            bundles.Add(new StyleBundle("~/Content/themes/default/all").Include("~/Content/themes/default/easyui.css"));

            bundles.Add(new StyleBundle("~/Content/themes/gray/all").Include("~/Content/themes/gray/easyui.css"));

            bundles.Add(new StyleBundle("~/Content/themes/bootstrap/all").Include("~/Content/themes/bootstrap/easyui.css"));



            bundles.IgnoreList.Clear();
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
        }
    }
}