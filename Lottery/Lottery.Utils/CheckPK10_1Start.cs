using System;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public static class CheckPK10_1Start
	{
		public static int PK10_1FS(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			Regex regex = new Regex("^[_0-9]+$");
			if (!regex.IsMatch(CheckNumber))
			{
				return 0;
			}
			string[] array = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					return 0;
				}
				if (CheckPK10_1Start.SubstringCount(CheckNumber, array[i]) > 1)
				{
					return 0;
				}
				if (array[i].Length != 2)
				{
					return 0;
				}
			}
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			for (int j = 0; j < array.Length; j++)
			{
				string value = array[j];
				if (array2[0].IndexOf(value) != -1)
				{
					num++;
				}
			}
			return num;
		}

		private static int SubstringCount(string str, string substring)
		{
			if (str.Contains(substring))
			{
				string text = str.Replace(substring, "");
				return (str.Length - text.Length) / substring.Length;
			}
			return 0;
		}
	}
}
