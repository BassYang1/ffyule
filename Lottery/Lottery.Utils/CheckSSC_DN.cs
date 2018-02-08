using System;
using System.Collections;

namespace Lottery.Utils
{
	public static class CheckSSC_DN
	{
		public static int P_Wj(string LotteryNumber, int flag)
		{
			int result = 0;
			int num = CheckSSC_DN.CheckNNumInt(LotteryNumber);
			int num2 = CheckSSC_DN.CheckNNumInt(CheckSSC_DN.AddDnNum(LotteryNumber, flag));
			if (num2 > num)
			{
				if (num2 == 50)
				{
					result = 6;
				}
				else if (num2 == 40)
				{
					result = 5;
				}
				else if (num2 == 30)
				{
					result = 4;
				}
				else if (num2 >= 8 && num2 <= 9)
				{
					result = 3;
				}
				else if (num2 >= 1 && num2 <= 7)
				{
					result = 2;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		public static int CheckNNumInt(string LotteryNumber)
		{
			ArrayList arrayList = new ArrayList();
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = "0,1,2@0,1,3@0,1,4@0,2,3@0,2,4@0,3,4@1,2,3@1,2,4@1,3,4@2,3,4".Split(new char[]
			{
				'@'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					','
				});
				int j = 0;
				while (j < array.Length)
				{
					arrayList.Add(int.Parse(array[0]));
					arrayList.Add(int.Parse(array[1]));
					arrayList.Add(int.Parse(array[2]));
					arrayList.Add(int.Parse(array[3]));
					arrayList.Add(int.Parse(array[4]));
					int num = int.Parse(array[0]) + int.Parse(array[1]) + int.Parse(array[2]) + int.Parse(array[3]) + int.Parse(array[4]);
					if (num <= 10)
					{
						return 50;
					}
					if (int.Parse(array[int.Parse(array3[0])]) + int.Parse(array[int.Parse(array3[1])]) + int.Parse(array[int.Parse(array3[2])]) == 10)
					{
						arrayList.RemoveAt(int.Parse(array3[2]));
						arrayList.RemoveAt(int.Parse(array3[1]));
						arrayList.RemoveAt(int.Parse(array3[0]));
						arrayList.Sort();
						if ((num - 10) % 10 == 0)
						{
							return 30;
						}
						if (arrayList[0] == arrayList[1])
						{
							return 40;
						}
						return (num - 10) % 10;
					}
					else if (int.Parse(array[int.Parse(array3[0])]) + int.Parse(array[int.Parse(array3[1])]) + int.Parse(array[int.Parse(array3[2])]) == 20)
					{
						arrayList.RemoveAt(int.Parse(array3[2]));
						arrayList.RemoveAt(int.Parse(array3[1]));
						arrayList.RemoveAt(int.Parse(array3[0]));
						arrayList.Sort();
						if ((num - 20) % 10 == 0)
						{
							return 30;
						}
						if (arrayList[0] == arrayList[1])
						{
							return 40;
						}
						return (num - 20) % 10;
					}
					else if (int.Parse(array[int.Parse(array3[0])]) + int.Parse(array[int.Parse(array3[1])]) + int.Parse(array[int.Parse(array3[2])]) == 30)
					{
						arrayList.RemoveAt(int.Parse(array3[2]));
						arrayList.RemoveAt(int.Parse(array3[1]));
						arrayList.RemoveAt(int.Parse(array3[0]));
						arrayList.Sort();
						if ((num - 30) % 10 == 0)
						{
							return 30;
						}
						if (arrayList[0] == arrayList[1])
						{
							return 40;
						}
						return (num - 30) % 10;
					}
					else
					{
						j++;
					}
				}
			}
			return 0;
		}

		public static string AddDnNum(string LotteryNumber, int n)
		{
			string text = "";
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				int num = Convert.ToInt32(array[i]) + n;
				if (num >= 10)
				{
					num %= 10;
				}
				text = text + num + ",";
			}
			return text.Substring(0, text.Length - 1);
		}

		public static string CheckNNum(string LotteryNumber)
		{
			ArrayList arrayList = new ArrayList();
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = "0,1,2@0,1,3@0,1,4@0,2,3@0,2,4@0,3,4@1,2,3@1,2,4@1,3,4@2,3,4".Split(new char[]
			{
				'@'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					','
				});
				int j = 0;
				while (j < array.Length)
				{
					arrayList.Add(int.Parse(array[0]));
					arrayList.Add(int.Parse(array[1]));
					arrayList.Add(int.Parse(array[2]));
					arrayList.Add(int.Parse(array[3]));
					arrayList.Add(int.Parse(array[4]));
					int num = int.Parse(array[0]) + int.Parse(array[1]) + int.Parse(array[2]) + int.Parse(array[3]) + int.Parse(array[4]);
					if (num <= 10)
					{
						return "五小";
					}
					if (int.Parse(array[int.Parse(array3[0])]) + int.Parse(array[int.Parse(array3[1])]) + int.Parse(array[int.Parse(array3[2])]) == 10)
					{
						arrayList.RemoveAt(int.Parse(array3[2]));
						arrayList.RemoveAt(int.Parse(array3[1]));
						arrayList.RemoveAt(int.Parse(array3[0]));
						arrayList.Sort();
						if ((num - 10) % 10 == 0)
						{
							return "牛牛";
						}
						if (arrayList[0] == arrayList[1])
						{
							return "牛对子";
						}
						return "牛" + (num - 10) % 10;
					}
					else if (int.Parse(array[int.Parse(array3[0])]) + int.Parse(array[int.Parse(array3[1])]) + int.Parse(array[int.Parse(array3[2])]) == 20)
					{
						arrayList.RemoveAt(int.Parse(array3[2]));
						arrayList.RemoveAt(int.Parse(array3[1]));
						arrayList.RemoveAt(int.Parse(array3[0]));
						arrayList.Sort();
						if ((num - 20) % 10 == 0)
						{
							return "牛牛";
						}
						if (arrayList[0] == arrayList[1])
						{
							return "牛对子";
						}
						return "牛" + (num - 20) % 10;
					}
					else if (int.Parse(array[int.Parse(array3[0])]) + int.Parse(array[int.Parse(array3[1])]) + int.Parse(array[int.Parse(array3[2])]) == 30)
					{
						arrayList.RemoveAt(int.Parse(array3[2]));
						arrayList.RemoveAt(int.Parse(array3[1]));
						arrayList.RemoveAt(int.Parse(array3[0]));
						arrayList.Sort();
						if ((num - 30) % 10 == 0)
						{
							return "牛牛";
						}
						if (arrayList[0] == arrayList[1])
						{
							return "牛对子";
						}
						return "牛" + (num - 30) % 10;
					}
					else
					{
						j++;
					}
				}
			}
			return "无牛";
		}

		private static int SubstringCount(string str, string substring)
		{
			string[] array = substring.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (str.Contains(array[i]))
				{
					string text = str.Replace(array[i], "");
					return (str.Length - text.Length) / array[i].Length;
				}
			}
			return 0;
		}
	}
}
