using System;
using System.Data;
using System.Data.SqlClient;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.DAL
{
	public class UserBetZhDAL : ComData
	{
		public UserBetZhDAL()
		{
			this.site = new conSite().GetSite();
		}

		public void GetListJSON_ZH(int _thispage, int _pagesize, string _wherestr1, string _wherestr2, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("Flex_UserBetZh");
				string sql = SqlHelp.GetSql0("row_number() over (order by STime desc) as rowid,*,substring(Convert(varchar(20),STime,120),6,11) as ShortTime", "Flex_UserBetZh", "STime", _pagesize, _thispage, "desc", _wherestr1);
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

		public void GetListJSON_ZHDetail(int _thispage, int _pagesize, string _wherestr1, string _wherestr2, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("V_UserBetZhDetail");
				string sql = SqlHelp.GetSql0("row_number() over (order by IssueNum asc) as rowid,*,substring(Convert(varchar(20),STime2,120),6,11) as ShortTime", "V_UserBetZhDetail", "IssueNum", _pagesize, _thispage, "asc", _wherestr1);
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = sql;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = string.Concat(new string[]
				{
					"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
					PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxZhList(<#page#>);"),
					"\",",
					dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)),
					"}"
				});
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string GetWhere()
		{
			string str = "(";
			str = str + " (IssueNum >'" + new LotteryDAL().GetListNextSn(1001) + "' and LotteryId=1001)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1002) + "' and LotteryId=1002)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1003) + "' and LotteryId=1003)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1004) + "' and LotteryId=1004)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1005) + "' and LotteryId=1005)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1007) + "' and LotteryId=1007)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1008) + "' and LotteryId=1008)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2001) + "' and LotteryId=2001)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2002) + "' and LotteryId=2002)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2003) + "' and LotteryId=2003)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2004) + "' and LotteryId=2004)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2005) + "' and LotteryId=2005)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(3001) + "' and LotteryId=3001)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(3002) + "' and LotteryId=3002)";
			str = str + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(3003) + "' and LotteryId=3003)";
			str += " )";
			return str + " and zhid<>0";
		}

		public int BetCancel(string betId)
		{
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
				sqlDataAdapter.SelectCommand = sqlCommand;
				try
				{
					string str = this.GetWhere() + " and State=0 and id in(" + betId + ")";
					sqlDataAdapter.SelectCommand.CommandText = "select top 1 UserId,SsId,STime from N_UserBet where " + str + " order by Id desc";
					DataTable dataTable = new DataTable();
					sqlDataAdapter.Fill(dataTable);
					string text = dataTable.Rows[0]["UserId"].ToString();
					string ssId = dataTable.Rows[0]["SsId"].ToString();
					if (!string.IsNullOrEmpty(text))
					{
						sqlCommand.CommandText = "select isnull(sum(Total*Times),0) from N_UserBet where " + str;
						decimal money = Convert.ToDecimal(string.Concat(sqlCommand.ExecuteScalar()));
						if (new UserTotalTran().MoneyOpers(ssId, text, money, 0, 0, 0, 6, 99, "", "", "终止追号", dataTable.Rows[0]["STime"].ToString()) > 0)
						{
							sqlCommand.CommandText = "update N_UserBet set State=1 where " + str;
							sqlCommand.ExecuteNonQuery();
							result = 1;
						}
						else
						{
							result = 0;
						}
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

		protected SiteModel site;
	}
}
