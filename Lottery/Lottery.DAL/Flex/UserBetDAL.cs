using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class UserBetDAL : ComData
	{
		public UserBetDAL()
		{
			this.site = new conSite().GetSite();
		}

		public string CheckBet(int UserId, int lotteryId, decimal betSumTotal, DateTime STime)
		{
			if (UserId == 0)
			{
				return "投注失败,请重新登录后再进行投注!";
			}
			if (this.site.BetIsOpen == 1)
			{
				return "系统正在维护，不能投注！";
			}
			if (betSumTotal <= 0m)
			{
				return "投注失败,您的帐号余额不足!";
			}
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select * from Sys_Lottery where Id=" + lotteryId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				int num = Convert.ToInt32(dataTable.Rows[0]["IsOpen"]);
				Convert.ToInt32(dataTable.Rows[0]["Ltype"]);
				int num2 = Convert.ToInt32(dataTable.Rows[0]["CloseTime"]);
				Convert.ToDecimal(dataTable.Rows[0]["MinTimes"]);
				Convert.ToDecimal(dataTable.Rows[0]["MaxTimes"]);
				if (num != 0)
				{
					string result = "暂停投注，请与客服联系。";
					return result;
				}
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select DATEDIFF(S,GETDATE(),'" + STime.ToString("yyyy-MM-dd HH:mm:ss") + "') as time,GETDATE() as now";
				DataTable dataTable2 = dbOperHandler.GetDataTable();
				if (Convert.ToDateTime(dataTable2.Rows[0]["now"]) > STime)
				{
					string result = "本期已封单,不能投注!";
					return result;
				}
				int num3 = Convert.ToInt32(dataTable2.Rows[0]["time"]);
				if (num3 <= num2)
				{
					string result = "本期已封单,不能投注!";
					return result;
				}
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select Money,IsEnable,IsBet,IsDelay,Point,EnableSeason from N_User where Id=" + UserId;
				DataTable dataTable3 = dbOperHandler.GetDataTable();
				int num4 = Convert.ToInt32(dataTable3.Rows[0]["IsBet"]);
				int num5 = Convert.ToInt32(dataTable3.Rows[0]["IsEnable"]);
				Convert.ToInt32(dataTable3.Rows[0]["IsDelay"]);
				decimal d = string.IsNullOrEmpty(string.Concat(dataTable3.Rows[0]["Point"])) ? 0m : Convert.ToDecimal(dataTable3.Rows[0]["Point"]);
				decimal d2 = Convert.ToDecimal(dataTable3.Rows[0]["Money"]);
				dataTable3.Rows[0]["EnableSeason"].ToString();
				if (num5 != 0)
				{
					string result = "当前帐号无法投注，请联系客服处理!";
					return result;
				}
				if (num4 != 0)
				{
					string result = "投注失败,您的帐号禁止投注!";
					return result;
				}
				if (d >= this.site.MaxLevel * 10m)
				{
					string result = "当前帐号无法投注，请联系客服处理!";
					return result;
				}
				if (d2 < betSumTotal)
				{
					string result = "投注失败,您的帐号余额不足!";
					return result;
				}
				if (betSumTotal > this.site.MaxBet)
				{
					string result = "系统设置最大投注额不能超过" + this.site.MaxBet + "元！";
					return result;
				}
			}
			return string.Empty;
		}

		public int InsertBet(UserBetModel model, string Source)
		{
			int num = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					string bet = SsId.Bet;
					decimal money = Convert.ToDecimal(model.SingleMoney * model.Num * model.Times);
					if (new UserTotalTran().MoneyOpers(bet, model.UserId.ToString(), money, model.LotteryId, model.PlayId, num, 3, 99, string.Empty, string.Empty, "会员投注", "") <= 0)
					{
						return 0;
					}
					SqlParameter[] values = new SqlParameter[]
					{
						new SqlParameter("@SsId", bet),
						new SqlParameter("@UserId", model.UserId),
						new SqlParameter("@UserMoney", model.UserMoney),
						new SqlParameter("@LotteryId", model.LotteryId),
						new SqlParameter("@PlayId", model.PlayId),
						new SqlParameter("@IssueNum", model.IssueNum),
						new SqlParameter("@SingleMoney", model.SingleMoney),
						new SqlParameter("@Num", model.Num),
						new SqlParameter("@Detail", ""),
						new SqlParameter("@Total", model.SingleMoney * model.Num),
						new SqlParameter("@Point", model.Point),
						new SqlParameter("@PointMoney", model.SingleMoney * model.Num * model.Point / 100m),
						new SqlParameter("@Bonus", model.Bonus),
						new SqlParameter("@Pos", model.Pos),
						new SqlParameter("@PlayCode", model.PlayCode),
						new SqlParameter("@STime", model.STime),
						new SqlParameter("@STime2", model.STime2),
						new SqlParameter("@IsDelay", model.IsDelay),
						new SqlParameter("@Times", model.Times),
						new SqlParameter("@ZhId", "0"),
						new SqlParameter("@Source", Source)
					};
					sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
					SqlCommand expr_2CE = sqlCommand;
					expr_2CE.CommandText += " SELECT SCOPE_IDENTITY()";
					sqlCommand.Parameters.AddRange(values);
					num = Convert.ToInt32(sqlCommand.ExecuteScalar());
					sqlCommand.Parameters.Clear();
					BetDetailDAL.SetBetDetail(model.STime2.ToString("yyyyMMdd"), model.UserId.ToString(), num.ToString(), model.Detail.Replace("|", "#"));
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
					num = 0;
				}
			}
			return num;
		}

		public int InsertBetPos(UserBetModel model, string Source)
		{
			int num = 0;
			int num2 = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					string text = "";
					string[] array = model.Pos.Split(new char[]
					{
						','
					});
					string text2 = model.PlayCode + "_";
					string playCode;
					switch (playCode = model.PlayCode)
					{
					case "R_4FS":
					case "R_4DS":
					case "R_4ZX24":
					case "R_4ZX12":
					case "R_4ZX6":
					case "R_4ZX4":
						if (model.Pos != "")
						{
							int count = Regex.Matches(model.Pos, "1").Count;
							if (count == 4)
							{
								text += text2;
								text += (array[0].Equals("1") ? "W" : "");
								text += (array[1].Equals("1") ? "Q" : "");
								text += (array[2].Equals("1") ? "B" : "");
								text += (array[3].Equals("1") ? "S" : "");
								text += (array[4].Equals("1") ? "G" : "");
								text += ",";
							}
							if (count == 5)
							{
								string[] array2 = "W,Q,B,S,G".Split(new char[]
								{
									','
								});
								for (int i = 0; i < array2.Length; i++)
								{
									for (int j = i + 1; j < array2.Length; j++)
									{
										for (int k = j + 1; k < array2.Length; k++)
										{
											for (int l = k + 1; l < array2.Length; l++)
											{
												string text3 = text;
												text = string.Concat(new string[]
												{
													text3,
													text2,
													array2[i],
													array2[j],
													array2[k],
													array2[l],
													","
												});
											}
										}
									}
								}
							}
						}
						break;
					case "R_3FS":
					case "R_3DS":
					case "R_3Z3":
					case "R_3Z6":
					case "R_3HX":
					case "R_3HE":
					case "R_3ZHE":
					case "R_3KD":
					case "R_3ZBD":
					case "R_3QTWS":
					case "R_3QTTS":
					case "R_3Z3DS":
					case "R_3Z6DS":
						if (model.Pos != "")
						{
							int count2 = Regex.Matches(model.Pos, "1").Count;
							if (count2 == 3)
							{
								text += text2;
								text += (array[0].Equals("1") ? "W" : "");
								text += (array[1].Equals("1") ? "Q" : "");
								text += (array[2].Equals("1") ? "B" : "");
								text += (array[3].Equals("1") ? "S" : "");
								text += (array[4].Equals("1") ? "G" : "");
								text += ",";
							}
							if (count2 >= 4)
							{
								string text4 = "";
								text4 += (array[0].Equals("1") ? "W," : "");
								text4 += (array[1].Equals("1") ? "Q," : "");
								text4 += (array[2].Equals("1") ? "B," : "");
								text4 += (array[3].Equals("1") ? "S," : "");
								text4 += (array[4].Equals("1") ? "G," : "");
								string[] array3 = text4.Substring(0, text4.Length - 1).Split(new char[]
								{
									','
								});
								for (int m = 0; m < array3.Length; m++)
								{
									for (int n = m + 1; n < array3.Length; n++)
									{
										for (int num4 = n + 1; num4 < array3.Length; num4++)
										{
											string text5 = text;
											text = string.Concat(new string[]
											{
												text5,
												text2,
												array3[m],
												array3[n],
												array3[num4],
												","
											});
										}
									}
								}
							}
						}
						break;
					case "R_2FS":
					case "R_2DS":
					case "R_2Z2":
					case "R_2HE":
					case "R_2ZHE":
					case "R_2ZDS":
					case "R_2KD":
					case "R_2ZBD":
						if (model.Pos != "")
						{
							int count3 = Regex.Matches(model.Pos, "1").Count;
							if (count3 == 2)
							{
								text += text2;
								text += (array[0].Equals("1") ? "W" : "");
								text += (array[1].Equals("1") ? "Q" : "");
								text += (array[2].Equals("1") ? "B" : "");
								text += (array[3].Equals("1") ? "S" : "");
								text += (array[4].Equals("1") ? "G" : "");
								text += ",";
							}
							if (count3 >= 3)
							{
								string text6 = "";
								text6 += (array[0].Equals("1") ? "W," : "");
								text6 += (array[1].Equals("1") ? "Q," : "");
								text6 += (array[2].Equals("1") ? "B," : "");
								text6 += (array[3].Equals("1") ? "S," : "");
								text6 += (array[4].Equals("1") ? "G," : "");
								string[] array4 = text6.Substring(0, text6.Length - 1).Split(new char[]
								{
									','
								});
								for (int num5 = 0; num5 < array4.Length; num5++)
								{
									for (int num6 = num5 + 1; num6 < array4.Length; num6++)
									{
										string text7 = text;
										text = string.Concat(new string[]
										{
											text7,
											text2,
											array4[num5],
											array4[num6],
											","
										});
									}
								}
							}
						}
						break;
					}
					text = text.Substring(0, text.Length - 1);
					string[] array5 = text.Split(new char[]
					{
						','
					});
					for (int num7 = 0; num7 < array5.Length; num7++)
					{
						string bet = SsId.Bet;
						decimal money = Convert.ToDecimal(model.SingleMoney * model.Num * model.Times / array5.Length);
						if (new UserTotalTran().MoneyOpers(bet, model.UserId.ToString(), money, model.LotteryId, model.PlayId, num, 3, 99, string.Empty, string.Empty, "会员投注", "") > 0)
						{
							SqlParameter[] values = new SqlParameter[]
							{
								new SqlParameter("@SsId", bet),
								new SqlParameter("@UserId", model.UserId),
								new SqlParameter("@UserMoney", model.UserMoney),
								new SqlParameter("@LotteryId", model.LotteryId),
								new SqlParameter("@PlayId", model.PlayId),
								new SqlParameter("@IssueNum", model.IssueNum),
								new SqlParameter("@SingleMoney", model.SingleMoney),
								new SqlParameter("@Num", model.Num / array5.Length),
								new SqlParameter("@Detail", ""),
								new SqlParameter("@Total", model.SingleMoney * model.Num / array5.Length),
								new SqlParameter("@Point", model.Point),
								new SqlParameter("@PointMoney", model.SingleMoney * model.Num * model.Point / array5.Length / 100m),
								new SqlParameter("@Bonus", model.Bonus),
								new SqlParameter("@Pos", ""),
								new SqlParameter("@PlayCode", array5[num7]),
								new SqlParameter("@STime", model.STime),
								new SqlParameter("@STime2", model.STime2),
								new SqlParameter("@IsDelay", model.IsDelay),
								new SqlParameter("@Times", model.Times),
								new SqlParameter("@ZhId", "0"),
								new SqlParameter("@Source", Source)
							};
							sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
							SqlCommand expr_C2D = sqlCommand;
							expr_C2D.CommandText += " SELECT SCOPE_IDENTITY()";
							sqlCommand.Parameters.AddRange(values);
							num = Convert.ToInt32(sqlCommand.ExecuteScalar());
							sqlCommand.Parameters.Clear();
							num2++;
							BetDetailDAL.SetBetDetail(model.STime2.ToString("yyyyMMdd"), model.UserId.ToString(), num.ToString(), model.Detail.Replace("|", "#"));
						}
					}
					if (num2 >= array5.Length)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
					num = 0;
				}
			}
			return num;
		}

		public int InsertBetZH(UserBetModel model, string Source)
		{
			int num = 0;
			int num2 = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					string text = "";
					string text2 = "";
					string text3 = "";
					if (model.PlayCode == "P_5ZH")
					{
						text = "P_5ZH_WQBSG,P_5ZH_QBSG,P_5ZH_BSG,P_5ZH_SG,P_5ZH_G";
						text2 = "1,10,100,1000,10000";
						string text4 = model.Detail.Replace("_", "");
						string[] array = text4.Split(new char[]
						{
							','
						});
						int num3 = 1;
						int num4 = 1;
						int num5 = 1;
						int num6 = 1;
						int num7 = 1;
						for (int i = 0; i < array.Length; i++)
						{
							num3 *= array[i].Length;
						}
						for (int i = 1; i < array.Length; i++)
						{
							num4 *= array[i].Length;
						}
						for (int i = 2; i < array.Length; i++)
						{
							num5 *= array[i].Length;
						}
						for (int i = 3; i < array.Length; i++)
						{
							num6 *= array[i].Length;
						}
						for (int i = 4; i < array.Length; i++)
						{
							num7 *= array[i].Length;
						}
						text3 = string.Concat(new object[]
						{
							num3,
							",",
							num4,
							",",
							num5,
							",",
							num6,
							",",
							num7
						});
					}
					if (model.PlayCode == "P_4ZH_L")
					{
						text = "P_4ZH_L_WQBS,P_4ZH_L_QBS,P_4ZH_L_BS,P_4ZH_L_S";
						text2 = "1,10,100,1000";
						string text5 = model.Detail.Replace("_", "");
						string[] array2 = text5.Split(new char[]
						{
							','
						});
						int num8 = 1;
						int num9 = 1;
						int num10 = 1;
						int num11 = 1;
						for (int j = 0; j < array2.Length; j++)
						{
							num8 *= array2[j].Length;
						}
						for (int j = 1; j < array2.Length; j++)
						{
							num9 *= array2[j].Length;
						}
						for (int j = 2; j < array2.Length; j++)
						{
							num10 *= array2[j].Length;
						}
						for (int j = 3; j < array2.Length; j++)
						{
							num11 *= array2[j].Length;
						}
						text3 = string.Concat(new object[]
						{
							num8,
							",",
							num9,
							",",
							num10,
							",",
							num11
						});
					}
					if (model.PlayCode == "P_4ZH_R")
					{
						text = "P_4ZH_R_QBSG,P_4ZH_R_BSG,P_4ZH_R_SG,P_4ZH_R_G";
						text2 = "1,10,100,1000";
						string text6 = model.Detail.Replace("_", "");
						string[] array3 = text6.Split(new char[]
						{
							','
						});
						int num12 = 1;
						int num13 = 1;
						int num14 = 1;
						int num15 = 1;
						for (int k = 0; k < array3.Length; k++)
						{
							num12 *= array3[k].Length;
						}
						for (int k = 1; k < array3.Length; k++)
						{
							num13 *= array3[k].Length;
						}
						for (int k = 2; k < array3.Length; k++)
						{
							num14 *= array3[k].Length;
						}
						for (int k = 3; k < array3.Length; k++)
						{
							num15 *= array3[k].Length;
						}
						text3 = string.Concat(new object[]
						{
							num12,
							",",
							num13,
							",",
							num14,
							",",
							num15
						});
					}
					if (model.PlayCode == "P_3ZH_L")
					{
						text = "P_3ZH_L_WQB,P_3ZH_L_QB,P_3ZH_L_B";
						text2 = "1,10,100";
						string text7 = model.Detail.Replace("_", "");
						string[] array4 = text7.Split(new char[]
						{
							','
						});
						int num16 = 1;
						int num17 = 1;
						int num18 = 1;
						for (int l = 0; l < array4.Length; l++)
						{
							num16 *= array4[l].Length;
						}
						for (int l = 1; l < array4.Length; l++)
						{
							num17 *= array4[l].Length;
						}
						for (int l = 2; l < array4.Length; l++)
						{
							num18 *= array4[l].Length;
						}
						text3 = string.Concat(new object[]
						{
							num16,
							",",
							num17,
							",",
							num18
						});
					}
					if (model.PlayCode == "P_3ZH_C")
					{
						text = "P_3ZH_C_QBS,P_3ZH_C_BS,P_3ZH_C_S";
						text2 = "1,10,100";
						string text8 = model.Detail.Replace("_", "");
						string[] array5 = text8.Split(new char[]
						{
							','
						});
						int num19 = 1;
						int num20 = 1;
						int num21 = 1;
						for (int m = 0; m < array5.Length; m++)
						{
							num19 *= array5[m].Length;
						}
						for (int m = 1; m < array5.Length; m++)
						{
							num20 *= array5[m].Length;
						}
						for (int m = 2; m < array5.Length; m++)
						{
							num21 *= array5[m].Length;
						}
						text3 = string.Concat(new object[]
						{
							num19,
							",",
							num20,
							",",
							num21
						});
					}
					if (model.PlayCode == "P_3ZH_R")
					{
						text = "P_3ZH_R_BSG,P_3ZH_R_SG,P_3ZH_R_G";
						text2 = "1,10,100";
						string text9 = model.Detail.Replace("_", "");
						string[] array6 = text9.Split(new char[]
						{
							','
						});
						int num22 = 1;
						int num23 = 1;
						int num24 = 1;
						for (int n = 0; n < array6.Length; n++)
						{
							num22 *= array6[n].Length;
						}
						for (int n = 1; n < array6.Length; n++)
						{
							num23 *= array6[n].Length;
						}
						for (int n = 2; n < array6.Length; n++)
						{
							num24 *= array6[n].Length;
						}
						text3 = string.Concat(new object[]
						{
							num22,
							",",
							num23,
							",",
							num24
						});
					}
					string[] array7 = text.Split(new char[]
					{
						','
					});
					string[] array8 = text2.Split(new char[]
					{
						','
					});
					string[] array9 = text3.Split(new char[]
					{
						','
					});
					for (int num25 = 0; num25 < array7.Length; num25++)
					{
						if (Convert.ToInt32(array9[num25]) > 0)
						{
							string bet = SsId.Bet;
							decimal money = Convert.ToDecimal(model.SingleMoney * Convert.ToInt32(array9[num25]) * model.Times);
							if (new UserTotalTran().MoneyOpers(bet, model.UserId.ToString(), money, model.LotteryId, model.PlayId, num, 3, 99, string.Empty, string.Empty, "会员投注", "") > 0)
							{
								SqlParameter[] values = new SqlParameter[]
								{
									new SqlParameter("@SsId", bet),
									new SqlParameter("@UserId", model.UserId),
									new SqlParameter("@UserMoney", model.UserMoney),
									new SqlParameter("@LotteryId", model.LotteryId),
									new SqlParameter("@PlayId", model.PlayId),
									new SqlParameter("@IssueNum", model.IssueNum),
									new SqlParameter("@SingleMoney", model.SingleMoney),
									new SqlParameter("@Num", Convert.ToInt32(array9[num25])),
									new SqlParameter("@Detail", ""),
									new SqlParameter("@Total", model.SingleMoney * Convert.ToInt32(array9[num25])),
									new SqlParameter("@Point", model.Point),
									new SqlParameter("@PointMoney", model.SingleMoney * Convert.ToInt32(array9[num25]) * model.Point / 100m),
									new SqlParameter("@Bonus", model.Bonus / Convert.ToInt32(array8[num25])),
									new SqlParameter("@Pos", ""),
									new SqlParameter("@PlayCode", array7[num25]),
									new SqlParameter("@STime", model.STime),
									new SqlParameter("@STime2", model.STime2),
									new SqlParameter("@IsDelay", model.IsDelay),
									new SqlParameter("@Times", model.Times),
									new SqlParameter("@ZhId", "0"),
									new SqlParameter("@Source", Source)
								};
								sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
								SqlCommand expr_A88 = sqlCommand;
								expr_A88.CommandText += " SELECT SCOPE_IDENTITY()";
								sqlCommand.Parameters.AddRange(values);
								num = Convert.ToInt32(sqlCommand.ExecuteScalar());
								sqlCommand.Parameters.Clear();
								num2++;
								BetDetailDAL.SetBetDetail(model.STime2.ToString("yyyyMMdd"), model.UserId.ToString(), num.ToString(), model.Detail.Replace("|", "#"));
							}
						}
					}
					if (num2 > 0)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
					num = 0;
				}
			}
			return num;
		}

        public int InsertZhBet(UserZhBetModel zhmodel, List<UserZhDetailModel> listZh, decimal money, string Source)
        {
            int num;
            int userId;
            char[] chrArray;
            object[] objArray;
            string str;
            string[] strArrays;
            int num1 = 0;
            if (listZh.Count > 0)
            {
                using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand()
                    {
                        Connection = sqlConnection
                    };
                    try
                    {
                        string zBet = SsId.ZBet;
                        if ((new UserTotalTran()).MoneyOpers(zBet, zhmodel.UserId.ToString(), money, 0, 0, 0, 3, 99, string.Empty, string.Empty, "会员追号", "") <= 0)
                        {
                            num = 0;
                            return num;
                        }
                        else
                        {
                            SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("@SsId", zBet), new SqlParameter("@UserId", (object)zhmodel.UserId), new SqlParameter("@LotteryId", (object)zhmodel.LotteryId), new SqlParameter("@PlayId", (object)zhmodel.PlayId), new SqlParameter("@StartIssueNum", zhmodel.StartIssueNum), new SqlParameter("@TotalNums", (object)zhmodel.TotalNums), new SqlParameter("@TotalSums", (object)zhmodel.TotalSums), new SqlParameter("@IsStop", (object)zhmodel.IsStop), new SqlParameter("@STime", (object)zhmodel.STime) };
                            SqlParameter[] sqlParameterArray = sqlParameter;
                            sqlCommand.CommandText = "INSERT INTO N_UserZhBet (SsId,UserId ,LotteryId ,PlayId ,StartIssueNum ,TotalNums ,TotalSums ,IsStop ,STime)\r\n                                        values(@SsId,@UserId,@LotteryId,@PlayId,@StartIssueNum,@TotalNums,@TotalSums,@IsStop,@STime)";
                            SqlCommand sqlCommand1 = sqlCommand;
                            sqlCommand1.CommandText = string.Concat(sqlCommand1.CommandText, " SELECT SCOPE_IDENTITY()");
                            sqlCommand.Parameters.AddRange(sqlParameterArray);
                            int num2 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                            sqlCommand.Parameters.Clear();
                            foreach (UserZhDetailModel userZhDetailModel in listZh)
                            {
                                UserBetModel item = userZhDetailModel.Lists[0];
                                if (!item.Pos.Equals(""))
                                {
                                    string str1 = "";
                                    string pos = item.Pos;
                                    chrArray = new char[] { ',' };
                                    string[] strArrays1 = pos.Split(chrArray);
                                    if (item.PlayCode == "R_4FS" || item.PlayCode == "R_4DS")
                                    {
                                        string str2 = string.Concat(item.PlayCode, "_");
                                        if (item.Pos != "")
                                        {
                                            int count = Regex.Matches(item.Pos, "1").Count;
                                            if (count == 4)
                                            {
                                                str1 = string.Concat(str1, str2);
                                                str1 = string.Concat(str1, (strArrays1[0].Equals("1") ? "W" : ""));
                                                str1 = string.Concat(str1, (strArrays1[1].Equals("1") ? "Q" : ""));
                                                str1 = string.Concat(str1, (strArrays1[2].Equals("1") ? "B" : ""));
                                                str1 = string.Concat(str1, (strArrays1[3].Equals("1") ? "S" : ""));
                                                str1 = string.Concat(str1, (strArrays1[4].Equals("1") ? "G" : ""));
                                                str1 = string.Concat(str1, ",");
                                            }
                                            if (count == 5)
                                            {
                                                chrArray = new char[] { ',' };
                                                string[] strArrays2 = "W,Q,B,S,G".Split(chrArray);
                                                for (int i = 0; i < (int)strArrays2.Length; i++)
                                                {
                                                    for (int j = i + 1; j < (int)strArrays2.Length; j++)
                                                    {
                                                        for (int k = j + 1; k < (int)strArrays2.Length; k++)
                                                        {
                                                            for (int l = k + 1; l < (int)strArrays2.Length; l++)
                                                            {
                                                                str = str1;
                                                                strArrays = new string[] { str, str2, strArrays2[i], strArrays2[j], strArrays2[k], strArrays2[l], "," };
                                                                str1 = string.Concat(strArrays);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (item.PlayCode == "R_3FS" || item.PlayCode == "R_3DS" || item.PlayCode == "R_3Z3" || item.PlayCode == "R_3Z6" || item.PlayCode == "R_3HX")
                                    {
                                        string str3 = string.Concat(item.PlayCode, "_");
                                        if (item.Pos != "")
                                        {
                                            int count1 = Regex.Matches(item.Pos, "1").Count;
                                            if (count1 == 3)
                                            {
                                                str1 = string.Concat(str1, str3);
                                                str1 = string.Concat(str1, (strArrays1[0].Equals("1") ? "W" : ""));
                                                str1 = string.Concat(str1, (strArrays1[1].Equals("1") ? "Q" : ""));
                                                str1 = string.Concat(str1, (strArrays1[2].Equals("1") ? "B" : ""));
                                                str1 = string.Concat(str1, (strArrays1[3].Equals("1") ? "S" : ""));
                                                str1 = string.Concat(str1, (strArrays1[4].Equals("1") ? "G" : ""));
                                                str1 = string.Concat(str1, ",");
                                            }
                                            if (count1 >= 4)
                                            {
                                                string str4 = "";
                                                str4 = string.Concat(str4, (strArrays1[0].Equals("1") ? "W," : ""));
                                                str4 = string.Concat(str4, (strArrays1[1].Equals("1") ? "Q," : ""));
                                                str4 = string.Concat(str4, (strArrays1[2].Equals("1") ? "B," : ""));
                                                str4 = string.Concat(str4, (strArrays1[3].Equals("1") ? "S," : ""));
                                                str4 = string.Concat(str4, (strArrays1[4].Equals("1") ? "G," : ""));
                                                string str5 = str4.Substring(0, str4.Length - 1);
                                                chrArray = new char[] { ',' };
                                                string[] strArrays3 = str5.Split(chrArray);
                                                for (int m = 0; m < (int)strArrays3.Length; m++)
                                                {
                                                    for (int n = m + 1; n < (int)strArrays3.Length; n++)
                                                    {
                                                        for (int o = n + 1; o < (int)strArrays3.Length; o++)
                                                        {
                                                            str = str1;
                                                            strArrays = new string[] { str, str3, strArrays3[m], strArrays3[n], strArrays3[o], "," };
                                                            str1 = string.Concat(strArrays);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (item.PlayCode == "R_2FS" || item.PlayCode == "R_2DS" || item.PlayCode == "R_2Z2")
                                    {
                                        string str6 = string.Concat(item.PlayCode, "_");
                                        if (item.Pos != "")
                                        {
                                            int count2 = Regex.Matches(item.Pos, "1").Count;
                                            if (count2 == 2)
                                            {
                                                str1 = string.Concat(str1, str6);
                                                str1 = string.Concat(str1, (strArrays1[0].Equals("1") ? "W" : ""));
                                                str1 = string.Concat(str1, (strArrays1[1].Equals("1") ? "Q" : ""));
                                                str1 = string.Concat(str1, (strArrays1[2].Equals("1") ? "B" : ""));
                                                str1 = string.Concat(str1, (strArrays1[3].Equals("1") ? "S" : ""));
                                                str1 = string.Concat(str1, (strArrays1[4].Equals("1") ? "G" : ""));
                                                str1 = string.Concat(str1, ",");
                                            }
                                            if (count2 >= 3)
                                            {
                                                string str7 = "";
                                                str7 = string.Concat(str7, (strArrays1[0].Equals("1") ? "W," : ""));
                                                str7 = string.Concat(str7, (strArrays1[1].Equals("1") ? "Q," : ""));
                                                str7 = string.Concat(str7, (strArrays1[2].Equals("1") ? "B," : ""));
                                                str7 = string.Concat(str7, (strArrays1[3].Equals("1") ? "S," : ""));
                                                str7 = string.Concat(str7, (strArrays1[4].Equals("1") ? "G," : ""));
                                                string str8 = str7.Substring(0, str7.Length - 1);
                                                chrArray = new char[] { ',' };
                                                string[] strArrays4 = str8.Split(chrArray);
                                                for (int p = 0; p < (int)strArrays4.Length; p++)
                                                {
                                                    for (int q = p + 1; q < (int)strArrays4.Length; q++)
                                                    {
                                                        str = str1;
                                                        strArrays = new string[] { str, str6, strArrays4[p], strArrays4[q], "," };
                                                        str1 = string.Concat(strArrays);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    str1 = str1.Substring(0, str1.Length - 1);
                                    chrArray = new char[] { ',' };
                                    string[] strArrays5 = str1.Split(chrArray);
                                    for (int r = 0; r < (int)strArrays5.Length; r++)
                                    {
                                        sqlParameter = new SqlParameter[] { new SqlParameter("@SsId", SsId.Bet), new SqlParameter("@UserId", (object)item.UserId), new SqlParameter("@UserMoney", (object)item.UserMoney), new SqlParameter("@LotteryId", (object)item.LotteryId), new SqlParameter("@PlayId", (object)item.PlayId), new SqlParameter("@IssueNum", userZhDetailModel.IssueNum), new SqlParameter("@SingleMoney", (object)item.SingleMoney), new SqlParameter("@Num", (object)(item.Num / (int)strArrays5.Length)), new SqlParameter("@Detail", ""), new SqlParameter("@Total", (object)((item.SingleMoney * item.Num) / (int)strArrays5.Length)), new SqlParameter("@Point", (object)item.Point), new SqlParameter("@PointMoney", (object)((((item.SingleMoney * item.Num) * item.Point) / (int)strArrays5.Length) / new decimal(100))), new SqlParameter("@Bonus", (object)item.Bonus), new SqlParameter("@Pos", ""), new SqlParameter("@PlayCode", strArrays5[r]), new SqlParameter("@STime", (object)userZhDetailModel.STime), new SqlParameter("@STime2", (object)item.STime2), new SqlParameter("@IsDelay", (object)item.IsDelay), new SqlParameter("@Times", (object)userZhDetailModel.Times), new SqlParameter("@ZhId", (object)num2), new SqlParameter("@Source", Source) };
                                        sqlParameterArray = sqlParameter;
                                        sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
                                        SqlCommand sqlCommand2 = sqlCommand;
                                        sqlCommand2.CommandText = string.Concat(sqlCommand2.CommandText, " SELECT SCOPE_IDENTITY()");
                                        sqlCommand.Parameters.AddRange(sqlParameterArray);
                                        num1 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                                        sqlCommand.Parameters.Clear();
                                        string str9 = item.STime2.ToString("yyyyMMdd");
                                        userId = item.UserId;
                                        BetDetailDAL.SetBetDetail(str9, userId.ToString(), num1.ToString(), item.Detail.Replace("|", "#"));
                                    }
                                }
                                else if (item.PlayCode.Equals("P_5ZH") || item.PlayCode.Equals("P_4ZH_L") || item.PlayCode.Equals("P_4ZH_R") || item.PlayCode.Equals("P_3ZH_L") || item.PlayCode.Equals("P_3ZH_C") || item.PlayCode.Equals("P_3ZH_R"))
                                {
                                    string str10 = "";
                                    string str11 = "";
                                    string str12 = "";
                                    if (item.PlayCode == "P_5ZH")
                                    {
                                        str10 = "P_5ZH_WQBSG,P_5ZH_QBSG,P_5ZH_BSG,P_5ZH_SG,P_5ZH_G";
                                        str11 = "1,10,100,1000,10000";
                                        string str13 = item.Detail.Replace("_", "");
                                        chrArray = new char[] { ',' };
                                        string[] strArrays6 = str13.Split(chrArray);
                                        int length = 1;
                                        int length1 = 1;
                                        int length2 = 1;
                                        int length3 = 1;
                                        int num3 = 1;
                                        int s = 0;
                                        for (s = 0; s < (int)strArrays6.Length; s++)
                                        {
                                            length *= strArrays6[s].Length;
                                        }
                                        for (s = 1; s < (int)strArrays6.Length; s++)
                                        {
                                            length1 *= strArrays6[s].Length;
                                        }
                                        for (s = 2; s < (int)strArrays6.Length; s++)
                                        {
                                            length2 *= strArrays6[s].Length;
                                        }
                                        for (s = 3; s < (int)strArrays6.Length; s++)
                                        {
                                            length3 *= strArrays6[s].Length;
                                        }
                                        for (s = 4; s < (int)strArrays6.Length; s++)
                                        {
                                            num3 *= strArrays6[s].Length;
                                        }
                                        objArray = new object[] { length, ",", length1, ",", length2, ",", length3, ",", num3 };
                                        str12 = string.Concat(objArray);
                                    }
                                    if (item.PlayCode == "P_4ZH_L")
                                    {
                                        str10 = "P_4ZH_L_WQBS,P_4ZH_L_QBS,P_4ZH_L_BS,P_4ZH_L_S";
                                        str11 = "1,10,100,1000";
                                        string str14 = item.Detail.Replace("_", "");
                                        chrArray = new char[] { ',' };
                                        string[] strArrays7 = str14.Split(chrArray);
                                        int length4 = 1;
                                        int num4 = 1;
                                        int length5 = 1;
                                        int num5 = 1;
                                        int t = 0;
                                        for (t = 0; t < (int)strArrays7.Length; t++)
                                        {
                                            length4 *= strArrays7[t].Length;
                                        }
                                        for (t = 1; t < (int)strArrays7.Length; t++)
                                        {
                                            num4 *= strArrays7[t].Length;
                                        }
                                        for (t = 2; t < (int)strArrays7.Length; t++)
                                        {
                                            length5 *= strArrays7[t].Length;
                                        }
                                        for (t = 3; t < (int)strArrays7.Length; t++)
                                        {
                                            num5 *= strArrays7[t].Length;
                                        }
                                        objArray = new object[] { length4, ",", num4, ",", length5, ",", num5 };
                                        str12 = string.Concat(objArray);
                                    }
                                    if (item.PlayCode == "P_4ZH_R")
                                    {
                                        str10 = "P_4ZH_R_QBSG,P_4ZH_R_BSG,P_4ZH_R_SG,P_4ZH_R_G";
                                        str11 = "1,10,100,1000";
                                        string str15 = item.Detail.Replace("_", "");
                                        chrArray = new char[] { ',' };
                                        string[] strArrays8 = str15.Split(chrArray);
                                        int length6 = 1;
                                        int num6 = 1;
                                        int length7 = 1;
                                        int num7 = 1;
                                        int u = 0;
                                        for (u = 0; u < (int)strArrays8.Length; u++)
                                        {
                                            length6 *= strArrays8[u].Length;
                                        }
                                        for (u = 1; u < (int)strArrays8.Length; u++)
                                        {
                                            num6 *= strArrays8[u].Length;
                                        }
                                        for (u = 2; u < (int)strArrays8.Length; u++)
                                        {
                                            length7 *= strArrays8[u].Length;
                                        }
                                        for (u = 3; u < (int)strArrays8.Length; u++)
                                        {
                                            num7 *= strArrays8[u].Length;
                                        }
                                        objArray = new object[] { length6, ",", num6, ",", length7, ",", num7 };
                                        str12 = string.Concat(objArray);
                                    }
                                    if (item.PlayCode == "P_3ZH_L")
                                    {
                                        str10 = "P_3ZH_L_WQB,P_3ZH_L_QB,P_3ZH_L_B";
                                        str11 = "1,10,100";
                                        string str16 = item.Detail.Replace("_", "");
                                        chrArray = new char[] { ',' };
                                        string[] strArrays9 = str16.Split(chrArray);
                                        int length8 = 1;
                                        int num8 = 1;
                                        int length9 = 1;
                                        int v = 0;
                                        for (v = 0; v < (int)strArrays9.Length; v++)
                                        {
                                            length8 *= strArrays9[v].Length;
                                        }
                                        for (v = 1; v < (int)strArrays9.Length; v++)
                                        {
                                            num8 *= strArrays9[v].Length;
                                        }
                                        for (v = 2; v < (int)strArrays9.Length; v++)
                                        {
                                            length9 *= strArrays9[v].Length;
                                        }
                                        objArray = new object[] { length8, ",", num8, ",", length9 };
                                        str12 = string.Concat(objArray);
                                    }
                                    if (item.PlayCode == "P_3ZH_C")
                                    {
                                        str10 = "P_3ZH_C_QBS,P_3ZH_C_BS,P_3ZH_C_S";
                                        str11 = "1,10,100";
                                        string str17 = item.Detail.Replace("_", "");
                                        chrArray = new char[] { ',' };
                                        string[] strArrays10 = str17.Split(chrArray);
                                        int num9 = 1;
                                        int length10 = 1;
                                        int num10 = 1;
                                        int w = 0;
                                        for (w = 0; w < (int)strArrays10.Length; w++)
                                        {
                                            num9 *= strArrays10[w].Length;
                                        }
                                        for (w = 1; w < (int)strArrays10.Length; w++)
                                        {
                                            length10 *= strArrays10[w].Length;
                                        }
                                        for (w = 2; w < (int)strArrays10.Length; w++)
                                        {
                                            num10 *= strArrays10[w].Length;
                                        }
                                        objArray = new object[] { num9, ",", length10, ",", num10 };
                                        str12 = string.Concat(objArray);
                                    }
                                    if (item.PlayCode == "P_3ZH_R")
                                    {
                                        str10 = "P_3ZH_R_BSG,P_3ZH_R_SG,P_3ZH_R_G";
                                        str11 = "1,10,100";
                                        string str18 = item.Detail.Replace("_", "");
                                        chrArray = new char[] { ',' };
                                        string[] strArrays11 = str18.Split(chrArray);
                                        int length11 = 1;
                                        int num11 = 1;
                                        int length12 = 1;
                                        int x = 0;
                                        for (x = 0; x < (int)strArrays11.Length; x++)
                                        {
                                            length11 *= strArrays11[x].Length;
                                        }
                                        for (x = 1; x < (int)strArrays11.Length; x++)
                                        {
                                            num11 *= strArrays11[x].Length;
                                        }
                                        for (x = 2; x < (int)strArrays11.Length; x++)
                                        {
                                            length12 *= strArrays11[x].Length;
                                        }
                                        objArray = new object[] { length11, ",", num11, ",", length12 };
                                        str12 = string.Concat(objArray);
                                    }
                                    chrArray = new char[] { ',' };
                                    string[] strArrays12 = str10.Split(chrArray);
                                    chrArray = new char[] { ',' };
                                    string[] strArrays13 = str11.Split(chrArray);
                                    chrArray = new char[] { ',' };
                                    string[] strArrays14 = str12.Split(chrArray);
                                    for (int y = 0; y < (int)strArrays12.Length; y++)
                                    {
                                        if (Convert.ToInt32(strArrays14[y]) > 0)
                                        {
                                            sqlParameter = new SqlParameter[] { new SqlParameter("@SsId", SsId.Bet), new SqlParameter("@UserId", (object)item.UserId), new SqlParameter("@UserMoney", (object)item.UserMoney), new SqlParameter("@LotteryId", (object)item.LotteryId), new SqlParameter("@PlayId", (object)item.PlayId), new SqlParameter("@IssueNum", userZhDetailModel.IssueNum), new SqlParameter("@SingleMoney", (object)item.SingleMoney), new SqlParameter("@Num", (object)Convert.ToInt32(strArrays14[y])), new SqlParameter("@Detail", ""), new SqlParameter("@Total", (object)(item.SingleMoney * Convert.ToInt32(strArrays14[y]))), new SqlParameter("@Point", (object)item.Point), new SqlParameter("@PointMoney", (object)(((item.SingleMoney * Convert.ToInt32(strArrays14[y])) * item.Point) / new decimal(100))), new SqlParameter("@Bonus", (object)(item.Bonus / Convert.ToInt32(strArrays13[y]))), new SqlParameter("@Pos", item.Pos), new SqlParameter("@PlayCode", item.PlayCode), new SqlParameter("@STime", (object)userZhDetailModel.STime), new SqlParameter("@STime2", (object)item.STime2), new SqlParameter("@IsDelay", (object)item.IsDelay), new SqlParameter("@Times", (object)userZhDetailModel.Times), new SqlParameter("@ZhId", (object)num2), new SqlParameter("@Source", Source) };
                                            sqlParameterArray = sqlParameter;
                                            sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                                                    ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                                                    values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                                                    ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
                                            SqlCommand sqlCommand3 = sqlCommand;
                                            sqlCommand3.CommandText = string.Concat(sqlCommand3.CommandText, " SELECT SCOPE_IDENTITY()");
                                            sqlCommand.Parameters.AddRange(sqlParameterArray);
                                            num1 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                                            sqlCommand.Parameters.Clear();
                                            string str19 = item.STime2.ToString("yyyyMMdd");
                                            userId = item.UserId;
                                            BetDetailDAL.SetBetDetail(str19, userId.ToString(), num1.ToString(), item.Detail.Replace("|", "#"));
                                        }
                                    }
                                }
                                else
                                {
                                    sqlParameter = new SqlParameter[] { new SqlParameter("@SsId", SsId.Bet), new SqlParameter("@UserId", (object)item.UserId), new SqlParameter("@UserMoney", (object)item.UserMoney), new SqlParameter("@LotteryId", (object)item.LotteryId), new SqlParameter("@PlayId", (object)item.PlayId), new SqlParameter("@IssueNum", userZhDetailModel.IssueNum), new SqlParameter("@SingleMoney", (object)item.SingleMoney), new SqlParameter("@Num", (object)item.Num), new SqlParameter("@Detail", ""), new SqlParameter("@Total", (object)(item.SingleMoney * item.Num)), new SqlParameter("@Point", (object)item.Point), new SqlParameter("@PointMoney", (object)(((item.SingleMoney * item.Num) * item.Point) / new decimal(100))), new SqlParameter("@Bonus", (object)item.Bonus), new SqlParameter("@Pos", item.Pos), new SqlParameter("@PlayCode", item.PlayCode), new SqlParameter("@STime", (object)userZhDetailModel.STime), new SqlParameter("@STime2", (object)item.STime2), new SqlParameter("@IsDelay", (object)item.IsDelay), new SqlParameter("@Times", (object)userZhDetailModel.Times), new SqlParameter("@ZhId", (object)num2), new SqlParameter("@Source", Source) };
                                    sqlParameterArray = sqlParameter;
                                    sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
                                    SqlCommand sqlCommand4 = sqlCommand;
                                    sqlCommand4.CommandText = string.Concat(sqlCommand4.CommandText, " SELECT SCOPE_IDENTITY()");
                                    sqlCommand.Parameters.AddRange(sqlParameterArray);
                                    num1 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                                    sqlCommand.Parameters.Clear();
                                    string str20 = item.STime2.ToString("yyyyMMdd");
                                    userId = item.UserId;
                                    BetDetailDAL.SetBetDetail(str20, userId.ToString(), num1.ToString(), item.Detail.Replace("|", "#"));
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        (new LogExceptionDAL()).Save("系统异常", exception.Message);
                        num1 = 0;
                    }
                    return num1;
                }
            }
            return num1;
        }

        public void GetPlayListJSON(int lotteryId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				string sqlCmd = "SELECT [Id],[Title] FROM [Sys_PlaySmallType] where flag=0 and lotteryId=" + lotteryId;
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sqlCmd;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetListJSON(int page, int PSize, string whereStr, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = whereStr;
				int num = dbOperHandler.Count("Flex_UserBet");
				string sql = SqlHelp.GetSql0(num + " as totalcount,row_number() over (order by Id desc) as rowid,Id,SsId,LotteryName+'-'+PlayName as LName,UserId,UserName,PlayId,PlayName,PlayCode,LotteryId,LotteryName,IssueNum,SingleMoney,moshi,Times,Num,cast(Times*Total as decimal(15,4)) as Total,Point,PointMoney,Bonus,Bonus2,WinNum,WinBonus,RealGet,Pos,STime,STime2,state,case state when 0 then '未开奖' when 1 then '已撤单' when 2 then '未中奖' when 3 then '已中奖' end as stateName,substring(Convert(varchar(20),STime2,120),6,11) as ShortTime,number", "Flex_UserBet", "Id", PSize, page, "desc", whereStr);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetListJSONById(string BetId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select Id,SsId,UserId,UserMoney,LotteryId,PlayId,PlayCode,IssueNum,Number,SingleMoney,Times,Num,Detail,DX,DS,Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime2,STime2 as STime,IsOpen,State,IsDelay,IsWin,STime9,IsCheat,ZhId,Source,case [SingleMoney] when '2.00' then '元' when '0.20' then '角' when '0.02' then '分' when '0.002' then '厘' end moshi,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetPlayName(PlayId) as PlayName,dbo.f_GetLotteryName(LotteryId) as LotteryName,cast(Times*Total as decimal(15,4)) as BetMoney,case state when 0 then '未开奖' when 1 then '已撤单' when 2 then '未中奖' when 3 then '已中奖' end as stateName,isnull(number,'未开奖不计算') as kjnumber,ZhId\r\n                                from N_UserBet where Id=" + BetId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetListZhJSON(int page, int PSize, string whereStr, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = whereStr;
				int num = dbOperHandler.Count("Flex_UserBetZh");
				string sql = SqlHelp.GetSql0(num + " as totalcount,row_number() over (order by Id desc) as rowid,*", "Flex_UserBetZh", "Id", PSize, page, "desc", whereStr);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string BetCancel(string betId)
		{
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandText = "select * From N_UserBet where state=0 and Id=" + betId;
					DataTable dataTable = new DataTable("N_UserBet");
					sqlDataAdapter.Fill(dataTable);
					string result;
					if (dataTable.Rows.Count <= 0)
					{
						result = this.JsonResult(0, "已经开奖或已撤单,不能撤单!");
						return result;
					}
					string ssId = dataTable.Rows[0]["SsId"].ToString();
					int num = Convert.ToInt32(dataTable.Rows[0]["LotteryId"]);
					string issueNum = dataTable.Rows[0]["IssueNum"].ToString();
					int num2 = Convert.ToInt32(dataTable.Rows[0]["State"]);
					if (num2 != 0)
					{
						result = this.JsonResult(0, "已经开奖或已撤单,不能撤单!");
						return result;
					}
					if ((num == 3002 || num == 3003) && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") && DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 20:30:00"))
					{
						result = this.JsonResult(0, "现在是封单时间,不能撤单!");
						return result;
					}
					DateTime dateTime = Convert.ToDateTime(new UserBetDAL().GetIssueTime(num, issueNum));
					sqlCommand.CommandText = "select CloseTime From Sys_Lottery with(nolock) where Id=" + num;
					int num3 = Convert.ToInt32(sqlCommand.ExecuteScalar());
					sqlCommand.CommandText = "select DATEDIFF(S,GETDATE(),'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "') as aaa";
					int num4 = Convert.ToInt32(sqlCommand.ExecuteScalar());
					if (num4 <= num3)
					{
						result = this.JsonResult(0, "现在是封单时间,不能撤单!");
						return result;
					}
					decimal money = Convert.ToDecimal(Convert.ToDecimal(dataTable.Rows[0]["Total"].ToString()) * Convert.ToDecimal(dataTable.Rows[0]["Times"].ToString()));
					if (new UserTotalTran().MoneyOpers(ssId, dataTable.Rows[0]["UserId"].ToString(), money, num, Convert.ToInt32(dataTable.Rows[0]["PlayId"].ToString()), Convert.ToInt32(betId), 6, 99, string.Empty, string.Empty, "会员撤单", dataTable.Rows[0]["Stime"].ToString()) > 0)
					{
						sqlCommand.CommandText = "update N_UserBet set State=1 where Id=" + betId;
						sqlCommand.ExecuteNonQuery();
						result = this.JsonResult(1, "撤单成功!");
						return result;
					}
					result = this.JsonResult(0, "撤单失败!");
					return result;
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
				}
			}
			return this.JsonResult(0, "撤单失败!");
		}

		public string BetAllCancel(string ZhId, string PlayId)
		{
			string result;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					sqlDataAdapter.SelectCommand.CommandText = "select * From N_UserBet with(nolock) where state=0 and PlayId=" + PlayId + " and ZhId=" + ZhId;
					DataTable dataTable = new DataTable("N_UserBet");
					sqlDataAdapter.Fill(dataTable);
					if (dataTable.Rows.Count > 0)
					{
						foreach (DataRow dataRow in dataTable.Rows)
						{
							bool flag = true;
							string text = dataRow["Id"].ToString();
							string ssId = dataRow["SsId"].ToString();
							int num = Convert.ToInt32(dataRow["LotteryId"]);
							string issueNum = dataRow["IssueNum"].ToString();
							int num2 = Convert.ToInt32(dataRow["State"]);
							if (num2 != 0)
							{
								flag = false;
							}
							if ((num == 3002 || num == 3003) && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") && DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 20:30:00"))
							{
								flag = false;
							}
							DateTime dateTime = Convert.ToDateTime(new UserBetDAL().GetIssueTime(num, issueNum));
							sqlCommand.CommandText = "select CloseTime From Sys_Lottery with(nolock) where Id=" + num;
							int num3 = Convert.ToInt32(sqlCommand.ExecuteScalar());
							sqlCommand.CommandText = "select DATEDIFF(S,GETDATE(),'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "') as aaa";
							int num4 = Convert.ToInt32(sqlCommand.ExecuteScalar());
							if (num4 <= num3)
							{
								flag = false;
							}
							if (flag)
							{
								decimal money = Convert.ToDecimal(Convert.ToDecimal(dataRow["Total"].ToString()) * Convert.ToDecimal(dataRow["Times"].ToString()));
								if (new UserTotalTran().MoneyOpers(ssId, dataRow["UserId"].ToString(), money, num, Convert.ToInt32(dataRow["PlayId"].ToString()), Convert.ToInt32(text), 6, 99, string.Empty, string.Empty, "会员撤单", dataRow["STime"].ToString()) > 0)
								{
									sqlCommand.CommandText = "update N_UserBet set State=1 where Id=" + text;
									sqlCommand.ExecuteNonQuery();
								}
							}
						}
						result = this.JsonResult(1, "撤单成功!");
					}
					else
					{
						result = this.JsonResult(0, "没有可撤单的订单,不能撤单!");
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
					result = this.JsonResult(0, "撤单失败!");
				}
			}
			return result;
		}

		public int InsertBetAgain(int Id, int userId, string IssueNum)
		{
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					sqlCommand.CommandText = string.Concat(new object[]
					{
						"Insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total,Point,PointMoney,Bonus,Pos,PlayCode,STime,IsDelay,Times) \r\n                                        select '",
						SsId.Bet,
						"',UserId,UserMoney,LotteryId,PlayId,'",
						IssueNum,
						"',SingleMoney,Num,Detail,Total,Point,PointMoney,Bonus,Pos,PlayCode,getdate(),IsDelay,Times from N_UserBet where Id=",
						Id
					});
					SqlCommand expr_67 = sqlCommand;
					expr_67.CommandText += " SELECT SCOPE_IDENTITY()";
					int logSysId = Convert.ToInt32(sqlCommand.ExecuteScalar());
					object[] array = new object[5];
					using (DbOperHandler dbOperHandler = new ComData().Doh())
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "Id=@Id";
						dbOperHandler.AddConditionParameter("@Id", Id);
						array = dbOperHandler.GetFields("N_UserBet", "Total,LotteryId,IssueNum,Times,PlayId,ssId");
					}
					decimal money = Convert.ToDecimal(Convert.ToDecimal(array[0]) * Convert.ToDecimal(array[3]));
					if (new UserTotalTran().MoneyOpers(array[5].ToString(), userId.ToString(), money, Convert.ToInt32(array[1].ToString()), Convert.ToInt32(array[4].ToString()), logSysId, 3, 99, string.Empty, string.Empty, "再次投注", "") > 0)
					{
						result = 1;
					}
					else
					{
						result = 0;
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
					result = 0;
				}
			}
			return result;
		}

		public string[] GetIssueTimeAndSN(int lotteryId)
		{
			string[] array = new string[2];
			DateTime now = DateTime.Now;
			switch (lotteryId)
			{
			case 3002:
			case 3003:
				using (DbOperHandler dbOperHandler = new ComData().Doh())
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "select datediff(d,Convert(varchar(4),getdate(),120)+'-01-01 20:30:00',Convert(varchar(20),getdate(),120)) as d";
					DataTable dataTable = dbOperHandler.GetDataTable();
					int num = Convert.ToInt32(dataTable.Rows[0]["d"]) - 7;
					num++;
					array[1] = now.ToString("yyyy-MM-dd") + " 20:30:00";
					if (now > Convert.ToDateTime(now.ToString(" 20:30:00")))
					{
						array[1] = now.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
					}
					else
					{
						num--;
					}
					array[0] = now.Year.ToString() + base.AddZero(num + 1, 3);
					return array;
				}
			}
			using (DbOperHandler dbOperHandler2 = new ComData().Doh())
			{
				dbOperHandler2.Reset();
				dbOperHandler2.SqlCmd = "select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where Time > Convert(varchar(10),getdate(),108) and LotteryId=" + lotteryId + " order by Time asc";
				DataTable dataTable2 = dbOperHandler2.GetDataTable();
				if (dataTable2.Rows.Count > 0)
				{
					DataRow dataRow = dataTable2.Rows[0];
					array[1] = now.ToString("yyyy-MM-dd") + " " + dataRow["Time"].ToString();
					int num2 = Convert.ToInt32(dataRow["Sn"].ToString());
					array[0] = now.ToString("yyyyMMdd") + "-" + dataRow["Sn"].ToString();
					if (lotteryId == 1003 && num2 >= 85)
					{
						array[0] = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + base.AddZero(num2, 2);
					}
					if (lotteryId == 1010 || lotteryId == 1017 || lotteryId == 3004 || lotteryId == 1012 || lotteryId == 1013)
					{
						array[0] = string.Concat(new LotteryTimeDAL().GetTsIssueNum(lotteryId.ToString()) + Convert.ToInt32(dataRow["Sn"].ToString()));
					}
					if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:59:59") && (lotteryId == 1014 || lotteryId == 1016))
					{
						array[0] = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataRow["Sn"].ToString();
						array[1] = now.ToString("yyyy-MM-dd") + " " + dataRow["Time"].ToString();
					}
					if (lotteryId == 1014 || lotteryId == 1015 || lotteryId == 1016)
					{
						array[0] = array[0].Replace("-", "");
					}
					if (lotteryId == 4001)
					{
						if (DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00") && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:07:01"))
						{
							array[0] = string.Concat(new LotteryTimeDAL().GetTsIssueNum("4001") + 179 + Convert.ToInt32(dataRow["Sn"].ToString()));
						}
						else
						{
							array[0] = string.Concat(new LotteryTimeDAL().GetTsIssueNum("4001") + Convert.ToInt32(dataRow["Sn"].ToString()));
						}
					}
				}
				else
				{
					dbOperHandler2.Reset();
					dbOperHandler2.SqlCmd = "select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where LotteryId=" + lotteryId + " order by Time asc";
					dataTable2 = dbOperHandler2.GetDataTable();
					array[0] = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataTable2.Rows[0]["Sn"].ToString();
					array[1] = now.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataTable2.Rows[0]["Time"].ToString();
					if (lotteryId == 1010 || lotteryId == 1017 || lotteryId == 3004)
					{
						array[0] = string.Concat(new LotteryTimeDAL().GetTsIssueNum("1010") + 880 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString()));
					}
					if (lotteryId == 1012)
					{
						array[0] = string.Concat(new LotteryTimeDAL().GetTsIssueNum("1012") + 660 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString()));
					}
					if (lotteryId == 1013)
					{
						array[0] = string.Concat(new LotteryTimeDAL().GetTsIssueNum("1013") + 203 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString()));
					}
					if (lotteryId == 1014 || lotteryId == 1015 || lotteryId == 1016)
					{
						array[0] = array[0].Replace("-", "");
					}
					if (lotteryId == 4001)
					{
						array[0] = string.Concat(new LotteryTimeDAL().GetTsIssueNum("4001") + 179 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString()));
					}
				}
			}
			return array;
		}

		public string GetIssueTime(int lotteryId, string IssueNum)
		{
			string result = "";
			string text = IssueNum;
			DateTime dateTime = new DateTimePubDAL().GetDateTime();
			string value = dateTime.ToString("yyyyMMdd");
			dateTime.ToString("HH:mm:ss");
			IssueNum = IssueNum.Substring(IssueNum.IndexOf('-') + 1);
			switch (lotteryId)
			{
			case 1010:
			case 1012:
			case 1013:
			case 1017:
				break;
			case 1011:
				goto IL_35D;
			case 1014:
			case 1015:
			case 1016:
				goto IL_24B;
			default:
				switch (lotteryId)
				{
				case 3002:
				case 3003:
					using (DbOperHandler dbOperHandler = new ComData().Doh())
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = "select datediff(d,Convert(varchar(4),getdate(),120)+'-01-01 20:30:00',Convert(varchar(20),getdate(),120)) as d";
						DataTable dataTable = dbOperHandler.GetDataTable();
						int num = Convert.ToInt32(dataTable.Rows[0]["d"]) - 7;
						num++;
						result = dateTime.ToString("yyyy-MM-dd") + " 20:30:00";
						if (dateTime > Convert.ToDateTime(dateTime.ToString(" 20:30:00")))
						{
							result = dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
						}
						else
						{
							num--;
						}
						return result;
					}
				case 3004:
					break;
				default:
					if (lotteryId != 4001)
					{
						goto IL_35D;
					}
					break;
				}
				break;
			}
			using (DbOperHandler dbOperHandler2 = new ComData().Doh())
			{
				int tsIssueNumToPet = new LotteryTimeDAL().GetTsIssueNumToPet(lotteryId, Convert.ToInt32(IssueNum));
				string text2 = (tsIssueNumToPet.ToString().Length < 3) ? ("0" + tsIssueNumToPet) : tsIssueNumToPet.ToString();
				dbOperHandler2.Reset();
				dbOperHandler2.SqlCmd = string.Concat(new object[]
				{
					"select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where sn ='",
					text2,
					"' and LotteryId=",
					lotteryId,
					" order by Time asc"
				});
				DataTable dataTable2 = dbOperHandler2.GetDataTable();
				if (dataTable2.Rows.Count > 0)
				{
					DataRow dataRow = dataTable2.Rows[0];
					result = dateTime.ToString("yyyy-MM-dd") + " " + dataRow["Time"].ToString();
				}
				return result;
			}
			IL_24B:
			text = text.Substring(0, 8);
			IssueNum = IssueNum.Substring(8);
			using (DbOperHandler dbOperHandler3 = new ComData().Doh())
			{
				dbOperHandler3.Reset();
				dbOperHandler3.SqlCmd = string.Concat(new object[]
				{
					"select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where sn ='",
					IssueNum,
					"' and LotteryId=",
					lotteryId,
					" order by Time asc"
				});
				DataTable dataTable3 = dbOperHandler3.GetDataTable();
				if (dataTable3.Rows.Count > 0)
				{
					DataRow dataRow2 = dataTable3.Rows[0];
					if (Convert.ToInt32(text) > Convert.ToInt32(value))
					{
						result = dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataRow2["Time"].ToString();
					}
					else
					{
						result = dateTime.ToString("yyyy-MM-dd") + " " + dataRow2["Time"].ToString();
					}
				}
				return result;
			}
			IL_35D:
			text = text.Substring(0, 8);
			using (DbOperHandler dbOperHandler4 = new ComData().Doh())
			{
				dbOperHandler4.Reset();
				dbOperHandler4.SqlCmd = string.Concat(new object[]
				{
					"select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where sn ='",
					IssueNum,
					"' and LotteryId=",
					lotteryId,
					" order by Time asc"
				});
				DataTable dataTable4 = dbOperHandler4.GetDataTable();
				if (dataTable4.Rows.Count > 0)
				{
					DataRow dataRow3 = dataTable4.Rows[0];
					if (Convert.ToInt32(text) > Convert.ToInt32(value))
					{
						result = dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataRow3["Time"].ToString();
					}
					else
					{
						result = dateTime.ToString("yyyy-MM-dd") + " " + dataRow3["Time"].ToString();
					}
				}
			}
			return result;
		}

		public void GetBetInfoJSON(string Id, string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 1 UserId,STime2 from N_UserBet where Id=" + Id;
				DataTable dataTable = dbOperHandler.GetDataTable();
				string betDetail = BetDetailDAL.GetBetDetail(Convert.ToDateTime(dataTable.Rows[0]["STime2"]).ToString("yyyyMMdd"), dataTable.Rows[0]["UserId"].ToString(), Id);
				if (!string.IsNullOrEmpty(betDetail))
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "select a.*," + UserId + " as CurUserId,'@@youle' as strDetail,cast(round(a.total*times,4) as numeric(15,4)) as Total2,Convert(varchar(15),cast(Bonus as numeric(15,4)))+'/'+Convert(varchar(10),cast(round([Point],2) as numeric(10,2)))+'%' as Point2,case [SingleMoney] when '2.00' then '元' when '0.20' then '角' when '0.02' then '分' when '0.002' then '厘' end moshi,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetPlayName(PlayId) as PlayName,dbo.f_GetLotteryName(LotteryId) as LotteryName,cast(Times*a.Total as decimal(15,4)) as BetMoney,case state when 0 then '未开奖' when 1 then '已撤单' when 2 then '未中奖' when 3 then '已中奖' end as stateName,isnull(b.number,'未开奖不计算') as kjnumber from N_UserBet a left join Sys_LotteryData b on a.LotteryId=b.Type and a.IssueNum=b.Title where a.Id=" + Id;
					DataTable dataTable2 = dbOperHandler.GetDataTable();
					_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable2) + "}";
					_jsonstr = _jsonstr.Replace("@@youle", betDetail);
					dataTable2.Clear();
					dataTable2.Dispose();
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = "select a.*," + UserId + " as CurUserId,Detail as strDetail,cast(round(a.total*times,4) as numeric(15,4)) as Total2,Convert(varchar(15),cast(Bonus as numeric(15,4)))+'/'+Convert(varchar(10),cast(round([Point],2) as numeric(10,2)))+'%' as Point2,case [SingleMoney] when '2.00' then '元' when '0.20' then '角' when '0.02' then '分' when '0.002' then '厘' end moshi,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetPlayName(PlayId) as PlayName,dbo.f_GetLotteryName(LotteryId) as LotteryName,cast(Times*a.Total as decimal(15,4)) as BetMoney,case state when 0 then '未开奖' when 1 then '已撤单' when 2 then '未中奖' when 3 then '已中奖' end as stateName,isnull(b.number,'未开奖不计算') as kjnumber from N_UserBet a left join Sys_LotteryData b on a.LotteryId=b.Type and a.IssueNum=b.Title where a.Id=" + Id;
					DataTable dataTable3 = dbOperHandler.GetDataTable();
					_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable3) + "}";
					dataTable3.Clear();
					dataTable3.Dispose();
				}
			}
		}

		protected new string JsonResult(int success, string str)
		{
			return string.Concat(new string[]
			{
				"[{\"result\" :\"",
				success.ToString(),
				"\",\"returnval\" :\"",
				str,
				"\"}]"
			});
		}

		protected SiteModel site;
	}
}
