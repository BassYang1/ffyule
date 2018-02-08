using System;
using System.Text;
using System.Web;

namespace THPayHelper
{
	public class URLUtils
	{
		public static string encode(string str, string charset)
		{
			string result;
			try
			{
				result = HttpUtility.UrlEncode(str, Encoding.GetEncoding(charset));
			}
			catch (Exception)
			{
				result = str;
			}
			return result;
		}

		public static string decode(string str, string charset)
		{
			string result;
			try
			{
				result = HttpUtility.UrlDecode(str, Encoding.GetEncoding(charset));
			}
			catch (Exception)
			{
				result = str;
			}
			return result;
		}

		public static void appendParam(StringBuilder sb, string name, string val)
		{
			URLUtils.appendParam(sb, name, val, true);
		}

		public static void appendParam(StringBuilder sb, string name, string val, string charset)
		{
			URLUtils.appendParam(sb, name, val, true, charset);
		}

		public static void appendParam(StringBuilder sb, string name, string val, bool and)
		{
			URLUtils.appendParam(sb, name, val, and, null);
		}

		public static void appendParam(StringBuilder sb, string name, string val, bool and, string charset)
		{
			if (and)
			{
				sb.Append("&");
			}
			else
			{
				sb.Append("?");
			}
			sb.Append(name);
			sb.Append("=");
			if (val == null)
			{
				val = "";
			}
			if (string.IsNullOrEmpty(charset))
			{
				sb.Append(val);
				return;
			}
			sb.Append(URLUtils.encode(val, charset));
		}
	}
}
