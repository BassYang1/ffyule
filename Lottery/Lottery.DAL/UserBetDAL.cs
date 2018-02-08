using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class UserBetDAL : ComData
	{
		public UserBetDAL()
		{
			this.site = new conSite().GetSite();
		}

		public string GetWhere()
		{
			string str = "(";
			str = str + " (IssueNum <='" + new LotteryDAL().GetListNextSn(1001) + "' and LotteryId=1001)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1002) + "' and LotteryId=1002)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1003) + "' and LotteryId=1003)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1004) + "' and LotteryId=1004)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1005) + "' and LotteryId=1005)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1007) + "' and LotteryId=1007)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1008) + "' and LotteryId=1008)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1009) + "' and LotteryId=1009)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2001) + "' and LotteryId=2001)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2002) + "' and LotteryId=2002)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2003) + "' and LotteryId=2003)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2004) + "' and LotteryId=2004)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2005) + "' and LotteryId=2005)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2006) + "' and LotteryId=2006)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(3001) + "' and LotteryId=3001)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(3002) + "' and LotteryId=3002)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(3003) + "' and LotteryId=3003)";
			str = str + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(4001) + "' and LotteryId=4001)";
			str += " )";
			return str + " and ((zhid<>0 and state<>1) or (zhid=0))";
		}

		public string GetCurWhere()
		{
			string str = "(";
			str = str + " (IssueNum ='" + new LotteryDAL().GetListNextSn(1001) + "' and LotteryId=1001)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1002) + "' and LotteryId=1002)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1003) + "' and LotteryId=1003)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1004) + "' and LotteryId=1004)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1005) + "' and LotteryId=1005)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1007) + "' and LotteryId=1007)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1008) + "' and LotteryId=1008)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1009) + "' and LotteryId=1009)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2001) + "' and LotteryId=2001)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2002) + "' and LotteryId=2002)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2003) + "' and LotteryId=2003)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2004) + "' and LotteryId=2004)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2005) + "' and LotteryId=2005)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2006) + "' and LotteryId=2006)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(3001) + "' and LotteryId=3001)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(3002) + "' and LotteryId=3002)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(3003) + "' and LotteryId=3003)";
			str = str + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(4001) + "' and LotteryId=4001)";
			str += " )";
			return str + " and ((zhid<>0 and state<>1) or (zhid=0))";
		}

		public string GetWQWhere()
		{
			string str = "(";
			str = str + " (IssueNum <'" + new LotteryDAL().GetCurrentSn(1001) + "' and LotteryId=1001)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1002) + "' and LotteryId=1002)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1003) + "' and LotteryId=1003)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1004) + "' and LotteryId=1004)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1005) + "' and LotteryId=1005)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1007) + "' and LotteryId=1007)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1008) + "' and LotteryId=1008)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1009) + "' and LotteryId=1009)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2001) + "' and LotteryId=2001)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2002) + "' and LotteryId=2002)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2003) + "' and LotteryId=2003)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2004) + "' and LotteryId=2004)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2005) + "' and LotteryId=2005)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2006) + "' and LotteryId=2006)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(3001) + "' and LotteryId=3001)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(3002) + "' and LotteryId=3002)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(3003) + "' and LotteryId=3003)";
			str = str + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(4001) + "' and LotteryId=4001)";
			return str + " )";
		}

		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, string _wherestr2, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("Flex_UserBet");
				string sql = SqlHelp.GetSql0(_wherestr2 + "as isme,row_number() over (order by Id desc) as rowid,Id,SsId,UserId,UserName,UserMoney,PlayId,PlayName,PlayCode,LotteryId,LotteryName,IssueNum,SingleMoney,moshi,Times,Num,DX,DS,cast(Times*Total as decimal(15,4)) as Total,Point,PointMoney,Bonus,Bonus2,WinNum,WinBonus,RealGet,Pos,STime,STime2,substring(Convert(varchar(20),STime2,120),6,11) as ShortTime,IsOpen,State,IsWin,number,poslen", "Flex_UserBet", "Id", _pagesize, _thispage, "desc", _wherestr1);
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

		public void BetCancelOfIssue(string IssueNum)
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
					sqlDataAdapter.SelectCommand.CommandText = "select Id,ssid,UserId,IssueNum,LotteryId,PlayId,Total,Times,STime from N_UserBet where IssueNum='" + IssueNum + "'";
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						string ssId = dataTable.Rows[i]["ssid"].ToString();
						string userId = dataTable.Rows[i]["UserId"].ToString();
						int logLotteryId = Convert.ToInt32(dataTable.Rows[i]["LotteryId"].ToString());
						int logPlayId = Convert.ToInt32(dataTable.Rows[i]["PlayId"].ToString());
						int num = Convert.ToInt32(dataTable.Rows[i]["Id"].ToString());
						decimal d = Convert.ToDecimal(dataTable.Rows[i]["Total"].ToString());
						decimal d2 = Convert.ToDecimal(dataTable.Rows[i]["Times"].ToString());
						decimal money = d * d2;
						if (new UserTotalTran().MoneyOpers(ssId, userId, money, logLotteryId, logPlayId, num, 6, 99, string.Empty, string.Empty, "会员撤单", dataTable.Rows[i]["STime"].ToString()) > 0)
						{
							sqlCommand.CommandText = "update N_UserBet set State=1 where Id=" + num;
							sqlCommand.ExecuteNonQuery();
						}
					}
				}
				catch (Exception ex)
				{
					new LogExceptionDAL().Save("系统异常", ex.Message);
				}
			}
		}

		public int BetCancel(string betId)
		{
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				try
				{
					object[] array = new object[6];
					using (DbOperHandler dbOperHandler = new ComData().Doh())
					{
						dbOperHandler.Reset();
						dbOperHandler.ConditionExpress = "Id=@Id";
						dbOperHandler.AddConditionParameter("@Id", betId);
						array = dbOperHandler.GetFields("N_UserBet", "UserId,IssueNum,LotteryId,PlayId,Total,Times,ssid,STime");
					}
					decimal money = Convert.ToDecimal(Convert.ToDecimal(array[4]) * Convert.ToDecimal(array[5]));
					if (new UserTotalTran().MoneyOpers(array[6].ToString(), array[0].ToString(), money, Convert.ToInt32(array[2].ToString()), Convert.ToInt32(array[3].ToString()), Convert.ToInt32(betId), 6, 99, string.Empty, string.Empty, "会员撤单", array[7].ToString()) > 0)
					{
						sqlCommand.CommandText = "update N_UserBet set State=1 where Id=" + betId;
						sqlCommand.ExecuteNonQuery();
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

		public void BetCheat(string betId)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "Id=@Id";
				dbOperHandler.AddConditionParameter("@Id", betId);
				dbOperHandler.AddFieldItem("IsCheat", "1");
				dbOperHandler.Update("N_UserBet");
			}
		}

		protected SiteModel site;
	}
}
