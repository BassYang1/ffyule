using System;
using System.Collections;

namespace Lottery.Utils
{
	public static class CheckSSC_DDBDD
	{
		public static int P_BDD(string LotteryNumber, string CheckNumber, string Pos)
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
			if (array2.Length > 1)
			{
				return 0;
			}
			if (Pos == "")
			{
				Pos = "L";
			}
			for (int i = 0; i < array2.Length; i++)
			{
				if (array.Length == 3)
				{
					if (array2[i].IndexOf(array[0]) != -1)
					{
						num++;
					}
					if (array2[i].IndexOf(array[1]) != -1)
					{
						num++;
					}
					if (array2[i].IndexOf(array[2]) != -1)
					{
						num++;
					}
				}
				else
				{
					if (Pos == "L")
					{
						if (array[0] == array[1] || array[1] == array[2] || array[0] == array[2])
						{
							if (array[0] == array[1] && array[0] == array[2])
							{
								if (array2[i].IndexOf(array[0]) != -1)
								{
									num++;
								}
							}
							else
							{
								if (array[0] == array[1])
								{
									if (array2[i].IndexOf(array[1]) != -1)
									{
										num++;
									}
									if (array2[i].IndexOf(array[2]) != -1)
									{
										num++;
									}
								}
								if (array[0] == array[2])
								{
									if (array2[i].IndexOf(array[0]) != -1)
									{
										num++;
									}
									if (array2[i].IndexOf(array[1]) != -1)
									{
										num++;
									}
								}
								if (array[1] == array[2])
								{
									if (array2[i].IndexOf(array[0]) != -1)
									{
										num++;
									}
									if (array2[i].IndexOf(array[2]) != -1)
									{
										num++;
									}
								}
							}
						}
						else
						{
							if (array2[i].IndexOf(array[0]) != -1)
							{
								num++;
							}
							if (array2[i].IndexOf(array[1]) != -1)
							{
								num++;
							}
							if (array2[i].IndexOf(array[2]) != -1)
							{
								num++;
							}
						}
					}
					if (Pos == "C")
					{
						if (array[1] == array[2] || array[2] == array[3] || array[1] == array[3])
						{
							if (array[1] == array[2] && array[1] == array[3])
							{
								if (array2[i].IndexOf(array[1]) != -1)
								{
									num++;
								}
							}
							else
							{
								if (array[1] == array[2])
								{
									if (array2[i].IndexOf(array[2]) != -1)
									{
										num++;
									}
									if (array2[i].IndexOf(array[3]) != -1)
									{
										num++;
									}
								}
								if (array[1] == array[3])
								{
									if (array2[i].IndexOf(array[1]) != -1)
									{
										num++;
									}
									if (array2[i].IndexOf(array[2]) != -1)
									{
										num++;
									}
								}
								if (array[2] == array[3])
								{
									if (array2[i].IndexOf(array[1]) != -1)
									{
										num++;
									}
									if (array2[i].IndexOf(array[3]) != -1)
									{
										num++;
									}
								}
							}
						}
						else
						{
							if (array2[i].IndexOf(array[1]) != -1)
							{
								num++;
							}
							if (array2[i].IndexOf(array[2]) != -1)
							{
								num++;
							}
							if (array2[i].IndexOf(array[3]) != -1)
							{
								num++;
							}
						}
					}
					if (Pos == "R")
					{
						if (array[2] == array[3] || array[3] == array[4] || array[2] == array[4])
						{
							if (array[2] == array[3] && array[2] == array[4])
							{
								if (array2[i].IndexOf(array[2]) != -1)
								{
									num++;
								}
							}
							else
							{
								if (array[2] == array[3])
								{
									if (array2[i].IndexOf(array[3]) != -1)
									{
										num++;
									}
									if (array2[i].IndexOf(array[4]) != -1)
									{
										num++;
									}
								}
								if (array[2] == array[4])
								{
									if (array2[i].IndexOf(array[2]) != -1)
									{
										num++;
									}
									if (array2[i].IndexOf(array[3]) != -1)
									{
										num++;
									}
								}
								if (array[3] == array[4])
								{
									if (array2[i].IndexOf(array[2]) != -1)
									{
										num++;
									}
									if (array2[i].IndexOf(array[4]) != -1)
									{
										num++;
									}
								}
							}
						}
						else
						{
							if (array2[i].IndexOf(array[2]) != -1)
							{
								num++;
							}
							if (array2[i].IndexOf(array[3]) != -1)
							{
								num++;
							}
							if (array2[i].IndexOf(array[4]) != -1)
							{
								num++;
							}
						}
					}
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
			if (array2.Length != 3 && array2.Length != 5)
			{
				return 0;
			}
			if (array.Length == 3)
			{
				if (array2[0].IndexOf(array[0]) != -1)
				{
					num++;
				}
				if (array2[1].IndexOf(array[1]) != -1)
				{
					num++;
				}
				if (array2[2].IndexOf(array[2]) != -1)
				{
					num++;
				}
			}
			if (array.Length == 5)
			{
				if (array2[0].IndexOf(array[0]) != -1)
				{
					num++;
				}
				if (array2[1].IndexOf(array[1]) != -1)
				{
					num++;
				}
				if (array2[2].IndexOf(array[2]) != -1)
				{
					num++;
				}
				if (array2[3].IndexOf(array[3]) != -1)
				{
					num++;
				}
				if (array2[4].IndexOf(array[4]) != -1)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_DD2(string LotteryNumber, string CheckNumber)
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
			if (array2.Length == 1 && array2[0].IndexOf(array[0]) != -1)
			{
				num++;
			}
			if (array2.Length == 2 && array2[0].IndexOf(array[0]) != -1 && array2[1].IndexOf(array[1]) != -1)
			{
				num++;
			}
			if (array2.Length == 3 && array2[0].IndexOf(array[0]) != -1 && array2[1].IndexOf(array[1]) != -1 && array2[2].IndexOf(array[2]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_3BDD1(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string text = "";
			if (Pos == "L")
			{
				text = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			if (Pos == "R")
			{
				text = string.Concat(new string[]
				{
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			string[] array2 = text.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array2.Length; i++)
			{
				if (!arrayList.Contains(array2[i]))
				{
					arrayList.Add(array2[i]);
				}
			}
			array2 = (string[])arrayList.ToArray(typeof(string));
			for (int j = 0; j < array2.Length; j++)
			{
				if (CheckNumber.Contains(array2[j]))
				{
					num++;
				}
			}
			return num;
		}

		public static int P_3BDD2(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string text = "";
			if (Pos == "L")
			{
				text = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			if (Pos == "R")
			{
				text = string.Concat(new string[]
				{
					array[2],
					",",
					array[3],
					",",
					array[4]
				});
			}
			string[] array2 = text.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array2.Length; i++)
			{
				if (!arrayList.Contains(array2[i]))
				{
					arrayList.Add(array2[i]);
				}
			}
			array2 = (string[])arrayList.ToArray(typeof(string));
			for (int j = 0; j < array2.Length; j++)
			{
				for (int k = j + 1; k < array2.Length; k++)
				{
					if (CheckNumber.Contains(array2[j]) && CheckNumber.Contains(array2[k]))
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int P_4BDD1(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string text = string.Concat(new string[]
			{
				array[1],
				",",
				array[2],
				",",
				array[3],
				",",
				array[4]
			});
			string[] array2 = text.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array2.Length; i++)
			{
				if (!arrayList.Contains(array2[i]))
				{
					arrayList.Add(array2[i]);
				}
			}
			array2 = (string[])arrayList.ToArray(typeof(string));
			for (int j = 0; j < array2.Length; j++)
			{
				if (CheckNumber.Contains(array2[j]))
				{
					num++;
				}
			}
			return num;
		}

		public static int P_4BDD2(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string text = string.Concat(new string[]
			{
				array[1],
				",",
				array[2],
				",",
				array[3],
				",",
				array[4]
			});
			string[] array2 = text.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array2.Length; i++)
			{
				if (!arrayList.Contains(array2[i]))
				{
					arrayList.Add(array2[i]);
				}
			}
			array2 = (string[])arrayList.ToArray(typeof(string));
			for (int j = 0; j < array2.Length; j++)
			{
				for (int k = j + 1; k < array2.Length; k++)
				{
					if (CheckNumber.Contains(array2[j]) && CheckNumber.Contains(array2[k]))
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int P_5BDD2(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string text = string.Concat(new string[]
			{
				array[0],
				",",
				array[1],
				",",
				array[2],
				",",
				array[3],
				",",
				array[4]
			});
			string[] array2 = text.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array2.Length; i++)
			{
				if (!arrayList.Contains(array2[i]))
				{
					arrayList.Add(array2[i]);
				}
			}
			array2 = (string[])arrayList.ToArray(typeof(string));
			for (int j = 0; j < array2.Length; j++)
			{
				for (int k = j + 1; k < array2.Length; k++)
				{
					if (CheckNumber.Contains(array2[j]) && CheckNumber.Contains(array2[k]))
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int P_5BDD3(string LotteryNumber, string CheckNumber)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string text = string.Concat(new string[]
			{
				array[0],
				",",
				array[1],
				",",
				array[2],
				",",
				array[3],
				",",
				array[4]
			});
			string[] array2 = text.Split(new char[]
			{
				','
			});
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array2.Length; i++)
			{
				if (!arrayList.Contains(array2[i]))
				{
					arrayList.Add(array2[i]);
				}
			}
			array2 = (string[])arrayList.ToArray(typeof(string));
			for (int j = 0; j < array2.Length; j++)
			{
				for (int k = j + 1; k < array2.Length; k++)
				{
					for (int l = k + 1; l < array2.Length; l++)
					{
						if (CheckNumber.Contains(array2[j]) && CheckNumber.Contains(array2[k]) && CheckNumber.Contains(array2[l]))
						{
							num++;
						}
					}
				}
			}
			return num;
		}
	}
}
