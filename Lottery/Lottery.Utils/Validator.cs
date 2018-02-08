using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public static class Validator
	{
		public static bool IsCommonDomain(string _value)
		{
			return Validator.QuickValidate("^(www.)?(\\w+\\.){1,3}(org|org.cn|gov.cn|com|cn|net|cc)$", _value.ToLower());
		}

		public static bool IsStringDate(string _value)
		{
			try
			{
				DateTime.Parse(_value);
			}
			catch (FormatException)
			{
				return false;
			}
			return true;
		}

		public static bool IsNumeric(string _value)
		{
			return Validator.QuickValidate("^[-]?[1-9]*[0-9]*$", _value);
		}

		public static bool IsLetterOrNumber(string _value)
		{
			return Validator.QuickValidate("^[a-zA-Z0-9_]*$", _value);
		}

		public static bool IsNumber(string _value)
		{
			return Validator.QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9]+)?$", _value);
		}

		public static bool QuickValidate(string _express, string _value)
		{
			Regex regex = new Regex(_express);
			return _value != null && _value.Length != 0 && regex.IsMatch(_value);
		}

		public static bool IsEmail(string _value)
		{
			Regex regex = new Regex("^\\w+([-+.]\\w+)*@(\\w+([-.]\\w+)*\\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase);
			return regex.Match(_value).Success;
		}

		public static bool IsZIPCode(string _value)
		{
			return Validator.QuickValidate("^([0-9]{6})$", _value);
		}

		public static bool IsIDCard(string _value)
		{
			if (_value.Length != 15 && _value.Length != 18)
			{
				return false;
			}
			Regex regex;
			string[] array;
			bool result;
			if (_value.Length == 15)
			{
				regex = new Regex("^(\\d{6})(\\d{2})(\\d{2})(\\d{2})(\\d{3})$");
				if (!regex.Match(_value).Success)
				{
					return false;
				}
				array = regex.Split(_value);
				try
				{
					new DateTime(int.Parse("19" + array[2]), int.Parse(array[3]), int.Parse(array[4]));
					result = true;
					return result;
				}
				catch
				{
					result = false;
					return result;
				}
			}
			regex = new Regex("^(\\d{6})(\\d{4})(\\d{2})(\\d{2})(\\d{3})([0-9Xx])$");
			if (!regex.Match(_value).Success)
			{
				return false;
			}
			array = regex.Split(_value);
			try
			{
				new DateTime(int.Parse(array[2]), int.Parse(array[3]), int.Parse(array[4]));
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool IsInt(string _value)
		{
			Regex regex = new Regex("^(-){0,1}\\d+$");
			return regex.Match(_value).Success && long.Parse(_value) <= 2147483647L && long.Parse(_value) >= -2147483648L;
		}

		public static bool IsLengthStr(string _value, int _begin, int _end)
		{
			int length = _value.Length;
			return length >= _begin || length <= _end;
		}

		public static bool IsChinese(string _value)
		{
			Regex regex = new Regex("^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$", RegexOptions.IgnoreCase);
			return regex.Match(_value).Success;
		}

		public static bool IsMobileNum(string _value)
		{
			Regex regex = new Regex("^(13|15)\\d{9}$", RegexOptions.IgnoreCase);
			return regex.Match(_value).Success;
		}

		public static bool IsPhoneNum(string _value)
		{
			Regex regex = new Regex("^(86)?(-)?(0\\d{2,3})?(-)?(\\d{7,8})(-)?(\\d{3,5})?$", RegexOptions.IgnoreCase);
			return regex.Match(_value).Success;
		}

		public static bool IsUrl(string _value)
		{
			Regex regex = new Regex("(http://)?([\\w-]+\\.)*[\\w-]+(/[\\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
			return regex.Match(_value).Success;
		}

		public static bool IsIP(string _value)
		{
			Regex regex = new Regex("^(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1}))$", RegexOptions.IgnoreCase);
			return regex.Match(_value).Success;
		}

		public static bool IsWordAndNum(string _value)
		{
			Regex regex = new Regex("[a-zA-Z0-9]?");
			return regex.Match(_value).Success;
		}

		public static DateTime StrToDate(string _value, DateTime _defaultValue)
		{
			if (Validator.IsStringDate(_value))
			{
				return Convert.ToDateTime(_value);
			}
			return _defaultValue;
		}

		public static bool CompareDate(string today, string writeDate, int n)
		{
			DateTime t = Convert.ToDateTime(today);
			DateTime t2 = Convert.ToDateTime(writeDate).AddDays((double)n);
			return !(t >= t2);
		}

		public static bool ValidDate(string myDate)
		{
			return !Validator.IsStringDate(myDate) || Validator.CompareDate(myDate, DateTime.Now.ToShortDateString(), 0);
		}

		public static int StrToInt(string _value, int _defaultValue)
		{
			if (Validator.IsNumeric(_value))
			{
				return int.Parse(_value);
			}
			return _defaultValue;
		}

		public static bool IsFreeSite(string _defaultpage, string _webname)
		{
			string text = HttpHelp.Get_Http(_defaultpage, 10000, Encoding.UTF8);
			string text2 = text.ToLower().Replace("\"", "").Replace("'", "");
			return text.Contains(_webname) && (text2.Contains("href=http://www.Lottery.net") || text2.Contains("href=http://Lottery.net"));
		}
	}
}
