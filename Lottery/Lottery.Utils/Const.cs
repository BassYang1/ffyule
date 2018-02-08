using System;
using System.Web;

namespace Lottery.Utils
{
	public class Const
	{
		public static string RootUserId
		{
			get
			{
				return "1026";
			}
		}

		public static string ConnectionString
		{
			get
			{
				return "Data Source=.;Initial Catalog=Ticket;Persist Security Info=True;User ID=sa;Password=aA321321";
			}
		}

		public static string DatabaseType
		{
			get
			{
				return "1";
			}
		}

		public static string GetUserIp
		{
			get
			{
				bool flag = false;
				string text;
				if (HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
				{
					text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
				}
				else
				{
					text = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
				}
				if (text.Length > 15)
				{
					flag = true;
				}
				else
				{
					string[] array = text.Split(new char[]
					{
						'.'
					});
					if (array.Length == 4)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].Length > 3)
							{
								flag = true;
							}
						}
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					return "1.1.1.1";
				}
				return text;
			}
		}

		public static string FormatIp(string ipStr)
		{
			string[] array = ipStr.Split(new char[]
			{
				'.'
			});
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length < 3)
				{
					array[i] = Convert.ToString("000" + array[i]).Substring(Convert.ToString("000" + array[i]).Length - 3, 3);
				}
				text += array[i].ToString();
			}
			return text;
		}

		public static string GetRefererUrl
		{
			get
			{
				if (HttpContext.Current.Request.ServerVariables["Http_Referer"] == null)
				{
					return "";
				}
				return HttpContext.Current.Request.ServerVariables["Http_Referer"].ToString();
			}
		}

		public static string GetCurrentUrl
		{
			get
			{
				string text = HttpContext.Current.Request.ServerVariables["Url"];
				if (HttpContext.Current.Request.QueryString.Count == 0)
				{
					return text;
				}
				return text + "?" + HttpContext.Current.Request.ServerVariables["Query_String"];
			}
		}
	}
}
