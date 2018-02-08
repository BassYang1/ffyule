using System;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public static class Check11X5_3Start
	{
		public static int P_3ZXFS(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			LotteryNumber = string.Concat(new string[]
			{
				array[0],
				",",
				array[1],
				",",
				array[2]
			});
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			Regex regex = new Regex("^[_0-9]+$");
			if (regex.IsMatch(array3[0]) && regex.IsMatch(array3[1]) && regex.IsMatch(array3[2]))
			{
				if (array3.Length == 3 && array3[0].IndexOf(array2[0]) != -1 && array3[1].IndexOf(array2[1]) != -1 && array3[2].IndexOf(array2[2]) != -1)
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

		public static int P_3ZXDS(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			LotteryNumber = array[0] + array[1] + array[2];
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

		public static int P_3ZXFS_Z(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			LotteryNumber = string.Concat(new string[]
			{
				array[0],
				",",
				array[1],
				",",
				array[2]
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				for (int j = 0; j < array2.Length; j++)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						if (i != j && j != k && k != i)
						{
							CheckNumber = array2[i] + array2[j] + array2[k];
							LotteryNumber = LotteryNumber.Replace(",", "");
							if (CheckNumber.Equals(LotteryNumber))
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		public static int P_3ZXDS_Z(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			LotteryNumber = string.Concat(new string[]
			{
				array[0],
				",",
				array[1],
				",",
				array[2]
			});
			array = LotteryNumber.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					for (int k = 0; k < array.Length; k++)
					{
						if (i != j && j != k && k != i)
						{
							LotteryNumber = array[i] + array[j] + array[k];
							string[] array2 = CheckNumber.Replace(" ", "").Split(new char[]
							{
								','
							});
							for (int l = 0; l < array2.Length; l++)
							{
								if (LotteryNumber == array2[l])
								{
									num++;
								}
							}
						}
					}
				}
			}
			return num;
		}
	}
}
