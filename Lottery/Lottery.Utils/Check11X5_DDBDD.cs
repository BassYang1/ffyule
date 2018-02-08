using System;

namespace Lottery.Utils
{
	public static class Check11X5_DDBDD
	{
		public static int P_BDD(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i].Length != 2 || Convert.ToInt32(array2[i]) > 11 || Convert.ToInt32(array2[i]) < 1)
				{
					return 0;
				}
			}
			for (int j = 0; j < array2.Length; j++)
			{
				if (array[0].IndexOf(array2[j]) != -1 || array[1].IndexOf(array2[j]) != -1 || array[2].IndexOf(array2[j]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_DD(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array2.Length != 3)
			{
				return 0;
			}
			for (int i = 0; i < array2.Length; i++)
			{
				if (!string.IsNullOrEmpty(array2[i]))
				{
					string[] array3 = array2[i].Split(new char[]
					{
						'_'
					});
					for (int j = 0; j < array3.Length; j++)
					{
						if (array3[j].Length != 2 || Convert.ToInt32(array3[j]) > 11)
						{
							return 0;
						}
					}
				}
			}
			if (array2.Length == 3)
			{
				if (array2[0].Length > 2)
				{
					if (array2[0].Contains("_") && array2[0].IndexOf(array[0]) != -1)
					{
						num++;
					}
				}
				else if (array2[0].IndexOf(array[0]) != -1)
				{
					num++;
				}
				if (array2[1].Length > 2)
				{
					if (array2[1].Contains("_") && array2[1].IndexOf(array[1]) != -1)
					{
						num++;
					}
				}
				else if (array2[1].IndexOf(array[1]) != -1)
				{
					num++;
				}
				if (array2[2].Length > 2)
				{
					if (array2[2].Contains("_") && array2[2].IndexOf(array[2]) != -1)
					{
						num++;
					}
				}
				else if (array2[2].IndexOf(array[2]) != -1)
				{
					num++;
				}
			}
			return num;
		}
	}
}
