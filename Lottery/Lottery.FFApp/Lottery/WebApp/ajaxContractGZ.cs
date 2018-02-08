using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Entity;
using Lottery.Utils;

namespace Lottery.WebApp
{
	public class ajaxContractGZ : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.CheckFormUrl())
			{
				base.Response.End();
			}
			base.Admin_Load("master", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			switch (operType)
			{
			case "ajaxSaveContract":
				this.ajaxSaveContract();
				goto IL_19C;
			case "UpdateContractState":
				this.UpdateContractState();
				goto IL_19C;
			case "GetContractInfo":
				this.GetContractInfo();
				goto IL_19C;
			case "IsContract":
				this.IsContract();
				goto IL_19C;
			case "IsContractState":
				this.IsContractState();
				goto IL_19C;
			case "UpdateContractStateUserId":
				this.UpdateContractStateUserId();
				goto IL_19C;
			case "ajaxGetList":
				this.ajaxGetList();
				goto IL_19C;
			case "ajaxGetDetail":
				this.ajaxGetDetail();
				goto IL_19C;
			case "ajaxContractgzOperInfo":
				this.ajaxContractgzOperInfo();
				goto IL_19C;
			case "ajaxGetAgentGZRecord":
				this.ajaxGetAgentGZRecord();
				goto IL_19C;
			case "ajaxGetContractGZRecord":
				this.ajaxGetContractGZRecord();
				goto IL_19C;
			}
			this.DefaultResponse();
			IL_19C:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void IsContractState()
		{
			string response = "";
			new ContractGzDAL().IsContract(this.AdminId, " and (IsUsed=0 or IsUsed=3)", ref response);
			this._response = response;
		}

		private void IsContract()
		{
			string response = "";
			new ContractGzDAL().IsContract(this.AdminId, ref response);
			this._response = response;
		}

		private void GetContractInfo()
		{
			string text = base.q("id");
			if (string.IsNullOrEmpty(text))
			{
				text = this.AdminId;
			}
			string response = "";
			new ContractGzDAL().GetContractInfo(text, ref response);
			this._response = response;
		}

		private void UpdateContractState()
		{
			string state = base.f("state");
			this._response = new ContractGzDAL().UpdateContractState(this.AdminId, state);
		}

		private void UpdateContractStateUserId()
		{
			string state = base.f("state");
			string userId = base.f("userid");
			this._response = new ContractGzDAL().UpdateContractState(userId, state);
		}

		private void ajaxSaveContract()
		{
			HttpContext.Current.Response.ContentType = "application/json";
			HttpRequest request = HttpContext.Current.Request;
			StreamReader streamReader = new StreamReader(request.InputStream);
			string str = streamReader.ReadToEnd();
			string jsonText = HttpUtility.UrlDecode(str);
			List<ajaxContractGZ.RequestDataJSON> list = ajaxContractGZ.JSONToObject<List<ajaxContractGZ.RequestDataJSON>>(jsonText);
			ajaxContractGZ.RequestDataJSON requestDataJSON = new ajaxContractGZ.RequestDataJSON();
			try
			{
				UserContract userContract = new UserContract();
				userContract.Type = 2;
				userContract.ParentId = Convert.ToInt32(this.AdminId);
				userContract.UserId = Convert.ToInt32(list[0].userId);
				List<UserContractDetail> list2 = new List<UserContractDetail>();
				for (int i = 0; i < list.Count; i++)
				{
					requestDataJSON = list[i];
					list2.Add(new UserContractDetail
					{
						MinMoney = Convert.ToDecimal(requestDataJSON.money),
						Money = Convert.ToDecimal(requestDataJSON.per)
					});
				}
				userContract.UserContractDetails = list2;
				if (new ContractGzDAL().SaveContract(userContract) > 0)
				{
					this._response = base.JsonResult(1, "分配契约成功！");
				}
				else
				{
					this._response = base.JsonResult(0, "分配契约失败！");
				}
			}
			catch
			{
				this._response = base.JsonResult(0, "分配契约失败！");
			}
		}

		private void ajaxGetList()
		{
			string text = base.q("p");
			string text2 = base.q("u");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text3 = "type=2 and ParentId=" + this.AdminId;
			if (!string.IsNullOrEmpty(text2))
			{
				text3 = text3 + " and UserName = '" + text2 + "'";
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text3;
			int totalCount = this.doh.Count("V_UserContract");
			string sql = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,'2' as Type,*", "V_UserContract", "Id", pageSize, num, "desc", text3);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(80, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetDetail()
		{
			string str = base.q("id");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			string text = "UcId=" + str;
			this.doh.Reset();
			this.doh.ConditionExpress = text;
			int totalCount = this.doh.Count("N_UserContractDetail");
			string sql = SqlHelp.GetSql0("*", "N_UserContractDetail", "id", pageSize, num, "desc", text);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(80, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxContractgzOperInfo()
		{
			string text = base.q("id");
			DateTime now = DateTime.Now;
			string text2 = "0";
			string newValue = "0";
			string text3 = "0";
			string text4 = now.ToString("yyyy-MM-dd") + " 00:00:00";
			string text5 = now.ToString("yyyy-MM-dd") + " 23:59:59";
			this.doh.Reset();
			this.doh.SqlCmd = string.Format("SELECT \r\n                    (isnull(sum(Bet),0)-isnull(sum(Cancellation),0)) as Bet,\r\n                    isnull(sum(Bet),0)-(isnull(sum(Win),0)+isnull(sum(Give),0)+isnull(sum(Change),0)+isnull(sum(Cancellation),0)+isnull(sum(Point),0)) as Loss\r\n                    FROM [N_UserMoneyStatAll] with(nolock)\r\n                    where (STime>='{0}' and STime<='{1}') and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code({2})+'%'", text4, text5, text);
			DataTable dataTable = this.doh.GetDataTable();
			if (dataTable.Rows.Count > 0)
			{
				text2 = dataTable.Rows[0]["Bet"].ToString();
				newValue = dataTable.Rows[0]["Loss"].ToString();
			}
			this.doh.Reset();
			this.doh.SqlCmd = string.Format("SELECT [Type],[ParentId],[UserId],[IsUsed],[STime],b.* FROM [N_UserContract] a left join [N_UserContractDetail] b on a.Id=b.UcId where Type=2 and userId={0} and {1}>=MinMoney*10000 order by MinMoney desc", text, text2);
			dataTable = this.doh.GetDataTable();
			if (dataTable.Rows.Count > 0)
			{
				text3 = dataTable.Rows[0]["Money"].ToString();
			}
			string newValue2 = Convert.ToDecimal(Convert.ToDecimal(text2) * Convert.ToDecimal(text3) / 100m).ToString("0.0000");
			string text6 = "{\"starttime\": \"开始时间\",\"endtime\": \"截止时间\",\"bet\": \"销量\",\"loss\": \"亏损\",\"per\": \"比例\",\"money\": \"金额\"}";
			text6 = text6.Replace("开始时间", text4).Replace("截止时间", text5);
			text6 = text6.Replace("销量", text2).Replace("亏损", newValue);
			text6 = text6.Replace("比例", text3).Replace("金额", newValue2);
			this._response = text6;
		}

		private void ajaxGetAgentGZRecord()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = this.StartTime;
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text3 = "UserId=" + this.AdminId;
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text4 = text3;
				text3 = string.Concat(new string[]
				{
					text4,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text3;
			int totalCount = this.doh.Count("Act_ActiveRecord");
			string sql = SqlHelp.GetSql0("*,dbo.f_GetUserName(UserId) as UserName,(select count(*) from N_UserContract where ParentId=a.userId and Type=1) as contractcount", "Act_ActiveRecord a", "id", pageSize, num, "desc", text3);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(80, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		private void ajaxGetContractGZRecord()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = this.StartTime;
			}
			if (text2.Trim().Length == 0)
			{
				text2 = this.EndTime;
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string text3 = "ActiveType='ActGongziContract' and dbo.f_GetUserCode(UserId) like '%," + this.AdminId + ",%'";
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text4 = text3;
				text3 = string.Concat(new string[]
				{
					text4,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text3;
			int totalCount = this.doh.Count("Act_ActiveRecord");
			string sql = SqlHelp.GetSql0("*,dbo.f_GetUserName(UserId) as UserName,(select count(*) from N_UserContract where ParentId=a.userId and Type=1) as contractcount", "Act_ActiveRecord a", "id", pageSize, num, "desc", text3);
			this.doh.Reset();
			this.doh.SqlCmd = sql;
			DataTable dataTable = this.doh.GetDataTable();
			this._response = string.Concat(new string[]
			{
				"{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"",
				PageBar.GetPageBar(80, "js", 2, totalCount, pageSize, num, "javascript:ajaxList(<#page#>);"),
				"\",",
				dtHelp.DT2JSON(dataTable),
				"}"
			});
			dataTable.Clear();
			dataTable.Dispose();
		}

		public static T JSONToObject<T>(string jsonText)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			T result;
			try
			{
				result = javaScriptSerializer.Deserialize<T>(jsonText);
			}
			catch (Exception ex)
			{
				throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
			}
			return result;
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;

		[Serializable]
		public class RequestDataJSON
		{
			public string userId
			{
				get;
				set;
			}

			public decimal money
			{
				get;
				set;
			}

			public decimal per
			{
				get;
				set;
			}
		}
	}
}
