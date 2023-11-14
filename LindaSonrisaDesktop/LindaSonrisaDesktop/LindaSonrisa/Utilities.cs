using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LindaSonrisa
{
    public static class Utilities
    {
        public static string IsControllerActive(this IHtmlHelper htmlHelper, string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController);

            return returnActive ? "active" : "";
        }

        public static string IsActionActive(this IHtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && (action == routeAction));

            return returnActive ? "active" : "";
        }

        public static string IsActionActive(this IHtmlHelper htmlHelper, string controller, string action, string model)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeModel = routeData.Values["action"].ToString().Replace("Edit", "");
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && (action == routeAction || routeModel == model));

            return returnActive ? "active" : "";
        }
    }
}
