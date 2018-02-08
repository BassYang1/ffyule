using System;
using System.Collections;
using System.Linq;

namespace Lottery.Utils
{
	public static class CheckSSC_3Start
	{
		public static int P_3ZX(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array3.Length >= 3 && array3[0].IndexOf(array2[0]) != -1 && array3[1].IndexOf(array2[1]) != -1 && array3[2].IndexOf(array2[2]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_3DS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = CheckNumber.Split(new char[]
			{
				','
			});
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i];
			}
			for (int j = 0; j < array2.Length; j++)
			{
				if (text == array2[j].Replace(",", ""))
				{
					num++;
				}
			}
			return num;
		}

		public static int P_3Z3(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array2[0] == array2[1] || array2[1] == array2[2] || array2[0] == array2[2])
			{
				if (array2[0] == array2[1] && array2[0] == array2[2] && array2[1] == array2[2])
				{
					return num;
				}
				string[] array3 = CheckNumber.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array3.Length; i++)
				{
					if (array2[0] == array2[1])
					{
						if (array3[i].IndexOf(array2[0]) != -1 && array3[i].IndexOf(array2[2]) != -1)
						{
							num++;
						}
					}
					else if (array2[0] == array2[2])
					{
						if (array3[i].IndexOf(array2[0]) != -1 && array3[i].IndexOf(array2[1]) != -1)
						{
							num++;
						}
					}
					else if (array2[1] == array2[2] && array3[i].IndexOf(array2[0]) != -1 && array3[i].IndexOf(array2[1]) != -1)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int P_3Z3_2(string LotteryNumber, string CheckNumber, string Pos)
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
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			string[] array3 = LotteryNumber.Split(new char[]
			{
				','
			});
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < array3.Length; i++)
			{
				for (int j = 0; j < array3.Length; j++)
				{
					for (int k = 0; k < array3.Length; k++)
					{
						if (i != j && j != k && i != k && (array3[i] == array3[j] || array3[j] == array3[k] || array3[i] == array3[k]) && (!(array3[i] == array3[j]) || !(array3[j] == array3[k]) || !(array3[i] == array3[k])))
						{
							string text = array3[i] + array3[j] + array3[k];
							if (!hashtable.Contains(text))
							{
								hashtable.Add(text, text);
								for (int l = 0; l < array2.Length; l++)
								{
									if (text == array2[l].Replace(",", ""))
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

		public static int P_3Z6(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array2[0] != array2[1] && array2[0] != array2[2] && array2[1] != array2[2])
			{
				string[] array3 = CheckNumber.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i].IndexOf(array2[0]) != -1 && array3[i].IndexOf(array2[1]) != -1 && array3[i].IndexOf(array2[2]) != -1)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int P_3Z6_2(string LotteryNumber, string CheckNumber, string Pos)
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
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			string[] array3 = LotteryNumber.Split(new char[]
			{
				','
			});
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < array3.Length; i++)
			{
				for (int j = 0; j < array3.Length; j++)
				{
					for (int k = 0; k < array3.Length; k++)
					{
						if (i != j && j != k && i != k)
						{
							string text = array3[i] + array3[j] + array3[k];
							if (!hashtable.Contains(text))
							{
								hashtable.Add(text, text);
								for (int l = 0; l < array2.Length; l++)
								{
									if (text == array2[l].Replace(",", ""))
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

		public static int P_3HX(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			string[] array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array3.Length; i++)
			{
				if (array3[i].Length != 3)
				{
					return 0;
				}
				if (array2[0] != array2[1] && array2[1] != array2[2] && array2[0] != array2[2])
				{
					if (array3[i].IndexOf(array2[0]) != -1 && array3[i].IndexOf(array2[1]) != -1 && array3[i].IndexOf(array2[2]) != -1)
					{
						num++;
					}
				}
				else if (array2[0] == array2[1])
				{
					if (Func.SearchStrNum(array3[i], array2[1]) == 2 && Func.SearchStrNum(array3[i], array2[2]) == 1)
					{
						num++;
					}
				}
				else if (array2[1] == array2[2])
				{
					if (Func.SearchStrNum(array3[i], array2[0]) == 1 && Func.SearchStrNum(array3[i], array2[1]) == 2)
					{
						num++;
					}
				}
				else if (Func.SearchStrNum(array3[i], array2[0]) == 2 && Func.SearchStrNum(array3[i], array2[1]) == 1)
				{
					num++;
				}
			}
			return num;
		}

		public static int R_3FS(string LotteryNumber, string CheckNumber, string Pos)
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
			string[] array3 = Pos.Split(new char[]
			{
				','
			});
			string text = "";
			for (int i = 0; i < array3.Length; i++)
			{
				int num2 = Convert.ToInt32(array3[i]);
				text = text + array[num2] + ",";
			}
			string[] array4 = text.Substring(0, text.Length - 1).Split(new char[]
			{
				','
			});
			for (int j = 0; j < array4.Length; j++)
			{
				for (int k = j + 1; k < array4.Length; k++)
				{
					for (int l = k + 1; l < array4.Length; l++)
					{
						string text2 = string.Concat(new string[]
						{
							array4[j],
							",",
							array4[k],
							",",
							array4[l]
						});
						string[] array5 = text2.Split(new char[]
						{
							','
						});
						if (array2[0].Contains(array5[0]) && array2[1].Contains(array5[1]) && array2[2].Contains(array5[2]))
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		public static int R_3DS(string LotteryNumber, string CheckNumber, string Pos)
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
			string[] array3 = Pos.Split(new char[]
			{
				','
			});
			string text = "";
			for (int i = 0; i < array3.Length; i++)
			{
				int num2 = Convert.ToInt32(array3[i]);
				text = text + array[num2] + ",";
			}
			string[] array4 = text.Substring(0, text.Length - 1).Split(new char[]
			{
				','
			});
			for (int j = 0; j < array4.Length; j++)
			{
				for (int k = j + 1; k < array4.Length; k++)
				{
					for (int l = k + 1; l < array4.Length; l++)
					{
						string a = array4[j] + array4[k] + array4[l];
						for (int m = 0; m < array2.Length; m++)
						{
							if (a == array2[m].Replace(",", ""))
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		public static int R_3Z3_2(string LotteryNumber, string CheckNumber, string Pos)
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
			string[] array3 = Pos.Split(new char[]
			{
				','
			});
			string[] array4 = new string[array3.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				int num2 = Convert.ToInt32(array3[i]);
				array4[i] = array[num2];
			}
			Hashtable hashtable = new Hashtable();
			for (int j = 0; j < array4.Length; j++)
			{
				for (int k = 0; k < array4.Length; k++)
				{
					for (int l = 0; l < array4.Length; l++)
					{
						if ((array4[j] == array4[k] || array4[k] == array4[l] || array4[j] == array4[l]) && (!(array4[j] == array4[k]) || !(array4[k] == array4[l]) || !(array4[j] == array4[l])))
						{
							string text = array4[j] + array4[k] + array4[l];
							if (!hashtable.Contains(text))
							{
								hashtable.Add(text, text);
								for (int m = 0; m < array2.Length; m++)
								{
									if (text == array2[m].Replace(",", ""))
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

		public static int R_3Z3(string LotteryNumber, string CheckNumber, string Pos)
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
			string[] array3 = Pos.Split(new char[]
			{
				','
			});
			string[] array4 = new string[array3.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				int num2 = Convert.ToInt32(array3[i]);
				array4[i] = array[num2];
			}
			new Hashtable();
			for (int j = 0; j < array4.Length; j++)
			{
				for (int k = j + 1; k < array4.Length; k++)
				{
					for (int l = k + 1; l < array4.Length; l++)
					{
						if ((array4[j] == array4[k] || array4[k] == array4[l] || array4[j] == array4[l]) && (!(array4[j] == array4[k]) || !(array4[k] == array4[l]) || !(array4[j] == array4[l])))
						{
							for (int m = 0; m < array2.Length; m++)
							{
								if (array2[m].IndexOf(array4[j]) != -1 && array2[m].IndexOf(array4[k]) != -1 && array2[m].IndexOf(array4[l]) != -1)
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

		public static int R_3Z6(string LotteryNumber, string CheckNumber, string Pos)
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
			string[] array3 = Pos.Split(new char[]
			{
				','
			});
			string[] array4 = new string[array3.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				int num2 = Convert.ToInt32(array3[i]);
				array4[i] = array[num2];
			}
			new Hashtable();
			for (int j = 0; j < array4.Length; j++)
			{
				for (int k = j + 1; k < array4.Length; k++)
				{
					for (int l = k + 1; l < array4.Length; l++)
					{
						if (j != k && j != l && k != l && (!(array4[j] == array4[k]) || !(array4[k] == array4[l]) || !(array4[j] == array4[l])) && array4[j] != array4[k] && array4[j] != array4[l] && array4[k] != array4[l])
						{
							for (int m = 0; m < array2.Length; m++)
							{
								if (array2[m].IndexOf(array4[j]) != -1 && array2[m].IndexOf(array4[k]) != -1 && array2[m].IndexOf(array4[l]) != -1)
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

		public static int R_3Z6_2(string LotteryNumber, string CheckNumber, string Pos)
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
			string[] array3 = Pos.Split(new char[]
			{
				','
			});
			string[] array4 = new string[array3.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				int num2 = Convert.ToInt32(array3[i]);
				array4[i] = array[num2];
			}
			new Hashtable();
			for (int j = 0; j < array4.Length; j++)
			{
				for (int k = j + 1; k < array4.Length; k++)
				{
					for (int l = k + 1; l < array4.Length; l++)
					{
						if ((!(array4[j] == array4[k]) || !(array4[k] == array4[l]) || !(array4[j] == array4[l])) && array4[j] != array4[k] && array4[j] != array4[l] && array4[k] != array4[l])
						{
							string a = array4[j] + array4[k] + array4[l];
							for (int m = 0; m < array2.Length; m++)
							{
								if (a == array2[m].Replace(",", ""))
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

		public static int P_3HE(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			int num2 = 0;
			if (array.Length == 3)
			{
				num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
			}
			else
			{
				if (Pos == "L")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
				}
				if (Pos == "C")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "R")
				{
					num2 = Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "WQB")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
				}
				if (Pos == "WQS")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "WQG")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "WBS")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "WBG")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "WSG")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "QBS")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "QBG")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "QSG")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "BSG")
				{
					num2 = Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
			}
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				if (Convert.ToInt32(array2[i]) == num2)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_3KD(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] source = new string[3];
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			source = LotteryNumber.Split(new char[]
			{
				','
			});
			int num2 = Convert.ToInt32(source.Max<string>()) - Convert.ToInt32(source.Min<string>());
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				if (Convert.ToInt32(array2[i]) == num2)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_3Z3DS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = new string[3];
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array2[0] == array2[1])
			{
				if (CheckNumber.Contains(array2[0] + array2[0] + array2[2]))
				{
					num++;
				}
				if (CheckNumber.Contains(array2[0] + array2[2] + array2[0]))
				{
					num++;
				}
				if (CheckNumber.Contains(array2[2] + array2[0] + array2[0]))
				{
					num++;
				}
			}
			if (array2[0] == array2[2])
			{
				if (CheckNumber.Contains(array2[0] + array2[0] + array2[1]))
				{
					num++;
				}
				if (CheckNumber.Contains(array2[0] + array2[1] + array2[0]))
				{
					num++;
				}
				if (CheckNumber.Contains(array2[1] + array2[0] + array2[0]))
				{
					num++;
				}
			}
			if (array2[1] == array2[2])
			{
				if (CheckNumber.Contains(array2[1] + array2[1] + array2[0]))
				{
					num++;
				}
				if (CheckNumber.Contains(array2[1] + array2[0] + array2[1]))
				{
					num++;
				}
				if (CheckNumber.Contains(array2[0] + array2[1] + array2[1]))
				{
					num++;
				}
			}
			return num;
		}

		public static int P_3Z6DS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = new string[3];
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array3 = CheckNumber.Split(new char[]
			{
				','
			});
			if (array2[0] != array2[1] && array2[0] != array2[2] && array2[1] != array2[2])
			{
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i].Contains(array2[0]) && array3[i].Contains(array2[1]) && array3[i].Contains(array2[2]))
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int P_3QTWS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			int num2 = 0;
			if (array.Length == 3)
			{
				num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
			}
			else
			{
				if (Pos == "L")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
				}
				if (Pos == "C")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "R")
				{
					num2 = Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "WQB")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]);
				}
				if (Pos == "WQS")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "WQG")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "WBS")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "WBG")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "WSG")
				{
					num2 = Convert.ToInt32(array[0]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "QBS")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]);
				}
				if (Pos == "QBG")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "QSG")
				{
					num2 = Convert.ToInt32(array[1]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
				if (Pos == "BSG")
				{
					num2 = Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]);
				}
			}
			string[] array2 = CheckNumber.Split(new char[]
			{
				'_'
			});
			for (int i = 0; i < array2.Length; i++)
			{
				if (Convert.ToInt32(array2[i]) == num2 % 10)
				{
					num++;
				}
			}
			return num;
		}

		public static int P_3QTTS(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = new string[3];
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array2[0] == array2[1] && array2[0] == array2[2] && array2[1] == array2[2] && CheckNumber.Contains("豹子"))
			{
				num++;
			}
			if (((array2[0] == array2[1] && array2[0] != array2[2]) || (array2[0] == array2[2] && array2[0] != array2[1]) || (array2[1] == array2[2] && array2[0] != array2[2])) && CheckNumber.Contains("对子"))
			{
				num++;
			}
			if (Convert.ToInt32(array2[0]) + 1 == Convert.ToInt32(array2[1]) && Convert.ToInt32(array2[1]) + 1 == Convert.ToInt32(array2[2]) && CheckNumber.Contains("顺子"))
			{
				num++;
			}
			return num;
		}

		public static int P_3ZBDZ3(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = new string[3];
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array2[0] == array2[1] && array2[0] == array2[2])
			{
				return 0;
			}
			if ((array2[0] == array2[1] || array2[0] == array2[2] || array2[1] == array2[2]) && string.Concat(new string[]
			{
				array2[0],
				",",
				array2[1],
				",",
				array2[2]
			}).Contains(CheckNumber))
			{
				num++;
			}
			return num;
		}

		public static int P_3ZBDZ6(string LotteryNumber, string CheckNumber, string Pos)
		{
			int num = 0;
			string[] array = LotteryNumber.Split(new char[]
			{
				','
			});
			string[] array2 = new string[3];
			if (array.Length == 3)
			{
				LotteryNumber = string.Concat(new string[]
				{
					array[0],
					",",
					array[1],
					",",
					array[2]
				});
			}
			else
			{
				if (Pos == "L")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "C")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "R")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "WQB")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[2]
					});
				}
				if (Pos == "WQS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[3]
					});
				}
				if (Pos == "WQG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[1],
						",",
						array[4]
					});
				}
				if (Pos == "WBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "WBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "WSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[0],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "QBS")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[3]
					});
				}
				if (Pos == "QBG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[2],
						",",
						array[4]
					});
				}
				if (Pos == "QSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[1],
						",",
						array[3],
						",",
						array[4]
					});
				}
				if (Pos == "BSG")
				{
					LotteryNumber = string.Concat(new string[]
					{
						array[2],
						",",
						array[3],
						",",
						array[4]
					});
				}
			}
			array2 = LotteryNumber.Split(new char[]
			{
				','
			});
			if (array2[0] == array2[1] && array2[0] == array2[2])
			{
				return 0;
			}
			if (array2[0] != array2[1] && array2[0] != array2[2] && array2[1] != array2[2] && string.Concat(new string[]
			{
				array2[0],
				",",
				array2[1],
				",",
				array2[2]
			}).Contains(CheckNumber))
			{
				num++;
			}
			return num;
		}

		public static int P_3ZH_3(string LotteryNumber, string CheckNumber, string Pos)
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
			if (Pos == "L" && array2.Length >= 3 && array2[0].IndexOf(array[0]) != -1 && array2[1].IndexOf(array[1]) != -1 && array2[2].IndexOf(array[2]) != -1)
			{
				num++;
			}
			if (Pos == "C" && array2.Length >= 3 && array2[0].IndexOf(array[1]) != -1 && array2[1].IndexOf(array[2]) != -1 && array2[2].IndexOf(array[3]) != -1)
			{
				num++;
			}
			if (Pos == "R" && array2.Length >= 3 && array2[0].IndexOf(array[2]) != -1 && array2[1].IndexOf(array[3]) != -1 && array2[2].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_3ZH_2(string LotteryNumber, string CheckNumber, string Pos)
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
			if (Pos == "L" && array2.Length >= 3 && array2[1].IndexOf(array[1]) != -1 && array2[2].IndexOf(array[2]) != -1)
			{
				num++;
			}
			if (Pos == "C" && array2.Length >= 3 && array2[1].IndexOf(array[2]) != -1 && array2[2].IndexOf(array[3]) != -1)
			{
				num++;
			}
			if (Pos == "R" && array2.Length >= 3 && array2[1].IndexOf(array[3]) != -1 && array2[2].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}

		public static int P_3ZH_1(string LotteryNumber, string CheckNumber, string Pos)
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
			if (Pos == "L" && array2.Length >= 3 && array2[2].IndexOf(array[2]) != -1)
			{
				num++;
			}
			if (Pos == "C" && array2.Length >= 3 && array2[2].IndexOf(array[3]) != -1)
			{
				num++;
			}
			if (Pos == "R" && array2.Length >= 3 && array2[2].IndexOf(array[4]) != -1)
			{
				num++;
			}
			return num;
		}
	}
}
