using System;
using System.Web.Security;

namespace Lottery.Utils
{
	public static class MD5
	{
		public static string Last64(string s)
		{
			if (s.Length != 32)
			{
				return "";
			}
			string s2 = s.Substring(0, 16);
			string s3 = s.Substring(16, 16);
			return MD5.Lower32(s2) + MD5.Lower32(s3);
		}

		public static string Upper32(string s)
		{
			s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
			return s.ToUpper();
		}

		public static string Lower32(string s)
		{
			s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
			return s.ToLower();
		}

		public static string Upper16(string s)
		{
			s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
			return s.ToUpper().Substring(8, 16);
		}

		public static string Lower16(string s)
		{
			s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
			return s.ToLower().Substring(8, 16);
		}
	}
}
