using System;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
	public static class Check11X5_RXFS
	{
		public static int P11_RXFS_1(string LotteryNumber, string CheckNumber)
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
				if (Check11X5_RXFS.SubstringCount(CheckNumber, array[i]) > 1)
				{
					return 0;
				}
				if (array[i].Length != 2)
				{
					return 0;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				string value = array[j];
				if (LotteryNumber.IndexOf(value) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P11_RXFS_2(string LotteryNumber, string CheckNumber)
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
			if (array.Length < 2)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					return 0;
				}
				if (Check11X5_RXFS.SubstringCount(CheckNumber, array[i]) > 1)
				{
					return 0;
				}
				if (array[i].Length != 2)
				{
					return 0;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				for (int k = j + 1; k < array.Length; k++)
				{
					string text = array[j] + "," + array[k];
					string[] array2 = text.Split(new char[]
					{
						','
					});
					if (LotteryNumber.IndexOf(array2[0]) != -1 && LotteryNumber.IndexOf(array2[1]) != -1)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int P11_RXFS_3(string LotteryNumber, string CheckNumber)
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
			if (array.Length < 3)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					return 0;
				}
				if (Check11X5_RXFS.SubstringCount(CheckNumber, array[i]) > 1)
				{
					return 0;
				}
				if (array[i].Length != 2)
				{
					return 0;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				for (int k = j + 1; k < array.Length; k++)
				{
					for (int l = k + 1; l < array.Length; l++)
					{
						string text = string.Concat(new string[]
						{
							array[j],
							",",
							array[k],
							",",
							array[l]
						});
						string[] array2 = text.Split(new char[]
						{
							','
						});
						if (LotteryNumber.IndexOf(array2[0]) != -1 && LotteryNumber.IndexOf(array2[1]) != -1 && LotteryNumber.IndexOf(array2[2]) != -1)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		public static int P11_RXFS_4(string LotteryNumber, string CheckNumber)
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
			if (array.Length < 4)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					return 0;
				}
				if (Check11X5_RXFS.SubstringCount(CheckNumber, array[i]) > 1)
				{
					return 0;
				}
				if (array[i].Length != 2)
				{
					return 0;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				for (int k = j + 1; k < array.Length; k++)
				{
					for (int l = k + 1; l < array.Length; l++)
					{
						for (int m = l + 1; m < array.Length; m++)
						{
							string text = string.Concat(new string[]
							{
								array[j],
								",",
								array[k],
								",",
								array[l],
								",",
								array[m]
							});
							string[] array2 = text.Split(new char[]
							{
								','
							});
							if (LotteryNumber.IndexOf(array2[0]) != -1 && LotteryNumber.IndexOf(array2[1]) != -1 && LotteryNumber.IndexOf(array2[2]) != -1 && LotteryNumber.IndexOf(array2[3]) != -1)
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		public static int P11_RXFS_5(string LotteryNumber, string CheckNumber)
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
			if (array.Length < 5)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					return 0;
				}
				if (Check11X5_RXFS.SubstringCount(CheckNumber, array[i]) > 1)
				{
					return 0;
				}
				if (array[i].Length != 2)
				{
					return 0;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				for (int k = j + 1; k < array.Length; k++)
				{
					for (int l = k + 1; l < array.Length; l++)
					{
						for (int m = l + 1; m < array.Length; m++)
						{
							for (int n = m + 1; n < array.Length; n++)
							{
								string text = string.Concat(new string[]
								{
									array[j],
									",",
									array[k],
									",",
									array[l],
									",",
									array[m],
									",",
									array[n]
								});
								string[] array2 = LotteryNumber.Split(new char[]
								{
									','
								});
								if (text.IndexOf(array2[0]) != -1 && text.IndexOf(array2[1]) != -1 && text.IndexOf(array2[2]) != -1 && text.IndexOf(array2[3]) != -1 && text.IndexOf(array2[4]) != -1)
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

		public static int P11_RXFS_6(string LotteryNumber, string CheckNumber)
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
			if (array.Length < 6)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					return 0;
				}
				if (Check11X5_RXFS.SubstringCount(CheckNumber, array[i]) > 1)
				{
					return 0;
				}
				if (array[i].Length != 2)
				{
					return 0;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				for (int k = j + 1; k < array.Length; k++)
				{
					for (int l = k + 1; l < array.Length; l++)
					{
						for (int m = l + 1; m < array.Length; m++)
						{
							for (int n = m + 1; n < array.Length; n++)
							{
								for (int num2 = n + 1; num2 < array.Length; num2++)
								{
									string text = string.Concat(new string[]
									{
										array[j],
										",",
										array[k],
										",",
										array[l],
										",",
										array[m],
										",",
										array[n],
										",",
										array[num2]
									});
									string[] array2 = LotteryNumber.Split(new char[]
									{
										','
									});
									if (text.IndexOf(array2[0]) != -1 && text.IndexOf(array2[1]) != -1 && text.IndexOf(array2[2]) != -1 && text.IndexOf(array2[3]) != -1 && text.IndexOf(array2[4]) != -1)
									{
										num++;
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		public static int P11_RXFS_7(string LotteryNumber, string CheckNumber)
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
			if (array.Length < 7)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					return 0;
				}
				if (Check11X5_RXFS.SubstringCount(CheckNumber, array[i]) > 1)
				{
					return 0;
				}
				if (array[i].Length != 2)
				{
					return 0;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				for (int k = j + 1; k < array.Length; k++)
				{
					for (int l = k + 1; l < array.Length; l++)
					{
						for (int m = l + 1; m < array.Length; m++)
						{
							for (int n = m + 1; n < array.Length; n++)
							{
								for (int num2 = n + 1; num2 < array.Length; num2++)
								{
									for (int num3 = num2 + 1; num3 < array.Length; num3++)
									{
										string text = string.Concat(new string[]
										{
											array[j],
											",",
											array[k],
											",",
											array[l],
											",",
											array[m],
											",",
											array[n],
											",",
											array[num2],
											",",
											array[num3]
										});
										string[] array2 = LotteryNumber.Split(new char[]
										{
											','
										});
										if (text.IndexOf(array2[0]) != -1 && text.IndexOf(array2[1]) != -1 && text.IndexOf(array2[2]) != -1 && text.IndexOf(array2[3]) != -1 && text.IndexOf(array2[4]) != -1)
										{
											num++;
										}
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		public static int P11_RXFS_8(string LotteryNumber, string CheckNumber)
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
			if (array.Length < 8)
			{
				return 0;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					return 0;
				}
				if (Check11X5_RXFS.SubstringCount(CheckNumber, array[i]) > 1)
				{
					return 0;
				}
				if (array[i].Length != 2)
				{
					return 0;
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				for (int k = j + 1; k < array.Length; k++)
				{
					for (int l = k + 1; l < array.Length; l++)
					{
						for (int m = l + 1; m < array.Length; m++)
						{
							for (int n = m + 1; n < array.Length; n++)
							{
								for (int num2 = n + 1; num2 < array.Length; num2++)
								{
									for (int num3 = num2 + 1; num3 < array.Length; num3++)
									{
										for (int num4 = num3 + 1; num4 < array.Length; num4++)
										{
											string text = string.Concat(new string[]
											{
												array[j],
												",",
												array[k],
												",",
												array[l],
												",",
												array[m],
												",",
												array[n],
												",",
												array[num2],
												",",
												array[num3],
												",",
												array[num4]
											});
											string[] array2 = LotteryNumber.Split(new char[]
											{
												','
											});
											if (text.IndexOf(array2[0]) != -1 && text.IndexOf(array2[1]) != -1 && text.IndexOf(array2[2]) != -1 && text.IndexOf(array2[3]) != -1 && text.IndexOf(array2[4]) != -1)
											{
												num++;
											}
										}
									}
								}
							}
						}
					}
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
