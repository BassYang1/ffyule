using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DAL.Flex;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class CheckOperation
	{
		public static bool Checking(DataRow row, string LotteryNumber, SqlCommand sqlCommand)
		{
			bool result;
			try
			{
				if (Convert.ToInt32(row["State"]) != 0)
				{
					result = true;
				}
				else
				{
					int logSysId = Convert.ToInt32(row["Id"]);
					string ssId = row["SsId"].ToString();
					int num = Convert.ToInt32(row["UserId"]);
					int num2 = Convert.ToInt32(row["LotteryId"]);
					int num3 = Convert.ToInt32(row["PlayId"]);
					int value = Convert.ToInt32(row["Num"]);
					string arg = row["IssueNum"].ToString();
					string betDetail = BetDetailDAL.GetBetDetail2(Convert.ToDateTime(row["STime2"]).ToString("yyyyMMdd"), num.ToString(), logSysId.ToString());
					decimal d = Convert.ToDecimal(row["Total"]);
					decimal num4 = Convert.ToDecimal(row["point"]);
					decimal num5 = Convert.ToDecimal(row["PointMoney"]);
					decimal num6 = Convert.ToDecimal(row["Bonus"]);
					decimal d2 = Convert.ToDecimal(row["Times"]);
					decimal d3 = Convert.ToDecimal(row["SingleMoney"]);
					string pos = row["Pos"].ToString();
					string text = row["PlayCode"].ToString();
					Convert.ToInt32(row["IsCheat"]);
					Convert.ToInt32(row["IsDelay"]);
					int num7 = Convert.ToInt32(row["ZhId"]);
					string sTime = row["STime"].ToString();
					string[] array = LotteryNumber.Split(new char[]
					{
						','
					});
					if (text.Equals("P_5QJ3"))
					{
						string[] array2 = betDetail.Split(new char[]
						{
							','
						});
						if (array2[0].IndexOf(CheckOperation.ReplaceStr(array[0])) == -1 || array2[1].IndexOf(CheckOperation.ReplaceStr(array[1])) == -1)
						{
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.CommandText = string.Concat(new object[]
							{
								"select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=",
								num,
								")-",
								num4,
								") from Sys_PlaySmallType where title2='P_5QJ3'"
							});
							num6 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
							text = text.Replace("P_5QJ3", "P_5QJ3_2");
						}
						else
						{
							text = text.Replace("P_5QJ3", "P_5QJ3_1");
						}
					}
					if (text.Equals("P_4QJ3"))
					{
						string[] array3 = betDetail.Split(new char[]
						{
							','
						});
						if (array3[0].IndexOf(CheckOperation.ReplaceStr(array[1])) == -1)
						{
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.CommandText = string.Concat(new object[]
							{
								"select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=",
								num,
								")-",
								num4,
								") from Sys_PlaySmallType where title2='P_4QJ3'"
							});
							num6 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
							text = text.Replace("P_4QJ3", "P_4QJ3_2");
						}
						else
						{
							text = text.Replace("P_4QJ3", "P_4QJ3_1");
						}
					}
					if (text.Equals("P_3QJ2_L"))
					{
						string[] array4 = betDetail.Split(new char[]
						{
							','
						});
						if (array4[0].IndexOf(CheckOperation.ReplaceStr(array[0])) == -1)
						{
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.CommandText = string.Concat(new object[]
							{
								"select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=",
								num,
								")-",
								num4,
								") from Sys_PlaySmallType where title2='P_3QJ2_L'"
							});
							num6 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
							text = text.Replace("P_3QJ2_L", "P_3QJ2_L_2");
						}
						else
						{
							text = text.Replace("P_3QJ2_L", "P_3QJ2_L_1");
						}
					}
					if (text.Equals("P_3QJ2_R"))
					{
						string[] array5 = betDetail.Split(new char[]
						{
							','
						});
						if (array5[0].IndexOf(CheckOperation.ReplaceStr(array[2])) == -1)
						{
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.CommandText = string.Concat(new object[]
							{
								"select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=",
								num,
								")-",
								num4,
								") from Sys_PlaySmallType where title2='P_3QJ2_R'"
							});
							num6 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
							text = text.Replace("P_3QJ2_R", "P_3QJ2_R_2");
						}
						else
						{
							text = text.Replace("P_3QJ2_R", "P_3QJ2_R_1");
						}
					}
					if (text.Equals("P_5QW3"))
					{
						string[] array6 = betDetail.Split(new char[]
						{
							','
						});
						if (array6[0].IndexOf(CheckOperation.ReplaceDX(array[0])) == -1 || array6[1].IndexOf(CheckOperation.ReplaceDX(array[1])) == -1)
						{
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.CommandText = string.Concat(new object[]
							{
								"select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=",
								num,
								")-",
								num4,
								") from Sys_PlaySmallType where title2='P_5QW3'"
							});
							num6 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
							text = text.Replace("P_5QW3", "P_5QW3_2");
						}
						else
						{
							text = text.Replace("P_5QW3", "P_5QW3_1");
						}
					}
					if (text.Equals("P_4QW3"))
					{
						string[] array7 = betDetail.Split(new char[]
						{
							','
						});
						if (array7[0].IndexOf(CheckOperation.ReplaceDX(array[1])) == -1)
						{
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.CommandText = string.Concat(new object[]
							{
								"select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=",
								num,
								")-",
								num4,
								") from Sys_PlaySmallType where title2='P_4QW3'"
							});
							num6 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
							text = text.Replace("P_4QW3", "P_4QW3_2");
						}
						else
						{
							text = text.Replace("P_4QW3", "P_4QW3_1");
						}
					}
					if (text.Equals("P_3QW2_L"))
					{
						string[] array8 = betDetail.Split(new char[]
						{
							','
						});
						if (array8[0].IndexOf(CheckOperation.ReplaceDX(array[0])) == -1)
						{
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.CommandText = string.Concat(new object[]
							{
								"select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=",
								num,
								")-",
								num4,
								") from Sys_PlaySmallType where title2='P_3QW2_L'"
							});
							num6 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
							text = text.Replace("P_3QW2_L", "P_3QW2_L_2");
						}
						else
						{
							text = text.Replace("P_3QW2_L", "P_3QW2_L_1");
						}
					}
					if (text.Equals("P_3QW2_R"))
					{
						string[] array9 = betDetail.Split(new char[]
						{
							','
						});
						if (array9[0].IndexOf(CheckOperation.ReplaceDX(array[2])) == -1)
						{
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.CommandText = string.Concat(new object[]
							{
								"select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=",
								num,
								")-",
								num4,
								") from Sys_PlaySmallType where title2='P_3QW2_R'"
							});
							num6 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
							text = text.Replace("P_3QW2_R", "P_3QW2_R_2");
						}
						else
						{
							text = text.Replace("P_3QW2_R", "P_3QW2_R_1");
						}
					}
					if (text.Equals("P_3ZBD_L"))
					{
						if (array[0] == array[1] || array[1] == array[2] || array[0] == array[2])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[0] != array[1] && array[1] != array[2] && array[0] != array[2])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("P_3ZBD_C"))
					{
						if (array[1] == array[2] || array[2] == array[3] || array[1] == array[3])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[1] != array[2] && array[2] != array[3] && array[1] != array[3])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("P_3ZBD_R"))
					{
						if (array[2] == array[3] || array[3] == array[4] || array[2] == array[4])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[2] != array[3] && array[3] != array[4] && array[2] != array[4])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_WQB"))
					{
						if (array[0] == array[1] || array[1] == array[2] || array[0] == array[2])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[0] != array[1] && array[1] != array[2] && array[0] != array[2])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_WQS"))
					{
						if (array[0] == array[1] || array[1] == array[3] || array[0] == array[3])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[0] != array[1] && array[1] != array[3] && array[0] != array[3])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_WQG"))
					{
						if (array[0] == array[1] || array[1] == array[4] || array[0] == array[4])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[0] != array[1] && array[1] != array[4] && array[0] != array[4])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_WBS"))
					{
						if (array[0] == array[2] || array[2] == array[3] || array[0] == array[3])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[0] != array[2] && array[2] != array[3] && array[0] != array[3])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_WBG"))
					{
						if (array[0] == array[2] || array[2] == array[4] || array[0] == array[4])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[0] != array[2] && array[2] != array[4] && array[0] != array[4])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_WSG"))
					{
						if (array[0] == array[3] || array[3] == array[4] || array[0] == array[4])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[0] != array[3] && array[3] != array[4] && array[0] != array[4])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_QBS"))
					{
						if (array[1] == array[2] || array[2] == array[3] || array[1] == array[3])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[1] != array[2] && array[2] != array[3] && array[1] != array[3])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_QBG"))
					{
						if (array[1] == array[2] || array[2] == array[4] || array[1] == array[4])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[1] != array[2] && array[2] != array[4] && array[1] != array[4])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_QSG"))
					{
						if (array[1] == array[3] || array[3] == array[4] || array[1] == array[4])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[1] != array[3] && array[3] != array[4] && array[1] != array[4])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("R_3ZBD_BSG"))
					{
						if (array[2] == array[3] || array[3] == array[4] || array[2] == array[4])
						{
							text = text.Replace("3ZBD", "3ZBDZ3");
						}
						if (array[2] != array[3] && array[3] != array[4] && array[2] != array[4])
						{
							num6 /= 2m;
							text = text.Replace("3ZBD", "3ZBDZ6");
						}
					}
					if (text.Equals("P_3ZHE_L") && array[0] != array[1] && array[0] != array[2] && array[1] != array[2])
					{
						num6 /= 2m;
					}
					if (text.Equals("P_3ZHE_C") && array[1] != array[2] && array[2] != array[3] && array[1] != array[3])
					{
						num6 /= 2m;
					}
					if (text.Equals("P_3ZHE_R") && array[0] != array[1] && array[1] != array[2] && array[0] != array[2])
					{
						num6 /= 2m;
					}
					if (text.Equals("P_3HX_L"))
					{
						if (array[0] == array[1] || array[1] == array[2] || array[0] == array[2])
						{
							text = text.Replace("3HX", "3Z3_2");
						}
						if (array[0] != array[1] && array[0] != array[2] && array[1] != array[2])
						{
							num6 /= 2m;
							text = text.Replace("3HX", "3Z6_2");
						}
					}
					if (text.Equals("P_3HX_C"))
					{
						if (array[1] == array[2] || array[2] == array[3] || array[1] == array[3])
						{
							text = text.Replace("3HX", "3Z3_2");
						}
						if (array[1] != array[2] && array[2] != array[3] && array[1] != array[3])
						{
							num6 /= 2m;
							text = text.Replace("3HX", "3Z6_2");
						}
					}
					if (text.Equals("P_3HX_R"))
					{
						if (array.Length == 3)
						{
							if (array[0] == array[1] || array[1] == array[2] || array[0] == array[2])
							{
								text = text.Replace("3HX", "3Z3_2");
							}
							if (array[0] != array[1] && array[1] != array[2] && array[0] != array[2])
							{
								num6 /= 2m;
								text = text.Replace("3HX", "3Z6_2");
							}
						}
						else
						{
							if (array[2] == array[3] || array[3] == array[4] || array[2] == array[4])
							{
								text = text.Replace("3HX", "3Z3_2");
							}
							if (array[2] != array[3] && array[3] != array[4] && array[2] != array[4])
							{
								num6 /= 2m;
								text = text.Replace("3HX", "3Z6_2");
							}
						}
					}
					if (text.Contains("R_3HX"))
					{
						if (array.Length == 3)
						{
							if (array[0] == array[1] || array[1] == array[2] || array[0] == array[2])
							{
								text = text.Replace("3HX", "3Z3_2");
							}
							if (array[0] != array[1] && array[1] != array[2] && array[0] != array[2])
							{
								num6 /= 2m;
								text = text.Replace("3HX", "3Z6_2");
							}
						}
						else
						{
							if (text.Equals("R_3HX_WQB") && array[0] != array[1] && array[1] != array[2] && array[0] != array[2])
							{
								num6 /= 2m;
							}
							if (text.Equals("R_3HX_WQS") && array[0] != array[1] && array[1] != array[3] && array[0] != array[3])
							{
								num6 /= 2m;
							}
							if (text.Equals("R_3HX_WQG") && array[0] != array[1] && array[1] != array[4] && array[0] != array[4])
							{
								num6 /= 2m;
							}
							if (text.Equals("R_3HX_WBS") && array[0] != array[2] && array[2] != array[3] && array[0] != array[3])
							{
								num6 /= 2m;
							}
							if (text.Equals("R_3HX_WBG") && array[0] != array[2] && array[2] != array[4] && array[0] != array[4])
							{
								num6 /= 2m;
							}
							if (text.Equals("R_3HX_WSG") && array[0] != array[3] && array[3] != array[4] && array[0] != array[4])
							{
								num6 /= 2m;
							}
							if (text.Equals("R_3HX_QBS") && array[1] != array[2] && array[2] != array[3] && array[1] != array[3])
							{
								num6 /= 2m;
							}
							if (text.Equals("R_3HX_QBG") && array[1] != array[2] && array[2] != array[4] && array[1] != array[4])
							{
								num6 /= 2m;
							}
							if (text.Equals("R_3HX_QSG") && array[1] != array[3] && array[3] != array[4] && array[1] != array[4])
							{
								num6 /= 2m;
							}
							if (text.Equals("R_3HX_BSG") && array[2] != array[3] && array[3] != array[4] && array[2] != array[4])
							{
								num6 /= 2m;
							}
						}
					}
					if (text.Contains("R_3ZHE"))
					{
						if (text.Equals("R_3ZHE_WQB") && array[0] != array[1] && array[1] != array[2] && array[0] != array[2])
						{
							num6 /= 2m;
						}
						if (text.Equals("R_3ZHE_WQS") && array[0] != array[1] && array[1] != array[3] && array[0] != array[3])
						{
							num6 /= 2m;
						}
						if (text.Equals("R_3ZHE_WQG") && array[0] != array[1] && array[1] != array[4] && array[0] != array[4])
						{
							num6 /= 2m;
						}
						if (text.Equals("R_3ZHE_WBS") && array[0] != array[2] && array[2] != array[3] && array[0] != array[3])
						{
							num6 /= 2m;
						}
						if (text.Equals("R_3ZHE_WBG") && array[0] != array[2] && array[2] != array[4] && array[0] != array[4])
						{
							num6 /= 2m;
						}
						if (text.Equals("R_3ZHE_WSG") && array[0] != array[3] && array[3] != array[4] && array[0] != array[4])
						{
							num6 /= 2m;
						}
						if (text.Equals("R_3ZHE_QBS") && array[1] != array[2] && array[2] != array[3] && array[1] != array[3])
						{
							num6 /= 2m;
						}
						if (text.Equals("R_3ZHE_QBG") && array[1] != array[2] && array[2] != array[4] && array[1] != array[4])
						{
							num6 /= 2m;
						}
						if (text.Equals("R_3ZHE_QSG") && array[1] != array[3] && array[3] != array[4] && array[1] != array[4])
						{
							num6 /= 2m;
						}
						if (text.Equals("R_3ZHE_BSG") && array[2] != array[3] && array[3] != array[4] && array[2] != array[4])
						{
							num6 /= 2m;
						}
					}
					int num8 = 0;
					int num9 = 0;
					if (text.Equals("P_LHH_WQ"))
					{
						num8 = Convert.ToInt32(array[0]);
						num9 = Convert.ToInt32(array[1]);
					}
					if (text.Equals("P_LHH_WB"))
					{
						num8 = Convert.ToInt32(array[0]);
						num9 = Convert.ToInt32(array[2]);
					}
					if (text.Equals("P_LHH_WS"))
					{
						num8 = Convert.ToInt32(array[0]);
						num9 = Convert.ToInt32(array[3]);
					}
					if (text.Equals("P_LHH_WG"))
					{
						num8 = Convert.ToInt32(array[0]);
						num9 = Convert.ToInt32(array[4]);
					}
					if (text.Equals("P_LHH_QB"))
					{
						num8 = Convert.ToInt32(array[1]);
						num9 = Convert.ToInt32(array[2]);
					}
					if (text.Equals("P_LHH_QS"))
					{
						num8 = Convert.ToInt32(array[1]);
						num9 = Convert.ToInt32(array[3]);
					}
					if (text.Equals("P_LHH_QG"))
					{
						num8 = Convert.ToInt32(array[1]);
						num9 = Convert.ToInt32(array[4]);
					}
					if (text.Equals("P_LHH_BS"))
					{
						num8 = Convert.ToInt32(array[2]);
						num9 = Convert.ToInt32(array[3]);
					}
					if (text.Equals("P_LHH_BG"))
					{
						num8 = Convert.ToInt32(array[2]);
						num9 = Convert.ToInt32(array[4]);
					}
					if (text.Equals("P_LHH_SG"))
					{
						num8 = Convert.ToInt32(array[3]);
						num9 = Convert.ToInt32(array[4]);
					}
					if (num8 != num9)
					{
						num6 = Convert.ToDecimal(num6 / Convert.ToDecimal(4.5));
					}
					int num10 = CheckPlay.Check(LotteryNumber, betDetail, pos, text);
					num5 *= d2;
					int num11;
					if (num10 > 0)
					{
						num11 = 3;
						num6 = num6 * d2 * num10 * d3 / 2m;
						decimal num12 = 200000m;
						if (num6 > num12)
						{
							num6 = num12;
						}
						sqlCommand.CommandType = CommandType.Text;
						sqlCommand.CommandText = "select top 1 MinNum from Sys_PlaySmallType where Id=" + num3;
						decimal num13 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
						if (num13 == 0m)
						{
							if (num6 > d * d2 * 100m)
							{
								decimal num14 = 18000m;
								if (num6 > num14)
								{
									num6 = num14;
								}
							}
						}
						else if (value < num13)
						{
							decimal num15 = 18000m;
							if (num6 > num15)
							{
								num6 = num15;
							}
						}
					}
					else
					{
						num11 = 2;
						num6 = 0m;
					}
					decimal num16 = num6 + num5 - d * d2;
					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.CommandText = string.Concat(new string[]
					{
						"update N_UserBet set State=",
						num11.ToString(),
						",WinNum=",
						num10.ToString(),
						",WinBonus=",
						num6.ToString(),
						",RealGet=",
						num16.ToString(),
						" where Id=",
						logSysId.ToString()
					});
					sqlCommand.ExecuteNonQuery();
					if (num6 > 0m)
					{
						new UserTotalTran().MoneyOpers(ssId, num.ToString(), num6, num2, num3, logSysId, 5, 99, "", "", "奖金派发", sTime);
					}
					if (num5 > 0m)
					{
						new UserTotalTran().MoneyOpers(ssId, num.ToString(), num5, num2, num3, logSysId, 4, 99, "", "", "返点派发", sTime);
					}
					if (num7 != 0)
					{
						string str = string.Format(" where LotteryId={0} and state=0 and zhid={1} and IssueNum>'{2}'", num2, num7, arg);
						sqlCommand.CommandType = CommandType.Text;
						sqlCommand.CommandText = "select count(Id) from N_UserBet" + str;
						if (Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0 && num10 > 0)
						{
							sqlCommand.CommandType = CommandType.Text;
							sqlCommand.CommandText = string.Format("select count(Id) from N_UserZhBet with(nolock) where isstop=1 and Id={0}", num7);
							if (Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0)
							{
								sqlCommand.CommandType = CommandType.Text;
								sqlCommand.CommandText = "select isnull(sum(Total*Times),0) from N_UserBet " + str;
								decimal money = Convert.ToDecimal(string.Concat(sqlCommand.ExecuteScalar()));
								sqlCommand.CommandType = CommandType.Text;
								sqlCommand.CommandText = "update N_UserBet set State=1 " + str;
								sqlCommand.ExecuteNonQuery();
								new UserTotalTran().MoneyOpers(ssId, num.ToString(), money, num2, num3, logSysId, 6, 99, "", "", "终止追号", sTime);
							}
						}
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				new LogExceptionDAL().Save("程序异常", "派奖过程中出现异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		public static bool AdminCancel(int BetId, SqlCommand sqlCommand)
		{
			bool result;
			try
			{
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
				sqlDataAdapter.SelectCommand.CommandText = "select top 1 * From N_UserBet with(nolock)  where Id=" + BetId.ToString();
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					string ssId = dataRow["ssId"].ToString();
					int userId = Convert.ToInt32(dataRow["UserId"]);
					int num = Convert.ToInt32(dataRow["LotteryId"]);
					int num2 = Convert.ToInt32(dataRow["PlayId"]);
					dataRow["IssueNum"].ToString();
					string betDetail = BetDetailDAL.GetBetDetail2(Convert.ToDateTime(dataRow["STime2"]).ToString("yyyyMMdd"), userId.ToString(), BetId.ToString());
					if (string.IsNullOrEmpty(betDetail))
					{
					}
					decimal d = Convert.ToDecimal(dataRow["Total"]);
					Convert.ToDecimal(dataRow["point"]);
					decimal d2 = Convert.ToDecimal(dataRow["PointMoney"]);
					decimal num3 = Convert.ToDecimal(dataRow["Bonus"]);
					decimal d3 = Convert.ToDecimal(dataRow["Times"]);
					Convert.ToDecimal(dataRow["SingleMoney"]);
					dataRow["Pos"].ToString();
					dataRow["PlayCode"].ToString();
					string sTime = dataRow["STime"].ToString();
					Convert.ToInt32(dataRow["IsCheat"]);
					num3 = d * d3 - d2 * d3;
					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.CommandText = "update N_UserBet set State=1,WinNum=0,RealGet=0 where Id=" + BetId.ToString();
					sqlCommand.ExecuteNonQuery();
					if (num3 > 0m)
					{
						new UserTotalTran().MoneyOpers(ssId, userId.ToString(), num3, num, num2, BetId, 6, 99, "", "", "后台撤单", sTime);
					}
					sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
					sqlDataAdapter.SelectCommand.CommandText = "select top 1 UserName,Point from N_User with(nolock)  where Id=" + userId.ToString();
					DataTable dataTable2 = new DataTable();
					sqlDataAdapter.Fill(dataTable2);
					string userName = dataTable2.Rows[0]["UserName"].ToString();
					int userPoint = Convert.ToInt32(dataTable2.Rows[0]["Point"]);
					CheckOperation.AgencyPoint(ssId, userId, userName, userPoint, num, num2, BetId, -Convert.ToDecimal(d * d3), sqlCommand);
				}
				dataTable.Dispose();
				result = true;
			}
			catch (Exception ex)
			{
				new LogExceptionDAL().Save("程序异常", "派奖过程中出现异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		public static bool AdminCancelToNO(int BetId, SqlCommand sqlCommand)
		{
			bool result;
			try
			{
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
				sqlDataAdapter.SelectCommand.CommandText = "select top 1 * From N_UserBet with(nolock)  where Id=" + BetId.ToString();
				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				if (dataTable.Rows.Count > 0)
				{
					DataRow dataRow = dataTable.Rows[0];
					string ssId = dataRow["ssId"].ToString();
					int userId = Convert.ToInt32(dataRow["UserId"]);
					int num = Convert.ToInt32(dataRow["LotteryId"]);
					int num2 = Convert.ToInt32(dataRow["PlayId"]);
					dataRow["IssueNum"].ToString();
					decimal d = Convert.ToDecimal(dataRow["Total"]);
					decimal d2 = Convert.ToDecimal(dataRow["Times"]);
					decimal num3 = Convert.ToDecimal(dataRow["WinBonus"]);
					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.CommandText = "update N_UserBet set State=0,WinNum=0,WinBonus=0,RealGet=0 where Id=" + BetId.ToString();
					sqlCommand.ExecuteNonQuery();
					if (num3 > 0m)
					{
						new UserTotalTran().MoneyOpers(ssId, userId.ToString(), num3, num, num2, BetId, 6, 99, "", "", "撤到未开奖", dataRow["STime"].ToString());
					}
					sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
					sqlDataAdapter.SelectCommand.CommandText = "select top 1 UserName,Point from N_User with(nolock)  where Id=" + userId.ToString();
					DataTable dataTable2 = new DataTable();
					sqlDataAdapter.Fill(dataTable2);
					string userName = dataTable2.Rows[0]["UserName"].ToString();
					int userPoint = Convert.ToInt32(dataTable2.Rows[0]["Point"]);
					CheckOperation.AgencyPoint(ssId, userId, userName, userPoint, num, num2, BetId, -Convert.ToDecimal(d * d2), sqlCommand);
				}
				dataTable.Dispose();
				result = true;
			}
			catch (Exception ex)
			{
				new LogExceptionDAL().Save("程序异常", "派奖过程中出现异常：" + ex.Message);
				result = false;
			}
			return result;
		}

		public static void AgencyPoint(string ssId, int UserId, string UserName, int UserPoint, int LotteryId, int PlayId, int BetId, decimal BetMoney, SqlCommand cmd)
		{
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "select ParentId from N_User with(nolock) where Id=" + UserId.ToString();
			int num = Convert.ToInt32(cmd.ExecuteScalar());
			if (num != 0)
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "select Point from N_User with(nolock) where Id=" + num.ToString();
				object obj = cmd.ExecuteScalar();
				if (!string.IsNullOrEmpty(string.Concat(obj)))
				{
					int num2 = Convert.ToInt32(obj);
					if (num2 < 133 && num2 >= UserPoint)
					{
						decimal money = BetMoney * Convert.ToDecimal(num2 - UserPoint) / 1000m;
						if (Convert.ToDecimal(money.ToString("0.0000")) > 0m)
						{
							new UserTotalTran().MoneyOpers(ssId, num.ToString(), money, LotteryId, PlayId, BetId, 4, 99, "", "", UserName + " 游戏返点", "");
						}
						CheckOperation.AgencyPoint(ssId, num, UserName, num2, LotteryId, PlayId, BetId, BetMoney, cmd);
					}
				}
			}
		}

		public static int UserMoneyStatTran(int UserId, string StatType, decimal StatValue, SqlCommand cmd)
		{
			int result;
			try
			{
				cmd.CommandText = "select Id From N_UserMoneyStatAll with(nolock) where UserId=" + UserId + " and DateDiff(D,STime,getDate())=0";
				int num = Convert.ToInt32(cmd.ExecuteScalar());
				int num2;
				if (num == 0)
				{
					cmd.CommandText = string.Concat(new object[]
					{
						"insert into N_UserMoneyStatAll(UserId,",
						StatType,
						",STime) values (",
						UserId,
						",",
						StatValue,
						",getdate())"
					});
					num2 = cmd.ExecuteNonQuery();
				}
				else
				{
					cmd.CommandText = string.Concat(new object[]
					{
						"update N_UserMoneyStatAll set ",
						StatType,
						"=",
						StatType,
						"+",
						StatValue,
						" where Id=",
						num
					});
					num2 = cmd.ExecuteNonQuery();
				}
				result = num2;
			}
			catch (Exception)
			{
				result = 0;
			}
			return result;
		}

		public static string ReplaceStr(string str)
		{
			return str.Replace("0", "一区").Replace("1", "一区").Replace("2", "二区").Replace("3", "二区").Replace("4", "三区").Replace("5", "三区").Replace("6", "四区").Replace("7", "四区").Replace("8", "五区").Replace("9", "五区");
		}

		public static string ReplaceDX(string str)
		{
			return str.Replace("0", "小").Replace("1", "小").Replace("2", "小").Replace("3", "小").Replace("4", "小").Replace("5", "大").Replace("6", "大").Replace("7", "大").Replace("8", "大").Replace("9", "大");
		}
	}
}
