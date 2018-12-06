using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Example.Mvc.Models;
using MVC.Extensions.Attributes;
using MVC.Extensions.Extensions;

namespace Example.Mvc.Controllers
{
	public class HomeController : Controller
	{
		[Powers]
		public async Task<IActionResult> Index()
		{
			ViewBag.UserName = this.GetAuthorizedUserData<dynamic>(false, false).UserName;
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
