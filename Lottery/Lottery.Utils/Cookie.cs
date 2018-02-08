using System;
using System.Collections.Specialized;
using System.Web;

namespace Lottery.Utils
{
	public static class Cookie
	{
		public static void SetObj(string strCookieName, string strValue)
		{
			Cookie.SetObj(strCookieName, 1, strValue, "", "/");
		}

		public static void SetObj(string strCookieName, int iExpires, string strValue)
		{
			Cookie.SetObj(strCookieName, iExpires, strValue, "", "/");
		}

		public static void SetObj(string strCookieName, int iExpires, string strValue, string strDomains)
		{
			Cookie.SetObj(strCookieName, iExpires, strValue, strDomains, "/");
		}

		public static void SetObj(string strCookieName, int iExpires, string strValue, string strDomains, string strPath)
		{
			string text = Cookie.SelectDomain(strDomains);
			HttpCookie httpCookie = new HttpCookie(strCookieName.Trim());
			httpCookie.Value = HttpUtility.UrlEncode(strValue.Trim());
			if (text.Length > 0)
			{
				httpCookie.Domain = text;
			}
			if (iExpires > 0)
			{
				if (iExpires == 1)
				{
					httpCookie.Expires = DateTime.MaxValue;
				}
				else
				{
					httpCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
				}
			}
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue)
		{
			Cookie.SetObj(strCookieName, iExpires, KeyValue, "", "/");
		}

		public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomains)
		{
			Cookie.SetObj(strCookieName, iExpires, KeyValue, strDomains, "/");
		}

		public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomains, string strPath)
		{
			string text = Cookie.SelectDomain(strDomains);
			HttpCookie httpCookie = new HttpCookie(strCookieName.Trim());
			string[] allKeys = KeyValue.AllKeys;
			for (int i = 0; i < allKeys.Length; i++)
			{
				string text2 = allKeys[i];
				httpCookie[text2] = HttpUtility.UrlEncode(KeyValue[text2].Trim());
			}
			if (text.Length > 0)
			{
				httpCookie.Domain = text;
			}
			httpCookie.Path = strPath.Trim();
			if (iExpires > 0)
			{
				if (iExpires == 1)
				{
					httpCookie.Expires = DateTime.MaxValue;
				}
				else
				{
					httpCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
				}
			}
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public static string GetValue(string strCookieName)
		{
			if (HttpContext.Current.Request.Cookies[strCookieName] == null)
			{
				return null;
			}
			string value = HttpContext.Current.Request.Cookies[strCookieName].Value;
			return HttpUtility.UrlDecode(value);
		}

		public static string GetValue(string strCookieName, string strKeyName)
		{
			if (HttpContext.Current.Request.Cookies[strCookieName] == null)
			{
				return null;
			}
			string value = HttpContext.Current.Request.Cookies[strCookieName].Value;
			string value2 = strKeyName + "=";
			if (!value.Contains(value2))
			{
				return null;
			}
			string str = HttpContext.Current.Request.Cookies[strCookieName][strKeyName];
			return HttpUtility.UrlDecode(str);
		}

		public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires)
		{
			return Cookie.Edit(strCookieName, strKeyName, KeyValue, iExpires, "", "/");
		}

		public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strPath)
		{
			return Cookie.Edit(strCookieName, strKeyName, KeyValue, iExpires, "", strPath);
		}

		public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strDomains, string strPath)
		{
			if (HttpContext.Current.Request.Cookies[strCookieName] == null)
			{
				return null;
			}
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strCookieName];
			httpCookie[strKeyName] = HttpUtility.UrlEncode(KeyValue.Trim());
			if (iExpires > 0)
			{
				if (iExpires == 1)
				{
					httpCookie.Expires = DateTime.MaxValue;
				}
				else
				{
					httpCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
				}
			}
			HttpContext.Current.Response.Cookies.Add(httpCookie);
			return "success";
		}

		public static void Del(string strCookieName)
		{
			Cookie.Del(strCookieName, "", "/");
		}

		public static void Del(string strCookieName, string strDomains)
		{
			Cookie.Del(strCookieName, strDomains, "/");
		}

		public static void Del(string strCookieName, string strDomains, string strPath)
		{
			string text = Cookie.SelectDomain(strDomains);
			HttpCookie httpCookie = new HttpCookie(strCookieName.Trim());
			if (text.Length > 0)
			{
				httpCookie.Domain = text;
			}
			httpCookie.Path = strPath.Trim();
			httpCookie.Expires = DateTime.Now.AddYears(-1);
			HttpContext.Current.Response.Cookies.Add(httpCookie);
		}

		public static string DelKey(string strCookieName, string strKeyName, int iExpires)
		{
			if (HttpContext.Current.Request.Cookies[strCookieName] == null)
			{
				return null;
			}
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strCookieName];
			httpCookie.Values.Remove(strKeyName);
			if (iExpires > 0)
			{
				if (iExpires == 1)
				{
					httpCookie.Expires = DateTime.MaxValue;
				}
				else
				{
					httpCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
				}
			}
			HttpContext.Current.Response.Cookies.Add(httpCookie);
			return "success";
		}

		private static string SelectDomain(string strDomains)
		{
			bool flag = false;
			if (strDomains.Trim().Length == 0)
			{
				return "";
			}
			string text = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
			if (!text.Contains("."))
			{
				flag = true;
			}
			string result = "www.abc.com";
			string[] array = strDomains.Split(new char[]
			{
				';'
			});
			int i = 0;
			while (i < array.Length)
			{
				if (text.Contains(array[i].Trim()))
				{
					if (flag)
					{
						result = "";
						break;
					}
					result = array[i].Trim();
					break;
				}
				else
				{
					i++;
				}
			}
			return result;
		}
	}
}
