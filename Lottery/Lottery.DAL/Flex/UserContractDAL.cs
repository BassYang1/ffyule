using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class UserContractDAL : ComData
	{
		public void GetContractInfoJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT * from N_User where Id=" + UserId, new object[0]);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 3)
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT * from Act_SetFHDetail where IsUsed=0", new object[0]);
						dataTable = dbOperHandler.GetDataTable();
					}
					else
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("SELECT * from Act_UserFHDetail where UserId=" + UserId, new object[0]);
						dataTable = dbOperHandler.GetDataTable();
					}
				}
				_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetBetInfoJSON(string StartTime, string EndTime, string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = string.Format("SELECT * from N_User where Id=" + UserId, new object[0]);
				DataTable dataTable = dbOperHandler.GetDataTable();
				if (dataTable.Rows.Count > 0)
				{
					if (Convert.ToInt32(dataTable.Rows[0]["UserGroup"]) == 3)
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("select isnull((select top 1 Group3 from Act_SetFHDetail with(nolock) where IsUsed=0 and Bet>=MinMoney*150000 order by MinMoney desc),0) as Per,Bet \r\n                                            from (SELECT isnull(sum(Bet),0)-isnull(sum(Cancellation),0) as Bet\r\n                                            FROM [N_UserMoneyStatAll] with(nolock)\r\n                                            where (STime>='{0} 00:00:00' and STime<'{1} 00:00:00') and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code({2})+'%') A", StartTime, EndTime, UserId);
						dataTable = dbOperHandler.GetDataTable();
						_jsonstr = base.ConverTableToJSON(dataTable);
					}
					else
					{
						dbOperHandler.Reset();
						dbOperHandler.SqlCmd = string.Format("select isnull((select top 1 Group3 from Act_UserFHDetail with(nolock) where UserId={2} and Bet>=MinMoney*150000 order by MinMoney desc),0) as Per,Bet \r\n                                            from (SELECT isnull(sum(Bet),0)-isnull(sum(Cancellation),0) as Bet\r\n                                            FROM [N_UserMoneyStatAll] with(nolock)\r\n                                            where (STime>='{0} 00:00:00' and STime<'{1} 00:00:00') and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code({2})+'%') A", StartTime, EndTime, UserId);
						dataTable = dbOperHandler.GetDataTable();
						_jsonstr = base.ConverTableToJSON(dataTable);
					}
				}
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string SaveContractState(string UserId)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "UserId=@UserId";
				dbOperHandler.AddConditionParameter("@UserId", UserId);
				dbOperHandler.AddFieldItem("IsUsed", 0);
				if (dbOperHandler.Update("Act_UserFHDetail") > 0)
				{
					return base.GetJsonResult(1, "契约签订成功！");
				}
			}
			return base.GetJsonResult(0, "契约签订失败！");
		}

		public string SaveContract(string ParentId, string UserId, decimal money, decimal per)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "UserId=" + UserId;
				dbOperHandler.Delete("Act_UserFHDetail");
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("ParentId", ParentId);
				dbOperHandler.AddFieldItem("UserId", UserId);
				dbOperHandler.AddFieldItem("MinMoney", money);
				dbOperHandler.AddFieldItem("Group3", per);
				dbOperHandler.AddFieldItem("IsUsed", 1);
				dbOperHandler.Insert("Act_UserFHDetail");
			}
			return base.JsonResult(1, "分配契约成功！");
		}
	}
}
