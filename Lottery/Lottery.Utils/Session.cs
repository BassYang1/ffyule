using System;
using System.Web;

namespace Lottery.Utils
{
	public static class Session
	{
		public static void Add(string strSessionName, string strValue)
		{
			HttpContext.Current.Session[strSessionName] = strValue;
			HttpContext.Current.Session.Timeout = 20;
		}

		public static void Adds(string strSessionName, string[] strValues)
		{
			HttpContext.Current.Session[strSessionName] = strValues;
			HttpContext.Current.Session.Timeout = 20;
		}

		public static void Add(string strSessionName, string strValue, int iExpires)
		{
			HttpContext.Current.Session[strSessionName] = strValue;
			HttpContext.Current.Session.Timeout = iExpires;
		}

		public static void Adds(string strSessionName, string[] strValues, int iExpires)
		{
			HttpContext.Current.Session[strSessionName] = strValues;
			HttpContext.Current.Session.Timeout = iExpires;
		}

		public static string Get(string strSessionName)
		{
			if (HttpContext.Current.Session[strSessionName] == null)
			{
				return null;
			}
			return HttpContext.Current.Session[strSessionName].ToString();
		}

		public static string[] Gets(string strSessionName)
		{
			if (HttpContext.Current.Session[strSessionName] == null)
			{
				return null;
			}
			return (string[])HttpContext.Current.Session[strSessionName];
		}

		public static void Del(string strSessionName)
		{
			HttpContext.Current.Session[strSessionName] = null;
		}
	}
}
