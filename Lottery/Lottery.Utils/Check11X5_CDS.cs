using System;

namespace Lottery.Utils
{
	public static class Check11X5_CDS
	{
		public static int P_CDS(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			int[] array = Check11X5_CDS.Sort(LotteryNumber.Split(new char[]
			{
				','
			}));
			string value = string.Concat(new object[]
			{
				array[0],
				"单",
				array[1],
				"双"
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i].IndexOf(value) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int[] Sort(string[] arr)
		{
			int num = 0;
			int num2 = 0;
			int[] array = new int[2];
			for (int i = 0; i < arr.Length; i++)
			{
				if (Convert.ToInt32(arr[i]) % 2 == 0)
				{
					num2++;
				}
				else
				{
					num++;
				}
			}
			array[0] = num;
			array[1] = num2;
			return array;
		}
	}
}
