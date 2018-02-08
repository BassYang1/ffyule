using System;
using System.Data;
using Lottery.Collect;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;

namespace Lottery.WebApp
{
	public class ajaxHistory : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			switch (operType)
			{
			case "ajaxGetList":
				this.ajaxGetList();
				goto IL_FE;
			case "ajaxGetList_User":
				this.ajaxGetList_User();
				goto IL_FE;
			case "ajaxGetChargeCashList":
				this.ajaxGetChargeCashList();
				goto IL_FE;
			case "GetUserTotalList":
				this.GetUserTotalList();
				goto IL_FE;
			case "GetLotteryOpenList":
				this.GetLotteryOpenList();
				goto IL_FE;
			case "ajaxGetListDay":
				this.ajaxGetListDay();
				goto IL_FE;
			}
			this.DefaultResponse();
			IL_FE:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string text = base.q("d1") + " 00:00:00";
			string text2 = base.q("d2") + " 23:59:59";
			string text3 = base.q("tid");
			string text4 = base.q("sel");
			string text5 = base.q("u");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
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
			string text6 = "";
			text6 = text6 + "UserId =" + this.AdminId;
			if (!string.IsNullOrEmpty(text5))
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and ",
					text4,
					" like '%",
					text5,
					"%'"
				});
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text6 = text6 + " and Code =" + text3;
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			string response = "";
			new WebAppListOper().GetHisStoryJSON(thispage, pagesize, text6, ref response);
			this._response = response;
		}

		private void ajaxGetList_User()
		{
			string text = base.q("d1") + " 00:00:00";
			string text2 = base.q("d2") + " 23:59:59";
			string text3 = base.q("tid");
			string text4 = base.q("sel");
			string text5 = base.q("u");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
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
			string text6 = "";
			text6 = text6 + "usercode like '%" + Strings.PadLeft(this.AdminId) + "%'";
			if (!string.IsNullOrEmpty(text5))
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and ",
					text4,
					" like '%",
					text5,
					"%'"
				});
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text6 = text6 + " and Code =" + text3;
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text7 = text6;
				text6 = string.Concat(new string[]
				{
					text7,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			string response = "";
			new WebAppListOper().GetHisStoryJSON(thispage, pagesize, text6, ref response);
			this._response = response;
		}

		private void ajaxGetChargeCashList()
		{
			string text = base.q("d1") + " 00:00:00";
			string text2 = base.q("d2") + " 23:59:59";
			string text3 = base.q("u");
			int thispage = base.Int_ThisPage();
			int pagesize = base.Str2Int(base.q("pagesize"), 20);
			int num = base.Str2Int(base.q("flag"), 0);
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
			string text4 = "UserCode like '%" + Strings.PadLeft(this.AdminId) + "%'";
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = text4 + " and Uname like '%" + text3 + "%'";
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			text4 += " and Code in (1,2)";
			string response = "";
			new WebAppListOper().GetHisStoryJSON(thispage, pagesize, text4, ref response);
			this._response = response;
		}

		private void GetUserTotalList()
		{
			string str = "";
			new Lottery.DAL.Flex.UserMoneyStatDAL().GetUserTotalList(this.AdminId, ref str);
			this._response = (this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + str + "}");
		}

		public void GetLotteryOpenList()
		{
			string value = base.q("lid");
			DataTable dt = base.ConvertXMLToDataSet(Public.GetOpenListJson(Convert.ToInt32(value))).Tables[0];
			this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dt) + "}";
		}

		private void ajaxGetListDay()
		{
			string text = base.q("d1") + " 00:00:00";
			string text2 = base.q("d2") + " 23:59:59";
			string text3 = base.q("u");
			int num = base.Int_ThisPage();
			int pageSize = base.Str2Int(base.q("pagesize"), 20);
			int num2 = base.Str2Int(base.q("flag"), 0);
			if (text.Trim().Length == 0)
			{
				text = Convert.ToDateTime(this.StartTime).AddDays(-30.0).ToString("yyyy-MM-dd");
			}
			if (text2.Trim().Length == 0)
			{
				text2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
			}
			if (Convert.ToDateTime(text) > Convert.ToDateTime(text2))
			{
				text = text2;
			}
			string fldName = "STime";
			string text4 = string.Concat(new string[]
			{
				" STime >='",
				text,
				"' and STime <='",
				text2,
				"'"
			});
			if (!string.IsNullOrEmpty(text3))
			{
				text4 = text4 + " and dbo.f_GetUserName(UserId) = '" + text3 + "'";
			}
			else
			{
				text4 = text4 + " and UserId = " + this.AdminId;
			}
			this.doh.Reset();
			this.doh.ConditionExpress = text4;
			int totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
			string sql = SqlHelp.GetSql0("*,-total as total2", "V_UserMoneyStatAllDayOfUser", fldName, pageSize, num, "desc", text4);
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

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
