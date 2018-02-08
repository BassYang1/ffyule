using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Lottery.Utils
{
	public class IPHelp
	{
		[DllImport("Iphlpapi.dll")]
		private static extern int SendARP(int dest, int host, ref long mac, ref int length);

		[DllImport("Ws2_32.dll")]
		private static extern int inet_addr(string ip);

		public static long IP2Long(IPAddress ip)
		{
			int num = 3;
			long num2 = 0L;
			byte[] addressBytes = ip.GetAddressBytes();
			for (int i = 0; i < addressBytes.Length; i++)
			{
				byte b = addressBytes[i];
				num2 += (long)((long)((ulong)b) << 8 * num--);
			}
			return num2;
		}

		public static IPAddress Long2IP(long l)
		{
			byte[] array = new byte[4];
			for (int i = 0; i < 4; i++)
			{
				array[3 - i] = (byte)(l >> 8 * i & 255L);
			}
			return new IPAddress(array);
		}

		public static string ClientIP
		{
			get
			{
				bool flag = false;
				string text;
				if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
				{
					text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
				}
				else
				{
					text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
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

		public static string GetMac()
		{
			string result = "";
			try
			{
				string arg_15_0 = HttpContext.Current.Request.UserHostAddress;
				string ip = HttpContext.Current.Request.UserHostAddress.ToString().Trim();
				int dest = IPHelp.inet_addr(ip);
				IPHelp.inet_addr(ip);
				long num = 0L;
				int num2 = 6;
				IPHelp.SendARP(dest, 0, ref num, ref num2);
				string text = num.ToString("X");
				if (text == "0")
				{
					return "错误";
				}
				while (text.Length < 12)
				{
					text = text.Insert(0, "0");
				}
				string text2 = "";
				for (int i = 0; i < 11; i++)
				{
					if (i % 2 == 0)
					{
						if (i == 10)
						{
							text2 = text2.Insert(0, text.Substring(i, 2));
						}
						else
						{
							text2 = "-" + text2.Insert(0, text.Substring(i, 2));
						}
					}
				}
				result = text2;
			}
			catch (Exception ex)
			{
				result = ex.ToString();
			}
			return result;
		}

		public static string GetBrowser()
		{
			HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
			return browser.Browser + browser.Version;
		}

		public static string GetOSVersion()
		{
			string text = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
			string result = "未知";
			if (text.Contains("NT 6.1"))
			{
				result = "Windows 7";
			}
			else if (text.Contains("NT 6.0"))
			{
				result = "Windows Vista/Server 2008";
			}
			else if (text.Contains("NT 5.2"))
			{
				result = "Windows Server 2003";
			}
			else if (text.Contains("NT 5.1"))
			{
				result = "Windows XP";
			}
			else if (text.Contains("NT 5"))
			{
				result = "Windows 2000";
			}
			else if (text.Contains("NT 4"))
			{
				result = "Windows NT4";
			}
			else if (text.Contains("Me"))
			{
				result = "Windows Me";
			}
			else if (text.Contains("98"))
			{
				result = "Windows 98";
			}
			else if (text.Contains("95"))
			{
				result = "Windows 95";
			}
			else if (text.Contains("Mac"))
			{
				result = "Mac";
			}
			else if (text.Contains("Unix"))
			{
				result = "UNIX";
			}
			else if (text.Contains("Linux"))
			{
				result = "Linux";
			}
			else if (text.Contains("SunOS"))
			{
				result = "SunOS";
			}
			return result;
		}

		public static string GetIP()
		{
			string text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if (string.IsNullOrEmpty(text))
			{
				text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			}
			if (string.IsNullOrEmpty(text))
			{
				text = HttpContext.Current.Request.UserHostAddress;
			}
			if (string.IsNullOrEmpty(text))
			{
				return "0.0.0.0";
			}
			return text;
		}

		public static string GetIPAddress
		{
			get
			{
				string text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
				if (!string.IsNullOrEmpty(text))
				{
					if (text.IndexOf(".") == -1)
					{
						text = null;
					}
					else if (text.IndexOf(",") != -1)
					{
						text = text.Replace("  ", "").Replace("'", "");
						string[] array = text.Split(",;".ToCharArray());
						for (int i = 0; i < array.Length; i++)
						{
							if (IPHelp.IsIPAddress(array[i]) && array[i].Substring(0, 3) != "10." && array[i].Substring(0, 7) != "192.168" && array[i].Substring(0, 7) != "172.16.")
							{
								return array[i];
							}
						}
					}
					else
					{
						if (IPHelp.IsIPAddress(text))
						{
							return text;
						}
						text = null;
					}
				}
				if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null || !(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != string.Empty))
				{
					string arg_141_0 = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
				}
				else
				{
					string arg_15D_0 = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
				}
				if (string.IsNullOrEmpty(text))
				{
					text = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
				}
				if (string.IsNullOrEmpty(text))
				{
					text = HttpContext.Current.Request.UserHostAddress;
				}
				return text;
			}
		}

		public static bool IsIPAddress(string str1)
		{
			if (string.IsNullOrEmpty(str1) || str1.Length < 7 || str1.Length > 15)
			{
				return false;
			}
			Regex regex = new Regex("^d{1,3}[.]d{1,3}[.]d{1,3}[.]d{1,3}$", RegexOptions.IgnoreCase);
			return regex.IsMatch(str1);
		}

		public static string GetNetIP()
		{
			string text = "";
			try
			{
				WebRequest webRequest = WebRequest.Create("http://city.ip138.com/ip2city.asp");
				Stream responseStream = webRequest.GetResponse().GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("gb2312"));
				string text2 = streamReader.ReadToEnd();
				int num = text2.IndexOf("[") + 1;
				int num2 = text2.IndexOf("]", num);
				text = text2.Substring(num, num2 - num);
				streamReader.Close();
				responseStream.Close();
			}
			catch
			{
				if (Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length > 1)
				{
					text = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
				}
				if (string.IsNullOrEmpty(text))
				{
					return IPHelp.GetIP();
				}
			}
			return text;
		}

		public static string domain2ip(string str)
		{
			string result = "";
			try
			{
				IPHostEntry hostByName = Dns.GetHostEntry(str);
				IPAddress[] addressList = hostByName.AddressList;
				result = addressList[0].ToString();
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}
	}
}
