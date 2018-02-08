using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class ActiveDAL : ComData
	{
		public void GetListJSON(int page, int PSize, string whereStr, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = whereStr;
				int num = dbOperHandler.Count("Act_ActiveRecord");
				string sql = SqlHelp.GetSql0(num + " as totalcount,row_number() over (order by Id desc) as rowid,[Id],[UserId],dbo.f_GetUserName(UserId) as UserName,[ActiveType],[ActiveName],[InMoney],[STime],[CheckIp],[CheckMachine],[FromUserId],[Remark]", "Act_ActiveRecord", "Id", PSize, page, "desc", whereStr);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetHBInfoJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT (isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)) as bet,\r\n                                            case when FLOOR(((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)))/1888)>8 then 8 else FLOOR(((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)))/1888) end as HbNum,\r\n                                            (select count(Id) from Act_ActiveRecord where ActiveType='ActHongBao' and UserId={0} and DateDiff(dd,STime,getdate())=0) as TodayHbNum,\r\n                                            (select isnull(sum(InMoney),0) from Act_ActiveRecord where ActiveType='ActHongBao' and UserId={0} and DateDiff(dd,STime,getdate())=0) as TodayHbMoney\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where UserId={0} and DateDiff(dd,STime,getdate())=0 ", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string SaveHbActive(string UserId, string ActiveType, string ActiveName, decimal InMoney, string Remark)
		{
			string clientIP = IPHelp.ClientIP;
			string fieldValue = "";
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='ActHongBao' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0]["count"]) > 3000)
				{
					jsonResult = base.GetJsonResult(0, "今日3000个红包也派发完毕，请明日继续参加！");
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT (isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)) as bet,\r\n                                           case when FLOOR(((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)))/1888)>8 then 8 else FLOOR(((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)))/1888) end as HbNum,\r\n                                            (select count(Id) from Act_ActiveRecord where ActiveType='ActHongBao' and UserId=a.UserId and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)) as TodayHbNum,\r\n                                            (select isnull(sum(InMoney),0) from Act_ActiveRecord where ActiveType='ActHongBao' and UserId=a.UserId and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)) as TodayHbMoney\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120) \r\n                                            group by UserId", UserId);
					dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						if (Convert.ToInt32(dataTable.Rows[0]["HbNum"]) - Convert.ToInt32(dataTable.Rows[0]["TodayHbNum"]) < 1)
						{
							jsonResult = base.GetJsonResult(0, "您没有可用的红包！");
						}
						else
						{
							string act = SsId.Act;
							dbOperHandler.Reset();
							dbOperHandler.AddFieldItem("SsId", act);
							dbOperHandler.AddFieldItem("UserId", UserId);
							dbOperHandler.AddFieldItem("ActiveType", ActiveType);
							dbOperHandler.AddFieldItem("ActiveName", ActiveName);
							dbOperHandler.AddFieldItem("InMoney", InMoney);
							dbOperHandler.AddFieldItem("Remark", Remark);
							dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
							dbOperHandler.AddFieldItem("CheckIp", clientIP);
							dbOperHandler.AddFieldItem("CheckMachine", fieldValue);
							if (dbOperHandler.Insert("Act_ActiveRecord") > 0)
							{
								new UserTotalTran().MoneyOpers(act, UserId, InMoney, 0, 0, 0, 9, 99, "", "", "红包领取", "");
								jsonResult = base.GetJsonResult(1, string.Concat(InMoney));
							}
							else
							{
								jsonResult = base.GetJsonResult(0, "红包领取失败！");
							}
						}
					}
					else
					{
						jsonResult = base.GetJsonResult(0, "您没有可用的红包！");
					}
				}
			}
			return jsonResult;
		}

		public void GetHistoryInfoJSON(ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT TOP 15 '恭喜 '+substring(dbo.f_GetUserName(UserId),1,2)+'***'+substring(dbo.f_GetUserName(UserId),len(dbo.f_GetUserName(UserId))-2,3)+' 获得红包 '+Convert(varchar(10),[InMoney])+' 元' as info FROM [Act_ActiveRecord] where ActiveType='ActHongBao' order by STime desc", new object[0]);
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetBetActiveInfoJSON(string UserId, ref string _jsonstr)
		{
			//DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT isnull(max(StartTime),'') as StartTime FROM [act_BetRecond] where type=0 and UserId={0}", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (!string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["StartTime"])))
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT top 10 [Id],[UserId],[StartTime],[STime],[IsFlag] FROM [act_BetRecond] where type=0 and UserId={0} and StartTime='{1}' order by STime desc", UserId, dataTable.Rows[0]["StartTime"].ToString());
					dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						DataRow[] array = dataTable.Select(string.Concat(new object[]
						{
							"STime ='",
							DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd"),
							"'"
						}), "STime desc");
						if (array.Length > 0)
						{
							array = dataTable.Select(string.Concat(new object[]
							{
								"STime ='",
								DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd"),
								"' and IsFlag=1"
							}), "STime desc");
							if (array.Length > 0)
							{
								TimeSpan timeSpan = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(array[0]["StartTime"]);
								array = dataTable.Select(string.Concat(new object[]
								{
									"STime ='",
									DateTime.Now.ToString("yyyy-MM-dd"),
									"' and IsFlag=1"
								}), "STime desc");
								if (array.Length > 0)
								{
									_jsonstr = "[{\"result\" :\"" + (timeSpan.Days + 1) + "\",\"flag\" :\"1\"}]";
								}
								else
								{
									_jsonstr = "[{\"result\" :\"" + (timeSpan.Days + 1) + "\",\"flag\" :\"0\"}]";
								}
							}
							else
							{
								_jsonstr = "[{\"result\" :\"1\",\"flag\" :\"0\"}]";
							}
						}
						else
						{
							array = dataTable.Select(string.Concat(new object[]
							{
								"STime ='",
								DateTime.Now.ToString("yyyy-MM-dd"),
								"' and IsFlag=1"
							}), "STime desc");
							if (array.Length > 0)
							{
								_jsonstr = "[{\"result\" :\"1\",\"flag\" :\"1\"}]";
							}
							else
							{
								_jsonstr = "[{\"result\" :\"1\",\"flag\" :\"0\"}]";
							}
						}
					}
					else
					{
						_jsonstr = "[{\"result\" :\"1\",\"flag\" :\"0\"}]";
					}
				}
				else
				{
					_jsonstr = "[{\"result\" :\"1\",\"flag\" :\"0\"}]";
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string SaveBetActive(string UserId, string ActiveType, string ActiveName, string Remark)
		{
			string clientIP = IPHelp.ClientIP;
			string fieldValue = "";
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='ActBet'  and UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0]["count"]) > 0)
				{
					jsonResult = base.GetJsonResult(0, "今日已领取，请明日继续参加！");
				}
				else
				{
					string conditionValue = DateTime.Now.ToString("yyyy-MM-dd");
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT isnull(max(StartTime),'') as StartTime FROM [act_BetRecond] where type=0 and UserId={0}", UserId);
					dataTable = dbOperHandler.GetDataTable();
					int num;
					bool flag;
					if (!string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["StartTime"])))
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT top 10 [Id],[UserId],[StartTime],[STime],[IsFlag] FROM [Act_BetRecond] where type=0 and UserId={0} and StartTime='{1}' order by STime desc", UserId, dataTable.Rows[0]["StartTime"].ToString());
						dataTable = dbOperHandler.GetDataTable();
						if (dataTable.Rows.Count > 0)
						{
							DataRow[] array = dataTable.Select(string.Concat(new object[]
							{
								"STime ='",
								DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd"),
								"'"
							}), "STime desc");
							if (array.Length > 0)
							{
								array = dataTable.Select(string.Concat(new object[]
								{
									"STime ='",
									DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd"),
									"' and IsFlag=1"
								}), "STime desc");
								if (array.Length > 0)
								{
									num = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(array[0]["StartTime"])).Days + 1;
									flag = true;
								}
								else
								{
									flag = false;
									num = 1;
								}
							}
							else
							{
								flag = false;
								num = 1;
							}
						}
						else
						{
							flag = false;
							num = 1;
						}
					}
					else
					{
						flag = false;
						num = 1;
					}
					decimal num2 = 0m;
					decimal d = 0m;
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT (isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)) as bet FROM [N_UserMoneyStatAll] a \r\n                                              where UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120) ", UserId);
					dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						d = Convert.ToDecimal(dataTable.Rows[0]["bet"]);
					}
					if (num == 1)
					{
						if (!(d >= 1888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 8m;
					}
					if (num == 2)
					{
						if (!(d >= 5888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 28m;
					}
					if (num == 3)
					{
						if (!(d >= 8888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 38m;
					}
					if (num == 4)
					{
						if (!(d >= 18888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 88m;
					}
					if (num == 5)
					{
						if (!(d >= 38888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 128m;
					}
					if (num == 6)
					{
						if (!(d >= 58888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 238m;
					}
					if (num == 7)
					{
						if (!(d >= 88888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 358m;
					}
					if (num == 8)
					{
						if (!(d >= 188888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 588m;
					}
					if (num == 9)
					{
						if (!(d >= 388888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 1288m;
					}
					if (num == 10)
					{
						if (!(d >= 588888m))
						{
							jsonResult = base.GetJsonResult(0, "消费未达标，请继续努力！");
							return jsonResult;
						}
						num2 = 2588m;
					}
					if (!flag)
					{
						for (int i = 0; i < 10; i++)
						{
							dbOperHandler.Reset();
							dbOperHandler.AddFieldItem("UserId", UserId);
							dbOperHandler.AddFieldItem("Type", 0);
							dbOperHandler.AddFieldItem("StartTime", DateTime.Now.ToString("yyyy-MM-dd"));
							dbOperHandler.AddFieldItem("STime", DateTime.Now.AddDays((double)i).ToString("yyyy-MM-dd"));
							if (i == 0)
							{
								dbOperHandler.AddFieldItem("IsFlag", 1);
							}
							else
							{
								dbOperHandler.AddFieldItem("IsFlag", 0);
							}
							dbOperHandler.Insert("Act_BetRecond");
						}
					}
					else
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "UserId=@UserId and STime=@STime and Type=0";
						dbOperHandler.AddConditionParameter("@UserId", UserId);
						dbOperHandler.AddConditionParameter("@STime", conditionValue);
						dbOperHandler.AddFieldItem("IsFlag", 1);
						dbOperHandler.Update("Act_BetRecond");
					}
					string act = SsId.Act;
					dbOperHandler.Reset();
					dbOperHandler.AddFieldItem("SsId", act);
					dbOperHandler.AddFieldItem("UserId", UserId);
					dbOperHandler.AddFieldItem("ActiveType", ActiveType);
					dbOperHandler.AddFieldItem("ActiveName", ActiveName);
					dbOperHandler.AddFieldItem("InMoney", num2);
					dbOperHandler.AddFieldItem("Remark", Remark);
					dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					dbOperHandler.AddFieldItem("CheckIp", clientIP);
					dbOperHandler.AddFieldItem("CheckMachine", fieldValue);
					if (dbOperHandler.Insert("Act_ActiveRecord") > 0)
					{
						new UserTotalTran().MoneyOpers(act, UserId, num2, 0, 0, 0, 9, 0, "消费大闯关领取", "您消费大闯关领取" + num2 + "元", "消费大闯关", "");
						jsonResult = base.GetJsonResult(1, string.Concat(num2));
					}
					else
					{
						jsonResult = base.GetJsonResult(0, "消费大闯关领取失败！");
					}
				}
			}
			return jsonResult;
		}

		public void GetChargeJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney],\r\n                                                                case when isnull([InMoney],0)>100 then '50.00' else '0' end as money,\r\n                                                                (SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) FROM [N_UserMoneyStatAll]\r\n                                                                where UserId=a.UserId and STime>a.STime) as bet\r\n                                                                FROM [N_UserCharge] a where UserId={0}  and DateDiff(dd,STime,getdate())=0 \r\n                                                                order by STime asc ", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string SaveChargeActive(string UserId)
		{
			return base.GetJsonResult(0, "活动已关闭！");
		}

		public void GetGroup3GzJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				string arg = dataTable.Rows[0]["bet"].ToString();
				string arg2 = dataTable.Rows[0]["hyNum"].ToString();
				if (dataTable.Rows.Count > 0)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", arg, arg2);
					dataTable = dbOperHandler.GetDataTable();
				}
				if (dataTable.Rows.Count < 1)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT {0} as bet,'未得到条件' as money", arg);
					dataTable = dbOperHandler.GetDataTable();
				}
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetGroup3IphoneGzJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				string arg = dataTable.Rows[0]["bet"].ToString();
				string arg2 = dataTable.Rows[0]["hyNum"].ToString();
				if (dataTable.Rows.Count > 0)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", arg, arg2);
					dataTable = dbOperHandler.GetDataTable();
				}
				if (dataTable.Rows.Count < 1)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT {0} as bet,'未得到条件' as money", arg);
					dataTable = dbOperHandler.GetDataTable();
				}
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string SaveGroup3Active(string UserId)
		{
			string clientIP = IPHelp.ClientIP;
			string fieldValue = "";
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("  select Id from Act_ActiveSet where Code='ActGongZi' and [IsUse]=0 and getdate()>=StartTime and getdate()<=EndTime", new object[0]);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count < 1)
				{
					jsonResult = base.GetJsonResult(0, "活动已关闭，请等待活动开放！");
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("select Id from N_User where UserGroup=3 and Id={0}", UserId);
					dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count < 1)
					{
						jsonResult = base.GetJsonResult(0, "您无权领取日奖励工资！");
					}
					else
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where UserId={0} and  ActiveType='ActGongZi3' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", UserId);
						dataTable = dbOperHandler.GetDataTable();
						if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0]["count"]) > 0)
						{
							jsonResult = base.GetJsonResult(0, "您已领取活动，请明日再参加！");
						}
						else
						{
							dbOperHandler.Reset();
							dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", UserId);
							dataTable = dbOperHandler.GetDataTable();
							string arg = dataTable.Rows[0]["bet"].ToString();
							string arg2 = dataTable.Rows[0]["hyNum"].ToString();
							if (dataTable.Rows.Count > 0)
							{
								dbOperHandler.Reset();
								dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", arg, arg2);
								dataTable = dbOperHandler.GetDataTable();
								if (dataTable.Rows.Count < 1)
								{
									jsonResult = base.GetJsonResult(0, "未得到最低消费标准或活跃人数不足，不能领取！");
								}
								else
								{
									string act = SsId.Act;
									decimal num = Convert.ToDecimal(dataTable.Rows[0]["money"].ToString());
									dbOperHandler.Reset();
									dbOperHandler.AddFieldItem("SsId", act);
									dbOperHandler.AddFieldItem("UserId", UserId);
									dbOperHandler.AddFieldItem("ActiveType", "ActGongZi3");
									dbOperHandler.AddFieldItem("ActiveName", "日奖励工资");
									dbOperHandler.AddFieldItem("Bet", Convert.ToDecimal(dataTable.Rows[0]["bet"]));
									dbOperHandler.AddFieldItem("InMoney", num);
									dbOperHandler.AddFieldItem("Remark", "日奖励工资");
									dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
									dbOperHandler.AddFieldItem("CheckIp", clientIP);
									dbOperHandler.AddFieldItem("CheckMachine", fieldValue);
									if (dbOperHandler.Insert("Act_ActiveRecord") > 0)
									{
										new UserTotalTran().MoneyOpers(act, UserId, num, 0, 0, 0, 9, 99, "", "", "日奖励工资领取", "");
										jsonResult = base.GetJsonResult(1, "您成功领取日奖励工资" + num + "元");
									}
									else
									{
										jsonResult = base.GetJsonResult(0, "日奖励工资领取失败！");
									}
								}
							}
							else
							{
								jsonResult = base.GetJsonResult(0, "您的团队昨日消费未消费，不能领取！");
							}
						}
					}
				}
			}
			return jsonResult;
		}

		public void GetGroup2GzJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				string arg = dataTable.Rows[0]["bet"].ToString();
				string arg2 = dataTable.Rows[0]["hyNum"].ToString();
				if (dataTable.Rows.Count > 0)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail2] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", arg, arg2);
					dataTable = dbOperHandler.GetDataTable();
				}
				if (dataTable.Rows.Count < 1)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT {0} as bet,'未得到条件' as money", arg);
					dataTable = dbOperHandler.GetDataTable();
				}
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string SaveGroup2Active(string UserId)
		{
			string clientIP = IPHelp.ClientIP;
			string fieldValue = "";
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("  select Id from Act_ActiveSet where Code='ActGongZi2' and [IsUse]=0 and getdate()>=StartTime and getdate()<=EndTime", new object[0]);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count < 1)
				{
					jsonResult = base.GetJsonResult(0, "活动已关闭，请等待活动开放！");
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("select Id from N_User where UserGroup=2 and Id={0}", UserId);
					dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count < 1)
					{
						jsonResult = base.GetJsonResult(0, "您无权领取日奖励工资！");
					}
					else
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where UserId={0} and  ActiveType='ActGongZi2' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", UserId);
						dataTable = dbOperHandler.GetDataTable();
						if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0]["count"]) > 0)
						{
							jsonResult = base.GetJsonResult(0, "您已领取活动，请明日再参加！");
						}
						else
						{
							dbOperHandler.Reset();
							dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", UserId);
							dataTable = dbOperHandler.GetDataTable();
							string arg = dataTable.Rows[0]["bet"].ToString();
							string arg2 = dataTable.Rows[0]["hyNum"].ToString();
							if (dataTable.Rows.Count > 0)
							{
								dbOperHandler.Reset();
								dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail2] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", arg, arg2);
								dataTable = dbOperHandler.GetDataTable();
								if (dataTable.Rows.Count < 1)
								{
									jsonResult = base.GetJsonResult(0, "未得到最低消费标准或活跃人数不足，不能领取！");
								}
								else
								{
									string act = SsId.Act;
									decimal num = Convert.ToDecimal(dataTable.Rows[0]["money"].ToString());
									dbOperHandler.Reset();
									dbOperHandler.AddFieldItem("SsId", act);
									dbOperHandler.AddFieldItem("UserId", UserId);
									dbOperHandler.AddFieldItem("ActiveType", "ActGongZi2");
									dbOperHandler.AddFieldItem("ActiveName", "日奖励工资");
									dbOperHandler.AddFieldItem("Bet", Convert.ToDecimal(dataTable.Rows[0]["bet"]));
									dbOperHandler.AddFieldItem("InMoney", num);
									dbOperHandler.AddFieldItem("Remark", "日奖励工资");
									dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
									dbOperHandler.AddFieldItem("CheckIp", clientIP);
									dbOperHandler.AddFieldItem("CheckMachine", fieldValue);
									if (dbOperHandler.Insert("Act_ActiveRecord") > 0)
									{
										new UserTotalTran().MoneyOpers(act, UserId, num, 0, 0, 0, 9, 99, "", "", "日奖励工资领取", "");
										jsonResult = base.GetJsonResult(1, "您成功领取日奖励工资" + num + "元");
									}
									else
									{
										jsonResult = base.GetJsonResult(0, "日奖励工资领取失败！");
									}
								}
							}
							else
							{
								jsonResult = base.GetJsonResult(0, "您的团队昨日消费未消费，不能领取！");
							}
						}
					}
				}
			}
			return jsonResult;
		}

		public void GetHyChargeJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney],\r\n                                            case \r\n                                            when isnull([InMoney],0)>=500 and isnull([InMoney],0)<1000 then '8.00' \r\n                                            when isnull([InMoney],0)>=1000 and isnull([InMoney],0)<2000 then '18.00' \r\n                                            when isnull([InMoney],0)>=2000 and isnull([InMoney],0)<5000 then '28.00' \r\n                                            when isnull([InMoney],0)>=5000 and isnull([InMoney],0)<10000 then '38.00' \r\n                                            when isnull([InMoney],0)>=10000 and isnull([InMoney],0)<20000 then '68.00' \r\n                                            when isnull([InMoney],0)>=20000 and isnull([InMoney],0)<30000 then '128.00' \r\n                                            when isnull([InMoney],0)>=30000 and isnull([InMoney],0)<50000 then '188.00' \r\n                                            when isnull([InMoney],0)>=50000 then '288.00' \r\n                                            else '0' end as money\r\n                                            FROM [N_UserCharge] a where UserId={0} and State=1 and DateDiff(dd,STime,getdate())=0 \r\n                                            order by STime asc ", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string SaveHyChargeActive(string UserId)
		{
			return base.GetJsonResult(0, "活动已关闭！");
		}

		public void GetChargeActiveInfoJSON(string UserId, ref string _jsonstr)
		{
			//DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT isnull(max(StartTime),'') as StartTime FROM [act_BetRecond] where type=1 and UserId={0}", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				int num;
				int num2;
				if (!string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["StartTime"])))
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT top 7 [Id],[UserId],[StartTime],[STime],[IsFlag] FROM [act_BetRecond] where type=1 and UserId={0} and StartTime='{1}' order by STime desc", UserId, dataTable.Rows[0]["StartTime"].ToString());
					dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						DataRow[] array = dataTable.Select(string.Concat(new object[]
						{
							"STime ='",
							DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd"),
							"'"
						}), "STime desc");
						if (array.Length > 0)
						{
							array = dataTable.Select(string.Concat(new object[]
							{
								"STime ='",
								DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd"),
								"' and IsFlag=1"
							}), "STime desc");
							if (array.Length > 0)
							{
								TimeSpan timeSpan = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(array[0]["StartTime"]);
								array = dataTable.Select(string.Concat(new object[]
								{
									"STime ='",
									DateTime.Now.ToString("yyyy-MM-dd"),
									"' and IsFlag=1"
								}), "STime desc");
								if (array.Length > 0)
								{
									num = timeSpan.Days + 1;
									num2 = 1;
								}
								else
								{
									num = timeSpan.Days + 1;
									num2 = 0;
								}
							}
							else
							{
								num = 1;
								num2 = 0;
							}
						}
						else
						{
							array = dataTable.Select(string.Concat(new object[]
							{
								"STime ='",
								DateTime.Now.ToString("yyyy-MM-dd"),
								"' and IsFlag=1"
							}), "STime desc");
							if (array.Length > 0)
							{
								num = 1;
								num2 = 1;
							}
							else
							{
								num = 1;
								num2 = 0;
							}
						}
					}
					else
					{
						num = 1;
						num2 = 0;
					}
				}
				else
				{
					num = 1;
					num2 = 0;
				}
				decimal num3 = 0m;
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney] FROM [N_UserCharge] a where UserId={0} and State=1 and DateDiff(dd,STime,getdate())=0 order by STime asc ", UserId);
				dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					num3 = Convert.ToDecimal(dataTable.Rows[0]["InMoney"].ToString());
				}
				string arg = "M0";
				if (num3 >= 500m)
				{
					arg = "M500";
				}
				if (num3 >= 1000m)
				{
					arg = "M1000";
				}
				if (num3 >= 3000m)
				{
					arg = "M3000";
				}
				if (num3 >= 5000m)
				{
					arg = "M5000";
				}
				if (num3 >= 10000m)
				{
					arg = "M10000";
				}
				if (num3 >= 20000m)
				{
					arg = "M20000";
				}
				if (num3 >= 30000m)
				{
					arg = "M30000";
				}
				if (num3 >= 50000m)
				{
					arg = "M50000";
				}
				decimal num4 = 0m;
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 {0} as give FROM [Act_SetChargeDetail] where Name='{1}'", arg, num);
				dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					num4 = Convert.ToDecimal(dataTable.Rows[0]["give"].ToString());
				}
				dataTable.Clear();
				dataTable.Dispose();
				_jsonstr = string.Concat(new object[]
				{
					"[{\"day\" :\"",
					num,
					"\",\"flag\" :\"",
					num2,
					"\",\"charge\" :\"",
					num3,
					"\",\"give\" :\"",
					num4,
					"\"}]"
				});
			}
		}

		public string SaveChargeActive(string UserId, string ActiveType, string ActiveName, string Remark)
		{
			string clientIP = IPHelp.ClientIP;
			string fieldValue = "";
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='ActCharge'  and UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0]["count"]) > 0)
				{
					jsonResult = base.GetJsonResult(0, "今日已领取，请明日继续参加！");
				}
				else
				{
					string conditionValue = DateTime.Now.ToString("yyyy-MM-dd");
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT isnull(max(StartTime),'') as StartTime FROM [act_BetRecond] where type=1 and UserId={0}", UserId);
					dataTable = dbOperHandler.GetDataTable();
					int num;
					bool flag;
					if (!string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["StartTime"])))
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT top 7 [Id],[UserId],[StartTime],[STime],[IsFlag] FROM [act_BetRecond] where type=1 and UserId={0} and StartTime='{1}' order by STime desc", UserId, dataTable.Rows[0]["StartTime"].ToString());
						dataTable = dbOperHandler.GetDataTable();
						if (dataTable.Rows.Count > 0)
						{
							DataRow[] array = dataTable.Select(string.Concat(new object[]
							{
								"STime ='",
								DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd"),
								"'"
							}), "STime desc");
							if (array.Length > 0)
							{
								array = dataTable.Select(string.Concat(new object[]
								{
									"STime ='",
									DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd"),
									"' and IsFlag=1"
								}), "STime desc");
								if (array.Length > 0)
								{
									num = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(array[0]["StartTime"])).Days + 1;
									flag = true;
								}
								else
								{
									flag = false;
									num = 1;
								}
							}
							else
							{
								flag = false;
								num = 1;
							}
						}
						else
						{
							flag = false;
							num = 1;
						}
					}
					else
					{
						flag = false;
						num = 1;
					}
					decimal num2 = 0m;
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney] FROM [N_UserCharge] a where UserId={0} and State=1 and DateDiff(dd,STime,getdate())=0 order by STime asc ", UserId);
					dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						num2 = Convert.ToDecimal(dataTable.Rows[0]["InMoney"].ToString());
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT (isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)) as bet FROM [N_UserMoneyStatAll] a \r\n                                              where UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120) ", UserId);
						dataTable = dbOperHandler.GetDataTable();
						if (dataTable.Rows.Count <= 0)
						{
							jsonResult = base.GetJsonResult(0, "消费未得到首次充值金额的一倍，不能领取！");
							return jsonResult;
						}
						if (Convert.ToDecimal(dataTable.Rows[0]["bet"].ToString()) < num2)
						{
							jsonResult = base.GetJsonResult(0, "消费未得到首次充值金额的一倍，不能领取！");
							return jsonResult;
						}
					}
					string arg = "M0";
					if (num2 >= 500m)
					{
						arg = "M500";
					}
					if (num2 >= 1000m)
					{
						arg = "M1000";
					}
					if (num2 >= 3000m)
					{
						arg = "M3000";
					}
					if (num2 >= 5000m)
					{
						arg = "M5000";
					}
					if (num2 >= 10000m)
					{
						arg = "M10000";
					}
					if (num2 >= 20000m)
					{
						arg = "M20000";
					}
					if (num2 >= 30000m)
					{
						arg = "M30000";
					}
					if (num2 >= 50000m)
					{
						arg = "M50000";
					}
					decimal num3 = 0m;
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 {0} as give FROM [Act_SetChargeDetail] where Name='{1}'", arg, num);
					dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						num3 = Convert.ToDecimal(dataTable.Rows[0]["give"].ToString());
					}
					if (num3 > 0m)
					{
						if (!flag)
						{
							for (int i = 0; i < 7; i++)
							{
								dbOperHandler.Reset();
								dbOperHandler.AddFieldItem("UserId", UserId);
								dbOperHandler.AddFieldItem("Type", 1);
								dbOperHandler.AddFieldItem("StartTime", DateTime.Now.ToString("yyyy-MM-dd"));
								dbOperHandler.AddFieldItem("STime", DateTime.Now.AddDays((double)i).ToString("yyyy-MM-dd"));
								if (i == 0)
								{
									dbOperHandler.AddFieldItem("IsFlag", 1);
								}
								else
								{
									dbOperHandler.AddFieldItem("IsFlag", 0);
								}
								dbOperHandler.Insert("Act_BetRecond");
							}
						}
						else
						{
							dbOperHandler.Reset();
							dbOperHandler.ConditionExpress = "UserId=@UserId and STime=@STime and Type=1";
							dbOperHandler.AddConditionParameter("@UserId", UserId);
							dbOperHandler.AddConditionParameter("@STime", conditionValue);
							dbOperHandler.AddFieldItem("IsFlag", 1);
							dbOperHandler.Update("Act_BetRecond");
						}
						string act = SsId.Act;
						dbOperHandler.Reset();
						dbOperHandler.AddFieldItem("SsId", act);
						dbOperHandler.AddFieldItem("UserId", UserId);
						dbOperHandler.AddFieldItem("ActiveType", ActiveType);
						dbOperHandler.AddFieldItem("ActiveName", ActiveName);
						dbOperHandler.AddFieldItem("InMoney", num3);
						dbOperHandler.AddFieldItem("Remark", Remark);
						dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
						dbOperHandler.AddFieldItem("CheckIp", clientIP);
						dbOperHandler.AddFieldItem("CheckMachine", fieldValue);
						if (dbOperHandler.Insert("Act_ActiveRecord") > 0)
						{
							new UserTotalTran().MoneyOpers(act, UserId, num3, 0, 0, 0, 9, 0, "首充大闯关领取", "您首充大闯关领取" + num3 + "元", "首充大闯关", "");
							jsonResult = base.GetJsonResult(1, string.Concat(num3));
						}
						else
						{
							jsonResult = base.GetJsonResult(0, "首充大闯关领取失败！");
						}
					}
					else
					{
						jsonResult = base.GetJsonResult(0, "首充未达标，请继续努力！");
					}
				}
			}
			return jsonResult;
		}

		public void GetGroupGzJSON(string GroupId, string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and (Bet-Cancellation)>1000 and DateDiff(dd,STime,getdate())=0 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=0 ", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				string text = dataTable.Rows[0]["bet"].ToString();
				string text2 = dataTable.Rows[0]["hyNum"].ToString();
				if (dataTable.Rows.Count > 0)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT top 1 {1} as bet,{2} as hynum,cast(round([Money]*{1}*0.01,4) as numeric(10,4)) as money FROM [Act_DayGzSet] where GroupId={0} and IsUsed=0 and MinMoney*10000<={1} and MinUsers<={2} order by Id desc", GroupId, text, text2);
					dataTable = dbOperHandler.GetDataTable();
				}
				if (dataTable.Rows.Count < 1)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT {0} as bet,{1} as hynum,'未得到条件' as money", text, text2);
					dataTable = dbOperHandler.GetDataTable();
				}
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetRegChargeJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney],\r\n                                            case when isnull([InMoney],0)>100 then '18.00' else '0' end as money\r\n                                            FROM [N_UserCharge] a where state=1 and UserId={0}\r\n                                            order by STime asc ", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count < 1)
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 0 as [InMoney],0 as money", new object[0]);
					dataTable = dbOperHandler.GetDataTable();
				}
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string SaveRegCharge(string UserId)
		{
			string clientIP = IPHelp.ClientIP;
			string fieldValue = "";
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT [Question],[Answer],[TrueName],b.* FROM [N_User] a left join N_UserBank b on a.Id=b.UserId where a.Id={0}", UserId);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (string.IsNullOrEmpty(dataTable.Rows[0]["TrueName"].ToString()))
					{
						jsonResult = base.GetJsonResult(0, "请您绑定真实姓名！");
						return jsonResult;
					}
					if (string.IsNullOrEmpty(dataTable.Rows[0]["Question"].ToString()))
					{
						jsonResult = base.GetJsonResult(0, "请您绑定密保问题！");
						return jsonResult;
					}
					if (string.IsNullOrEmpty(dataTable.Rows[0]["PayAccount"].ToString()))
					{
						jsonResult = base.GetJsonResult(0, "请您绑定银行资料！");
						return jsonResult;
					}
				}
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='RegCharge'", UserId);
				dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0]["count"]) > 0)
				{
					jsonResult = base.GetJsonResult(0, "您也领取，本活动只有首次绑定资料后充值领取！");
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney],\r\n                                            case when isnull([InMoney],0)>100 then '18.00' else '0' end as money\r\n                                            FROM [N_UserCharge] a where state=1 and UserId={0}\r\n                                            order by STime asc ", UserId);
					dataTable = dbOperHandler.GetDataTable();
					if (dataTable.Rows.Count > 0)
					{
						if (Convert.ToDecimal(dataTable.Rows[0]["InMoney"]) < 100m)
						{
							jsonResult = base.GetJsonResult(0, "您的首充金额不足100元，不能领取！");
						}
						else
						{
							string act = SsId.Act;
							decimal num = Convert.ToDecimal(dataTable.Rows[0]["money"].ToString());
							dbOperHandler.Reset();
							dbOperHandler.AddFieldItem("SsId", act);
							dbOperHandler.AddFieldItem("UserId", UserId);
							dbOperHandler.AddFieldItem("ActiveType", "RegCharge");
							dbOperHandler.AddFieldItem("ActiveName", "注册首充佣金");
							dbOperHandler.AddFieldItem("InMoney", num);
							dbOperHandler.AddFieldItem("Remark", "注册首充佣金");
							dbOperHandler.AddFieldItem("STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
							dbOperHandler.AddFieldItem("CheckIp", clientIP);
							dbOperHandler.AddFieldItem("CheckMachine", fieldValue);
							if (dbOperHandler.Insert("Act_ActiveRecord") > 0)
							{
								new UserTotalTran().MoneyOpers(act, UserId, num, 0, 0, 0, 9, 99, "", "", "注册首充佣金", "");
								jsonResult = base.GetJsonResult(1, "您成功领取注册首充佣金" + num + "元");
							}
							else
							{
								jsonResult = base.GetJsonResult(0, "注册首充佣金领取失败！");
							}
						}
					}
					else
					{
						jsonResult = base.GetJsonResult(0, "您还未充值，不能领取！");
					}
				}
			}
			return jsonResult;
		}
	}
}
