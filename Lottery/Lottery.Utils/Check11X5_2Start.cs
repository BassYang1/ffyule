using System;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public static class Check11X5_2Start
	{
		public static int P_2ZXFS(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			LotteryNumber = array[0] + "," + array[1];
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			Regex regex = new Regex("^[_0-9]+$");
			if (regex.IsMatch(array3[0]) && regex.IsMatch(array3[1]))
			{
				if (array3.Length >= 2 && array3[0].IndexOf(array2[0]) != -1 && array3[1].IndexOf(array2[1]) != -1)
				{
					num++;
				}
			}
			else
			{
				num = 0;
			}
			return num;
		}

		public static int P_2ZXDS(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			LotteryNumber = array[0] + array[1];
			string[] array2 = CheckNumber.Replace(" ", "").Split(new char[]
			{
				','
			});
			for (int i = 0; i < array2.Length; i++)
			{
				Regex regex = new Regex("^[_0-9]+$");
				if (!regex.IsMatch(array2[i]))
				{
					return 0;
				}
				if (LotteryNumber == array2[i])
				{
					num++;
				}
			}
			return num;
		}

		public static int P_2ZXFS_Z(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			LotteryNumber = array[0] + "," + array[1];
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				for (int j = 0; j < array2.Length; j++)
				{
					if (i != j)
					{
						CheckNumber = array2[i] + array2[j];
						LotteryNumber = LotteryNumber.Replace(",", "");
						if (CheckNumber.Equals(LotteryNumber))
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		public static int P_2ZXDS_Z(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			LotteryNumber = array[0] + "," + array[1];
			array = LotteryNumber.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (i != j)
					{
						LotteryNumber = array[i] + array[j];
						string[] array2 = CheckNumber.Replace(" ", "").Split(new char[]
						{
							','
						});
						for (int k = 0; k < array2.Length; k++)
						{
							if (LotteryNumber == array2[k])
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}
	}
}
