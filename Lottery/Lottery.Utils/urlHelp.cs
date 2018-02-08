using System;
using System.Web;

namespace Lottery.Utils
{
	public static class urlHelp
	{
		public static string GetUrlPrefix
		{
			get
			{
				HttpRequest arg_0A_0 = HttpContext.Current.Request;
				string str = HttpContext.Current.Request.ServerVariables["Url"];
				if (HttpContext.Current.Request.QueryString.Count == 0)
				{
					return str + "?page=";
				}
				if (HttpContext.Current.Request.ServerVariables["Query_String"].StartsWith("page=", StringComparison.OrdinalIgnoreCase))
				{
					return str + "?page=";
				}
				string[] array = HttpContext.Current.Request.ServerVariables["Query_String"].Split(new string[]
				{
					"page="
				}, StringSplitOptions.None);
				if (array.Length == 1)
				{
					return str + "?" + array[0] + "&page=";
				}
				return str + "?" + array[0] + "page=";
			}
		}
	}
}
