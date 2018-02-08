using System;

namespace Lottery.Utils
{
	public static class Check11X5_RXDS
	{
		public static int P11_RXDS_1(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (text.Length != 2)
				{
					return 0;
				}
				if (LotteryNumber.IndexOf(text) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P11_RXDS_2(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					' '
				});
				if (array2.Length != 2)
				{
					return 0;
				}
				for (int j = 0; j < array2.Length; j++)
				{
					if (Check11X5_RXDS.SubstringCount(array[i], array2[j]) > 1)
					{
						return 0;
					}
				}
				if (LotteryNumber.IndexOf(array2[0]) != -1 && LotteryNumber.IndexOf(array2[1]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P11_RXDS_3(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					' '
				});
				if (array2.Length != 3)
				{
					return 0;
				}
				for (int j = 0; j < array2.Length; j++)
				{
					if (Check11X5_RXDS.SubstringCount(array[i], array2[j]) > 1)
					{
						return 0;
					}
				}
				if (LotteryNumber.IndexOf(array2[0]) != -1 && LotteryNumber.IndexOf(array2[1]) != -1 && LotteryNumber.IndexOf(array2[2]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P11_RXDS_4(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = CheckNumber.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					' '
				});
				if (array2.Length != 4)
				{
					return 0;
				}
				for (int j = 0; j < array2.Length; j++)
				{
					if (Check11X5_RXDS.SubstringCount(array[i], array2[j]) > 1)
					{
						return 0;
					}
				}
				if (LotteryNumber.IndexOf(array2[0]) != -1 && LotteryNumber.IndexOf(array2[1]) != -1 && LotteryNumber.IndexOf(array2[2]) != -1 && LotteryNumber.IndexOf(array2[3]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P11_RXDS_5(string LotteryNumber, string CheckNumber)
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
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					' '
				});
				if (array3.Length != 5)
				{
					return 0;
				}
				for (int j = 0; j < array3.Length; j++)
				{
					if (Check11X5_RXDS.SubstringCount(array2[i], array3[j]) > 1)
					{
						return 0;
					}
				}
				if (array2[i].IndexOf(array[0]) != -1 && array2[i].IndexOf(array[1]) != -1 && array2[i].IndexOf(array[2]) != -1 && array2[i].IndexOf(array[3]) != -1 && array2[i].IndexOf(array[4]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P11_RXDS_6(string LotteryNumber, string CheckNumber)
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
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					' '
				});
				if (array3.Length != 6)
				{
					return 0;
				}
				for (int j = 0; j < array3.Length; j++)
				{
					if (Check11X5_RXDS.SubstringCount(array2[i], array3[j]) > 1)
					{
						return 0;
					}
				}
				string text = array2[i];
				if (text.IndexOf(array[0]) != -1 && text.IndexOf(array[1]) != -1 && text.IndexOf(array[2]) != -1 && text.IndexOf(array[3]) != -1 && text.IndexOf(array[4]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P11_RXDS_7(string LotteryNumber, string CheckNumber)
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
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					' '
				});
				if (array3.Length != 7)
				{
					return 0;
				}
				for (int j = 0; j < array3.Length; j++)
				{
					if (Check11X5_RXDS.SubstringCount(array2[i], array3[j]) > 1)
					{
						return 0;
					}
				}
				string text = array2[i];
				if (text.IndexOf(array[0]) != -1 && text.IndexOf(array[1]) != -1 && text.IndexOf(array[2]) != -1 && text.IndexOf(array[3]) != -1 && text.IndexOf(array[4]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P11_RXDS_8(string LotteryNumber, string CheckNumber)
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
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					' '
				});
				if (array3.Length != 8)
				{
					return 0;
				}
				for (int j = 0; j < array3.Length; j++)
				{
					if (Check11X5_RXDS.SubstringCount(array2[i], array3[j]) > 1)
					{
						return 0;
					}
				}
				string text = array2[i];
				if (text.IndexOf(array[0]) != -1 && text.IndexOf(array[1]) != -1 && text.IndexOf(array[2]) != -1 && text.IndexOf(array[3]) != -1 && text.IndexOf(array[4]) != -1)
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
