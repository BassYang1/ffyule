using System;

namespace Lottery.Utils
{
	public static class Check11X5_CZW
	{
		public static int P_CZW(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = Check11X5_CZW.Sort(LotteryNumber.Split(new char[]
			{
				','
			}));
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i].IndexOf(string.Concat(int.Parse(array[2]))) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static string[] Sort(string[] arr)
		{
			for (int i = 0; i < arr.Length - 1; i++)
			{
				Check11X5_CZW.min = i;
				for (int j = i + 1; j < arr.Length; j++)
				{
					if (Convert.ToInt32(arr[j]) < Convert.ToInt32(arr[Check11X5_CZW.min]))
					{
						Check11X5_CZW.min = j;
					}
				}
				string text = arr[Check11X5_CZW.min];
				arr[Check11X5_CZW.min] = arr[i];
				arr[i] = text;
			}
			return arr;
		}

		private static int min;
	}
}
