using System;
using System.Security.Cryptography;
using System.Text;

namespace THPayHelper
{
	public class MD5Encoder
	{
		public static string encode(string str, string charset)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.GetEncoding(charset).GetBytes(str);
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("x2");
			}
			return text;
		}
	}
}
