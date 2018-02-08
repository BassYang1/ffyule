using System;

namespace Lottery.Utils
{
	public static class CheckPK10_DDBDD
	{
		public static int P_DD1_5(string LotteryNumber, string CheckNumber)
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
			if (array2.Length != 5)
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
						if (array3[j].Length != 2 || Convert.ToInt32(array3[j]) > 10)
						{
							return 0;
						}
					}
				}
			}
			if (array2.Length == 5)
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
				if (array2[3].Length > 2)
				{
					if (array2[3].Contains("_") && array2[3].IndexOf(array[3]) != -1)
					{
						num++;
					}
				}
				else if (array2[3].IndexOf(array[3]) != -1)
				{
					num++;
				}
				if (array2[4].Length > 2)
				{
					if (array2[4].Contains("_") && array2[4].IndexOf(array[4]) != -1)
					{
						num++;
					}
				}
				else if (array2[4].IndexOf(array[4]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_DD6_10(string LotteryNumber, string CheckNumber)
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
			if (array2.Length != 5)
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
						if (array3[j].Length != 2 || Convert.ToInt32(array3[j]) > 10)
						{
							return 0;
						}
					}
				}
			}
			if (array2.Length == 5)
			{
				if (array2[0].Length > 2)
				{
					if (array2[0].Contains("_") && array2[0].IndexOf(array[5]) != -1)
					{
						num++;
					}
				}
				else if (array2[0].IndexOf(array[5]) != -1)
				{
					num++;
				}
				if (array2[1].Length > 2)
				{
					if (array2[1].Contains("_") && array2[1].IndexOf(array[6]) != -1)
					{
						num++;
					}
				}
				else if (array2[1].IndexOf(array[6]) != -1)
				{
					num++;
				}
				if (array2[2].Length > 2)
				{
					if (array2[2].Contains("_") && array2[2].IndexOf(array[7]) != -1)
					{
						num++;
					}
				}
				else if (array2[2].IndexOf(array[7]) != -1)
				{
					num++;
				}
				if (array2[3].Length > 2)
				{
					if (array2[3].Contains("_") && array2[3].IndexOf(array[8]) != -1)
					{
						num++;
					}
				}
				else if (array2[3].IndexOf(array[8]) != -1)
				{
					num++;
				}
				if (array2[4].Length > 2)
				{
					if (array2[4].Contains("_") && array2[4].IndexOf(array[9]) != -1)
					{
						num++;
					}
				}
				else if (array2[4].IndexOf(array[9]) != -1)
				{
					num++;
				}
			}
			return num;
		}
	}
}
