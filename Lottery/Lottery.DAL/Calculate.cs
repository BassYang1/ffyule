using System;
using System.Data;
using System.Linq;
using Lottery.DBUtility;

namespace Lottery.DAL
{
	public class Calculate
	{
		public static string BetNumerice(int userId, int lotteryId, string balls, string playId, string pos, int betnum, decimal Point, ref decimal singelBouns)
		{
			string text = "";
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "LotteryId=" + lotteryId;
				string text2 = dbOperHandler.GetField("Sys_LotteryPlaySetting", "Setting").ToString();
				if (text2.IndexOf("," + playId + ",") != -1)
				{
					string result = Calculate.JsonResult(0, "投注失败,该玩法已关闭!");
					return result;
				}
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = string.Concat(new object[]
				{
					"UserId=",
					userId,
					" and LotteryId=",
					lotteryId
				});
				string text3 = dbOperHandler.GetField("N_UserPlaySetting", "Setting").ToString();
				if (text3.IndexOf("," + playId + ",") != -1)
				{
					string result = Calculate.JsonResult(0, "投注失败,该玩法已关闭!");
					return result;
				}
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=@Id";
				dbOperHandler.AddConditionParameter("@Id", userId.ToString());
				object field = dbOperHandler.GetField("N_User", "Point");
				decimal num2 = string.IsNullOrEmpty(string.Concat(field)) ? 0m : Convert.ToDecimal(string.Concat(field));
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select * from Sys_PlaySmallType where Id=" + playId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				int num3 = Convert.ToInt32(dataTable.Rows[0]["LotteryId"].ToString());
				text = dataTable.Rows[0]["Title2"].ToString();
				Convert.ToDecimal(dataTable.Rows[0]["MaxBonus"].ToString());
				decimal d = Convert.ToDecimal(dataTable.Rows[0]["MinBonus"].ToString());
				Convert.ToDecimal(dataTable.Rows[0]["MinBonus2"].ToString());
				decimal num4 = Convert.ToDecimal(dataTable.Rows[0]["PosBonus"].ToString());
				string value = dataTable.Rows[0]["IsOpen"].ToString();
				num = Convert.ToInt32(dataTable.Rows[0]["MaxNum"].ToString());
				if (Convert.ToInt32(lotteryId.ToString().Substring(0, 1)) != num3)
				{
					string result = Calculate.JsonResult(0, "投注失败, 投注玩法错误!");
					return result;
				}
				if (Convert.ToInt32(value) == 1)
				{
					string result = Calculate.JsonResult(0, "投注失败,该玩法已关闭!");
					return result;
				}
				if (num4 != 0m && lotteryId != 5001)
				{
					if (Point > num2 || Point < 0m)
					{
						string result = Calculate.JsonResult(0, "投注失败,返点错误，请重新投注！");
						return result;
					}
					singelBouns = Convert.ToDecimal(Convert.ToDecimal(d + (num2 - Point * 10m) * 2m * num4).ToString("0.0000"));
				}
			}
			string key;
			int num6;
			switch (key = text)
			{
			case "P_5ZX120":
				num6 = Calculate.RedZu120(balls);
				goto IL_1B37;
			case "P_5ZX60":
				num6 = Calculate.RedZu60(balls);
				goto IL_1B37;
			case "P_5ZX30":
				num6 = Calculate.RedZu30(balls);
				goto IL_1B37;
			case "P_5ZX20":
				num6 = Calculate.RedZu20(balls);
				goto IL_1B37;
			case "P_5ZX10":
				num6 = Calculate.RedZu10(balls);
				goto IL_1B37;
			case "P_5ZX5":
				num6 = Calculate.RedZu5(balls);
				goto IL_1B37;
			case "P_5TS1":
			case "P_5TS2":
			case "P_5TS3":
			case "P_5TS4":
				if (balls.Contains(',') || (balls.Length > 1 && !balls.Contains("_")))
				{
					num6 = 0;
					goto IL_1B37;
				}
				num6 = Calculate.RedTS(balls);
				goto IL_1B37;
			case "P_4ZX24":
				num6 = Calculate.RedZu24(balls);
				goto IL_1B37;
			case "P_4ZX12":
				num6 = Calculate.RedZu12(balls);
				goto IL_1B37;
			case "P_4ZX6":
				num6 = Calculate.RedZu61(balls);
				goto IL_1B37;
			case "P_4ZX4":
				num6 = Calculate.RedZu4(balls);
				goto IL_1B37;
			case "P_5FS":
				if (balls.Split(new char[]
				{
					','
				}).Length != 5)
				{
					num6 = 0;
					goto IL_1B37;
				}
				num6 = Calculate.RedFS(balls);
				goto IL_1B37;
			case "P_4FS_L":
			case "P_4FS_R":
				if (balls.Split(new char[]
				{
					','
				}).Length != 4)
				{
					num6 = 0;
					goto IL_1B37;
				}
				num6 = Calculate.RedFS(balls);
				goto IL_1B37;
			case "P_3FS_L":
			case "P_3FS_C":
			case "P_3FS_R":
				if (balls.Split(new char[]
				{
					','
				}).Length != 3)
				{
					num6 = 0;
					goto IL_1B37;
				}
				num6 = Calculate.RedFS(balls);
				goto IL_1B37;
			case "P_2FS_L":
			case "P_2FS_R":
				if (balls.Split(new char[]
				{
					','
				}).Length != 2)
				{
					num6 = 0;
					goto IL_1B37;
				}
				num6 = Calculate.RedFS(balls);
				goto IL_1B37;
			case "P_5DS":
				num6 = Calculate.RedDS(balls, 5);
				goto IL_1B37;
			case "P_4DS_L":
			case "P_4DS_R":
				num6 = Calculate.RedDS(balls, 4);
				goto IL_1B37;
			case "P_3DS_L":
			case "P_3DS_C":
			case "P_3DS_R":
				num6 = Calculate.RedDS(balls, 3);
				goto IL_1B37;
			case "P_2DS_L":
			case "P_2DS_R":
			case "P_2ZDS_L":
			case "P_2ZDS_R":
				num6 = Calculate.RedDS(balls, 2);
				goto IL_1B37;
			case "P_3HX_L":
			case "P_3HX_C":
			case "P_3HX_R":
				num6 = Calculate.RedDS(balls, 3);
				goto IL_1B37;
			case "P_3Z3_L":
			case "P_3Z3_C":
			case "P_3Z3_R":
				if (!balls.Contains(","))
				{
					num6 = Calculate.RedZu3(balls);
					goto IL_1B37;
				}
				num6 = 0;
				goto IL_1B37;
			case "P_3Z6_L":
			case "P_3Z6_C":
			case "P_3Z6_R":
				if (!balls.Contains(","))
				{
					num6 = Calculate.RedZu6(balls);
					goto IL_1B37;
				}
				num6 = 0;
				goto IL_1B37;
			case "P_2Z2_L":
			case "P_2Z2_R":
				if (!balls.Contains(","))
				{
					num6 = Calculate.RedZu2(balls);
					goto IL_1B37;
				}
				num6 = 0;
				goto IL_1B37;
			case "P_DD":
			{
				if (balls.Split(new char[]
				{
					','
				}).Length != 5)
				{
					num6 = 0;
				}
				else
				{
					num6 = Calculate.RedDD(balls);
				}
				bool flag = false;
				string[] array = balls.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Length > 8)
					{
						flag = true;
					}
				}
				if (flag)
				{
					return Calculate.JsonResult(0, "投注失败,定位胆单个位置最多允许投注8码！");
				}
				goto IL_1B37;
			}
			case "P_BDD_C":
			case "P_BDD_L":
			case "P_BDD_R":
				if (!balls.Contains(","))
				{
					num6 = Calculate.RedFS(balls);
					goto IL_1B37;
				}
				num6 = 0;
				goto IL_1B37;
			case "P_2DXDS_L":
			case "P_2DXDS_R":
			case "P_LHH_WQ":
			case "P_LHH_WB":
			case "P_LHH_WS":
			case "P_LHH_WG":
			case "P_LHH_QB":
			case "P_LHH_QS":
			case "P_LHH_QG":
			case "P_LHH_BS":
			case "P_LHH_BG":
			case "P_LHH_SG":
				num6 = Calculate.RedFS(balls.Replace("_", ""));
				goto IL_1B37;
			case "P_3HE_L":
			case "P_3HE_C":
			case "P_3HE_R":
				num6 = Calculate.RedHE3(balls);
				goto IL_1B37;
			case "P_3ZHE_L":
			case "P_3ZHE_C":
			case "P_3ZHE_R":
				num6 = Calculate.RedZHE3(balls);
				goto IL_1B37;
			case "P_3KD_L":
			case "P_3KD_C":
			case "P_3KD_R":
				num6 = Calculate.Red3KD(balls);
				goto IL_1B37;
			case "P_3Z3DS_L":
			case "P_3Z6DS_L":
			case "P_3Z3DS_C":
			case "P_3Z6DS_C":
			case "P_3Z3DS_R":
			case "P_3Z6DS_R":
				num6 = Calculate.RedDS(balls, 3);
				goto IL_1B37;
			case "P_3ZBD_L":
			case "P_3ZBD_C":
			case "P_3ZBD_R":
				num6 = 54;
				goto IL_1B37;
			case "P_3QTWS_L":
			case "P_3QTWS_C":
			case "P_3QTWS_R":
				num6 = Calculate.RedDD(balls.Replace("_", ""));
				goto IL_1B37;
			case "P_3QTTS_L":
			case "P_3QTTS_C":
			case "P_3QTTS_R":
				num6 = Calculate.RedDD(balls.Replace("_", "")) / 2;
				goto IL_1B37;
			case "P_2HE_L":
			case "P_2HE_R":
				num6 = Calculate.RedHE2(balls);
				goto IL_1B37;
			case "P_2ZHE_L":
			case "P_2ZHE_R":
				num6 = Calculate.RedZHE2(balls);
				goto IL_1B37;
			case "P_2KD_L":
			case "P_2KD_R":
				num6 = Calculate.Red2KD(balls);
				goto IL_1B37;
			case "P_2ZBD_L":
			case "P_2ZBD_R":
				num6 = 9;
				goto IL_1B37;
			case "P_5ZH":
				num6 = Calculate.Red5ZuHe(balls);
				goto IL_1B37;
			case "P_4ZH_L":
			case "P_4ZH_R":
				num6 = Calculate.Red4ZuHe(balls);
				goto IL_1B37;
			case "P_3ZH_L":
			case "P_3ZH_C":
			case "P_3ZH_R":
				num6 = Calculate.Red3ZuHe(balls);
				goto IL_1B37;
			case "P_3BDD1_R":
			case "P_3BDD1_L":
			case "P_4BDD1":
				num6 = Calculate.RedDD(balls.Replace("_", ""));
				goto IL_1B37;
			case "P_3BDD2_R":
			case "P_3BDD2_L":
			case "P_4BDD2":
			case "P_5BDD2":
				num6 = Calculate.RedZu2(balls.Replace("_", ""));
				goto IL_1B37;
			case "P_5BDD3":
				num6 = Calculate.RedZu6(balls.Replace("_", ""));
				goto IL_1B37;
			case "P_5QJ3":
			case "P_4QJ3":
			case "P_3QJ2_L":
			case "P_3QJ2_R":
				num6 = Calculate.RedQwQj(balls);
				goto IL_1B37;
			case "P_5QW3":
			case "P_4QW3":
			case "P_3QW2_L":
			case "P_3QW2_R":
				num6 = Calculate.RedFS(balls.Replace("_", ""));
				goto IL_1B37;
			case "R_4FS":
				num6 = Calculate.RedFS_R(balls, pos, 4);
				goto IL_1B37;
			case "R_4DS":
				num6 = Calculate.RedDS_R(balls, pos, 4);
				goto IL_1B37;
			case "R_4ZX24":
				num6 = Calculate.RedZu24(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 4);
				goto IL_1B37;
			case "R_4ZX12":
				num6 = Calculate.RedZu12(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 4);
				goto IL_1B37;
			case "R_4ZX6":
				num6 = Calculate.RedZu61(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 4);
				goto IL_1B37;
			case "R_4ZX4":
				num6 = Calculate.RedZu4(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 4);
				goto IL_1B37;
			case "R_3FS":
				num6 = Calculate.RedFS_R(balls, pos, 3);
				goto IL_1B37;
			case "R_3DS":
				num6 = Calculate.RedDS_R(balls, pos, 3);
				goto IL_1B37;
			case "R_3HX":
				num6 = Calculate.RedDS_R(balls, pos, 3);
				goto IL_1B37;
			case "R_3Z6":
				if (!balls.Contains(","))
				{
					num6 = Calculate.RedZu6_R(balls, pos, 3);
					goto IL_1B37;
				}
				num6 = 0;
				goto IL_1B37;
			case "R_3Z3":
				if (!balls.Contains(","))
				{
					num6 = Calculate.RedZu3_R(balls, pos, 3);
					goto IL_1B37;
				}
				num6 = 0;
				goto IL_1B37;
			case "R_3HE":
				num6 = Calculate.RedHE3(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 3);
				goto IL_1B37;
			case "R_3ZHE":
				num6 = Calculate.RedZHE3(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 3);
				goto IL_1B37;
			case "R_3KD":
				num6 = Calculate.Red3KD(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 3);
				goto IL_1B37;
			case "R_3ZBD":
				num6 = 54 * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 3);
				goto IL_1B37;
			case "R_3QTWS":
				num6 = Calculate.RedDD(balls.Replace("_", "")) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 3);
				goto IL_1B37;
			case "R_3QTTS":
				num6 = Calculate.RedDD(balls.Replace("_", "")) / 2 * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 3);
				goto IL_1B37;
			case "R_3Z3DS":
			case "R_3Z6DS":
				num6 = Calculate.RedDS(balls, 3) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 3);
				goto IL_1B37;
			case "R_2FS":
				num6 = Calculate.RedFS_R(balls, pos, 2);
				goto IL_1B37;
			case "R_2DS":
				num6 = Calculate.RedDS_R(balls, pos, 2);
				goto IL_1B37;
			case "R_2Z2":
				if (!balls.Contains(","))
				{
					num6 = Calculate.RedZu2_R(balls, pos, 2);
					goto IL_1B37;
				}
				num6 = 0;
				goto IL_1B37;
			case "R_2HE":
				num6 = Calculate.RedHE2(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 2);
				goto IL_1B37;
			case "R_2ZHE":
				num6 = Calculate.RedZHE2(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 2);
				goto IL_1B37;
			case "R_2ZDS":
				num6 = Calculate.RedDS(balls, 2) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 2);
				goto IL_1B37;
			case "R_2KD":
				num6 = Calculate.Red2KD(balls) * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 2);
				goto IL_1B37;
			case "R_2ZBD":
				num6 = 9 * Calculate.Combine(pos.Split(new char[]
				{
					'1'
				}).Length - 1, 2);
				goto IL_1B37;
			case "P11_RXDS_1":
				num6 = Calculate.RedDS(balls, 1);
				goto IL_1B37;
			case "P11_RXDS_2":
				num6 = Calculate.RedDS(balls, 2);
				goto IL_1B37;
			case "P11_RXDS_3":
				num6 = Calculate.RedDS(balls, 3);
				goto IL_1B37;
			case "P11_RXDS_4":
				num6 = Calculate.RedDS(balls, 4);
				goto IL_1B37;
			case "P11_RXDS_5":
				num6 = Calculate.RedDS(balls, 5);
				goto IL_1B37;
			case "P11_RXDS_6":
				num6 = Calculate.RedDS(balls, 6);
				goto IL_1B37;
			case "P11_RXDS_7":
				num6 = Calculate.RedDS(balls, 7);
				goto IL_1B37;
			case "P11_RXDS_8":
				num6 = Calculate.RedDS(balls, 8);
				goto IL_1B37;
			case "P11_RXFS_1":
				num6 = Calculate.RedRXFS_11(balls, 1);
				goto IL_1B37;
			case "P11_RXFS_2":
				num6 = Calculate.RedRXFS_11(balls, 2);
				goto IL_1B37;
			case "P11_RXFS_3":
				num6 = Calculate.RedRXFS_11(balls, 3);
				goto IL_1B37;
			case "P11_RXFS_4":
				num6 = Calculate.RedRXFS_11(balls, 4);
				goto IL_1B37;
			case "P11_RXFS_5":
				num6 = Calculate.RedRXFS_11(balls, 5);
				goto IL_1B37;
			case "P11_RXFS_6":
				num6 = Calculate.RedRXFS_11(balls, 6);
				goto IL_1B37;
			case "P11_RXFS_7":
				num6 = Calculate.RedRXFS_11(balls, 7);
				goto IL_1B37;
			case "P11_RXFS_8":
				num6 = Calculate.RedRXFS_11(balls, 8);
				goto IL_1B37;
			case "P11_3FS_L":
				num6 = Calculate.Red3FS_11(balls);
				goto IL_1B37;
			case "P11_3ZFS_L":
				num6 = Calculate.Red3ZFS_11(balls);
				goto IL_1B37;
			case "P11_2FS_L":
				num6 = Calculate.Red2FS_11(balls);
				goto IL_1B37;
			case "P11_2ZFS_L":
				num6 = Calculate.Red2ZFS_11(balls);
				goto IL_1B37;
			case "P11_3DS_L":
			case "P11_3ZDS_L":
				num6 = Calculate.RedDS(balls, 3);
				goto IL_1B37;
			case "P11_2DS_L":
			case "P11_2ZDS_L":
				num6 = Calculate.RedDS(balls, 2);
				goto IL_1B37;
			case "P11_DD":
			{
				if (balls.Split(new char[]
				{
					','
				}).Length != 3)
				{
					num6 = 0;
				}
				else
				{
					num6 = Calculate.RedDD_11(balls);
				}
				bool flag2 = false;
				string[] array2 = balls.Split(new char[]
				{
					','
				});
				for (int j = 0; j < array2.Length; j++)
				{
					if (array2[j].Split(new char[]
					{
						'_'
					}).Length > 8)
					{
					}
				}
				if (flag2)
				{
					return Calculate.JsonResult(0, "投注失败,定位胆单个位置最多允许投注8码！");
				}
				goto IL_1B37;
			}
			case "P11_BDD_L":
				if (balls.Contains(',') || (balls.Length > 2 && !balls.Contains("_")))
				{
					num6 = 0;
					goto IL_1B37;
				}
				num6 = Calculate.RedRXFS_11(balls, 1);
				goto IL_1B37;
			case "P11_CDS":
				num6 = Calculate.RedRXFS_11(balls, 1);
				goto IL_1B37;
			case "P11_CZW":
				num6 = Calculate.RedRXFS_11(balls, 1);
				goto IL_1B37;
			case "P_DD_3":
				num6 = Calculate.RedDD(balls);
				goto IL_1B37;
			case "PK10_One":
				num6 = Calculate.PK10FS_One(balls);
				goto IL_1B37;
			case "PK10_TwoFS":
				num6 = Calculate.Red2FS_11(balls);
				goto IL_1B37;
			case "PK10_TwoDS":
				num6 = Calculate.RedDS(balls, 2);
				goto IL_1B37;
			case "PK10_ThreeFS":
				num6 = Calculate.Red3FS_11(balls);
				goto IL_1B37;
			case "PK10_ThreeDS":
				num6 = Calculate.RedDS(balls, 3);
				goto IL_1B37;
			case "PK10_DD1_5":
			case "PK10_DD6_10":
			{
				num6 = Calculate.RedDD_11(balls);
				bool flag3 = false;
				string[] array3 = balls.Split(new char[]
				{
					','
				});
				for (int k = 0; k < array3.Length; k++)
				{
					if (array3[k].Split(new char[]
					{
						'_'
					}).Length > 8)
					{
						flag3 = true;
					}
				}
				if (flag3)
				{
					return Calculate.JsonResult(0, "投注失败,定位胆单个位置最多允许投注8码！");
				}
				goto IL_1B37;
			}
			case "PK10_DXOne":
			case "PK10_DXTwo":
			case "PK10_DXThree":
			case "PK10_DSOne":
			case "PK10_DSTwo":
			case "PK10_DSThree":
				num6 = Calculate.PK10DXDS(balls);
				goto IL_1B37;
			}
			num6 = 0;
			IL_1B37:
			if (num6 < 1)
			{
				return Calculate.JsonResult(0, "投注失败,投注号码不正确，请重新投注！");
			}
			if (betnum != num6)
			{
				return Calculate.JsonResult(0, "投注失败,投注号码不正确，请重新投注！");
			}
			if (num < num6)
			{
				return Calculate.JsonResult(0, "投注失败,该玩法最大允许投注" + num + "注！");
			}
			return "";
		}

		public static int RedFS(string balls)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					','
				});
				int num = 1;
				for (int i = 0; i < array.Length; i++)
				{
					int length = array[i].Length;
					num *= length;
				}
				return num;
			}
			return 0;
		}

		public static int RedDS(string balls, int num)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						' '
					});
					if (array2.Length > num)
					{
						return 0;
					}
				}
				return array.Length;
			}
			return 0;
		}

		public static int Pareto(int n, int r)
		{
			int num = 1;
			for (int num2 = n; num2 != n - r; num2--)
			{
				num *= num2;
			}
			return num;
		}

		public static int Combine(int n, int r)
		{
			return Calculate.Pareto(n, r) / Calculate.Pareto(r, r);
		}

		public static int RedZu6(string balls)
		{
			if (balls != "")
			{
				int length = balls.Length;
				return Calculate.Pareto(length, 3) / Calculate.Pareto(3, 3);
			}
			return 0;
		}

		public static int RedZu3(string balls)
		{
			if (balls != "")
			{
				int length = balls.Length;
				return Calculate.Pareto(length, 2);
			}
			return 0;
		}

		public static int RedZu2(string balls)
		{
			if (balls != "")
			{
				int length = balls.Length;
				return length * (length - 1) / 2;
			}
			return 0;
		}

		public static int RedDD(string balls)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					','
				});
				int num = 0;
				for (int i = 0; i < array.Length; i++)
				{
					int length = array[i].Length;
					num += length;
				}
				return num;
			}
			return 0;
		}

		public static int RedFS_R(string balls, string p, int PlayWzNum)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					','
				});
				int num = 1;
				for (int i = 0; i < array.Length; i++)
				{
					int length = array[i].Length;
					num *= length;
				}
				int n = p.Split(new char[]
				{
					'1'
				}).Length - 1;
				return num * Calculate.Combine(n, PlayWzNum);
			}
			return 0;
		}

		public static int RedDS_R(string balls, string p, int PlayWzNum)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					','
				});
				int n = p.Split(new char[]
				{
					'1'
				}).Length - 1;
				return array.Length * Calculate.Combine(n, PlayWzNum);
			}
			return 0;
		}

		public static int RedZu6_R(string balls, string p, int PlayWzNum)
		{
			if (balls != "")
			{
				int length = balls.Length;
				int n = p.Split(new char[]
				{
					'1'
				}).Length - 1;
				return Calculate.Pareto(length, 3) / Calculate.Pareto(3, 3) * Calculate.Combine(n, PlayWzNum);
			}
			return 0;
		}

		public static int RedZu3_R(string balls, string p, int PlayWzNum)
		{
			if (balls != "")
			{
				int length = balls.Length;
				int n = p.Split(new char[]
				{
					'1'
				}).Length - 1;
				return Calculate.Pareto(length, 2) * Calculate.Combine(n, PlayWzNum);
			}
			return 0;
		}

		public static int RedZu2_R(string balls, string p, int PlayWzNum)
		{
			if (balls != "")
			{
				int length = balls.Length;
				int n = p.Split(new char[]
				{
					'1'
				}).Length - 1;
				return length * (length - 1) / 2 * Calculate.Combine(n, PlayWzNum);
			}
			return 0;
		}

		public static int RedHE3(string balls)
		{
			int num = 0;
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					if (Convert.ToInt32(array[num2]) == 0)
					{
						num++;
					}
					if (Convert.ToInt32(array[num2]) == 1)
					{
						num += 3;
					}
					if (Convert.ToInt32(array[num2]) == 2)
					{
						num += 6;
					}
					if (Convert.ToInt32(array[num2]) == 3)
					{
						num += 10;
					}
					if (Convert.ToInt32(array[num2]) == 4)
					{
						num += 15;
					}
					if (Convert.ToInt32(array[num2]) == 5)
					{
						num += 21;
					}
					if (Convert.ToInt32(array[num2]) == 6)
					{
						num += 28;
					}
					if (Convert.ToInt32(array[num2]) == 7)
					{
						num += 36;
					}
					if (Convert.ToInt32(array[num2]) == 8)
					{
						num += 45;
					}
					if (Convert.ToInt32(array[num2]) == 9)
					{
						num += 55;
					}
					if (Convert.ToInt32(array[num2]) == 10)
					{
						num += 63;
					}
					if (Convert.ToInt32(array[num2]) == 11)
					{
						num += 69;
					}
					if (Convert.ToInt32(array[num2]) == 12)
					{
						num += 73;
					}
					if (Convert.ToInt32(array[num2]) == 13)
					{
						num += 75;
					}
					if (Convert.ToInt32(array[num2]) == 14)
					{
						num += 75;
					}
					if (Convert.ToInt32(array[num2]) == 15)
					{
						num += 73;
					}
					if (Convert.ToInt32(array[num2]) == 16)
					{
						num += 69;
					}
					if (Convert.ToInt32(array[num2]) == 17)
					{
						num += 63;
					}
					if (Convert.ToInt32(array[num2]) == 18)
					{
						num += 55;
					}
					if (Convert.ToInt32(array[num2]) == 19)
					{
						num += 45;
					}
					if (Convert.ToInt32(array[num2]) == 20)
					{
						num += 36;
					}
					if (Convert.ToInt32(array[num2]) == 21)
					{
						num += 28;
					}
					if (Convert.ToInt32(array[num2]) == 22)
					{
						num += 21;
					}
					if (Convert.ToInt32(array[num2]) == 23)
					{
						num += 15;
					}
					if (Convert.ToInt32(array[num2]) == 24)
					{
						num += 10;
					}
					if (Convert.ToInt32(array[num2]) == 25)
					{
						num += 6;
					}
					if (Convert.ToInt32(array[num2]) == 26)
					{
						num += 3;
					}
					if (Convert.ToInt32(array[num2]) == 27)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int RedZHE3(string balls)
		{
			int num = 0;
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					if (Convert.ToInt32(array[num2]) == 1)
					{
						num++;
					}
					if (Convert.ToInt32(array[num2]) == 2)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 3)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 4)
					{
						num += 4;
					}
					if (Convert.ToInt32(array[num2]) == 5)
					{
						num += 5;
					}
					if (Convert.ToInt32(array[num2]) == 6)
					{
						num += 6;
					}
					if (Convert.ToInt32(array[num2]) == 7)
					{
						num += 8;
					}
					if (Convert.ToInt32(array[num2]) == 8)
					{
						num += 10;
					}
					if (Convert.ToInt32(array[num2]) == 9)
					{
						num += 11;
					}
					if (Convert.ToInt32(array[num2]) == 10)
					{
						num += 13;
					}
					if (Convert.ToInt32(array[num2]) == 11)
					{
						num += 14;
					}
					if (Convert.ToInt32(array[num2]) == 12)
					{
						num += 14;
					}
					if (Convert.ToInt32(array[num2]) == 13)
					{
						num += 15;
					}
					if (Convert.ToInt32(array[num2]) == 14)
					{
						num += 15;
					}
					if (Convert.ToInt32(array[num2]) == 15)
					{
						num += 14;
					}
					if (Convert.ToInt32(array[num2]) == 16)
					{
						num += 14;
					}
					if (Convert.ToInt32(array[num2]) == 17)
					{
						num += 13;
					}
					if (Convert.ToInt32(array[num2]) == 18)
					{
						num += 11;
					}
					if (Convert.ToInt32(array[num2]) == 19)
					{
						num += 10;
					}
					if (Convert.ToInt32(array[num2]) == 20)
					{
						num += 8;
					}
					if (Convert.ToInt32(array[num2]) == 21)
					{
						num += 6;
					}
					if (Convert.ToInt32(array[num2]) == 22)
					{
						num += 5;
					}
					if (Convert.ToInt32(array[num2]) == 23)
					{
						num += 4;
					}
					if (Convert.ToInt32(array[num2]) == 24)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 25)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 26)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int Red3KD(string balls)
		{
			int num = 0;
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					if (Convert.ToInt32(array[num2]) == 0)
					{
						num += 10;
					}
					if (Convert.ToInt32(array[num2]) == 1)
					{
						num += 54;
					}
					if (Convert.ToInt32(array[num2]) == 2)
					{
						num += 96;
					}
					if (Convert.ToInt32(array[num2]) == 3)
					{
						num += 126;
					}
					if (Convert.ToInt32(array[num2]) == 4)
					{
						num += 144;
					}
					if (Convert.ToInt32(array[num2]) == 5)
					{
						num += 150;
					}
					if (Convert.ToInt32(array[num2]) == 6)
					{
						num += 144;
					}
					if (Convert.ToInt32(array[num2]) == 7)
					{
						num += 126;
					}
					if (Convert.ToInt32(array[num2]) == 8)
					{
						num += 96;
					}
					if (Convert.ToInt32(array[num2]) == 9)
					{
						num += 54;
					}
				}
			}
			return num;
		}

		public static int RedHE2(string balls)
		{
			int num = 0;
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					if (Convert.ToInt32(array[num2]) == 0)
					{
						num++;
					}
					if (Convert.ToInt32(array[num2]) == 1)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 2)
					{
						num += 3;
					}
					if (Convert.ToInt32(array[num2]) == 3)
					{
						num += 4;
					}
					if (Convert.ToInt32(array[num2]) == 4)
					{
						num += 5;
					}
					if (Convert.ToInt32(array[num2]) == 5)
					{
						num += 6;
					}
					if (Convert.ToInt32(array[num2]) == 6)
					{
						num += 7;
					}
					if (Convert.ToInt32(array[num2]) == 7)
					{
						num += 8;
					}
					if (Convert.ToInt32(array[num2]) == 8)
					{
						num += 9;
					}
					if (Convert.ToInt32(array[num2]) == 9)
					{
						num += 10;
					}
					if (Convert.ToInt32(array[num2]) == 10)
					{
						num += 9;
					}
					if (Convert.ToInt32(array[num2]) == 11)
					{
						num += 8;
					}
					if (Convert.ToInt32(array[num2]) == 12)
					{
						num += 7;
					}
					if (Convert.ToInt32(array[num2]) == 13)
					{
						num += 6;
					}
					if (Convert.ToInt32(array[num2]) == 14)
					{
						num += 5;
					}
					if (Convert.ToInt32(array[num2]) == 15)
					{
						num += 4;
					}
					if (Convert.ToInt32(array[num2]) == 16)
					{
						num += 3;
					}
					if (Convert.ToInt32(array[num2]) == 17)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 18)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int RedZHE2(string balls)
		{
			int num = 0;
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					if (Convert.ToInt32(array[num2]) == 1)
					{
						num++;
					}
					if (Convert.ToInt32(array[num2]) == 2)
					{
						num++;
					}
					if (Convert.ToInt32(array[num2]) == 3)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 4)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 5)
					{
						num += 3;
					}
					if (Convert.ToInt32(array[num2]) == 6)
					{
						num += 3;
					}
					if (Convert.ToInt32(array[num2]) == 7)
					{
						num += 4;
					}
					if (Convert.ToInt32(array[num2]) == 8)
					{
						num += 4;
					}
					if (Convert.ToInt32(array[num2]) == 9)
					{
						num += 5;
					}
					if (Convert.ToInt32(array[num2]) == 10)
					{
						num += 4;
					}
					if (Convert.ToInt32(array[num2]) == 11)
					{
						num += 4;
					}
					if (Convert.ToInt32(array[num2]) == 12)
					{
						num += 3;
					}
					if (Convert.ToInt32(array[num2]) == 13)
					{
						num += 3;
					}
					if (Convert.ToInt32(array[num2]) == 14)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 15)
					{
						num += 2;
					}
					if (Convert.ToInt32(array[num2]) == 16)
					{
						num++;
					}
					if (Convert.ToInt32(array[num2]) == 17)
					{
						num++;
					}
				}
			}
			return num;
		}

		public static int Red2KD(string balls)
		{
			int num = 0;
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					if (Convert.ToInt32(array[num2]) == 0)
					{
						num += 10;
					}
					if (Convert.ToInt32(array[num2]) == 1)
					{
						num += 18;
					}
					if (Convert.ToInt32(array[num2]) == 2)
					{
						num += 16;
					}
					if (Convert.ToInt32(array[num2]) == 3)
					{
						num += 14;
					}
					if (Convert.ToInt32(array[num2]) == 4)
					{
						num += 12;
					}
					if (Convert.ToInt32(array[num2]) == 5)
					{
						num += 10;
					}
					if (Convert.ToInt32(array[num2]) == 6)
					{
						num += 8;
					}
					if (Convert.ToInt32(array[num2]) == 7)
					{
						num += 6;
					}
					if (Convert.ToInt32(array[num2]) == 8)
					{
						num += 4;
					}
					if (Convert.ToInt32(array[num2]) == 9)
					{
						num += 2;
					}
				}
			}
			return num;
		}

		public static int RedQwQj(string balls)
		{
			if (balls != "")
			{
				balls = balls.Replace("_", "");
				string[] array = balls.Split(new char[]
				{
					','
				});
				int num = 1;
				if (array.Length == 5)
				{
					num = 1;
					for (int i = 2; i < array.Length; i++)
					{
						int length = array[i].Length;
						num *= length;
					}
					num = num * array[0].Length / 2 * array[1].Length / 2;
				}
				if (array.Length == 4)
				{
					num = 1;
					for (int j = 1; j < array.Length; j++)
					{
						int length2 = array[j].Length;
						num *= length2;
					}
					num = num * array[0].Length / 2;
				}
				if (array.Length == 3)
				{
					num = 1;
					for (int k = 1; k < array.Length; k++)
					{
						int length3 = array[k].Length;
						num *= length3;
					}
					num = num * array[0].Length / 2;
				}
				return num;
			}
			return 0;
		}

		public static int Red5ZuHe(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			balls = balls.Replace("_", "");
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length < 5)
			{
				return 0;
			}
			int num = 1;
			int num2 = 1;
			int num3 = 1;
			int num4 = 1;
			int num5 = 1;
			for (int i = 0; i < array.Length; i++)
			{
				num *= array[i].Length;
			}
			for (int i = 1; i < array.Length; i++)
			{
				num2 *= array[i].Length;
			}
			for (int i = 2; i < array.Length; i++)
			{
				num3 *= array[i].Length;
			}
			for (int i = 3; i < array.Length; i++)
			{
				num4 *= array[i].Length;
			}
			for (int i = 4; i < array.Length; i++)
			{
				num5 *= array[i].Length;
			}
			return num + num2 + num3 + num4 + num5;
		}

		public static int Red4ZuHe(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			balls = balls.Replace("_", "");
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length < 4)
			{
				return 0;
			}
			int num = 1;
			int num2 = 1;
			int num3 = 1;
			int num4 = 1;
			for (int i = 0; i < array.Length; i++)
			{
				num *= array[i].Length;
			}
			for (int i = 1; i < array.Length; i++)
			{
				num2 *= array[i].Length;
			}
			for (int i = 2; i < array.Length; i++)
			{
				num3 *= array[i].Length;
			}
			for (int i = 3; i < array.Length; i++)
			{
				num4 *= array[i].Length;
			}
			return num + num2 + num3 + num4;
		}

		public static int Red3ZuHe(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			balls = balls.Replace("_", "");
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length < 3)
			{
				return 0;
			}
			int num = 1;
			int num2 = 1;
			int num3 = 1;
			for (int i = 0; i < array.Length; i++)
			{
				num *= array[i].Length;
			}
			for (int i = 1; i < array.Length; i++)
			{
				num2 *= array[i].Length;
			}
			for (int i = 2; i < array.Length; i++)
			{
				num3 *= array[i].Length;
			}
			return num + num2 + num3;
		}

		public static int RedRXFS_11(string balls, int num)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				return Calculate.Combine(array.Length, num);
			}
			return 0;
		}

		public static int Red3FS_11(string balls)
		{
			int num = 0;
			if (balls != "")
			{
				if (!balls.Contains(","))
				{
					return 0;
				}
				string[] array = balls.Split(new char[]
				{
					','
				});
				if (array.Length == 3)
				{
					string[] array2 = array[0].Split(new char[]
					{
						'_'
					});
					string[] array3 = array[1].Split(new char[]
					{
						'_'
					});
					string[] array4 = array[2].Split(new char[]
					{
						'_'
					});
					for (int i = 0; i < array2.Length; i++)
					{
						if (array2[i].Length > 2)
						{
							return 0;
						}
						for (int j = 0; j < array3.Length; j++)
						{
							if (array3[j].Length > 2)
							{
								return 0;
							}
							if (array2[i] != array3[j])
							{
								for (int k = 0; k < array4.Length; k++)
								{
									if (array4[k].Length > 2)
									{
										return 0;
									}
									if (array4[k] != array2[i] && array4[k] != array3[j])
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

		public static int Red3ZFS_11(string balls)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Length > 2)
					{
						return 0;
					}
				}
				return Calculate.Combine(array.Length, 3);
			}
			return 0;
		}

		public static int Red2FS_11(string balls)
		{
			int num = 0;
			if (balls != "")
			{
				if (!balls.Contains(","))
				{
					return 0;
				}
				string[] array = balls.Split(new char[]
				{
					','
				});
				if (array.Length == 2)
				{
					string[] array2 = array[0].Split(new char[]
					{
						'_'
					});
					string[] array3 = array[1].Split(new char[]
					{
						'_'
					});
					for (int i = 0; i < array2.Length; i++)
					{
						if (array2[i].Length > 2)
						{
							return 0;
						}
						for (int j = 0; j < array3.Length; j++)
						{
							if (array3[j].Length > 2)
							{
								return 0;
							}
							if (array2[i] != array3[j])
							{
								num++;
							}
						}
					}
				}
			}
			return num;
		}

		public static int Red2ZFS_11(string balls)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Length > 2)
					{
						return 0;
					}
				}
				return array.Length * (array.Length - 1) / 2;
			}
			return 0;
		}

		public static int RedDD_11(string balls)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					','
				});
				int num = 0;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != "")
					{
						int num2 = array[i].Split(new char[]
						{
							'_'
						}).Length;
						num += num2;
					}
				}
				return num;
			}
			return 0;
		}

		public static int RedZH(string balls)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					','
				});
				int num = 5;
				for (int i = 0; i < array.Length; i++)
				{
					int length = array[i].Length;
					num *= length;
				}
				return num;
			}
			return 0;
		}

		public static int RedZu120(string balls)
		{
			if (balls != "")
			{
				int length = balls.Replace("_", "").Length;
				return Calculate.Pareto(length, 5) / Calculate.Pareto(5, 5);
			}
			return 0;
		}

		public static int RedZu60(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length > 1)
			{
				string[] array2 = array[0].Split(new char[]
				{
					'_'
				});
				string[] array3 = array[1].Split(new char[]
				{
					'_'
				});
				int num = 0;
				string text = "";
				for (int i = 0; i < array3.Length; i++)
				{
					for (int j = i; j < array3.Length; j++)
					{
						for (int k = j; k < array3.Length; k++)
						{
							if (array3[i] != array3[j] && array3[j] != array3[k] && array3[k] != array3[i])
							{
								string text2 = text;
								text = string.Concat(new string[]
								{
									text2,
									array3[i],
									array3[j],
									array3[k],
									","
								});
							}
						}
					}
				}
				text = text.Substring(0, text.Length - 1);
				string[] array4 = text.Split(new char[]
				{
					','
				});
				for (int l = 0; l < array4.Length; l++)
				{
					for (int m = 0; m < array2.Length; m++)
					{
						string text3 = array4[l];
						if (text3.IndexOf(array2[m]) == -1)
						{
							num++;
						}
					}
				}
				return num;
			}
			return 0;
		}

		public static int RedZu30(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length > 1)
			{
				string[] array2 = array[0].Split(new char[]
				{
					'_'
				});
				string[] array3 = array[1].Split(new char[]
				{
					'_'
				});
				int num = 0;
				string text = "";
				for (int i = 0; i < array2.Length; i++)
				{
					for (int j = i; j < array2.Length; j++)
					{
						if (array2[i] != array2[j])
						{
							text = text + array2[i] + array2[j] + ",";
						}
					}
				}
				text = text.Substring(0, text.Length - 1);
				string[] array4 = text.Split(new char[]
				{
					','
				});
				for (int k = 0; k < array4.Length; k++)
				{
					for (int l = 0; l < array3.Length; l++)
					{
						string text2 = array4[k];
						if (text2.IndexOf(array3[l]) == -1)
						{
							num++;
						}
					}
				}
				return num;
			}
			return 0;
		}

		public static int RedZu20(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length > 1)
			{
				string[] array2 = array[0].Split(new char[]
				{
					'_'
				});
				string[] array3 = array[1].Split(new char[]
				{
					'_'
				});
				int num = 0;
				string text = "";
				for (int i = 0; i < array3.Length; i++)
				{
					for (int j = i; j < array3.Length; j++)
					{
						if (array3[i] != array3[j])
						{
							text = text + array3[i] + array3[j] + ",";
						}
					}
				}
				text = text.Substring(0, text.Length - 1);
				string[] array4 = text.Split(new char[]
				{
					','
				});
				for (int k = 0; k < array4.Length; k++)
				{
					for (int l = 0; l < array2.Length; l++)
					{
						string text2 = array4[k];
						if (text2.IndexOf(array2[l]) == -1)
						{
							num++;
						}
					}
				}
				return num;
			}
			return 0;
		}

		public static int RedZu10(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length > 1)
			{
				string[] array2 = array[0].Split(new char[]
				{
					'_'
				});
				string[] array3 = array[1].Split(new char[]
				{
					'_'
				});
				int num = 0;
				string text = "";
				for (int i = 0; i < array3.Length; i++)
				{
					text = text + array3[i] + ",";
				}
				text = text.Substring(0, text.Length - 1);
				string[] array4 = text.Split(new char[]
				{
					','
				});
				for (int j = 0; j < array4.Length; j++)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						string text2 = array4[j];
						if (text2.IndexOf(array2[k]) == -1)
						{
							num++;
						}
					}
				}
				return num;
			}
			return 0;
		}

		public static int RedZu5(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length > 1)
			{
				string[] array2 = array[0].Split(new char[]
				{
					'_'
				});
				string[] array3 = array[1].Split(new char[]
				{
					'_'
				});
				int num = 0;
				string text = "";
				for (int i = 0; i < array3.Length; i++)
				{
					text = text + array3[i] + ",";
				}
				text = text.Substring(0, text.Length - 1);
				string[] array4 = text.Split(new char[]
				{
					','
				});
				for (int j = 0; j < array4.Length; j++)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						string text2 = array4[j];
						if (text2.IndexOf(array2[k]) == -1)
						{
							num++;
						}
					}
				}
				return num;
			}
			return 0;
		}

		public static int RedTS(string balls)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				int num = 0;
				for (int i = 0; i < array.Length; i++)
				{
					int length = array[i].Length;
					num += length;
				}
				return num;
			}
			return 0;
		}

		public static int RedZu24(string balls)
		{
			if (balls != "")
			{
				int length = balls.Replace("_", "").Length;
				return Calculate.Pareto(length, 4) / Calculate.Pareto(4, 4);
			}
			return 0;
		}

		public static int RedZu12(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length > 1)
			{
				string[] array2 = array[0].Split(new char[]
				{
					'_'
				});
				string[] array3 = array[1].Split(new char[]
				{
					'_'
				});
				int num = 0;
				string text = "";
				for (int i = 0; i < array3.Length; i++)
				{
					for (int j = i; j < array3.Length; j++)
					{
						if (array3[i] != array3[j])
						{
							text = text + array3[i] + array3[j] + ",";
						}
					}
				}
				text = text.Substring(0, text.Length - 1);
				string[] array4 = text.Split(new char[]
				{
					','
				});
				for (int k = 0; k < array4.Length; k++)
				{
					for (int l = 0; l < array2.Length; l++)
					{
						string text2 = array4[k];
						if (text2.IndexOf(array2[l]) == -1)
						{
							num++;
						}
					}
				}
				return num;
			}
			return 0;
		}

		public static int RedZu61(string balls)
		{
			if (balls != "")
			{
				int length = balls.Replace("_", "").Length;
				return Calculate.Pareto(length, 2) / Calculate.Pareto(2, 2);
			}
			return 0;
		}

		public static int RedZu4(string balls)
		{
			if (!(balls != ""))
			{
				return 0;
			}
			string[] array = balls.Split(new char[]
			{
				','
			});
			if (array.Length > 1)
			{
				string[] array2 = array[0].Split(new char[]
				{
					'_'
				});
				string[] array3 = array[1].Split(new char[]
				{
					'_'
				});
				int num = 0;
				string text = "";
				for (int i = 0; i < array3.Length; i++)
				{
					text = text + array3[i] + ",";
				}
				text = text.Substring(0, text.Length - 1);
				string[] array4 = text.Split(new char[]
				{
					','
				});
				for (int j = 0; j < array4.Length; j++)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						string text2 = array4[j];
						if (text2.IndexOf(array2[k]) == -1)
						{
							num++;
						}
					}
				}
				return num;
			}
			return 0;
		}

		public static int PK10FS_One(string balls)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				return Calculate.Combine(array.Length, 1);
			}
			return 0;
		}

		public static int PK10DXDS(string balls)
		{
			if (balls != "")
			{
				string[] array = balls.Split(new char[]
				{
					'_'
				});
				int num = 1;
				int num2 = array.Length;
				return num * num2;
			}
			return 0;
		}

		protected static string JsonResult(int success, string str)
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
	}
}
