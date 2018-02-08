using System;

namespace Lottery.Utils
{
	public static class ValidateCode
	{
		public static bool CheckValidateCode(string _code, ref string _realcode)
		{
			_realcode = Session.Get("ValidateCode");
			return _code != null && _code.Length != 0 && _realcode != null && _realcode.Length != 0 && _realcode.ToLower() == _code.ToLower();
		}

		public static string GetValidateCode(int _length, bool _init)
		{
			if (_init)
			{
				ValidateCode.CreateValidateCode(_length, true);
			}
			return Session.Get("ValidateCode");
		}

		public static void CreateValidateCode(int _length, bool _cover)
		{
			if (_cover)
			{
				ValidateCode.SaveCookie(_length);
				return;
			}
			if (Session.Get("ValidateCode") == null)
			{
				ValidateCode.SaveCookie(_length);
			}
		}

		public static void SaveCookie(int _length)
		{
			char[] array = "0123456789".ToCharArray();
			Random random = new Random();
			string text = string.Empty;
			for (int i = 0; i < _length; i++)
			{
				text += array[random.Next(0, array.Length)].ToString();
			}
			if (Session.Get("ValidateCode") != null)
			{
				Session.Del("ValidateCode");
			}
			Session.Add("ValidateCode", text);
		}
	}
}
