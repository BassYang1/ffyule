using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Utils;

namespace Lottery.DAL.Flex
{
	public class UserBankDAL : ComData
	{
		public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr1;
				int totalCount = dbOperHandler.Count("N_UserBank");
				string sql = SqlHelp.GetSql0("row_number() over (order by Id asc) as rowid,'************'+substring(Payaccount,len(Payaccount)-3,4) as tPayaccount,substring(PayName,1,1)+'**' as tPayName,*", "N_UserBank", "Id", _pagesize, _thispage, "asc", _wherestr1);
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

		public void GetBankInfoJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select * from V_UserBankInfo where UserId=" + UserId;
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = base.ConverTableToJSON(dataTable);
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetIphoneBankInfoJSON(string UserId, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "UserId=" + UserId;
				dbOperHandler.Count("N_UserBank");
				dbOperHandler.Reset();
				dbOperHandler.SqlCmd = "select top 3 * from V_UserBankInfo where UserId=" + UserId;
				DbOperHandler expr_46 = dbOperHandler;
				expr_46.SqlCmd += "order by Id desc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetChargeSetJSON(ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where IsUsed=0 and Id<>1020";
				DbOperHandler expr_17 = dbOperHandler;
				expr_17.SqlCmd += " ORDER BY Sort asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public void GetIphoneChargeSetJSON(string Id, ref string _jsonstr)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where Id in (" + Id + ")";
				DbOperHandler expr_22 = dbOperHandler;
				expr_22.SqlCmd += " ORDER BY Id asc";
				DataTable dataTable = dbOperHandler.GetDataTable();
				_jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
				dataTable.Clear();
				dataTable.Dispose();
			}
		}

		public string Save(string userId, string PayMethod, string PayBank, string PayBankAddress, string PayAccount, string PayName, string Question, string Answer)
		{
			if (this.Exists(" PayAccount='" + PayAccount + "'"))
			{
				return base.GetJsonResult(0, "绑定失败,一张银行卡只能绑一个帐户！");
			}
			string jsonResult;
			if (this.Exists(" UserId=" + userId))
			{
				if (!this.Exists(" PayName='" + PayName + "' and UserId=" + userId))
				{
					return base.GetJsonResult(0, "绑定失败,同一账户下只能绑定相同的开户名卡号！");
				}
				using (DbOperHandler dbOperHandler = new ComData().Doh())
				{
					dbOperHandler.Reset();
					dbOperHandler.ConditionExpress = "UserId=" + userId;
					dbOperHandler.AddFieldItem("PayMethod", PayMethod);
					dbOperHandler.AddFieldItem("PayBank", PayBank);
					dbOperHandler.AddFieldItem("PayBankAddress", PayBankAddress);
					dbOperHandler.AddFieldItem("PayAccount", PayAccount);
					dbOperHandler.AddFieldItem("PayName", PayName);
					dbOperHandler.AddFieldItem("AddTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					dbOperHandler.AddFieldItem("IsLock", 1);
					if (dbOperHandler.Update("N_UserBank") > 0)
					{
						new UserDAL().UpdateInfo(userId, Question, Answer);
						jsonResult = base.GetJsonResult(1, "银行资料绑定成功！");
						return jsonResult;
					}
					jsonResult = base.GetJsonResult(0, "银行资料绑定失败！");
					return jsonResult;
				}
			}
			using (DbOperHandler dbOperHandler2 = new ComData().Doh())
			{
				dbOperHandler2.Reset();
				dbOperHandler2.AddFieldItem("UserId", userId);
				dbOperHandler2.AddFieldItem("PayMethod", PayMethod);
				dbOperHandler2.AddFieldItem("PayBank", PayBank);
				dbOperHandler2.AddFieldItem("PayBankAddress", PayBankAddress);
				dbOperHandler2.AddFieldItem("PayAccount", PayAccount);
				dbOperHandler2.AddFieldItem("PayName", PayName);
				dbOperHandler2.AddFieldItem("AddTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				dbOperHandler2.AddFieldItem("IsLock", 1);
				if (dbOperHandler2.Insert("N_UserBank") > 0)
				{
					new UserDAL().UpdateInfo(userId, Question, Answer);
					jsonResult = base.GetJsonResult(1, "银行资料绑定成功！");
				}
				else
				{
					jsonResult = base.GetJsonResult(0, "银行资料绑定失败！");
				}
			}
			return jsonResult;
		}

		public string Save(string userId, string PayMethod, string PayBank, string PayBankAddress, string PayAccount, string PayName)
		{
			if (this.Exists(" PayAccount='" + PayAccount + "'"))
			{
				return base.GetJsonResult(0, "绑定失败,一张银行卡只能绑一个帐户！");
			}
			if (this.Exists(" UserId=" + userId) && !this.Exists(" PayName='" + PayName + "' and UserId=" + userId))
			{
				return base.GetJsonResult(0, "绑定失败,同一账户下只能绑定相同的开户名卡号！");
			}
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.AddFieldItem("UserId", userId);
				dbOperHandler.AddFieldItem("PayMethod", PayMethod);
				dbOperHandler.AddFieldItem("PayBank", PayBank);
				dbOperHandler.AddFieldItem("PayBankAddress", PayBankAddress);
				dbOperHandler.AddFieldItem("PayAccount", PayAccount);
				dbOperHandler.AddFieldItem("PayName", PayName);
				dbOperHandler.AddFieldItem("AddTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				dbOperHandler.AddFieldItem("IsLock", 1);
				if (dbOperHandler.Insert("N_UserBank") > 0)
				{
					jsonResult = base.GetJsonResult(1, "银行资料绑定成功！");
				}
				else
				{
					jsonResult = base.GetJsonResult(0, "银行资料绑定失败！");
				}
			}
			return jsonResult;
		}

		public string Save(string userId, string PayMethod, string PayBank, string PayBankAddress, string PayAccount, string PayName, string strPwd)
		{
			if (this.Exists(" PayAccount='" + PayAccount + "'"))
			{
				return base.GetJsonResult(0, "绑定失败,一张银行卡只能绑一个帐户！");
			}
			if (this.Exists(" UserId=" + userId) && !this.Exists(" PayName='" + PayName + "' and UserId=" + userId))
			{
				return base.GetJsonResult(0, "绑定失败,同一账户下只能绑定相同的开户名卡号！");
			}
			string jsonResult;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=@id";
				dbOperHandler.AddConditionParameter("@id", userId);
				object field = dbOperHandler.GetField("N_User", "PayPass");
				if (!MD5.Last64(strPwd).Equals(field.ToString()))
				{
					jsonResult = base.GetJsonResult(0, "绑定失败,您的提现密码错误！");
				}
				else
				{
					dbOperHandler.Reset();
					dbOperHandler.AddFieldItem("UserId", userId);
					dbOperHandler.AddFieldItem("PayMethod", PayMethod);
					dbOperHandler.AddFieldItem("PayBank", PayBank);
					dbOperHandler.AddFieldItem("PayBankAddress", PayBankAddress);
					dbOperHandler.AddFieldItem("PayAccount", PayAccount);
					dbOperHandler.AddFieldItem("PayName", PayName);
					dbOperHandler.AddFieldItem("AddTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					dbOperHandler.AddFieldItem("IsLock", 1);
					if (dbOperHandler.Insert("N_UserBank") > 0)
					{
						jsonResult = base.GetJsonResult(1, "银行资料绑定成功！");
					}
					else
					{
						jsonResult = base.GetJsonResult(0, "银行资料绑定失败！");
					}
				}
			}
			return jsonResult;
		}

		public bool Exists(string _wherestr)
		{
			int num = 0;
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = _wherestr;
				if (dbOperHandler.Exist("N_UserBank"))
				{
					num = 1;
				}
			}
			return num == 1;
		}

		public void Delete(string Id)
		{
			using (DbOperHandler dbOperHandler = new ComData().Doh())
			{
				dbOperHandler.Reset();
				dbOperHandler.ConditionExpress = "id=" + Id;
				dbOperHandler.Delete("N_UserBank");
			}
		}
	}
}
