using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Extensions.Extensions
{
	public static class MvcExtensionMethods
	{
		/// <summary>
		/// 载入分页控件（支持bootstrap样式）
		/// </summary>
		/// <param name="html"></param>
		/// <param name="actionName"></param>
		/// <param name="controllerName"></param>
		/// <param name="pagesize"></param>
		/// <param name="count"></param>
		/// <param name="showMaxCount"></param>
		/// <param name="keepParameters"></param>
		/// <param name="formMethod"></param>
		/// <returns></returns>
		public static IHtmlContent Paging(this IHtmlHelper html, string actionName, string controllerName, int pagesize = 20, int count = 0, int showMaxCount = 10, Dictionary<string, object> keepParameters = null, FormMethod formMethod = FormMethod.Get)
		{
			return html.Partial("Pager", new MvcPager
			{
				ActionName = actionName,
				ControllerName = controllerName,
				PageSize = pagesize,
				Count = count,
				KeepParameters = keepParameters,
				FormMethod = formMethod,
				ShowMaxCount = showMaxCount
			});
		}

		public static IHtmlContent Paging(this IHtmlHelper html, MvcPager pagerOptions)
		{
			return html.Partial("Pager", pagerOptions);
		}


		public static IHtmlContent GetUrl(this IHtmlHelper html, string actionName, string controllerName, IEnumerable<KeyValuePair<string, object>> parameters = null)
		{
			var para = parameters?.Aggregate("", (current, key) => current + ("&" + key.Key + "=" + key.Value))?.TrimStart('&');
			return new HtmlString($"/{controllerName}/{actionName}" + (string.IsNullOrWhiteSpace(para) ? "" : "?" + para));
		}
	}
	public class MvcPager
	{
		public string ActionName { get; set; }
		public string ControllerName { get; set; }
		public int PageSize { get; set; } = 20;
		public int Count { get; set; }

		public int ShowMaxCount { get; set; } = 10;
		public Dictionary<string, object> KeepParameters { get; set; }

		public FormMethod FormMethod { get; set; } = FormMethod.Get;

		public string PreButtonText { get; set; } = "上一页";

		public string NextButtonText { get; set; } = "下一页";
	}
}
