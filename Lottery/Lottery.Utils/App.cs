using System;
using System.Web;

namespace Lottery.Utils
{
	public static class App
	{
		public static string Url
		{
			get
			{
				if (HttpContext.Current.Request.Url.Port == 80)
				{
					return "http://" + HttpContext.Current.Request.Url.Host;
				}
				return string.Concat(new object[]
				{
					"http://",
					HttpContext.Current.Request.Url.Host,
					":",
					HttpContext.Current.Request.Url.Port
				});
			}
		}

		public static string Path
		{
			get
			{
				string text = HttpContext.Current.Request.ApplicationPath;
				if (text != "/")
				{
					text += "/";
				}
				return text;
			}
		}
	}
}
