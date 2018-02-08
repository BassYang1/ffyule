using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class UserGetCashDAL : ComData
	{
		public UserGetCashDAL()
		{
			this.site = new conSite().GetSite();
		}

		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				dbOperHandler.Count("Flex_UserGetCash");
				string sql = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,0.0000 as sxf,*", "Flex_UserGetCash", "Id", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetIphoneListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("Flex_UserGetCash");
				string sql = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,0.0000 as sxf,*", "Flex_UserGetCash", "Id", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public DataTable GetListDataTable(string Id)
		{
			DataTable result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select * from Flex_UserGetCash where state=0 and Id=" + Id;
				DataTable dataTable = dbOperHandler.GetDataTable();
				result = dataTable;
			}
			return result;
		}

		public string UserGetCash(string UserId, string UserBankId, string BankId, string Money, string PassWord)
		{
			string result;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 1 * from Sys_Bank where id=" + BankId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (Convert.ToInt32(dataTable.Rows[0]["IsUsed"]) == 1)
				{
					result = "取款失败,当前银行禁止取款!";
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "id=@id";
					dbOperHandler.AddConditionParameter("@id", UserId);
					object[] fields = dbOperHandler.GetFields("N_User", "Money,PayPass,IsGetCash,EnableSeason,UserGroup");
					if (fields.Length > 0)
					{
						int num = Convert.ToInt32(fields[2]);
						fields[3].ToString();
						if (num != 0)
						{
							result = "取款失败,您的帐号禁止取款!";
						}
						else if (Convert.ToDecimal(Money) > Convert.ToDecimal(fields[0]))
						{
							result = "您的可用余额不足";
						}
						else if (!MD5.Last64(MD5.Lower32(PassWord)).Equals(fields[1].ToString()))
						{
							result = "您的提现密码错误";
						}
						else
						{
							dbOperHandler.Reset();
							dbOperHandler.SqlCmd = string.Format("select STime from Act_ActiveRecord where UserId={0} and ActiveType='Charge' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", UserId);
							DataTable dataTable2 = dbOperHandler.GetDataTable();
							if (dataTable2.Rows.Count > 0)
							{
								if (Convert.ToDecimal(fields[0]) - Convert.ToDecimal(Money) < 50m)
								{
									dbOperHandler.Reset();
									dbOperHandler.SqlCmd = string.Format("SELECT cast(round(isnull(Sum(Total*Times),0),4) as numeric(20,4)) as bet FROM [N_UserBet]\r\n                                                                where UserId={0} and (state=2 or state=3) and STime>'{1}' ", UserId, dataTable2.Rows[0]["STime"].ToString());
									dataTable2 = dbOperHandler.GetDataTable();
									if (dataTable2.Rows.Count > 0 && Convert.ToDecimal(dataTable2.Rows[0]["bet"]) < 800m)
									{
										result = "首充佣金50元不能体现，您的消费未满800元！";
										return result;
									}
								}
							}
							else if (Convert.ToInt32(fields[4].ToString()) < 2)
							{
								dbOperHandler.Reset();
								dbOperHandler.SqlCmd = "SELECT (isnull(sum(Bet),0)-isnull(sum(Cancellation),0)) as bet,isnull(sum(charge),0) as charge FROM [N_UserMoneyStatAll] with(nolock) where userId=" + UserId;
								DataTable dataTable3 = dbOperHandler.GetDataTable();
								double num2 = Convert.ToDouble(dataTable3.Rows[0]["bet"].ToString());
								double num3 = Convert.ToDouble(dataTable3.Rows[0]["charge"].ToString());
								if (num3 > 0.0 && num2 * 100.0 / num3 < Convert.ToDouble(dataTable.Rows[0]["BetPerCheck"]))
								{
									result = "对不起，您未消费到充值的" + dataTable.Rows[0]["BetPerCheck"] + "%，不能提现！";
									return result;
								}
							}
							if (Convert.ToDecimal(Money) < Convert.ToDecimal(dataTable.Rows[0]["MinCharge"]))
							{
								result = "提现金额不能小于单笔最小金额";
							}
							else if (Convert.ToDecimal(Money) > Convert.ToDecimal(dataTable.Rows[0]["MaxCharge"]))
							{
								result = "提现金额不能大于单笔最大金额";
							}
							else
							{
								DateTime t = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + dataTable.Rows[0]["StartTime"]);
								DateTime t2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + dataTable.Rows[0]["EndTime"]);
								DateTime now = DateTime.Now;
								if (t.Hour >= t2.Hour)
								{
									if (now < t && now > t2)
									{
										result = string.Concat(new object[]
										{
											"提现时间为",
											dataTable.Rows[0]["StartTime"],
											"至",
											dataTable.Rows[0]["EndTime"]
										});
										return result;
									}
								}
								else if (now < t || now > t2)
								{
									result = string.Concat(new object[]
									{
										"提现时间为",
										dataTable.Rows[0]["StartTime"],
										"至",
										dataTable.Rows[0]["EndTime"]
									});
									return result;
								}
								string value = "0";
								dbOperHandler.Reset();
								dbOperHandler.SqlCmd = "select count(*) as txcs,isnull(sum(Money),0) as txje from N_UserGetCash where userId=" + UserId + " and datediff(d,STime,getdate())=0 and State<>2";
								DataTable dataTable4 = dbOperHandler.GetDataTable();
								if (dataTable4.Rows.Count > 0)
								{
									value = dataTable4.Rows[0]["txcs"].ToString();
									dataTable4.Rows[0]["txje"].ToString();
								}
								if (Convert.ToDecimal(value) > Convert.ToDecimal(dataTable.Rows[0]["MaxGetCash"]))
								{
									result = "今日提现已得到最大提现次数";
								}
								else
								{
									dbOperHandler.Reset();
									dbOperHandler.SqlCmd = "SELECT [PayBank],[PayAccount],[PayName] FROM [N_UserBank] where UserId=" + UserId + " and Id=" + UserBankId;
									dataTable4 = dbOperHandler.GetDataTable();
									if (dataTable4.Rows.Count > 0)
									{
										if (this.Save(UserId, UserBankId, dataTable4.Rows[0]["PayBank"].ToString(), dataTable4.Rows[0]["PayAccount"].ToString(), dataTable4.Rows[0]["PayName"].ToString(), Convert.ToDecimal(Money)) > 0)
										{
											new LogSysDAL().Save("会员管理", "Id为" + UserId + "的会员申请提现！");
											result = "申请提现成功！";
										}
										else
										{
											result = "申请提现失败！";
										}
									}
									else
									{
										result = "申请提现失败！";
									}
								}
							}
						}
					}
					else
					{
						result = "账号出现问题，请您重新登陆！";
					}
				}
			}
			return result;
		}

		public int Save(string _userId, string Bank, string PayBank, string PayAccount, string PayName, decimal Money)
		{
			int result = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					string getCash = SsId.GetCash;
					decimal money = Convert.ToDecimal(Money);
					if (new UserTotalTran().MoneyOpers(getCash, _userId, money, 0, 0, 0, 2, 99, "提现申请", "提现申请成功，请耐心等待……", "会员提现", "") > 0)
					{
						SqlParameter[] values = new SqlParameter[]
						{
							new SqlParameter("@SsId", getCash),
							new SqlParameter("@UserId", _userId),
							new SqlParameter("@BankId", Bank),
							new SqlParameter("@PayBank", PayBank),
							new SqlParameter("@PayAccount", PayAccount),
							new SqlParameter("@PayName", PayName),
							new SqlParameter("@Money", Money),
							new SqlParameter("@STime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
						};
						sqlCommand.CommandText = "insert into N_UserGetCash(SsId,UserId,BankId,PayBank,PayAccount,PayName,Money,STime) values(@SsId,@UserId,@BankId,@PayBank,@PayAccount,@PayName,@Money,@STime)";
						SqlCommand expr_107 = sqlCommand;
						expr_107.CommandText += " SELECT SCOPE_IDENTITY()";
						sqlCommand.Parameters.AddRange(values);
						result = Convert.ToInt32(sqlCommand.ExecuteScalar());
						sqlCommand.Parameters.Clear();
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
				}
			}
			return result;
		}

		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("N_UserGetCash"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public void GetAdminListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("V_UserGetCash");
				string sql = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,*", "V_UserGetCash", "Id", _pagesize, _thispage, "desc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public int Check(string _cashId, string _Msg, int State)
		{
			int result = 0;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					object[] array = new object[2];
					using (DbOperHandler dbOperHandler = new ComData().Doh())
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "id=" + _cashId;
						array = dbOperHandler.GetFields("N_UserGetCash", "UserId,Money,ssId,STime");
					}
					if (State == 1)
					{
						SqlParameter[] values = new SqlParameter[]
						{
							new SqlParameter("@Id", _cashId),
							new SqlParameter("@Msg", _Msg),
							new SqlParameter("@State", 1)
						};
						sqlCommand.CommandText = "update N_UserGetCash set Msg=@Msg,STime2=getdate(),State=1 where Id=" + _cashId;
						sqlCommand.Parameters.AddRange(values);
						result = Convert.ToInt32(sqlCommand.ExecuteScalar());
						sqlCommand.Parameters.Clear();
						new UserMessageDAL().Save(sqlCommand, int.Parse(array[0].ToString()), "提现成功", "您的" + array[1].ToString() + "元提现已处理，请注意查收，如有疑问请联系在线客服！");
					}
					if (State == 2)
					{
						if (new UserTotalTran().MoneyOpers(array[2].ToString(), array[0].ToString(), -Convert.ToDecimal(array[1].ToString()), 0, 0, 0, 2, 99, "提现失败", string.Concat(new string[]
						{
							"您的",
							array[1].ToString(),
							"元提现被拒绝，拒绝理由：（",
							_Msg,
							"），如有疑问请联系在线客服！"
						}), "提现失败", array[3].ToString()) <= 0)
						{
							return 0;
						}
						SqlParameter[] values2 = new SqlParameter[]
						{
							new SqlParameter("@Id", _cashId),
							new SqlParameter("@Msg", _Msg),
							new SqlParameter("@State", 2)
						};
						sqlCommand.CommandText = "update N_UserGetCash set Msg=@Msg,STime2=getdate(),State=2 where Id=" + _cashId;
						sqlCommand.Parameters.AddRange(values2);
						result = Convert.ToInt32(sqlCommand.ExecuteScalar());
						sqlCommand.Parameters.Clear();
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
				}
			}
			return result;
		}

		public void DeleteLogs()
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "1=1";
				dbOperHandler.Delete("N_UserGetCash");
			}
		}

		protected SiteModel site;
	}
}
