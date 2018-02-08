using System;
using Lottery.DAL;
using Lottery.Utils;

namespace Lottery.IPhone
{
	public class ajaxHistory : UserCenterSession
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Admin_Load("", "json");
			this._operType = base.q("oper");
			string operType = this._operType;
			if (operType != null)
			{
				if (operType == "ajaxGetList")
				{
					this.ajaxGetList();
					goto IL_64;
				}
				if (operType == "ajaxGetChargeCashList")
				{
					this.ajaxGetChargeCashList();
					goto IL_64;
				}
			}
			this.DefaultResponse();
			IL_64:
			base.Response.Write(this._response);
		}

		private void DefaultResponse()
		{
			this._response = base.JsonResult(0, "未知操作");
		}

		private void ajaxGetList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string text3 = base.q("lid");
			string text4 = base.q("sid");
			string text5 = base.q("tid");
			string text6 = base.q("u");
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
			string text7 = "";
			if (string.IsNullOrEmpty(text6))
			{
				text7 = text7 + "UserId =" + this.AdminId;
			}
			else
			{
				string text8 = text7;
				text7 = string.Concat(new string[]
				{
					text8,
					"dbo.f_GetUserCode(UserId) like '%",
					Strings.PadLeft(this.AdminId),
					"%' and UserId<>",
					this.AdminId
				});
				text7 = text7 + " and Uname like '%" + text6 + "%'";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text7 = text7 + " and LotteryId =" + text3;
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text7 = text7 + " and SingleMoney ='" + text4 + "'";
			}
			if (!string.IsNullOrEmpty(text5))
			{
				text7 = text7 + " and Code =" + text5;
			}
			if (text.Trim().Length > 0 && text2.Trim().Length > 0)
			{
				string text8 = text7;
				text7 = string.Concat(new string[]
				{
					text8,
					" and STime >='",
					text,
					"' and STime <='",
					text2,
					"'"
				});
			}
			string response = "";
			new HistoryDAL().GetListJSON(thispage, pagesize, text7, ref response);
			this._response = response;
		}

		private void ajaxGetChargeCashList()
		{
			string text = base.q("d1");
			string text2 = base.q("d2");
			string value = base.q("type");
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
			string text4 = "";
			if (string.IsNullOrEmpty(value))
			{
				text4 = text4 + "UserId =" + this.AdminId;
			}
			else
			{
				string text5 = text4;
				text4 = string.Concat(new string[]
				{
					text5,
					"dbo.f_GetUserCode(UserId) like '%",
					Strings.PadLeft(this.AdminId),
					"%' and UserId<>",
					this.AdminId
				});
				text4 = text4 + " and Uname like '%" + text3 + "%'";
			}
			if (string.IsNullOrEmpty(text3))
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
			text4 += " and Code in (1,2,3,10,11,15)";
			string response = "";
			new HistoryDAL().GetListJSON(thispage, pagesize, text4, ref response);
			this._response = response;
		}

		private string _operType = string.Empty;

		private string _response = string.Empty;
	}
}
